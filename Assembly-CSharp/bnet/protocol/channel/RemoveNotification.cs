﻿using System;
using System.IO;

namespace bnet.protocol.channel
{
	public class RemoveNotification : IProtoBuf
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

		public EntityId MemberId { get; set; }

		public void SetMemberId(EntityId val)
		{
			this.MemberId = val;
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
			num ^= this.MemberId.GetHashCode();
			if (this.HasReason)
			{
				num ^= this.Reason.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			RemoveNotification removeNotification = obj as RemoveNotification;
			return removeNotification != null && this.HasAgentId == removeNotification.HasAgentId && (!this.HasAgentId || this.AgentId.Equals(removeNotification.AgentId)) && this.MemberId.Equals(removeNotification.MemberId) && this.HasReason == removeNotification.HasReason && (!this.HasReason || this.Reason.Equals(removeNotification.Reason));
		}

		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		public static RemoveNotification ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<RemoveNotification>(bs, 0, -1);
		}

		public void Deserialize(Stream stream)
		{
			RemoveNotification.Deserialize(stream, this);
		}

		public static RemoveNotification Deserialize(Stream stream, RemoveNotification instance)
		{
			return RemoveNotification.Deserialize(stream, instance, -1L);
		}

		public static RemoveNotification DeserializeLengthDelimited(Stream stream)
		{
			RemoveNotification removeNotification = new RemoveNotification();
			RemoveNotification.DeserializeLengthDelimited(stream, removeNotification);
			return removeNotification;
		}

		public static RemoveNotification DeserializeLengthDelimited(Stream stream, RemoveNotification instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return RemoveNotification.Deserialize(stream, instance, num);
		}

		public static RemoveNotification Deserialize(Stream stream, RemoveNotification instance, long limit)
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
						if (num != 24)
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
					else if (instance.MemberId == null)
					{
						instance.MemberId = EntityId.DeserializeLengthDelimited(stream);
					}
					else
					{
						EntityId.DeserializeLengthDelimited(stream, instance.MemberId);
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
			RemoveNotification.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, RemoveNotification instance)
		{
			if (instance.HasAgentId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.AgentId.GetSerializedSize());
				EntityId.Serialize(stream, instance.AgentId);
			}
			if (instance.MemberId == null)
			{
				throw new ArgumentNullException("MemberId", "Required by proto specification.");
			}
			stream.WriteByte(18);
			ProtocolParser.WriteUInt32(stream, instance.MemberId.GetSerializedSize());
			EntityId.Serialize(stream, instance.MemberId);
			if (instance.HasReason)
			{
				stream.WriteByte(24);
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
			uint serializedSize2 = this.MemberId.GetSerializedSize();
			num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			if (this.HasReason)
			{
				num += 1u;
				num += ProtocolParser.SizeOfUInt32(this.Reason);
			}
			return num + 1u;
		}

		public bool HasAgentId;

		private EntityId _AgentId;

		public bool HasReason;

		private uint _Reason;
	}
}
