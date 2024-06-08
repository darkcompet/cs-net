namespace Tool.Compet.AutoDI;

using Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Register the class as scoped with interface.
/// </summary>
public class RegisterAsScopedWithInterface : AutoDIRegistrationAttribute {
	public static readonly string FullName = typeof(RegisterAsScopedWithInterface).FullName!;

	public RegisterAsScopedWithInterface() {
		this.serviceLifetime = ServiceLifetime.Scoped;
	}
}
