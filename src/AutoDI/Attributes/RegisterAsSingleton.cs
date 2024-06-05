namespace Tool.Compet.AutoDI;

using Microsoft.Extensions.DependencyInjection;

/// <summary>
/// This attribute which can be added on top of any class.
/// Sets ServiceLifetime in the base <see cref="RegisterClassAttribute"/> to Singleton.
/// </summary>
public class RegisterAsSingleton : RegisterClassAttribute {
	public RegisterAsSingleton() {
		this.ServiceLifetime = ServiceLifetime.Singleton;
	}
}
