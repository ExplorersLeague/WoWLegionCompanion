using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Bson
{
	public class BsonReader : JsonReader
	{
		public BsonReader(Stream stream) : this(stream, false, DateTimeKind.Local)
		{
		}

		public BsonReader(Stream stream, bool readRootValueAsArray, DateTimeKind dateTimeKindHandling)
		{
			ValidationUtils.ArgumentNotNull(stream, "stream");
			this._reader = new BinaryReader(stream);
			this._stack = new List<BsonReader.ContainerContext>();
			this._readRootValueAsArray = readRootValueAsArray;
			this._dateTimeKindHandling = dateTimeKindHandling;
		}

		public bool JsonNet35BinaryCompatibility
		{
			get
			{
				return this._jsonNet35BinaryCompatibility;
			}
			set
			{
				this._jsonNet35BinaryCompatibility = value;
			}
		}

		public bool ReadRootValueAsArray
		{
			get
			{
				return this._readRootValueAsArray;
			}
			set
			{
				this._readRootValueAsArray = value;
			}
		}

		public DateTimeKind DateTimeKindHandling
		{
			get
			{
				return this._dateTimeKindHandling;
			}
			set
			{
				this._dateTimeKindHandling = value;
			}
		}

		private string ReadElement()
		{
			this._currentElementType = this.ReadType();
			return this.ReadString();
		}

		public override byte[] ReadAsBytes()
		{
			this.Read();
			if (this.IsWrappedInTypeObject())
			{
				byte[] array = this.ReadAsBytes();
				this.Read();
				this.SetToken(JsonToken.Bytes, array);
				return array;
			}
			if (this.TokenType == JsonToken.Null)
			{
				return null;
			}
			if (this.TokenType == JsonToken.Bytes)
			{
				return (byte[])this.Value;
			}
			throw new JsonReaderException("Error reading bytes. Expected bytes but got {0}.".FormatWith(CultureInfo.InvariantCulture, new object[]
			{
				this.TokenType
			}));
		}

		private bool IsWrappedInTypeObject()
		{
			if (this.TokenType == JsonToken.StartObject)
			{
				this.Read();
				if (this.Value.ToString() == "$type")
				{
					this.Read();
					if (this.Value != null && this.Value.ToString().StartsWith("System.Byte[]"))
					{
						this.Read();
						if (this.Value.ToString() == "$value")
						{
							return true;
						}
					}
				}
				throw new JsonReaderException("Unexpected token when reading bytes: {0}.".FormatWith(CultureInfo.InvariantCulture, new object[]
				{
					JsonToken.StartObject
				}));
			}
			return false;
		}

		public override decimal? ReadAsDecimal()
		{
			this.Read();
			if (this.TokenType == JsonToken.Null)
			{
				return null;
			}
			if (this.TokenType == JsonToken.Integer || this.TokenType == JsonToken.Float)
			{
				this.SetToken(JsonToken.Float, Convert.ToDecimal(this.Value, CultureInfo.InvariantCulture));
				return new decimal?((decimal)this.Value);
			}
			throw new JsonReaderException("Error reading decimal. Expected a number but got {0}.".FormatWith(CultureInfo.InvariantCulture, new object[]
			{
				this.TokenType
			}));
		}

		public override DateTimeOffset? ReadAsDateTimeOffset()
		{
			this.Read();
			if (this.TokenType == JsonToken.Null)
			{
				return null;
			}
			if (this.TokenType == JsonToken.Date)
			{
				this.SetToken(JsonToken.Date, new DateTimeOffset((DateTime)this.Value));
				return new DateTimeOffset?((DateTimeOffset)this.Value);
			}
			throw new JsonReaderException("Error reading date. Expected bytes but got {0}.".FormatWith(CultureInfo.InvariantCulture, new object[]
			{
				this.TokenType
			}));
		}

		public override bool Read()
		{
			bool result;
			try
			{
				switch (this._bsonReaderState)
				{
				case BsonReader.BsonReaderState.Normal:
					result = this.ReadNormal();
					break;
				case BsonReader.BsonReaderState.ReferenceStart:
				case BsonReader.BsonReaderState.ReferenceRef:
				case BsonReader.BsonReaderState.ReferenceId:
					result = this.ReadReference();
					break;
				case BsonReader.BsonReaderState.CodeWScopeStart:
				case BsonReader.BsonReaderState.CodeWScopeCode:
				case BsonReader.BsonReaderState.CodeWScopeScope:
				case BsonReader.BsonReaderState.CodeWScopeScopeObject:
				case BsonReader.BsonReaderState.CodeWScopeScopeEnd:
					result = this.ReadCodeWScope();
					break;
				default:
					throw new JsonReaderException("Unexpected state: {0}".FormatWith(CultureInfo.InvariantCulture, new object[]
					{
						this._bsonReaderState
					}));
				}
			}
			catch (EndOfStreamException)
			{
				result = false;
			}
			return result;
		}

		public override void Close()
		{
			base.Close();
			if (base.CloseInput && this._reader != null)
			{
				this._reader.Close();
			}
		}

		private bool ReadCodeWScope()
		{
			switch (this._bsonReaderState)
			{
			case BsonReader.BsonReaderState.CodeWScopeStart:
				this.SetToken(JsonToken.PropertyName, "$code");
				this._bsonReaderState = BsonReader.BsonReaderState.CodeWScopeCode;
				return true;
			case BsonReader.BsonReaderState.CodeWScopeCode:
				this.ReadInt32();
				this.SetToken(JsonToken.String, this.ReadLengthString());
				this._bsonReaderState = BsonReader.BsonReaderState.CodeWScopeScope;
				return true;
			case BsonReader.BsonReaderState.CodeWScopeScope:
			{
				if (base.CurrentState == JsonReader.State.PostValue)
				{
					this.SetToken(JsonToken.PropertyName, "$scope");
					return true;
				}
				base.SetToken(JsonToken.StartObject);
				this._bsonReaderState = BsonReader.BsonReaderState.CodeWScopeScopeObject;
				BsonReader.ContainerContext containerContext = new BsonReader.ContainerContext(BsonType.Object);
				this.PushContext(containerContext);
				containerContext.Length = this.ReadInt32();
				return true;
			}
			case BsonReader.BsonReaderState.CodeWScopeScopeObject:
			{
				bool flag = this.ReadNormal();
				if (flag && this.TokenType == JsonToken.EndObject)
				{
					this._bsonReaderState = BsonReader.BsonReaderState.CodeWScopeScopeEnd;
				}
				return flag;
			}
			case BsonReader.BsonReaderState.CodeWScopeScopeEnd:
				base.SetToken(JsonToken.EndObject);
				this._bsonReaderState = BsonReader.BsonReaderState.Normal;
				return true;
			default:
				throw new ArgumentOutOfRangeException();
			}
		}

		private bool ReadReference()
		{
			JsonReader.State currentState = base.CurrentState;
			if (currentState != JsonReader.State.Property)
			{
				if (currentState == JsonReader.State.ObjectStart)
				{
					this.SetToken(JsonToken.PropertyName, "$ref");
					this._bsonReaderState = BsonReader.BsonReaderState.ReferenceRef;
					return true;
				}
				if (currentState != JsonReader.State.PostValue)
				{
					throw new JsonReaderException("Unexpected state when reading BSON reference: " + base.CurrentState);
				}
				if (this._bsonReaderState == BsonReader.BsonReaderState.ReferenceRef)
				{
					this.SetToken(JsonToken.PropertyName, "$id");
					this._bsonReaderState = BsonReader.BsonReaderState.ReferenceId;
					return true;
				}
				if (this._bsonReaderState == BsonReader.BsonReaderState.ReferenceId)
				{
					base.SetToken(JsonToken.EndObject);
					this._bsonReaderState = BsonReader.BsonReaderState.Normal;
					return true;
				}
				throw new JsonReaderException("Unexpected state when reading BSON reference: " + this._bsonReaderState);
			}
			else
			{
				if (this._bsonReaderState == BsonReader.BsonReaderState.ReferenceRef)
				{
					this.SetToken(JsonToken.String, this.ReadLengthString());
					return true;
				}
				if (this._bsonReaderState == BsonReader.BsonReaderState.ReferenceId)
				{
					this.SetToken(JsonToken.Bytes, this.ReadBytes(12));
					return true;
				}
				throw new JsonReaderException("Unexpected state when reading BSON reference: " + this._bsonReaderState);
			}
		}

		private bool ReadNormal()
		{
			switch (base.CurrentState)
			{
			case JsonReader.State.Start:
			{
				JsonToken token = this._readRootValueAsArray ? JsonToken.StartArray : JsonToken.StartObject;
				BsonType type = this._readRootValueAsArray ? BsonType.Array : BsonType.Object;
				base.SetToken(token);
				BsonReader.ContainerContext containerContext = new BsonReader.ContainerContext(type);
				this.PushContext(containerContext);
				containerContext.Length = this.ReadInt32();
				return true;
			}
			case JsonReader.State.Complete:
			case JsonReader.State.Closed:
				return false;
			case JsonReader.State.Property:
				this.ReadType(this._currentElementType);
				return true;
			case JsonReader.State.ObjectStart:
			case JsonReader.State.ArrayStart:
			case JsonReader.State.PostValue:
			{
				BsonReader.ContainerContext currentContext = this._currentContext;
				if (currentContext == null)
				{
					return false;
				}
				int num = currentContext.Length - 1;
				if (currentContext.Position < num)
				{
					if (currentContext.Type == BsonType.Array)
					{
						this.ReadElement();
						this.ReadType(this._currentElementType);
						return true;
					}
					this.SetToken(JsonToken.PropertyName, this.ReadElement());
					return true;
				}
				else
				{
					if (currentContext.Position != num)
					{
						throw new JsonReaderException("Read past end of current container context.");
					}
					if (this.ReadByte() != 0)
					{
						throw new JsonReaderException("Unexpected end of object byte value.");
					}
					this.PopContext();
					if (this._currentContext != null)
					{
						this.MovePosition(currentContext.Length);
					}
					JsonToken token2 = (currentContext.Type != BsonType.Object) ? JsonToken.EndArray : JsonToken.EndObject;
					base.SetToken(token2);
					return true;
				}
				break;
			}
			case JsonReader.State.ConstructorStart:
				return false;
			case JsonReader.State.Constructor:
				return false;
			case JsonReader.State.Error:
				return false;
			case JsonReader.State.Finished:
				return false;
			}
			throw new ArgumentOutOfRangeException();
		}

		private void PopContext()
		{
			this._stack.RemoveAt(this._stack.Count - 1);
			if (this._stack.Count == 0)
			{
				this._currentContext = null;
			}
			else
			{
				this._currentContext = this._stack[this._stack.Count - 1];
			}
		}

		private void PushContext(BsonReader.ContainerContext newContext)
		{
			this._stack.Add(newContext);
			this._currentContext = newContext;
		}

		private byte ReadByte()
		{
			this.MovePosition(1);
			return this._reader.ReadByte();
		}

		private void ReadType(BsonType type)
		{
			switch (type)
			{
			case BsonType.Number:
				this.SetToken(JsonToken.Float, this.ReadDouble());
				break;
			case BsonType.String:
			case BsonType.Symbol:
				this.SetToken(JsonToken.String, this.ReadLengthString());
				break;
			case BsonType.Object:
			{
				base.SetToken(JsonToken.StartObject);
				BsonReader.ContainerContext containerContext = new BsonReader.ContainerContext(BsonType.Object);
				this.PushContext(containerContext);
				containerContext.Length = this.ReadInt32();
				break;
			}
			case BsonType.Array:
			{
				base.SetToken(JsonToken.StartArray);
				BsonReader.ContainerContext containerContext2 = new BsonReader.ContainerContext(BsonType.Array);
				this.PushContext(containerContext2);
				containerContext2.Length = this.ReadInt32();
				break;
			}
			case BsonType.Binary:
				this.SetToken(JsonToken.Bytes, this.ReadBinary());
				break;
			case BsonType.Undefined:
				base.SetToken(JsonToken.Undefined);
				break;
			case BsonType.Oid:
			{
				byte[] value = this.ReadBytes(12);
				this.SetToken(JsonToken.Bytes, value);
				break;
			}
			case BsonType.Boolean:
			{
				bool flag = Convert.ToBoolean(this.ReadByte());
				this.SetToken(JsonToken.Boolean, flag);
				break;
			}
			case BsonType.Date:
			{
				long javaScriptTicks = this.ReadInt64();
				DateTime dateTime = JsonConvert.ConvertJavaScriptTicksToDateTime(javaScriptTicks);
				DateTimeKind dateTimeKindHandling = this.DateTimeKindHandling;
				DateTime dateTime2;
				if (dateTimeKindHandling != DateTimeKind.Unspecified)
				{
					if (dateTimeKindHandling != DateTimeKind.Local)
					{
						dateTime2 = dateTime;
					}
					else
					{
						dateTime2 = dateTime.ToLocalTime();
					}
				}
				else
				{
					dateTime2 = DateTime.SpecifyKind(dateTime, DateTimeKind.Unspecified);
				}
				this.SetToken(JsonToken.Date, dateTime2);
				break;
			}
			case BsonType.Null:
				base.SetToken(JsonToken.Null);
				break;
			case BsonType.Regex:
			{
				string str = this.ReadString();
				string str2 = this.ReadString();
				string value2 = "/" + str + "/" + str2;
				this.SetToken(JsonToken.String, value2);
				break;
			}
			case BsonType.Reference:
				base.SetToken(JsonToken.StartObject);
				this._bsonReaderState = BsonReader.BsonReaderState.ReferenceStart;
				break;
			case BsonType.Code:
				this.SetToken(JsonToken.String, this.ReadLengthString());
				break;
			case BsonType.CodeWScope:
				base.SetToken(JsonToken.StartObject);
				this._bsonReaderState = BsonReader.BsonReaderState.CodeWScopeStart;
				break;
			case BsonType.Integer:
				this.SetToken(JsonToken.Integer, (long)this.ReadInt32());
				break;
			case BsonType.TimeStamp:
			case BsonType.Long:
				this.SetToken(JsonToken.Integer, this.ReadInt64());
				break;
			default:
				throw new ArgumentOutOfRangeException("type", "Unexpected BsonType value: " + type);
			}
		}

		private byte[] ReadBinary()
		{
			int count = this.ReadInt32();
			BsonBinaryType bsonBinaryType = (BsonBinaryType)this.ReadByte();
			if (bsonBinaryType == BsonBinaryType.Data && !this._jsonNet35BinaryCompatibility)
			{
				count = this.ReadInt32();
			}
			return this.ReadBytes(count);
		}

		private string ReadString()
		{
			this.EnsureBuffers();
			StringBuilder stringBuilder = null;
			int num = 0;
			int num2 = 0;
			int num4;
			for (;;)
			{
				int num3 = num2;
				byte b;
				while (num3 < 128 && (b = this._reader.ReadByte()) > 0)
				{
					this._byteBuffer[num3++] = b;
				}
				num4 = num3 - num2;
				num += num4;
				if (num3 < 128 && stringBuilder == null)
				{
					break;
				}
				int lastFullCharStop = this.GetLastFullCharStop(num3 - 1);
				int chars = Encoding.UTF8.GetChars(this._byteBuffer, 0, lastFullCharStop + 1, this._charBuffer, 0);
				if (stringBuilder == null)
				{
					stringBuilder = new StringBuilder(256);
				}
				stringBuilder.Append(this._charBuffer, 0, chars);
				if (lastFullCharStop < num4 - 1)
				{
					num2 = num4 - lastFullCharStop - 1;
					Array.Copy(this._byteBuffer, lastFullCharStop + 1, this._byteBuffer, 0, num2);
				}
				else
				{
					if (num3 < 128)
					{
						goto Block_6;
					}
					num2 = 0;
				}
			}
			int chars2 = Encoding.UTF8.GetChars(this._byteBuffer, 0, num4, this._charBuffer, 0);
			this.MovePosition(num + 1);
			return new string(this._charBuffer, 0, chars2);
			Block_6:
			this.MovePosition(num + 1);
			return stringBuilder.ToString();
		}

		private string ReadLengthString()
		{
			int num = this.ReadInt32();
			this.MovePosition(num);
			string @string = this.GetString(num - 1);
			this._reader.ReadByte();
			return @string;
		}

		private string GetString(int length)
		{
			if (length == 0)
			{
				return string.Empty;
			}
			this.EnsureBuffers();
			StringBuilder stringBuilder = null;
			int num = 0;
			int num2 = 0;
			int num3;
			for (;;)
			{
				int count = (length - num <= 128 - num2) ? (length - num) : (128 - num2);
				num3 = this._reader.BaseStream.Read(this._byteBuffer, num2, count);
				if (num3 == 0)
				{
					break;
				}
				num += num3;
				num3 += num2;
				if (num3 == length)
				{
					goto Block_4;
				}
				int lastFullCharStop = this.GetLastFullCharStop(num3 - 1);
				if (stringBuilder == null)
				{
					stringBuilder = new StringBuilder(length);
				}
				int chars = Encoding.UTF8.GetChars(this._byteBuffer, 0, lastFullCharStop + 1, this._charBuffer, 0);
				stringBuilder.Append(this._charBuffer, 0, chars);
				if (lastFullCharStop < num3 - 1)
				{
					num2 = num3 - lastFullCharStop - 1;
					Array.Copy(this._byteBuffer, lastFullCharStop + 1, this._byteBuffer, 0, num2);
				}
				else
				{
					num2 = 0;
				}
				if (num >= length)
				{
					goto Block_7;
				}
			}
			throw new EndOfStreamException("Unable to read beyond the end of the stream.");
			Block_4:
			int chars2 = Encoding.UTF8.GetChars(this._byteBuffer, 0, num3, this._charBuffer, 0);
			return new string(this._charBuffer, 0, chars2);
			Block_7:
			return stringBuilder.ToString();
		}

		private int GetLastFullCharStop(int start)
		{
			int i = start;
			int num = 0;
			while (i >= 0)
			{
				num = this.BytesInSequence(this._byteBuffer[i]);
				if (num == 0)
				{
					i--;
				}
				else
				{
					if (num == 1)
					{
						break;
					}
					i--;
					break;
				}
			}
			if (num == start - i)
			{
				return start;
			}
			return i;
		}

		private int BytesInSequence(byte b)
		{
			if (b <= BsonReader._seqRange1[1])
			{
				return 1;
			}
			if (b >= BsonReader._seqRange2[0] && b <= BsonReader._seqRange2[1])
			{
				return 2;
			}
			if (b >= BsonReader._seqRange3[0] && b <= BsonReader._seqRange3[1])
			{
				return 3;
			}
			if (b >= BsonReader._seqRange4[0] && b <= BsonReader._seqRange4[1])
			{
				return 4;
			}
			return 0;
		}

		private void EnsureBuffers()
		{
			if (this._byteBuffer == null)
			{
				this._byteBuffer = new byte[128];
			}
			if (this._charBuffer == null)
			{
				int maxCharCount = Encoding.UTF8.GetMaxCharCount(128);
				this._charBuffer = new char[maxCharCount];
			}
		}

		private double ReadDouble()
		{
			this.MovePosition(8);
			return this._reader.ReadDouble();
		}

		private int ReadInt32()
		{
			this.MovePosition(4);
			return this._reader.ReadInt32();
		}

		private long ReadInt64()
		{
			this.MovePosition(8);
			return this._reader.ReadInt64();
		}

		private BsonType ReadType()
		{
			this.MovePosition(1);
			return (BsonType)this._reader.ReadSByte();
		}

		private void MovePosition(int count)
		{
			this._currentContext.Position += count;
		}

		private byte[] ReadBytes(int count)
		{
			this.MovePosition(count);
			return this._reader.ReadBytes(count);
		}

		private const int MaxCharBytesSize = 128;

		private static readonly byte[] _seqRange1 = new byte[]
		{
			0,
			127
		};

		private static readonly byte[] _seqRange2 = new byte[]
		{
			194,
			223
		};

		private static readonly byte[] _seqRange3 = new byte[]
		{
			224,
			239
		};

		private static readonly byte[] _seqRange4 = new byte[]
		{
			240,
			244
		};

		private readonly BinaryReader _reader;

		private readonly List<BsonReader.ContainerContext> _stack;

		private byte[] _byteBuffer;

		private char[] _charBuffer;

		private BsonType _currentElementType;

		private BsonReader.BsonReaderState _bsonReaderState;

		private BsonReader.ContainerContext _currentContext;

		private bool _readRootValueAsArray;

		private bool _jsonNet35BinaryCompatibility;

		private DateTimeKind _dateTimeKindHandling;

		private enum BsonReaderState
		{
			Normal,
			ReferenceStart,
			ReferenceRef,
			ReferenceId,
			CodeWScopeStart,
			CodeWScopeCode,
			CodeWScopeScope,
			CodeWScopeScopeObject,
			CodeWScopeScopeEnd
		}

		private class ContainerContext
		{
			public ContainerContext(BsonType type)
			{
				this.Type = type;
			}

			public readonly BsonType Type;

			public int Length;

			public int Position;
		}
	}
}
