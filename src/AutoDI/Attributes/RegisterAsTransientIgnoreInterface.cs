﻿namespace Tool.Compet.AutoDI;

using Microsoft.Extensions.DependencyInjection;

public class RegisterAsTransientIgnoreInterface : RegisterClass {
	public RegisterAsTransientIgnoreInterface() {
		this.ServiceLifetime = ServiceLifetime.Transient;
	}
}
