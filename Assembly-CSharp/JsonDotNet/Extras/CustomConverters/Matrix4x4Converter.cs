using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace JsonDotNet.Extras.CustomConverters
{
	public class Matrix4x4Converter : JsonConverter
	{
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			JToken jtoken = JToken.FromObject(value);
			if (jtoken.Type != JTokenType.Object)
			{
				jtoken.WriteTo(writer, new JsonConverter[0]);
			}
			else
			{
				JObject jobject = (JObject)jtoken;
				IList<string> content = (from p in jobject.Properties()
				where p.Name != "inverse" && p.Name != "transpose"
				select p.Name).ToList<string>();
				jobject.AddFirst(new JProperty("Keys", new JArray(content)));
				jobject.WriteTo(writer, new JsonConverter[0]);
			}
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			throw new NotImplementedException("Unnecessary because CanRead is false. The type will skip the converter.");
		}

		public override bool CanRead
		{
			get
			{
				return false;
			}
		}

		public override bool CanConvert(Type objectType)
		{
			return objectType == typeof(Matrix4x4);
		}
	}
}
