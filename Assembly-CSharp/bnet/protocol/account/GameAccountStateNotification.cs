using System;
using System.IO;

namespace bnet.protocol.account
{
	public class GameAccountStateNotification : IProtoBuf
	{
		public void Deserialize(Stream stream)
		{
			GameAccountStateNotification.Deserialize(stream, this);
		}

		public static GameAccountStateNotification Deserialize(Stream stream, GameAccountStateNotification instance)
		{
			return GameAccountStateNotification.Deserialize(stream, instance, -1L);
		}

		public static GameAccountStateNotification DeserializeLengthDelimited(Stream stream)
		{
			GameAccountStateNotification gameAccountStateNotification = new GameAccountStateNotification();
			GameAccountStateNotification.DeserializeLengthDelimited(stream, gameAccountStateNotification);
			return gameAccountStateNotification;
		}

		public static GameAccountStateNotification DeserializeLengthDelimited(Stream stream, GameAccountStateNotification instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GameAccountStateNotification.Deserialize(stream, instance, num);
		}

		public static GameAccountStateNotification Deserialize(Stream stream, GameAccountStateNotification instance, long limit)
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
						if (num != 26)
						{
							if (num != 32)
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
								instance.SubscriptionCompleted = ProtocolParser.ReadBool(stream);
							}
						}
						else if (instance.GameAccountTags == null)
						{
							instance.GameAccountTags = GameAccountFieldTags.DeserializeLengthDelimited(stream);
						}
						else
						{
							GameAccountFieldTags.DeserializeLengthDelimited(stream, instance.GameAccountTags);
						}
					}
					else
					{
						instance.SubscriberId = ProtocolParser.ReadUInt64(stream);
					}
				}
				else if (instance.State == null)
				{
					instance.State = GameAccountState.DeserializeLengthDelimited(stream);
				}
				else
				{
					GameAccountState.DeserializeLengthDelimited(stream, instance.State);
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
			GameAccountStateNotification.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, GameAccountStateNotification instance)
		{
			if (instance.HasState)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.State.GetSerializedSize());
				GameAccountState.Serialize(stream, instance.State);
			}
			if (instance.HasSubscriberId)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, instance.SubscriberId);
			}
			if (instance.HasGameAccountTags)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteUInt32(stream, instance.GameAccountTags.GetSerializedSize());
				GameAccountFieldTags.Serialize(stream, instance.GameAccountTags);
			}
			if (instance.HasSubscriptionCompleted)
			{
				stream.WriteByte(32);
				ProtocolParser.WriteBool(stream, instance.SubscriptionCompleted);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (this.HasState)
			{
				num += 1u;
				uint serializedSize = this.State.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasSubscriberId)
			{
				num += 1u;
				num += ProtocolParser.SizeOfUInt64(this.SubscriberId);
			}
			if (this.HasGameAccountTags)
			{
				num += 1u;
				uint serializedSize2 = this.GameAccountTags.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			if (this.HasSubscriptionCompleted)
			{
				num += 1u;
				num += 1u;
			}
			return num;
		}

		public GameAccountState State
		{
			get
			{
				return this._State;
			}
			set
			{
				this._State = value;
				this.HasState = (value != null);
			}
		}

		public void SetState(GameAccountState val)
		{
			this.State = val;
		}

		public ulong SubscriberId
		{
			get
			{
				return this._SubscriberId;
			}
			set
			{
				this._SubscriberId = value;
				this.HasSubscriberId = true;
			}
		}

		public void SetSubscriberId(ulong val)
		{
			this.SubscriberId = val;
		}

		public GameAccountFieldTags GameAccountTags
		{
			get
			{
				return this._GameAccountTags;
			}
			set
			{
				this._GameAccountTags = value;
				this.HasGameAccountTags = (value != null);
			}
		}

		public void SetGameAccountTags(GameAccountFieldTags val)
		{
			this.GameAccountTags = val;
		}

		public bool SubscriptionCompleted
		{
			get
			{
				return this._SubscriptionCompleted;
			}
			set
			{
				this._SubscriptionCompleted = value;
				this.HasSubscriptionCompleted = true;
			}
		}

		public void SetSubscriptionCompleted(bool val)
		{
			this.SubscriptionCompleted = val;
		}

		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasState)
			{
				num ^= this.State.GetHashCode();
			}
			if (this.HasSubscriberId)
			{
				num ^= this.SubscriberId.GetHashCode();
			}
			if (this.HasGameAccountTags)
			{
				num ^= this.GameAccountTags.GetHashCode();
			}
			if (this.HasSubscriptionCompleted)
			{
				num ^= this.SubscriptionCompleted.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			GameAccountStateNotification gameAccountStateNotification = obj as GameAccountStateNotification;
			return gameAccountStateNotification != null && this.HasState == gameAccountStateNotification.HasState && (!this.HasState || this.State.Equals(gameAccountStateNotification.State)) && this.HasSubscriberId == gameAccountStateNotification.HasSubscriberId && (!this.HasSubscriberId || this.SubscriberId.Equals(gameAccountStateNotification.SubscriberId)) && this.HasGameAccountTags == gameAccountStateNotification.HasGameAccountTags && (!this.HasGameAccountTags || this.GameAccountTags.Equals(gameAccountStateNotification.GameAccountTags)) && this.HasSubscriptionCompleted == gameAccountStateNotification.HasSubscriptionCompleted && (!this.HasSubscriptionCompleted || this.SubscriptionCompleted.Equals(gameAccountStateNotification.SubscriptionCompleted));
		}

		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		public static GameAccountStateNotification ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GameAccountStateNotification>(bs, 0, -1);
		}

		public bool HasState;

		private GameAccountState _State;

		public bool HasSubscriberId;

		private ulong _SubscriberId;

		public bool HasGameAccountTags;

		private GameAccountFieldTags _GameAccountTags;

		public bool HasSubscriptionCompleted;

		private bool _SubscriptionCompleted;
	}
}
