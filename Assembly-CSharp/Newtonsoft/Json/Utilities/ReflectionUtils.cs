using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters;
using System.Text;

namespace Newtonsoft.Json.Utilities
{
	internal static class ReflectionUtils
	{
		public static bool IsVirtual(this PropertyInfo propertyInfo)
		{
			ValidationUtils.ArgumentNotNull(propertyInfo, "propertyInfo");
			MethodInfo methodInfo = propertyInfo.GetGetMethod();
			if (methodInfo != null && methodInfo.IsVirtual)
			{
				return true;
			}
			methodInfo = propertyInfo.GetSetMethod();
			return methodInfo != null && methodInfo.IsVirtual;
		}

		public static Type GetObjectType(object v)
		{
			return (v == null) ? null : v.GetType();
		}

		public static string GetTypeName(Type t, FormatterAssemblyStyle assemblyFormat)
		{
			return ReflectionUtils.GetTypeName(t, assemblyFormat, null);
		}

		public static string GetTypeName(Type t, FormatterAssemblyStyle assemblyFormat, SerializationBinder binder)
		{
			string assemblyQualifiedName = t.AssemblyQualifiedName;
			if (assemblyFormat == FormatterAssemblyStyle.Simple)
			{
				return ReflectionUtils.RemoveAssemblyDetails(assemblyQualifiedName);
			}
			if (assemblyFormat != FormatterAssemblyStyle.Full)
			{
				throw new ArgumentOutOfRangeException();
			}
			return t.AssemblyQualifiedName;
		}

		private static string RemoveAssemblyDetails(string fullyQualifiedTypeName)
		{
			StringBuilder stringBuilder = new StringBuilder();
			bool flag = false;
			bool flag2 = false;
			foreach (char c in fullyQualifiedTypeName)
			{
				switch (c)
				{
				case '[':
					flag = false;
					flag2 = false;
					stringBuilder.Append(c);
					break;
				default:
					if (c != ',')
					{
						if (!flag2)
						{
							stringBuilder.Append(c);
						}
					}
					else if (!flag)
					{
						flag = true;
						stringBuilder.Append(c);
					}
					else
					{
						flag2 = true;
					}
					break;
				case ']':
					flag = false;
					flag2 = false;
					stringBuilder.Append(c);
					break;
				}
			}
			return stringBuilder.ToString();
		}

		public static bool IsInstantiatableType(Type t)
		{
			ValidationUtils.ArgumentNotNull(t, "t");
			return !t.IsAbstract && !t.IsInterface && !t.IsArray && !t.IsGenericTypeDefinition && t != typeof(void) && ReflectionUtils.HasDefaultConstructor(t);
		}

		public static bool HasDefaultConstructor(Type t)
		{
			return ReflectionUtils.HasDefaultConstructor(t, false);
		}

		public static bool HasDefaultConstructor(Type t, bool nonPublic)
		{
			ValidationUtils.ArgumentNotNull(t, "t");
			return t.IsValueType || ReflectionUtils.GetDefaultConstructor(t, nonPublic) != null;
		}

		public static ConstructorInfo GetDefaultConstructor(Type t)
		{
			return ReflectionUtils.GetDefaultConstructor(t, false);
		}

		public static ConstructorInfo GetDefaultConstructor(Type t, bool nonPublic)
		{
			BindingFlags bindingFlags = BindingFlags.Public;
			if (nonPublic)
			{
				bindingFlags |= BindingFlags.NonPublic;
			}
			return t.GetConstructor(bindingFlags | BindingFlags.Instance, null, new Type[0], null);
		}

		public static bool IsNullable(Type t)
		{
			ValidationUtils.ArgumentNotNull(t, "t");
			return !t.IsValueType || ReflectionUtils.IsNullableType(t);
		}

		public static bool IsNullableType(Type t)
		{
			ValidationUtils.ArgumentNotNull(t, "t");
			return t.IsGenericType && t.GetGenericTypeDefinition() == typeof(Nullable<>);
		}

