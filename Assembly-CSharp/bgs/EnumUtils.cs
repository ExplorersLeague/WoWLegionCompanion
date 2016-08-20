using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;

namespace bgs
{
	public class EnumUtils
	{
		public static string GetString<T>(T enumVal)
		{
			string text = enumVal.ToString();
			FieldInfo field = enumVal.GetType().GetField(text);
			DescriptionAttribute[] array = (DescriptionAttribute[])field.GetCustomAttributes(typeof(DescriptionAttribute), false);
			if (array.Length > 0)
			{
				return array[0].Description;
			}
			return text;
		}

		public static bool TryGetEnum<T>(string str, StringComparison comparisonType, out T result)
		{
			Type typeFromHandle = typeof(T);
			Dictionary<string, object> dictionary;
			EnumUtils.s_enumCache.TryGetValue(typeFromHandle, out dictionary);
			object obj;
			if (dictionary != null && dictionary.TryGetValue(str, out obj))
			{
				result = (T)((object)obj);
				return true;
			}
			foreach (object obj2 in Enum.GetValues(typeFromHandle))
			{
				T t = (T)((object)obj2);
				bool flag = false;
				string @string = EnumUtils.GetString<T>(t);
				if (@string.Equals(str, comparisonType))
				{
					flag = true;
					result = t;
				}
				else
				{
					FieldInfo field = t.GetType().GetField(t.ToString());
					DescriptionAttribute[] array = (DescriptionAttribute[])field.GetCustomAttributes(typeof(DescriptionAttribute), false);
					for (int i = 0; i < array.Length; i++)
					{
						if (array[i].Description.Equals(str, comparisonType))
						{
							flag = true;
							break;
						}
					}
				}
				if (flag)
				{
					if (dictionary == null)
					{
						dictionary = new Dictionary<string, object>();
						EnumUtils.s_enumCache.Add(typeFromHandle, dictionary);
					}
					if (!dictionary.ContainsKey(str))
					{
						dictionary.Add(str, t);
					}
					result = t;
					return true;
				}
			}
			result = default(T);
			return false;
		}

		public static T GetEnum<T>(string str)
		{
			return EnumUtils.GetEnum<T>(str, StringComparison.Ordinal);
		}

		public static T GetEnum<T>(string str, StringComparison comparisonType)
		{
			T result;
			if (EnumUtils.TryGetEnum<T>(str, comparisonType, out result))
			{
				return result;
			}
			string message = string.Format("EnumUtils.GetEnum() - \"{0}\" has no matching value in enum {1}", str, typeof(T));
			throw new ArgumentException(message);
		}

		public static bool TryGetEnum<T>(string str, out T outVal)
		{
			return EnumUtils.TryGetEnum<T>(str, StringComparison.Ordinal, out outVal);
		}

		public static T Parse<T>(string str)
		{
			return (T)((object)Enum.Parse(typeof(T), str));
		}

		public static T SafeParse<T>(string str)
		{
			T result;
			try
			{
				result = (T)((object)Enum.Parse(typeof(T), str));
			}
			catch (Exception)
			{
				result = default(T);
			}
			return result;
		}

		public static bool TryCast<T>(object inVal, out T outVal)
		{
			outVal = default(T);
			bool result;
			try
			{
				outVal = (T)((object)inVal);
				result = true;
			}
			catch (Exception)
			{
				result = false;
			}
			return result;
		}

		public static int Length<T>()
		{
			return Enum.GetValues(typeof(T)).Length;
		}

		private static Dictionary<Type, Dictionary<string, object>> s_enumCache = new Dictionary<Type, Dictionary<string, object>>();
	}
}
