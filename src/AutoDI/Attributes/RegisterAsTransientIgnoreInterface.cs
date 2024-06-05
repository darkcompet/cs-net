namespace Tool.Compet.AutoDI;

using Microsoft.Extensions.DependencyInjection;

public class RegisterAsTransientIgnoreInterface : RegisterClassAttribute {
	public RegisterAsTransientIgnoreInterface() {
		this.ServiceLifetime = ServiceLifetime.Transient;
	}
}
