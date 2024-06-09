namespace Tool.Compet.AutoDI;

using Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Register the class as scoped with interfaces.
/// </summary>
public class RegisterAsSingletonWithInterfaces : AutoDIRegistrationAttribute {
	public static readonly string FullName = typeof(RegisterAsSingletonWithInterfaces).FullName!;

	public RegisterAsSingletonWithInterfaces() {
		this.serviceLifetime = ServiceLifetime.Singleton;
	}
}
