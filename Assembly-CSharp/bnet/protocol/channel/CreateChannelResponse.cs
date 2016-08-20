using System;
using System.IO;

namespace bnet.protocol.channel
{
	public class CreateChannelResponse : IProtoBuf
	{
		public void Deserialize(Stream stream)
		{
			CreateChannelResponse.Deserialize(stream, this);
		}

		public static CreateChannelResponse Deserialize(Stream stream, CreateChannelResponse instance)
		{
			return CreateChannelResponse.Deserialize(stream, instance, -1L);
		}

		public static CreateChannelResponse DeserializeLengthDelimited(Stream stream)
		{
			CreateChannelResponse createChannelResponse = new CreateChannelResponse();
			CreateChannelResponse.DeserializeLengthDelimited(stream, createChannelResponse);
			return createChannelResponse;
		}

		public static CreateChannelResponse DeserializeLengthDelimited(Stream stream, CreateChannelResponse instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return CreateChannelResponse.Deserialize(stream, instance, num);
		}

		public static CreateChannelResponse Deserialize(Stream stream, CreateChannelResponse instance, long limit)
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
						else if (instance.ChannelId == null)
						{
							instance.ChannelId = EntityId.DeserializeLengthDelimited(stream);
						}
						else
						{
							EntityId.DeserializeLengthDelimited(stream, instance.ChannelId);
						}
					}
					else
					{
						instance.ObjectId = ProtocolParser.ReadUInt64(stream);
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
			CreateChannelResponse.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, CreateChannelResponse instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, instance.ObjectId);
			if (instance.HasChannelId)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.ChannelId.GetSerializedSize());
				EntityId.Serialize(stream, instance.ChannelId);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			num += ProtocolParser.SizeOfUInt64(this.ObjectId);
			if (this.HasChannelId)
			{
				num += 1u;
				uint serializedSize = this.ChannelId.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			return num + 1u;
		}

		public ulong ObjectId { get; set; }

		public void SetObjectId(ulong val)
		{
			this.ObjectId = val;
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

		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.ObjectId.GetHashCode();
			if (this.HasChannelId)
			{
				num ^= this.ChannelId.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			CreateChannelResponse createChannelResponse = obj as CreateChannelResponse;
			return createChannelResponse != null && this.ObjectId.Equals(createChannelResponse.ObjectId) && this.HasChannelId == createChannelResponse.HasChannelId && (!this.HasChannelId || this.ChannelId.Equals(createChannelResponse.ChannelId));
		}

		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		public static CreateChannelResponse ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<CreateChannelResponse>(bs, 0, -1);
		}

		public bool HasChannelId;

		private EntityId _ChannelId;
	}
}
