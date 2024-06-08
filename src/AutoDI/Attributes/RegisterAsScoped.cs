namespace Tool.Compet.AutoDI;

using Microsoft.Extensions.DependencyInjection;

/// <summary>
/// This attribute which can be added on top of any class.
/// Sets ServiceLifetime in the base <see cref="AutoDependencyRegistrationAttribute"/> to Scoped.
/// </summary>
public class RegisterAsScoped : AutoDependencyRegistrationAttribute {
	public RegisterAsScoped() {
		this.ServiceLifetime = ServiceLifetime.Scoped;
	}
}
