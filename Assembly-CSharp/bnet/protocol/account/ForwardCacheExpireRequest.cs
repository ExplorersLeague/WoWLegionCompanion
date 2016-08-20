using System;
using System.IO;

namespace bnet.protocol.account
{
	public class ForwardCacheExpireRequest : IProtoBuf
	{
		public void Deserialize(Stream stream)
		{
			ForwardCacheExpireRequest.Deserialize(stream, this);
		}

		public static ForwardCacheExpireRequest Deserialize(Stream stream, ForwardCacheExpireRequest instance)
		{
			return ForwardCacheExpireRequest.Deserialize(stream, instance, -1L);
		}

		public static ForwardCacheExpireRequest DeserializeLengthDelimited(Stream stream)
		{
			ForwardCacheExpireRequest forwardCacheExpireRequest = new ForwardCacheExpireRequest();
			ForwardCacheExpireRequest.DeserializeLengthDelimited(stream, forwardCacheExpireRequest);
			return forwardCacheExpireRequest;
		}

		public static ForwardCacheExpireRequest DeserializeLengthDelimited(Stream stream, ForwardCacheExpireRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return ForwardCacheExpireRequest.Deserialize(stream, instance, num);
		}

		public static ForwardCacheExpireRequest Deserialize(Stream stream, ForwardCacheExpireRequest instance, long limit)
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
						Key key = ProtocolParser.ReadKey((byte)num, stream);
						uint field = key.Field;
						if (field == 0u)
						{
							throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
						}
						ProtocolParser.SkipKey(stream, key);
					}
					else if (instance.EntityId == null)
					{
						instance.EntityId = EntityId.DeserializeLengthDelimited(stream);
					}
					else
					{
						EntityId.DeserializeLengthDelimited(stream, instance.EntityId);
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
			ForwardCacheExpireRequest.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, ForwardCacheExpireRequest instance)
		{
			if (instance.HasEntityId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.EntityId.GetSerializedSize());
				EntityId.Serialize(stream, instance.EntityId);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (this.HasEntityId)
			{
				num += 1u;
				uint serializedSize = this.EntityId.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			return num;
		}

		public EntityId EntityId
		{
			get
			{
				return this._EntityId;
			}
			set
			{
				this._EntityId = value;
				this.HasEntityId = (value != null);
			}
		}

		public void SetEntityId(EntityId val)
		{
			this.EntityId = val;
		}

		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasEntityId)
			{
				num ^= this.EntityId.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			ForwardCacheExpireRequest forwardCacheExpireRequest = obj as ForwardCacheExpireRequest;
			return forwardCacheExpireRequest != null && this.HasEntityId == forwardCacheExpireRequest.HasEntityId && (!this.HasEntityId || this.EntityId.Equals(forwardCacheExpireRequest.EntityId));
		}

		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		public static ForwardCacheExpireRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<ForwardCacheExpireRequest>(bs, 0, -1);
		}

		public bool HasEntityId;

		private EntityId _EntityId;
	}
}
