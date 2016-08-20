using System;
using System.Globalization;
using System.IO;
using System.Text;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Bson
{
	internal class BsonBinaryWriter
	{
		public BsonBinaryWriter(Stream stream)
		{
			this.DateTimeKindHandling = DateTimeKind.Utc;
			this._writer = new BinaryWriter(stream);
		}

		public DateTimeKind DateTimeKindHandling { get; set; }

		public void Flush()
		{
			this._writer.Flush();
		}

		public void Close()
		{
			this._writer.Close();
		}

		public void WriteToken(BsonToken t)
		{
			this.CalculateSize(t);
			this.WriteTokenInternal(t);
		}

		private void WriteTokenInternal(BsonToken t)
		{
			switch (t.Type)
			{
			case BsonType.Number:
			{
				BsonValue bsonValue = (BsonValue)t;
				this._writer.Write(Convert.ToDouble(bsonValue.Value, CultureInfo.InvariantCulture));
				return;
			}
			case BsonType.String:
			{
				BsonString bsonString = (BsonString)t;
				this.WriteString((string)bsonString.Value, bsonString.ByteCount, new int?(bsonString.CalculatedSize - 4));
				return;
			}
			case BsonType.Object:
			{
				BsonObject bsonObject = (BsonObject)t;
				this._writer.Write(bsonObject.CalculatedSize);
				foreach (BsonProperty bsonProperty in bsonObject)
				{
					this._writer.Write((sbyte)bsonProperty.Value.Type);
					this.WriteString((string)bsonProperty.Name.Value, bsonProperty.Name.ByteCount, null);
					this.WriteTokenInternal(bsonProperty.Value);
				}
				this._writer.Write(0);
				return;
			}
			case BsonType.Array:
			{
				BsonArray bsonArray = (BsonArray)t;
				this._writer.Write(bsonArray.CalculatedSize);
				int num = 0;
				foreach (BsonToken bsonToken in bsonArray)
				{
					this._writer.Write((sbyte)bsonToken.Type);
					this.WriteString(num.ToString(CultureInfo.InvariantCulture), MathUtils.IntLength(num), null);
					this.WriteTokenInternal(bsonToken);
					num++;
				}
				this._writer.Write(0);
				return;
			}
			case BsonType.Binary:
			{
				BsonValue bsonValue2 = (BsonValue)t;
				byte[] array = (byte[])bsonValue2.Value;
				this._writer.Write(array.Length);
				this._writer.Write(0);
				this._writer.Write(array);
				return;
			}
			case BsonType.Undefined:
			case BsonType.Null:
				return;
			case BsonType.Oid:
			{
				BsonValue bsonValue3 = (BsonValue)t;
				byte[] buffer = (byte[])bsonValue3.Value;
				this._writer.Write(buffer);
				return;
			}
			case BsonType.Boolean:
			{
				BsonValue bsonValue4 = (BsonValue)t;
				this._writer.Write((bool)bsonValue4.Value);
				return;
			}
			case BsonType.Date:
			{
				BsonValue bsonValue5 = (BsonValue)t;
				long value;
				if (bsonValue5.Value is DateTime)
				{
					DateTime dateTime = (DateTime)bsonValue5.Value;
					if (this.DateTimeKindHandling == DateTimeKind.Utc)
					{
						dateTime = dateTime.ToUniversalTime();
					}
					else if (this.DateTimeKindHandling == DateTimeKind.Local)
					{
						dateTime = dateTime.ToLocalTime();
					}
					value = JsonConvert.ConvertDateTimeToJavaScriptTicks(dateTime, false);
				}
				else
				{
					DateTimeOffset dateTimeOffset = (DateTimeOffset)bsonValue5.Value;
					value = JsonConvert.ConvertDateTimeToJavaScriptTicks(dateTimeOffset.UtcDateTime, dateTimeOffset.Offset);
				}
				this._writer.Write(value);
				return;
			}
			case BsonType.Regex:
			{
				BsonRegex bsonRegex = (BsonRegex)t;
				this.WriteString((string)bsonRegex.Pattern.Value, bsonRegex.Pattern.ByteCount, null);
				this.WriteString((string)bsonRegex.Options.Value, bsonRegex.Options.ByteCount, null);
				return;
			}
			case BsonType.Integer:
			{
				BsonValue bsonValue6 = (BsonValue)t;
				this._writer.Write(Convert.ToInt32(bsonValue6.Value, CultureInfo.InvariantCulture));
				return;
			}
			case BsonType.Long:
			{
				BsonValue bsonValue7 = (BsonValue)t;
				this._writer.Write(Convert.ToInt64(bsonValue7.Value, CultureInfo.InvariantCulture));
				return;
			}
			}
			throw new ArgumentOutOfRangeException("t", "Unexpected token when writing BSON: {0}".FormatWith(CultureInfo.InvariantCulture, new object[]
			{
				t.Type
			}));
		}

		private void WriteString(string s, int byteCount, int? calculatedlengthPrefix)
		{
			if (calculatedlengthPrefix != null)
			{
				this._writer.Write(calculatedlengthPrefix.Value);
			}
			if (s != null)
			{
				if (this._largeByteBuffer == null)
				{
					this._largeByteBuffer = new byte[256];
					this._maxChars = 256 / BsonBinaryWriter.Encoding.GetMaxByteCount(1);
				}
				if (byteCount <= 256)
				{
					BsonBinaryWriter.Encoding.GetBytes(s, 0, s.Length, this._largeByteBuffer, 0);
					this._writer.Write(this._largeByteBuffer, 0, byteCount);
				}
				else
				{
					int num = 0;
					int num2;
					for (int i = s.Length; i > 0; i -= num2)
					{
						num2 = ((i <= this._maxChars) ? i : this._maxChars);
						int bytes = BsonBinaryWriter.Encoding.GetBytes(s, num, num2, this._largeByteBuffer, 0);
						this._writer.Write(this._largeByteBuffer, 0, bytes);
						num += num2;
					}
				}
			}
			this._writer.Write(0);
		}

		private int CalculateSize(int stringByteCount)
		{
			return stringByteCount + 1;
		}

		private int CalculateSizeWithLength(int stringByteCount, bool includeSize)
		{
			int num = (!includeSize) ? 1 : 5;
			return num + stringByteCount;
		}

		private int CalculateSize(BsonToken t)
		{
			switch (t.Type)
			{
			case BsonType.Number:
				return 8;
			case BsonType.String:
			{
				BsonString bsonString = (BsonString)t;
				string text = (string)bsonString.Value;
				bsonString.ByteCount = ((text == null) ? 0 : BsonBinaryWriter.Encoding.GetByteCount(text));
				bsonString.CalculatedSize = this.CalculateSizeWithLength(bsonString.ByteCount, bsonString.IncludeLength);
				return bsonString.CalculatedSize;
			}
			case BsonType.Object:
			{
				BsonObject bsonObject = (BsonObject)t;
				int num = 4;
				foreach (BsonProperty bsonProperty in bsonObject)
				{
					int num2 = 1;
					num2 += this.CalculateSize(bsonProperty.Name);
					num2 += this.CalculateSize(bsonProperty.Value);
					num += num2;
				}
				num++;
				bsonObject.CalculatedSize = num;
				return num;
			}
			case BsonType.Array:
			{
				BsonArray bsonArray = (BsonArray)t;
				int num3 = 4;
				int num4 = 0;
				foreach (BsonToken t2 in bsonArray)
				{
					num3++;
					num3 += this.CalculateSize(MathUtils.IntLength(num4));
					num3 += this.CalculateSize(t2);
					num4++;
				}
				num3++;
				bsonArray.CalculatedSize = num3;
				return bsonArray.CalculatedSize;
			}
			case BsonType.Binary:
			{
				BsonValue bsonValue = (BsonValue)t;
				byte[] array = (byte[])bsonValue.Value;
				bsonValue.CalculatedSize = 5 + array.Length;
				return bsonValue.CalculatedSize;
			}
			case BsonType.Undefined:
			case BsonType.Null:
				return 0;
			case BsonType.Oid:
				return 12;
			case BsonType.Boolean:
				return 1;
			case BsonType.Date:
				return 8;
			case BsonType.Regex:
			{
				BsonRegex bsonRegex = (BsonRegex)t;
				int num5 = 0;
				num5 += this.CalculateSize(bsonRegex.Pattern);
				num5 += this.CalculateSize(bsonRegex.Options);
				bsonRegex.CalculatedSize = num5;
				return bsonRegex.CalculatedSize;
			}
			case BsonType.Integer:
				return 4;
			case BsonType.Long:
				return 8;
			}
			throw new ArgumentOutOfRangeException("t", "Unexpected token when writing BSON: {0}".FormatWith(CultureInfo.InvariantCulture, new object[]
			{
				t.Type
			}));
		}

		private static readonly Encoding Encoding = Encoding.UTF8;

		private readonly BinaryWriter _writer;

		private byte[] _largeByteBuffer;

		private int _maxChars;
	}
}
