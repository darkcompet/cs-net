namespace Tool.Compet.AutoDI;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

public static class RegisterDependenciesService {
	public static string Register(IServiceCollection serviceCollection) {
		var assemblies = RegisterDependenciesHelper.GetAssemblies();
		var filterClasses = RegisterDependenciesHelper.FindRegisteredClassesByAttribute(assemblies);
		return RegisterServices(filterClasses, serviceCollection);
	}

	private static string RegisterServices(IEnumerable<ClassesToRegister> services, IServiceCollection serviceCollection) {
		Log.Logger = new LoggerConfiguration().WriteTo.Console().CreateBootstrapLogger();

		var classesRegistered = new StringBuilder();

		foreach (var service in services) {
			if (service.ClassName != null && service.InterfaceName.Any() && !service.IgnoreInterface) {
				AddServiceWithInterface(service, serviceCollection, classesRegistered);
			}
			else if ((service.ClassName != null && !service.InterfaceName.Any()) || service is { ClassName: { }, IgnoreInterface: true }) {
				AddServiceWithoutInterface(service, serviceCollection, classesRegistered);
			}
		}

		Log.Logger.Information("{OverallCount} services were registered. {SingletonCount} singleton, {ScopedCount} scoped, {TransientCount} transient.",
			services.Count(),
			services.Count(x => x.ServiceLifetime == ServiceLifetime.Singleton),
			services.Count(x => x.ServiceLifetime == ServiceLifetime.Scoped),
			services.Count(x => x.ServiceLifetime == ServiceLifetime.Transient)
		);

		return classesRegistered.ToString();
	}

	private static void AddServiceWithInterface(
			ClassesToRegister service,
			IServiceCollection serviceCollection,
			StringBuilder classesRegistered) {
		foreach (var implementation in service.InterfaceName) {
			serviceCollection.Add(new ServiceDescriptor(
				implementation,
				service.ClassName!,
				service.ServiceLifetime)
			);

			var message = $"{service.ClassName?.Name}, {implementation} has been registered as {service.ServiceLifetime}. ";
			classesRegistered.AppendLine(message);
			Log.Logger.Information("{Message}", message);
		}
	}

	private static void AddServiceWithoutInterface(
		ClassesToRegister service,
		IServiceCollection serviceCollection,
		StringBuilder classesRegistered
	) {
		serviceCollection.Add(new ServiceDescriptor(
			service.ClassName!,
			service.ClassName!,
			service.ServiceLifetime)
		);

		var message = $"{service.ClassName?.Name} has been registered as {service.ServiceLifetime}. ";
		classesRegistered.AppendLine(message);
		Log.Logger.Information("{Message}", message);
	}
}
