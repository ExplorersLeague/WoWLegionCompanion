using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace JamLib
{
	public class ByteArrayConverter : JsonConverter
	{
		public override bool CanConvert(Type objectType)
		{
			return objectType == typeof(byte[]);
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			if (reader.TokenType == JsonToken.StartArray)
			{
				List<byte> list = new List<byte>();
				while (reader.Read())
				{
					JsonToken tokenType = reader.TokenType;
					switch (tokenType)
					{
					case JsonToken.Comment:
						break;
					default:
						if (tokenType != JsonToken.EndArray)
						{
							throw new Exception(string.Format("Unexpected token when reading bytes: {0}", reader.TokenType));
						}
						return list.ToArray();
					case JsonToken.Integer:
						list.Add(Convert.ToByte(reader.Value));
						break;
					case JsonToken.String:
						list.Add(Convert.ToByte(reader.Value));
						break;
					}
				}
				throw new Exception("Unexpected end when reading bytes.");
			}
			throw new Exception(string.Format("Unexpected token parsing binary. Expected StartArray, got {0}.", reader.TokenType));
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			if (value == null)
			{
				writer.WriteNull();
				return;
			}
			byte[] array = (byte[])value;
			writer.WriteStartArray();
			for (int i = 0; i < array.Length; i++)
			{
				writer.WriteValue(array[i]);
			}
			writer.WriteEndArray();
		}
	}
}