		public static Type EnsureNotNullableType(Type t)
		{
			return (!ReflectionUtils.IsNullableType(t)) ? t : Nullable.GetUnderlyingType(t);
		}

		public static bool IsUnitializedValue(object value)
		{
			if (value == null)
			{
				return true;
			}
			object obj = ReflectionUtils.CreateUnitializedValue(value.GetType());
			return value.Equals(obj);
		}

		public static object CreateUnitializedValue(Type type)
		{
			ValidationUtils.ArgumentNotNull(type, "type");
			if (type.IsGenericTypeDefinition)
			{
				throw new ArgumentException("Type {0} is a generic type definition and cannot be instantiated.".FormatWith(CultureInfo.InvariantCulture, new object[]
				{
					type
				}), "type");
			}
			if (type.IsClass || type.IsInterface || type == typeof(void))
			{
				return null;
			}
			if (type.IsValueType)
			{
				return Activator.CreateInstance(type);
			}
			throw new ArgumentException("Type {0} cannot be instantiated.".FormatWith(CultureInfo.InvariantCulture, new object[]
			{
				type
			}), "type");
		}

		public static bool IsPropertyIndexed(PropertyInfo property)
		{
			ValidationUtils.ArgumentNotNull(property, "property");
			return !CollectionUtils.IsNullOrEmpty<ParameterInfo>(property.GetIndexParameters());
		}

		public static bool ImplementsGenericDefinition(Type type, Type genericInterfaceDefinition)
		{
			Type type2;
			return ReflectionUtils.ImplementsGenericDefinition(type, genericInterfaceDefinition, out type2);
		}

		public static bool ImplementsGenericDefinition(Type type, Type genericInterfaceDefinition, out Type implementingType)
		{
			ValidationUtils.ArgumentNotNull(type, "type");
			ValidationUtils.ArgumentNotNull(genericInterfaceDefinition, "genericInterfaceDefinition");
			if (!genericInterfaceDefinition.IsInterface || !genericInterfaceDefinition.IsGenericTypeDefinition)
			{
				throw new ArgumentNullException("'{0}' is not a generic interface definition.".FormatWith(CultureInfo.InvariantCulture, new object[]
				{
					genericInterfaceDefinition
				}));
			}
			if (type.IsInterface && type.IsGenericType)
			{
				Type genericTypeDefinition = type.GetGenericTypeDefinition();
				if (genericInterfaceDefinition == genericTypeDefinition)
				{
					implementingType = type;
					return true;
				}
			}
			foreach (Type type2 in type.GetInterfaces())
			{
				if (type2.IsGenericType)
				{
					Type genericTypeDefinition2 = type2.GetGenericTypeDefinition();
					if (genericInterfaceDefinition == genericTypeDefinition2)
					{
						implementingType = type2;
						return true;
					}
				}
			}
			implementingType = null;
			return false;
		}

		public static bool AssignableToTypeName(this Type type, string fullTypeName, out Type match)
		{
			for (Type type2 = type; type2 != null; type2 = type2.BaseType)
			{
				if (string.Equals(type2.FullName, fullTypeName, StringComparison.Ordinal))
				{
					match = type2;
					return true;
				}
			}
			foreach (Type type3 in type.GetInterfaces())
			{
				if (string.Equals(type3.Name, fullTypeName, StringComparison.Ordinal))
				{
					match = type;
					return true;
				}
			}
			match = null;
			return false;
		}

		public static bool AssignableToTypeName(this Type type, string fullTypeName)
		{
			Type type2;
			return type.AssignableToTypeName(fullTypeName, out type2);
		}

		public static bool InheritsGenericDefinition(Type type, Type genericClassDefinition)
		{
			Type type2;
			return ReflectionUtils.InheritsGenericDefinition(type, genericClassDefinition, out type2);
		}

