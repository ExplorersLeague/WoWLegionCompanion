using System;
using System.IO;
using System.Text;

namespace bnet.protocol.account
{
	public class CurrencyRestriction : IProtoBuf
	{
		public string Currency { get; set; }

		public void SetCurrency(string val)
		{
			this.Currency = val;
		}

		public string AuthenticatorCap { get; set; }

		public void SetAuthenticatorCap(string val)
		{
			this.AuthenticatorCap = val;
		}

		public string SoftCap { get; set; }

		public void SetSoftCap(string val)
		{
			this.SoftCap = val;
		}

		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.Currency.GetHashCode();
			num ^= this.AuthenticatorCap.GetHashCode();
			return num ^ this.SoftCap.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			CurrencyRestriction currencyRestriction = obj as CurrencyRestriction;
			return currencyRestriction != null && this.Currency.Equals(currencyRestriction.Currency) && this.AuthenticatorCap.Equals(currencyRestriction.AuthenticatorCap) && this.SoftCap.Equals(currencyRestriction.SoftCap);
		}

		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		public static CurrencyRestriction ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<CurrencyRestriction>(bs, 0, -1);
		}

		public void Deserialize(Stream stream)
		{
			CurrencyRestriction.Deserialize(stream, this);
		}

		public static CurrencyRestriction Deserialize(Stream stream, CurrencyRestriction instance)
		{
			return CurrencyRestriction.Deserialize(stream, instance, -1L);
		}

		public static CurrencyRestriction DeserializeLengthDelimited(Stream stream)
		{
			CurrencyRestriction currencyRestriction = new CurrencyRestriction();
			CurrencyRestriction.DeserializeLengthDelimited(stream, currencyRestriction);
			return currencyRestriction;
		}

		public static CurrencyRestriction DeserializeLengthDelimited(Stream stream, CurrencyRestriction instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return CurrencyRestriction.Deserialize(stream, instance, num);
		}

		public static CurrencyRestriction Deserialize(Stream stream, CurrencyRestriction instance, long limit)
		{
			while (limit < 0L || stream.Position < limit)
			{
				int num = stream.ReadByte();
				if (num == -1)
				{
					if (limit >= 0L)
					{
						throw new EndOfStreamException();
					}
					return instance;
				}
				else if (num != 10)
				{
					if (num != 18)
					{
						if (num != 26)
						{
							Key key = ProtocolParser.ReadKey((byte)num, stream);
							uint field = key.Field;
							if (field == 0u)
							{
								throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
							}
							ProtocolParser.SkipKey(stream, key);
						}
						else
						{
							instance.SoftCap = ProtocolParser.ReadString(stream);
						}
					}
					else
					{
						instance.AuthenticatorCap = ProtocolParser.ReadString(stream);
					}
				}
				else
				{
					instance.Currency = ProtocolParser.ReadString(stream);
				}
			}
			if (stream.Position == limit)
			{
				return instance;
			}
			throw new ProtocolBufferException("Read past max limit");
		}

		public void Serialize(Stream stream)
		{
			CurrencyRestriction.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, CurrencyRestriction instance)
		{
			if (instance.Currency == null)
			{
				throw new ArgumentNullException("Currency", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Currency));
			if (instance.AuthenticatorCap == null)
			{
				throw new ArgumentNullException("AuthenticatorCap", "Required by proto specification.");
			}
			stream.WriteByte(18);
			ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.AuthenticatorCap));
			if (instance.SoftCap == null)
			{
				throw new ArgumentNullException("SoftCap", "Required by proto specification.");
			}
			stream.WriteByte(26);
			ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.SoftCap));
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.Currency);
			num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(this.AuthenticatorCap);
			num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			uint byteCount3 = (uint)Encoding.UTF8.GetByteCount(this.SoftCap);
			num += ProtocolParser.SizeOfUInt32(byteCount3) + byteCount3;
			return num + 3u;
		}
	}
}
