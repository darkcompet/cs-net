namespace Tool.Compet.AutoDI;

using Microsoft.Extensions.DependencyInjection;

public class RegisterAsScopedIgnoreInterface : RegisterClass {
	public RegisterAsScopedIgnoreInterface() {
		this.ServiceLifetime = ServiceLifetime.Scoped;
	}
}
