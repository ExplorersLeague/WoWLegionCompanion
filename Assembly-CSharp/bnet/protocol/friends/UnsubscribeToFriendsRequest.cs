using System;
using System.IO;

namespace bnet.protocol.friends
{
	public class UnsubscribeToFriendsRequest : IProtoBuf
	{
		public void Deserialize(Stream stream)
		{
			UnsubscribeToFriendsRequest.Deserialize(stream, this);
		}

		public static UnsubscribeToFriendsRequest Deserialize(Stream stream, UnsubscribeToFriendsRequest instance)
		{
			return UnsubscribeToFriendsRequest.Deserialize(stream, instance, -1L);
		}

		public static UnsubscribeToFriendsRequest DeserializeLengthDelimited(Stream stream)
		{
			UnsubscribeToFriendsRequest unsubscribeToFriendsRequest = new UnsubscribeToFriendsRequest();
			UnsubscribeToFriendsRequest.DeserializeLengthDelimited(stream, unsubscribeToFriendsRequest);
			return unsubscribeToFriendsRequest;
		}

		public static UnsubscribeToFriendsRequest DeserializeLengthDelimited(Stream stream, UnsubscribeToFriendsRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return UnsubscribeToFriendsRequest.Deserialize(stream, instance, num);
		}

		public static UnsubscribeToFriendsRequest Deserialize(Stream stream, UnsubscribeToFriendsRequest instance, long limit)
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
						instance.ObjectId = ProtocolParser.ReadUInt64(stream);
					}
				}
				else if (instance.AgentId == null)
				{
					instance.AgentId = EntityId.DeserializeLengthDelimited(stream);
				}
				else
				{
					EntityId.DeserializeLengthDelimited(stream, instance.AgentId);
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
			UnsubscribeToFriendsRequest.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, UnsubscribeToFriendsRequest instance)
		{
			if (instance.HasAgentId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.AgentId.GetSerializedSize());
				EntityId.Serialize(stream, instance.AgentId);
			}
			if (instance.HasObjectId)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, instance.ObjectId);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (this.HasAgentId)
			{
				num += 1u;
				uint serializedSize = this.AgentId.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasObjectId)
			{
				num += 1u;
				num += ProtocolParser.SizeOfUInt64(this.ObjectId);
			}
			return num;
		}

		public EntityId AgentId
		{
			get
			{
				return this._AgentId;
			}
			set
			{
				this._AgentId = value;
				this.HasAgentId = (value != null);
			}
		}

		public void SetAgentId(EntityId val)
		{
			this.AgentId = val;
		}

		public ulong ObjectId
		{
			get
			{
				return this._ObjectId;
			}
			set
			{
				this._ObjectId = value;
				this.HasObjectId = true;
			}
		}

		public void SetObjectId(ulong val)
		{
			this.ObjectId = val;
		}

		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasAgentId)
			{
				num ^= this.AgentId.GetHashCode();
			}
			if (this.HasObjectId)
			{
				num ^= this.ObjectId.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			UnsubscribeToFriendsRequest unsubscribeToFriendsRequest = obj as UnsubscribeToFriendsRequest;
			return unsubscribeToFriendsRequest != null && this.HasAgentId == unsubscribeToFriendsRequest.HasAgentId && (!this.HasAgentId || this.AgentId.Equals(unsubscribeToFriendsRequest.AgentId)) && this.HasObjectId == unsubscribeToFriendsRequest.HasObjectId && (!this.HasObjectId || this.ObjectId.Equals(unsubscribeToFriendsRequest.ObjectId));
		}

		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		public static UnsubscribeToFriendsRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<UnsubscribeToFriendsRequest>(bs, 0, -1);
		}

		public bool HasAgentId;

		private EntityId _AgentId;

		public bool HasObjectId;

		private ulong _ObjectId;
	}
}
