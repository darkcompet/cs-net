namespace Tool.Compet.AutoDI;

/// <summary>
/// Dependecy Injection.
/// - Singleton: IoC container will create and share a single instance of a service throughout the application's lifetime.
/// - Transient: The IoC container will create a new instance of the specified service type every time you ask for it.
/// - Scoped: IoC container will create an instance of the specified service type once per request and will be shared in a single request.
/// Ref: https://www.tutorialsteacher.com/core/aspnet-core-introduction
/// </summary>
public static class RegisterClassesExtension {
	/// <summary>
	/// Registers all classes which have [RegisterClassAsScoped], [RegisterClassAsSingleton] or [RegisterClassAsTransient]
	/// above them automatically. This scans all assemblies but only registers classes which have one of these attributes.
	/// Having multiple attributes will cause the first one to get used. Using [RegisterClass] registers the class as
	/// transient.
	/// </summary>
	/// <param name="services"></param>
	/// <returns>A string where you can see a list of registered classes if you put a breakpoint over the implementation</returns>
	public static string AutoRegisterDependencies(this IServiceCollection services) {
		return RegisterDependenciesService.Register(services);
	}
}
