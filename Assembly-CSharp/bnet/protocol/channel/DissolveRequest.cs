using System;
using System.IO;

namespace bnet.protocol.channel
{
	public class DissolveRequest : IProtoBuf
	{
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

		public uint Reason
		{
			get
			{
				return this._Reason;
			}
			set
			{
				this._Reason = value;
				this.HasReason = true;
			}
		}

		public void SetReason(uint val)
		{
			this.Reason = val;
		}

		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasAgentId)
			{
				num ^= this.AgentId.GetHashCode();
			}
			if (this.HasReason)
			{
				num ^= this.Reason.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			DissolveRequest dissolveRequest = obj as DissolveRequest;
			return dissolveRequest != null && this.HasAgentId == dissolveRequest.HasAgentId && (!this.HasAgentId || this.AgentId.Equals(dissolveRequest.AgentId)) && this.HasReason == dissolveRequest.HasReason && (!this.HasReason || this.Reason.Equals(dissolveRequest.Reason));
		}

		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		public static DissolveRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<DissolveRequest>(bs, 0, -1);
		}

		public void Deserialize(Stream stream)
		{
			DissolveRequest.Deserialize(stream, this);
		}

		public static DissolveRequest Deserialize(Stream stream, DissolveRequest instance)
		{
			return DissolveRequest.Deserialize(stream, instance, -1L);
		}

		public static DissolveRequest DeserializeLengthDelimited(Stream stream)
		{
			DissolveRequest dissolveRequest = new DissolveRequest();
			DissolveRequest.DeserializeLengthDelimited(stream, dissolveRequest);
			return dissolveRequest;
		}

		public static DissolveRequest DeserializeLengthDelimited(Stream stream, DissolveRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return DissolveRequest.Deserialize(stream, instance, num);
		}

		public static DissolveRequest Deserialize(Stream stream, DissolveRequest instance, long limit)
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
						instance.Reason = ProtocolParser.ReadUInt32(stream);
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
			DissolveRequest.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, DissolveRequest instance)
		{
			if (instance.HasAgentId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.AgentId.GetSerializedSize());
				EntityId.Serialize(stream, instance.AgentId);
			}
			if (instance.HasReason)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt32(stream, instance.Reason);
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
			if (this.HasReason)
			{
				num += 1u;
				num += ProtocolParser.SizeOfUInt32(this.Reason);
			}
			return num;
		}

		public bool HasAgentId;

		private EntityId _AgentId;

		public bool HasReason;

		private uint _Reason;
	}
}
