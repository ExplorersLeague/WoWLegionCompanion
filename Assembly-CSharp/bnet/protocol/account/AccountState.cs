using System;
using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.account
{
	public class AccountState : IProtoBuf
	{
		public void Deserialize(Stream stream)
		{
			AccountState.Deserialize(stream, this);
		}

		public static AccountState Deserialize(Stream stream, AccountState instance)
		{
			return AccountState.Deserialize(stream, instance, -1L);
		}

		public static AccountState DeserializeLengthDelimited(Stream stream)
		{
			AccountState accountState = new AccountState();
			AccountState.DeserializeLengthDelimited(stream, accountState);
			return accountState;
		}

		public static AccountState DeserializeLengthDelimited(Stream stream, AccountState instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return AccountState.Deserialize(stream, instance, num);
		}

		public static AccountState Deserialize(Stream stream, AccountState instance, long limit)
		{
			if (instance.GameLevelInfo == null)
			{
				instance.GameLevelInfo = new List<GameLevelInfo>();
			}
			if (instance.GameStatus == null)
			{
				instance.GameStatus = new List<GameStatus>();
			}
			if (instance.GameAccounts == null)
			{
				instance.GameAccounts = new List<GameAccountList>();
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
							if (num != 42)
							{
								if (num != 50)
								{
									if (num != 58)
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
										instance.GameAccounts.Add(GameAccountList.DeserializeLengthDelimited(stream));
									}
								}
								else
								{
									instance.GameStatus.Add(bnet.protocol.account.GameStatus.DeserializeLengthDelimited(stream));
								}
							}
							else
							{
								instance.GameLevelInfo.Add(bnet.protocol.account.GameLevelInfo.DeserializeLengthDelimited(stream));
							}
						}
						else if (instance.ParentalControlInfo == null)
						{
							instance.ParentalControlInfo = ParentalControlInfo.DeserializeLengthDelimited(stream);
						}
						else
						{
							ParentalControlInfo.DeserializeLengthDelimited(stream, instance.ParentalControlInfo);
						}
					}
					else if (instance.PrivacyInfo == null)
					{
						instance.PrivacyInfo = PrivacyInfo.DeserializeLengthDelimited(stream);
					}
					else
					{
						PrivacyInfo.DeserializeLengthDelimited(stream, instance.PrivacyInfo);
					}
				}
				else if (instance.AccountLevelInfo == null)
				{
					instance.AccountLevelInfo = AccountLevelInfo.DeserializeLengthDelimited(stream);
				}
				else
				{
					AccountLevelInfo.DeserializeLengthDelimited(stream, instance.AccountLevelInfo);
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
			AccountState.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, AccountState instance)
		{
			if (instance.HasAccountLevelInfo)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.AccountLevelInfo.GetSerializedSize());
				AccountLevelInfo.Serialize(stream, instance.AccountLevelInfo);
			}
			if (instance.HasPrivacyInfo)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.PrivacyInfo.GetSerializedSize());
				PrivacyInfo.Serialize(stream, instance.PrivacyInfo);
			}
			if (instance.HasParentalControlInfo)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteUInt32(stream, instance.ParentalControlInfo.GetSerializedSize());
				ParentalControlInfo.Serialize(stream, instance.ParentalControlInfo);
			}
			if (instance.GameLevelInfo.Count > 0)
			{
				foreach (GameLevelInfo gameLevelInfo in instance.GameLevelInfo)
				{
					stream.WriteByte(42);
					ProtocolParser.WriteUInt32(stream, gameLevelInfo.GetSerializedSize());
					bnet.protocol.account.GameLevelInfo.Serialize(stream, gameLevelInfo);
				}
			}
			if (instance.GameStatus.Count > 0)
			{
				foreach (GameStatus gameStatus in instance.GameStatus)
				{
					stream.WriteByte(50);
					ProtocolParser.WriteUInt32(stream, gameStatus.GetSerializedSize());
					bnet.protocol.account.GameStatus.Serialize(stream, gameStatus);
				}
			}
			if (instance.GameAccounts.Count > 0)
			{
				foreach (GameAccountList gameAccountList in instance.GameAccounts)
				{
					stream.WriteByte(58);
					ProtocolParser.WriteUInt32(stream, gameAccountList.GetSerializedSize());
					GameAccountList.Serialize(stream, gameAccountList);
				}
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (this.HasAccountLevelInfo)
			{
				num += 1u;
				uint serializedSize = this.AccountLevelInfo.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasPrivacyInfo)
			{
				num += 1u;
				uint serializedSize2 = this.PrivacyInfo.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			if (this.HasParentalControlInfo)
			{
				num += 1u;
				uint serializedSize3 = this.ParentalControlInfo.GetSerializedSize();
				num += serializedSize3 + ProtocolParser.SizeOfUInt32(serializedSize3);
			}
			if (this.GameLevelInfo.Count > 0)
			{
				foreach (GameLevelInfo gameLevelInfo in this.GameLevelInfo)
				{
					num += 1u;
					uint serializedSize4 = gameLevelInfo.GetSerializedSize();
					num += serializedSize4 + ProtocolParser.SizeOfUInt32(serializedSize4);
				}
			}
			if (this.GameStatus.Count > 0)
			{
				foreach (GameStatus gameStatus in this.GameStatus)
				{
					num += 1u;
					uint serializedSize5 = gameStatus.GetSerializedSize();
					num += serializedSize5 + ProtocolParser.SizeOfUInt32(serializedSize5);
				}
			}
			if (this.GameAccounts.Count > 0)
			{
				foreach (GameAccountList gameAccountList in this.GameAccounts)
				{
					num += 1u;
					uint serializedSize6 = gameAccountList.GetSerializedSize();
					num += serializedSize6 + ProtocolParser.SizeOfUInt32(serializedSize6);
				}
			}
			return num;
		}

		public AccountLevelInfo AccountLevelInfo
		{
			get
			{
				return this._AccountLevelInfo;
			}
			set
			{
				this._AccountLevelInfo = value;
				this.HasAccountLevelInfo = (value != null);
			}
		}

		public void SetAccountLevelInfo(AccountLevelInfo val)
		{
			this.AccountLevelInfo = val;
		}

		public PrivacyInfo PrivacyInfo
		{
			get
			{
				return this._PrivacyInfo;
			}
			set
			{
				this._PrivacyInfo = value;
				this.HasPrivacyInfo = (value != null);
			}
		}

		public void SetPrivacyInfo(PrivacyInfo val)
		{
			this.PrivacyInfo = val;
		}

		public ParentalControlInfo ParentalControlInfo
		{
			get
			{
				return this._ParentalControlInfo;
			}
			set
			{
				this._ParentalControlInfo = value;
				this.HasParentalControlInfo = (value != null);
			}
		}

		public void SetParentalControlInfo(ParentalControlInfo val)
		{
			this.ParentalControlInfo = val;
		}

		public List<GameLevelInfo> GameLevelInfo
		{
			get
			{
				return this._GameLevelInfo;
			}
			set
			{
				this._GameLevelInfo = value;
			}
		}

		public List<GameLevelInfo> GameLevelInfoList
		{
			get
			{
				return this._GameLevelInfo;
			}
		}

		public int GameLevelInfoCount
		{
			get
			{
				return this._GameLevelInfo.Count;
			}
		}

		public void AddGameLevelInfo(GameLevelInfo val)
		{
			this._GameLevelInfo.Add(val);
		}

		public void ClearGameLevelInfo()
		{
			this._GameLevelInfo.Clear();
		}

		public void SetGameLevelInfo(List<GameLevelInfo> val)
		{
			this.GameLevelInfo = val;
		}

		public List<GameStatus> GameStatus
		{
			get
			{
				return this._GameStatus;
			}
			set
			{
				this._GameStatus = value;
			}
		}

		public List<GameStatus> GameStatusList
		{
			get
			{
				return this._GameStatus;
			}
		}

		public int GameStatusCount
		{
			get
			{
				return this._GameStatus.Count;
			}
		}

		public void AddGameStatus(GameStatus val)
		{
			this._GameStatus.Add(val);
		}

		public void ClearGameStatus()
		{
			this._GameStatus.Clear();
		}

		public void SetGameStatus(List<GameStatus> val)
		{
			this.GameStatus = val;
		}

		public List<GameAccountList> GameAccounts
		{
			get
			{
				return this._GameAccounts;
			}
			set
			{
				this._GameAccounts = value;
			}
		}

		public List<GameAccountList> GameAccountsList
		{
			get
			{
				return this._GameAccounts;
			}
		}

		public int GameAccountsCount
		{
			get
			{
				return this._GameAccounts.Count;
			}
		}

		public void AddGameAccounts(GameAccountList val)
		{
			this._GameAccounts.Add(val);
		}

		public void ClearGameAccounts()
		{
			this._GameAccounts.Clear();
		}

		public void SetGameAccounts(List<GameAccountList> val)
		{
			this.GameAccounts = val;
		}

		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasAccountLevelInfo)
			{
				num ^= this.AccountLevelInfo.GetHashCode();
			}
			if (this.HasPrivacyInfo)
			{
				num ^= this.PrivacyInfo.GetHashCode();
			}
			if (this.HasParentalControlInfo)
			{
				num ^= this.ParentalControlInfo.GetHashCode();
			}
			foreach (GameLevelInfo gameLevelInfo in this.GameLevelInfo)
			{
				num ^= gameLevelInfo.GetHashCode();
			}
			foreach (GameStatus gameStatus in this.GameStatus)
			{
				num ^= gameStatus.GetHashCode();
			}
			foreach (GameAccountList gameAccountList in this.GameAccounts)
			{
				num ^= gameAccountList.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			AccountState accountState = obj as AccountState;
			if (accountState == null)
			{
				return false;
			}
			if (this.HasAccountLevelInfo != accountState.HasAccountLevelInfo || (this.HasAccountLevelInfo && !this.AccountLevelInfo.Equals(accountState.AccountLevelInfo)))
			{
				return false;
			}
			if (this.HasPrivacyInfo != accountState.HasPrivacyInfo || (this.HasPrivacyInfo && !this.PrivacyInfo.Equals(accountState.PrivacyInfo)))
			{
				return false;
			}
			if (this.HasParentalControlInfo != accountState.HasParentalControlInfo || (this.HasParentalControlInfo && !this.ParentalControlInfo.Equals(accountState.ParentalControlInfo)))
			{
				return false;
			}
			if (this.GameLevelInfo.Count != accountState.GameLevelInfo.Count)
			{
				return false;
			}
			for (int i = 0; i < this.GameLevelInfo.Count; i++)
			{
				if (!this.GameLevelInfo[i].Equals(accountState.GameLevelInfo[i]))
				{
					return false;
				}
			}
			if (this.GameStatus.Count != accountState.GameStatus.Count)
			{
				return false;
			}
			for (int j = 0; j < this.GameStatus.Count; j++)
			{
				if (!this.GameStatus[j].Equals(accountState.GameStatus[j]))
				{
					return false;
				}
			}
			if (this.GameAccounts.Count != accountState.GameAccounts.Count)
			{
				return false;
			}
			for (int k = 0; k < this.GameAccounts.Count; k++)
			{
				if (!this.GameAccounts[k].Equals(accountState.GameAccounts[k]))
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

		public static AccountState ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<AccountState>(bs, 0, -1);
		}

		public bool HasAccountLevelInfo;

		private AccountLevelInfo _AccountLevelInfo;

		public bool HasPrivacyInfo;

		private PrivacyInfo _PrivacyInfo;

		public bool HasParentalControlInfo;

		private ParentalControlInfo _ParentalControlInfo;

		private List<GameLevelInfo> _GameLevelInfo = new List<GameLevelInfo>();

		private List<GameStatus> _GameStatus = new List<GameStatus>();

		private List<GameAccountList> _GameAccounts = new List<GameAccountList>();
	}
}
