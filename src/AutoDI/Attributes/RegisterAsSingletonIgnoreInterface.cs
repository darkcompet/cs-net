namespace Tool.Compet.AutoDI;

using Microsoft.Extensions.DependencyInjection;

public class RegisterAsSingletonIgnoreInterface : RegisterClass {
	public RegisterAsSingletonIgnoreInterface() {
		this.ServiceLifetime = ServiceLifetime.Singleton;
	}
}
