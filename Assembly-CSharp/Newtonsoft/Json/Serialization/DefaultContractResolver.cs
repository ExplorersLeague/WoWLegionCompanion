using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Serialization
{
	public class DefaultContractResolver : IContractResolver
	{
		public DefaultContractResolver() : this(false)
		{
		}

		public DefaultContractResolver(bool shareCache)
		{
			this.DefaultMembersSearchFlags = (BindingFlags.Instance | BindingFlags.Public);
			this._sharedCache = shareCache;
		}

		internal static IContractResolver Instance
		{
			get
			{
				return DefaultContractResolver._instance;
			}
		}

		public bool DynamicCodeGeneration
		{
			get
			{
				return JsonTypeReflector.DynamicCodeGeneration;
			}
		}

		public BindingFlags DefaultMembersSearchFlags { get; set; }

		public bool SerializeCompilerGeneratedMembers { get; set; }

		private Dictionary<ResolverContractKey, JsonContract> GetCache()
		{
			if (this._sharedCache)
			{
				return DefaultContractResolver._sharedContractCache;
			}
			return this._instanceContractCache;
		}

		private void UpdateCache(Dictionary<ResolverContractKey, JsonContract> cache)
		{
			if (this._sharedCache)
			{
				DefaultContractResolver._sharedContractCache = cache;
			}
			else
			{
				this._instanceContractCache = cache;
			}
		}

		public virtual JsonContract ResolveContract(Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			ResolverContractKey key = new ResolverContractKey(base.GetType(), type);
			Dictionary<ResolverContractKey, JsonContract> cache = this.GetCache();
			JsonContract jsonContract;
			if (cache == null || !cache.TryGetValue(key, out jsonContract))
			{
				jsonContract = this.CreateContract(type);
				object typeContractCacheLock = DefaultContractResolver._typeContractCacheLock;
				lock (typeContractCacheLock)
				{
					cache = this.GetCache();
					Dictionary<ResolverContractKey, JsonContract> dictionary = (cache == null) ? new Dictionary<ResolverContractKey, JsonContract>() : new Dictionary<ResolverContractKey, JsonContract>(cache);
					dictionary[key] = jsonContract;
					this.UpdateCache(dictionary);
				}
			}
			return jsonContract;
		}

		protected virtual List<MemberInfo> GetSerializableMembers(Type objectType)
		{
			DataContractAttribute dataContractAttribute = JsonTypeReflector.GetDataContractAttribute(objectType);
			List<MemberInfo> list = (from m in ReflectionUtils.GetFieldsAndProperties(objectType, this.DefaultMembersSearchFlags)
			where !ReflectionUtils.IsIndexedProperty(m)
			select m).ToList<MemberInfo>();
			List<MemberInfo> list2 = (from m in ReflectionUtils.GetFieldsAndProperties(objectType, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static)
			where !ReflectionUtils.IsIndexedProperty(m)
			select m).ToList<MemberInfo>();
			List<MemberInfo> list3 = new List<MemberInfo>();
			foreach (MemberInfo memberInfo in list2)
			{
				if (this.SerializeCompilerGeneratedMembers || !memberInfo.IsDefined(typeof(CompilerGeneratedAttribute), true))
				{
					if (list.Contains(memberInfo))
					{
						list3.Add(memberInfo);
					}
					else if (JsonTypeReflector.GetAttribute<JsonPropertyAttribute>(memberInfo) != null)
					{
						list3.Add(memberInfo);
					}
					else if (dataContractAttribute != null && JsonTypeReflector.GetAttribute<DataMemberAttribute>(memberInfo) != null)
					{
						list3.Add(memberInfo);
					}
				}
			}
			Type type;
			if (objectType.AssignableToTypeName("System.Data.Objects.DataClasses.EntityObject", out type))
			{
				list3 = list3.Where(new Func<MemberInfo, bool>(this.ShouldSerializeEntityMember)).ToList<MemberInfo>();
			}
			return list3;
		}

		private bool ShouldSerializeEntityMember(MemberInfo memberInfo)
		{
			PropertyInfo propertyInfo = memberInfo as PropertyInfo;
			return propertyInfo == null || !propertyInfo.PropertyType.IsGenericType || !(propertyInfo.PropertyType.GetGenericTypeDefinition().FullName == "System.Data.Objects.DataClasses.EntityReference`1");
		}

		protected virtual JsonObjectContract CreateObjectContract(Type objectType)
		{
			JsonObjectContract jsonObjectContract = new JsonObjectContract(objectType);
			this.InitializeContract(jsonObjectContract);
			jsonObjectContract.MemberSerialization = JsonTypeReflector.GetObjectMemberSerialization(objectType);
			jsonObjectContract.Properties.AddRange(this.CreateProperties(jsonObjectContract.UnderlyingType, jsonObjectContract.MemberSerialization));
			if (objectType.GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public).Any((ConstructorInfo c) => c.IsDefined(typeof(JsonConstructorAttribute), true)))
			{
				ConstructorInfo attributeConstructor = this.GetAttributeConstructor(objectType);
				if (attributeConstructor != null)
				{
					jsonObjectContract.OverrideConstructor = attributeConstructor;
					jsonObjectContract.ConstructorParameters.AddRange(this.CreateConstructorParameters(attributeConstructor, jsonObjectContract.Properties));
				}
			}
			else if (jsonObjectContract.DefaultCreator == null || jsonObjectContract.DefaultCreatorNonPublic)
			{
				ConstructorInfo parametrizedConstructor = this.GetParametrizedConstructor(objectType);
				if (parametrizedConstructor != null)
				{
					jsonObjectContract.ParametrizedConstructor = parametrizedConstructor;
					jsonObjectContract.ConstructorParameters.AddRange(this.CreateConstructorParameters(parametrizedConstructor, jsonObjectContract.Properties));
				}
			}
			return jsonObjectContract;
		}

		private ConstructorInfo GetAttributeConstructor(Type objectType)
		{
			IList<ConstructorInfo> list = (from c in objectType.GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public)
			where c.IsDefined(typeof(JsonConstructorAttribute), true)
			select c).ToList<ConstructorInfo>();
			if (list.Count > 1)
			{
				throw new Exception("Multiple constructors with the JsonConstructorAttribute.");
			}
			if (list.Count == 1)
			{
				return list[0];
			}
			return null;
		}

		private ConstructorInfo GetParametrizedConstructor(Type objectType)
		{
			IList<ConstructorInfo> constructors = objectType.GetConstructors(BindingFlags.Instance | BindingFlags.Public);
			if (constructors.Count == 1)
			{
				return constructors[0];
			}
			return null;
		}

		protected virtual IList<JsonProperty> CreateConstructorParameters(ConstructorInfo constructor, JsonPropertyCollection memberProperties)
		{
			ParameterInfo[] parameters = constructor.GetParameters();
			JsonPropertyCollection jsonPropertyCollection = new JsonPropertyCollection(constructor.DeclaringType);
			foreach (ParameterInfo parameterInfo in parameters)
			{
				JsonProperty jsonProperty = memberProperties.GetClosestMatchProperty(parameterInfo.Name);
				if (jsonProperty != null && jsonProperty.PropertyType != parameterInfo.ParameterType)
				{
					jsonProperty = null;
				}
				JsonProperty jsonProperty2 = this.CreatePropertyFromConstructorParameter(jsonProperty, parameterInfo);
				if (jsonProperty2 != null)
				{
					jsonPropertyCollection.AddProperty(jsonProperty2);
				}
			}
			return jsonPropertyCollection;
		}

		protected virtual JsonProperty CreatePropertyFromConstructorParameter(JsonProperty matchingMemberProperty, ParameterInfo parameterInfo)
		{
			JsonProperty jsonProperty = new JsonProperty();
			jsonProperty.PropertyType = parameterInfo.ParameterType;
			bool flag;
			bool flag2;
			this.SetPropertySettingsFromAttributes(jsonProperty, parameterInfo, parameterInfo.Name, parameterInfo.Member.DeclaringType, MemberSerialization.OptOut, out flag, out flag2);
			jsonProperty.Readable = false;
			jsonProperty.Writable = true;
			if (matchingMemberProperty != null)
			{
				jsonProperty.PropertyName = ((!(jsonProperty.PropertyName != parameterInfo.Name)) ? matchingMemberProperty.PropertyName : jsonProperty.PropertyName);
				jsonProperty.Converter = (jsonProperty.Converter ?? matchingMemberProperty.Converter);
				jsonProperty.MemberConverter = (jsonProperty.MemberConverter ?? matchingMemberProperty.MemberConverter);
				jsonProperty.DefaultValue = (jsonProperty.DefaultValue ?? matchingMemberProperty.DefaultValue);
				jsonProperty.Required = ((jsonProperty.Required == Required.Default) ? matchingMemberProperty.Required : jsonProperty.Required);
				JsonProperty jsonProperty2 = jsonProperty;
				bool? isReference = jsonProperty.IsReference;
				jsonProperty2.IsReference = ((isReference == null) ? matchingMemberProperty.IsReference : isReference);
				JsonProperty jsonProperty3 = jsonProperty;
				NullValueHandling? nullValueHandling = jsonProperty.NullValueHandling;
				jsonProperty3.NullValueHandling = ((nullValueHandling == null) ? matchingMemberProperty.NullValueHandling : nullValueHandling);
				JsonProperty jsonProperty4 = jsonProperty;
				DefaultValueHandling? defaultValueHandling = jsonProperty.DefaultValueHandling;
				jsonProperty4.DefaultValueHandling = ((defaultValueHandling == null) ? matchingMemberProperty.DefaultValueHandling : defaultValueHandling);
				JsonProperty jsonProperty5 = jsonProperty;
				ReferenceLoopHandling? referenceLoopHandling = jsonProperty.ReferenceLoopHandling;
				jsonProperty5.ReferenceLoopHandling = ((referenceLoopHandling == null) ? matchingMemberProperty.ReferenceLoopHandling : referenceLoopHandling);
				JsonProperty jsonProperty6 = jsonProperty;
				ObjectCreationHandling? objectCreationHandling = jsonProperty.ObjectCreationHandling;
				jsonProperty6.ObjectCreationHandling = ((objectCreationHandling == null) ? matchingMemberProperty.ObjectCreationHandling : objectCreationHandling);
				JsonProperty jsonProperty7 = jsonProperty;
				TypeNameHandling? typeNameHandling = jsonProperty.TypeNameHandling;
				jsonProperty7.TypeNameHandling = ((typeNameHandling == null) ? matchingMemberProperty.TypeNameHandling : typeNameHandling);
			}
			return jsonProperty;
		}

		protected virtual JsonConverter ResolveContractConverter(Type objectType)
		{
			return JsonTypeReflector.GetJsonConverter(objectType, objectType);
		}

		private Func<object> GetDefaultCreator(Type createdType)
		{
			return JsonTypeReflector.ReflectionDelegateFactory.CreateDefaultConstructor<object>(createdType);
		}

		private void InitializeContract(JsonContract contract)
		{
			JsonContainerAttribute jsonContainerAttribute = JsonTypeReflector.GetJsonContainerAttribute(contract.UnderlyingType);
			if (jsonContainerAttribute != null)
			{
				contract.IsReference = jsonContainerAttribute._isReference;
			}
			else
			{
				DataContractAttribute dataContractAttribute = JsonTypeReflector.GetDataContractAttribute(contract.UnderlyingType);
				if (dataContractAttribute != null && dataContractAttribute.IsReference)
				{
					contract.IsReference = new bool?(true);
				}
			}
			contract.Converter = this.ResolveContractConverter(contract.UnderlyingType);
			contract.InternalConverter = JsonSerializer.GetMatchingConverter(DefaultContractResolver.BuiltInConverters, contract.UnderlyingType);
			if (ReflectionUtils.HasDefaultConstructor(contract.CreatedType, true) || contract.CreatedType.IsValueType)
			{
				contract.DefaultCreator = this.GetDefaultCreator(contract.CreatedType);
				contract.DefaultCreatorNonPublic = (!contract.CreatedType.IsValueType && ReflectionUtils.GetDefaultConstructor(contract.CreatedType) == null);
			}
			this.ResolveCallbackMethods(contract, contract.UnderlyingType);
		}

		private void ResolveCallbackMethods(JsonContract contract, Type t)
		{
			if (t.BaseType != null)
			{
				this.ResolveCallbackMethods(contract, t.BaseType);
			}
			MethodInfo methodInfo;
			MethodInfo methodInfo2;
			MethodInfo methodInfo3;
			MethodInfo methodInfo4;
			MethodInfo methodInfo5;
			this.GetCallbackMethodsForType(t, out methodInfo, out methodInfo2, out methodInfo3, out methodInfo4, out methodInfo5);
			if (methodInfo != null)
			{
				contract.OnSerializing = methodInfo;
			}
			if (methodInfo2 != null)
			{
				contract.OnSerialized = methodInfo2;
			}
			if (methodInfo3 != null)
			{
				contract.OnDeserializing = methodInfo3;
			}
			if (methodInfo4 != null)
			{
				contract.OnDeserialized = methodInfo4;
			}
			if (methodInfo5 != null)
			{
				contract.OnError = methodInfo5;
			}
		}

		private void GetCallbackMethodsForType(Type type, out MethodInfo onSerializing, out MethodInfo onSerialized, out MethodInfo onDeserializing, out MethodInfo onDeserialized, out MethodInfo onError)
		{
			onSerializing = null;
			onSerialized = null;
			onDeserializing = null;
			onDeserialized = null;
			onError = null;
			foreach (MethodInfo methodInfo in type.GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public))
			{
				if (!methodInfo.ContainsGenericParameters)
				{
					Type type2 = null;
					ParameterInfo[] parameters = methodInfo.GetParameters();
					if (DefaultContractResolver.IsValidCallback(methodInfo, parameters, typeof(OnSerializingAttribute), onSerializing, ref type2))
					{
						onSerializing = methodInfo;
					}
					if (DefaultContractResolver.IsValidCallback(methodInfo, parameters, typeof(OnSerializedAttribute), onSerialized, ref type2))
					{
						onSerialized = methodInfo;
					}
					if (DefaultContractResolver.IsValidCallback(methodInfo, parameters, typeof(OnDeserializingAttribute), onDeserializing, ref type2))
					{
						onDeserializing = methodInfo;
					}
					if (DefaultContractResolver.IsValidCallback(methodInfo, parameters, typeof(OnDeserializedAttribute), onDeserialized, ref type2))
					{
						onDeserialized = methodInfo;
					}
					if (DefaultContractResolver.IsValidCallback(methodInfo, parameters, typeof(OnErrorAttribute), onError, ref type2))
					{
						onError = methodInfo;
					}
				}
			}
		}

		protected virtual JsonDictionaryContract CreateDictionaryContract(Type objectType)
		{
			JsonDictionaryContract jsonDictionaryContract = new JsonDictionaryContract(objectType);
			this.InitializeContract(jsonDictionaryContract);
			jsonDictionaryContract.PropertyNameResolver = new Func<string, string>(this.ResolvePropertyName);
			return jsonDictionaryContract;
		}

		protected virtual JsonArrayContract CreateArrayContract(Type objectType)
		{
			JsonArrayContract jsonArrayContract = new JsonArrayContract(objectType);
			this.InitializeContract(jsonArrayContract);
			return jsonArrayContract;
		}

		protected virtual JsonPrimitiveContract CreatePrimitiveContract(Type objectType)
		{
			JsonPrimitiveContract jsonPrimitiveContract = new JsonPrimitiveContract(objectType);
			this.InitializeContract(jsonPrimitiveContract);
			return jsonPrimitiveContract;
		}

		protected virtual JsonLinqContract CreateLinqContract(Type objectType)
		{
			JsonLinqContract jsonLinqContract = new JsonLinqContract(objectType);
			this.InitializeContract(jsonLinqContract);
			return jsonLinqContract;
		}

		protected virtual JsonISerializableContract CreateISerializableContract(Type objectType)
		{
			JsonISerializableContract jsonISerializableContract = new JsonISerializableContract(objectType);
			this.InitializeContract(jsonISerializableContract);
			ConstructorInfo constructor = objectType.GetConstructor(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public, null, new Type[]
			{
				typeof(SerializationInfo),
				typeof(StreamingContext)
			}, null);
			if (constructor != null)
			{
				MethodCall<object, object> methodCall = JsonTypeReflector.ReflectionDelegateFactory.CreateMethodCall<object>(constructor);
				jsonISerializableContract.ISerializableCreator = ((object[] args) => methodCall(null, args));
			}
			return jsonISerializableContract;
		}

		protected virtual JsonStringContract CreateStringContract(Type objectType)
		{
			JsonStringContract jsonStringContract = new JsonStringContract(objectType);
			this.InitializeContract(jsonStringContract);
			return jsonStringContract;
		}

		protected virtual JsonContract CreateContract(Type objectType)
		{
			Type type = ReflectionUtils.EnsureNotNullableType(objectType);
			if (JsonConvert.IsJsonPrimitiveType(type))
			{
				return this.CreatePrimitiveContract(type);
			}
			if (JsonTypeReflector.GetJsonObjectAttribute(type) != null)
			{
				return this.CreateObjectContract(type);
			}
			if (JsonTypeReflector.GetJsonArrayAttribute(type) != null)
			{
				return this.CreateArrayContract(type);
			}
			if (type == typeof(JToken) || type.IsSubclassOf(typeof(JToken)))
			{
				return this.CreateLinqContract(type);
			}
			if (CollectionUtils.IsDictionaryType(type))
			{
				return this.CreateDictionaryContract(type);
			}
			if (typeof(IEnumerable).IsAssignableFrom(type))
			{
				return this.CreateArrayContract(type);
			}
			if (DefaultContractResolver.CanConvertToString(type))
			{
				return this.CreateStringContract(type);
			}
			if (typeof(ISerializable).IsAssignableFrom(type))
			{
				return this.CreateISerializableContract(type);
			}
			return this.CreateObjectContract(type);
		}

		internal static bool CanConvertToString(Type type)
		{
			TypeConverter converter = ConvertUtils.GetConverter(type);
			return (converter != null && !(converter is ComponentConverter) && !(converter is ReferenceConverter) && converter.GetType() != typeof(TypeConverter) && converter.CanConvertTo(typeof(string))) || (type == typeof(Type) || type.IsSubclassOf(typeof(Type)));
		}

		private static bool IsValidCallback(MethodInfo method, ParameterInfo[] parameters, Type attributeType, MethodInfo currentCallback, ref Type prevAttributeType)
		{
			if (!method.IsDefined(attributeType, false))
			{
				return false;
			}
			if (currentCallback != null)
			{
				throw new Exception("Invalid attribute. Both '{0}' and '{1}' in type '{2}' have '{3}'.".FormatWith(CultureInfo.InvariantCulture, new object[]
				{
					method,
					currentCallback,
					DefaultContractResolver.GetClrTypeFullName(method.DeclaringType),
					attributeType
				}));
			}
			if (prevAttributeType != null)
			{
				throw new Exception("Invalid Callback. Method '{3}' in type '{2}' has both '{0}' and '{1}'.".FormatWith(CultureInfo.InvariantCulture, new object[]
				{
					prevAttributeType,
					attributeType,
					DefaultContractResolver.GetClrTypeFullName(method.DeclaringType),
					method
				}));
			}
			if (method.IsVirtual)
			{
				throw new Exception("Virtual Method '{0}' of type '{1}' cannot be marked with '{2}' attribute.".FormatWith(CultureInfo.InvariantCulture, new object[]
				{
					method,
					DefaultContractResolver.GetClrTypeFullName(method.DeclaringType),
					attributeType
				}));
			}
			if (method.ReturnType != typeof(void))
			{
				throw new Exception("Serialization Callback '{1}' in type '{0}' must return void.".FormatWith(CultureInfo.InvariantCulture, new object[]
				{
					DefaultContractResolver.GetClrTypeFullName(method.DeclaringType),
					method
				}));
			}
			if (attributeType == typeof(OnErrorAttribute))
			{
				if (parameters == null || parameters.Length != 2 || parameters[0].ParameterType != typeof(StreamingContext) || parameters[1].ParameterType != typeof(ErrorContext))
				{
					throw new Exception("Serialization Error Callback '{1}' in type '{0}' must have two parameters of type '{2}' and '{3}'.".FormatWith(CultureInfo.InvariantCulture, new object[]
					{
						DefaultContractResolver.GetClrTypeFullName(method.DeclaringType),
						method,
						typeof(StreamingContext),
						typeof(ErrorContext)
					}));
				}
			}
			else if (parameters == null || parameters.Length != 1 || parameters[0].ParameterType != typeof(StreamingContext))
			{
				throw new Exception("Serialization Callback '{1}' in type '{0}' must have a single parameter of type '{2}'.".FormatWith(CultureInfo.InvariantCulture, new object[]
				{
					DefaultContractResolver.GetClrTypeFullName(method.DeclaringType),
					method,
					typeof(StreamingContext)
				}));
			}
			prevAttributeType = attributeType;
			return true;
		}

		internal static string GetClrTypeFullName(Type type)
		{
			if (type.IsGenericTypeDefinition || !type.ContainsGenericParameters)
			{
				return type.FullName;
			}
			return string.Format(CultureInfo.InvariantCulture, "{0}.{1}", new object[]
			{
				type.Namespace,
				type.Name
			});
		}

		protected virtual IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
		{
			List<MemberInfo> serializableMembers = this.GetSerializableMembers(type);
			if (serializableMembers == null)
			{
				throw new JsonSerializationException("Null collection of seralizable members returned.");
			}
			JsonPropertyCollection jsonPropertyCollection = new JsonPropertyCollection(type);
			foreach (MemberInfo member in serializableMembers)
			{
				JsonProperty jsonProperty = this.CreateProperty(member, memberSerialization);
				if (jsonProperty != null)
				{
					jsonPropertyCollection.AddProperty(jsonProperty);
				}
			}
			return jsonPropertyCollection.OrderBy(delegate(JsonProperty p)
			{
				int? order = p.Order;
				return (order == null) ? -1 : order.Value;
			}).ToList<JsonProperty>();
		}

		protected virtual IValueProvider CreateMemberValueProvider(MemberInfo member)
		{
			return new ReflectionValueProvider(member);
		}

		protected virtual JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
		{
			JsonProperty jsonProperty = new JsonProperty();
			jsonProperty.PropertyType = ReflectionUtils.GetMemberUnderlyingType(member);
			jsonProperty.ValueProvider = this.CreateMemberValueProvider(member);
			bool flag;
			bool canSetReadOnly;
			this.SetPropertySettingsFromAttributes(jsonProperty, member, member.Name, member.DeclaringType, memberSerialization, out flag, out canSetReadOnly);
			jsonProperty.Readable = ReflectionUtils.CanReadMemberValue(member, flag);
			jsonProperty.Writable = ReflectionUtils.CanSetMemberValue(member, flag, canSetReadOnly);
			jsonProperty.ShouldSerialize = this.CreateShouldSerializeTest(member);
			this.SetIsSpecifiedActions(jsonProperty, member, flag);
			return jsonProperty;
		}

		private void SetPropertySettingsFromAttributes(JsonProperty property, ICustomAttributeProvider attributeProvider, string name, Type declaringType, MemberSerialization memberSerialization, out bool allowNonPublicAccess, out bool hasExplicitAttribute)
		{
			hasExplicitAttribute = false;
			DataContractAttribute dataContractAttribute = JsonTypeReflector.GetDataContractAttribute(declaringType);
			DataMemberAttribute dataMemberAttribute;
			if (dataContractAttribute != null && attributeProvider is MemberInfo)
			{
				dataMemberAttribute = JsonTypeReflector.GetDataMemberAttribute((MemberInfo)attributeProvider);
			}
			else
			{
				dataMemberAttribute = null;
			}
			JsonPropertyAttribute attribute = JsonTypeReflector.GetAttribute<JsonPropertyAttribute>(attributeProvider);
			if (attribute != null)
			{
				hasExplicitAttribute = true;
			}
			bool flag = JsonTypeReflector.GetAttribute<JsonIgnoreAttribute>(attributeProvider) != null;
			string propertyName;
			if (attribute != null && attribute.PropertyName != null)
			{
				propertyName = attribute.PropertyName;
			}
			else if (dataMemberAttribute != null && dataMemberAttribute.Name != null)
			{
				propertyName = dataMemberAttribute.Name;
			}
			else
			{
				propertyName = name;
			}
			property.PropertyName = this.ResolvePropertyName(propertyName);
			property.UnderlyingName = name;
			if (attribute != null)
			{
				property.Required = attribute.Required;
				property.Order = attribute._order;
			}
			else if (dataMemberAttribute != null)
			{
				property.Required = ((!dataMemberAttribute.IsRequired) ? Required.Default : Required.AllowNull);
				property.Order = ((dataMemberAttribute.Order == -1) ? null : new int?(dataMemberAttribute.Order));
			}
			else
			{
				property.Required = Required.Default;
			}
			property.Ignored = (flag || (memberSerialization == MemberSerialization.OptIn && attribute == null && dataMemberAttribute == null));
			property.Converter = JsonTypeReflector.GetJsonConverter(attributeProvider, property.PropertyType);
			property.MemberConverter = JsonTypeReflector.GetJsonConverter(attributeProvider, property.PropertyType);
			DefaultValueAttribute attribute2 = JsonTypeReflector.GetAttribute<DefaultValueAttribute>(attributeProvider);
			property.DefaultValue = ((attribute2 == null) ? null : attribute2.Value);
			property.NullValueHandling = ((attribute == null) ? null : attribute._nullValueHandling);
			property.DefaultValueHandling = ((attribute == null) ? null : attribute._defaultValueHandling);
			property.ReferenceLoopHandling = ((attribute == null) ? null : attribute._referenceLoopHandling);
			property.ObjectCreationHandling = ((attribute == null) ? null : attribute._objectCreationHandling);
			property.TypeNameHandling = ((attribute == null) ? null : attribute._typeNameHandling);
			property.IsReference = ((attribute == null) ? null : attribute._isReference);
			allowNonPublicAccess = false;
			if ((this.DefaultMembersSearchFlags & BindingFlags.NonPublic) == BindingFlags.NonPublic)
			{
				allowNonPublicAccess = true;
			}
			if (attribute != null)
			{
				allowNonPublicAccess = true;
			}
			if (dataMemberAttribute != null)
			{
				allowNonPublicAccess = true;
				hasExplicitAttribute = true;
			}
		}

		private Predicate<object> CreateShouldSerializeTest(MemberInfo member)
		{
			MethodInfo method = member.DeclaringType.GetMethod("ShouldSerialize" + member.Name, new Type[0]);
			if (method == null || method.ReturnType != typeof(bool))
			{
				return null;
			}
			MethodCall<object, object> shouldSerializeCall = JsonTypeReflector.ReflectionDelegateFactory.CreateMethodCall<object>(method);
			return (object o) => (bool)shouldSerializeCall(o, new object[0]);
		}

		private void SetIsSpecifiedActions(JsonProperty property, MemberInfo member, bool allowNonPublicAccess)
		{
			MemberInfo memberInfo = member.DeclaringType.GetProperty(member.Name + "Specified");
			if (memberInfo == null)
			{
				memberInfo = member.DeclaringType.GetField(member.Name + "Specified");
			}
			if (memberInfo == null || ReflectionUtils.GetMemberUnderlyingType(memberInfo) != typeof(bool))
			{
				return;
			}
			Func<object, object> specifiedPropertyGet = JsonTypeReflector.ReflectionDelegateFactory.CreateGet<object>(memberInfo);
			property.GetIsSpecified = ((object o) => (bool)specifiedPropertyGet(o));
			if (ReflectionUtils.CanSetMemberValue(memberInfo, allowNonPublicAccess, false))
			{
				property.SetIsSpecified = JsonTypeReflector.ReflectionDelegateFactory.CreateSet<object>(memberInfo);
			}
		}

		protected internal virtual string ResolvePropertyName(string propertyName)
		{
			return propertyName;
		}

		private static readonly IContractResolver _instance = new DefaultContractResolver(true);

		private static readonly IList<JsonConverter> BuiltInConverters = new List<JsonConverter>
		{
			new KeyValuePairConverter(),
			new BsonObjectIdConverter()
		};

		private static Dictionary<ResolverContractKey, JsonContract> _sharedContractCache;

		private static readonly object _typeContractCacheLock = new object();

		private Dictionary<ResolverContractKey, JsonContract> _instanceContractCache;

		private readonly bool _sharedCache;
	}
}