		public static bool InheritsGenericDefinition(Type type, Type genericClassDefinition, out Type implementingType)
		{
			ValidationUtils.ArgumentNotNull(type, "type");
			ValidationUtils.ArgumentNotNull(genericClassDefinition, "genericClassDefinition");
			if (!genericClassDefinition.IsClass || !genericClassDefinition.IsGenericTypeDefinition)
			{
				throw new ArgumentNullException("'{0}' is not a generic class definition.".FormatWith(CultureInfo.InvariantCulture, new object[]
				{
					genericClassDefinition
				}));
			}
			return ReflectionUtils.InheritsGenericDefinitionInternal(type, genericClassDefinition, out implementingType);
		}

		private static bool InheritsGenericDefinitionInternal(Type currentType, Type genericClassDefinition, out Type implementingType)
		{
			if (currentType.IsGenericType)
			{
				Type genericTypeDefinition = currentType.GetGenericTypeDefinition();
				if (genericClassDefinition == genericTypeDefinition)
				{
					implementingType = currentType;
					return true;
				}
			}
			if (currentType.BaseType == null)
			{
				implementingType = null;
				return false;
			}
			return ReflectionUtils.InheritsGenericDefinitionInternal(currentType.BaseType, genericClassDefinition, out implementingType);
		}

		public static Type GetCollectionItemType(Type type)
		{
			ValidationUtils.ArgumentNotNull(type, "type");
			if (type.IsArray)
			{
				return type.GetElementType();
			}
			Type type2;
			if (ReflectionUtils.ImplementsGenericDefinition(type, typeof(IEnumerable<>), out type2))
			{
				if (type2.IsGenericTypeDefinition)
				{
					throw new Exception("Type {0} is not a collection.".FormatWith(CultureInfo.InvariantCulture, new object[]
					{
						type
					}));
				}
				return type2.GetGenericArguments()[0];
			}
			else
			{
				if (typeof(IEnumerable).IsAssignableFrom(type))
				{
					return null;
				}
				throw new Exception("Type {0} is not a collection.".FormatWith(CultureInfo.InvariantCulture, new object[]
				{
					type
				}));
			}
		}

		public static void GetDictionaryKeyValueTypes(Type dictionaryType, out Type keyType, out Type valueType)
		{
			ValidationUtils.ArgumentNotNull(dictionaryType, "type");
			Type type;
			if (ReflectionUtils.ImplementsGenericDefinition(dictionaryType, typeof(IDictionary<, >), out type))
			{
				if (type.IsGenericTypeDefinition)
				{
					throw new Exception("Type {0} is not a dictionary.".FormatWith(CultureInfo.InvariantCulture, new object[]
					{
						dictionaryType
					}));
				}
				Type[] genericArguments = type.GetGenericArguments();
				keyType = genericArguments[0];
				valueType = genericArguments[1];
				return;
			}
			else
			{
				if (typeof(IDictionary).IsAssignableFrom(dictionaryType))
				{
					keyType = null;
					valueType = null;
					return;
				}
				throw new Exception("Type {0} is not a dictionary.".FormatWith(CultureInfo.InvariantCulture, new object[]
				{
					dictionaryType
				}));
			}
		}

		public static Type GetDictionaryValueType(Type dictionaryType)
		{
			Type type;
			Type result;
			ReflectionUtils.GetDictionaryKeyValueTypes(dictionaryType, out type, out result);
			return result;
		}

		public static Type GetDictionaryKeyType(Type dictionaryType)
		{
			Type result;
			Type type;
			ReflectionUtils.GetDictionaryKeyValueTypes(dictionaryType, out result, out type);
			return result;
		}

		public static bool ItemsUnitializedValue<T>(IList<T> list)
		{
			ValidationUtils.ArgumentNotNull(list, "list");
			Type collectionItemType = ReflectionUtils.GetCollectionItemType(list.GetType());
			if (collectionItemType.IsValueType)
			{
				object obj = ReflectionUtils.CreateUnitializedValue(collectionItemType);
				for (int i = 0; i < list.Count; i++)
				{
					T t = list[i];
					if (!t.Equals(obj))
					{
						return false;
					}
				}
			}
			else
			{
				if (!collectionItemType.IsClass)
				{
					throw new Exception("Type {0} is neither a ValueType or a Class.".FormatWith(CultureInfo.InvariantCulture, new object[]
					{
						collectionItemType
					}));
				}
				for (int j = 0; j < list.Count; j++)
				{
					object obj2 = list[j];
					if (obj2 != null)
					{
						return false;
					}
				}
			}
			return true;
		}

