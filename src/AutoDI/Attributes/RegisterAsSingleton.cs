namespace Tool.Compet.AutoDI;

using Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Register the class as singleton without interface.
/// </summary>
public class RegisterAsSingleton : AutoDIRegistrationAttribute {
	public static readonly string FullName = typeof(RegisterAsSingleton).FullName!;

	public RegisterAsSingleton() {
		this.serviceLifetime = ServiceLifetime.Singleton;
	}
}
