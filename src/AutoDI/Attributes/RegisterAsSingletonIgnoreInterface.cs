namespace Tool.Compet.AutoDI;

using Microsoft.Extensions.DependencyInjection;

public class RegisterAsSingletonIgnoreInterface : AutoDependencyRegistrationAttribute {
	public RegisterAsSingletonIgnoreInterface() {
		this.ServiceLifetime = ServiceLifetime.Singleton;
	}
}
