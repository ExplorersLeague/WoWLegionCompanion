using System;
using System.IO;

namespace bnet.protocol.account
{
	public class GetCAISInfoRequest : IProtoBuf
	{
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
			GetCAISInfoRequest getCAISInfoRequest = obj as GetCAISInfoRequest;
			return getCAISInfoRequest != null && this.HasEntityId == getCAISInfoRequest.HasEntityId && (!this.HasEntityId || this.EntityId.Equals(getCAISInfoRequest.EntityId));
		}

		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		public static GetCAISInfoRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GetCAISInfoRequest>(bs, 0, -1);
		}

		public void Deserialize(Stream stream)
		{
			GetCAISInfoRequest.Deserialize(stream, this);
		}

		public static GetCAISInfoRequest Deserialize(Stream stream, GetCAISInfoRequest instance)
		{
			return GetCAISInfoRequest.Deserialize(stream, instance, -1L);
		}

		public static GetCAISInfoRequest DeserializeLengthDelimited(Stream stream)
		{
			GetCAISInfoRequest getCAISInfoRequest = new GetCAISInfoRequest();
			GetCAISInfoRequest.DeserializeLengthDelimited(stream, getCAISInfoRequest);
			return getCAISInfoRequest;
		}

		public static GetCAISInfoRequest DeserializeLengthDelimited(Stream stream, GetCAISInfoRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GetCAISInfoRequest.Deserialize(stream, instance, num);
		}

		public static GetCAISInfoRequest Deserialize(Stream stream, GetCAISInfoRequest instance, long limit)
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
				else if (instance.EntityId == null)
				{
					instance.EntityId = EntityId.DeserializeLengthDelimited(stream);
				}
				else
				{
					EntityId.DeserializeLengthDelimited(stream, instance.EntityId);
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
			GetCAISInfoRequest.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, GetCAISInfoRequest instance)
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

		public bool HasEntityId;

		private EntityId _EntityId;
	}
}
