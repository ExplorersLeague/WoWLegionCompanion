using System;
using System.IO;

namespace bnet.protocol.account
{
	public class AccountStateTagged : IProtoBuf
	{
		public void Deserialize(Stream stream)
		{
			AccountStateTagged.Deserialize(stream, this);
		}

		public static AccountStateTagged Deserialize(Stream stream, AccountStateTagged instance)
		{
			return AccountStateTagged.Deserialize(stream, instance, -1L);
		}

		public static AccountStateTagged DeserializeLengthDelimited(Stream stream)
		{
			AccountStateTagged accountStateTagged = new AccountStateTagged();
			AccountStateTagged.DeserializeLengthDelimited(stream, accountStateTagged);
			return accountStateTagged;
		}

		public static AccountStateTagged DeserializeLengthDelimited(Stream stream, AccountStateTagged instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return AccountStateTagged.Deserialize(stream, instance, num);
		}

		public static AccountStateTagged Deserialize(Stream stream, AccountStateTagged instance, long limit)
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
				else
				{
					int num2 = num;
					if (num2 != 10)
					{
						if (num2 != 18)
						{
							Key key = ProtocolParser.ReadKey((byte)num, stream);
							uint field = key.Field;
							if (field == 0u)
							{
								throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
							}
							ProtocolParser.SkipKey(stream, key);
						}
						else if (instance.Tags == null)
						{
							instance.Tags = AccountFieldTags.DeserializeLengthDelimited(stream);
						}
						else
						{
							AccountFieldTags.DeserializeLengthDelimited(stream, instance.Tags);
						}
					}
					else if (instance.AccountState == null)
					{
						instance.AccountState = AccountState.DeserializeLengthDelimited(stream);
					}
					else
					{
						AccountState.DeserializeLengthDelimited(stream, instance.AccountState);
					}
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
			AccountStateTagged.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, AccountStateTagged instance)
		{
			if (instance.HasAccountState)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.AccountState.GetSerializedSize());
				AccountState.Serialize(stream, instance.AccountState);
			}
			if (instance.HasTags)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.Tags.GetSerializedSize());
				AccountFieldTags.Serialize(stream, instance.Tags);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (this.HasAccountState)
			{
				num += 1u;
				uint serializedSize = this.AccountState.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasTags)
			{
				num += 1u;
				uint serializedSize2 = this.Tags.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			return num;
		}

		public AccountState AccountState
		{
			get
			{
				return this._AccountState;
			}
			set
			{
				this._AccountState = value;
				this.HasAccountState = (value != null);
			}
		}

		public void SetAccountState(AccountState val)
		{
			this.AccountState = val;
		}

		public AccountFieldTags Tags
		{
			get
			{
				return this._Tags;
			}
			set
			{
				this._Tags = value;
				this.HasTags = (value != null);
			}
		}

		public void SetTags(AccountFieldTags val)
		{
			this.Tags = val;
		}

		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasAccountState)
			{
				num ^= this.AccountState.GetHashCode();
			}
			if (this.HasTags)
			{
				num ^= this.Tags.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			AccountStateTagged accountStateTagged = obj as AccountStateTagged;
			return accountStateTagged != null && this.HasAccountState == accountStateTagged.HasAccountState && (!this.HasAccountState || this.AccountState.Equals(accountStateTagged.AccountState)) && this.HasTags == accountStateTagged.HasTags && (!this.HasTags || this.Tags.Equals(accountStateTagged.Tags));
		}

		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		public static AccountStateTagged ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<AccountStateTagged>(bs, 0, -1);
		}

		public bool HasAccountState;

		private AccountState _AccountState;

		public bool HasTags;

		private AccountFieldTags _Tags;
	}
}
