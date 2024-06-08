namespace Tool.Compet.AutoDI;

using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Dependecy Injection.
/// - Singleton: IoC container will create and share a single instance of a service throughout the application's lifetime.
/// - Transient: The IoC container will create a new instance of the specified service type every time you ask for it.
/// - Scoped: IoC container will create an instance of the specified service type once per request and will be shared in a single request.
/// Ref: https://www.tutorialsteacher.com/core/aspnet-core-introduction
/// </summary>
public static class AutoDIService {
	/// <summary>
	/// Registers all classes which have <see cref="RegisterAsScoped"/>, <see cref="RegisterAsSingleton"/> or <see cref="RegisterAsTransient"/>
	/// above them automatically. This scans all assemblies but only registers classes which have one of these attributes.
	/// Having multiple attributes will cause the first one to get used. Using [RegisterClass] registers the class as
	/// transient.
	/// </summary>
	/// <param name="services"></param>
	public static void AutoRegisterDependencies(this IServiceCollection services) {
		var assemblies = GetAssemblies();
		var registeredServices = FindRegisteredClassesByAttribute(assemblies);

		Console.WriteLine($"------ registeredServices: {registeredServices.Count()}");
		foreach (var regService in registeredServices) {
			if (regService.serviceType is null) {
				Console.WriteLine($"------------> service.ClassType is null");
				continue;
			}
			if (regService.ignoreInterface) {
				AddServiceWithoutInterface(regService, services);
			}
			else if (regService.interfaceTypes.Any()) {
				AddServiceWithInterface(regService, services);
			}
			// else if (service is { ClassType: { }, IgnoreInterface: true }) {
			// 	AddServiceWithoutInterface(service, serviceCollection);
			// }
		}

		// Log.Logger.Information("{OverallCount} services were registered. {SingletonCount} singleton, {ScopedCount} scoped, {TransientCount} transient.",
		// 	services.Count(),
		// 	services.Count(x => x.ServiceLifetime == ServiceLifetime.Singleton),
		// 	services.Count(x => x.ServiceLifetime == ServiceLifetime.Scoped),
		// 	services.Count(x => x.ServiceLifetime == ServiceLifetime.Transient)
		// );
	}

	public static IEnumerable<Assembly> GetAssemblies() {
		var assemblies = new List<Assembly>();
		foreach (var assemblyFilePath in Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.dll")) {
			// try {
			assemblies.Add(Assembly.Load(AssemblyName.GetAssemblyName(assemblyFilePath)));
			// }
			// catch (BadImageFormatException e) {
			// 	Log.Logger.Error("{Assembly} could not be loaded", e.Source);
			// }
		}
		return assemblies;
	}

	public static IEnumerable<RegisterServiceInfo> FindRegisteredClassesByAttribute(IEnumerable<Assembly> assembly) {
		var classes = assembly
			.SelectMany(x => x.GetExportedTypes())
			.Where(FilterTargetService)
		;
		// Map assemblies to services
		var serviceInfos = new List<RegisterServiceInfo>();
		foreach (var type in classes) {
			var customAttribute = type.CustomAttributes.FirstOrDefault(att => IsRegisterable(att.AttributeType.FullName));
			if (customAttribute != null) {
				var attributeName = customAttribute.AttributeType.FullName!;
				var serviceLifetime = GetServiceLifetime(attributeName);
				var ignoreInterface = IsIgnoreInterface(attributeName);

				var typeInfo = type.GetTypeInfo();
				var registerServiceInfo = new RegisterServiceInfo {
					serviceType = typeInfo,
					interfaceTypes = typeInfo.ImplementedInterfaces,
					serviceLifetime = serviceLifetime,
					ignoreInterface = ignoreInterface
				};
				serviceInfos.Add(registerServiceInfo);
				// Console.WriteLine($"--> sinfo: {sinfo.serviceType.FullName}, {sinfo.ignoreInterface}, {sinfo.serviceLifetime}");
			}
		}
		return serviceInfos;
	}

	private static bool IsRegisterable(string? attributeName) {
		return attributeName != null && (
			attributeName.Contains(nameof(RegisterAsScoped)) ||
			attributeName.Contains(nameof(RegisterAsSingleton)) ||
			attributeName.Contains(nameof(RegisterAsTransient)) ||
			attributeName.Contains(nameof(RegisterAsScopedIgnoreInterface)) ||
			attributeName.Contains(nameof(RegisterAsSingletonIgnoreInterface)) ||
			attributeName.Contains(nameof(RegisterAsTransientIgnoreInterface))
		);
	}

	private static bool IsIgnoreInterface(string attributeName) {
		return attributeName.Contains(nameof(RegisterAsScopedIgnoreInterface)) ||
			attributeName.Contains(nameof(RegisterAsSingletonIgnoreInterface)) ||
			attributeName.Contains(nameof(RegisterAsTransientIgnoreInterface))
		;
	}

	private static bool FilterTargetService(Type type) {
		return
			!type.IsAbstract &&
			!type.IsGenericType &&
			!type.IsNested &&
			type.GetCustomAttributes(typeof(AutoDependencyRegistrationAttribute), true).Length > 0
		;
	}

	private static ServiceLifetime GetServiceLifetime(string attributeName) {
		if (attributeName.Contains(nameof(RegisterAsScoped)) || attributeName.Contains(nameof(RegisterAsScopedIgnoreInterface))) {
			return ServiceLifetime.Scoped;
		}
		if (attributeName.Contains(nameof(RegisterAsSingleton)) || attributeName.Contains(nameof(RegisterAsSingletonIgnoreInterface))) {
			return ServiceLifetime.Singleton;
		}
		return ServiceLifetime.Transient;
	}

	private static void AddServiceWithInterface(
		RegisterServiceInfo serviceInfo,
		IServiceCollection services
	) {
		foreach (var implementation in serviceInfo.interfaceTypes) {
			services.Add(new ServiceDescriptor(
				implementation,
				serviceInfo.serviceType,
				serviceInfo.serviceLifetime)
			);
			Console.WriteLine($"---> Register as {serviceInfo.serviceLifetime}: {serviceInfo.serviceType.FullName}");

			// var message = $"{serviceInfo.ClassType?.Name}, {implementation} has been registered as {serviceInfo.ServiceLifetime}. ";
			// classesRegistered.AppendLine(message);
			// Log.Logger.Information("{Message}", message);
		}
	}

	private static void AddServiceWithoutInterface(
		RegisterServiceInfo serviceInfo,
		IServiceCollection services
	) {
		services.Add(new ServiceDescriptor(
			serviceInfo.serviceType!,
			serviceInfo.serviceType!,
			serviceInfo.serviceLifetime)
		);
		Console.WriteLine($"---> Register as {serviceInfo.serviceLifetime}: {serviceInfo.serviceType.FullName}");

		// var message = $"{serviceInfo.ClassType?.Name} has been registered as {serviceInfo.ServiceLifetime}. ";
		// classesRegistered.AppendLine(message);
		// Log.Logger.Information("{Message}", message);
	}
}
