using System;
using System.Collections.Generic;
using System.Globalization;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json
{
	public abstract class JsonWriter : IDisposable
	{
		protected JsonWriter()
		{
			this._stack = new List<JTokenType>(8);
			this._stack.Add(JTokenType.None);
			this._currentState = JsonWriter.State.Start;
			this._formatting = Formatting.None;
			this.CloseOutput = true;
		}

		public bool CloseOutput { get; set; }

		protected internal int Top
		{
			get
			{
				return this._top;
			}
		}

		public WriteState WriteState
		{
			get
			{
				switch (this._currentState)
				{
				case JsonWriter.State.Start:
					return WriteState.Start;
				case JsonWriter.State.Property:
					return WriteState.Property;
				case JsonWriter.State.ObjectStart:
				case JsonWriter.State.Object:
					return WriteState.Object;
				case JsonWriter.State.ArrayStart:
				case JsonWriter.State.Array:
					return WriteState.Array;
				case JsonWriter.State.ConstructorStart:
				case JsonWriter.State.Constructor:
					return WriteState.Constructor;
				case JsonWriter.State.Closed:
					return WriteState.Closed;
				case JsonWriter.State.Error:
					return WriteState.Error;
				}
				throw new JsonWriterException("Invalid state: " + this._currentState);
			}
		}

		public Formatting Formatting
		{
			get
			{
				return this._formatting;
			}
			set
			{
				this._formatting = value;
			}
		}

		private void Push(JTokenType value)
		{
			this._top++;
			if (this._stack.Count <= this._top)
			{
				this._stack.Add(value);
			}
			else
			{
				this._stack[this._top] = value;
			}
		}

		private JTokenType Pop()
		{
			JTokenType result = this.Peek();
			this._top--;
			return result;
		}

		private JTokenType Peek()
		{
			return this._stack[this._top];
		}

		public abstract void Flush();

		public virtual void Close()
		{
			this.AutoCompleteAll();
		}

		public virtual void WriteStartObject()
		{
			this.AutoComplete(JsonToken.StartObject);
			this.Push(JTokenType.Object);
		}

		public virtual void WriteEndObject()
		{
			this.AutoCompleteClose(JsonToken.EndObject);
		}

		public virtual void WriteStartArray()
		{
			this.AutoComplete(JsonToken.StartArray);
			this.Push(JTokenType.Array);
		}

		public virtual void WriteEndArray()
		{
			this.AutoCompleteClose(JsonToken.EndArray);
		}

		public virtual void WriteStartConstructor(string name)
		{
			this.AutoComplete(JsonToken.StartConstructor);
			this.Push(JTokenType.Constructor);
		}

		public virtual void WriteEndConstructor()
		{
			this.AutoCompleteClose(JsonToken.EndConstructor);
		}

		public virtual void WritePropertyName(string name)
		{
			this.AutoComplete(JsonToken.PropertyName);
		}

		public virtual void WriteEnd()
		{
			this.WriteEnd(this.Peek());
		}

		public void WriteToken(JsonReader reader)
		{
			ValidationUtils.ArgumentNotNull(reader, "reader");
			int initialDepth;
			if (reader.TokenType == JsonToken.None)
			{
				initialDepth = -1;
			}
			else if (!this.IsStartToken(reader.TokenType))
			{
				initialDepth = reader.Depth + 1;
			}
			else
			{
				initialDepth = reader.Depth;
			}
			this.WriteToken(reader, initialDepth);
		}

		internal void WriteToken(JsonReader reader, int initialDepth)
		{
			for (;;)
			{
				switch (reader.TokenType)
				{
				case JsonToken.None:
					goto IL_1DB;
				case JsonToken.StartObject:
					this.WriteStartObject();
					goto IL_1DB;
				case JsonToken.StartArray:
					this.WriteStartArray();
					goto IL_1DB;
				case JsonToken.StartConstructor:
				{
					string strA = reader.Value.ToString();
					if (string.Compare(strA, "Date", StringComparison.Ordinal) == 0)
					{
						this.WriteConstructorDate(reader);
					}
					else
					{
						this.WriteStartConstructor(reader.Value.ToString());
					}
					goto IL_1DB;
				}
				case JsonToken.PropertyName:
					this.WritePropertyName(reader.Value.ToString());
					goto IL_1DB;
				case JsonToken.Comment:
					this.WriteComment(reader.Value.ToString());
					goto IL_1DB;
				case JsonToken.Raw:
					this.WriteRawValue((string)reader.Value);
					goto IL_1DB;
				case JsonToken.Integer:
					this.WriteValue(Convert.ToInt64(reader.Value, CultureInfo.InvariantCulture));
					goto IL_1DB;
				case JsonToken.Float:
					this.WriteValue(Convert.ToDouble(reader.Value, CultureInfo.InvariantCulture));
					goto IL_1DB;
				case JsonToken.String:
					this.WriteValue(reader.Value.ToString());
					goto IL_1DB;
				case JsonToken.Boolean:
					this.WriteValue(Convert.ToBoolean(reader.Value, CultureInfo.InvariantCulture));
					goto IL_1DB;
				case JsonToken.Null:
					this.WriteNull();
					goto IL_1DB;
				case JsonToken.Undefined:
					this.WriteUndefined();
					goto IL_1DB;
				case JsonToken.EndObject:
					this.WriteEndObject();
					goto IL_1DB;
				case JsonToken.EndArray:
					this.WriteEndArray();
					goto IL_1DB;
				case JsonToken.EndConstructor:
					this.WriteEndConstructor();
					goto IL_1DB;
				case JsonToken.Date:
					this.WriteValue((DateTime)reader.Value);
					goto IL_1DB;
				case JsonToken.Bytes:
					this.WriteValue((byte[])reader.Value);
					goto IL_1DB;
				}
				break;
				IL_1DB:
				if (initialDepth - 1 >= reader.Depth - ((!this.IsEndToken(reader.TokenType)) ? 0 : 1) || !reader.Read())
				{
					return;
				}
			}
			throw MiscellaneousUtils.CreateArgumentOutOfRangeException("TokenType", reader.TokenType, "Unexpected token type.");
		}

		private void WriteConstructorDate(JsonReader reader)
		{
			if (!reader.Read())
			{
				throw new Exception("Unexpected end while reading date constructor.");
			}
			if (reader.TokenType != JsonToken.Integer)
			{
				throw new Exception("Unexpected token while reading date constructor. Expected Integer, got " + reader.TokenType);
			}
			long javaScriptTicks = (long)reader.Value;
			DateTime value = JsonConvert.ConvertJavaScriptTicksToDateTime(javaScriptTicks);
			if (!reader.Read())
			{
				throw new Exception("Unexpected end while reading date constructor.");
			}
			if (reader.TokenType != JsonToken.EndConstructor)
			{
				throw new Exception("Unexpected token while reading date constructor. Expected EndConstructor, got " + reader.TokenType);
			}
			this.WriteValue(value);
		}

		private bool IsEndToken(JsonToken token)
		{
			switch (token)
			{
			case JsonToken.EndObject:
			case JsonToken.EndArray:
			case JsonToken.EndConstructor:
				return true;
			default:
				return false;
			}
		}

		private bool IsStartToken(JsonToken token)
		{
			switch (token)
			{
			case JsonToken.StartObject:
			case JsonToken.StartArray:
			case JsonToken.StartConstructor:
				return true;
			default:
				return false;
			}
		}

		private void WriteEnd(JTokenType type)
		{
			switch (type)
			{
			case JTokenType.Object:
				this.WriteEndObject();
				break;
			case JTokenType.Array:
				this.WriteEndArray();
				break;
			case JTokenType.Constructor:
				this.WriteEndConstructor();
				break;
			default:
				throw new JsonWriterException("Unexpected type when writing end: " + type);
			}
		}

		private void AutoCompleteAll()
		{
			while (this._top > 0)
			{
				this.WriteEnd();
			}
		}

		private JTokenType GetTypeForCloseToken(JsonToken token)
		{
			switch (token)
			{
			case JsonToken.EndObject:
				return JTokenType.Object;
			case JsonToken.EndArray:
				return JTokenType.Array;
			case JsonToken.EndConstructor:
				return JTokenType.Constructor;
			default:
				throw new JsonWriterException("No type for token: " + token);
			}
		}

		private JsonToken GetCloseTokenForType(JTokenType type)
		{
			switch (type)
			{
			case JTokenType.Object:
				return JsonToken.EndObject;
			case JTokenType.Array:
				return JsonToken.EndArray;
			case JTokenType.Constructor:
				return JsonToken.EndConstructor;
			default:
				throw new JsonWriterException("No close token for type: " + type);
			}
		}

		private void AutoCompleteClose(JsonToken tokenBeingClosed)
		{
			int num = 0;
			for (int i = 0; i < this._top; i++)
			{
				int index = this._top - i;
				if (this._stack[index] == this.GetTypeForCloseToken(tokenBeingClosed))
				{
					num = i + 1;
					break;
				}
			}
			if (num == 0)
			{
				throw new JsonWriterException("No token to close.");
			}
			for (int j = 0; j < num; j++)
			{
				JsonToken closeTokenForType = this.GetCloseTokenForType(this.Pop());
				if (this._currentState != JsonWriter.State.ObjectStart && this._currentState != JsonWriter.State.ArrayStart)
				{
					this.WriteIndent();
				}
				this.WriteEnd(closeTokenForType);
			}
			JTokenType jtokenType = this.Peek();
			switch (jtokenType)
			{
			case JTokenType.None:
				this._currentState = JsonWriter.State.Start;
				break;
			case JTokenType.Object:
				this._currentState = JsonWriter.State.Object;
				break;
			case JTokenType.Array:
				this._currentState = JsonWriter.State.Array;
				break;
			case JTokenType.Constructor:
				this._currentState = JsonWriter.State.Array;
				break;
			default:
				throw new JsonWriterException("Unknown JsonType: " + jtokenType);
			}
		}

		protected virtual void WriteEnd(JsonToken token)
		{
		}

		protected virtual void WriteIndent()
		{
		}

		protected virtual void WriteValueDelimiter()
		{
		}

		protected virtual void WriteIndentSpace()
		{
		}

		internal void AutoComplete(JsonToken tokenBeingWritten)
		{
			int num;
			switch (tokenBeingWritten)
			{
			case JsonToken.Integer:
			case JsonToken.Float:
			case JsonToken.String:
			case JsonToken.Boolean:
			case JsonToken.Null:
			case JsonToken.Undefined:
			case JsonToken.Date:
			case JsonToken.Bytes:
				num = 7;
				break;
			default:
				num = (int)tokenBeingWritten;
				break;
			}
			JsonWriter.State state = JsonWriter.stateArray[num][(int)this._currentState];
			if (state == JsonWriter.State.Error)
			{
				throw new JsonWriterException("Token {0} in state {1} would result in an invalid JavaScript object.".FormatWith(CultureInfo.InvariantCulture, new object[]
				{
					tokenBeingWritten.ToString(),
					this._currentState.ToString()
				}));
			}
			if ((this._currentState == JsonWriter.State.Object || this._currentState == JsonWriter.State.Array || this._currentState == JsonWriter.State.Constructor) && tokenBeingWritten != JsonToken.Comment)
			{
				this.WriteValueDelimiter();
			}
			else if (this._currentState == JsonWriter.State.Property && this._formatting == Formatting.Indented)
			{
				this.WriteIndentSpace();
			}
			WriteState writeState = this.WriteState;
			if ((tokenBeingWritten == JsonToken.PropertyName && writeState != WriteState.Start) || writeState == WriteState.Array || writeState == WriteState.Constructor)
			{
				this.WriteIndent();
			}
			this._currentState = state;
		}

		public virtual void WriteNull()
		{
			this.AutoComplete(JsonToken.Null);
		}

		public virtual void WriteUndefined()
		{
			this.AutoComplete(JsonToken.Undefined);
		}

		public virtual void WriteRaw(string json)
		{
		}

		public virtual void WriteRawValue(string json)
		{
			this.AutoComplete(JsonToken.Undefined);
			this.WriteRaw(json);
		}

		public virtual void WriteValue(string value)
		{
			this.AutoComplete(JsonToken.String);
		}

		public virtual void WriteValue(int value)
		{
			this.AutoComplete(JsonToken.Integer);
		}

		public virtual void WriteValue(uint value)
		{
			this.AutoComplete(JsonToken.Integer);
		}

		public virtual void WriteValue(long value)
		{
			this.AutoComplete(JsonToken.Integer);
		}

		public virtual void WriteValue(ulong value)
		{
			this.AutoComplete(JsonToken.Integer);
		}

		public virtual void WriteValue(float value)
		{
			this.AutoComplete(JsonToken.Float);
		}

		public virtual void WriteValue(double value)
		{
			this.AutoComplete(JsonToken.Float);
		}

		public virtual void WriteValue(bool value)
		{
			this.AutoComplete(JsonToken.Boolean);
		}

		public virtual void WriteValue(short value)
		{
			this.AutoComplete(JsonToken.Integer);
		}

		public virtual void WriteValue(ushort value)
		{
			this.AutoComplete(JsonToken.Integer);
		}

		public virtual void WriteValue(char value)
		{
			this.AutoComplete(JsonToken.String);
		}

		public virtual void WriteValue(byte value)
		{
			this.AutoComplete(JsonToken.Integer);
		}

		public virtual void WriteValue(sbyte value)
		{
			this.AutoComplete(JsonToken.Integer);
		}

		public virtual void WriteValue(decimal value)
		{
			this.AutoComplete(JsonToken.Float);
		}

		public virtual void WriteValue(DateTime value)
		{
			this.AutoComplete(JsonToken.Date);
		}

		public virtual void WriteValue(DateTimeOffset value)
		{
			this.AutoComplete(JsonToken.Date);
		}

		public virtual void WriteValue(Guid value)
		{
			this.AutoComplete(JsonToken.String);
		}

		public virtual void WriteValue(TimeSpan value)
		{
			this.AutoComplete(JsonToken.String);
		}

		public virtual void WriteValue(int? value)
		{
			if (value == null)
			{
				this.WriteNull();
			}
			else
			{
				this.WriteValue(value.Value);
			}
		}

		public virtual void WriteValue(uint? value)
		{
			if (value == null)
			{
				this.WriteNull();
			}
			else
			{
				this.WriteValue(value.Value);
			}
		}

		public virtual void WriteValue(long? value)
		{
			if (value == null)
			{
				this.WriteNull();
			}
			else
			{
				this.WriteValue(value.Value);
			}
		}

		public virtual void WriteValue(ulong? value)
		{
			if (value == null)
			{
				this.WriteNull();
			}
			else
			{
				this.WriteValue(value.Value);
			}
		}

		public virtual void WriteValue(float? value)
		{
			if (value == null)
			{
				this.WriteNull();
			}
			else
			{
				this.WriteValue(value.Value);
			}
		}

		public virtual void WriteValue(double? value)
		{
			if (value == null)
			{
				this.WriteNull();
			}
			else
			{
				this.WriteValue(value.Value);
			}
		}

		public virtual void WriteValue(bool? value)
		{
			if (value == null)
			{
				this.WriteNull();
			}
			else
			{
				this.WriteValue(value.Value);
			}
		}

		public virtual void WriteValue(short? value)
		{
			if (value == null)
			{
				this.WriteNull();
			}
			else
			{
				this.WriteValue(value.Value);
			}
		}

		public virtual void WriteValue(ushort? value)
		{
			if (value == null)
			{
				this.WriteNull();
			}
			else
			{
				this.WriteValue(value.Value);
			}
		}

		public virtual void WriteValue(char? value)
		{
			if (value == null)
			{
				this.WriteNull();
			}
			else
			{
				this.WriteValue(value.Value);
			}
		}

		public virtual void WriteValue(byte? value)
		{
			if (value == null)
			{
				this.WriteNull();
			}
			else
			{
				this.WriteValue(value.Value);
			}
		}

		public virtual void WriteValue(sbyte? value)
		{
			if (value == null)
			{
				this.WriteNull();
			}
			else
			{
				this.WriteValue(value.Value);
			}
		}

		public virtual void WriteValue(decimal? value)
		{
			if (value == null)
			{
				this.WriteNull();
			}
			else
			{
				this.WriteValue(value.Value);
			}
		}

		public virtual void WriteValue(DateTime? value)
		{
			if (value == null)
			{
				this.WriteNull();
			}
			else
			{
				this.WriteValue(value.Value);
			}
		}

		public virtual void WriteValue(DateTimeOffset? value)
		{
			if (value == null)
			{
				this.WriteNull();
			}
			else
			{
				this.WriteValue(value.Value);
			}
		}

		public virtual void WriteValue(Guid? value)
		{
			if (value == null)
			{
				this.WriteNull();
			}
			else
			{
				this.WriteValue(value.Value);
			}
		}

		public virtual void WriteValue(TimeSpan? value)
		{
			if (value == null)
			{
				this.WriteNull();
			}
			else
			{
				this.WriteValue(value.Value);
			}
		}

		public virtual void WriteValue(byte[] value)
		{
			if (value == null)
			{
				this.WriteNull();
			}
			else
			{
				this.AutoComplete(JsonToken.Bytes);
			}
		}

		public virtual void WriteValue(Uri value)
		{
			if (value == null)
			{
				this.WriteNull();
			}
			else
			{
				this.AutoComplete(JsonToken.String);
			}
		}

		public virtual void WriteValue(object value)
		{
			if (value == null)
			{
				this.WriteNull();
				return;
			}
			if (value is IConvertible)
			{
				IConvertible convertible = value as IConvertible;
				switch (convertible.GetTypeCode())
				{
				case TypeCode.DBNull:
					this.WriteNull();
					return;
				case TypeCode.Boolean:
					this.WriteValue(convertible.ToBoolean(CultureInfo.InvariantCulture));
					return;
				case TypeCode.Char:
					this.WriteValue(convertible.ToChar(CultureInfo.InvariantCulture));
					return;
				case TypeCode.SByte:
					this.WriteValue(convertible.ToSByte(CultureInfo.InvariantCulture));
					return;
				case TypeCode.Byte:
					this.WriteValue(convertible.ToByte(CultureInfo.InvariantCulture));
					return;
				case TypeCode.Int16:
					this.WriteValue(convertible.ToInt16(CultureInfo.InvariantCulture));
					return;
				case TypeCode.UInt16:
					this.WriteValue(convertible.ToUInt16(CultureInfo.InvariantCulture));
					return;
				case TypeCode.Int32:
					this.WriteValue(convertible.ToInt32(CultureInfo.InvariantCulture));
					return;
				case TypeCode.UInt32:
					this.WriteValue(convertible.ToUInt32(CultureInfo.InvariantCulture));
					return;
				case TypeCode.Int64:
					this.WriteValue(convertible.ToInt64(CultureInfo.InvariantCulture));
					return;
				case TypeCode.UInt64:
					this.WriteValue(convertible.ToUInt64(CultureInfo.InvariantCulture));
					return;
				case TypeCode.Single:
					this.WriteValue(convertible.ToSingle(CultureInfo.InvariantCulture));
					return;
				case TypeCode.Double:
					this.WriteValue(convertible.ToDouble(CultureInfo.InvariantCulture));
					return;
				case TypeCode.Decimal:
					this.WriteValue(convertible.ToDecimal(CultureInfo.InvariantCulture));
					return;
				case TypeCode.DateTime:
					this.WriteValue(convertible.ToDateTime(CultureInfo.InvariantCulture));
					return;
				case TypeCode.String:
					this.WriteValue(convertible.ToString(CultureInfo.InvariantCulture));
					return;
				}
			}
			else
			{
				if (value is DateTimeOffset)
				{
					this.WriteValue((DateTimeOffset)value);
					return;
				}
				if (value is byte[])
				{
					this.WriteValue((byte[])value);
					return;
				}
				if (value is Guid)
				{
					this.WriteValue((Guid)value);
					return;
				}
				if (value is Uri)
				{
					this.WriteValue((Uri)value);
					return;
				}
				if (value is TimeSpan)
				{
					this.WriteValue((TimeSpan)value);
					return;
				}
			}
			throw new ArgumentException("Unsupported type: {0}. Use the JsonSerializer class to get the object's JSON representation.".FormatWith(CultureInfo.InvariantCulture, new object[]
			{
				value.GetType()
			}));
		}

		public virtual void WriteComment(string text)
		{
			this.AutoComplete(JsonToken.Comment);
		}

		public virtual void WriteWhitespace(string ws)
		{
			if (ws != null && !StringUtils.IsWhiteSpace(ws))
			{
				throw new JsonWriterException("Only white space characters should be used.");
			}
		}

		void IDisposable.Dispose()
		{
			this.Dispose(true);
		}

		private void Dispose(bool disposing)
		{
			if (this.WriteState != WriteState.Closed)
			{
				this.Close();
			}
		}

		private static readonly JsonWriter.State[][] stateArray = new JsonWriter.State[][]
		{
			new JsonWriter.State[]
			{
				JsonWriter.State.Error,
				JsonWriter.State.Error,
				JsonWriter.State.Error,
				JsonWriter.State.Error,
				JsonWriter.State.Error,
				JsonWriter.State.Error,
				JsonWriter.State.Error,
				JsonWriter.State.Error,
				JsonWriter.State.Error,
				JsonWriter.State.Error
			},
			new JsonWriter.State[]
			{
				JsonWriter.State.ObjectStart,
				JsonWriter.State.ObjectStart,
				JsonWriter.State.Error,
				JsonWriter.State.Error,
				JsonWriter.State.ObjectStart,
				JsonWriter.State.ObjectStart,
				JsonWriter.State.ObjectStart,
				JsonWriter.State.ObjectStart,
				JsonWriter.State.Error,
				JsonWriter.State.Error
			},
			new JsonWriter.State[]
			{
				JsonWriter.State.ArrayStart,
				JsonWriter.State.ArrayStart,
				JsonWriter.State.Error,
				JsonWriter.State.Error,
				JsonWriter.State.ArrayStart,
				JsonWriter.State.ArrayStart,
				JsonWriter.State.ArrayStart,
				JsonWriter.State.ArrayStart,
				JsonWriter.State.Error,
				JsonWriter.State.Error
			},
			new JsonWriter.State[]
			{
				JsonWriter.State.ConstructorStart,
				JsonWriter.State.ConstructorStart,
				JsonWriter.State.Error,
				JsonWriter.State.Error,
				JsonWriter.State.ConstructorStart,
				JsonWriter.State.ConstructorStart,
				JsonWriter.State.ConstructorStart,
				JsonWriter.State.ConstructorStart,
				JsonWriter.State.Error,
				JsonWriter.State.Error
			},
			new JsonWriter.State[]
			{
				JsonWriter.State.Property,
				JsonWriter.State.Error,
				JsonWriter.State.Property,
				JsonWriter.State.Property,
				JsonWriter.State.Error,
				JsonWriter.State.Error,
				JsonWriter.State.Error,
				JsonWriter.State.Error,
				JsonWriter.State.Error,
				JsonWriter.State.Error
			},
			new JsonWriter.State[]
			{
				JsonWriter.State.Start,
				JsonWriter.State.Property,
				JsonWriter.State.ObjectStart,
				JsonWriter.State.Object,
				JsonWriter.State.ArrayStart,
				JsonWriter.State.Array,
				JsonWriter.State.Constructor,
				JsonWriter.State.Constructor,
				JsonWriter.State.Error,
				JsonWriter.State.Error
			},
			new JsonWriter.State[]
			{
				JsonWriter.State.Start,
				JsonWriter.State.Property,
				JsonWriter.State.ObjectStart,
				JsonWriter.State.Object,
				JsonWriter.State.ArrayStart,
				JsonWriter.State.Array,
				JsonWriter.State.Constructor,
				JsonWriter.State.Constructor,
				JsonWriter.State.Error,
				JsonWriter.State.Error
			},
			new JsonWriter.State[]
			{
				JsonWriter.State.Start,
				JsonWriter.State.Object,
				JsonWriter.State.Error,
				JsonWriter.State.Error,
				JsonWriter.State.Array,
				JsonWriter.State.Array,
				JsonWriter.State.Constructor,
				JsonWriter.State.Constructor,
				JsonWriter.State.Error,
				JsonWriter.State.Error
			}
		};

		private int _top;

		private readonly List<JTokenType> _stack;

		private JsonWriter.State _currentState;

		private Formatting _formatting;

		private enum State
		{
			Start,
			Property,
			ObjectStart,
			Object,
			ArrayStart,
			Array,
			ConstructorStart,
			Constructor,
			Bytes,
			Closed,
			Error
		}
	}
}
