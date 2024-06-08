namespace Tool.Compet.AutoDI;

using Microsoft.Extensions.DependencyInjection;

public class RegisterAsScopedWithInterface : AutoDIRegistrationAttribute {
	public static readonly string FullName = typeof(RegisterAsScopedWithInterface).FullName!;

	public RegisterAsScopedWithInterface() {
		this.serviceLifetime = ServiceLifetime.Scoped;
	}
}
