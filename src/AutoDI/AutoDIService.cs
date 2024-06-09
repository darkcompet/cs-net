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
		var registeredServices = FindRegisteredServicesByAttribute(assemblies);

		foreach (var serviceInfo in registeredServices) {
			// Register without interface
			if (serviceInfo.ignoreInterface) {
				services.Add(new ServiceDescriptor(
					serviceInfo.serviceType!,
					serviceInfo.serviceType!,
					serviceInfo.serviceLifetime
				));
			}
			// Register with interface
			else {
				foreach (var interfaceType in serviceInfo.interfaceTypes) {
					services.Add(new ServiceDescriptor(
						interfaceType,
						serviceInfo.serviceType,
						serviceInfo.serviceLifetime
					));
				}
			}
		}
	}

	/// Get .dll assembly file that be used to reflect.
	private static List<Assembly> GetAssemblies() {
		var assemblies = new List<Assembly>();
		foreach (var assemblyFilePath in Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.dll")) {
			assemblies.Add(Assembly.Load(AssemblyName.GetAssemblyName(assemblyFilePath)));
		}
		return assemblies;
	}

	/// From assembly, find info of services that be registered by our attributes.
	private static List<RegisterServiceInfo> FindRegisteredServicesByAttribute(IEnumerable<Assembly> assembly) {
		var serviceTypes = assembly
			.SelectMany(x => x.GetExportedTypes())
			.Where(FilterOurAutoDIService)
		;
		// Map assemblies to services
		var serviceInfos = new List<RegisterServiceInfo>();
		foreach (var stype in serviceTypes) {
			var customAttribute = stype.CustomAttributes.FirstOrDefault(att => IsAutoRegisterAttribute(att.AttributeType.FullName));
			if (customAttribute != null) {
				var attributeFullName = customAttribute.AttributeType.FullName!;
				var typeInfo = stype.GetTypeInfo();

				serviceInfos.Add(new() {
					serviceType = typeInfo,
					interfaceTypes = typeInfo.ImplementedInterfaces,
					serviceLifetime = CalcServiceLifetime(attributeFullName),
					ignoreInterface = IsIgnoreInterfaceAttribute(attributeFullName)
				});
			}
		}
		return serviceInfos;
	}

	/// We only target to class that be annotated with our <see cref="AutoDIRegistrationAttribute">.
	private static bool FilterOurAutoDIService(Type type) {
		return
			!type.IsAbstract && !type.IsGenericType && !type.IsNested &&
			type.GetCustomAttribute(AutoDIRegistrationAttribute.AttributeType, true) != null
		;
	}

	/// Check whether the attribute is our attribute or not.
	private static bool IsAutoRegisterAttribute(string? attributeFullName) {
		return attributeFullName != null && (
			attributeFullName == RegisterAsScoped.FullName ||
			attributeFullName == RegisterAsSingleton.FullName ||
			attributeFullName == RegisterAsTransient.FullName ||
			attributeFullName == RegisterAsScopedWithInterfaces.FullName ||
			attributeFullName == RegisterAsSingletonWithInterfaces.FullName ||
			attributeFullName == RegisterAsTransientWithInterfaces.FullName
		);
	}

	/// Check whether the attribute does ignore register interface or not.
	private static bool IsIgnoreInterfaceAttribute(string attributeFullName) {
		return attributeFullName == RegisterAsScoped.FullName ||
			attributeFullName == RegisterAsSingleton.FullName ||
			attributeFullName == RegisterAsTransient.FullName
		;
	}

	/// Calculate service lifetime from attribute fullname.
	private static ServiceLifetime CalcServiceLifetime(string attributeFullName) {
		if (attributeFullName == RegisterAsScoped.FullName || attributeFullName == RegisterAsScopedWithInterfaces.FullName) {
			return ServiceLifetime.Scoped;
		}
		if (attributeFullName == RegisterAsSingleton.FullName || attributeFullName == RegisterAsSingletonWithInterfaces.FullName) {
			return ServiceLifetime.Singleton;
		}
		if (attributeFullName == RegisterAsTransient.FullName || attributeFullName == RegisterAsTransientWithInterfaces.FullName) {
			return ServiceLifetime.Transient;
		}
		throw new InvalidDataException("Invalid attribute: " + attributeFullName);
	}
}
