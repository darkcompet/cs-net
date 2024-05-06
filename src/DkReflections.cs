namespace Tool.Compet.Core;

using System.Collections;
using System.Reflection;
using System.Text.Json.Serialization;

public class DkReflections {
	/// <summary>
	/// Create new object from given type T, result as `dstObj`.
	/// Then copy all properties which be annotated with `JsonPropertyNameAttribute` from `srcObj` to `dstObj`.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="srcObj"></param>
	/// <returns></returns>
	public static T CloneJsonAnnotatedProperties<T>(object srcObj) where T : class {
		var dstObj = DkObjects.NewInstace<T>();
		CopyJsonAnnotatedProperties(srcObj, dstObj);
		return dstObj;
	}

	/// <summary>
	/// Copy properties which be annotated with `JsonPropertyNameAttribute` from `srcObj` to `dstObj`.
	/// Get properties: https://docs.microsoft.com/en-us/dotnet/api/system.type.getproperties
	/// </summary>
	/// <param name="srcObj"></param>
	/// <param name="dstObj"></param>
	public static void CopyJsonAnnotatedProperties(object srcObj, object dstObj) {
		var name2prop_src = _CollectJsonAnnotatedPropertiesRecursively(srcObj.GetType());
		var name2prop_dst = _CollectJsonAnnotatedPropertiesRecursively(dstObj.GetType());

		foreach (var (propertyName, propertyInfo) in name2prop_dst) {
			// Copy value at the property from srcObj -> dstObj
			if (name2prop_src.TryGetValue(propertyName, out var propertyInfo_src)) {
				propertyInfo.SetValue(dstObj, propertyInfo_src.GetValue(srcObj));
			}
		}
	}

	public static void TrimJsonAnnotatedProperties(object obj) {
		var name2prop = _CollectJsonAnnotatedPropertiesRecursively(obj.GetType());

		foreach (var (_, propertyInfo) in name2prop) {
			var propertyValue = propertyInfo.GetValue(obj);

			if (propertyValue is string str) {
				propertyInfo.SetValue(obj, str.Trim());
			}
			else if (propertyValue is IList<string> arr) {
				for (var i = arr.Count - 1; i >= 0; --i) {
					if (arr[i] != null) {
						arr[i] = arr[i].Trim();
					}
				}
			}
			else if (propertyValue is IList<object> list) {
				for (var i = list.Count - 1; i >= 0; --i) {
					if (list[i] != null) {
						TrimJsonAnnotatedProperties(list[i]);
					}
				}
			}
			else if (propertyValue is object nextObj) {
				TrimJsonAnnotatedProperties(nextObj);
			}
		}
	}

	private static Dictionary<string, PropertyInfo> _CollectJsonAnnotatedPropertiesRecursively(Type type) {
		var result_name2prop = new Dictionary<string, PropertyInfo>();

		var props = type.GetProperties();
		for (var index = props.Length - 1; index >= 0; --index) {
			var prop = props[index];
			var attribute = prop.GetCustomAttribute<JsonPropertyNameAttribute>();
			if (attribute != null) {
				// Use Set (don't use Add to avoid exception when duplicate key)
				result_name2prop[attribute.Name] = prop;
			}
		}

		var baseType = type.BaseType;
		if (baseType != null) {
			// Use Set (do not use Add to avoid exception when duplicated key)
			foreach (var name2prop in _CollectJsonAnnotatedPropertiesRecursively(baseType)) {
				result_name2prop[name2prop.Key] = name2prop.Value;
			}
		}

		return result_name2prop;
	}
}
