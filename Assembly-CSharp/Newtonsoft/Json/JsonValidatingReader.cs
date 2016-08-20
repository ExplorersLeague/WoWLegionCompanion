using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json
{
	public class JsonValidatingReader : JsonReader, IJsonLineInfo
	{
		public JsonValidatingReader(JsonReader reader)
		{
			ValidationUtils.ArgumentNotNull(reader, "reader");
			this._reader = reader;
			this._stack = new Stack<JsonValidatingReader.SchemaScope>();
		}

		public event ValidationEventHandler ValidationEventHandler;

		bool IJsonLineInfo.HasLineInfo()
		{
			IJsonLineInfo jsonLineInfo = this._reader as IJsonLineInfo;
			return jsonLineInfo != null && jsonLineInfo.HasLineInfo();
		}

		int IJsonLineInfo.LineNumber
		{
			get
			{
				IJsonLineInfo jsonLineInfo = this._reader as IJsonLineInfo;
				return (jsonLineInfo == null) ? 0 : jsonLineInfo.LineNumber;
			}
		}

		int IJsonLineInfo.LinePosition
		{
			get
			{
				IJsonLineInfo jsonLineInfo = this._reader as IJsonLineInfo;
				return (jsonLineInfo == null) ? 0 : jsonLineInfo.LinePosition;
			}
		}

		public override object Value
		{
			get
			{
				return this._reader.Value;
			}
		}

		public override int Depth
		{
			get
			{
				return this._reader.Depth;
			}
		}

		public override char QuoteChar
		{
			get
			{
				return this._reader.QuoteChar;
			}
			protected internal set
			{
			}
		}

		public override JsonToken TokenType
		{
			get
			{
				return this._reader.TokenType;
			}
		}

		public override Type ValueType
		{
			get
			{
				return this._reader.ValueType;
			}
		}

		private void Push(JsonValidatingReader.SchemaScope scope)
		{
			this._stack.Push(scope);
			this._currentScope = scope;
		}

		private JsonValidatingReader.SchemaScope Pop()
		{
			JsonValidatingReader.SchemaScope result = this._stack.Pop();
			this._currentScope = ((this._stack.Count == 0) ? null : this._stack.Peek());
			return result;
		}

		private IEnumerable<JsonSchemaModel> CurrentSchemas
		{
			get
			{
				return this._currentScope.Schemas;
			}
		}

		private IEnumerable<JsonSchemaModel> CurrentMemberSchemas
		{
			get
			{
				if (this._currentScope == null)
				{
					return new List<JsonSchemaModel>(new JsonSchemaModel[]
					{
						this._model
					});
				}
				if (this._currentScope.Schemas == null || this._currentScope.Schemas.Count == 0)
				{
					return Enumerable.Empty<JsonSchemaModel>();
				}
				switch (this._currentScope.TokenType)
				{
				case JTokenType.None:
					return this._currentScope.Schemas;
				case JTokenType.Object:
				{
					if (this._currentScope.CurrentPropertyName == null)
					{
						throw new Exception("CurrentPropertyName has not been set on scope.");
					}
					IList<JsonSchemaModel> list = new List<JsonSchemaModel>();
					foreach (JsonSchemaModel jsonSchemaModel in this.CurrentSchemas)
					{
						JsonSchemaModel item;
						if (jsonSchemaModel.Properties != null && jsonSchemaModel.Properties.TryGetValue(this._currentScope.CurrentPropertyName, out item))
						{
							list.Add(item);
						}
						if (jsonSchemaModel.PatternProperties != null)
						{
							foreach (KeyValuePair<string, JsonSchemaModel> keyValuePair in jsonSchemaModel.PatternProperties)
							{
								if (Regex.IsMatch(this._currentScope.CurrentPropertyName, keyValuePair.Key))
								{
									list.Add(keyValuePair.Value);
								}
							}
						}
						if (list.Count == 0 && jsonSchemaModel.AllowAdditionalProperties && jsonSchemaModel.AdditionalProperties != null)
						{
							list.Add(jsonSchemaModel.AdditionalProperties);
						}
					}
					return list;
				}
				case JTokenType.Array:
				{
					IList<JsonSchemaModel> list2 = new List<JsonSchemaModel>();
					foreach (JsonSchemaModel jsonSchemaModel2 in this.CurrentSchemas)
					{
						if (!CollectionUtils.IsNullOrEmpty<JsonSchemaModel>(jsonSchemaModel2.Items))
						{
							if (jsonSchemaModel2.Items.Count == 1)
							{
								list2.Add(jsonSchemaModel2.Items[0]);
							}
							if (jsonSchemaModel2.Items.Count > this._currentScope.ArrayItemCount - 1)
							{
								list2.Add(jsonSchemaModel2.Items[this._currentScope.ArrayItemCount - 1]);
							}
						}
						if (jsonSchemaModel2.AllowAdditionalProperties && jsonSchemaModel2.AdditionalProperties != null)
						{
							list2.Add(jsonSchemaModel2.AdditionalProperties);
						}
					}
					return list2;
				}
				case JTokenType.Constructor:
					return Enumerable.Empty<JsonSchemaModel>();
				default:
					throw new ArgumentOutOfRangeException("TokenType", "Unexpected token type: {0}".FormatWith(CultureInfo.InvariantCulture, new object[]
					{
						this._currentScope.TokenType
					}));
				}
			}
		}

		private void RaiseError(string message, JsonSchemaModel schema)
		{
			string message2 = (!((IJsonLineInfo)this).HasLineInfo()) ? message : (message + " Line {0}, position {1}.".FormatWith(CultureInfo.InvariantCulture, new object[]
			{
				((IJsonLineInfo)this).LineNumber,
				((IJsonLineInfo)this).LinePosition
			}));
			this.OnValidationEvent(new JsonSchemaException(message2, null, ((IJsonLineInfo)this).LineNumber, ((IJsonLineInfo)this).LinePosition));
		}

		private void OnValidationEvent(JsonSchemaException exception)
		{
			ValidationEventHandler validationEventHandler = this.ValidationEventHandler;
			if (validationEventHandler != null)
			{
				validationEventHandler(this, new ValidationEventArgs(exception));
				return;
			}
			throw exception;
		}

		public JsonSchema Schema
		{
			get
			{
				return this._schema;
			}
			set
			{
				if (this.TokenType != JsonToken.None)
				{
					throw new Exception("Cannot change schema while validating JSON.");
				}
				this._schema = value;
				this._model = null;
			}
		}

		public JsonReader Reader
		{
			get
			{
				return this._reader;
			}
		}

		private void ValidateInEnumAndNotDisallowed(JsonSchemaModel schema)
		{
			if (schema == null)
			{
				return;
			}
			JToken jtoken = new JValue(this._reader.Value);
			if (schema.Enum != null)
			{
				StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture);
				jtoken.WriteTo(new JsonTextWriter(stringWriter), new JsonConverter[0]);
				if (!schema.Enum.ContainsValue(jtoken, new JTokenEqualityComparer()))
				{
					this.RaiseError("Value {0} is not defined in enum.".FormatWith(CultureInfo.InvariantCulture, new object[]
					{
						stringWriter.ToString()
					}), schema);
				}
			}
			JsonSchemaType? currentNodeSchemaType = this.GetCurrentNodeSchemaType();
			if (currentNodeSchemaType != null && JsonSchemaGenerator.HasFlag(new JsonSchemaType?(schema.Disallow), currentNodeSchemaType.Value))
			{
				this.RaiseError("Type {0} is disallowed.".FormatWith(CultureInfo.InvariantCulture, new object[]
				{
					currentNodeSchemaType
				}), schema);
			}
		}

		private JsonSchemaType? GetCurrentNodeSchemaType()
		{
			switch (this._reader.TokenType)
			{
			case JsonToken.StartObject:
				return new JsonSchemaType?(JsonSchemaType.Object);
			case JsonToken.StartArray:
				return new JsonSchemaType?(JsonSchemaType.Array);
			case JsonToken.Integer:
				return new JsonSchemaType?(JsonSchemaType.Integer);
			case JsonToken.Float:
				return new JsonSchemaType?(JsonSchemaType.Float);
			case JsonToken.String:
				return new JsonSchemaType?(JsonSchemaType.String);
			case JsonToken.Boolean:
				return new JsonSchemaType?(JsonSchemaType.Boolean);
			case JsonToken.Null:
				return new JsonSchemaType?(JsonSchemaType.Null);
			}
			return null;
		}

		public override byte[] ReadAsBytes()
		{
			byte[] result = this._reader.ReadAsBytes();
			this.ValidateCurrentToken();
			return result;
		}

		public override decimal? ReadAsDecimal()
		{
			decimal? result = this._reader.ReadAsDecimal();
			this.ValidateCurrentToken();
			return result;
		}

		public override DateTimeOffset? ReadAsDateTimeOffset()
		{
			DateTimeOffset? result = this._reader.ReadAsDateTimeOffset();
			this.ValidateCurrentToken();
			return result;
		}

		public override bool Read()
		{
			if (!this._reader.Read())
			{
				return false;
			}
			if (this._reader.TokenType == JsonToken.Comment)
			{
				return true;
			}
			this.ValidateCurrentToken();
			return true;
		}

		private void ValidateCurrentToken()
		{
			if (this._model == null)
			{
				JsonSchemaModelBuilder jsonSchemaModelBuilder = new JsonSchemaModelBuilder();
				this._model = jsonSchemaModelBuilder.Build(this._schema);
			}
			switch (this._reader.TokenType)
			{
			case JsonToken.StartObject:
			{
				this.ProcessValue();
				IList<JsonSchemaModel> schemas = this.CurrentMemberSchemas.Where(new Func<JsonSchemaModel, bool>(this.ValidateObject)).ToList<JsonSchemaModel>();
				this.Push(new JsonValidatingReader.SchemaScope(JTokenType.Object, schemas));
				return;
			}
			case JsonToken.StartArray:
			{
				this.ProcessValue();
				IList<JsonSchemaModel> schemas2 = this.CurrentMemberSchemas.Where(new Func<JsonSchemaModel, bool>(this.ValidateArray)).ToList<JsonSchemaModel>();
				this.Push(new JsonValidatingReader.SchemaScope(JTokenType.Array, schemas2));
				return;
			}
			case JsonToken.StartConstructor:
				this.Push(new JsonValidatingReader.SchemaScope(JTokenType.Constructor, null));
				return;
			case JsonToken.PropertyName:
				foreach (JsonSchemaModel schema in this.CurrentSchemas)
				{
					this.ValidatePropertyName(schema);
				}
				return;
			case JsonToken.Raw:
				return;
			case JsonToken.Integer:
				this.ProcessValue();
				foreach (JsonSchemaModel schema2 in this.CurrentMemberSchemas)
				{
					this.ValidateInteger(schema2);
				}
				return;
			case JsonToken.Float:
				this.ProcessValue();
				foreach (JsonSchemaModel schema3 in this.CurrentMemberSchemas)
				{
					this.ValidateFloat(schema3);
				}
				return;
			case JsonToken.String:
				this.ProcessValue();
				foreach (JsonSchemaModel schema4 in this.CurrentMemberSchemas)
				{
					this.ValidateString(schema4);
				}
				return;
			case JsonToken.Boolean:
				this.ProcessValue();
				foreach (JsonSchemaModel schema5 in this.CurrentMemberSchemas)
				{
					this.ValidateBoolean(schema5);
				}
				return;
			case JsonToken.Null:
				this.ProcessValue();
				foreach (JsonSchemaModel schema6 in this.CurrentMemberSchemas)
				{
					this.ValidateNull(schema6);
				}
				return;
			case JsonToken.Undefined:
				return;
			case JsonToken.EndObject:
				foreach (JsonSchemaModel schema7 in this.CurrentSchemas)
				{
					this.ValidateEndObject(schema7);
				}
				this.Pop();
				return;
			case JsonToken.EndArray:
				foreach (JsonSchemaModel schema8 in this.CurrentSchemas)
				{
					this.ValidateEndArray(schema8);
				}
				this.Pop();
				return;
			case JsonToken.EndConstructor:
				this.Pop();
				return;
			case JsonToken.Date:
				return;
			}
			throw new ArgumentOutOfRangeException();
		}

		private void ValidateEndObject(JsonSchemaModel schema)
		{
			if (schema == null)
			{
				return;
			}
			Dictionary<string, bool> requiredProperties = this._currentScope.RequiredProperties;
			if (requiredProperties != null)
			{
				List<string> list = (from kv in requiredProperties
				where !kv.Value
				select kv.Key).ToList<string>();
				if (list.Count > 0)
				{
					this.RaiseError("Required properties are missing from object: {0}.".FormatWith(CultureInfo.InvariantCulture, new object[]
					{
						string.Join(", ", list.ToArray())
					}), schema);
				}
			}
		}

		private void ValidateEndArray(JsonSchemaModel schema)
		{
			if (schema == null)
			{
				return;
			}
			int arrayItemCount = this._currentScope.ArrayItemCount;
			if (schema.MaximumItems != null)
			{
				int? maximumItems = schema.MaximumItems;
				if (maximumItems != null && arrayItemCount > maximumItems.Value)
				{
					this.RaiseError("Array item count {0} exceeds maximum count of {1}.".FormatWith(CultureInfo.InvariantCulture, new object[]
					{
						arrayItemCount,
						schema.MaximumItems
					}), schema);
				}
			}
			if (schema.MinimumItems != null)
			{
				int? minimumItems = schema.MinimumItems;
				if (minimumItems != null && arrayItemCount < minimumItems.Value)
				{
					this.RaiseError("Array item count {0} is less than minimum count of {1}.".FormatWith(CultureInfo.InvariantCulture, new object[]
					{
						arrayItemCount,
						schema.MinimumItems
					}), schema);
				}
			}
		}

		private void ValidateNull(JsonSchemaModel schema)
		{
			if (schema == null)
			{
				return;
			}
			if (!this.TestType(schema, JsonSchemaType.Null))
			{
				return;
			}
			this.ValidateInEnumAndNotDisallowed(schema);
		}

		private void ValidateBoolean(JsonSchemaModel schema)
		{
			if (schema == null)
			{
				return;
			}
			if (!this.TestType(schema, JsonSchemaType.Boolean))
			{
				return;
			}
			this.ValidateInEnumAndNotDisallowed(schema);
		}

		private void ValidateString(JsonSchemaModel schema)
		{
			if (schema == null)
			{
				return;
			}
			if (!this.TestType(schema, JsonSchemaType.String))
			{
				return;
			}
			this.ValidateInEnumAndNotDisallowed(schema);
			string text = this._reader.Value.ToString();
			if (schema.MaximumLength != null)
			{
				int? maximumLength = schema.MaximumLength;
				if (maximumLength != null && text.Length > maximumLength.Value)
				{
					this.RaiseError("String '{0}' exceeds maximum length of {1}.".FormatWith(CultureInfo.InvariantCulture, new object[]
					{
						text,
						schema.MaximumLength
					}), schema);
				}
			}
			if (schema.MinimumLength != null)
			{
				int? minimumLength = schema.MinimumLength;
				if (minimumLength != null && text.Length < minimumLength.Value)
				{
					this.RaiseError("String '{0}' is less than minimum length of {1}.".FormatWith(CultureInfo.InvariantCulture, new object[]
					{
						text,
						schema.MinimumLength
					}), schema);
				}
			}
			if (schema.Patterns != null)
			{
				foreach (string text2 in schema.Patterns)
				{
					if (!Regex.IsMatch(text, text2))
					{
						this.RaiseError("String '{0}' does not match regex pattern '{1}'.".FormatWith(CultureInfo.InvariantCulture, new object[]
						{
							text,
							text2
						}), schema);
					}
				}
			}
		}

		private void ValidateInteger(JsonSchemaModel schema)
		{
			if (schema == null)
			{
				return;
			}
			if (!this.TestType(schema, JsonSchemaType.Integer))
			{
				return;
			}
			this.ValidateInEnumAndNotDisallowed(schema);
			long num = Convert.ToInt64(this._reader.Value, CultureInfo.InvariantCulture);
			if (schema.Maximum != null)
			{
				double? maximum = schema.Maximum;
				if (maximum != null && (double)num > maximum.Value)
				{
					this.RaiseError("Integer {0} exceeds maximum value of {1}.".FormatWith(CultureInfo.InvariantCulture, new object[]
					{
						num,
						schema.Maximum
					}), schema);
				}
				if (schema.ExclusiveMaximum && (double)num == schema.Maximum)
				{
					this.RaiseError("Integer {0} equals maximum value of {1} and exclusive maximum is true.".FormatWith(CultureInfo.InvariantCulture, new object[]
					{
						num,
						schema.Maximum
					}), schema);
				}
			}
			if (schema.Minimum != null)
			{
				double? minimum = schema.Minimum;
				if (minimum != null && (double)num < minimum.Value)
				{
					this.RaiseError("Integer {0} is less than minimum value of {1}.".FormatWith(CultureInfo.InvariantCulture, new object[]
					{
						num,
						schema.Minimum
					}), schema);
				}
				if (schema.ExclusiveMinimum && (double)num == schema.Minimum)
				{
					this.RaiseError("Integer {0} equals minimum value of {1} and exclusive minimum is true.".FormatWith(CultureInfo.InvariantCulture, new object[]
					{
						num,
						schema.Minimum
					}), schema);
				}
			}
			if (schema.DivisibleBy != null && !JsonValidatingReader.IsZero((double)num % schema.DivisibleBy.Value))
			{
				this.RaiseError("Integer {0} is not evenly divisible by {1}.".FormatWith(CultureInfo.InvariantCulture, new object[]
				{
					JsonConvert.ToString(num),
					schema.DivisibleBy
				}), schema);
			}
		}

		private void ProcessValue()
		{
			if (this._currentScope != null && this._currentScope.TokenType == JTokenType.Array)
			{
				this._currentScope.ArrayItemCount++;
				foreach (JsonSchemaModel jsonSchemaModel in this.CurrentSchemas)
				{
					if (jsonSchemaModel != null && jsonSchemaModel.Items != null && jsonSchemaModel.Items.Count > 1 && this._currentScope.ArrayItemCount >= jsonSchemaModel.Items.Count)
					{
						this.RaiseError("Index {0} has not been defined and the schema does not allow additional items.".FormatWith(CultureInfo.InvariantCulture, new object[]
						{
							this._currentScope.ArrayItemCount
						}), jsonSchemaModel);
					}
				}
			}
		}

		private void ValidateFloat(JsonSchemaModel schema)
		{
			if (schema == null)
			{
				return;
			}
			if (!this.TestType(schema, JsonSchemaType.Float))
			{
				return;
			}
			this.ValidateInEnumAndNotDisallowed(schema);
			double num = Convert.ToDouble(this._reader.Value, CultureInfo.InvariantCulture);
			if (schema.Maximum != null)
			{
				double? maximum = schema.Maximum;
				if (maximum != null && num > maximum.Value)
				{
					this.RaiseError("Float {0} exceeds maximum value of {1}.".FormatWith(CultureInfo.InvariantCulture, new object[]
					{
						JsonConvert.ToString(num),
						schema.Maximum
					}), schema);
				}
				if (schema.ExclusiveMaximum && num == schema.Maximum)
				{
					this.RaiseError("Float {0} equals maximum value of {1} and exclusive maximum is true.".FormatWith(CultureInfo.InvariantCulture, new object[]
					{
						JsonConvert.ToString(num),
						schema.Maximum
					}), schema);
				}
			}
			if (schema.Minimum != null)
			{
				double? minimum = schema.Minimum;
				if (minimum != null && num < minimum.Value)
				{
					this.RaiseError("Float {0} is less than minimum value of {1}.".FormatWith(CultureInfo.InvariantCulture, new object[]
					{
						JsonConvert.ToString(num),
						schema.Minimum
					}), schema);
				}
				if (schema.ExclusiveMinimum && num == schema.Minimum)
				{
					this.RaiseError("Float {0} equals minimum value of {1} and exclusive minimum is true.".FormatWith(CultureInfo.InvariantCulture, new object[]
					{
						JsonConvert.ToString(num),
						schema.Minimum
					}), schema);
				}
			}
			if (schema.DivisibleBy != null && !JsonValidatingReader.IsZero(num % schema.DivisibleBy.Value))
			{
				this.RaiseError("Float {0} is not evenly divisible by {1}.".FormatWith(CultureInfo.InvariantCulture, new object[]
				{
					JsonConvert.ToString(num),
					schema.DivisibleBy
				}), schema);
			}
		}

		private static bool IsZero(double value)
		{
			double num = 2.2204460492503131E-16;
			return Math.Abs(value) < 10.0 * num;
		}

		private void ValidatePropertyName(JsonSchemaModel schema)
		{
			if (schema == null)
			{
				return;
			}
			string text = Convert.ToString(this._reader.Value, CultureInfo.InvariantCulture);
			if (this._currentScope.RequiredProperties.ContainsKey(text))
			{
				this._currentScope.RequiredProperties[text] = true;
			}
			if (!schema.AllowAdditionalProperties && !this.IsPropertyDefinied(schema, text))
			{
				this.RaiseError("Property '{0}' has not been defined and the schema does not allow additional properties.".FormatWith(CultureInfo.InvariantCulture, new object[]
				{
					text
				}), schema);
			}
			this._currentScope.CurrentPropertyName = text;
		}

		private bool IsPropertyDefinied(JsonSchemaModel schema, string propertyName)
		{
			if (schema.Properties != null && schema.Properties.ContainsKey(propertyName))
			{
				return true;
			}
			if (schema.PatternProperties != null)
			{
				foreach (string pattern in schema.PatternProperties.Keys)
				{
					if (Regex.IsMatch(propertyName, pattern))
					{
						return true;
					}
				}
				return false;
			}
			return false;
		}

		private bool ValidateArray(JsonSchemaModel schema)
		{
			return schema == null || this.TestType(schema, JsonSchemaType.Array);
		}

		private bool ValidateObject(JsonSchemaModel schema)
		{
			return schema == null || this.TestType(schema, JsonSchemaType.Object);
		}

		private bool TestType(JsonSchemaModel currentSchema, JsonSchemaType currentType)
		{
			if (!JsonSchemaGenerator.HasFlag(new JsonSchemaType?(currentSchema.Type), currentType))
			{
				this.RaiseError("Invalid type. Expected {0} but got {1}.".FormatWith(CultureInfo.InvariantCulture, new object[]
				{
					currentSchema.Type,
					currentType
				}), currentSchema);
				return false;
			}
			return true;
		}

		private readonly JsonReader _reader;

		private readonly Stack<JsonValidatingReader.SchemaScope> _stack;

		private JsonSchema _schema;

		private JsonSchemaModel _model;

		private JsonValidatingReader.SchemaScope _currentScope;

		private class SchemaScope
		{
			public SchemaScope(JTokenType tokenType, IList<JsonSchemaModel> schemas)
			{
				this._tokenType = tokenType;
				this._schemas = schemas;
				this._requiredProperties = schemas.SelectMany(new Func<JsonSchemaModel, IEnumerable<string>>(this.GetRequiredProperties)).Distinct<string>().ToDictionary((string p) => p, (string p) => false);
			}

			public string CurrentPropertyName { get; set; }

			public int ArrayItemCount { get; set; }

			public IList<JsonSchemaModel> Schemas
			{
				get
				{
					return this._schemas;
				}
			}

			public Dictionary<string, bool> RequiredProperties
			{
				get
				{
					return this._requiredProperties;
				}
			}

			public JTokenType TokenType
			{
				get
				{
					return this._tokenType;
				}
			}

			private IEnumerable<string> GetRequiredProperties(JsonSchemaModel schema)
			{
				if (schema == null || schema.Properties == null)
				{
					return Enumerable.Empty<string>();
				}
				return from p in schema.Properties
				where p.Value.Required
				select p.Key;
			}

			private readonly JTokenType _tokenType;

			private readonly IList<JsonSchemaModel> _schemas;

			private readonly Dictionary<string, bool> _requiredProperties;
		}
	}
}
