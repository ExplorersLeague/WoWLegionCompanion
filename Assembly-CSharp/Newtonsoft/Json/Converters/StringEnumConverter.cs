using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Converters
{
	public class StringEnumConverter : JsonConverter
	{
		public bool CamelCaseText { get; set; }

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			if (value == null)
			{
				writer.WriteNull();
				return;
			}
			Enum @enum = (Enum)value;
			string text = @enum.ToString("G");
			if (char.IsNumber(text[0]) || text[0] == '-')
			{
				writer.WriteValue(value);
			}
			else
			{
				BidirectionalDictionary<string, string> enumNameMap = this.GetEnumNameMap(@enum.GetType());
				string text2;
				enumNameMap.TryGetByFirst(text, out text2);
				text2 = (text2 ?? text);
				if (this.CamelCaseText)
				{
					text2 = StringUtils.ToCamelCase(text2);
				}
				writer.WriteValue(text2);
			}
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			Type type = (!ReflectionUtils.IsNullableType(objectType)) ? objectType : Nullable.GetUnderlyingType(objectType);
			if (reader.TokenType == JsonToken.Null)
			{
				if (!ReflectionUtils.IsNullableType(objectType))
				{
					throw new Exception("Cannot convert null value to {0}.".FormatWith(CultureInfo.InvariantCulture, new object[]
					{
						objectType
					}));
				}
				return null;
			}
			else
			{
				if (reader.TokenType == JsonToken.String)
				{
					BidirectionalDictionary<string, string> enumNameMap = this.GetEnumNameMap(type);
					string text;
					enumNameMap.TryGetBySecond(reader.Value.ToString(), out text);
					text = (text ?? reader.Value.ToString());
					return Enum.Parse(type, text, true);
				}
				if (reader.TokenType == JsonToken.Integer)
				{
					return ConvertUtils.ConvertOrCast(reader.Value, CultureInfo.InvariantCulture, type);
				}
				throw new Exception("Unexpected token when parsing enum. Expected String or Integer, got {0}.".FormatWith(CultureInfo.InvariantCulture, new object[]
				{
					reader.TokenType
				}));
			}
		}

		private BidirectionalDictionary<string, string> GetEnumNameMap(Type t)
		{
			BidirectionalDictionary<string, string> bidirectionalDictionary;
			if (!this._enumMemberNamesPerType.TryGetValue(t, out bidirectionalDictionary))
			{
				object enumMemberNamesPerType = this._enumMemberNamesPerType;
				lock (enumMemberNamesPerType)
				{
					if (this._enumMemberNamesPerType.TryGetValue(t, out bidirectionalDictionary))
					{
						return bidirectionalDictionary;
					}
					bidirectionalDictionary = new BidirectionalDictionary<string, string>(StringComparer.OrdinalIgnoreCase, StringComparer.OrdinalIgnoreCase);
					foreach (FieldInfo fieldInfo in t.GetFields())
					{
						string name = fieldInfo.Name;
						string text = (from System.Runtime.Serialization.EnumMemberAttribute a in fieldInfo.GetCustomAttributes(typeof(System.Runtime.Serialization.EnumMemberAttribute), true)
						select a.Value).SingleOrDefault<string>() ?? fieldInfo.Name;
						string text2;
						if (bidirectionalDictionary.TryGetBySecond(text, out text2))
						{
							throw new Exception("Enum name '{0}' already exists on enum '{1}'.".FormatWith(CultureInfo.InvariantCulture, new object[]
							{
								text,
								t.Name
							}));
						}
						bidirectionalDictionary.Add(name, text);
					}
					this._enumMemberNamesPerType[t] = bidirectionalDictionary;
				}
				return bidirectionalDictionary;
			}
			return bidirectionalDictionary;
		}

		public override bool CanConvert(Type objectType)
		{
			Type type = (!ReflectionUtils.IsNullableType(objectType)) ? objectType : Nullable.GetUnderlyingType(objectType);
			return type.IsEnum;
		}

		private readonly Dictionary<Type, BidirectionalDictionary<string, string>> _enumMemberNamesPerType = new Dictionary<Type, BidirectionalDictionary<string, string>>();
	}
}
