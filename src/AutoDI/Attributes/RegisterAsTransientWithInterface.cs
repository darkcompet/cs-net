namespace Tool.Compet.AutoDI;

using Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Register the class as scoped with interface.
/// </summary>
public class RegisterAsTransientWithInterface : AutoDIRegistrationAttribute {
	public static readonly string FullName = typeof(RegisterAsTransientWithInterface).FullName!;

	public RegisterAsTransientWithInterface() {
		this.serviceLifetime = ServiceLifetime.Transient;
	}
}
