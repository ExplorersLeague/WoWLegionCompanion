using System;
using System.IO;
using System.Text;

namespace bnet.protocol.account
{
	public class Wallet : IProtoBuf
	{
		public void Deserialize(Stream stream)
		{
			Wallet.Deserialize(stream, this);
		}

		public static Wallet Deserialize(Stream stream, Wallet instance)
		{
			return Wallet.Deserialize(stream, instance, -1L);
		}

		public static Wallet DeserializeLengthDelimited(Stream stream)
		{
			Wallet wallet = new Wallet();
			Wallet.DeserializeLengthDelimited(stream, wallet);
			return wallet;
		}

		public static Wallet DeserializeLengthDelimited(Stream stream, Wallet instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return Wallet.Deserialize(stream, instance, num);
		}

		public static Wallet Deserialize(Stream stream, Wallet instance, long limit)
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
				else if (num != 8)
				{
					if (num != 16)
					{
						if (num != 24)
						{
							if (num != 34)
							{
								if (num != 40)
								{
									if (num != 50)
									{
										if (num != 58)
										{
											if (num != 66)
											{
												if (num != 74)
												{
													if (num != 82)
													{
														if (num != 90)
														{
															if (num != 98)
															{
																if (num != 106)
																{
																	if (num != 114)
																	{
																		if (num != 120)
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
																			instance.BirthDate = ProtocolParser.ReadUInt64(stream);
																		}
																	}
																	else
																	{
																		instance.LastName = ProtocolParser.ReadString(stream);
																	}
																}
																else
																{
																	instance.FirstName = ProtocolParser.ReadString(stream);
																}
															}
															else
															{
																instance.Street = ProtocolParser.ReadString(stream);
															}
														}
														else
														{
															instance.LocaleId = ProtocolParser.ReadString(stream);
														}
													}
													else
													{
														instance.Bin = ProtocolParser.ReadString(stream);
													}
												}
												else
												{
													instance.PaymentInfo = ProtocolParser.ReadBytes(stream);
												}
											}
											else
											{
												instance.PostalCode = ProtocolParser.ReadString(stream);
											}
										}
										else
										{
											instance.City = ProtocolParser.ReadString(stream);
										}
									}
									else
									{
										instance.State = ProtocolParser.ReadString(stream);
									}
								}
								else
								{
									instance.CountryId = ProtocolParser.ReadUInt32(stream);
								}
							}
							else
							{
								instance.Description = ProtocolParser.ReadString(stream);
							}
						}
						else
						{
							instance.WalletType = ProtocolParser.ReadUInt32(stream);
						}
					}
					else
					{
						instance.WalletId = ProtocolParser.ReadUInt64(stream);
					}
				}
				else
				{
					instance.Region = ProtocolParser.ReadUInt32(stream);
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
			Wallet.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, Wallet instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt32(stream, instance.Region);
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, instance.WalletId);
			stream.WriteByte(24);
			ProtocolParser.WriteUInt32(stream, instance.WalletType);
			if (instance.HasDescription)
			{
				stream.WriteByte(34);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Description));
			}
			stream.WriteByte(40);
			ProtocolParser.WriteUInt32(stream, instance.CountryId);
			if (instance.HasState)
			{
				stream.WriteByte(50);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.State));
			}
			if (instance.HasCity)
			{
				stream.WriteByte(58);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.City));
			}
			if (instance.HasPostalCode)
			{
				stream.WriteByte(66);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.PostalCode));
			}
			if (instance.HasPaymentInfo)
			{
				stream.WriteByte(74);
				ProtocolParser.WriteBytes(stream, instance.PaymentInfo);
			}
			if (instance.HasBin)
			{
				stream.WriteByte(82);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Bin));
			}
			if (instance.HasLocaleId)
			{
				stream.WriteByte(90);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.LocaleId));
			}
			if (instance.HasStreet)
			{
				stream.WriteByte(98);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Street));
			}
			if (instance.HasFirstName)
			{
				stream.WriteByte(106);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.FirstName));
			}
			if (instance.HasLastName)
			{
				stream.WriteByte(114);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.LastName));
			}
			if (instance.HasBirthDate)
			{
				stream.WriteByte(120);
				ProtocolParser.WriteUInt64(stream, instance.BirthDate);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			num += ProtocolParser.SizeOfUInt32(this.Region);
			num += ProtocolParser.SizeOfUInt64(this.WalletId);
			num += ProtocolParser.SizeOfUInt32(this.WalletType);
			if (this.HasDescription)
			{
				num += 1u;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.Description);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			num += ProtocolParser.SizeOfUInt32(this.CountryId);
			if (this.HasState)
			{
				num += 1u;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(this.State);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			}
			if (this.HasCity)
			{
				num += 1u;
				uint byteCount3 = (uint)Encoding.UTF8.GetByteCount(this.City);
				num += ProtocolParser.SizeOfUInt32(byteCount3) + byteCount3;
			}
			if (this.HasPostalCode)
			{
				num += 1u;
				uint byteCount4 = (uint)Encoding.UTF8.GetByteCount(this.PostalCode);
				num += ProtocolParser.SizeOfUInt32(byteCount4) + byteCount4;
			}
			if (this.HasPaymentInfo)
			{
				num += 1u;
				num += ProtocolParser.SizeOfUInt32(this.PaymentInfo.Length) + (uint)this.PaymentInfo.Length;
			}
			if (this.HasBin)
			{
				num += 1u;
				uint byteCount5 = (uint)Encoding.UTF8.GetByteCount(this.Bin);
				num += ProtocolParser.SizeOfUInt32(byteCount5) + byteCount5;
			}
			if (this.HasLocaleId)
			{
				num += 1u;
				uint byteCount6 = (uint)Encoding.UTF8.GetByteCount(this.LocaleId);
				num += ProtocolParser.SizeOfUInt32(byteCount6) + byteCount6;
			}
			if (this.HasStreet)
			{
				num += 1u;
				uint byteCount7 = (uint)Encoding.UTF8.GetByteCount(this.Street);
				num += ProtocolParser.SizeOfUInt32(byteCount7) + byteCount7;
			}
			if (this.HasFirstName)
			{
				num += 1u;
				uint byteCount8 = (uint)Encoding.UTF8.GetByteCount(this.FirstName);
				num += ProtocolParser.SizeOfUInt32(byteCount8) + byteCount8;
			}
			if (this.HasLastName)
			{
				num += 1u;
				uint byteCount9 = (uint)Encoding.UTF8.GetByteCount(this.LastName);
				num += ProtocolParser.SizeOfUInt32(byteCount9) + byteCount9;
			}
			if (this.HasBirthDate)
			{
				num += 1u;
				num += ProtocolParser.SizeOfUInt64(this.BirthDate);
			}
			return num + 4u;
		}

		public uint Region { get; set; }

		public void SetRegion(uint val)
		{
			this.Region = val;
		}

		public ulong WalletId { get; set; }

		public void SetWalletId(ulong val)
		{
			this.WalletId = val;
		}

		public uint WalletType { get; set; }

		public void SetWalletType(uint val)
		{
			this.WalletType = val;
		}

		public string Description
		{
			get
			{
				return this._Description;
			}
			set
			{
				this._Description = value;
				this.HasDescription = (value != null);
			}
		}

		public void SetDescription(string val)
		{
			this.Description = val;
		}

		public uint CountryId { get; set; }

		public void SetCountryId(uint val)
		{
			this.CountryId = val;
		}

		public string State
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

		public void SetState(string val)
		{
			this.State = val;
		}

		public string City
		{
			get
			{
				return this._City;
			}
			set
			{
				this._City = value;
				this.HasCity = (value != null);
			}
		}

		public void SetCity(string val)
		{
			this.City = val;
		}

		public string PostalCode
		{
			get
			{
				return this._PostalCode;
			}
			set
			{
				this._PostalCode = value;
				this.HasPostalCode = (value != null);
			}
		}

		public void SetPostalCode(string val)
		{
			this.PostalCode = val;
		}

		public byte[] PaymentInfo
		{
			get
			{
				return this._PaymentInfo;
			}
			set
			{
				this._PaymentInfo = value;
				this.HasPaymentInfo = (value != null);
			}
		}

		public void SetPaymentInfo(byte[] val)
		{
			this.PaymentInfo = val;
		}

		public string Bin
		{
			get
			{
				return this._Bin;
			}
			set
			{
				this._Bin = value;
				this.HasBin = (value != null);
			}
		}

		public void SetBin(string val)
		{
			this.Bin = val;
		}

		public string LocaleId
		{
			get
			{
				return this._LocaleId;
			}
			set
			{
				this._LocaleId = value;
				this.HasLocaleId = (value != null);
			}
		}

		public void SetLocaleId(string val)
		{
			this.LocaleId = val;
		}

		public string Street
		{
			get
			{
				return this._Street;
			}
			set
			{
				this._Street = value;
				this.HasStreet = (value != null);
			}
		}

		public void SetStreet(string val)
		{
			this.Street = val;
		}

		public string FirstName
		{
			get
			{
				return this._FirstName;
			}
			set
			{
				this._FirstName = value;
				this.HasFirstName = (value != null);
			}
		}

		public void SetFirstName(string val)
		{
			this.FirstName = val;
		}

		public string LastName
		{
			get
			{
				return this._LastName;
			}
			set
			{
				this._LastName = value;
				this.HasLastName = (value != null);
			}
		}

		public void SetLastName(string val)
		{
			this.LastName = val;
		}

		public ulong BirthDate
		{
			get
			{
				return this._BirthDate;
			}
			set
			{
				this._BirthDate = value;
				this.HasBirthDate = true;
			}
		}

		public void SetBirthDate(ulong val)
		{
			this.BirthDate = val;
		}

		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.Region.GetHashCode();
			num ^= this.WalletId.GetHashCode();
			num ^= this.WalletType.GetHashCode();
			if (this.HasDescription)
			{
				num ^= this.Description.GetHashCode();
			}
			num ^= this.CountryId.GetHashCode();
			if (this.HasState)
			{
				num ^= this.State.GetHashCode();
			}
			if (this.HasCity)
			{
				num ^= this.City.GetHashCode();
			}
			if (this.HasPostalCode)
			{
				num ^= this.PostalCode.GetHashCode();
			}
			if (this.HasPaymentInfo)
			{
				num ^= this.PaymentInfo.GetHashCode();
			}
			if (this.HasBin)
			{
				num ^= this.Bin.GetHashCode();
			}
			if (this.HasLocaleId)
			{
				num ^= this.LocaleId.GetHashCode();
			}
			if (this.HasStreet)
			{
				num ^= this.Street.GetHashCode();
			}
			if (this.HasFirstName)
			{
				num ^= this.FirstName.GetHashCode();
			}
			if (this.HasLastName)
			{
				num ^= this.LastName.GetHashCode();
			}
			if (this.HasBirthDate)
			{
				num ^= this.BirthDate.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			Wallet wallet = obj as Wallet;
			return wallet != null && this.Region.Equals(wallet.Region) && this.WalletId.Equals(wallet.WalletId) && this.WalletType.Equals(wallet.WalletType) && this.HasDescription == wallet.HasDescription && (!this.HasDescription || this.Description.Equals(wallet.Description)) && this.CountryId.Equals(wallet.CountryId) && this.HasState == wallet.HasState && (!this.HasState || this.State.Equals(wallet.State)) && this.HasCity == wallet.HasCity && (!this.HasCity || this.City.Equals(wallet.City)) && this.HasPostalCode == wallet.HasPostalCode && (!this.HasPostalCode || this.PostalCode.Equals(wallet.PostalCode)) && this.HasPaymentInfo == wallet.HasPaymentInfo && (!this.HasPaymentInfo || this.PaymentInfo.Equals(wallet.PaymentInfo)) && this.HasBin == wallet.HasBin && (!this.HasBin || this.Bin.Equals(wallet.Bin)) && this.HasLocaleId == wallet.HasLocaleId && (!this.HasLocaleId || this.LocaleId.Equals(wallet.LocaleId)) && this.HasStreet == wallet.HasStreet && (!this.HasStreet || this.Street.Equals(wallet.Street)) && this.HasFirstName == wallet.HasFirstName && (!this.HasFirstName || this.FirstName.Equals(wallet.FirstName)) && this.HasLastName == wallet.HasLastName && (!this.HasLastName || this.LastName.Equals(wallet.LastName)) && this.HasBirthDate == wallet.HasBirthDate && (!this.HasBirthDate || this.BirthDate.Equals(wallet.BirthDate));
		}

		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		public static Wallet ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<Wallet>(bs, 0, -1);
		}

		public bool HasDescription;

		private string _Description;

		public bool HasState;

		private string _State;

		public bool HasCity;

		private string _City;

		public bool HasPostalCode;

		private string _PostalCode;

		public bool HasPaymentInfo;

		private byte[] _PaymentInfo;

		public bool HasBin;

		private string _Bin;

		public bool HasLocaleId;

		private string _LocaleId;

		public bool HasStreet;

		private string _Street;

		public bool HasFirstName;

		private string _FirstName;

		public bool HasLastName;

		private string _LastName;

		public bool HasBirthDate;

		private ulong _BirthDate;
	}
}
