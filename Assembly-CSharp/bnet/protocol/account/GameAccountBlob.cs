using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace bnet.protocol.account
{
	public class GameAccountBlob : IProtoBuf
	{
		public GameAccountHandle GameAccount { get; set; }

		public void SetGameAccount(GameAccountHandle val)
		{
			this.GameAccount = val;
		}

		public string Name
		{
			get
			{
				return this._Name;
			}
			set
			{
				this._Name = value;
				this.HasName = (value != null);
			}
		}

		public void SetName(string val)
		{
			this.Name = val;
		}

		public uint RealmPermissions
		{
			get
			{
				return this._RealmPermissions;
			}
			set
			{
				this._RealmPermissions = value;
				this.HasRealmPermissions = true;
			}
		}

		public void SetRealmPermissions(uint val)
		{
			this.RealmPermissions = val;
		}

		public uint Status { get; set; }

		public void SetStatus(uint val)
		{
			this.Status = val;
		}

		public ulong Flags
		{
			get
			{
				return this._Flags;
			}
			set
			{
				this._Flags = value;
				this.HasFlags = true;
			}
		}

		public void SetFlags(ulong val)
		{
			this.Flags = val;
		}

		public uint BillingFlags
		{
			get
			{
				return this._BillingFlags;
			}
			set
			{
				this._BillingFlags = value;
				this.HasBillingFlags = true;
			}
		}

		public void SetBillingFlags(uint val)
		{
			this.BillingFlags = val;
		}

		public ulong CacheExpiration { get; set; }

		public void SetCacheExpiration(ulong val)
		{
			this.CacheExpiration = val;
		}

		public ulong SubscriptionExpiration
		{
			get
			{
				return this._SubscriptionExpiration;
			}
			set
			{
				this._SubscriptionExpiration = value;
				this.HasSubscriptionExpiration = true;
			}
		}

		public void SetSubscriptionExpiration(ulong val)
		{
			this.SubscriptionExpiration = val;
		}

		public uint UnitsRemaining
		{
			get
			{
				return this._UnitsRemaining;
			}
			set
			{
				this._UnitsRemaining = value;
				this.HasUnitsRemaining = true;
			}
		}

		public void SetUnitsRemaining(uint val)
		{
			this.UnitsRemaining = val;
		}

		public ulong StatusExpiration
		{
			get
			{
				return this._StatusExpiration;
			}
			set
			{
				this._StatusExpiration = value;
				this.HasStatusExpiration = true;
			}
		}

		public void SetStatusExpiration(ulong val)
		{
			this.StatusExpiration = val;
		}

		public uint BoxLevel
		{
			get
			{
				return this._BoxLevel;
			}
			set
			{
				this._BoxLevel = value;
				this.HasBoxLevel = true;
			}
		}

		public void SetBoxLevel(uint val)
		{
			this.BoxLevel = val;
		}

		public ulong BoxLevelExpiration
		{
			get
			{
				return this._BoxLevelExpiration;
			}
			set
			{
				this._BoxLevelExpiration = value;
				this.HasBoxLevelExpiration = true;
			}
		}

		public void SetBoxLevelExpiration(ulong val)
		{
			this.BoxLevelExpiration = val;
		}

		public List<AccountLicense> Licenses
		{
			get
			{
				return this._Licenses;
			}
			set
			{
				this._Licenses = value;
			}
		}

		public List<AccountLicense> LicensesList
		{
			get
			{
				return this._Licenses;
			}
		}

		public int LicensesCount
		{
			get
			{
				return this._Licenses.Count;
			}
		}

		public void AddLicenses(AccountLicense val)
		{
			this._Licenses.Add(val);
		}

		public void ClearLicenses()
		{
			this._Licenses.Clear();
		}

		public void SetLicenses(List<AccountLicense> val)
		{
			this.Licenses = val;
		}

		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.GameAccount.GetHashCode();
			if (this.HasName)
			{
				num ^= this.Name.GetHashCode();
			}
			if (this.HasRealmPermissions)
			{
				num ^= this.RealmPermissions.GetHashCode();
			}
			num ^= this.Status.GetHashCode();
			if (this.HasFlags)
			{
				num ^= this.Flags.GetHashCode();
			}
			if (this.HasBillingFlags)
			{
				num ^= this.BillingFlags.GetHashCode();
			}
			num ^= this.CacheExpiration.GetHashCode();
			if (this.HasSubscriptionExpiration)
			{
				num ^= this.SubscriptionExpiration.GetHashCode();
			}
			if (this.HasUnitsRemaining)
			{
				num ^= this.UnitsRemaining.GetHashCode();
			}
			if (this.HasStatusExpiration)
			{
				num ^= this.StatusExpiration.GetHashCode();
			}
			if (this.HasBoxLevel)
			{
				num ^= this.BoxLevel.GetHashCode();
			}
			if (this.HasBoxLevelExpiration)
			{
				num ^= this.BoxLevelExpiration.GetHashCode();
			}
			foreach (AccountLicense accountLicense in this.Licenses)
			{
				num ^= accountLicense.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			GameAccountBlob gameAccountBlob = obj as GameAccountBlob;
			if (gameAccountBlob == null)
			{
				return false;
			}
			if (!this.GameAccount.Equals(gameAccountBlob.GameAccount))
			{
				return false;
			}
			if (this.HasName != gameAccountBlob.HasName || (this.HasName && !this.Name.Equals(gameAccountBlob.Name)))
			{
				return false;
			}
			if (this.HasRealmPermissions != gameAccountBlob.HasRealmPermissions || (this.HasRealmPermissions && !this.RealmPermissions.Equals(gameAccountBlob.RealmPermissions)))
			{
				return false;
			}
			if (!this.Status.Equals(gameAccountBlob.Status))
			{
				return false;
			}
			if (this.HasFlags != gameAccountBlob.HasFlags || (this.HasFlags && !this.Flags.Equals(gameAccountBlob.Flags)))
			{
				return false;
			}
			if (this.HasBillingFlags != gameAccountBlob.HasBillingFlags || (this.HasBillingFlags && !this.BillingFlags.Equals(gameAccountBlob.BillingFlags)))
			{
				return false;
			}
			if (!this.CacheExpiration.Equals(gameAccountBlob.CacheExpiration))
			{
				return false;
			}
			if (this.HasSubscriptionExpiration != gameAccountBlob.HasSubscriptionExpiration || (this.HasSubscriptionExpiration && !this.SubscriptionExpiration.Equals(gameAccountBlob.SubscriptionExpiration)))
			{
				return false;
			}
			if (this.HasUnitsRemaining != gameAccountBlob.HasUnitsRemaining || (this.HasUnitsRemaining && !this.UnitsRemaining.Equals(gameAccountBlob.UnitsRemaining)))
			{
				return false;
			}
			if (this.HasStatusExpiration != gameAccountBlob.HasStatusExpiration || (this.HasStatusExpiration && !this.StatusExpiration.Equals(gameAccountBlob.StatusExpiration)))
			{
				return false;
			}
			if (this.HasBoxLevel != gameAccountBlob.HasBoxLevel || (this.HasBoxLevel && !this.BoxLevel.Equals(gameAccountBlob.BoxLevel)))
			{
				return false;
			}
			if (this.HasBoxLevelExpiration != gameAccountBlob.HasBoxLevelExpiration || (this.HasBoxLevelExpiration && !this.BoxLevelExpiration.Equals(gameAccountBlob.BoxLevelExpiration)))
			{
				return false;
			}
			if (this.Licenses.Count != gameAccountBlob.Licenses.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Licenses.Count; i++)
			{
				if (!this.Licenses[i].Equals(gameAccountBlob.Licenses[i]))
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

		public static GameAccountBlob ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GameAccountBlob>(bs, 0, -1);
		}

		public void Deserialize(Stream stream)
		{
			GameAccountBlob.Deserialize(stream, this);
		}

		public static GameAccountBlob Deserialize(Stream stream, GameAccountBlob instance)
		{
			return GameAccountBlob.Deserialize(stream, instance, -1L);
		}

		public static GameAccountBlob DeserializeLengthDelimited(Stream stream)
		{
			GameAccountBlob gameAccountBlob = new GameAccountBlob();
			GameAccountBlob.DeserializeLengthDelimited(stream, gameAccountBlob);
			return gameAccountBlob;
		}

		public static GameAccountBlob DeserializeLengthDelimited(Stream stream, GameAccountBlob instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GameAccountBlob.Deserialize(stream, instance, num);
		}

		public static GameAccountBlob Deserialize(Stream stream, GameAccountBlob instance, long limit)
		{
			instance.Name = string.Empty;
			instance.RealmPermissions = 0u;
			instance.Flags = 0UL;
			instance.BillingFlags = 0u;
			if (instance.Licenses == null)
			{
				instance.Licenses = new List<AccountLicense>();
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
						if (num != 24)
						{
							if (num != 32)
							{
								if (num != 40)
								{
									if (num != 48)
									{
										if (num != 56)
										{
											if (num != 80)
											{
												if (num != 88)
												{
													if (num != 96)
													{
														if (num != 104)
														{
															if (num != 112)
															{
																Key key = ProtocolParser.ReadKey((byte)num, stream);
																uint field = key.Field;
																if (field == 0u)
																{
																	throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
																}
																if (field != 20u)
																{
																	ProtocolParser.SkipKey(stream, key);
																}
																else if (key.WireType == Wire.LengthDelimited)
																{
																	instance.Licenses.Add(AccountLicense.DeserializeLengthDelimited(stream));
																}
															}
															else
															{
																instance.BoxLevelExpiration = ProtocolParser.ReadUInt64(stream);
															}
														}
														else
														{
															instance.BoxLevel = ProtocolParser.ReadUInt32(stream);
														}
													}
													else
													{
														instance.StatusExpiration = ProtocolParser.ReadUInt64(stream);
													}
												}
												else
												{
													instance.UnitsRemaining = ProtocolParser.ReadUInt32(stream);
												}
											}
											else
											{
												instance.SubscriptionExpiration = ProtocolParser.ReadUInt64(stream);
											}
										}
										else
										{
											instance.CacheExpiration = ProtocolParser.ReadUInt64(stream);
										}
									}
									else
									{
										instance.BillingFlags = ProtocolParser.ReadUInt32(stream);
									}
								}
								else
								{
									instance.Flags = ProtocolParser.ReadUInt64(stream);
								}
							}
							else
							{
								instance.Status = ProtocolParser.ReadUInt32(stream);
							}
						}
						else
						{
							instance.RealmPermissions = ProtocolParser.ReadUInt32(stream);
						}
					}
					else
					{
						instance.Name = ProtocolParser.ReadString(stream);
					}
				}
				else if (instance.GameAccount == null)
				{
					instance.GameAccount = GameAccountHandle.DeserializeLengthDelimited(stream);
				}
				else
				{
					GameAccountHandle.DeserializeLengthDelimited(stream, instance.GameAccount);
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
			GameAccountBlob.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, GameAccountBlob instance)
		{
			if (instance.GameAccount == null)
			{
				throw new ArgumentNullException("GameAccount", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteUInt32(stream, instance.GameAccount.GetSerializedSize());
			GameAccountHandle.Serialize(stream, instance.GameAccount);
			if (instance.HasName)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Name));
			}
			if (instance.HasRealmPermissions)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt32(stream, instance.RealmPermissions);
			}
			stream.WriteByte(32);
			ProtocolParser.WriteUInt32(stream, instance.Status);
			if (instance.HasFlags)
			{
				stream.WriteByte(40);
				ProtocolParser.WriteUInt64(stream, instance.Flags);
			}
			if (instance.HasBillingFlags)
			{
				stream.WriteByte(48);
				ProtocolParser.WriteUInt32(stream, instance.BillingFlags);
			}
			stream.WriteByte(56);
			ProtocolParser.WriteUInt64(stream, instance.CacheExpiration);
			if (instance.HasSubscriptionExpiration)
			{
				stream.WriteByte(80);
				ProtocolParser.WriteUInt64(stream, instance.SubscriptionExpiration);
			}
			if (instance.HasUnitsRemaining)
			{
				stream.WriteByte(88);
				ProtocolParser.WriteUInt32(stream, instance.UnitsRemaining);
			}
			if (instance.HasStatusExpiration)
			{
				stream.WriteByte(96);
				ProtocolParser.WriteUInt64(stream, instance.StatusExpiration);
			}
			if (instance.HasBoxLevel)
			{
				stream.WriteByte(104);
				ProtocolParser.WriteUInt32(stream, instance.BoxLevel);
			}
			if (instance.HasBoxLevelExpiration)
			{
				stream.WriteByte(112);
				ProtocolParser.WriteUInt64(stream, instance.BoxLevelExpiration);
			}
			if (instance.Licenses.Count > 0)
			{
				foreach (AccountLicense accountLicense in instance.Licenses)
				{
					stream.WriteByte(162);
					stream.WriteByte(1);
					ProtocolParser.WriteUInt32(stream, accountLicense.GetSerializedSize());
					AccountLicense.Serialize(stream, accountLicense);
				}
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			uint serializedSize = this.GameAccount.GetSerializedSize();
			num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			if (this.HasName)
			{
				num += 1u;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.Name);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (this.HasRealmPermissions)
			{
				num += 1u;
				num += ProtocolParser.SizeOfUInt32(this.RealmPermissions);
			}
			num += ProtocolParser.SizeOfUInt32(this.Status);
			if (this.HasFlags)
			{
				num += 1u;
				num += ProtocolParser.SizeOfUInt64(this.Flags);
			}
			if (this.HasBillingFlags)
			{
				num += 1u;
				num += ProtocolParser.SizeOfUInt32(this.BillingFlags);
			}
			num += ProtocolParser.SizeOfUInt64(this.CacheExpiration);
			if (this.HasSubscriptionExpiration)
			{
				num += 1u;
				num += ProtocolParser.SizeOfUInt64(this.SubscriptionExpiration);
			}
			if (this.HasUnitsRemaining)
			{
				num += 1u;
				num += ProtocolParser.SizeOfUInt32(this.UnitsRemaining);
			}
			if (this.HasStatusExpiration)
			{
				num += 1u;
				num += ProtocolParser.SizeOfUInt64(this.StatusExpiration);
			}
			if (this.HasBoxLevel)
			{
				num += 1u;
				num += ProtocolParser.SizeOfUInt32(this.BoxLevel);
			}
			if (this.HasBoxLevelExpiration)
			{
				num += 1u;
				num += ProtocolParser.SizeOfUInt64(this.BoxLevelExpiration);
			}
			if (this.Licenses.Count > 0)
			{
				foreach (AccountLicense accountLicense in this.Licenses)
				{
					num += 2u;
					uint serializedSize2 = accountLicense.GetSerializedSize();
					num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
				}
			}
			num += 3u;
			return num;
		}

		public bool HasName;

		private string _Name;

		public bool HasRealmPermissions;

		private uint _RealmPermissions;

		public bool HasFlags;

		private ulong _Flags;

		public bool HasBillingFlags;

		private uint _BillingFlags;

		public bool HasSubscriptionExpiration;

		private ulong _SubscriptionExpiration;

		public bool HasUnitsRemaining;

		private uint _UnitsRemaining;

		public bool HasStatusExpiration;

		private ulong _StatusExpiration;

		public bool HasBoxLevel;

		private uint _BoxLevel;

		public bool HasBoxLevelExpiration;

		private ulong _BoxLevelExpiration;

		private List<AccountLicense> _Licenses = new List<AccountLicense>();
	}
}
