using System;
using System.IO;

namespace bnet.protocol.account
{
	public class AccountStateNotification : IProtoBuf
	{
		public AccountState State
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

		public void SetState(AccountState val)
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

		public AccountFieldTags AccountTags
		{
			get
			{
				return this._AccountTags;
			}
			set
			{
				this._AccountTags = value;
				this.HasAccountTags = (value != null);
			}
		}

		public void SetAccountTags(AccountFieldTags val)
		{
			this.AccountTags = val;
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
			if (this.HasAccountTags)
			{
				num ^= this.AccountTags.GetHashCode();
			}
			if (this.HasSubscriptionCompleted)
			{
				num ^= this.SubscriptionCompleted.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			AccountStateNotification accountStateNotification = obj as AccountStateNotification;
			return accountStateNotification != null && this.HasState == accountStateNotification.HasState && (!this.HasState || this.State.Equals(accountStateNotification.State)) && this.HasSubscriberId == accountStateNotification.HasSubscriberId && (!this.HasSubscriberId || this.SubscriberId.Equals(accountStateNotification.SubscriberId)) && this.HasAccountTags == accountStateNotification.HasAccountTags && (!this.HasAccountTags || this.AccountTags.Equals(accountStateNotification.AccountTags)) && this.HasSubscriptionCompleted == accountStateNotification.HasSubscriptionCompleted && (!this.HasSubscriptionCompleted || this.SubscriptionCompleted.Equals(accountStateNotification.SubscriptionCompleted));
		}

		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		public static AccountStateNotification ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<AccountStateNotification>(bs, 0, -1);
		}

		public void Deserialize(Stream stream)
		{
			AccountStateNotification.Deserialize(stream, this);
		}

		public static AccountStateNotification Deserialize(Stream stream, AccountStateNotification instance)
		{
			return AccountStateNotification.Deserialize(stream, instance, -1L);
		}

		public static AccountStateNotification DeserializeLengthDelimited(Stream stream)
		{
			AccountStateNotification accountStateNotification = new AccountStateNotification();
			AccountStateNotification.DeserializeLengthDelimited(stream, accountStateNotification);
			return accountStateNotification;
		}

		public static AccountStateNotification DeserializeLengthDelimited(Stream stream, AccountStateNotification instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return AccountStateNotification.Deserialize(stream, instance, num);
		}

		public static AccountStateNotification Deserialize(Stream stream, AccountStateNotification instance, long limit)
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
						else if (instance.AccountTags == null)
						{
							instance.AccountTags = AccountFieldTags.DeserializeLengthDelimited(stream);
						}
						else
						{
							AccountFieldTags.DeserializeLengthDelimited(stream, instance.AccountTags);
						}
					}
					else
					{
						instance.SubscriberId = ProtocolParser.ReadUInt64(stream);
					}
				}
				else if (instance.State == null)
				{
					instance.State = AccountState.DeserializeLengthDelimited(stream);
				}
				else
				{
					AccountState.DeserializeLengthDelimited(stream, instance.State);
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
			AccountStateNotification.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, AccountStateNotification instance)
		{
			if (instance.HasState)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.State.GetSerializedSize());
				AccountState.Serialize(stream, instance.State);
			}
			if (instance.HasSubscriberId)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, instance.SubscriberId);
			}
			if (instance.HasAccountTags)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteUInt32(stream, instance.AccountTags.GetSerializedSize());
				AccountFieldTags.Serialize(stream, instance.AccountTags);
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
			if (this.HasAccountTags)
			{
				num += 1u;
				uint serializedSize2 = this.AccountTags.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			if (this.HasSubscriptionCompleted)
			{
				num += 1u;
				num += 1u;
			}
			return num;
		}

		public bool HasState;

		private AccountState _State;

		public bool HasSubscriberId;

		private ulong _SubscriberId;

		public bool HasAccountTags;

		private AccountFieldTags _AccountTags;

		public bool HasSubscriptionCompleted;

		private bool _SubscriptionCompleted;
	}
}
