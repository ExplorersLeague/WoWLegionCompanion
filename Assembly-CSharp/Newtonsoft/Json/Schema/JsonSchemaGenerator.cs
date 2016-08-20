using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Schema
{
	public class JsonSchemaGenerator
	{
		public UndefinedSchemaIdHandling UndefinedSchemaIdHandling { get; set; }

		public IContractResolver ContractResolver
		{
			get
			{
				if (this._contractResolver == null)
				{
					return DefaultContractResolver.Instance;
				}
				return this._contractResolver;
			}
			set
			{
				this._contractResolver = value;
			}
		}

		private JsonSchema CurrentSchema
		{
			get
			{
				return this._currentSchema;
			}
		}

		private void Push(JsonSchemaGenerator.TypeSchema typeSchema)
		{
			this._currentSchema = typeSchema.Schema;
			this._stack.Add(typeSchema);
			this._resolver.LoadedSchemas.Add(typeSchema.Schema);
		}

		private JsonSchemaGenerator.TypeSchema Pop()
		{
			JsonSchemaGenerator.TypeSchema result = this._stack[this._stack.Count - 1];
			this._stack.RemoveAt(this._stack.Count - 1);
			JsonSchemaGenerator.TypeSchema typeSchema = this._stack.LastOrDefault<JsonSchemaGenerator.TypeSchema>();
			if (typeSchema != null)
			{
				this._currentSchema = typeSchema.Schema;
			}
			else
			{
				this._currentSchema = null;
			}
			return result;
		}

		public JsonSchema Generate(Type type)
		{
			return this.Generate(type, new JsonSchemaResolver(), false);
		}

		public JsonSchema Generate(Type type, JsonSchemaResolver resolver)
		{
			return this.Generate(type, resolver, false);
		}

		public JsonSchema Generate(Type type, bool rootSchemaNullable)
		{
			return this.Generate(type, new JsonSchemaResolver(), rootSchemaNullable);
		}

		public JsonSchema Generate(Type type, JsonSchemaResolver resolver, bool rootSchemaNullable)
		{
			ValidationUtils.ArgumentNotNull(type, "type");
			ValidationUtils.ArgumentNotNull(resolver, "resolver");
			this._resolver = resolver;
			return this.GenerateInternal(type, rootSchemaNullable ? Required.Default : Required.Always, false);
		}

		private string GetTitle(Type type)
		{
			JsonContainerAttribute jsonContainerAttribute = JsonTypeReflector.GetJsonContainerAttribute(type);
			if (jsonContainerAttribute != null && !string.IsNullOrEmpty(jsonContainerAttribute.Title))
			{
				return jsonContainerAttribute.Title;
			}
			return null;
		}

		private string GetDescription(Type type)
		{
			JsonContainerAttribute jsonContainerAttribute = JsonTypeReflector.GetJsonContainerAttribute(type);
			if (jsonContainerAttribute != null && !string.IsNullOrEmpty(jsonContainerAttribute.Description))
			{
				return jsonContainerAttribute.Description;
			}
			DescriptionAttribute attribute = ReflectionUtils.GetAttribute<DescriptionAttribute>(type);
			if (attribute != null)
			{
				return attribute.Description;
			}
			return null;
		}

		private string GetTypeId(Type type, bool explicitOnly)
		{
			JsonContainerAttribute jsonContainerAttribute = JsonTypeReflector.GetJsonContainerAttribute(type);
			if (jsonContainerAttribute != null && !string.IsNullOrEmpty(jsonContainerAttribute.Id))
			{
				return jsonContainerAttribute.Id;
			}
			if (explicitOnly)
			{
				return null;
			}
			UndefinedSchemaIdHandling undefinedSchemaIdHandling = this.UndefinedSchemaIdHandling;
			if (undefinedSchemaIdHandling == UndefinedSchemaIdHandling.UseTypeName)
			{
				return type.FullName;
			}
			if (undefinedSchemaIdHandling != UndefinedSchemaIdHandling.UseAssemblyQualifiedName)
			{
				return null;
			}
			return type.AssemblyQualifiedName;
		}

		private JsonSchema GenerateInternal(Type type, Required valueRequired, bool required)
		{
			ValidationUtils.ArgumentNotNull(type, "type");
			string typeId = this.GetTypeId(type, false);
			string typeId2 = this.GetTypeId(type, true);
			if (!string.IsNullOrEmpty(typeId))
			{
				JsonSchema schema = this._resolver.GetSchema(typeId);
				if (schema != null)
				{
					if (valueRequired != Required.Always && !JsonSchemaGenerator.HasFlag(schema.Type, JsonSchemaType.Null))
					{
						JsonSchema jsonSchema = schema;
						JsonSchemaType? type2 = jsonSchema.Type;
						jsonSchema.Type = ((type2 == null) ? null : new JsonSchemaType?(type2.GetValueOrDefault() | JsonSchemaType.Null));
					}
					if (required && schema.Required != true)
					{
						schema.Required = new bool?(true);
					}
					return schema;
				}
			}
			if (this._stack.Any((JsonSchemaGenerator.TypeSchema tc) => tc.Type == type))
			{
				throw new Exception("Unresolved circular reference for type '{0}'. Explicitly define an Id for the type using a JsonObject/JsonArray attribute or automatically generate a type Id using the UndefinedSchemaIdHandling property.".FormatWith(CultureInfo.InvariantCulture, new object[]
				{
					type
				}));
			}
			JsonContract jsonContract = this.ContractResolver.ResolveContract(type);
			JsonConverter jsonConverter;
			if ((jsonConverter = jsonContract.Converter) != null || (jsonConverter = jsonContract.InternalConverter) != null)
			{
				JsonSchema schema2 = jsonConverter.GetSchema();
				if (schema2 != null)
				{
					return schema2;
				}
			}
			this.Push(new JsonSchemaGenerator.TypeSchema(type, new JsonSchema()));
			if (typeId2 != null)
			{
				this.CurrentSchema.Id = typeId2;
			}
			if (required)
			{
				this.CurrentSchema.Required = new bool?(true);
			}
			this.CurrentSchema.Title = this.GetTitle(type);
			this.CurrentSchema.Description = this.GetDescription(type);
			if (jsonConverter != null)
			{
				this.CurrentSchema.Type = new JsonSchemaType?(JsonSchemaType.Any);
			}
			else if (jsonContract is JsonDictionaryContract)
			{
				this.CurrentSchema.Type = new JsonSchemaType?(this.AddNullType(JsonSchemaType.Object, valueRequired));
				Type type3;
				Type type4;
				ReflectionUtils.GetDictionaryKeyValueTypes(type, out type3, out type4);
				if (type3 != null && typeof(IConvertible).IsAssignableFrom(type3))
				{
					this.CurrentSchema.AdditionalProperties = this.GenerateInternal(type4, Required.Default, false);
				}
			}
			else if (jsonContract is JsonArrayContract)
			{
				this.CurrentSchema.Type = new JsonSchemaType?(this.AddNullType(JsonSchemaType.Array, valueRequired));
				this.CurrentSchema.Id = this.GetTypeId(type, false);
				JsonArrayAttribute jsonArrayAttribute = JsonTypeReflector.GetJsonContainerAttribute(type) as JsonArrayAttribute;
				bool flag = jsonArrayAttribute == null || jsonArrayAttribute.AllowNullItems;
				Type collectionItemType = ReflectionUtils.GetCollectionItemType(type);
				if (collectionItemType != null)
				{
					this.CurrentSchema.Items = new List<JsonSchema>();
					this.CurrentSchema.Items.Add(this.GenerateInternal(collectionItemType, flag ? Required.Default : Required.Always, false));
				}
			}
			else if (jsonContract is JsonPrimitiveContract)
			{
				this.CurrentSchema.Type = new JsonSchemaType?(this.GetJsonSchemaType(type, valueRequired));
				if (this.CurrentSchema.Type == JsonSchemaType.Integer && type.IsEnum && !type.IsDefined(typeof(FlagsAttribute), true))
				{
					this.CurrentSchema.Enum = new List<JToken>();
					this.CurrentSchema.Options = new Dictionary<JToken, string>();
					EnumValues<long> namesAndValues = EnumUtils.GetNamesAndValues<long>(type);
					foreach (EnumValue<long> enumValue in namesAndValues)
					{
						JToken jtoken = JToken.FromObject(enumValue.Value);
						this.CurrentSchema.Enum.Add(jtoken);
						this.CurrentSchema.Options.Add(jtoken, enumValue.Name);
					}
				}
			}
			else if (jsonContract is JsonObjectContract)
			{
				this.CurrentSchema.Type = new JsonSchemaType?(this.AddNullType(JsonSchemaType.Object, valueRequired));
				this.CurrentSchema.Id = this.GetTypeId(type, false);
				this.GenerateObjectSchema(type, (JsonObjectContract)jsonContract);
			}
			else if (jsonContract is JsonISerializableContract)
			{
				this.CurrentSchema.Type = new JsonSchemaType?(this.AddNullType(JsonSchemaType.Object, valueRequired));
				this.CurrentSchema.Id = this.GetTypeId(type, false);
				this.GenerateISerializableContract(type, (JsonISerializableContract)jsonContract);
			}
			else if (jsonContract is JsonStringContract)
			{
				JsonSchemaType value = ReflectionUtils.IsNullable(jsonContract.UnderlyingType) ? this.AddNullType(JsonSchemaType.String, valueRequired) : JsonSchemaType.String;
				this.CurrentSchema.Type = new JsonSchemaType?(value);
			}
			else
			{
				if (!(jsonContract is JsonLinqContract))
				{
					throw new Exception("Unexpected contract type: {0}".FormatWith(CultureInfo.InvariantCulture, new object[]
					{
						jsonContract
					}));
				}
				this.CurrentSchema.Type = new JsonSchemaType?(JsonSchemaType.Any);
			}
			return this.Pop().Schema;
		}

		private JsonSchemaType AddNullType(JsonSchemaType type, Required valueRequired)
		{
			if (valueRequired != Required.Always)
			{
				return type | JsonSchemaType.Null;
			}
			return type;
		}

		private bool HasFlag(DefaultValueHandling value, DefaultValueHandling flag)
		{
			return (value & flag) == flag;
		}

		private void GenerateObjectSchema(Type type, JsonObjectContract contract)
		{
			this.CurrentSchema.Properties = new Dictionary<string, JsonSchema>();
			foreach (JsonProperty jsonProperty in contract.Properties)
			{
				if (!jsonProperty.Ignored)
				{
					bool flag = jsonProperty.NullValueHandling == NullValueHandling.Ignore || this.HasFlag(jsonProperty.DefaultValueHandling.GetValueOrDefault(), DefaultValueHandling.Ignore) || jsonProperty.ShouldSerialize != null || jsonProperty.GetIsSpecified != null;
					JsonSchema jsonSchema = this.GenerateInternal(jsonProperty.PropertyType, jsonProperty.Required, !flag);
					if (jsonProperty.DefaultValue != null)
					{
						jsonSchema.Default = JToken.FromObject(jsonProperty.DefaultValue);
					}
					this.CurrentSchema.Properties.Add(jsonProperty.PropertyName, jsonSchema);
				}
			}
			if (type.IsSealed)
			{
				this.CurrentSchema.AllowAdditionalProperties = false;
			}
		}

		private void GenerateISerializableContract(Type type, JsonISerializableContract contract)
		{
			this.CurrentSchema.AllowAdditionalProperties = true;
		}

		internal static bool HasFlag(JsonSchemaType? value, JsonSchemaType flag)
		{
			return value == null || ((value == null) ? null : new JsonSchemaType?(value.GetValueOrDefault() & flag)) == flag;
		}

		private JsonSchemaType GetJsonSchemaType(Type type, Required valueRequired)
		{
			JsonSchemaType jsonSchemaType = JsonSchemaType.None;
			if (valueRequired != Required.Always && ReflectionUtils.IsNullable(type))
			{
				jsonSchemaType = JsonSchemaType.Null;
				if (ReflectionUtils.IsNullableType(type))
				{
					type = Nullable.GetUnderlyingType(type);
				}
			}
			TypeCode typeCode = Type.GetTypeCode(type);
			switch (typeCode)
			{
			case TypeCode.Empty:
			case TypeCode.Object:
				return jsonSchemaType | JsonSchemaType.String;
			case TypeCode.DBNull:
				return jsonSchemaType | JsonSchemaType.Null;
			case TypeCode.Boolean:
				return jsonSchemaType | JsonSchemaType.Boolean;
			case TypeCode.Char:
				return jsonSchemaType | JsonSchemaType.String;
			case TypeCode.SByte:
			case TypeCode.Byte:
			case TypeCode.Int16:
			case TypeCode.UInt16:
			case TypeCode.Int32:
			case TypeCode.UInt32:
			case TypeCode.Int64:
			case TypeCode.UInt64:
				return jsonSchemaType | JsonSchemaType.Integer;
			case TypeCode.Single:
			case TypeCode.Double:
			case TypeCode.Decimal:
				return jsonSchemaType | JsonSchemaType.Float;
			case TypeCode.DateTime:
				return jsonSchemaType | JsonSchemaType.String;
			case TypeCode.String:
				return jsonSchemaType | JsonSchemaType.String;
			}
			throw new Exception("Unexpected type code '{0}' for type '{1}'.".FormatWith(CultureInfo.InvariantCulture, new object[]
			{
				typeCode,
				type
			}));
		}

		private IContractResolver _contractResolver;

		private JsonSchemaResolver _resolver;

		private IList<JsonSchemaGenerator.TypeSchema> _stack = new List<JsonSchemaGenerator.TypeSchema>();

		private JsonSchema _currentSchema;

		private class TypeSchema
		{
			public TypeSchema(Type type, JsonSchema schema)
			{
				ValidationUtils.ArgumentNotNull(type, "type");
				ValidationUtils.ArgumentNotNull(schema, "schema");
				this.Type = type;
				this.Schema = schema;
			}

			public Type Type { get; private set; }

			public JsonSchema Schema { get; private set; }
		}
	}
}
