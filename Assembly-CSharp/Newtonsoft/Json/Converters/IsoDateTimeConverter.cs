using System;
using System.Globalization;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Converters
{
	public class IsoDateTimeConverter : DateTimeConverterBase
	{
		public DateTimeStyles DateTimeStyles
		{
			get
			{
				return this._dateTimeStyles;
			}
			set
			{
				this._dateTimeStyles = value;
			}
		}

		public string DateTimeFormat
		{
			get
			{
				return this._dateTimeFormat ?? string.Empty;
			}
			set
			{
				this._dateTimeFormat = StringUtils.NullEmptyString(value);
			}
		}

		public CultureInfo Culture
		{
			get
			{
				return this._culture ?? CultureInfo.CurrentCulture;
			}
			set
			{
				this._culture = value;
			}
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			string value2;
			if (value is DateTime)
			{
				DateTime dateTime = (DateTime)value;
				if ((this._dateTimeStyles & DateTimeStyles.AdjustToUniversal) == DateTimeStyles.AdjustToUniversal || (this._dateTimeStyles & DateTimeStyles.AssumeUniversal) == DateTimeStyles.AssumeUniversal)
				{
					dateTime = dateTime.ToUniversalTime();
				}
				value2 = dateTime.ToString(this._dateTimeFormat ?? "yyyy'-'MM'-'dd'T'HH':'mm':'ss.FFFFFFFK", this.Culture);
			}
			else
			{
				if (!(value is DateTimeOffset))
				{
					throw new Exception("Unexpected value when converting date. Expected DateTime or DateTimeOffset, got {0}.".FormatWith(CultureInfo.InvariantCulture, new object[]
					{
						ReflectionUtils.GetObjectType(value)
					}));
				}
				DateTimeOffset dateTimeOffset = (DateTimeOffset)value;
				if ((this._dateTimeStyles & DateTimeStyles.AdjustToUniversal) == DateTimeStyles.AdjustToUniversal || (this._dateTimeStyles & DateTimeStyles.AssumeUniversal) == DateTimeStyles.AssumeUniversal)
				{
					dateTimeOffset = dateTimeOffset.ToUniversalTime();
				}
				value2 = dateTimeOffset.ToString(this._dateTimeFormat ?? "yyyy'-'MM'-'dd'T'HH':'mm':'ss.FFFFFFFK", this.Culture);
			}
			writer.WriteValue(value2);
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			bool flag = ReflectionUtils.IsNullableType(objectType);
			Type type = (!flag) ? objectType : Nullable.GetUnderlyingType(objectType);
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
				if (reader.TokenType != JsonToken.String)
				{
					throw new Exception("Unexpected token parsing date. Expected String, got {0}.".FormatWith(CultureInfo.InvariantCulture, new object[]
					{
						reader.TokenType
					}));
				}
				string text = reader.Value.ToString();
				if (string.IsNullOrEmpty(text) && flag)
				{
					return null;
				}
				if (type == typeof(DateTimeOffset))
				{
					if (!string.IsNullOrEmpty(this._dateTimeFormat))
					{
						return DateTimeOffset.ParseExact(text, this._dateTimeFormat, this.Culture, this._dateTimeStyles);
					}
					return DateTimeOffset.Parse(text, this.Culture, this._dateTimeStyles);
				}
				else
				{
					if (!string.IsNullOrEmpty(this._dateTimeFormat))
					{
						return DateTime.ParseExact(text, this._dateTimeFormat, this.Culture, this._dateTimeStyles);
					}
					return DateTime.Parse(text, this.Culture, this._dateTimeStyles);
				}
			}
		}

		private const string DefaultDateTimeFormat = "yyyy'-'MM'-'dd'T'HH':'mm':'ss.FFFFFFFK";

		private DateTimeStyles _dateTimeStyles = DateTimeStyles.RoundtripKind;

		private string _dateTimeFormat;

		private CultureInfo _culture;
	}
}
