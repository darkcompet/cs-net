﻿namespace Tool.Compet.AutoDI;

using System;
using Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Base attribute, used by <see cref="RegisterAsScoped"/>, <see cref="RegisterAsSingleton"/> and
/// <see cref="RegisterAsTransient"/>. If applied to any class, the service lifetime will be set to transient by
/// default. The same as using  <see cref="RegisterAsTransient"/>.
/// </summary>
[AttributeUsage(AttributeTargets.Class)]
public class RegisterClassAttribute : Attribute {
	protected ServiceLifetime ServiceLifetime { get; set; } = ServiceLifetime.Transient;
}
