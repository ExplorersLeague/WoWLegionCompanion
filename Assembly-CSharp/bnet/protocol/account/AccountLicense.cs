using System;
using System.IO;

namespace bnet.protocol.account
{
	public class AccountLicense : IProtoBuf
	{
		public void Deserialize(Stream stream)
		{
			AccountLicense.Deserialize(stream, this);
		}

		public static AccountLicense Deserialize(Stream stream, AccountLicense instance)
		{
			return AccountLicense.Deserialize(stream, instance, -1L);
		}

		public static AccountLicense DeserializeLengthDelimited(Stream stream)
		{
			AccountLicense accountLicense = new AccountLicense();
			AccountLicense.DeserializeLengthDelimited(stream, accountLicense);
			return accountLicense;
		}

		public static AccountLicense DeserializeLengthDelimited(Stream stream, AccountLicense instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return AccountLicense.Deserialize(stream, instance, num);
		}

		public static AccountLicense Deserialize(Stream stream, AccountLicense instance, long limit)
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
				else if (num != 8)
				{
					if (num != 16)
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
						instance.Expires = ProtocolParser.ReadUInt64(stream);
					}
				}
				else
				{
					instance.Id = ProtocolParser.ReadUInt32(stream);
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
			AccountLicense.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, AccountLicense instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt32(stream, instance.Id);
			if (instance.HasExpires)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, instance.Expires);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			num += ProtocolParser.SizeOfUInt32(this.Id);
			if (this.HasExpires)
			{
				num += 1u;
				num += ProtocolParser.SizeOfUInt64(this.Expires);
			}
			return num + 1u;
		}

		public uint Id { get; set; }

		public void SetId(uint val)
		{
			this.Id = val;
		}

		public ulong Expires
		{
			get
			{
				return this._Expires;
			}
			set
			{
				this._Expires = value;
				this.HasExpires = true;
			}
		}

		public void SetExpires(ulong val)
		{
			this.Expires = val;
		}

		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.Id.GetHashCode();
			if (this.HasExpires)
			{
				num ^= this.Expires.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			AccountLicense accountLicense = obj as AccountLicense;
			return accountLicense != null && this.Id.Equals(accountLicense.Id) && this.HasExpires == accountLicense.HasExpires && (!this.HasExpires || this.Expires.Equals(accountLicense.Expires));
		}

		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		public static AccountLicense ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<AccountLicense>(bs, 0, -1);
		}

		public bool HasExpires;

		private ulong _Expires;
	}
}