		public static Type GetMemberUnderlyingType(MemberInfo member)
		{
			ValidationUtils.ArgumentNotNull(member, "member");
			MemberTypes memberType = member.MemberType;
			switch (memberType)
			{
			case MemberTypes.Event:
				return ((EventInfo)member).EventHandlerType;
			default:
				if (memberType != MemberTypes.Property)
				{
					throw new ArgumentException("MemberInfo must be of type FieldInfo, PropertyInfo or EventInfo", "member");
				}
				return ((PropertyInfo)member).PropertyType;
			case MemberTypes.Field:
				return ((FieldInfo)member).FieldType;
			}
		}

		public static bool IsIndexedProperty(MemberInfo member)
		{
			ValidationUtils.ArgumentNotNull(member, "member");
			PropertyInfo propertyInfo = member as PropertyInfo;
			return propertyInfo != null && ReflectionUtils.IsIndexedProperty(propertyInfo);
		}

		public static bool IsIndexedProperty(PropertyInfo property)
		{
			ValidationUtils.ArgumentNotNull(property, "property");
			return property.GetIndexParameters().Length > 0;
		}

		public static object GetMemberValue(MemberInfo member, object target)
		{
			ValidationUtils.ArgumentNotNull(member, "member");
			ValidationUtils.ArgumentNotNull(target, "target");
			MemberTypes memberType = member.MemberType;
			if (memberType != MemberTypes.Field)
			{
				if (memberType == MemberTypes.Property)
				{
					try
					{
						return ((PropertyInfo)member).GetValue(target, null);
					}
					catch (TargetParameterCountException innerException)
					{
						throw new ArgumentException("MemberInfo '{0}' has index parameters".FormatWith(CultureInfo.InvariantCulture, new object[]
						{
							member.Name
						}), innerException);
					}
				}
				throw new ArgumentException("MemberInfo '{0}' is not of type FieldInfo or PropertyInfo".FormatWith(CultureInfo.InvariantCulture, new object[]
				{
					CultureInfo.InvariantCulture,
					member.Name
				}), "member");
			}
			return ((FieldInfo)member).GetValue(target);
		}

		public static void SetMemberValue(MemberInfo member, object target, object value)
		{
			ValidationUtils.ArgumentNotNull(member, "member");
			ValidationUtils.ArgumentNotNull(target, "target");
			MemberTypes memberType = member.MemberType;
			if (memberType != MemberTypes.Field)
			{
				if (memberType != MemberTypes.Property)
				{
					throw new ArgumentException("MemberInfo '{0}' must be of type FieldInfo or PropertyInfo".FormatWith(CultureInfo.InvariantCulture, new object[]
					{
						member.Name
					}), "member");
				}
				((PropertyInfo)member).SetValue(target, value, null);
			}
			else
			{
				((FieldInfo)member).SetValue(target, value);
			}
		}

		public static bool CanReadMemberValue(MemberInfo member, bool nonPublic)
		{
			MemberTypes memberType = member.MemberType;
			if (memberType == MemberTypes.Field)
			{
				FieldInfo fieldInfo = (FieldInfo)member;
				return nonPublic || fieldInfo.IsPublic;
			}
			if (memberType != MemberTypes.Property)
			{
				return false;
			}
			PropertyInfo propertyInfo = (PropertyInfo)member;
			return propertyInfo.CanRead && (nonPublic || propertyInfo.GetGetMethod(nonPublic) != null);
		}

