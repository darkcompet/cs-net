namespace Tool.Compet.AutoDI;

using Microsoft.Extensions.DependencyInjection;

/// <summary>
/// This attribute which can be added on top of any class.
/// Sets ServiceLifetime in the base <see cref="AutoDIRegistrationAttribute"/> to Transient.
/// </summary>
public class RegisterAsTransient : AutoDIRegistrationAttribute {
	public static readonly string FullName = typeof(RegisterAsTransient).FullName!;

	public RegisterAsTransient() {
		this.serviceLifetime = ServiceLifetime.Transient;
	}
}
