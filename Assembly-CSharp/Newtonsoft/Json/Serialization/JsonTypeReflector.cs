using System;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Serialization
{
	internal static class JsonTypeReflector
	{
		public static JsonContainerAttribute GetJsonContainerAttribute(Type type)
		{
			return CachedAttributeGetter<JsonContainerAttribute>.GetAttribute(type);
		}

		public static JsonObjectAttribute GetJsonObjectAttribute(Type type)
		{
			return JsonTypeReflector.GetJsonContainerAttribute(type) as JsonObjectAttribute;
		}

		public static JsonArrayAttribute GetJsonArrayAttribute(Type type)
		{
			return JsonTypeReflector.GetJsonContainerAttribute(type) as JsonArrayAttribute;
		}

		public static DataContractAttribute GetDataContractAttribute(Type type)
		{
			DataContractAttribute dataContractAttribute = null;
			Type type2 = type;
			while (dataContractAttribute == null && type2 != null)
			{
				dataContractAttribute = CachedAttributeGetter<DataContractAttribute>.GetAttribute(type2);
				type2 = type2.BaseType;
			}
			return dataContractAttribute;
		}

		public static DataMemberAttribute GetDataMemberAttribute(MemberInfo memberInfo)
		{
			if (memberInfo.MemberType == MemberTypes.Field)
			{
				return CachedAttributeGetter<DataMemberAttribute>.GetAttribute(memberInfo);
			}
			PropertyInfo propertyInfo = (PropertyInfo)memberInfo;
			DataMemberAttribute attribute = CachedAttributeGetter<DataMemberAttribute>.GetAttribute(propertyInfo);
			if (attribute == null && propertyInfo.IsVirtual())
			{
				Type type = propertyInfo.DeclaringType;
				while (attribute == null && type != null)
				{
					PropertyInfo propertyInfo2 = (PropertyInfo)ReflectionUtils.GetMemberInfoFromType(type, propertyInfo);
					if (propertyInfo2 != null && propertyInfo2.IsVirtual())
					{
						attribute = CachedAttributeGetter<DataMemberAttribute>.GetAttribute(propertyInfo2);
					}
					type = type.BaseType;
				}
			}
			return attribute;
		}

		public static MemberSerialization GetObjectMemberSerialization(Type objectType)
		{
			JsonObjectAttribute jsonObjectAttribute = JsonTypeReflector.GetJsonObjectAttribute(objectType);
			if (jsonObjectAttribute != null)
			{
				return jsonObjectAttribute.MemberSerialization;
			}
			DataContractAttribute dataContractAttribute = JsonTypeReflector.GetDataContractAttribute(objectType);
			if (dataContractAttribute != null)
			{
				return MemberSerialization.OptIn;
			}
			return MemberSerialization.OptOut;
		}

		private static Type GetJsonConverterType(ICustomAttributeProvider attributeProvider)
		{
			return JsonTypeReflector.JsonConverterTypeCache.Get(attributeProvider);
		}

		private static Type GetJsonConverterTypeFromAttribute(ICustomAttributeProvider attributeProvider)
		{
			JsonConverterAttribute attribute = JsonTypeReflector.GetAttribute<JsonConverterAttribute>(attributeProvider);
			return (attribute == null) ? null : attribute.ConverterType;
		}

		public static JsonConverter GetJsonConverter(ICustomAttributeProvider attributeProvider, Type targetConvertedType)
		{
			Type jsonConverterType = JsonTypeReflector.GetJsonConverterType(attributeProvider);
			if (jsonConverterType == null)
			{
				return null;
			}
			JsonConverter jsonConverter = JsonConverterAttribute.CreateJsonConverterInstance(jsonConverterType);
			if (!jsonConverter.CanConvert(targetConvertedType))
			{
				throw new JsonSerializationException("JsonConverter {0} on {1} is not compatible with member type {2}.".FormatWith(CultureInfo.InvariantCulture, new object[]
				{
					jsonConverter.GetType().Name,
					attributeProvider,
					targetConvertedType.Name
				}));
			}
			return jsonConverter;
		}

		public static TypeConverter GetTypeConverter(Type type)
		{
			return TypeDescriptor.GetConverter(type);
		}

		private static Type GetAssociatedMetadataType(Type type)
		{
			return JsonTypeReflector.AssociatedMetadataTypesCache.Get(type);
		}

		private static Type GetAssociateMetadataTypeFromAttribute(Type type)
		{
			Type metadataTypeAttributeType = JsonTypeReflector.GetMetadataTypeAttributeType();
			if (metadataTypeAttributeType == null)
			{
				return null;
			}
			object obj = type.GetCustomAttributes(metadataTypeAttributeType, true).SingleOrDefault<object>();
			if (obj == null)
			{
				return null;
			}
			IMetadataTypeAttribute metadataTypeAttribute = new LateBoundMetadataTypeAttribute(obj);
			return metadataTypeAttribute.MetadataClassType;
		}

		private static Type GetMetadataTypeAttributeType()
		{
			if (JsonTypeReflector._cachedMetadataTypeAttributeType == null)
			{
				Type type = Type.GetType("System.ComponentModel.DataAnnotations.MetadataTypeAttribute, System.ComponentModel.DataAnnotations, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35");
				if (type == null)
				{
					return null;
				}
				JsonTypeReflector._cachedMetadataTypeAttributeType = type;
			}
			return JsonTypeReflector._cachedMetadataTypeAttributeType;
		}

		private static T GetAttribute<T>(Type type) where T : Attribute
		{
			Type associatedMetadataType = JsonTypeReflector.GetAssociatedMetadataType(type);
			T attribute;
			if (associatedMetadataType != null)
			{
				attribute = ReflectionUtils.GetAttribute<T>(associatedMetadataType, true);
				if (attribute != null)
				{
					return attribute;
				}
			}
			attribute = ReflectionUtils.GetAttribute<T>(type, true);
			if (attribute != null)
			{
				return attribute;
			}
			foreach (Type attributeProvider in type.GetInterfaces())
			{
				attribute = ReflectionUtils.GetAttribute<T>(attributeProvider, true);
				if (attribute != null)
				{
					return attribute;
				}
			}
			return (T)((object)null);
		}

		private static T GetAttribute<T>(MemberInfo memberInfo) where T : Attribute
		{
			Type associatedMetadataType = JsonTypeReflector.GetAssociatedMetadataType(memberInfo.DeclaringType);
			T attribute;
			if (associatedMetadataType != null)
			{
				MemberInfo memberInfoFromType = ReflectionUtils.GetMemberInfoFromType(associatedMetadataType, memberInfo);
				if (memberInfoFromType != null)
				{
					attribute = ReflectionUtils.GetAttribute<T>(memberInfoFromType, true);
					if (attribute != null)
					{
						return attribute;
					}
				}
			}
			attribute = ReflectionUtils.GetAttribute<T>(memberInfo, true);
			if (attribute != null)
			{
				return attribute;
			}
			foreach (Type targetType in memberInfo.DeclaringType.GetInterfaces())
			{
				MemberInfo memberInfoFromType2 = ReflectionUtils.GetMemberInfoFromType(targetType, memberInfo);
				if (memberInfoFromType2 != null)
				{
					attribute = ReflectionUtils.GetAttribute<T>(memberInfoFromType2, true);
					if (attribute != null)
					{
						return attribute;
					}
				}
			}
			return (T)((object)null);
		}

		public static T GetAttribute<T>(ICustomAttributeProvider attributeProvider) where T : Attribute
		{
			Type type = attributeProvider as Type;
			if (type != null)
			{
				return JsonTypeReflector.GetAttribute<T>(type);
			}
			MemberInfo memberInfo = attributeProvider as MemberInfo;
			if (memberInfo != null)
			{
				return JsonTypeReflector.GetAttribute<T>(memberInfo);
			}
			return ReflectionUtils.GetAttribute<T>(attributeProvider, true);
		}

		public static bool DynamicCodeGeneration
		{
			get
			{
				bool? dynamicCodeGeneration = JsonTypeReflector._dynamicCodeGeneration;
				if (dynamicCodeGeneration == null)
				{
					JsonTypeReflector._dynamicCodeGeneration = new bool?(false);
				}
				return JsonTypeReflector._dynamicCodeGeneration.Value;
			}
		}

		public static ReflectionDelegateFactory ReflectionDelegateFactory
		{
			get
			{
				return LateBoundReflectionDelegateFactory.Instance;
			}
		}

		static JsonTypeReflector()
		{
			// Note: this type is marked as 'beforefieldinit'.
			if (JsonTypeReflector.<>f__mg$cache0 == null)
			{
				JsonTypeReflector.<>f__mg$cache0 = new Func<ICustomAttributeProvider, Type>(JsonTypeReflector.GetJsonConverterTypeFromAttribute);
			}
			JsonTypeReflector.JsonConverterTypeCache = new ThreadSafeStore<ICustomAttributeProvider, Type>(JsonTypeReflector.<>f__mg$cache0);
			if (JsonTypeReflector.<>f__mg$cache1 == null)
			{
				JsonTypeReflector.<>f__mg$cache1 = new Func<Type, Type>(JsonTypeReflector.GetAssociateMetadataTypeFromAttribute);
			}
			JsonTypeReflector.AssociatedMetadataTypesCache = new ThreadSafeStore<Type, Type>(JsonTypeReflector.<>f__mg$cache1);
		}

		public const string IdPropertyName = "$id";

		public const string RefPropertyName = "$ref";

		public const string TypePropertyName = "$type";

		public const string ValuePropertyName = "$value";

		public const string ArrayValuesPropertyName = "$values";

		public const string ShouldSerializePrefix = "ShouldSerialize";

		public const string SpecifiedPostfix = "Specified";

		private static readonly ThreadSafeStore<ICustomAttributeProvider, Type> JsonConverterTypeCache;

		private static readonly ThreadSafeStore<Type, Type> AssociatedMetadataTypesCache;

		private const string MetadataTypeAttributeTypeName = "System.ComponentModel.DataAnnotations.MetadataTypeAttribute, System.ComponentModel.DataAnnotations, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35";

		private static Type _cachedMetadataTypeAttributeType;

		private static bool? _dynamicCodeGeneration;

		[CompilerGenerated]
		private static Func<ICustomAttributeProvider, Type> <>f__mg$cache0;

		[CompilerGenerated]
		private static Func<Type, Type> <>f__mg$cache1;
	}
}