		public static bool CanSetMemberValue(MemberInfo member, bool nonPublic, bool canSetReadOnly)
		{
			MemberTypes memberType = member.MemberType;
			if (memberType == MemberTypes.Field)
			{
				FieldInfo fieldInfo = (FieldInfo)member;
				return (!fieldInfo.IsInitOnly || canSetReadOnly) && (nonPublic || fieldInfo.IsPublic);
			}
			if (memberType != MemberTypes.Property)
			{
				return false;
			}
			PropertyInfo propertyInfo = (PropertyInfo)member;
			return propertyInfo.CanWrite && (nonPublic || propertyInfo.GetSetMethod(nonPublic) != null);
		}

		public static List<MemberInfo> GetFieldsAndProperties<T>(BindingFlags bindingAttr)
		{
			return ReflectionUtils.GetFieldsAndProperties(typeof(T), bindingAttr);
		}

		public static List<MemberInfo> GetFieldsAndProperties(Type type, BindingFlags bindingAttr)
		{
			List<MemberInfo> list = new List<MemberInfo>();
			list.AddRange(ReflectionUtils.GetFields(type, bindingAttr));
			list.AddRange(ReflectionUtils.GetProperties(type, bindingAttr));
			List<MemberInfo> list2 = new List<MemberInfo>(list.Count);
			var enumerable = from m in list
			group m by m.Name into g
			select new
			{
				Count = g.Count<MemberInfo>(),
				Members = g.Cast<MemberInfo>()
			};
			foreach (var <>__AnonType in enumerable)
			{
				if (<>__AnonType.Count == 1)
				{
					list2.Add(<>__AnonType.Members.First<MemberInfo>());
				}
				else
				{
					IEnumerable<MemberInfo> collection = from m in <>__AnonType.Members
					where !ReflectionUtils.IsOverridenGenericMember(m, bindingAttr) || m.Name == "Item"
					select m;
					list2.AddRange(collection);
				}
			}
			return list2;
		}

		private static bool IsOverridenGenericMember(MemberInfo memberInfo, BindingFlags bindingAttr)
		{
			if (memberInfo.MemberType != MemberTypes.Field && memberInfo.MemberType != MemberTypes.Property)
			{
				throw new ArgumentException("Member must be a field or property.");
			}
			Type declaringType = memberInfo.DeclaringType;
			if (!declaringType.IsGenericType)
			{
				return false;
			}
			Type genericTypeDefinition = declaringType.GetGenericTypeDefinition();
			if (genericTypeDefinition == null)
			{
				return false;
			}
			MemberInfo[] member = genericTypeDefinition.GetMember(memberInfo.Name, bindingAttr);
			if (member.Length == 0)
			{
				return false;
			}
			Type memberUnderlyingType = ReflectionUtils.GetMemberUnderlyingType(member[0]);
			return memberUnderlyingType.IsGenericParameter;
		}

		public static T GetAttribute<T>(ICustomAttributeProvider attributeProvider) where T : Attribute
		{
			return ReflectionUtils.GetAttribute<T>(attributeProvider, true);
		}

		public static T GetAttribute<T>(ICustomAttributeProvider attributeProvider, bool inherit) where T : Attribute
		{
			T[] attributes = ReflectionUtils.GetAttributes<T>(attributeProvider, inherit);
			return CollectionUtils.GetSingleItem<T>(attributes, true);
		}

		public static T[] GetAttributes<T>(ICustomAttributeProvider attributeProvider, bool inherit) where T : Attribute
		{
			ValidationUtils.ArgumentNotNull(attributeProvider, "attributeProvider");
			if (attributeProvider is Type)
			{
				return (T[])((Type)attributeProvider).GetCustomAttributes(typeof(T), inherit);
			}
			if (attributeProvider is Assembly)
			{
				return (T[])Attribute.GetCustomAttributes((Assembly)attributeProvider, typeof(T), inherit);
			}
			if (attributeProvider is MemberInfo)
			{
				return (T[])Attribute.GetCustomAttributes((MemberInfo)attributeProvider, typeof(T), inherit);
			}
			if (attributeProvider is Module)
			{
				return (T[])Attribute.GetCustomAttributes((Module)attributeProvider, typeof(T), inherit);
			}
			if (attributeProvider is ParameterInfo)
			{
				return (T[])Attribute.GetCustomAttributes((ParameterInfo)attributeProvider, typeof(T), inherit);
			}
			return (T[])attributeProvider.GetCustomAttributes(typeof(T), inherit);
		}

