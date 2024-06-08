namespace Tool.Compet.AutoDI;

using Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Register the class as transient without interface.
/// </summary>
public class RegisterAsTransient : AutoDIRegistrationAttribute {
	public static readonly string FullName = typeof(RegisterAsTransient).FullName!;

	public RegisterAsTransient() {
		this.serviceLifetime = ServiceLifetime.Transient;
	}
}
