using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace bnet.protocol.authentication
{
	public class LogonResult : IProtoBuf
	{
		public uint ErrorCode { get; set; }

		public void SetErrorCode(uint val)
		{
			this.ErrorCode = val;
		}

		public EntityId Account
		{
			get
			{
				return this._Account;
			}
			set
			{
				this._Account = value;
				this.HasAccount = (value != null);
			}
		}

		public void SetAccount(EntityId val)
		{
			this.Account = val;
		}

		public List<EntityId> GameAccount
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

		public List<EntityId> GameAccountList
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

		public void AddGameAccount(EntityId val)
		{
			this._GameAccount.Add(val);
		}

		public void ClearGameAccount()
		{
			this._GameAccount.Clear();
		}

		public void SetGameAccount(List<EntityId> val)
		{
			this.GameAccount = val;
		}

		public string Email
		{
			get
			{
				return this._Email;
			}
			set
			{
				this._Email = value;
				this.HasEmail = (value != null);
			}
		}

		public void SetEmail(string val)
		{
			this.Email = val;
		}

		public List<uint> AvailableRegion
		{
			get
			{
				return this._AvailableRegion;
			}
			set
			{
				this._AvailableRegion = value;
			}
		}

		public List<uint> AvailableRegionList
		{
			get
			{
				return this._AvailableRegion;
			}
		}

		public int AvailableRegionCount
		{
			get
			{
				return this._AvailableRegion.Count;
			}
		}

		public void AddAvailableRegion(uint val)
		{
			this._AvailableRegion.Add(val);
		}

		public void ClearAvailableRegion()
		{
			this._AvailableRegion.Clear();
		}

		public void SetAvailableRegion(List<uint> val)
		{
			this.AvailableRegion = val;
		}

		public uint ConnectedRegion
		{
			get
			{
				return this._ConnectedRegion;
			}
			set
			{
				this._ConnectedRegion = value;
				this.HasConnectedRegion = true;
			}
		}

		public void SetConnectedRegion(uint val)
		{
			this.ConnectedRegion = val;
		}

		public string BattleTag
		{
			get
			{
				return this._BattleTag;
			}
			set
			{
				this._BattleTag = value;
				this.HasBattleTag = (value != null);
			}
		}

		public void SetBattleTag(string val)
		{
			this.BattleTag = val;
		}

		public string GeoipCountry
		{
			get
			{
				return this._GeoipCountry;
			}
			set
			{
				this._GeoipCountry = value;
				this.HasGeoipCountry = (value != null);
			}
		}

		public void SetGeoipCountry(string val)
		{
			this.GeoipCountry = val;
		}

		public byte[] SessionKey
		{
			get
			{
				return this._SessionKey;
			}
			set
			{
				this._SessionKey = value;
				this.HasSessionKey = (value != null);
			}
		}

		public void SetSessionKey(byte[] val)
		{
			this.SessionKey = val;
		}

		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.ErrorCode.GetHashCode();
			if (this.HasAccount)
			{
				num ^= this.Account.GetHashCode();
			}
			foreach (EntityId entityId in this.GameAccount)
			{
				num ^= entityId.GetHashCode();
			}
			if (this.HasEmail)
			{
				num ^= this.Email.GetHashCode();
			}
			foreach (uint num2 in this.AvailableRegion)
			{
				num ^= num2.GetHashCode();
			}
			if (this.HasConnectedRegion)
			{
				num ^= this.ConnectedRegion.GetHashCode();
			}
			if (this.HasBattleTag)
			{
				num ^= this.BattleTag.GetHashCode();
			}
			if (this.HasGeoipCountry)
			{
				num ^= this.GeoipCountry.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			LogonResult logonResult = obj as LogonResult;
			if (logonResult == null)
			{
				return false;
			}
			if (!this.ErrorCode.Equals(logonResult.ErrorCode))
			{
				return false;
			}
			if (this.HasAccount != logonResult.HasAccount || (this.HasAccount && !this.Account.Equals(logonResult.Account)))
			{
				return false;
			}
			if (this.GameAccount.Count != logonResult.GameAccount.Count)
			{
				return false;
			}
			for (int i = 0; i < this.GameAccount.Count; i++)
			{
				if (!this.GameAccount[i].Equals(logonResult.GameAccount[i]))
				{
					return false;
				}
			}
			if (this.HasEmail != logonResult.HasEmail || (this.HasEmail && !this.Email.Equals(logonResult.Email)))
			{
				return false;
			}
			if (this.AvailableRegion.Count != logonResult.AvailableRegion.Count)
			{
				return false;
			}
			for (int j = 0; j < this.AvailableRegion.Count; j++)
			{
				if (!this.AvailableRegion[j].Equals(logonResult.AvailableRegion[j]))
				{
					return false;
				}
			}
			return this.HasConnectedRegion == logonResult.HasConnectedRegion && (!this.HasConnectedRegion || this.ConnectedRegion.Equals(logonResult.ConnectedRegion)) && this.HasBattleTag == logonResult.HasBattleTag && (!this.HasBattleTag || this.BattleTag.Equals(logonResult.BattleTag)) && this.HasGeoipCountry == logonResult.HasGeoipCountry && (!this.HasGeoipCountry || this.GeoipCountry.Equals(logonResult.GeoipCountry));
		}

		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		public static LogonResult ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<LogonResult>(bs, 0, -1);
		}

		public void Deserialize(Stream stream)
		{
			LogonResult.Deserialize(stream, this);
		}

		public static LogonResult Deserialize(Stream stream, LogonResult instance)
		{
			return LogonResult.Deserialize(stream, instance, -1L);
		}

		public static LogonResult DeserializeLengthDelimited(Stream stream)
		{
			LogonResult logonResult = new LogonResult();
			LogonResult.DeserializeLengthDelimited(stream, logonResult);
			return logonResult;
		}

		public static LogonResult DeserializeLengthDelimited(Stream stream, LogonResult instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return LogonResult.Deserialize(stream, instance, num);
		}

		public static LogonResult Deserialize(Stream stream, LogonResult instance, long limit)
		{
			if (instance.GameAccount == null)
			{
				instance.GameAccount = new List<EntityId>();
			}
			if (instance.AvailableRegion == null)
			{
				instance.AvailableRegion = new List<uint>();
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
				else if (num != 8)
				{
					if (num != 18)
					{
						if (num != 26)
						{
							if (num != 34)
							{
								if (num != 40)
								{
									if (num != 48)
									{
										if (num != 58)
										{
											if (num != 66)
											{
												if (num != 74)
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
													instance.SessionKey = ProtocolParser.ReadBytes(stream);
												}
											}
											else
											{
												instance.GeoipCountry = ProtocolParser.ReadString(stream);
											}
										}
										else
										{
											instance.BattleTag = ProtocolParser.ReadString(stream);
										}
									}
									else
									{
										instance.ConnectedRegion = ProtocolParser.ReadUInt32(stream);
									}
								}
								else
								{
									instance.AvailableRegion.Add(ProtocolParser.ReadUInt32(stream));
								}
							}
							else
							{
								instance.Email = ProtocolParser.ReadString(stream);
							}
						}
						else
						{
							instance.GameAccount.Add(EntityId.DeserializeLengthDelimited(stream));
						}
					}
					else if (instance.Account == null)
					{
						instance.Account = EntityId.DeserializeLengthDelimited(stream);
					}
					else
					{
						EntityId.DeserializeLengthDelimited(stream, instance.Account);
					}
				}
				else
				{
					instance.ErrorCode = ProtocolParser.ReadUInt32(stream);
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
			LogonResult.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, LogonResult instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt32(stream, instance.ErrorCode);
			if (instance.HasAccount)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.Account.GetSerializedSize());
				EntityId.Serialize(stream, instance.Account);
			}
			if (instance.GameAccount.Count > 0)
			{
				foreach (EntityId entityId in instance.GameAccount)
				{
					stream.WriteByte(26);
					ProtocolParser.WriteUInt32(stream, entityId.GetSerializedSize());
					EntityId.Serialize(stream, entityId);
				}
			}
			if (instance.HasEmail)
			{
				stream.WriteByte(34);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Email));
			}
			if (instance.AvailableRegion.Count > 0)
			{
				foreach (uint val in instance.AvailableRegion)
				{
					stream.WriteByte(40);
					ProtocolParser.WriteUInt32(stream, val);
				}
			}
			if (instance.HasConnectedRegion)
			{
				stream.WriteByte(48);
				ProtocolParser.WriteUInt32(stream, instance.ConnectedRegion);
			}
			if (instance.HasBattleTag)
			{
				stream.WriteByte(58);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.BattleTag));
			}
			if (instance.HasGeoipCountry)
			{
				stream.WriteByte(66);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.GeoipCountry));
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			num += ProtocolParser.SizeOfUInt32(this.ErrorCode);
			if (this.HasAccount)
			{
				num += 1u;
				uint serializedSize = this.Account.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.GameAccount.Count > 0)
			{
				foreach (EntityId entityId in this.GameAccount)
				{
					num += 1u;
					uint serializedSize2 = entityId.GetSerializedSize();
					num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
				}
			}
			if (this.HasEmail)
			{
				num += 1u;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.Email);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (this.AvailableRegion.Count > 0)
			{
				foreach (uint val in this.AvailableRegion)
				{
					num += 1u;
					num += ProtocolParser.SizeOfUInt32(val);
				}
			}
			if (this.HasConnectedRegion)
			{
				num += 1u;
				num += ProtocolParser.SizeOfUInt32(this.ConnectedRegion);
			}
			if (this.HasBattleTag)
			{
				num += 1u;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(this.BattleTag);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			}
			if (this.HasGeoipCountry)
			{
				num += 1u;
				uint byteCount3 = (uint)Encoding.UTF8.GetByteCount(this.GeoipCountry);
				num += ProtocolParser.SizeOfUInt32(byteCount3) + byteCount3;
			}
			num += 1u;
			return num;
		}

		public bool HasAccount;

		private EntityId _Account;

		private List<EntityId> _GameAccount = new List<EntityId>();

		public bool HasEmail;

		private string _Email;

		private List<uint> _AvailableRegion = new List<uint>();

		public bool HasConnectedRegion;

		private uint _ConnectedRegion;

		public bool HasBattleTag;

		private string _BattleTag;

		public bool HasGeoipCountry;

		private string _GeoipCountry;

		public bool HasSessionKey;

		private byte[] _SessionKey;
	}
}
