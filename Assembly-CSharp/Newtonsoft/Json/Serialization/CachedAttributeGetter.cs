using System;
using System.Reflection;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Serialization
{
	internal static class CachedAttributeGetter<T> where T : Attribute
	{
		public static T GetAttribute(ICustomAttributeProvider type)
		{
			return CachedAttributeGetter<T>.TypeAttributeCache.Get(type);
		}

		private static readonly ThreadSafeStore<ICustomAttributeProvider, T> TypeAttributeCache = new ThreadSafeStore<ICustomAttributeProvider, T>(new Func<ICustomAttributeProvider, T>(JsonTypeReflector.GetAttribute<T>));
	}
}
