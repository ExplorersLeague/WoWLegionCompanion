using System;
using System.IO;
using System.Text;

namespace bnet.protocol.channel_invitation
{
	public class ChannelCount : IProtoBuf
	{
		public void Deserialize(Stream stream)
		{
			ChannelCount.Deserialize(stream, this);
		}

		public static ChannelCount Deserialize(Stream stream, ChannelCount instance)
		{
			return ChannelCount.Deserialize(stream, instance, -1L);
		}

		public static ChannelCount DeserializeLengthDelimited(Stream stream)
		{
			ChannelCount channelCount = new ChannelCount();
			ChannelCount.DeserializeLengthDelimited(stream, channelCount);
			return channelCount;
		}

		public static ChannelCount DeserializeLengthDelimited(Stream stream, ChannelCount instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return ChannelCount.Deserialize(stream, instance, num);
		}

		public static ChannelCount Deserialize(Stream stream, ChannelCount instance, long limit)
		{
			instance.ChannelType = "default";
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
						instance.ChannelType = ProtocolParser.ReadString(stream);
					}
				}
				else if (instance.ChannelId == null)
				{
					instance.ChannelId = EntityId.DeserializeLengthDelimited(stream);
				}
				else
				{
					EntityId.DeserializeLengthDelimited(stream, instance.ChannelId);
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
			ChannelCount.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, ChannelCount instance)
		{
			if (instance.HasChannelId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.ChannelId.GetSerializedSize());
				EntityId.Serialize(stream, instance.ChannelId);
			}
			if (instance.HasChannelType)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.ChannelType));
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (this.HasChannelId)
			{
				num += 1u;
				uint serializedSize = this.ChannelId.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasChannelType)
			{
				num += 1u;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.ChannelType);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			return num;
		}

		public EntityId ChannelId
		{
			get
			{
				return this._ChannelId;
			}
			set
			{
				this._ChannelId = value;
				this.HasChannelId = (value != null);
			}
		}

		public void SetChannelId(EntityId val)
		{
			this.ChannelId = val;
		}

		public string ChannelType
		{
			get
			{
				return this._ChannelType;
			}
			set
			{
				this._ChannelType = value;
				this.HasChannelType = (value != null);
			}
		}

		public void SetChannelType(string val)
		{
			this.ChannelType = val;
		}

		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasChannelId)
			{
				num ^= this.ChannelId.GetHashCode();
			}
			if (this.HasChannelType)
			{
				num ^= this.ChannelType.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			ChannelCount channelCount = obj as ChannelCount;
			return channelCount != null && this.HasChannelId == channelCount.HasChannelId && (!this.HasChannelId || this.ChannelId.Equals(channelCount.ChannelId)) && this.HasChannelType == channelCount.HasChannelType && (!this.HasChannelType || this.ChannelType.Equals(channelCount.ChannelType));
		}

		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		public static ChannelCount ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<ChannelCount>(bs, 0, -1);
		}

		public bool HasChannelId;

		private EntityId _ChannelId;

		public bool HasChannelType;

		private string _ChannelType;
	}
}
