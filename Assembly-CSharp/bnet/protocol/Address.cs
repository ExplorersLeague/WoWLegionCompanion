using System;
using System.IO;
using System.Text;

namespace bnet.protocol
{
	public class Address : IProtoBuf
	{
		public void Deserialize(Stream stream)
		{
			Address.Deserialize(stream, this);
		}

		public static Address Deserialize(Stream stream, Address instance)
		{
			return Address.Deserialize(stream, instance, -1L);
		}

		public static Address DeserializeLengthDelimited(Stream stream)
		{
			Address address = new Address();
			Address.DeserializeLengthDelimited(stream, address);
			return address;
		}

		public static Address DeserializeLengthDelimited(Stream stream, Address instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return Address.Deserialize(stream, instance, num);
		}

		public static Address Deserialize(Stream stream, Address instance, long limit)
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
						instance.Port = ProtocolParser.ReadUInt32(stream);
					}
				}
				else
				{
					instance.Address_ = ProtocolParser.ReadString(stream);
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
			Address.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, Address instance)
		{
			if (instance.Address_ == null)
			{
				throw new ArgumentNullException("Address_", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Address_));
			if (instance.HasPort)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt32(stream, instance.Port);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.Address_);
			num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			if (this.HasPort)
			{
				num += 1u;
				num += ProtocolParser.SizeOfUInt32(this.Port);
			}
			return num + 1u;
		}

		public string Address_ { get; set; }

		public void SetAddress_(string val)
		{
			this.Address_ = val;
		}

		public uint Port
		{
			get
			{
				return this._Port;
			}
			set
			{
				this._Port = value;
				this.HasPort = true;
			}
		}

		public void SetPort(uint val)
		{
			this.Port = val;
		}

		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.Address_.GetHashCode();
			if (this.HasPort)
			{
				num ^= this.Port.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			Address address = obj as Address;
			return address != null && this.Address_.Equals(address.Address_) && this.HasPort == address.HasPort && (!this.HasPort || this.Port.Equals(address.Port));
		}

		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		public static Address ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<Address>(bs, 0, -1);
		}

		public bool HasPort;

		private uint _Port;
	}
}
