using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Schema
{
	internal class JsonSchemaBuilder
	{
		public JsonSchemaBuilder(JsonSchemaResolver resolver)
		{
			this._stack = new List<JsonSchema>();
			this._resolver = resolver;
		}

		private void Push(JsonSchema value)
		{
			this._currentSchema = value;
			this._stack.Add(value);
			this._resolver.LoadedSchemas.Add(value);
		}

		private JsonSchema Pop()
		{
			JsonSchema currentSchema = this._currentSchema;
			this._stack.RemoveAt(this._stack.Count - 1);
			this._currentSchema = this._stack.LastOrDefault<JsonSchema>();
			return currentSchema;
		}

		private JsonSchema CurrentSchema
		{
			get
			{
				return this._currentSchema;
			}
		}

		internal JsonSchema Parse(JsonReader reader)
		{
			this._reader = reader;
			if (reader.TokenType == JsonToken.None)
			{
				this._reader.Read();
			}
			return this.BuildSchema();
		}

		private JsonSchema BuildSchema()
		{
			if (this._reader.TokenType != JsonToken.StartObject)
			{
				throw new Exception("Expected StartObject while parsing schema object, got {0}.".FormatWith(CultureInfo.InvariantCulture, new object[]
				{
					this._reader.TokenType
				}));
			}
			this._reader.Read();
			if (this._reader.TokenType == JsonToken.EndObject)
			{
				this.Push(new JsonSchema());
				return this.Pop();
			}
			string text = Convert.ToString(this._reader.Value, CultureInfo.InvariantCulture);
			this._reader.Read();
			if (!(text == "$ref"))
			{
				this.Push(new JsonSchema());
				this.ProcessSchemaProperty(text);
				while (this._reader.Read() && this._reader.TokenType != JsonToken.EndObject)
				{
					text = Convert.ToString(this._reader.Value, CultureInfo.InvariantCulture);
					this._reader.Read();
					this.ProcessSchemaProperty(text);
				}
				return this.Pop();
			}
			string text2 = (string)this._reader.Value;
			while (this._reader.Read() && this._reader.TokenType != JsonToken.EndObject)
			{
				if (this._reader.TokenType == JsonToken.StartObject)
				{
					throw new Exception("Found StartObject within the schema reference with the Id '{0}'".FormatWith(CultureInfo.InvariantCulture, new object[]
					{
						text2
					}));
				}
			}
			JsonSchema schema = this._resolver.GetSchema(text2);
			if (schema == null)
			{
				throw new Exception("Could not resolve schema reference for Id '{0}'.".FormatWith(CultureInfo.InvariantCulture, new object[]
				{
					text2
				}));
			}
			return schema;
		}

		private void ProcessSchemaProperty(string propertyName)
		{
			switch (propertyName)
			{
			case "type":
				this.CurrentSchema.Type = this.ProcessType();
				return;
			case "id":
				this.CurrentSchema.Id = (string)this._reader.Value;
				return;
			case "title":
				this.CurrentSchema.Title = (string)this._reader.Value;
				return;
			case "description":
				this.CurrentSchema.Description = (string)this._reader.Value;
				return;
			case "properties":
				this.ProcessProperties();
				return;
			case "items":
				this.ProcessItems();
				return;
			case "additionalProperties":
				this.ProcessAdditionalProperties();
				return;
			case "patternProperties":
				this.ProcessPatternProperties();
				return;
			case "required":
				this.CurrentSchema.Required = new bool?((bool)this._reader.Value);
				return;
			case "requires":
				this.CurrentSchema.Requires = (string)this._reader.Value;
				return;
			case "identity":
				this.ProcessIdentity();
				return;
			case "minimum":
				this.CurrentSchema.Minimum = new double?(Convert.ToDouble(this._reader.Value, CultureInfo.InvariantCulture));
				return;
			case "maximum":
				this.CurrentSchema.Maximum = new double?(Convert.ToDouble(this._reader.Value, CultureInfo.InvariantCulture));
				return;
			case "exclusiveMinimum":
				this.CurrentSchema.ExclusiveMinimum = new bool?((bool)this._reader.Value);
				return;
			case "exclusiveMaximum":
				this.CurrentSchema.ExclusiveMaximum = new bool?((bool)this._reader.Value);
				return;
			case "maxLength":
				this.CurrentSchema.MaximumLength = new int?(Convert.ToInt32(this._reader.Value, CultureInfo.InvariantCulture));
				return;
			case "minLength":
				this.CurrentSchema.MinimumLength = new int?(Convert.ToInt32(this._reader.Value, CultureInfo.InvariantCulture));
				return;
			case "maxItems":
				this.CurrentSchema.MaximumItems = new int?(Convert.ToInt32(this._reader.Value, CultureInfo.InvariantCulture));
				return;
			case "minItems":
				this.CurrentSchema.MinimumItems = new int?(Convert.ToInt32(this._reader.Value, CultureInfo.InvariantCulture));
				return;
			case "divisibleBy":
				this.CurrentSchema.DivisibleBy = new double?(Convert.ToDouble(this._reader.Value, CultureInfo.InvariantCulture));
				return;
			case "disallow":
				this.CurrentSchema.Disallow = this.ProcessType();
				return;
			case "default":
				this.ProcessDefault();
				return;
			case "hidden":
				this.CurrentSchema.Hidden = new bool?((bool)this._reader.Value);
				return;
			case "readonly":
				this.CurrentSchema.ReadOnly = new bool?((bool)this._reader.Value);
				return;
			case "format":
				this.CurrentSchema.Format = (string)this._reader.Value;
				return;
			case "pattern":
				this.CurrentSchema.Pattern = (string)this._reader.Value;
				return;
			case "options":
				this.ProcessOptions();
				return;
			case "enum":
				this.ProcessEnum();
				return;
			case "extends":
				this.ProcessExtends();
				return;
			}
			this._reader.Skip();
		}

		private void ProcessExtends()
		{
			this.CurrentSchema.Extends = this.BuildSchema();
		}

		private void ProcessEnum()
		{
			if (this._reader.TokenType != JsonToken.StartArray)
			{
				throw new Exception("Expected StartArray token while parsing enum values, got {0}.".FormatWith(CultureInfo.InvariantCulture, new object[]
				{
					this._reader.TokenType
				}));
			}
			this.CurrentSchema.Enum = new List<JToken>();
			while (this._reader.Read() && this._reader.TokenType != JsonToken.EndArray)
			{
				JToken item = JToken.ReadFrom(this._reader);
				this.CurrentSchema.Enum.Add(item);
			}
		}

		private void ProcessOptions()
		{
			this.CurrentSchema.Options = new Dictionary<JToken, string>(new JTokenEqualityComparer());
			JsonToken tokenType = this._reader.TokenType;
			if (tokenType != JsonToken.StartArray)
			{
				throw new Exception("Expected array token, got {0}.".FormatWith(CultureInfo.InvariantCulture, new object[]
				{
					this._reader.TokenType
				}));
			}
			while (this._reader.Read() && this._reader.TokenType != JsonToken.EndArray)
			{
				if (this._reader.TokenType != JsonToken.StartObject)
				{
					throw new Exception("Expect object token, got {0}.".FormatWith(CultureInfo.InvariantCulture, new object[]
					{
						this._reader.TokenType
					}));
				}
				string value = null;
				JToken jtoken = null;
				while (this._reader.Read() && this._reader.TokenType != JsonToken.EndObject)
				{
					string text = Convert.ToString(this._reader.Value, CultureInfo.InvariantCulture);
					this._reader.Read();
					if (text != null)
					{
						if (text == "value")
						{
							jtoken = JToken.ReadFrom(this._reader);
							continue;
						}
						if (text == "label")
						{
							value = (string)this._reader.Value;
							continue;
						}
					}
					throw new Exception("Unexpected property in JSON schema option: {0}.".FormatWith(CultureInfo.InvariantCulture, new object[]
					{
						text
					}));
				}
				if (jtoken == null)
				{
					throw new Exception("No value specified for JSON schema option.");
				}
				if (this.CurrentSchema.Options.ContainsKey(jtoken))
				{
					throw new Exception("Duplicate value in JSON schema option collection: {0}".FormatWith(CultureInfo.InvariantCulture, new object[]
					{
						jtoken
					}));
				}
				this.CurrentSchema.Options.Add(jtoken, value);
			}
		}

		private void ProcessDefault()
		{
			this.CurrentSchema.Default = JToken.ReadFrom(this._reader);
		}

		private void ProcessIdentity()
		{
			this.CurrentSchema.Identity = new List<string>();
			JsonToken tokenType = this._reader.TokenType;
			if (tokenType != JsonToken.String)
			{
				if (tokenType != JsonToken.StartArray)
				{
					throw new Exception("Expected array or JSON property name string token, got {0}.".FormatWith(CultureInfo.InvariantCulture, new object[]
					{
						this._reader.TokenType
					}));
				}
				while (this._reader.Read() && this._reader.TokenType != JsonToken.EndArray)
				{
					if (this._reader.TokenType != JsonToken.String)
					{
						throw new Exception("Exception JSON property name string token, got {0}.".FormatWith(CultureInfo.InvariantCulture, new object[]
						{
							this._reader.TokenType
						}));
					}
					this.CurrentSchema.Identity.Add(this._reader.Value.ToString());
				}
			}
			else
			{
				this.CurrentSchema.Identity.Add(this._reader.Value.ToString());
			}
		}

		private void ProcessAdditionalProperties()
		{
			if (this._reader.TokenType == JsonToken.Boolean)
			{
				this.CurrentSchema.AllowAdditionalProperties = (bool)this._reader.Value;
			}
			else
			{
				this.CurrentSchema.AdditionalProperties = this.BuildSchema();
			}
		}

		private void ProcessPatternProperties()
		{
			Dictionary<string, JsonSchema> dictionary = new Dictionary<string, JsonSchema>();
			if (this._reader.TokenType != JsonToken.StartObject)
			{
				throw new Exception("Expected start object token.");
			}
			while (this._reader.Read() && this._reader.TokenType != JsonToken.EndObject)
			{
				string text = Convert.ToString(this._reader.Value, CultureInfo.InvariantCulture);
				this._reader.Read();
				if (dictionary.ContainsKey(text))
				{
					throw new Exception("Property {0} has already been defined in schema.".FormatWith(CultureInfo.InvariantCulture, new object[]
					{
						text
					}));
				}
				dictionary.Add(text, this.BuildSchema());
			}
			this.CurrentSchema.PatternProperties = dictionary;
		}

		private void ProcessItems()
		{
			this.CurrentSchema.Items = new List<JsonSchema>();
			JsonToken tokenType = this._reader.TokenType;
			if (tokenType != JsonToken.StartObject)
			{
				if (tokenType != JsonToken.StartArray)
				{
					throw new Exception("Expected array or JSON schema object token, got {0}.".FormatWith(CultureInfo.InvariantCulture, new object[]
					{
						this._reader.TokenType
					}));
				}
				while (this._reader.Read() && this._reader.TokenType != JsonToken.EndArray)
				{
					this.CurrentSchema.Items.Add(this.BuildSchema());
				}
			}
			else
			{
				this.CurrentSchema.Items.Add(this.BuildSchema());
			}
		}

		private void ProcessProperties()
		{
			IDictionary<string, JsonSchema> dictionary = new Dictionary<string, JsonSchema>();
			if (this._reader.TokenType != JsonToken.StartObject)
			{
				throw new Exception("Expected StartObject token while parsing schema properties, got {0}.".FormatWith(CultureInfo.InvariantCulture, new object[]
				{
					this._reader.TokenType
				}));
			}
			while (this._reader.Read() && this._reader.TokenType != JsonToken.EndObject)
			{
				string text = Convert.ToString(this._reader.Value, CultureInfo.InvariantCulture);
				this._reader.Read();
				if (dictionary.ContainsKey(text))
				{
					throw new Exception("Property {0} has already been defined in schema.".FormatWith(CultureInfo.InvariantCulture, new object[]
					{
						text
					}));
				}
				dictionary.Add(text, this.BuildSchema());
			}
			this.CurrentSchema.Properties = dictionary;
		}

		private JsonSchemaType? ProcessType()
		{
			JsonToken tokenType = this._reader.TokenType;
			if (tokenType == JsonToken.String)
			{
				return new JsonSchemaType?(JsonSchemaBuilder.MapType(this._reader.Value.ToString()));
			}
			if (tokenType != JsonToken.StartArray)
			{
				throw new Exception("Expected array or JSON schema type string token, got {0}.".FormatWith(CultureInfo.InvariantCulture, new object[]
				{
					this._reader.TokenType
				}));
			}
			JsonSchemaType? result = new JsonSchemaType?(JsonSchemaType.None);
			while (this._reader.Read() && this._reader.TokenType != JsonToken.EndArray)
			{
				if (this._reader.TokenType != JsonToken.String)
				{
					throw new Exception("Exception JSON schema type string token, got {0}.".FormatWith(CultureInfo.InvariantCulture, new object[]
					{
						this._reader.TokenType
					}));
				}
				result = ((result == null) ? null : new JsonSchemaType?(result.GetValueOrDefault() | JsonSchemaBuilder.MapType(this._reader.Value.ToString())));
			}
			return result;
		}

		internal static JsonSchemaType MapType(string type)
		{
			JsonSchemaType result;
			if (!JsonSchemaConstants.JsonSchemaTypeMapping.TryGetValue(type, out result))
			{
				throw new Exception("Invalid JSON schema type: {0}".FormatWith(CultureInfo.InvariantCulture, new object[]
				{
					type
				}));
			}
			return result;
		}

		internal static string MapType(JsonSchemaType type)
		{
			return JsonSchemaConstants.JsonSchemaTypeMapping.Single((KeyValuePair<string, JsonSchemaType> kv) => kv.Value == type).Key;
		}

		private JsonReader _reader;

		private readonly IList<JsonSchema> _stack;

		private readonly JsonSchemaResolver _resolver;

		private JsonSchema _currentSchema;
	}
}
