using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Object = UnityEngine.Object;

namespace verell.Architecture
{
	public static class ReflectionUtils
	{
		public static List<Type> GetAllSubtypes<T>(bool includeBaseType = true)
		{
			var targetType = typeof(T);
			var types = new List<Type>();

			foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
			{
				var assemblyTypes = assembly
						.GetTypes()
						.Where(type => !type.IsAbstract && type.IsSubclassOf(targetType) || targetType.IsAssignableFrom(type));
				types.AddRange(assemblyTypes);
			}

			if (!includeBaseType)
			{
				types.Remove(targetType);
			}

			return types;
		}

		public static List<FieldInfo> GetAllFields(this Type type, BindingFlags flags)
		{
			if (type == null ||
			    type == typeof(Object) ||
			    type == typeof(object))
				return new List<FieldInfo>();

			var list = type.BaseType.GetAllFields(flags);
			
			list.AddRange(type.GetFields(flags | BindingFlags.DeclaredOnly));
			
			return list;
		}
	}
}
