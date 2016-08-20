using System;
using Newtonsoft.Json;

namespace JamLib
{
	public class JamEmbeddedMessageConverter : JsonConverter
	{
		public override bool CanConvert(Type objectType)
		{
			return objectType == typeof(JamEmbeddedMessage);
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			if (reader.TokenType != JsonToken.String)
			{
				throw new JsonSerializationException(string.Format("Unexpected token parsing binary. Expected String or StartArray, got {0}.", reader.TokenType));
			}
			string message = reader.Value.ToString();
			JamEmbeddedMessage jamEmbeddedMessage = (JamEmbeddedMessage)existingValue;
			if (jamEmbeddedMessage == null)
			{
				jamEmbeddedMessage = new JamEmbeddedMessage();
			}
			jamEmbeddedMessage.Message = MessageFactory.Deserialize(message);
			return jamEmbeddedMessage;
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			string value2 = MessageFactory.Serialize(((JamEmbeddedMessage)value).Message);
			writer.WriteValue(value2);
		}
	}
}
