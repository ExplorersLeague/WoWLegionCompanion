using System;
using System.IO;
using bnet.protocol;

namespace bgs
{
	public class BattleNetPacket : PacketFormat
	{
		public BattleNetPacket()
		{
			this.header = null;
			this.body = null;
		}

		public BattleNetPacket(Header h, IProtoBuf b)
		{
			this.header = h;
			this.body = b;
		}

		public Header GetHeader()
		{
			return this.header;
		}

		public object GetBody()
		{
			return this.body;
		}

		public override bool IsLoaded()
		{
			return this.header != null && this.body != null;
		}

		public override int Decode(byte[] bytes, int offset, int available)
		{
			int num = 0;
			if (this.headerSize < 0)
			{
				if (available < 2)
				{
					return num;
				}
				this.headerSize = ((int)bytes[offset] << 8) + (int)bytes[offset + 1];
				available -= 2;
				num += 2;
				offset += 2;
			}
			if (this.header == null)
			{
				if (available < this.headerSize)
				{
					return num;
				}
				this.header = new Header();
				this.header.Deserialize(new MemoryStream(bytes, offset, this.headerSize));
				this.bodySize = (int)((!this.header.HasSize) ? 0u : this.header.Size);
				if (this.header == null)
				{
					throw new Exception("failed to parse BattleNet packet header");
				}
				available -= this.headerSize;
				num += this.headerSize;
				offset += this.headerSize;
			}
			if (this.body == null)
			{
				if (available < this.bodySize)
				{
					return num;
				}
				byte[] destinationArray = new byte[this.bodySize];
				Array.Copy(bytes, offset, destinationArray, 0, this.bodySize);
				this.body = destinationArray;
				num += this.bodySize;
			}
			return num;
		}

		public override byte[] Encode()
		{
			if (!(this.body is IProtoBuf))
			{
				return null;
			}
			IProtoBuf protoBuf = (IProtoBuf)this.body;
			int serializedSize = (int)this.header.GetSerializedSize();
			int serializedSize2 = (int)protoBuf.GetSerializedSize();
			byte[] array = new byte[2 + serializedSize + serializedSize2];
			array[0] = (byte)(serializedSize >> 8 & 255);
			array[1] = (byte)(serializedSize & 255);
			this.header.Serialize(new MemoryStream(array, 2, serializedSize));
			protoBuf.Serialize(new MemoryStream(array, 2 + serializedSize, serializedSize2));
			return array;
		}

		private Header header;

		private object body;

		private int headerSize = -1;

		private int bodySize = -1;
	}
}
