using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace bnet.protocol.account
{
	public class AccountBlob : IProtoBuf
	{
		public void Deserialize(Stream stream)
		{
			AccountBlob.Deserialize(stream, this);
		}

		public static AccountBlob Deserialize(Stream stream, AccountBlob instance)
		{
			return AccountBlob.Deserialize(stream, instance, -1L);
		}

		public static AccountBlob DeserializeLengthDelimited(Stream stream)
		{
			AccountBlob accountBlob = new AccountBlob();
			AccountBlob.DeserializeLengthDelimited(stream, accountBlob);
			return accountBlob;
		}

		public static AccountBlob DeserializeLengthDelimited(Stream stream, AccountBlob instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return AccountBlob.Deserialize(stream, instance, num);
		}

		public static AccountBlob Deserialize(Stream stream, AccountBlob instance, long limit)
		{
			BinaryReader binaryReader = new BinaryReader(stream);
			if (instance.Email == null)
			{
				instance.Email = new List<string>();
			}
			if (instance.Licenses == null)
			{
				instance.Licenses = new List<AccountLicense>();
			}
			if (instance.Credentials == null)
			{
				instance.Credentials = new List<AccountCredential>();
			}
			if (instance.AccountLinks == null)
			{
				instance.AccountLinks = new List<GameAccountLink>();
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
				else
				{
					switch (num)
					{
					case 21:
						instance.Id = binaryReader.ReadUInt32();
						break;
					default:
						if (num != 34)
						{
							if (num != 40)
							{
								if (num != 48)
								{
									if (num != 56)
									{
										if (num != 64)
										{
											if (num != 82)
											{
												Key key = ProtocolParser.ReadKey((byte)num, stream);
												uint field = key.Field;
												switch (field)
												{
												case 20u:
													if (key.WireType == Wire.LengthDelimited)
													{
														instance.Licenses.Add(AccountLicense.DeserializeLengthDelimited(stream));
													}
													break;
												case 21u:
													if (key.WireType == Wire.LengthDelimited)
													{
														instance.Credentials.Add(AccountCredential.DeserializeLengthDelimited(stream));
													}
													break;
												case 22u:
													if (key.WireType == Wire.LengthDelimited)
													{
														instance.AccountLinks.Add(GameAccountLink.DeserializeLengthDelimited(stream));
													}
													break;
												case 23u:
													if (key.WireType == Wire.LengthDelimited)
													{
														instance.BattleTag = ProtocolParser.ReadString(stream);
													}
													break;
												default:
													if (field == 0u)
													{
														throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
													}
													ProtocolParser.SkipKey(stream, key);
													break;
												case 25u:
													if (key.WireType == Wire.Fixed32)
													{
														instance.DefaultCurrency = binaryReader.ReadUInt32();
													}
													break;
												case 26u:
													if (key.WireType == Wire.Varint)
													{
														instance.LegalRegion = ProtocolParser.ReadUInt32(stream);
													}
													break;
												case 27u:
													if (key.WireType == Wire.Fixed32)
													{
														instance.LegalLocale = binaryReader.ReadUInt32();
													}
													break;
												case 30u:
													if (key.WireType == Wire.Varint)
													{
														instance.CacheExpiration = ProtocolParser.ReadUInt64(stream);
													}
													break;
												case 31u:
													if (key.WireType == Wire.LengthDelimited)
													{
														if (instance.ParentalControlInfo == null)
														{
															instance.ParentalControlInfo = ParentalControlInfo.DeserializeLengthDelimited(stream);
														}
														else
														{
															ParentalControlInfo.DeserializeLengthDelimited(stream, instance.ParentalControlInfo);
														}
													}
													break;
												case 32u:
													if (key.WireType == Wire.LengthDelimited)
													{
														instance.Country = ProtocolParser.ReadString(stream);
													}
													break;
												case 33u:
													if (key.WireType == Wire.Varint)
													{
														instance.PreferredRegion = ProtocolParser.ReadUInt32(stream);
													}
													break;
												}
											}
											else
											{
												instance.FullName = ProtocolParser.ReadString(stream);
											}
										}
										else
										{
											instance.WhitelistEnd = ProtocolParser.ReadUInt64(stream);
										}
									}
									else
									{
										instance.WhitelistStart = ProtocolParser.ReadUInt64(stream);
									}
								}
								else
								{
									instance.SecureRelease = ProtocolParser.ReadUInt64(stream);
								}
							}
							else
							{
								instance.Flags = ProtocolParser.ReadUInt64(stream);
							}
						}
						else
						{
							instance.Email.Add(ProtocolParser.ReadString(stream));
						}
						break;
					case 24:
						instance.Region = ProtocolParser.ReadUInt32(stream);
						break;
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
			AccountBlob.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, AccountBlob instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			stream.WriteByte(21);
			binaryWriter.Write(instance.Id);
			stream.WriteByte(24);
			ProtocolParser.WriteUInt32(stream, instance.Region);
			if (instance.Email.Count > 0)
			{
				foreach (string s in instance.Email)
				{
					stream.WriteByte(34);
					ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(s));
				}
			}
			stream.WriteByte(40);
			ProtocolParser.WriteUInt64(stream, instance.Flags);
			if (instance.HasSecureRelease)
			{
				stream.WriteByte(48);
				ProtocolParser.WriteUInt64(stream, instance.SecureRelease);
			}
			if (instance.HasWhitelistStart)
			{
				stream.WriteByte(56);
				ProtocolParser.WriteUInt64(stream, instance.WhitelistStart);
			}
			if (instance.HasWhitelistEnd)
			{
				stream.WriteByte(64);
				ProtocolParser.WriteUInt64(stream, instance.WhitelistEnd);
			}
			if (instance.FullName == null)
			{
				throw new ArgumentNullException("FullName", "Required by proto specification.");
			}
			stream.WriteByte(82);
			ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.FullName));
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
			if (instance.Credentials.Count > 0)
			{
				foreach (AccountCredential accountCredential in instance.Credentials)
				{
					stream.WriteByte(170);
					stream.WriteByte(1);
					ProtocolParser.WriteUInt32(stream, accountCredential.GetSerializedSize());
					AccountCredential.Serialize(stream, accountCredential);
				}
			}
			if (instance.AccountLinks.Count > 0)
			{
				foreach (GameAccountLink gameAccountLink in instance.AccountLinks)
				{
					stream.WriteByte(178);
					stream.WriteByte(1);
					ProtocolParser.WriteUInt32(stream, gameAccountLink.GetSerializedSize());
					GameAccountLink.Serialize(stream, gameAccountLink);
				}
			}
			if (instance.HasBattleTag)
			{
				stream.WriteByte(186);
				stream.WriteByte(1);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.BattleTag));
			}
			if (instance.HasDefaultCurrency)
			{
				stream.WriteByte(205);
				stream.WriteByte(1);
				binaryWriter.Write(instance.DefaultCurrency);
			}
			if (instance.HasLegalRegion)
			{
				stream.WriteByte(208);
				stream.WriteByte(1);
				ProtocolParser.WriteUInt32(stream, instance.LegalRegion);
			}
			if (instance.HasLegalLocale)
			{
				stream.WriteByte(221);
				stream.WriteByte(1);
				binaryWriter.Write(instance.LegalLocale);
			}
			stream.WriteByte(240);
			stream.WriteByte(1);
			ProtocolParser.WriteUInt64(stream, instance.CacheExpiration);
			if (instance.HasParentalControlInfo)
			{
				stream.WriteByte(250);
				stream.WriteByte(1);
				ProtocolParser.WriteUInt32(stream, instance.ParentalControlInfo.GetSerializedSize());
				ParentalControlInfo.Serialize(stream, instance.ParentalControlInfo);
			}
			if (instance.HasCountry)
			{
				stream.WriteByte(130);
				stream.WriteByte(2);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Country));
			}
			if (instance.HasPreferredRegion)
			{
				stream.WriteByte(136);
				stream.WriteByte(2);
				ProtocolParser.WriteUInt32(stream, instance.PreferredRegion);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			num += 4u;
			num += ProtocolParser.SizeOfUInt32(this.Region);
			if (this.Email.Count > 0)
			{
				foreach (string s in this.Email)
				{
					num += 1u;
					uint byteCount = (uint)Encoding.UTF8.GetByteCount(s);
					num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
				}
			}
			num += ProtocolParser.SizeOfUInt64(this.Flags);
			if (this.HasSecureRelease)
			{
				num += 1u;
				num += ProtocolParser.SizeOfUInt64(this.SecureRelease);
			}
			if (this.HasWhitelistStart)
			{
				num += 1u;
				num += ProtocolParser.SizeOfUInt64(this.WhitelistStart);
			}
			if (this.HasWhitelistEnd)
			{
				num += 1u;
				num += ProtocolParser.SizeOfUInt64(this.WhitelistEnd);
			}
			uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(this.FullName);
			num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			if (this.Licenses.Count > 0)
			{
				foreach (AccountLicense accountLicense in this.Licenses)
				{
					num += 2u;
					uint serializedSize = accountLicense.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			if (this.Credentials.Count > 0)
			{
				foreach (AccountCredential accountCredential in this.Credentials)
				{
					num += 2u;
					uint serializedSize2 = accountCredential.GetSerializedSize();
					num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
				}
			}
			if (this.AccountLinks.Count > 0)
			{
				foreach (GameAccountLink gameAccountLink in this.AccountLinks)
				{
					num += 2u;
					uint serializedSize3 = gameAccountLink.GetSerializedSize();
					num += serializedSize3 + ProtocolParser.SizeOfUInt32(serializedSize3);
				}
			}
			if (this.HasBattleTag)
			{
				num += 2u;
				uint byteCount3 = (uint)Encoding.UTF8.GetByteCount(this.BattleTag);
				num += ProtocolParser.SizeOfUInt32(byteCount3) + byteCount3;
			}
			if (this.HasDefaultCurrency)
			{
				num += 2u;
				num += 4u;
			}
			if (this.HasLegalRegion)
			{
				num += 2u;
				num += ProtocolParser.SizeOfUInt32(this.LegalRegion);
			}
			if (this.HasLegalLocale)
			{
				num += 2u;
				num += 4u;
			}
			num += ProtocolParser.SizeOfUInt64(this.CacheExpiration);
			if (this.HasParentalControlInfo)
			{
				num += 2u;
				uint serializedSize4 = this.ParentalControlInfo.GetSerializedSize();
				num += serializedSize4 + ProtocolParser.SizeOfUInt32(serializedSize4);
			}
			if (this.HasCountry)
			{
				num += 2u;
				uint byteCount4 = (uint)Encoding.UTF8.GetByteCount(this.Country);
				num += ProtocolParser.SizeOfUInt32(byteCount4) + byteCount4;
			}
			if (this.HasPreferredRegion)
			{
				num += 2u;
				num += ProtocolParser.SizeOfUInt32(this.PreferredRegion);
			}
			num += 6u;
			return num;
		}

		public uint Id { get; set; }

		public void SetId(uint val)
		{
			this.Id = val;
		}

		public uint Region { get; set; }

		public void SetRegion(uint val)
		{
			this.Region = val;
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

		public ulong Flags { get; set; }

		public void SetFlags(ulong val)
		{
			this.Flags = val;
		}

		public ulong SecureRelease
		{
			get
			{
				return this._SecureRelease;
			}
			set
			{
				this._SecureRelease = value;
				this.HasSecureRelease = true;
			}
		}

		public void SetSecureRelease(ulong val)
		{
			this.SecureRelease = val;
		}

		public ulong WhitelistStart
		{
			get
			{
				return this._WhitelistStart;
			}
			set
			{
				this._WhitelistStart = value;
				this.HasWhitelistStart = true;
			}
		}

		public void SetWhitelistStart(ulong val)
		{
			this.WhitelistStart = val;
		}

		public ulong WhitelistEnd
		{
			get
			{
				return this._WhitelistEnd;
			}
			set
			{
				this._WhitelistEnd = value;
				this.HasWhitelistEnd = true;
			}
		}

		public void SetWhitelistEnd(ulong val)
		{
			this.WhitelistEnd = val;
		}

		public string FullName { get; set; }

		public void SetFullName(string val)
		{
			this.FullName = val;
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

		public List<AccountCredential> Credentials
		{
			get
			{
				return this._Credentials;
			}
			set
			{
				this._Credentials = value;
			}
		}

		public List<AccountCredential> CredentialsList
		{
			get
			{
				return this._Credentials;
			}
		}

		public int CredentialsCount
		{
			get
			{
				return this._Credentials.Count;
			}
		}

		public void AddCredentials(AccountCredential val)
		{
			this._Credentials.Add(val);
		}

		public void ClearCredentials()
		{
			this._Credentials.Clear();
		}

		public void SetCredentials(List<AccountCredential> val)
		{
			this.Credentials = val;
		}

		public List<GameAccountLink> AccountLinks
		{
			get
			{
				return this._AccountLinks;
			}
			set
			{
				this._AccountLinks = value;
			}
		}

		public List<GameAccountLink> AccountLinksList
		{
			get
			{
				return this._AccountLinks;
			}
		}

		public int AccountLinksCount
		{
			get
			{
				return this._AccountLinks.Count;
			}
		}

		public void AddAccountLinks(GameAccountLink val)
		{
			this._AccountLinks.Add(val);
		}

		public void ClearAccountLinks()
		{
			this._AccountLinks.Clear();
		}

		public void SetAccountLinks(List<GameAccountLink> val)
		{
			this.AccountLinks = val;
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

		public uint DefaultCurrency
		{
			get
			{
				return this._DefaultCurrency;
			}
			set
			{
				this._DefaultCurrency = value;
				this.HasDefaultCurrency = true;
			}
		}

		public void SetDefaultCurrency(uint val)
		{
			this.DefaultCurrency = val;
		}

		public uint LegalRegion
		{
			get
			{
				return this._LegalRegion;
			}
			set
			{
				this._LegalRegion = value;
				this.HasLegalRegion = true;
			}
		}

		public void SetLegalRegion(uint val)
		{
			this.LegalRegion = val;
		}

		public uint LegalLocale
		{
			get
			{
				return this._LegalLocale;
			}
			set
			{
				this._LegalLocale = value;
				this.HasLegalLocale = true;
			}
		}

		public void SetLegalLocale(uint val)
		{
			this.LegalLocale = val;
		}

		public ulong CacheExpiration { get; set; }

		public void SetCacheExpiration(ulong val)
		{
			this.CacheExpiration = val;
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

		public string Country
		{
			get
			{
				return this._Country;
			}
			set
			{
				this._Country = value;
				this.HasCountry = (value != null);
			}
		}

		public void SetCountry(string val)
		{
			this.Country = val;
		}

		public uint PreferredRegion
		{
			get
			{
				return this._PreferredRegion;
			}
			set
			{
				this._PreferredRegion = value;
				this.HasPreferredRegion = true;
			}
		}

		public void SetPreferredRegion(uint val)
		{
			this.PreferredRegion = val;
		}

		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.Id.GetHashCode();
			num ^= this.Region.GetHashCode();
			foreach (string text in this.Email)
			{
				num ^= text.GetHashCode();
			}
			num ^= this.Flags.GetHashCode();
			if (this.HasSecureRelease)
			{
				num ^= this.SecureRelease.GetHashCode();
			}
			if (this.HasWhitelistStart)
			{
				num ^= this.WhitelistStart.GetHashCode();
			}
			if (this.HasWhitelistEnd)
			{
				num ^= this.WhitelistEnd.GetHashCode();
			}
			num ^= this.FullName.GetHashCode();
			foreach (AccountLicense accountLicense in this.Licenses)
			{
				num ^= accountLicense.GetHashCode();
			}
			foreach (AccountCredential accountCredential in this.Credentials)
			{
				num ^= accountCredential.GetHashCode();
			}
			foreach (GameAccountLink gameAccountLink in this.AccountLinks)
			{
				num ^= gameAccountLink.GetHashCode();
			}
			if (this.HasBattleTag)
			{
				num ^= this.BattleTag.GetHashCode();
			}
			if (this.HasDefaultCurrency)
			{
				num ^= this.DefaultCurrency.GetHashCode();
			}
			if (this.HasLegalRegion)
			{
				num ^= this.LegalRegion.GetHashCode();
			}
			if (this.HasLegalLocale)
			{
				num ^= this.LegalLocale.GetHashCode();
			}
			num ^= this.CacheExpiration.GetHashCode();
			if (this.HasParentalControlInfo)
			{
				num ^= this.ParentalControlInfo.GetHashCode();
			}
			if (this.HasCountry)
			{
				num ^= this.Country.GetHashCode();
			}
			if (this.HasPreferredRegion)
			{
				num ^= this.PreferredRegion.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			AccountBlob accountBlob = obj as AccountBlob;
			if (accountBlob == null)
			{
				return false;
			}
			if (!this.Id.Equals(accountBlob.Id))
			{
				return false;
			}
			if (!this.Region.Equals(accountBlob.Region))
			{
				return false;
			}
			if (this.Email.Count != accountBlob.Email.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Email.Count; i++)
			{
				if (!this.Email[i].Equals(accountBlob.Email[i]))
				{
					return false;
				}
			}
			if (!this.Flags.Equals(accountBlob.Flags))
			{
				return false;
			}
			if (this.HasSecureRelease != accountBlob.HasSecureRelease || (this.HasSecureRelease && !this.SecureRelease.Equals(accountBlob.SecureRelease)))
			{
				return false;
			}
			if (this.HasWhitelistStart != accountBlob.HasWhitelistStart || (this.HasWhitelistStart && !this.WhitelistStart.Equals(accountBlob.WhitelistStart)))
			{
				return false;
			}
			if (this.HasWhitelistEnd != accountBlob.HasWhitelistEnd || (this.HasWhitelistEnd && !this.WhitelistEnd.Equals(accountBlob.WhitelistEnd)))
			{
				return false;
			}
			if (!this.FullName.Equals(accountBlob.FullName))
			{
				return false;
			}
			if (this.Licenses.Count != accountBlob.Licenses.Count)
			{
				return false;
			}
			for (int j = 0; j < this.Licenses.Count; j++)
			{
				if (!this.Licenses[j].Equals(accountBlob.Licenses[j]))
				{
					return false;
				}
			}
			if (this.Credentials.Count != accountBlob.Credentials.Count)
			{
				return false;
			}
			for (int k = 0; k < this.Credentials.Count; k++)
			{
				if (!this.Credentials[k].Equals(accountBlob.Credentials[k]))
				{
					return false;
				}
			}
			if (this.AccountLinks.Count != accountBlob.AccountLinks.Count)
			{
				return false;
			}
			for (int l = 0; l < this.AccountLinks.Count; l++)
			{
				if (!this.AccountLinks[l].Equals(accountBlob.AccountLinks[l]))
				{
					return false;
				}
			}
			return this.HasBattleTag == accountBlob.HasBattleTag && (!this.HasBattleTag || this.BattleTag.Equals(accountBlob.BattleTag)) && this.HasDefaultCurrency == accountBlob.HasDefaultCurrency && (!this.HasDefaultCurrency || this.DefaultCurrency.Equals(accountBlob.DefaultCurrency)) && this.HasLegalRegion == accountBlob.HasLegalRegion && (!this.HasLegalRegion || this.LegalRegion.Equals(accountBlob.LegalRegion)) && this.HasLegalLocale == accountBlob.HasLegalLocale && (!this.HasLegalLocale || this.LegalLocale.Equals(accountBlob.LegalLocale)) && this.CacheExpiration.Equals(accountBlob.CacheExpiration) && this.HasParentalControlInfo == accountBlob.HasParentalControlInfo && (!this.HasParentalControlInfo || this.ParentalControlInfo.Equals(accountBlob.ParentalControlInfo)) && this.HasCountry == accountBlob.HasCountry && (!this.HasCountry || this.Country.Equals(accountBlob.Country)) && this.HasPreferredRegion == accountBlob.HasPreferredRegion && (!this.HasPreferredRegion || this.PreferredRegion.Equals(accountBlob.PreferredRegion));
		}

		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		public static AccountBlob ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<AccountBlob>(bs, 0, -1);
		}

		private List<string> _Email = new List<string>();

		public bool HasSecureRelease;

		private ulong _SecureRelease;

		public bool HasWhitelistStart;

		private ulong _WhitelistStart;

		public bool HasWhitelistEnd;

		private ulong _WhitelistEnd;

		private List<AccountLicense> _Licenses = new List<AccountLicense>();

		private List<AccountCredential> _Credentials = new List<AccountCredential>();

		private List<GameAccountLink> _AccountLinks = new List<GameAccountLink>();

		public bool HasBattleTag;

		private string _BattleTag;

		public bool HasDefaultCurrency;

		private uint _DefaultCurrency;

		public bool HasLegalRegion;

		private uint _LegalRegion;

		public bool HasLegalLocale;

		private uint _LegalLocale;

		public bool HasParentalControlInfo;

		private ParentalControlInfo _ParentalControlInfo;

		public bool HasCountry;

		private string _Country;

		public bool HasPreferredRegion;

		private uint _PreferredRegion;
	}
}
