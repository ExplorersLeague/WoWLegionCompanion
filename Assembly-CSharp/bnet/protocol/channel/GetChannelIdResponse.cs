using System;
using System.IO;

namespace bnet.protocol.channel
{
	public class GetChannelIdResponse : IProtoBuf
	{
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

		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasChannelId)
			{
				num ^= this.ChannelId.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			GetChannelIdResponse getChannelIdResponse = obj as GetChannelIdResponse;
			return getChannelIdResponse != null && this.HasChannelId == getChannelIdResponse.HasChannelId && (!this.HasChannelId || this.ChannelId.Equals(getChannelIdResponse.ChannelId));
		}

		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		public static GetChannelIdResponse ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GetChannelIdResponse>(bs, 0, -1);
		}

		public void Deserialize(Stream stream)
		{
			GetChannelIdResponse.Deserialize(stream, this);
		}

		public static GetChannelIdResponse Deserialize(Stream stream, GetChannelIdResponse instance)
		{
			return GetChannelIdResponse.Deserialize(stream, instance, -1L);
		}

		public static GetChannelIdResponse DeserializeLengthDelimited(Stream stream)
		{
			GetChannelIdResponse getChannelIdResponse = new GetChannelIdResponse();
			GetChannelIdResponse.DeserializeLengthDelimited(stream, getChannelIdResponse);
			return getChannelIdResponse;
		}

		public static GetChannelIdResponse DeserializeLengthDelimited(Stream stream, GetChannelIdResponse instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GetChannelIdResponse.Deserialize(stream, instance, num);
		}

		public static GetChannelIdResponse Deserialize(Stream stream, GetChannelIdResponse instance, long limit)
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
					Key key = ProtocolParser.ReadKey((byte)num, stream);
					uint field = key.Field;
					if (field == 0u)
					{
						throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
					}
					ProtocolParser.SkipKey(stream, key);
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
			GetChannelIdResponse.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, GetChannelIdResponse instance)
		{
			if (instance.HasChannelId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.ChannelId.GetSerializedSize());
				EntityId.Serialize(stream, instance.ChannelId);
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
			return num;
		}

		public bool HasChannelId;

		private EntityId _ChannelId;
	}
}
