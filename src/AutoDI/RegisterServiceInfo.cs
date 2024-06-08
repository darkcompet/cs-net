namespace Tool.Compet.AutoDI;

using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Object used to store the class name, interface name and service
/// lifetime of discovered classes.
/// </summary>
public class RegisterServiceInfo {
	/// Service type.
	public Type serviceType;

	/// Type of interfaces that the class implements.
	public IEnumerable<Type> interfaceTypes;

	/// One of: scoped, singletone, trasient.
	public ServiceLifetime serviceLifetime;

	/// Ignore add pair of [interface, implement].
	public bool ignoreInterface;
}
