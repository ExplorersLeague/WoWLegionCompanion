using System;
using System.IO;

namespace bnet.protocol.game_utilities
{
	public class PresenceChannelCreatedRequest : IProtoBuf
	{
		public EntityId Id { get; set; }

		public void SetId(EntityId val)
		{
			this.Id = val;
		}

		public EntityId GameAccountId
		{
			get
			{
				return this._GameAccountId;
			}
			set
			{
				this._GameAccountId = value;
				this.HasGameAccountId = (value != null);
			}
		}

		public void SetGameAccountId(EntityId val)
		{
			this.GameAccountId = val;
		}

		public EntityId BnetAccountId
		{
			get
			{
				return this._BnetAccountId;
			}
			set
			{
				this._BnetAccountId = value;
				this.HasBnetAccountId = (value != null);
			}
		}

		public void SetBnetAccountId(EntityId val)
		{
			this.BnetAccountId = val;
		}

		public ProcessId Host
		{
			get
			{
				return this._Host;
			}
			set
			{
				this._Host = value;
				this.HasHost = (value != null);
			}
		}

		public void SetHost(ProcessId val)
		{
			this.Host = val;
		}

		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.Id.GetHashCode();
			if (this.HasGameAccountId)
			{
				num ^= this.GameAccountId.GetHashCode();
			}
			if (this.HasBnetAccountId)
			{
				num ^= this.BnetAccountId.GetHashCode();
			}
			if (this.HasHost)
			{
				num ^= this.Host.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			PresenceChannelCreatedRequest presenceChannelCreatedRequest = obj as PresenceChannelCreatedRequest;
			return presenceChannelCreatedRequest != null && this.Id.Equals(presenceChannelCreatedRequest.Id) && this.HasGameAccountId == presenceChannelCreatedRequest.HasGameAccountId && (!this.HasGameAccountId || this.GameAccountId.Equals(presenceChannelCreatedRequest.GameAccountId)) && this.HasBnetAccountId == presenceChannelCreatedRequest.HasBnetAccountId && (!this.HasBnetAccountId || this.BnetAccountId.Equals(presenceChannelCreatedRequest.BnetAccountId)) && this.HasHost == presenceChannelCreatedRequest.HasHost && (!this.HasHost || this.Host.Equals(presenceChannelCreatedRequest.Host));
		}

		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		public static PresenceChannelCreatedRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<PresenceChannelCreatedRequest>(bs, 0, -1);
		}

		public void Deserialize(Stream stream)
		{
			PresenceChannelCreatedRequest.Deserialize(stream, this);
		}

		public static PresenceChannelCreatedRequest Deserialize(Stream stream, PresenceChannelCreatedRequest instance)
		{
			return PresenceChannelCreatedRequest.Deserialize(stream, instance, -1L);
		}

		public static PresenceChannelCreatedRequest DeserializeLengthDelimited(Stream stream)
		{
			PresenceChannelCreatedRequest presenceChannelCreatedRequest = new PresenceChannelCreatedRequest();
			PresenceChannelCreatedRequest.DeserializeLengthDelimited(stream, presenceChannelCreatedRequest);
			return presenceChannelCreatedRequest;
		}

		public static PresenceChannelCreatedRequest DeserializeLengthDelimited(Stream stream, PresenceChannelCreatedRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return PresenceChannelCreatedRequest.Deserialize(stream, instance, num);
		}

		public static PresenceChannelCreatedRequest Deserialize(Stream stream, PresenceChannelCreatedRequest instance, long limit)
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
					if (num != 26)
					{
						if (num != 34)
						{
							if (num != 42)
							{
								Key key = ProtocolParser.ReadKey((byte)num, stream);
								uint field = key.Field;
								if (field == 0u)
								{
									throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
								}
								ProtocolParser.SkipKey(stream, key);
							}
							else if (instance.Host == null)
							{
								instance.Host = ProcessId.DeserializeLengthDelimited(stream);
							}
							else
							{
								ProcessId.DeserializeLengthDelimited(stream, instance.Host);
							}
						}
						else if (instance.BnetAccountId == null)
						{
							instance.BnetAccountId = EntityId.DeserializeLengthDelimited(stream);
						}
						else
						{
							EntityId.DeserializeLengthDelimited(stream, instance.BnetAccountId);
						}
					}
					else if (instance.GameAccountId == null)
					{
						instance.GameAccountId = EntityId.DeserializeLengthDelimited(stream);
					}
					else
					{
						EntityId.DeserializeLengthDelimited(stream, instance.GameAccountId);
					}
				}
				else if (instance.Id == null)
				{
					instance.Id = EntityId.DeserializeLengthDelimited(stream);
				}
				else
				{
					EntityId.DeserializeLengthDelimited(stream, instance.Id);
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
			PresenceChannelCreatedRequest.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, PresenceChannelCreatedRequest instance)
		{
			if (instance.Id == null)
			{
				throw new ArgumentNullException("Id", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteUInt32(stream, instance.Id.GetSerializedSize());
			EntityId.Serialize(stream, instance.Id);
			if (instance.HasGameAccountId)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteUInt32(stream, instance.GameAccountId.GetSerializedSize());
				EntityId.Serialize(stream, instance.GameAccountId);
			}
			if (instance.HasBnetAccountId)
			{
				stream.WriteByte(34);
				ProtocolParser.WriteUInt32(stream, instance.BnetAccountId.GetSerializedSize());
				EntityId.Serialize(stream, instance.BnetAccountId);
			}
			if (instance.HasHost)
			{
				stream.WriteByte(42);
				ProtocolParser.WriteUInt32(stream, instance.Host.GetSerializedSize());
				ProcessId.Serialize(stream, instance.Host);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			uint serializedSize = this.Id.GetSerializedSize();
			num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			if (this.HasGameAccountId)
			{
				num += 1u;
				uint serializedSize2 = this.GameAccountId.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			if (this.HasBnetAccountId)
			{
				num += 1u;
				uint serializedSize3 = this.BnetAccountId.GetSerializedSize();
				num += serializedSize3 + ProtocolParser.SizeOfUInt32(serializedSize3);
			}
			if (this.HasHost)
			{
				num += 1u;
				uint serializedSize4 = this.Host.GetSerializedSize();
				num += serializedSize4 + ProtocolParser.SizeOfUInt32(serializedSize4);
			}
			return num + 1u;
		}

		public bool HasGameAccountId;

		private EntityId _GameAccountId;

		public bool HasBnetAccountId;

		private EntityId _BnetAccountId;

		public bool HasHost;

		private ProcessId _Host;
	}
}
