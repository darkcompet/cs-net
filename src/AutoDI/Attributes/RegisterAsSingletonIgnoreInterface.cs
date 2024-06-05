namespace Tool.Compet.AutoDI;

using Microsoft.Extensions.DependencyInjection;

public class RegisterAsSingletonIgnoreInterface : RegisterClassAttribute {
	public RegisterAsSingletonIgnoreInterface() {
		this.ServiceLifetime = ServiceLifetime.Singleton;
	}
}
