namespace Tool.Compet.AutoDI;

using Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Register the class as scoped with interface.
/// </summary>
public class RegisterAsSingletonWithInterface : AutoDIRegistrationAttribute {
	public static readonly string FullName = typeof(RegisterAsSingletonWithInterface).FullName!;

	public RegisterAsSingletonWithInterface() {
		this.serviceLifetime = ServiceLifetime.Singleton;
	}
}
