namespace Tool.Compet.AutoDI;

using Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Register the class as scoped without interface.
/// </summary>
public class RegisterAsScoped : AutoDIRegistrationAttribute {
	public static readonly string FullName = typeof(RegisterAsScoped).FullName!;

	public RegisterAsScoped() {
		this.serviceLifetime = ServiceLifetime.Scoped;
	}
}