		public static string GetNameAndAssessmblyName(Type t)
		{
			ValidationUtils.ArgumentNotNull(t, "t");
			return t.FullName + ", " + t.Assembly.GetName().Name;
		}

		public static Type MakeGenericType(Type genericTypeDefinition, params Type[] innerTypes)
		{
			ValidationUtils.ArgumentNotNull(genericTypeDefinition, "genericTypeDefinition");
			ValidationUtils.ArgumentNotNullOrEmpty<Type>(innerTypes, "innerTypes");
			ValidationUtils.ArgumentConditionTrue(genericTypeDefinition.IsGenericTypeDefinition, "genericTypeDefinition", "Type {0} is not a generic type definition.".FormatWith(CultureInfo.InvariantCulture, new object[]
			{
				genericTypeDefinition
			}));
			return genericTypeDefinition.MakeGenericType(innerTypes);
		}

		public static object CreateGeneric(Type genericTypeDefinition, Type innerType, params object[] args)
		{
			return ReflectionUtils.CreateGeneric(genericTypeDefinition, new Type[]
			{
				innerType
			}, args);
		}

		public static object CreateGeneric(Type genericTypeDefinition, IList<Type> innerTypes, params object[] args)
		{
			return ReflectionUtils.CreateGeneric(genericTypeDefinition, innerTypes, (Type t, IList<object> a) => ReflectionUtils.CreateInstance(t, a.ToArray<object>()), args);
		}

		public static object CreateGeneric(Type genericTypeDefinition, IList<Type> innerTypes, Func<Type, IList<object>, object> instanceCreator, params object[] args)
		{
			ValidationUtils.ArgumentNotNull(genericTypeDefinition, "genericTypeDefinition");
			ValidationUtils.ArgumentNotNullOrEmpty<Type>(innerTypes, "innerTypes");
			ValidationUtils.ArgumentNotNull(instanceCreator, "createInstance");
			Type arg = ReflectionUtils.MakeGenericType(genericTypeDefinition, innerTypes.ToArray<Type>());
			return instanceCreator(arg, args);
		}

		public static bool IsCompatibleValue(object value, Type type)
		{
			if (value == null)
			{
				return ReflectionUtils.IsNullable(type);
			}
			return type.IsAssignableFrom(value.GetType());
		}

		public static object CreateInstance(Type type, params object[] args)
		{
			ValidationUtils.ArgumentNotNull(type, "type");
			return Activator.CreateInstance(type, args);
		}

		public static void SplitFullyQualifiedTypeName(string fullyQualifiedTypeName, out string typeName, out string assemblyName)
		{
			int? assemblyDelimiterIndex = ReflectionUtils.GetAssemblyDelimiterIndex(fullyQualifiedTypeName);
			if (assemblyDelimiterIndex != null)
			{
				typeName = fullyQualifiedTypeName.Substring(0, assemblyDelimiterIndex.Value).Trim();
				assemblyName = fullyQualifiedTypeName.Substring(assemblyDelimiterIndex.Value + 1, fullyQualifiedTypeName.Length - assemblyDelimiterIndex.Value - 1).Trim();
			}
			else
			{
				typeName = fullyQualifiedTypeName;
				assemblyName = null;
			}
		}

		private static int? GetAssemblyDelimiterIndex(string fullyQualifiedTypeName)
		{
			int num = 0;
			for (int i = 0; i < fullyQualifiedTypeName.Length; i++)
			{
				char c = fullyQualifiedTypeName[i];
				if (c != '[')
				{
					if (c != ']')
					{
						if (c == ',')
						{
							if (num == 0)
							{
								return new int?(i);
							}
						}
					}
					else
					{
						num--;
					}
				}
				else
				{
					num++;
				}
			}
			return null;
		}

