namespace Tool.Compet.AutoDI;

using Microsoft.Extensions.DependencyInjection;

public class RegisterAsScopedIgnoreInterface : AutoDependencyRegistrationAttribute {
	public RegisterAsScopedIgnoreInterface() {
		this.ServiceLifetime = ServiceLifetime.Scoped;
	}
}
