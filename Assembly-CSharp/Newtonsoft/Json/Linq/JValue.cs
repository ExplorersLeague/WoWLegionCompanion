using System;
using System.Collections.Generic;
using System.Globalization;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Linq
{
	public class JValue : JToken, IFormattable, IComparable, IEquatable<JValue>, IComparable<JValue>
	{
		internal JValue(object value, JTokenType type)
		{
			this._value = value;
			this._valueType = type;
		}

		public JValue(JValue other) : this(other.Value, other.Type)
		{
		}

		public JValue(long value) : this(value, JTokenType.Integer)
		{
		}

		public JValue(ulong value) : this(value, JTokenType.Integer)
		{
		}

		public JValue(double value) : this(value, JTokenType.Float)
		{
		}

		public JValue(DateTime value) : this(value, JTokenType.Date)
		{
		}

		public JValue(bool value) : this(value, JTokenType.Boolean)
		{
		}

		public JValue(string value) : this(value, JTokenType.String)
		{
		}

		public JValue(Guid value) : this(value, JTokenType.String)
		{
		}

		public JValue(Uri value) : this(value, JTokenType.String)
		{
		}

		public JValue(TimeSpan value) : this(value, JTokenType.String)
		{
		}

		public JValue(object value) : this(value, JValue.GetValueType(null, value))
		{
		}

		int IComparable.CompareTo(object obj)
		{
			if (obj == null)
			{
				return 1;
			}
			object objB = (!(obj is JValue)) ? obj : ((JValue)obj).Value;
			return JValue.Compare(this._valueType, this._value, objB);
		}

		internal override bool DeepEquals(JToken node)
		{
			JValue jvalue = node as JValue;
			return jvalue != null && JValue.ValuesEquals(this, jvalue);
		}

		public override bool HasValues
		{
			get
			{
				return false;
			}
		}

		private static int Compare(JTokenType valueType, object objA, object objB)
		{
			if (objA == null && objB == null)
			{
				return 0;
			}
			if (objA != null && objB == null)
			{
				return 1;
			}
			if (objA == null && objB != null)
			{
				return -1;
			}
			switch (valueType)
			{
			case JTokenType.Comment:
			case JTokenType.String:
			case JTokenType.Raw:
			{
				string text = Convert.ToString(objA, CultureInfo.InvariantCulture);
				string strB = Convert.ToString(objB, CultureInfo.InvariantCulture);
				return text.CompareTo(strB);
			}
			case JTokenType.Integer:
				if (objA is ulong || objB is ulong || objA is decimal || objB is decimal)
				{
					return Convert.ToDecimal(objA, CultureInfo.InvariantCulture).CompareTo(Convert.ToDecimal(objB, CultureInfo.InvariantCulture));
				}
				if (objA is float || objB is float || objA is double || objB is double)
				{
					return JValue.CompareFloat(objA, objB);
				}
				return Convert.ToInt64(objA, CultureInfo.InvariantCulture).CompareTo(Convert.ToInt64(objB, CultureInfo.InvariantCulture));
			case JTokenType.Float:
				return JValue.CompareFloat(objA, objB);
			case JTokenType.Boolean:
			{
				bool flag = Convert.ToBoolean(objA, CultureInfo.InvariantCulture);
				bool value = Convert.ToBoolean(objB, CultureInfo.InvariantCulture);
				return flag.CompareTo(value);
			}
			case JTokenType.Date:
			{
				if (objA is DateTime)
				{
					DateTime dateTime = Convert.ToDateTime(objA, CultureInfo.InvariantCulture);
					DateTime value2 = Convert.ToDateTime(objB, CultureInfo.InvariantCulture);
					return dateTime.CompareTo(value2);
				}
				if (!(objB is DateTimeOffset))
				{
					throw new ArgumentException("Object must be of type DateTimeOffset.");
				}
				DateTimeOffset dateTimeOffset = (DateTimeOffset)objA;
				DateTimeOffset other = (DateTimeOffset)objB;
				return dateTimeOffset.CompareTo(other);
			}
			case JTokenType.Bytes:
			{
				if (!(objB is byte[]))
				{
					throw new ArgumentException("Object must be of type byte[].");
				}
				byte[] array = objA as byte[];
				byte[] array2 = objB as byte[];
				if (array == null)
				{
					return -1;
				}
				if (array2 == null)
				{
					return 1;
				}
				return MiscellaneousUtils.ByteArrayCompare(array, array2);
			}
			case JTokenType.Guid:
			{
				if (!(objB is Guid))
				{
					throw new ArgumentException("Object must be of type Guid.");
				}
				Guid guid = (Guid)objA;
				Guid value3 = (Guid)objB;
				return guid.CompareTo(value3);
			}
			case JTokenType.Uri:
			{
				if (!(objB is Uri))
				{
					throw new ArgumentException("Object must be of type Uri.");
				}
				Uri uri = (Uri)objA;
				Uri uri2 = (Uri)objB;
				return Comparer<string>.Default.Compare(uri.ToString(), uri2.ToString());
			}
			case JTokenType.TimeSpan:
			{
				if (!(objB is TimeSpan))
				{
					throw new ArgumentException("Object must be of type TimeSpan.");
				}
				TimeSpan timeSpan = (TimeSpan)objA;
				TimeSpan value4 = (TimeSpan)objB;
				return timeSpan.CompareTo(value4);
			}
			}
			throw MiscellaneousUtils.CreateArgumentOutOfRangeException("valueType", valueType, "Unexpected value type: {0}".FormatWith(CultureInfo.InvariantCulture, new object[]
			{
				valueType
			}));
		}

		private static int CompareFloat(object objA, object objB)
		{
			double d = Convert.ToDouble(objA, CultureInfo.InvariantCulture);
			double num = Convert.ToDouble(objB, CultureInfo.InvariantCulture);
			if (MathUtils.ApproxEquals(d, num))
			{
				return 0;
			}
			return d.CompareTo(num);
		}

		internal override JToken CloneToken()
		{
			return new JValue(this);
		}

		public static JValue CreateComment(string value)
		{
			return new JValue(value, JTokenType.Comment);
		}

		public static JValue CreateString(string value)
		{
			return new JValue(value, JTokenType.String);
		}

		private static JTokenType GetValueType(JTokenType? current, object value)
		{
			if (value == null)
			{
				return JTokenType.Null;
			}
			if (value == DBNull.Value)
			{
				return JTokenType.Null;
			}
			if (value is string)
			{
				return JValue.GetStringValueType(current);
			}
			if (value is long || value is int || value is short || value is sbyte || value is ulong || value is uint || value is ushort || value is byte)
			{
				return JTokenType.Integer;
			}
			if (value is Enum)
			{
				return JTokenType.Integer;
			}
			if (value is double || value is float || value is decimal)
			{
				return JTokenType.Float;
			}
			if (value is DateTime)
			{
				return JTokenType.Date;
			}
			if (value is DateTimeOffset)
			{
				return JTokenType.Date;
			}
			if (value is byte[])
			{
				return JTokenType.Bytes;
			}
			if (value is bool)
			{
				return JTokenType.Boolean;
			}
			if (value is Guid)
			{
				return JTokenType.Guid;
			}
			if (value is Uri)
			{
				return JTokenType.Uri;
			}
			if (value is TimeSpan)
			{
				return JTokenType.TimeSpan;
			}
			throw new ArgumentException("Could not determine JSON object type for type {0}.".FormatWith(CultureInfo.InvariantCulture, new object[]
			{
				value.GetType()
			}));
		}

		private static JTokenType GetStringValueType(JTokenType? current)
		{
			if (current == null)
			{
				return JTokenType.String;
			}
			JTokenType value = current.Value;
			switch (value)
			{
			case JTokenType.Comment:
			case JTokenType.String:
				break;
			default:
				if (value != JTokenType.Raw)
				{
					return JTokenType.String;
				}
				break;
			}
			return current.Value;
		}

		public override JTokenType Type
		{
			get
			{
				return this._valueType;
			}
		}

		public new object Value
		{
			get
			{
				return this._value;
			}
			set
			{
				Type type = (this._value == null) ? null : this._value.GetType();
				Type type2 = (value == null) ? null : value.GetType();
				if (type != type2)
				{
					this._valueType = JValue.GetValueType(new JTokenType?(this._valueType), value);
				}
				this._value = value;
			}
		}

		public override void WriteTo(JsonWriter writer, params JsonConverter[] converters)
		{
			JTokenType valueType = this._valueType;
			switch (valueType)
			{
			case JTokenType.Null:
				writer.WriteNull();
				return;
			case JTokenType.Undefined:
				writer.WriteUndefined();
				return;
			default:
			{
				if (valueType == JTokenType.Comment)
				{
					writer.WriteComment(this._value.ToString());
					return;
				}
				JsonConverter matchingConverter;
				if (this._value != null && (matchingConverter = JsonSerializer.GetMatchingConverter(converters, this._value.GetType())) != null)
				{
					matchingConverter.WriteJson(writer, this._value, new JsonSerializer());
					return;
				}
				switch (this._valueType)
				{
				case JTokenType.Integer:
					writer.WriteValue(Convert.ToInt64(this._value, CultureInfo.InvariantCulture));
					return;
				case JTokenType.Float:
					writer.WriteValue(Convert.ToDouble(this._value, CultureInfo.InvariantCulture));
					return;
				case JTokenType.String:
					writer.WriteValue((this._value == null) ? null : this._value.ToString());
					return;
				case JTokenType.Boolean:
					writer.WriteValue(Convert.ToBoolean(this._value, CultureInfo.InvariantCulture));
					return;
				case JTokenType.Date:
					if (this._value is DateTimeOffset)
					{
						writer.WriteValue((DateTimeOffset)this._value);
					}
					else
					{
						writer.WriteValue(Convert.ToDateTime(this._value, CultureInfo.InvariantCulture));
					}
					return;
				case JTokenType.Bytes:
					writer.WriteValue((byte[])this._value);
					return;
				case JTokenType.Guid:
				case JTokenType.Uri:
				case JTokenType.TimeSpan:
					writer.WriteValue((this._value == null) ? null : this._value.ToString());
					return;
				}
				throw MiscellaneousUtils.CreateArgumentOutOfRangeException("TokenType", this._valueType, "Unexpected token type.");
			}
			case JTokenType.Raw:
				writer.WriteRawValue((this._value == null) ? null : this._value.ToString());
				return;
			}
		}

		internal override int GetDeepHashCode()
		{
			int num = (this._value == null) ? 0 : this._value.GetHashCode();
			return this._valueType.GetHashCode() ^ num;
		}

		private static bool ValuesEquals(JValue v1, JValue v2)
		{
			return v1 == v2 || (v1._valueType == v2._valueType && JValue.Compare(v1._valueType, v1._value, v2._value) == 0);
		}

		public bool Equals(JValue other)
		{
			return other != null && JValue.ValuesEquals(this, other);
		}

		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			JValue jvalue = obj as JValue;
			if (jvalue != null)
			{
				return this.Equals(jvalue);
			}
			return base.Equals(obj);
		}

		public override int GetHashCode()
		{
			if (this._value == null)
			{
				return 0;
			}
			return this._value.GetHashCode();
		}

		public override string ToString()
		{
			if (this._value == null)
			{
				return string.Empty;
			}
			return this._value.ToString();
		}

		public string ToString(string format)
		{
			return this.ToString(format, CultureInfo.CurrentCulture);
		}

		public string ToString(IFormatProvider formatProvider)
		{
			return this.ToString(null, formatProvider);
		}

		public string ToString(string format, IFormatProvider formatProvider)
		{
			if (this._value == null)
			{
				return string.Empty;
			}
			IFormattable formattable = this._value as IFormattable;
			if (formattable != null)
			{
				return formattable.ToString(format, formatProvider);
			}
			return this._value.ToString();
		}

		public int CompareTo(JValue obj)
		{
			if (obj == null)
			{
				return 1;
			}
			return JValue.Compare(this._valueType, this._value, obj._value);
		}

		private JTokenType _valueType;

		private object _value;
	}
}
