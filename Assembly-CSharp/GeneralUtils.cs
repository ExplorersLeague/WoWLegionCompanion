using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;

public static class GeneralUtils
{
	public static void Swap<T>(ref T a, ref T b)
	{
		T t = a;
		a = b;
		b = t;
	}

	public static void ListSwap<T>(IList<T> list, int indexA, int indexB)
	{
		T value = list[indexA];
		list[indexA] = list[indexB];
		list[indexB] = value;
	}

	public static void ListMove<T>(IList<T> list, int srcIndex, int dstIndex)
	{
		if (srcIndex == dstIndex)
		{
			return;
		}
		T item = list[srcIndex];
		list.RemoveAt(srcIndex);
		if (dstIndex > srcIndex)
		{
			dstIndex--;
		}
		list.Insert(dstIndex, item);
	}

	public static T[] Combine<T>(T[] arr1, T[] arr2)
	{
		T[] array = new T[arr1.Length + arr2.Length];
		Array.Copy(arr1, 0, array, 0, arr1.Length);
		Array.Copy(arr1, 0, array, arr1.Length, arr2.Length);
		return array;
	}

	public static bool IsOverriddenMethod(MethodInfo childMethod, MethodInfo ancestorMethod)
	{
		if (childMethod == null)
		{
			return false;
		}
		if (ancestorMethod == null)
		{
			return false;
		}
		if (childMethod.Equals(ancestorMethod))
		{
			return false;
		}
		MethodInfo baseDefinition = childMethod.GetBaseDefinition();
		while (!baseDefinition.Equals(childMethod) && !baseDefinition.Equals(ancestorMethod))
		{
			MethodInfo obj = baseDefinition;
			baseDefinition = baseDefinition.GetBaseDefinition();
			if (baseDefinition.Equals(obj))
			{
				return false;
			}
		}
		return baseDefinition.Equals(ancestorMethod);
	}

	public static bool IsObjectAlive(object obj)
	{
		return obj != null;
	}

	public static bool CallbackIsValid(Delegate callback)
	{
		bool flag = true;
		if (callback == null)
		{
			flag = false;
		}
		else if (!callback.Method.IsStatic)
		{
			object target = callback.Target;
			flag = GeneralUtils.IsObjectAlive(target);
			if (!flag)
			{
				Console.WriteLine(string.Format("Target for callback {0} is null.", callback.Method.Name));
			}
		}
		return flag;
	}

	public static bool IsEditorPlaying()
	{
		return false;
	}

	public static bool TryParseBool(string strVal, out bool boolVal)
	{
		string a = strVal.ToLowerInvariant().Trim();
		if (a == "off" || a == "0" || a == "false")
		{
			boolVal = false;
			return true;
		}
		if (a == "on" || a == "1" || a == "true")
		{
			boolVal = true;
			return true;
		}
		boolVal = false;
		return false;
	}

	public static bool ForceBool(string strVal)
	{
		string a = strVal.ToLowerInvariant().Trim();
		return a == "on" || a == "1" || a == "true";
	}

	public static bool TryParseInt(string str, out int val)
	{
		return int.TryParse(str, NumberStyles.Any, null, out val);
	}

	public static int ForceInt(string str)
	{
		int result = 0;
		GeneralUtils.TryParseInt(str, out result);
		return result;
	}

	public static bool TryParseLong(string str, out long val)
	{
		return long.TryParse(str, NumberStyles.Any, null, out val);
	}

	public static long ForceLong(string str)
	{
		long result = 0L;
		GeneralUtils.TryParseLong(str, out result);
		return result;
	}

	public static bool TryParseFloat(string str, out float val)
	{
		return float.TryParse(str, NumberStyles.Any, null, out val);
	}

	public static float ForceFloat(string str)
	{
		float result = 0f;
		GeneralUtils.TryParseFloat(str, out result);
		return result;
	}

	public static int UnsignedMod(int x, int y)
	{
		int num = x % y;
		if (num < 0)
		{
			num += y;
		}
		return num;
	}

	public static bool AreArraysEqual<T>(T[] arr1, T[] arr2)
	{
		if (arr1 == arr2)
		{
			return true;
		}
		if (arr1 == null)
		{
			return false;
		}
		if (arr2 == null)
		{
			return false;
		}
		if (arr1.Length != arr2.Length)
		{
			return false;
		}
		for (int i = 0; i < arr1.Length; i++)
		{
			if (!arr1[i].Equals(arr2[i]))
			{
				return false;
			}
		}
		return true;
	}

	public static bool AreBytesEqual(byte[] bytes1, byte[] bytes2)
	{
		return GeneralUtils.AreArraysEqual<byte>(bytes1, bytes2);
	}

	public static T DeepClone<T>(T obj)
	{
		return (T)((object)GeneralUtils.CloneValue(obj, obj.GetType()));
	}

	private static object CloneClass(object obj, Type objType)
	{
		object obj2 = GeneralUtils.CreateNewType(objType);
		FieldInfo[] fields = objType.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
		foreach (FieldInfo fieldInfo in fields)
		{
			fieldInfo.SetValue(obj2, GeneralUtils.CloneValue(fieldInfo.GetValue(obj), fieldInfo.FieldType));
		}
		return obj2;
	}

	private static object CloneValue(object src, Type type)
	{
		if (src != null && type != typeof(string) && type.IsClass)
		{
			if (!type.IsGenericType)
			{
				return GeneralUtils.CloneClass(src, type);
			}
			if (src is IDictionary)
			{
				IDictionary dictionary = src as IDictionary;
				IDictionary dictionary2 = GeneralUtils.CreateNewType(type) as IDictionary;
				Type type2 = type.GetGenericArguments()[0];
				Type type3 = type.GetGenericArguments()[1];
				foreach (object obj in dictionary)
				{
					DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
					dictionary2.Add(GeneralUtils.CloneValue(dictionaryEntry.Key, type2), GeneralUtils.CloneValue(dictionaryEntry.Value, type3));
				}
				return dictionary2;
			}
			if (src is IList)
			{
				IList list = src as IList;
				IList list2 = GeneralUtils.CreateNewType(type) as IList;
				Type type4 = type.GetGenericArguments()[0];
				foreach (object src2 in list)
				{
					list2.Add(GeneralUtils.CloneValue(src2, type4));
				}
				return list2;
			}
		}
		return src;
	}

	private static object CreateNewType(Type type)
	{
		object obj = Activator.CreateInstance(type);
		if (obj == null)
		{
			throw new SystemException(string.Format("Unable to instantiate type {0} with default constructor.", type.Name));
		}
		return obj;
	}

	public static void DeepReset<T>(T obj)
	{
		Type typeFromHandle = typeof(T);
		T t = Activator.CreateInstance<T>();
		if (t == null)
		{
			throw new SystemException(string.Format("Unable to instantiate type {0} with default constructor.", typeFromHandle.Name));
		}
		FieldInfo[] fields = typeFromHandle.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static);
		foreach (FieldInfo fieldInfo in fields)
		{
			fieldInfo.SetValue(obj, fieldInfo.GetValue(t));
		}
	}

	public const float DEVELOPMENT_BUILD_TEXT_WIDTH = 115f;
}
