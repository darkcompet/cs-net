namespace Tool.Compet.AutoDI;

using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Object used to store the class name, interface name and service
/// lifetime of discovered classes.
/// </summary>
public class ClassesToRegister {
	public Type? ClassName { get; set; }

	public IEnumerable<Type> InterfaceName { get; set; }

	public ServiceLifetime ServiceLifetime { get; set; }

	public bool IgnoreInterface { get; set; }
}
