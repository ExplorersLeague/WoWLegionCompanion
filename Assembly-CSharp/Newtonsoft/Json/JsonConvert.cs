using System;
using System.Globalization;
using System.IO;
using System.Text;
using System.Xml;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json
{
	public static class JsonConvert
	{
		public static string ToString(DateTime value)
		{
			string result;
			using (StringWriter stringWriter = StringUtils.CreateStringWriter(64))
			{
				JsonConvert.WriteDateTimeString(stringWriter, value, JsonConvert.GetUtcOffset(value), value.Kind);
				result = stringWriter.ToString();
			}
			return result;
		}

		public static string ToString(DateTimeOffset value)
		{
			string result;
			using (StringWriter stringWriter = StringUtils.CreateStringWriter(64))
			{
				JsonConvert.WriteDateTimeString(stringWriter, value.UtcDateTime, value.Offset, DateTimeKind.Local);
				result = stringWriter.ToString();
			}
			return result;
		}

		private static TimeSpan GetUtcOffset(DateTime dateTime)
		{
			return TimeZone.CurrentTimeZone.GetUtcOffset(dateTime);
		}

		internal static void WriteDateTimeString(TextWriter writer, DateTime value)
		{
			JsonConvert.WriteDateTimeString(writer, value, JsonConvert.GetUtcOffset(value), value.Kind);
		}

		internal static void WriteDateTimeString(TextWriter writer, DateTime value, TimeSpan offset, DateTimeKind kind)
		{
			long value2 = JsonConvert.ConvertDateTimeToJavaScriptTicks(value, offset);
			writer.Write("\"\\/Date(");
			writer.Write(value2);
			if (kind == DateTimeKind.Local || kind == DateTimeKind.Unspecified)
			{
				writer.Write((offset.Ticks < 0L) ? "-" : "+");
				int num = Math.Abs(offset.Hours);
				if (num < 10)
				{
					writer.Write(0);
				}
				writer.Write(num);
				int num2 = Math.Abs(offset.Minutes);
				if (num2 < 10)
				{
					writer.Write(0);
				}
				writer.Write(num2);
			}
			writer.Write(")\\/\"");
		}

		private static long ToUniversalTicks(DateTime dateTime)
		{
			if (dateTime.Kind == DateTimeKind.Utc)
			{
				return dateTime.Ticks;
			}
			return JsonConvert.ToUniversalTicks(dateTime, JsonConvert.GetUtcOffset(dateTime));
		}

		private static long ToUniversalTicks(DateTime dateTime, TimeSpan offset)
		{
			if (dateTime.Kind == DateTimeKind.Utc)
			{
				return dateTime.Ticks;
			}
			long num = dateTime.Ticks - offset.Ticks;
			if (num > 3155378975999999999L)
			{
				return 3155378975999999999L;
			}
			if (num < 0L)
			{
				return 0L;
			}
			return num;
		}

		internal static long ConvertDateTimeToJavaScriptTicks(DateTime dateTime, TimeSpan offset)
		{
			long universialTicks = JsonConvert.ToUniversalTicks(dateTime, offset);
			return JsonConvert.UniversialTicksToJavaScriptTicks(universialTicks);
		}

		internal static long ConvertDateTimeToJavaScriptTicks(DateTime dateTime)
		{
			return JsonConvert.ConvertDateTimeToJavaScriptTicks(dateTime, true);
		}

		internal static long ConvertDateTimeToJavaScriptTicks(DateTime dateTime, bool convertToUtc)
		{
			long universialTicks = (!convertToUtc) ? dateTime.Ticks : JsonConvert.ToUniversalTicks(dateTime);
			return JsonConvert.UniversialTicksToJavaScriptTicks(universialTicks);
		}

		private static long UniversialTicksToJavaScriptTicks(long universialTicks)
		{
			return (universialTicks - JsonConvert.InitialJavaScriptDateTicks) / 10000L;
		}

		internal static DateTime ConvertJavaScriptTicksToDateTime(long javaScriptTicks)
		{
			DateTime result = new DateTime(javaScriptTicks * 10000L + JsonConvert.InitialJavaScriptDateTicks, DateTimeKind.Utc);
			return result;
		}

		public static string ToString(bool value)
		{
			return (!value) ? JsonConvert.False : JsonConvert.True;
		}

		public static string ToString(char value)
		{
			return JsonConvert.ToString(char.ToString(value));
		}

		public static string ToString(Enum value)
		{
			return value.ToString("D");
		}

		public static string ToString(int value)
		{
			return value.ToString(null, CultureInfo.InvariantCulture);
		}

		public static string ToString(short value)
		{
			return value.ToString(null, CultureInfo.InvariantCulture);
		}

		public static string ToString(ushort value)
		{
			return value.ToString(null, CultureInfo.InvariantCulture);
		}

		public static string ToString(uint value)
		{
			return value.ToString(null, CultureInfo.InvariantCulture);
		}

		public static string ToString(long value)
		{
			return value.ToString(null, CultureInfo.InvariantCulture);
		}

		public static string ToString(ulong value)
		{
			return value.ToString(null, CultureInfo.InvariantCulture);
		}

		public static string ToString(float value)
		{
			return JsonConvert.EnsureDecimalPlace((double)value, value.ToString("R", CultureInfo.InvariantCulture));
		}

		public static string ToString(double value)
		{
			return JsonConvert.EnsureDecimalPlace(value, value.ToString("R", CultureInfo.InvariantCulture));
		}

		private static string EnsureDecimalPlace(double value, string text)
		{
			if (double.IsNaN(value) || double.IsInfinity(value) || text.IndexOf('.') != -1 || text.IndexOf('E') != -1)
			{
				return text;
			}
			return text + ".0";
		}

		private static string EnsureDecimalPlace(string text)
		{
			if (text.IndexOf('.') != -1)
			{
				return text;
			}
			return text + ".0";
		}

		public static string ToString(byte value)
		{
			return value.ToString(null, CultureInfo.InvariantCulture);
		}

		public static string ToString(sbyte value)
		{
			return value.ToString(null, CultureInfo.InvariantCulture);
		}

		public static string ToString(decimal value)
		{
			return JsonConvert.EnsureDecimalPlace(value.ToString(null, CultureInfo.InvariantCulture));
		}

		public static string ToString(Guid value)
		{
			return '"' + value.ToString("D", CultureInfo.InvariantCulture) + '"';
		}

		public static string ToString(TimeSpan value)
		{
			return '"' + value.ToString() + '"';
		}

		public static string ToString(Uri value)
		{
			return '"' + value.ToString() + '"';
		}

		public static string ToString(string value)
		{
			return JsonConvert.ToString(value, '"');
		}

		public static string ToString(string value, char delimter)
		{
			return JavaScriptUtils.ToEscapedJavaScriptString(value, delimter, true);
		}

		public static string ToString(object value)
		{
			if (value == null)
			{
				return JsonConvert.Null;
			}
			IConvertible convertible = value as IConvertible;
			if (convertible != null)
			{
				switch (convertible.GetTypeCode())
				{
				case TypeCode.DBNull:
					return JsonConvert.Null;
				case TypeCode.Boolean:
					return JsonConvert.ToString(convertible.ToBoolean(CultureInfo.InvariantCulture));
				case TypeCode.Char:
					return JsonConvert.ToString(convertible.ToChar(CultureInfo.InvariantCulture));
				case TypeCode.SByte:
					return JsonConvert.ToString(convertible.ToSByte(CultureInfo.InvariantCulture));
				case TypeCode.Byte:
					return JsonConvert.ToString(convertible.ToByte(CultureInfo.InvariantCulture));
				case TypeCode.Int16:
					return JsonConvert.ToString(convertible.ToInt16(CultureInfo.InvariantCulture));
				case TypeCode.UInt16:
					return JsonConvert.ToString(convertible.ToUInt16(CultureInfo.InvariantCulture));
				case TypeCode.Int32:
					return JsonConvert.ToString(convertible.ToInt32(CultureInfo.InvariantCulture));
				case TypeCode.UInt32:
					return JsonConvert.ToString(convertible.ToUInt32(CultureInfo.InvariantCulture));
				case TypeCode.Int64:
					return JsonConvert.ToString(convertible.ToInt64(CultureInfo.InvariantCulture));
				case TypeCode.UInt64:
					return JsonConvert.ToString(convertible.ToUInt64(CultureInfo.InvariantCulture));
				case TypeCode.Single:
					return JsonConvert.ToString(convertible.ToSingle(CultureInfo.InvariantCulture));
				case TypeCode.Double:
					return JsonConvert.ToString(convertible.ToDouble(CultureInfo.InvariantCulture));
				case TypeCode.Decimal:
					return JsonConvert.ToString(convertible.ToDecimal(CultureInfo.InvariantCulture));
				case TypeCode.DateTime:
					return JsonConvert.ToString(convertible.ToDateTime(CultureInfo.InvariantCulture));
				case TypeCode.String:
					return JsonConvert.ToString(convertible.ToString(CultureInfo.InvariantCulture));
				}
			}
			else
			{
				if (value is DateTimeOffset)
				{
					return JsonConvert.ToString((DateTimeOffset)value);
				}
				if (value is Guid)
				{
					return JsonConvert.ToString((Guid)value);
				}
				if (value is Uri)
				{
					return JsonConvert.ToString((Uri)value);
				}
				if (value is TimeSpan)
				{
					return JsonConvert.ToString((TimeSpan)value);
				}
			}
			throw new ArgumentException("Unsupported type: {0}. Use the JsonSerializer class to get the object's JSON representation.".FormatWith(CultureInfo.InvariantCulture, new object[]
			{
				value.GetType()
			}));
		}

		private static bool IsJsonPrimitiveTypeCode(TypeCode typeCode)
		{
			switch (typeCode)
			{
			case TypeCode.DBNull:
			case TypeCode.Boolean:
			case TypeCode.Char:
			case TypeCode.SByte:
			case TypeCode.Byte:
			case TypeCode.Int16:
			case TypeCode.UInt16:
			case TypeCode.Int32:
			case TypeCode.UInt32:
			case TypeCode.Int64:
			case TypeCode.UInt64:
			case TypeCode.Single:
			case TypeCode.Double:
			case TypeCode.Decimal:
			case TypeCode.DateTime:
			case TypeCode.String:
				return true;
			}
			return false;
		}

		internal static bool IsJsonPrimitiveType(Type type)
		{
			if (ReflectionUtils.IsNullableType(type))
			{
				type = Nullable.GetUnderlyingType(type);
			}
			return type == typeof(DateTimeOffset) || type == typeof(byte[]) || type == typeof(Uri) || type == typeof(TimeSpan) || type == typeof(Guid) || JsonConvert.IsJsonPrimitiveTypeCode(Type.GetTypeCode(type));
		}

		internal static bool IsJsonPrimitive(object value)
		{
			if (value == null)
			{
				return true;
			}
			IConvertible convertible = value as IConvertible;
			if (convertible != null)
			{
				return JsonConvert.IsJsonPrimitiveTypeCode(convertible.GetTypeCode());
			}
			return value is DateTimeOffset || value is byte[] || value is Uri || value is TimeSpan || value is Guid;
		}

		public static string SerializeObject(object value)
		{
			return JsonConvert.SerializeObject(value, Formatting.None, null);
		}

		public static string SerializeObject(object value, Formatting formatting)
		{
			return JsonConvert.SerializeObject(value, formatting, null);
		}

		public static string SerializeObject(object value, params JsonConverter[] converters)
		{
			return JsonConvert.SerializeObject(value, Formatting.None, converters);
		}

		public static string SerializeObject(object value, Formatting formatting, params JsonConverter[] converters)
		{
			JsonSerializerSettings settings = (converters == null || converters.Length <= 0) ? null : new JsonSerializerSettings
			{
				Converters = converters
			};
			return JsonConvert.SerializeObject(value, formatting, settings);
		}

		public static string SerializeObject(object value, Formatting formatting, JsonSerializerSettings settings)
		{
			JsonSerializer jsonSerializer = JsonSerializer.Create(settings);
			StringBuilder sb = new StringBuilder(128);
			StringWriter stringWriter = new StringWriter(sb, CultureInfo.InvariantCulture);
			using (JsonTextWriter jsonTextWriter = new JsonTextWriter(stringWriter))
			{
				jsonTextWriter.Formatting = formatting;
				jsonSerializer.Serialize(jsonTextWriter, value);
			}
			return stringWriter.ToString();
		}

		public static object DeserializeObject(string value)
		{
			return JsonConvert.DeserializeObject(value, null, null);
		}

		public static object DeserializeObject(string value, JsonSerializerSettings settings)
		{
			return JsonConvert.DeserializeObject(value, null, settings);
		}

		public static object DeserializeObject(string value, Type type)
		{
			return JsonConvert.DeserializeObject(value, type, null);
		}

		public static T DeserializeObject<T>(string value)
		{
			return JsonConvert.DeserializeObject<T>(value, null);
		}

		public static T DeserializeAnonymousType<T>(string value, T anonymousTypeObject)
		{
			return JsonConvert.DeserializeObject<T>(value);
		}

		public static T DeserializeObject<T>(string value, params JsonConverter[] converters)
		{
			return (T)((object)JsonConvert.DeserializeObject(value, typeof(T), converters));
		}

		public static T DeserializeObject<T>(string value, JsonSerializerSettings settings)
		{
			return (T)((object)JsonConvert.DeserializeObject(value, typeof(T), settings));
		}

		public static object DeserializeObject(string value, Type type, params JsonConverter[] converters)
		{
			JsonSerializerSettings settings = (converters == null || converters.Length <= 0) ? null : new JsonSerializerSettings
			{
				Converters = converters
			};
			return JsonConvert.DeserializeObject(value, type, settings);
		}

		public static object DeserializeObject(string value, Type type, JsonSerializerSettings settings)
		{
			StringReader reader = new StringReader(value);
			JsonSerializer jsonSerializer = JsonSerializer.Create(settings);
			object result;
			using (JsonReader jsonReader = new JsonTextReader(reader))
			{
				result = jsonSerializer.Deserialize(jsonReader, type);
				if (jsonReader.Read() && jsonReader.TokenType != JsonToken.Comment)
				{
					throw new JsonSerializationException("Additional text found in JSON string after finishing deserializing object.");
				}
			}
			return result;
		}

		public static void PopulateObject(string value, object target)
		{
			JsonConvert.PopulateObject(value, target, null);
		}

		public static void PopulateObject(string value, object target, JsonSerializerSettings settings)
		{
			StringReader reader = new StringReader(value);
			JsonSerializer jsonSerializer = JsonSerializer.Create(settings);
			using (JsonReader jsonReader = new JsonTextReader(reader))
			{
				jsonSerializer.Populate(jsonReader, target);
				if (jsonReader.Read() && jsonReader.TokenType != JsonToken.Comment)
				{
					throw new JsonSerializationException("Additional text found in JSON string after finishing deserializing object.");
				}
			}
		}

		public static string SerializeXmlNode(XmlNode node)
		{
			return JsonConvert.SerializeXmlNode(node, Formatting.None);
		}

		public static string SerializeXmlNode(XmlNode node, Formatting formatting)
		{
			XmlNodeConverter xmlNodeConverter = new XmlNodeConverter();
			return JsonConvert.SerializeObject(node, formatting, new JsonConverter[]
			{
				xmlNodeConverter
			});
		}

		public static string SerializeXmlNode(XmlNode node, Formatting formatting, bool omitRootObject)
		{
			XmlNodeConverter xmlNodeConverter = new XmlNodeConverter
			{
				OmitRootObject = omitRootObject
			};
			return JsonConvert.SerializeObject(node, formatting, new JsonConverter[]
			{
				xmlNodeConverter
			});
		}

		public static XmlDocument DeserializeXmlNode(string value)
		{
			return JsonConvert.DeserializeXmlNode(value, null);
		}

		public static XmlDocument DeserializeXmlNode(string value, string deserializeRootElementName)
		{
			return JsonConvert.DeserializeXmlNode(value, deserializeRootElementName, false);
		}

		public static XmlDocument DeserializeXmlNode(string value, string deserializeRootElementName, bool writeArrayAttribute)
		{
			XmlNodeConverter xmlNodeConverter = new XmlNodeConverter
			{
				DeserializeRootElementName = deserializeRootElementName,
				WriteArrayAttribute = writeArrayAttribute
			};
			return (XmlDocument)JsonConvert.DeserializeObject(value, typeof(XmlDocument), new JsonConverter[]
			{
				xmlNodeConverter
			});
		}

		public static readonly string True = "true";

		public static readonly string False = "false";

		public static readonly string Null = "null";

		public static readonly string Undefined = "undefined";

		public static readonly string PositiveInfinity = "Infinity";

		public static readonly string NegativeInfinity = "-Infinity";

		public static readonly string NaN = "NaN";

		internal static readonly long InitialJavaScriptDateTicks = 621355968000000000L;
	}
}
