using System;
using System.Globalization;
using System.Text.RegularExpressions;
using Newtonsoft.Json.Bson;

namespace Newtonsoft.Json.Converters
{
	public class RegexConverter : JsonConverter
	{
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			Regex regex = (Regex)value;
			BsonWriter bsonWriter = writer as BsonWriter;
			if (bsonWriter != null)
			{
				this.WriteBson(bsonWriter, regex);
			}
			else
			{
				this.WriteJson(writer, regex);
			}
		}

		private bool HasFlag(RegexOptions options, RegexOptions flag)
		{
			return (options & flag) == flag;
		}

		private void WriteBson(BsonWriter writer, Regex regex)
		{
			string text = null;
			if (this.HasFlag(regex.Options, RegexOptions.IgnoreCase))
			{
				text += "i";
			}
			if (this.HasFlag(regex.Options, RegexOptions.Multiline))
			{
				text += "m";
			}
			if (this.HasFlag(regex.Options, RegexOptions.Singleline))
			{
				text += "s";
			}
			text += "u";
			if (this.HasFlag(regex.Options, RegexOptions.ExplicitCapture))
			{
				text += "x";
			}
			writer.WriteRegex(regex.ToString(), text);
		}

		private void WriteJson(JsonWriter writer, Regex regex)
		{
			writer.WriteStartObject();
			writer.WritePropertyName("Pattern");
			writer.WriteValue(regex.ToString());
			writer.WritePropertyName("Options");
			writer.WriteValue(regex.Options);
			writer.WriteEndObject();
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			BsonReader bsonReader = reader as BsonReader;
			if (bsonReader != null)
			{
				return this.ReadBson(bsonReader);
			}
			return this.ReadJson(reader);
		}

		private object ReadBson(BsonReader reader)
		{
			string text = (string)reader.Value;
			int num = text.LastIndexOf("/");
			string pattern = text.Substring(1, num - 1);
			string text2 = text.Substring(num + 1);
			RegexOptions regexOptions = RegexOptions.None;
			foreach (char c in text2)
			{
				char c2 = c;
				if (c2 != 'i')
				{
					if (c2 != 'm')
					{
						if (c2 != 's')
						{
							if (c2 == 'x')
							{
								regexOptions |= RegexOptions.ExplicitCapture;
							}
						}
						else
						{
							regexOptions |= RegexOptions.Singleline;
						}
					}
					else
					{
						regexOptions |= RegexOptions.Multiline;
					}
				}
				else
				{
					regexOptions |= RegexOptions.IgnoreCase;
				}
			}
			return new Regex(pattern, regexOptions);
		}

		private Regex ReadJson(JsonReader reader)
		{
			reader.Read();
			reader.Read();
			string pattern = (string)reader.Value;
			reader.Read();
			reader.Read();
			int options = Convert.ToInt32(reader.Value, CultureInfo.InvariantCulture);
			reader.Read();
			return new Regex(pattern, (RegexOptions)options);
		}

		public override bool CanConvert(Type objectType)
		{
			return objectType == typeof(Regex);
		}
	}
}
