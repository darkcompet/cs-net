namespace Tool.Compet.AutoDI;

using Microsoft.Extensions.DependencyInjection;

/// <summary>
/// This attribute which can be added on top of any class.
/// Sets ServiceLifetime in the base <see cref="RegisterClassAttribute"/> to Transient.
/// </summary>
public class RegisterAsTransient : RegisterClassAttribute {
	public RegisterAsTransient() {
		this.ServiceLifetime = ServiceLifetime.Transient;
	}
}
