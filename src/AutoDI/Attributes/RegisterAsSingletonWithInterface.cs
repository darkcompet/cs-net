namespace Tool.Compet.AutoDI;

using Microsoft.Extensions.DependencyInjection;

public class RegisterAsSingletonWithInterface : AutoDIRegistrationAttribute {
	public static readonly string FullName = typeof(RegisterAsSingletonWithInterface).FullName!;

	public RegisterAsSingletonWithInterface() {
		this.serviceLifetime = ServiceLifetime.Singleton;
	}
}
