using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Serialization
{
	internal static class CachedAttributeGetter<T> where T : Attribute
	{
		public static T GetAttribute(ICustomAttributeProvider type)
		{
			return CachedAttributeGetter<T>.TypeAttributeCache.Get(type);
		}

		static CachedAttributeGetter()
		{
			// Note: this type is marked as 'beforefieldinit'.
			if (CachedAttributeGetter<T>.<>f__mg$cache0 == null)
			{
				CachedAttributeGetter<T>.<>f__mg$cache0 = new Func<ICustomAttributeProvider, T>(JsonTypeReflector.GetAttribute<T>);
			}
			CachedAttributeGetter<T>.TypeAttributeCache = new ThreadSafeStore<ICustomAttributeProvider, T>(CachedAttributeGetter<T>.<>f__mg$cache0);
		}

		private static readonly ThreadSafeStore<ICustomAttributeProvider, T> TypeAttributeCache;

		[CompilerGenerated]
		private static Func<ICustomAttributeProvider, T> <>f__mg$cache0;
	}
}