		public static MemberInfo GetMemberInfoFromType(Type targetType, MemberInfo memberInfo)
		{
			BindingFlags bindingAttr = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static;
			MemberTypes memberType = memberInfo.MemberType;
			if (memberType != MemberTypes.Property)
			{
				return targetType.GetMember(memberInfo.Name, memberInfo.MemberType, bindingAttr).SingleOrDefault<MemberInfo>();
			}
			PropertyInfo propertyInfo = (PropertyInfo)memberInfo;
			Type[] types = (from p in propertyInfo.GetIndexParameters()
			select p.ParameterType).ToArray<Type>();
			return targetType.GetProperty(propertyInfo.Name, bindingAttr, null, propertyInfo.PropertyType, types, null);
		}

		public static IEnumerable<FieldInfo> GetFields(Type targetType, BindingFlags bindingAttr)
		{
			ValidationUtils.ArgumentNotNull(targetType, "targetType");
			List<MemberInfo> list = new List<MemberInfo>(targetType.GetFields(bindingAttr));
			ReflectionUtils.GetChildPrivateFields(list, targetType, bindingAttr);
			return list.Cast<FieldInfo>();
		}

		private static void GetChildPrivateFields(IList<MemberInfo> initialFields, Type targetType, BindingFlags bindingAttr)
		{
			if ((bindingAttr & BindingFlags.NonPublic) != BindingFlags.Default)
			{
				BindingFlags bindingAttr2 = bindingAttr.RemoveFlag(BindingFlags.Public);
				while ((targetType = targetType.BaseType) != null)
				{
					IEnumerable<MemberInfo> collection = (from f in targetType.GetFields(bindingAttr2)
					where f.IsPrivate
					select f).Cast<MemberInfo>();
					initialFields.AddRange(collection);
				}
			}
		}

		public static IEnumerable<PropertyInfo> GetProperties(Type targetType, BindingFlags bindingAttr)
		{
			ValidationUtils.ArgumentNotNull(targetType, "targetType");
			List<PropertyInfo> list = new List<PropertyInfo>(targetType.GetProperties(bindingAttr));
			ReflectionUtils.GetChildPrivateProperties(list, targetType, bindingAttr);
			for (int i = 0; i < list.Count; i++)
			{
				PropertyInfo propertyInfo = list[i];
				if (propertyInfo.DeclaringType != targetType)
				{
					PropertyInfo value = (PropertyInfo)ReflectionUtils.GetMemberInfoFromType(propertyInfo.DeclaringType, propertyInfo);
					list[i] = value;
				}
			}
			return list;
		}

		public static BindingFlags RemoveFlag(this BindingFlags bindingAttr, BindingFlags flag)
		{
			return ((bindingAttr & flag) != flag) ? bindingAttr : (bindingAttr ^ flag);
		}

		private static void GetChildPrivateProperties(IList<PropertyInfo> initialProperties, Type targetType, BindingFlags bindingAttr)
		{
			if ((bindingAttr & BindingFlags.NonPublic) != BindingFlags.Default)
			{
				BindingFlags bindingAttr2 = bindingAttr.RemoveFlag(BindingFlags.Public);
				while ((targetType = targetType.BaseType) != null)
				{
					PropertyInfo[] properties = targetType.GetProperties(bindingAttr2);
					for (int i = 0; i < properties.Length; i++)
					{
						PropertyInfo nonPublicProperty2 = properties[i];
						PropertyInfo nonPublicProperty = nonPublicProperty2;
						int num = initialProperties.IndexOf((PropertyInfo p) => p.Name == nonPublicProperty.Name);
						if (num == -1)
						{
							initialProperties.Add(nonPublicProperty);
						}
						else
						{
							initialProperties[num] = nonPublicProperty;
						}
					}
				}
			}
		}
	}
}
