using System;
using System.IO;

namespace bnet.protocol.account
{
	public class AccountCredential : IProtoBuf
	{
		public void Deserialize(Stream stream)
		{
			AccountCredential.Deserialize(stream, this);
		}

		public static AccountCredential Deserialize(Stream stream, AccountCredential instance)
		{
			return AccountCredential.Deserialize(stream, instance, -1L);
		}

		public static AccountCredential DeserializeLengthDelimited(Stream stream)
		{
			AccountCredential accountCredential = new AccountCredential();
			AccountCredential.DeserializeLengthDelimited(stream, accountCredential);
			return accountCredential;
		}

		public static AccountCredential DeserializeLengthDelimited(Stream stream, AccountCredential instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return AccountCredential.Deserialize(stream, instance, num);
		}

		public static AccountCredential Deserialize(Stream stream, AccountCredential instance, long limit)
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
				else
				{
					int num2 = num;
					if (num2 != 8)
					{
						if (num2 != 18)
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
							instance.Data = ProtocolParser.ReadBytes(stream);
						}
					}
					else
					{
						instance.Id = ProtocolParser.ReadUInt32(stream);
					}
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
			AccountCredential.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, AccountCredential instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt32(stream, instance.Id);
			if (instance.HasData)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteBytes(stream, instance.Data);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			num += ProtocolParser.SizeOfUInt32(this.Id);
			if (this.HasData)
			{
				num += 1u;
				num += ProtocolParser.SizeOfUInt32(this.Data.Length) + (uint)this.Data.Length;
			}
			return num + 1u;
		}

		public uint Id { get; set; }

		public void SetId(uint val)
		{
			this.Id = val;
		}

		public byte[] Data
		{
			get
			{
				return this._Data;
			}
			set
			{
				this._Data = value;
				this.HasData = (value != null);
			}
		}

		public void SetData(byte[] val)
		{
			this.Data = val;
		}

		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.Id.GetHashCode();
			if (this.HasData)
			{
				num ^= this.Data.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			AccountCredential accountCredential = obj as AccountCredential;
			return accountCredential != null && this.Id.Equals(accountCredential.Id) && this.HasData == accountCredential.HasData && (!this.HasData || this.Data.Equals(accountCredential.Data));
		}

		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		public static AccountCredential ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<AccountCredential>(bs, 0, -1);
		}

		public bool HasData;

		private byte[] _Data;
	}
}
