using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace Newtonsoft.Json.Utilities
{
	internal static class CollectionUtils
	{
		public static IEnumerable<T> CastValid<T>(this IEnumerable enumerable)
		{
			ValidationUtils.ArgumentNotNull(enumerable, "enumerable");
			return (from object o in enumerable
			where o is T
			select o).Cast<T>();
		}

		public static List<T> CreateList<T>(params T[] values)
		{
			return new List<T>(values);
		}

		public static bool IsNullOrEmpty(ICollection collection)
		{
			return collection == null || collection.Count == 0;
		}

		public static bool IsNullOrEmpty<T>(ICollection<T> collection)
		{
			return collection == null || collection.Count == 0;
		}

		public static bool IsNullOrEmptyOrDefault<T>(IList<T> list)
		{
			return CollectionUtils.IsNullOrEmpty<T>(list) || ReflectionUtils.ItemsUnitializedValue<T>(list);
		}

		public static IList<T> Slice<T>(IList<T> list, int? start, int? end)
		{
			return CollectionUtils.Slice<T>(list, start, end, null);
		}

		public static IList<T> Slice<T>(IList<T> list, int? start, int? end, int? step)
		{
			if (list == null)
			{
				throw new ArgumentNullException("list");
			}
			if (step == 0)
			{
				throw new ArgumentException("Step cannot be zero.", "step");
			}
			List<T> list2 = new List<T>();
			if (list.Count == 0)
			{
				return list2;
			}
			int num = (step == null) ? 1 : step.Value;
			int num2 = (start == null) ? 0 : start.Value;
			int num3 = (end == null) ? list.Count : end.Value;
			num2 = ((num2 >= 0) ? num2 : (list.Count + num2));
			num3 = ((num3 >= 0) ? num3 : (list.Count + num3));
			num2 = Math.Max(num2, 0);
			num3 = Math.Min(num3, list.Count - 1);
			for (int i = num2; i < num3; i += num)
			{
				list2.Add(list[i]);
			}
			return list2;
		}

		public static Dictionary<K, List<V>> GroupBy<K, V>(ICollection<V> source, Func<V, K> keySelector)
		{
			if (keySelector == null)
			{
				throw new ArgumentNullException("keySelector");
			}
			Dictionary<K, List<V>> dictionary = new Dictionary<K, List<V>>();
			foreach (V v in source)
			{
				K key = keySelector(v);
				List<V> list;
				if (!dictionary.TryGetValue(key, out list))
				{
					list = new List<V>();
					dictionary.Add(key, list);
				}
				list.Add(v);
			}
			return dictionary;
		}

		public static void AddRange<T>(this IList<T> initial, IEnumerable<T> collection)
		{
			if (initial == null)
			{
				throw new ArgumentNullException("initial");
			}
			if (collection == null)
			{
				return;
			}
			foreach (T item in collection)
			{
				initial.Add(item);
			}
		}

		public static void AddRange(this IList initial, IEnumerable collection)
		{
			ValidationUtils.ArgumentNotNull(initial, "initial");
			ListWrapper<object> initial2 = new ListWrapper<object>(initial);
			initial2.AddRange(collection.Cast<object>());
		}

		public static List<T> Distinct<T>(List<T> collection)
		{
			List<T> list = new List<T>();
			foreach (T item in collection)
			{
				if (!list.Contains(item))
				{
					list.Add(item);
				}
			}
			return list;
		}

		public static List<List<T>> Flatten<T>(params IList<T>[] lists)
		{
			List<List<T>> list = new List<List<T>>();
			Dictionary<int, T> currentSet = new Dictionary<int, T>();
			CollectionUtils.Recurse<T>(new List<IList<T>>(lists), 0, currentSet, list);
			return list;
		}

		private static void Recurse<T>(IList<IList<T>> global, int current, Dictionary<int, T> currentSet, List<List<T>> flattenedResult)
		{
			IList<T> list = global[current];
			for (int i = 0; i < list.Count; i++)
			{
				currentSet[current] = list[i];
				if (current == global.Count - 1)
				{
					List<T> list2 = new List<T>();
					for (int j = 0; j < currentSet.Count; j++)
					{
						list2.Add(currentSet[j]);
					}
					flattenedResult.Add(list2);
				}
				else
				{
					CollectionUtils.Recurse<T>(global, current + 1, currentSet, flattenedResult);
				}
			}
		}

		public static List<T> CreateList<T>(ICollection collection)
		{
			if (collection == null)
			{
				throw new ArgumentNullException("collection");
			}
			T[] array = new T[collection.Count];
			collection.CopyTo(array, 0);
			return new List<T>(array);
		}

		public static bool ListEquals<T>(IList<T> a, IList<T> b)
		{
			if (a == null || b == null)
			{
				return a == null && b == null;
			}
			if (a.Count != b.Count)
			{
				return false;
			}
			EqualityComparer<T> @default = EqualityComparer<T>.Default;
			for (int i = 0; i < a.Count; i++)
			{
				if (!@default.Equals(a[i], b[i]))
				{
					return false;
				}
			}
			return true;
		}

		public static bool TryGetSingleItem<T>(IList<T> list, out T value)
		{
			return CollectionUtils.TryGetSingleItem<T>(list, false, out value);
		}

		public static bool TryGetSingleItem<T>(IList<T> list, bool returnDefaultIfEmpty, out T value)
		{
			return MiscellaneousUtils.TryAction<T>(() => CollectionUtils.GetSingleItem<T>(list, returnDefaultIfEmpty), out value);
		}

		public static T GetSingleItem<T>(IList<T> list)
		{
			return CollectionUtils.GetSingleItem<T>(list, false);
		}

		public static T GetSingleItem<T>(IList<T> list, bool returnDefaultIfEmpty)
		{
			if (list.Count == 1)
			{
				return list[0];
			}
			if (returnDefaultIfEmpty && list.Count == 0)
			{
				return default(T);
			}
			throw new Exception("Expected single {0} in list but got {1}.".FormatWith(CultureInfo.InvariantCulture, new object[]
			{
				typeof(T),
				list.Count
			}));
		}

		public static IList<T> Minus<T>(IList<T> list, IList<T> minus)
		{
			ValidationUtils.ArgumentNotNull(list, "list");
			List<T> list2 = new List<T>(list.Count);
			foreach (T item in list)
			{
				if (minus == null || !minus.Contains(item))
				{
					list2.Add(item);
				}
			}
			return list2;
		}

		public static IList CreateGenericList(Type listType)
		{
			ValidationUtils.ArgumentNotNull(listType, "listType");
			return (IList)ReflectionUtils.CreateGeneric(typeof(List<>), listType, new object[0]);
		}

		public static IDictionary CreateGenericDictionary(Type keyType, Type valueType)
		{
			ValidationUtils.ArgumentNotNull(keyType, "keyType");
			ValidationUtils.ArgumentNotNull(valueType, "valueType");
			return (IDictionary)ReflectionUtils.CreateGeneric(typeof(Dictionary<, >), keyType, new object[]
			{
				valueType
			});
		}

		public static bool IsListType(Type type)
		{
			ValidationUtils.ArgumentNotNull(type, "type");
			return type.IsArray || typeof(IList).IsAssignableFrom(type) || ReflectionUtils.ImplementsGenericDefinition(type, typeof(IList<>));
		}

		public static bool IsCollectionType(Type type)
		{
			ValidationUtils.ArgumentNotNull(type, "type");
			return type.IsArray || typeof(ICollection).IsAssignableFrom(type) || ReflectionUtils.ImplementsGenericDefinition(type, typeof(ICollection<>));
		}

		public static bool IsDictionaryType(Type type)
		{
			ValidationUtils.ArgumentNotNull(type, "type");
			return typeof(IDictionary).IsAssignableFrom(type) || ReflectionUtils.ImplementsGenericDefinition(type, typeof(IDictionary<, >));
		}

		public static IWrappedCollection CreateCollectionWrapper(object list)
		{
			ValidationUtils.ArgumentNotNull(list, "list");
			Type collectionDefinition;
			if (ReflectionUtils.ImplementsGenericDefinition(list.GetType(), typeof(ICollection<>), out collectionDefinition))
			{
				Type collectionItemType = ReflectionUtils.GetCollectionItemType(collectionDefinition);
				Func<Type, IList<object>, object> instanceCreator = delegate(Type t, IList<object> a)
				{
					ConstructorInfo constructor = t.GetConstructor(new Type[]
					{
						collectionDefinition
					});
					return constructor.Invoke(new object[]
					{
						list
					});
				};
				return (IWrappedCollection)ReflectionUtils.CreateGeneric(typeof(CollectionWrapper<>), new Type[]
				{
					collectionItemType
				}, instanceCreator, new object[]
				{
					list
				});
			}
			if (list is IList)
			{
				return new CollectionWrapper<object>((IList)list);
			}
			throw new Exception("Can not create ListWrapper for type {0}.".FormatWith(CultureInfo.InvariantCulture, new object[]
			{
				list.GetType()
			}));
		}

		public static IWrappedList CreateListWrapper(object list)
		{
			ValidationUtils.ArgumentNotNull(list, "list");
			Type listDefinition;
			if (ReflectionUtils.ImplementsGenericDefinition(list.GetType(), typeof(IList<>), out listDefinition))
			{
				Type collectionItemType = ReflectionUtils.GetCollectionItemType(listDefinition);
				Func<Type, IList<object>, object> instanceCreator = delegate(Type t, IList<object> a)
				{
					ConstructorInfo constructor = t.GetConstructor(new Type[]
					{
						listDefinition
					});
					return constructor.Invoke(new object[]
					{
						list
					});
				};
				return (IWrappedList)ReflectionUtils.CreateGeneric(typeof(ListWrapper<>), new Type[]
				{
					collectionItemType
				}, instanceCreator, new object[]
				{
					list
				});
			}
			if (list is IList)
			{
				return new ListWrapper<object>((IList)list);
			}
			throw new Exception("Can not create ListWrapper for type {0}.".FormatWith(CultureInfo.InvariantCulture, new object[]
			{
				list.GetType()
			}));
		}

		public static IWrappedDictionary CreateDictionaryWrapper(object dictionary)
		{
			ValidationUtils.ArgumentNotNull(dictionary, "dictionary");
			Type dictionaryDefinition;
			if (ReflectionUtils.ImplementsGenericDefinition(dictionary.GetType(), typeof(IDictionary<, >), out dictionaryDefinition))
			{
				Type dictionaryKeyType = ReflectionUtils.GetDictionaryKeyType(dictionaryDefinition);
				Type dictionaryValueType = ReflectionUtils.GetDictionaryValueType(dictionaryDefinition);
				Func<Type, IList<object>, object> instanceCreator = delegate(Type t, IList<object> a)
				{
					ConstructorInfo constructor = t.GetConstructor(new Type[]
					{
						dictionaryDefinition
					});
					return constructor.Invoke(new object[]
					{
						dictionary
					});
				};
				return (IWrappedDictionary)ReflectionUtils.CreateGeneric(typeof(DictionaryWrapper<, >), new Type[]
				{
					dictionaryKeyType,
					dictionaryValueType
				}, instanceCreator, new object[]
				{
					dictionary
				});
			}
			if (dictionary is IDictionary)
			{
				return new DictionaryWrapper<object, object>((IDictionary)dictionary);
			}
			throw new Exception("Can not create DictionaryWrapper for type {0}.".FormatWith(CultureInfo.InvariantCulture, new object[]
			{
				dictionary.GetType()
			}));
		}

		public static object CreateAndPopulateList(Type listType, Action<IList, bool> populateList)
		{
			ValidationUtils.ArgumentNotNull(listType, "listType");
			ValidationUtils.ArgumentNotNull(populateList, "populateList");
			bool flag = false;
			IList list;
			Type type;
			if (listType.IsArray)
			{
				list = new List<object>();
				flag = true;
			}
			else if (ReflectionUtils.InheritsGenericDefinition(listType, typeof(ReadOnlyCollection<>), out type))
			{
				Type type2 = type.GetGenericArguments()[0];
				Type type3 = ReflectionUtils.MakeGenericType(typeof(IEnumerable<>), new Type[]
				{
					type2
				});
				bool flag2 = false;
				foreach (ConstructorInfo constructorInfo in listType.GetConstructors())
				{
					IList<ParameterInfo> parameters = constructorInfo.GetParameters();
					if (parameters.Count == 1 && type3.IsAssignableFrom(parameters[0].ParameterType))
					{
						flag2 = true;
						break;
					}
				}
				if (!flag2)
				{
					throw new Exception("Read-only type {0} does not have a public constructor that takes a type that implements {1}.".FormatWith(CultureInfo.InvariantCulture, new object[]
					{
						listType,
						type3
					}));
				}
				list = CollectionUtils.CreateGenericList(type2);
				flag = true;
			}
			else if (typeof(IList).IsAssignableFrom(listType))
			{
				if (ReflectionUtils.IsInstantiatableType(listType))
				{
					list = (IList)Activator.CreateInstance(listType);
				}
				else if (listType == typeof(IList))
				{
					list = new List<object>();
				}
				else
				{
					list = null;
				}
			}
			else if (ReflectionUtils.ImplementsGenericDefinition(listType, typeof(ICollection<>)))
			{
				if (ReflectionUtils.IsInstantiatableType(listType))
				{
					list = CollectionUtils.CreateCollectionWrapper(Activator.CreateInstance(listType));
				}
				else
				{
					list = null;
				}
			}
			else if (listType == typeof(BitArray))
			{
				list = new List<object>();
				flag = true;
			}
			else
			{
				list = null;
			}
			if (list == null)
			{
				throw new Exception("Cannot create and populate list type {0}.".FormatWith(CultureInfo.InvariantCulture, new object[]
				{
					listType
				}));
			}
			populateList(list, flag);
			if (flag)
			{
				if (listType.IsArray)
				{
					if (listType.GetArrayRank() > 1)
					{
						list = CollectionUtils.ToMultidimensionalArray(list, ReflectionUtils.GetCollectionItemType(listType), listType.GetArrayRank());
					}
					else
					{
						list = CollectionUtils.ToArray(((List<object>)list).ToArray(), ReflectionUtils.GetCollectionItemType(listType));
					}
				}
				else if (ReflectionUtils.InheritsGenericDefinition(listType, typeof(ReadOnlyCollection<>)))
				{
					list = (IList)ReflectionUtils.CreateInstance(listType, new object[]
					{
						list
					});
				}
				else if (listType == typeof(BitArray))
				{
					BitArray bitArray = new BitArray(list.Count);
					for (int j = 0; j < list.Count; j++)
					{
						bitArray[j] = (bool)list[j];
					}
					return bitArray;
				}
			}
			else if (list is IWrappedCollection)
			{
				return ((IWrappedCollection)list).UnderlyingCollection;
			}
			return list;
		}

		public static Array ToArray(Array initial, Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			Array array = Array.CreateInstance(type, initial.Length);
			Array.Copy(initial, 0, array, 0, initial.Length);
			return array;
		}

		private static IList<int> GetDimensions(IList values)
		{
			IList<int> list = new List<int>();
			IList list2 = values;
			for (;;)
			{
				list.Add(list2.Count);
				if (list2.Count == 0)
				{
					break;
				}
				object obj = list2[0];
				if (!(obj is IList))
				{
					break;
				}
				list2 = (IList)obj;
			}
			return list;
		}

		public static Array ToMultidimensionalArray(IList values, Type type, int rank)
		{
			IList<int> dimensions = CollectionUtils.GetDimensions(values);
			while (dimensions.Count < rank)
			{
				dimensions.Add(0);
			}
			Array array = Array.CreateInstance(type, dimensions.ToArray<int>());
			CollectionUtils.CopyFromJaggedToMultidimensionalArray(values, array, new int[0]);
			return array;
		}

		private static object JaggedArrayGetValue(IList values, int[] indices)
		{
			IList list = values;
			for (int i = 0; i < indices.Length; i++)
			{
				int index = indices[i];
				if (i == indices.Length - 1)
				{
					return list[index];
				}
				list = (IList)list[index];
			}
			return list;
		}

		private static void CopyFromJaggedToMultidimensionalArray(IList values, Array multidimensionalArray, int[] indices)
		{
			int num = indices.Length;
			if (num == multidimensionalArray.Rank)
			{
				multidimensionalArray.SetValue(CollectionUtils.JaggedArrayGetValue(values, indices), indices);
				return;
			}
			int length = multidimensionalArray.GetLength(num);
			IList list = (IList)CollectionUtils.JaggedArrayGetValue(values, indices);
			int count = list.Count;
			if (count != length)
			{
				throw new Exception("Cannot deserialize non-cubical array as multidimensional array.");
			}
			int[] array = new int[num + 1];
			for (int i = 0; i < num; i++)
			{
				array[i] = indices[i];
			}
			for (int j = 0; j < multidimensionalArray.GetLength(num); j++)
			{
				array[num] = j;
				CollectionUtils.CopyFromJaggedToMultidimensionalArray(values, multidimensionalArray, array);
			}
		}

		public static bool AddDistinct<T>(this IList<T> list, T value)
		{
			return list.AddDistinct(value, EqualityComparer<T>.Default);
		}

		public static bool AddDistinct<T>(this IList<T> list, T value, IEqualityComparer<T> comparer)
		{
			if (list.ContainsValue(value, comparer))
			{
				return false;
			}
			list.Add(value);
			return true;
		}

		public static bool ContainsValue<TSource>(this IEnumerable<TSource> source, TSource value, IEqualityComparer<TSource> comparer)
		{
			if (comparer == null)
			{
				comparer = EqualityComparer<TSource>.Default;
			}
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			foreach (TSource x in source)
			{
				if (comparer.Equals(x, value))
				{
					return true;
				}
			}
			return false;
		}

		public static bool AddRangeDistinct<T>(this IList<T> list, IEnumerable<T> values)
		{
			return list.AddRangeDistinct(values, EqualityComparer<T>.Default);
		}

		public static bool AddRangeDistinct<T>(this IList<T> list, IEnumerable<T> values, IEqualityComparer<T> comparer)
		{
			bool result = true;
			foreach (T value in values)
			{
				if (!list.AddDistinct(value, comparer))
				{
					result = false;
				}
			}
			return result;
		}

		public static int IndexOf<T>(this IEnumerable<T> collection, Func<T, bool> predicate)
		{
			int num = 0;
			foreach (T arg in collection)
			{
				if (predicate(arg))
				{
					return num;
				}
				num++;
			}
			return -1;
		}

		public static int IndexOf<TSource>(this IEnumerable<TSource> list, TSource value) where TSource : IEquatable<TSource>
		{
			return list.IndexOf(value, EqualityComparer<TSource>.Default);
		}

		public static int IndexOf<TSource>(this IEnumerable<TSource> list, TSource value, IEqualityComparer<TSource> comparer)
		{
			int num = 0;
			foreach (TSource x in list)
			{
				if (comparer.Equals(x, value))
				{
					return num;
				}
				num++;
			}
			return -1;
		}
	}
}
