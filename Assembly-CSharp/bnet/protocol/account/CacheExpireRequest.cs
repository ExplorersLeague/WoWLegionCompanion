using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace bnet.protocol.account
{
	public class CacheExpireRequest : IProtoBuf
	{
		public List<AccountId> Account
		{
			get
			{
				return this._Account;
			}
			set
			{
				this._Account = value;
			}
		}

		public List<AccountId> AccountList
		{
			get
			{
				return this._Account;
			}
		}

		public int AccountCount
		{
			get
			{
				return this._Account.Count;
			}
		}

		public void AddAccount(AccountId val)
		{
			this._Account.Add(val);
		}

		public void ClearAccount()
		{
			this._Account.Clear();
		}

		public void SetAccount(List<AccountId> val)
		{
			this.Account = val;
		}

		public List<GameAccountHandle> GameAccount
		{
			get
			{
				return this._GameAccount;
			}
			set
			{
				this._GameAccount = value;
			}
		}

		public List<GameAccountHandle> GameAccountList
		{
			get
			{
				return this._GameAccount;
			}
		}

		public int GameAccountCount
		{
			get
			{
				return this._GameAccount.Count;
			}
		}

		public void AddGameAccount(GameAccountHandle val)
		{
			this._GameAccount.Add(val);
		}

		public void ClearGameAccount()
		{
			this._GameAccount.Clear();
		}

		public void SetGameAccount(List<GameAccountHandle> val)
		{
			this.GameAccount = val;
		}

		public List<string> Email
		{
			get
			{
				return this._Email;
			}
			set
			{
				this._Email = value;
			}
		}

		public List<string> EmailList
		{
			get
			{
				return this._Email;
			}
		}

		public int EmailCount
		{
			get
			{
				return this._Email.Count;
			}
		}

		public void AddEmail(string val)
		{
			this._Email.Add(val);
		}

		public void ClearEmail()
		{
			this._Email.Clear();
		}

		public void SetEmail(List<string> val)
		{
			this.Email = val;
		}

		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			foreach (AccountId accountId in this.Account)
			{
				num ^= accountId.GetHashCode();
			}
			foreach (GameAccountHandle gameAccountHandle in this.GameAccount)
			{
				num ^= gameAccountHandle.GetHashCode();
			}
			foreach (string text in this.Email)
			{
				num ^= text.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			CacheExpireRequest cacheExpireRequest = obj as CacheExpireRequest;
			if (cacheExpireRequest == null)
			{
				return false;
			}
			if (this.Account.Count != cacheExpireRequest.Account.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Account.Count; i++)
			{
				if (!this.Account[i].Equals(cacheExpireRequest.Account[i]))
				{
					return false;
				}
			}
			if (this.GameAccount.Count != cacheExpireRequest.GameAccount.Count)
			{
				return false;
			}
			for (int j = 0; j < this.GameAccount.Count; j++)
			{
				if (!this.GameAccount[j].Equals(cacheExpireRequest.GameAccount[j]))
				{
					return false;
				}
			}
			if (this.Email.Count != cacheExpireRequest.Email.Count)
			{
				return false;
			}
			for (int k = 0; k < this.Email.Count; k++)
			{
				if (!this.Email[k].Equals(cacheExpireRequest.Email[k]))
				{
					return false;
				}
			}
			return true;
		}

		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		public static CacheExpireRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<CacheExpireRequest>(bs, 0, -1);
		}

		public void Deserialize(Stream stream)
		{
			CacheExpireRequest.Deserialize(stream, this);
		}

		public static CacheExpireRequest Deserialize(Stream stream, CacheExpireRequest instance)
		{
			return CacheExpireRequest.Deserialize(stream, instance, -1L);
		}

		public static CacheExpireRequest DeserializeLengthDelimited(Stream stream)
		{
			CacheExpireRequest cacheExpireRequest = new CacheExpireRequest();
			CacheExpireRequest.DeserializeLengthDelimited(stream, cacheExpireRequest);
			return cacheExpireRequest;
		}

		public static CacheExpireRequest DeserializeLengthDelimited(Stream stream, CacheExpireRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return CacheExpireRequest.Deserialize(stream, instance, num);
		}

		public static CacheExpireRequest Deserialize(Stream stream, CacheExpireRequest instance, long limit)
		{
			if (instance.Account == null)
			{
				instance.Account = new List<AccountId>();
			}
			if (instance.GameAccount == null)
			{
				instance.GameAccount = new List<GameAccountHandle>();
			}
			if (instance.Email == null)
			{
				instance.Email = new List<string>();
			}
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
						if (num != 26)
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
							instance.Email.Add(ProtocolParser.ReadString(stream));
						}
					}
					else
					{
						instance.GameAccount.Add(GameAccountHandle.DeserializeLengthDelimited(stream));
					}
				}
				else
				{
					instance.Account.Add(AccountId.DeserializeLengthDelimited(stream));
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
			CacheExpireRequest.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, CacheExpireRequest instance)
		{
			if (instance.Account.Count > 0)
			{
				foreach (AccountId accountId in instance.Account)
				{
					stream.WriteByte(10);
					ProtocolParser.WriteUInt32(stream, accountId.GetSerializedSize());
					AccountId.Serialize(stream, accountId);
				}
			}
			if (instance.GameAccount.Count > 0)
			{
				foreach (GameAccountHandle gameAccountHandle in instance.GameAccount)
				{
					stream.WriteByte(18);
					ProtocolParser.WriteUInt32(stream, gameAccountHandle.GetSerializedSize());
					GameAccountHandle.Serialize(stream, gameAccountHandle);
				}
			}
			if (instance.Email.Count > 0)
			{
				foreach (string s in instance.Email)
				{
					stream.WriteByte(26);
					ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(s));
				}
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (this.Account.Count > 0)
			{
				foreach (AccountId accountId in this.Account)
				{
					num += 1u;
					uint serializedSize = accountId.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			if (this.GameAccount.Count > 0)
			{
				foreach (GameAccountHandle gameAccountHandle in this.GameAccount)
				{
					num += 1u;
					uint serializedSize2 = gameAccountHandle.GetSerializedSize();
					num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
				}
			}
			if (this.Email.Count > 0)
			{
				foreach (string s in this.Email)
				{
					num += 1u;
					uint byteCount = (uint)Encoding.UTF8.GetByteCount(s);
					num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
				}
			}
			return num;
		}

		private List<AccountId> _Account = new List<AccountId>();

		private List<GameAccountHandle> _GameAccount = new List<GameAccountHandle>();

		private List<string> _Email = new List<string>();
	}
}
