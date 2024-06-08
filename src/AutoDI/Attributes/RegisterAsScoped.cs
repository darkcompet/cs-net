namespace Tool.Compet.AutoDI;

using Microsoft.Extensions.DependencyInjection;

/// <summary>
/// This attribute which can be added on top of any class.
/// Sets ServiceLifetime in the base <see cref="AutoDIRegistrationAttribute"/> to Scoped.
/// </summary>
public class RegisterAsScoped : AutoDIRegistrationAttribute {
	public static readonly string FullName = typeof(RegisterAsScoped).FullName!;

	public RegisterAsScoped() {
		this.serviceLifetime = ServiceLifetime.Scoped;
	}
}
