﻿using System;
using System.IO;

namespace bnet.protocol.channel_invitation
{
	public class SubscribeRequest : IProtoBuf
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

		public ulong ObjectId { get; set; }

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
			return num ^ this.ObjectId.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			SubscribeRequest subscribeRequest = obj as SubscribeRequest;
			return subscribeRequest != null && this.HasAgentId == subscribeRequest.HasAgentId && (!this.HasAgentId || this.AgentId.Equals(subscribeRequest.AgentId)) && this.ObjectId.Equals(subscribeRequest.ObjectId);
		}

		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		public static SubscribeRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<SubscribeRequest>(bs, 0, -1);
		}

		public void Deserialize(Stream stream)
		{
			SubscribeRequest.Deserialize(stream, this);
		}

		public static SubscribeRequest Deserialize(Stream stream, SubscribeRequest instance)
		{
			return SubscribeRequest.Deserialize(stream, instance, -1L);
		}

		public static SubscribeRequest DeserializeLengthDelimited(Stream stream)
		{
			SubscribeRequest subscribeRequest = new SubscribeRequest();
			SubscribeRequest.DeserializeLengthDelimited(stream, subscribeRequest);
			return subscribeRequest;
		}

		public static SubscribeRequest DeserializeLengthDelimited(Stream stream, SubscribeRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return SubscribeRequest.Deserialize(stream, instance, num);
		}

		public static SubscribeRequest Deserialize(Stream stream, SubscribeRequest instance, long limit)
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
			SubscribeRequest.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, SubscribeRequest instance)
		{
			if (instance.HasAgentId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.AgentId.GetSerializedSize());
				EntityId.Serialize(stream, instance.AgentId);
			}
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, instance.ObjectId);
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
			num += ProtocolParser.SizeOfUInt64(this.ObjectId);
			return num + 1u;
		}

		public bool HasAgentId;

		private EntityId _AgentId;
	}
}
