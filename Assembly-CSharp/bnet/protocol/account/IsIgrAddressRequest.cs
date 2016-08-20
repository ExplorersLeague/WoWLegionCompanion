using System;
using System.IO;
using System.Text;

namespace bnet.protocol.account
{
	public class IsIgrAddressRequest : IProtoBuf
	{
		public void Deserialize(Stream stream)
		{
			IsIgrAddressRequest.Deserialize(stream, this);
		}

		public static IsIgrAddressRequest Deserialize(Stream stream, IsIgrAddressRequest instance)
		{
			return IsIgrAddressRequest.Deserialize(stream, instance, -1L);
		}

		public static IsIgrAddressRequest DeserializeLengthDelimited(Stream stream)
		{
			IsIgrAddressRequest isIgrAddressRequest = new IsIgrAddressRequest();
			IsIgrAddressRequest.DeserializeLengthDelimited(stream, isIgrAddressRequest);
			return isIgrAddressRequest;
		}

		public static IsIgrAddressRequest DeserializeLengthDelimited(Stream stream, IsIgrAddressRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return IsIgrAddressRequest.Deserialize(stream, instance, num);
		}

		public static IsIgrAddressRequest Deserialize(Stream stream, IsIgrAddressRequest instance, long limit)
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
					if (num2 != 10)
					{
						if (num2 != 16)
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
							instance.Region = ProtocolParser.ReadUInt32(stream);
						}
					}
					else
					{
						instance.ClientAddress = ProtocolParser.ReadString(stream);
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
			IsIgrAddressRequest.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, IsIgrAddressRequest instance)
		{
			if (instance.HasClientAddress)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.ClientAddress));
			}
			if (instance.HasRegion)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt32(stream, instance.Region);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (this.HasClientAddress)
			{
				num += 1u;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.ClientAddress);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (this.HasRegion)
			{
				num += 1u;
				num += ProtocolParser.SizeOfUInt32(this.Region);
			}
			return num;
		}

		public string ClientAddress
		{
			get
			{
				return this._ClientAddress;
			}
			set
			{
				this._ClientAddress = value;
				this.HasClientAddress = (value != null);
			}
		}

		public void SetClientAddress(string val)
		{
			this.ClientAddress = val;
		}

		public uint Region
		{
			get
			{
				return this._Region;
			}
			set
			{
				this._Region = value;
				this.HasRegion = true;
			}
		}

		public void SetRegion(uint val)
		{
			this.Region = val;
		}

		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasClientAddress)
			{
				num ^= this.ClientAddress.GetHashCode();
			}
			if (this.HasRegion)
			{
				num ^= this.Region.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			IsIgrAddressRequest isIgrAddressRequest = obj as IsIgrAddressRequest;
			return isIgrAddressRequest != null && this.HasClientAddress == isIgrAddressRequest.HasClientAddress && (!this.HasClientAddress || this.ClientAddress.Equals(isIgrAddressRequest.ClientAddress)) && this.HasRegion == isIgrAddressRequest.HasRegion && (!this.HasRegion || this.Region.Equals(isIgrAddressRequest.Region));
		}

		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		public static IsIgrAddressRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<IsIgrAddressRequest>(bs, 0, -1);
		}

		public bool HasClientAddress;

		private string _ClientAddress;

		public bool HasRegion;

		private uint _Region;
	}
}
