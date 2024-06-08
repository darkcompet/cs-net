namespace Tool.Compet.AutoDI;

using Microsoft.Extensions.DependencyInjection;

public class RegisterAsTransientIgnoreInterface : AutoDependencyRegistrationAttribute {
	public RegisterAsTransientIgnoreInterface() {
		this.ServiceLifetime = ServiceLifetime.Transient;
	}
}
