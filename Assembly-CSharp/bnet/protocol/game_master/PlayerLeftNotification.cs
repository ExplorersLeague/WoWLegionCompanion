using System;
using System.IO;

namespace bnet.protocol.game_master
{
	public class PlayerLeftNotification : IProtoBuf
	{
		public void Deserialize(Stream stream)
		{
			PlayerLeftNotification.Deserialize(stream, this);
		}

		public static PlayerLeftNotification Deserialize(Stream stream, PlayerLeftNotification instance)
		{
			return PlayerLeftNotification.Deserialize(stream, instance, -1L);
		}

		public static PlayerLeftNotification DeserializeLengthDelimited(Stream stream)
		{
			PlayerLeftNotification playerLeftNotification = new PlayerLeftNotification();
			PlayerLeftNotification.DeserializeLengthDelimited(stream, playerLeftNotification);
			return playerLeftNotification;
		}

		public static PlayerLeftNotification DeserializeLengthDelimited(Stream stream, PlayerLeftNotification instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return PlayerLeftNotification.Deserialize(stream, instance, num);
		}

		public static PlayerLeftNotification Deserialize(Stream stream, PlayerLeftNotification instance, long limit)
		{
			instance.Reason = 1u;
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
				else if (instance.GameHandle == null)
				{
					instance.GameHandle = GameHandle.DeserializeLengthDelimited(stream);
				}
				else
				{
					GameHandle.DeserializeLengthDelimited(stream, instance.GameHandle);
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
			PlayerLeftNotification.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, PlayerLeftNotification instance)
		{
			if (instance.GameHandle == null)
			{
				throw new ArgumentNullException("GameHandle", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteUInt32(stream, instance.GameHandle.GetSerializedSize());
			GameHandle.Serialize(stream, instance.GameHandle);
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
			uint serializedSize = this.GameHandle.GetSerializedSize();
			num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			uint serializedSize2 = this.MemberId.GetSerializedSize();
			num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			if (this.HasReason)
			{
				num += 1u;
				num += ProtocolParser.SizeOfUInt32(this.Reason);
			}
			return num + 2u;
		}

		public GameHandle GameHandle { get; set; }

		public void SetGameHandle(GameHandle val)
		{
			this.GameHandle = val;
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
			num ^= this.GameHandle.GetHashCode();
			num ^= this.MemberId.GetHashCode();
			if (this.HasReason)
			{
				num ^= this.Reason.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			PlayerLeftNotification playerLeftNotification = obj as PlayerLeftNotification;
			return playerLeftNotification != null && this.GameHandle.Equals(playerLeftNotification.GameHandle) && this.MemberId.Equals(playerLeftNotification.MemberId) && this.HasReason == playerLeftNotification.HasReason && (!this.HasReason || this.Reason.Equals(playerLeftNotification.Reason));
		}

		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		public static PlayerLeftNotification ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<PlayerLeftNotification>(bs, 0, -1);
		}

		public bool HasReason;

		private uint _Reason;
	}
}
