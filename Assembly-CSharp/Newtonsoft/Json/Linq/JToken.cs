using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Linq
{
	public abstract class JToken : IJsonLineInfo, IEnumerable, IEnumerable<JToken>, ICloneable, IJEnumerable<JToken>
	{
		internal JToken()
		{
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable<JToken>)this).GetEnumerator();
		}

		IEnumerator<JToken> IEnumerable<JToken>.GetEnumerator()
		{
			return this.Children().GetEnumerator();
		}

		IJEnumerable<JToken> IJEnumerable<JToken>.this[object key]
		{
			get
			{
				return this[key];
			}
		}

		bool IJsonLineInfo.HasLineInfo()
		{
			int? lineNumber = this._lineNumber;
			bool result;
			if (lineNumber != null)
			{
				int? linePosition = this._linePosition;
				result = (linePosition != null);
			}
			else
			{
				result = false;
			}
			return result;
		}

		int IJsonLineInfo.LineNumber
		{
			get
			{
				int? lineNumber = this._lineNumber;
				return (lineNumber == null) ? 0 : lineNumber.Value;
			}
		}

		int IJsonLineInfo.LinePosition
		{
			get
			{
				int? linePosition = this._linePosition;
				return (linePosition == null) ? 0 : linePosition.Value;
			}
		}

		object ICloneable.Clone()
		{
			return this.DeepClone();
		}

		public static JTokenEqualityComparer EqualityComparer
		{
			get
			{
				if (JToken._equalityComparer == null)
				{
					JToken._equalityComparer = new JTokenEqualityComparer();
				}
				return JToken._equalityComparer;
			}
		}

		public JContainer Parent
		{
			[DebuggerStepThrough]
			get
			{
				return this._parent;
			}
			internal set
			{
				this._parent = value;
			}
		}

		public JToken Root
		{
			get
			{
				JContainer parent = this.Parent;
				if (parent == null)
				{
					return this;
				}
				while (parent.Parent != null)
				{
					parent = parent.Parent;
				}
				return parent;
			}
		}

		internal abstract JToken CloneToken();

		internal abstract bool DeepEquals(JToken node);

		public abstract JTokenType Type { get; }

		public abstract bool HasValues { get; }

		public static bool DeepEquals(JToken t1, JToken t2)
		{
			return t1 == t2 || (t1 != null && t2 != null && t1.DeepEquals(t2));
		}

		public JToken Next
		{
			get
			{
				return this._next;
			}
			internal set
			{
				this._next = value;
			}
		}

		public JToken Previous
		{
			get
			{
				return this._previous;
			}
			internal set
			{
				this._previous = value;
			}
		}

		public void AddAfterSelf(object content)
		{
			if (this._parent == null)
			{
				throw new InvalidOperationException("The parent is missing.");
			}
			int num = this._parent.IndexOfItem(this);
			this._parent.AddInternal(num + 1, content);
		}

		public void AddBeforeSelf(object content)
		{
			if (this._parent == null)
			{
				throw new InvalidOperationException("The parent is missing.");
			}
			int index = this._parent.IndexOfItem(this);
			this._parent.AddInternal(index, content);
		}

		public IEnumerable<JToken> Ancestors()
		{
			for (JToken parent = this.Parent; parent != null; parent = parent.Parent)
			{
				yield return parent;
			}
			yield break;
		}

		public IEnumerable<JToken> AfterSelf()
		{
			if (this.Parent == null)
			{
				yield break;
			}
			for (JToken o = this.Next; o != null; o = o.Next)
			{
				yield return o;
			}
			yield break;
		}

		public IEnumerable<JToken> BeforeSelf()
		{
			for (JToken o = this.Parent.First; o != this; o = o.Next)
			{
				yield return o;
			}
			yield break;
		}

		public virtual JToken this[object key]
		{
			get
			{
				throw new InvalidOperationException("Cannot access child value on {0}.".FormatWith(CultureInfo.InvariantCulture, new object[]
				{
					base.GetType()
				}));
			}
			set
			{
				throw new InvalidOperationException("Cannot set child value on {0}.".FormatWith(CultureInfo.InvariantCulture, new object[]
				{
					base.GetType()
				}));
			}
		}

		public virtual T Value<T>(object key)
		{
			JToken token = this[key];
			return token.Convert<JToken, T>();
		}

		public virtual JToken First
		{
			get
			{
				throw new InvalidOperationException("Cannot access child value on {0}.".FormatWith(CultureInfo.InvariantCulture, new object[]
				{
					base.GetType()
				}));
			}
		}

		public virtual JToken Last
		{
			get
			{
				throw new InvalidOperationException("Cannot access child value on {0}.".FormatWith(CultureInfo.InvariantCulture, new object[]
				{
					base.GetType()
				}));
			}
		}

		public virtual JEnumerable<JToken> Children()
		{
			return JEnumerable<JToken>.Empty;
		}

		public JEnumerable<T> Children<T>() where T : JToken
		{
			return new JEnumerable<T>(this.Children().OfType<T>());
		}

		public virtual IEnumerable<T> Values<T>()
		{
			throw new InvalidOperationException("Cannot access child value on {0}.".FormatWith(CultureInfo.InvariantCulture, new object[]
			{
				base.GetType()
			}));
		}

		public void Remove()
		{
			if (this._parent == null)
			{
				throw new InvalidOperationException("The parent is missing.");
			}
			this._parent.RemoveItem(this);
		}

		public void Replace(JToken value)
		{
			if (this._parent == null)
			{
				throw new InvalidOperationException("The parent is missing.");
			}
			this._parent.ReplaceItem(this, value);
		}

		public abstract void WriteTo(JsonWriter writer, params JsonConverter[] converters);

		public override string ToString()
		{
			return this.ToString(Formatting.Indented, new JsonConverter[0]);
		}

		public string ToString(Formatting formatting, params JsonConverter[] converters)
		{
			string result;
			using (StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture))
			{
				this.WriteTo(new JsonTextWriter(stringWriter)
				{
					Formatting = formatting
				}, converters);
				result = stringWriter.ToString();
			}
			return result;
		}

		private static JValue EnsureValue(JToken value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (value is JProperty)
			{
				value = ((JProperty)value).Value;
			}
			return value as JValue;
		}

		private static string GetType(JToken token)
		{
			ValidationUtils.ArgumentNotNull(token, "token");
			if (token is JProperty)
			{
				token = ((JProperty)token).Value;
			}
			return token.Type.ToString();
		}

		private static bool IsNullable(JToken o)
		{
			return o.Type == JTokenType.Undefined || o.Type == JTokenType.Null;
		}

		private static bool ValidateFloat(JToken o, bool nullable)
		{
			return o.Type == JTokenType.Float || o.Type == JTokenType.Integer || (nullable && JToken.IsNullable(o));
		}

		private static bool ValidateInteger(JToken o, bool nullable)
		{
			return o.Type == JTokenType.Integer || (nullable && JToken.IsNullable(o));
		}

		private static bool ValidateDate(JToken o, bool nullable)
		{
			return o.Type == JTokenType.Date || (nullable && JToken.IsNullable(o));
		}

		private static bool ValidateBoolean(JToken o, bool nullable)
		{
			return o.Type == JTokenType.Boolean || (nullable && JToken.IsNullable(o));
		}

		private static bool ValidateString(JToken o)
		{
			return o.Type == JTokenType.String || o.Type == JTokenType.Comment || o.Type == JTokenType.Raw || JToken.IsNullable(o);
		}

		private static bool ValidateBytes(JToken o)
		{
			return o.Type == JTokenType.Bytes || JToken.IsNullable(o);
		}

		internal abstract int GetDeepHashCode();

		public JsonReader CreateReader()
		{
			return new JTokenReader(this);
		}

		internal static JToken FromObjectInternal(object o, JsonSerializer jsonSerializer)
		{
			ValidationUtils.ArgumentNotNull(o, "o");
			ValidationUtils.ArgumentNotNull(jsonSerializer, "jsonSerializer");
			JToken token;
			using (JTokenWriter jtokenWriter = new JTokenWriter())
			{
				jsonSerializer.Serialize(jtokenWriter, o);
				token = jtokenWriter.Token;
			}
			return token;
		}

		public static JToken FromObject(object o)
		{
			return JToken.FromObjectInternal(o, new JsonSerializer());
		}

		public static JToken FromObject(object o, JsonSerializer jsonSerializer)
		{
			return JToken.FromObjectInternal(o, jsonSerializer);
		}

		public T ToObject<T>()
		{
			return this.ToObject<T>(new JsonSerializer());
		}

		public T ToObject<T>(JsonSerializer jsonSerializer)
		{
			ValidationUtils.ArgumentNotNull(jsonSerializer, "jsonSerializer");
			T result;
			using (JTokenReader jtokenReader = new JTokenReader(this))
			{
				result = jsonSerializer.Deserialize<T>(jtokenReader);
			}
			return result;
		}

		public static JToken ReadFrom(JsonReader reader)
		{
			ValidationUtils.ArgumentNotNull(reader, "reader");
			if (reader.TokenType == JsonToken.None && !reader.Read())
			{
				throw new Exception("Error reading JToken from JsonReader.");
			}
			if (reader.TokenType == JsonToken.StartObject)
			{
				return JObject.Load(reader);
			}
			if (reader.TokenType == JsonToken.StartArray)
			{
				return JArray.Load(reader);
			}
			if (reader.TokenType == JsonToken.PropertyName)
			{
				return JProperty.Load(reader);
			}
			if (reader.TokenType == JsonToken.StartConstructor)
			{
				return JConstructor.Load(reader);
			}
			if (!JsonReader.IsStartToken(reader.TokenType))
			{
				return new JValue(reader.Value);
			}
			throw new Exception("Error reading JToken from JsonReader. Unexpected token: {0}".FormatWith(CultureInfo.InvariantCulture, new object[]
			{
				reader.TokenType
			}));
		}

		public static JToken Parse(string json)
		{
			JsonReader reader = new JsonTextReader(new StringReader(json));
			return JToken.Load(reader);
		}

		public static JToken Load(JsonReader reader)
		{
			return JToken.ReadFrom(reader);
		}

		internal void SetLineInfo(IJsonLineInfo lineInfo)
		{
			if (lineInfo == null || !lineInfo.HasLineInfo())
			{
				return;
			}
			this.SetLineInfo(lineInfo.LineNumber, lineInfo.LinePosition);
		}

		internal void SetLineInfo(int lineNumber, int linePosition)
		{
			this._lineNumber = new int?(lineNumber);
			this._linePosition = new int?(linePosition);
		}

		public JToken SelectToken(string path)
		{
			return this.SelectToken(path, false);
		}

		public JToken SelectToken(string path, bool errorWhenNoMatch)
		{
			JPath jpath = new JPath(path);
			return jpath.Evaluate(this, errorWhenNoMatch);
		}

		public JToken DeepClone()
		{
			return this.CloneToken();
		}

		public static explicit operator bool(JToken value)
		{
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateBoolean(jvalue, false))
			{
				throw new ArgumentException("Can not convert {0} to Boolean.".FormatWith(CultureInfo.InvariantCulture, new object[]
				{
					JToken.GetType(value)
				}));
			}
			return (bool)jvalue.Value;
		}

		public static explicit operator DateTimeOffset(JToken value)
		{
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateDate(jvalue, false))
			{
				throw new ArgumentException("Can not convert {0} to DateTimeOffset.".FormatWith(CultureInfo.InvariantCulture, new object[]
				{
					JToken.GetType(value)
				}));
			}
			return (DateTimeOffset)jvalue.Value;
		}

		public static explicit operator bool?(JToken value)
		{
			if (value == null)
			{
				return null;
			}
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateBoolean(jvalue, true))
			{
				throw new ArgumentException("Can not convert {0} to Boolean.".FormatWith(CultureInfo.InvariantCulture, new object[]
				{
					JToken.GetType(value)
				}));
			}
			return (bool?)jvalue.Value;
		}

		public static explicit operator long(JToken value)
		{
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateInteger(jvalue, false))
			{
				throw new ArgumentException("Can not convert {0} to Int64.".FormatWith(CultureInfo.InvariantCulture, new object[]
				{
					JToken.GetType(value)
				}));
			}
			return (long)jvalue.Value;
		}

		public static explicit operator DateTime?(JToken value)
		{
			if (value == null)
			{
				return null;
			}
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateDate(jvalue, true))
			{
				throw new ArgumentException("Can not convert {0} to DateTime.".FormatWith(CultureInfo.InvariantCulture, new object[]
				{
					JToken.GetType(value)
				}));
			}
			return (DateTime?)jvalue.Value;
		}

		public static explicit operator DateTimeOffset?(JToken value)
		{
			if (value == null)
			{
				return null;
			}
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateDate(jvalue, true))
			{
				throw new ArgumentException("Can not convert {0} to DateTimeOffset.".FormatWith(CultureInfo.InvariantCulture, new object[]
				{
					JToken.GetType(value)
				}));
			}
			return (DateTimeOffset?)jvalue.Value;
		}

		public static explicit operator decimal?(JToken value)
		{
			if (value == null)
			{
				return null;
			}
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateFloat(jvalue, true))
			{
				throw new ArgumentException("Can not convert {0} to Decimal.".FormatWith(CultureInfo.InvariantCulture, new object[]
				{
					JToken.GetType(value)
				}));
			}
			return (jvalue.Value == null) ? null : new decimal?(Convert.ToDecimal(jvalue.Value, CultureInfo.InvariantCulture));
		}

		public static explicit operator double?(JToken value)
		{
			if (value == null)
			{
				return null;
			}
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateFloat(jvalue, true))
			{
				throw new ArgumentException("Can not convert {0} to Double.".FormatWith(CultureInfo.InvariantCulture, new object[]
				{
					JToken.GetType(value)
				}));
			}
			return (double?)jvalue.Value;
		}

		public static explicit operator int(JToken value)
		{
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateInteger(jvalue, false))
			{
				throw new ArgumentException("Can not convert {0} to Int32.".FormatWith(CultureInfo.InvariantCulture, new object[]
				{
					JToken.GetType(value)
				}));
			}
			return Convert.ToInt32(jvalue.Value, CultureInfo.InvariantCulture);
		}

		public static explicit operator short(JToken value)
		{
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateInteger(jvalue, false))
			{
				throw new ArgumentException("Can not convert {0} to Int16.".FormatWith(CultureInfo.InvariantCulture, new object[]
				{
					JToken.GetType(value)
				}));
			}
			return Convert.ToInt16(jvalue.Value, CultureInfo.InvariantCulture);
		}

		public static explicit operator ushort(JToken value)
		{
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateInteger(jvalue, false))
			{
				throw new ArgumentException("Can not convert {0} to UInt16.".FormatWith(CultureInfo.InvariantCulture, new object[]
				{
					JToken.GetType(value)
				}));
			}
			return Convert.ToUInt16(jvalue.Value, CultureInfo.InvariantCulture);
		}

		public static explicit operator int?(JToken value)
		{
			if (value == null)
			{
				return null;
			}
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateInteger(jvalue, true))
			{
				throw new ArgumentException("Can not convert {0} to Int32.".FormatWith(CultureInfo.InvariantCulture, new object[]
				{
					JToken.GetType(value)
				}));
			}
			return (jvalue.Value == null) ? null : new int?(Convert.ToInt32(jvalue.Value, CultureInfo.InvariantCulture));
		}

		public static explicit operator short?(JToken value)
		{
			if (value == null)
			{
				return null;
			}
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateInteger(jvalue, true))
			{
				throw new ArgumentException("Can not convert {0} to Int16.".FormatWith(CultureInfo.InvariantCulture, new object[]
				{
					JToken.GetType(value)
				}));
			}
			return (jvalue.Value == null) ? null : new short?(Convert.ToInt16(jvalue.Value, CultureInfo.InvariantCulture));
		}

		public static explicit operator ushort?(JToken value)
		{
			if (value == null)
			{
				return null;
			}
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateInteger(jvalue, true))
			{
				throw new ArgumentException("Can not convert {0} to UInt16.".FormatWith(CultureInfo.InvariantCulture, new object[]
				{
					JToken.GetType(value)
				}));
			}
			return (jvalue.Value == null) ? null : new ushort?((ushort)Convert.ToInt16(jvalue.Value, CultureInfo.InvariantCulture));
		}

		public static explicit operator DateTime(JToken value)
		{
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateDate(jvalue, false))
			{
				throw new ArgumentException("Can not convert {0} to DateTime.".FormatWith(CultureInfo.InvariantCulture, new object[]
				{
					JToken.GetType(value)
				}));
			}
			return (DateTime)jvalue.Value;
		}

		public static explicit operator long?(JToken value)
		{
			if (value == null)
			{
				return null;
			}
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateInteger(jvalue, true))
			{
				throw new ArgumentException("Can not convert {0} to Int64.".FormatWith(CultureInfo.InvariantCulture, new object[]
				{
					JToken.GetType(value)
				}));
			}
			return (long?)jvalue.Value;
		}

		public static explicit operator float?(JToken value)
		{
			if (value == null)
			{
				return null;
			}
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateFloat(jvalue, true))
			{
				throw new ArgumentException("Can not convert {0} to Single.".FormatWith(CultureInfo.InvariantCulture, new object[]
				{
					JToken.GetType(value)
				}));
			}
			return (jvalue.Value == null) ? null : new float?(Convert.ToSingle(jvalue.Value, CultureInfo.InvariantCulture));
		}

		public static explicit operator decimal(JToken value)
		{
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateFloat(jvalue, false))
			{
				throw new ArgumentException("Can not convert {0} to Decimal.".FormatWith(CultureInfo.InvariantCulture, new object[]
				{
					JToken.GetType(value)
				}));
			}
			return Convert.ToDecimal(jvalue.Value, CultureInfo.InvariantCulture);
		}

		public static explicit operator uint?(JToken value)
		{
			if (value == null)
			{
				return null;
			}
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateInteger(jvalue, true))
			{
				throw new ArgumentException("Can not convert {0} to UInt32.".FormatWith(CultureInfo.InvariantCulture, new object[]
				{
					JToken.GetType(value)
				}));
			}
			return (uint?)jvalue.Value;
		}

		public static explicit operator ulong?(JToken value)
		{
			if (value == null)
			{
				return null;
			}
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateInteger(jvalue, true))
			{
				throw new ArgumentException("Can not convert {0} to UInt64.".FormatWith(CultureInfo.InvariantCulture, new object[]
				{
					JToken.GetType(value)
				}));
			}
			return (ulong?)jvalue.Value;
		}

		public static explicit operator double(JToken value)
		{
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateFloat(jvalue, false))
			{
				throw new ArgumentException("Can not convert {0} to Double.".FormatWith(CultureInfo.InvariantCulture, new object[]
				{
					JToken.GetType(value)
				}));
			}
			return (double)jvalue.Value;
		}

		public static explicit operator float(JToken value)
		{
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateFloat(jvalue, false))
			{
				throw new ArgumentException("Can not convert {0} to Single.".FormatWith(CultureInfo.InvariantCulture, new object[]
				{
					JToken.GetType(value)
				}));
			}
			return Convert.ToSingle(jvalue.Value, CultureInfo.InvariantCulture);
		}

		public static explicit operator string(JToken value)
		{
			if (value == null)
			{
				return null;
			}
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateString(jvalue))
			{
				throw new ArgumentException("Can not convert {0} to String.".FormatWith(CultureInfo.InvariantCulture, new object[]
				{
					JToken.GetType(value)
				}));
			}
			return (string)jvalue.Value;
		}

		public static explicit operator uint(JToken value)
		{
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateInteger(jvalue, false))
			{
				throw new ArgumentException("Can not convert {0} to UInt32.".FormatWith(CultureInfo.InvariantCulture, new object[]
				{
					JToken.GetType(value)
				}));
			}
			return Convert.ToUInt32(jvalue.Value, CultureInfo.InvariantCulture);
		}

		public static explicit operator ulong(JToken value)
		{
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateInteger(jvalue, false))
			{
				throw new ArgumentException("Can not convert {0} to UInt64.".FormatWith(CultureInfo.InvariantCulture, new object[]
				{
					JToken.GetType(value)
				}));
			}
			return Convert.ToUInt64(jvalue.Value, CultureInfo.InvariantCulture);
		}

		public static explicit operator byte[](JToken value)
		{
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateBytes(jvalue))
			{
				throw new ArgumentException("Can not convert {0} to byte array.".FormatWith(CultureInfo.InvariantCulture, new object[]
				{
					JToken.GetType(value)
				}));
			}
			return (byte[])jvalue.Value;
		}

		public static implicit operator JToken(bool value)
		{
			return new JValue(value);
		}

		public static implicit operator JToken(DateTimeOffset value)
		{
			return new JValue(value);
		}

		public static implicit operator JToken(bool? value)
		{
			return new JValue(value);
		}

		public static implicit operator JToken(long value)
		{
			return new JValue(value);
		}

		public static implicit operator JToken(DateTime? value)
		{
			return new JValue(value);
		}

		public static implicit operator JToken(DateTimeOffset? value)
		{
			return new JValue(value);
		}

		public static implicit operator JToken(decimal? value)
		{
			return new JValue(value);
		}

		public static implicit operator JToken(double? value)
		{
			return new JValue(value);
		}

		public static implicit operator JToken(short value)
		{
			return new JValue((long)value);
		}

		public static implicit operator JToken(ushort value)
		{
			return new JValue((long)value);
		}

		public static implicit operator JToken(int value)
		{
			return new JValue((long)value);
		}

		public static implicit operator JToken(int? value)
		{
			return new JValue(value);
		}

		public static implicit operator JToken(DateTime value)
		{
			return new JValue(value);
		}

		public static implicit operator JToken(long? value)
		{
			return new JValue(value);
		}

		public static implicit operator JToken(float? value)
		{
			return new JValue(value);
		}

		public static implicit operator JToken(decimal value)
		{
			return new JValue(value);
		}

		public static implicit operator JToken(short? value)
		{
			return new JValue(value);
		}

		public static implicit operator JToken(ushort? value)
		{
			return new JValue(value);
		}

		public static implicit operator JToken(uint? value)
		{
			return new JValue(value);
		}

		public static implicit operator JToken(ulong? value)
		{
			return new JValue(value);
		}

		public static implicit operator JToken(double value)
		{
			return new JValue(value);
		}

		public static implicit operator JToken(float value)
		{
			return new JValue((double)value);
		}

		public static implicit operator JToken(string value)
		{
			return new JValue(value);
		}

		public static implicit operator JToken(uint value)
		{
			return new JValue((long)((ulong)value));
		}

		public static implicit operator JToken(ulong value)
		{
			return new JValue(value);
		}

		public static implicit operator JToken(byte[] value)
		{
			return new JValue(value);
		}

		private JContainer _parent;

		private JToken _previous;

		private JToken _next;

		private static JTokenEqualityComparer _equalityComparer;

		private int? _lineNumber;

		private int? _linePosition;
	}
}
