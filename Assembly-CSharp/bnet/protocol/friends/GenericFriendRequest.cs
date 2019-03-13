﻿using System;
using System.IO;

namespace bnet.protocol.friends
{
	public class GenericFriendRequest : IProtoBuf
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

		public EntityId TargetId { get; set; }

		public void SetTargetId(EntityId val)
		{
			this.TargetId = val;
		}

		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasAgentId)
			{
				num ^= this.AgentId.GetHashCode();
			}
			return num ^ this.TargetId.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			GenericFriendRequest genericFriendRequest = obj as GenericFriendRequest;
			return genericFriendRequest != null && this.HasAgentId == genericFriendRequest.HasAgentId && (!this.HasAgentId || this.AgentId.Equals(genericFriendRequest.AgentId)) && this.TargetId.Equals(genericFriendRequest.TargetId);
		}

		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		public static GenericFriendRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GenericFriendRequest>(bs, 0, -1);
		}

		public void Deserialize(Stream stream)
		{
			GenericFriendRequest.Deserialize(stream, this);
		}

		public static GenericFriendRequest Deserialize(Stream stream, GenericFriendRequest instance)
		{
			return GenericFriendRequest.Deserialize(stream, instance, -1L);
		}

		public static GenericFriendRequest DeserializeLengthDelimited(Stream stream)
		{
			GenericFriendRequest genericFriendRequest = new GenericFriendRequest();
			GenericFriendRequest.DeserializeLengthDelimited(stream, genericFriendRequest);
			return genericFriendRequest;
		}

		public static GenericFriendRequest DeserializeLengthDelimited(Stream stream, GenericFriendRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GenericFriendRequest.Deserialize(stream, instance, num);
		}

		public static GenericFriendRequest Deserialize(Stream stream, GenericFriendRequest instance, long limit)
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
					else if (instance.TargetId == null)
					{
						instance.TargetId = EntityId.DeserializeLengthDelimited(stream);
					}
					else
					{
						EntityId.DeserializeLengthDelimited(stream, instance.TargetId);
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
			GenericFriendRequest.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, GenericFriendRequest instance)
		{
			if (instance.HasAgentId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.AgentId.GetSerializedSize());
				EntityId.Serialize(stream, instance.AgentId);
			}
			if (instance.TargetId == null)
			{
				throw new ArgumentNullException("TargetId", "Required by proto specification.");
			}
			stream.WriteByte(18);
			ProtocolParser.WriteUInt32(stream, instance.TargetId.GetSerializedSize());
			EntityId.Serialize(stream, instance.TargetId);
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
			uint serializedSize2 = this.TargetId.GetSerializedSize();
			num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			return num + 1u;
		}

		public bool HasAgentId;

		private EntityId _AgentId;
	}
}
