namespace Tool.Compet.AutoDI;

using Microsoft.Extensions.DependencyInjection;

/// <summary>
/// This attribute which can be added on top of any class.
/// Sets ServiceLifetime in the base <see cref="RegisterClass"/> to Singleton.
/// </summary>
public class RegisterAsSingleton : RegisterClass {
	public RegisterAsSingleton() {
		this.ServiceLifetime = ServiceLifetime.Singleton;
	}
}
