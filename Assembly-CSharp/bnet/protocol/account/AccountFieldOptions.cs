using System;
using System.IO;

namespace bnet.protocol.account
{
	public class AccountFieldOptions : IProtoBuf
	{
		public void Deserialize(Stream stream)
		{
			AccountFieldOptions.Deserialize(stream, this);
		}

		public static AccountFieldOptions Deserialize(Stream stream, AccountFieldOptions instance)
		{
			return AccountFieldOptions.Deserialize(stream, instance, -1L);
		}

		public static AccountFieldOptions DeserializeLengthDelimited(Stream stream)
		{
			AccountFieldOptions accountFieldOptions = new AccountFieldOptions();
			AccountFieldOptions.DeserializeLengthDelimited(stream, accountFieldOptions);
			return accountFieldOptions;
		}

		public static AccountFieldOptions DeserializeLengthDelimited(Stream stream, AccountFieldOptions instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return AccountFieldOptions.Deserialize(stream, instance, num);
		}

		public static AccountFieldOptions Deserialize(Stream stream, AccountFieldOptions instance, long limit)
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
					if (num2 != 8)
					{
						if (num2 != 16)
						{
							if (num2 != 24)
							{
								if (num2 != 32)
								{
									if (num2 != 48)
									{
										if (num2 != 56)
										{
											if (num2 != 64)
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
												instance.FieldGameAccounts = ProtocolParser.ReadBool(stream);
											}
										}
										else
										{
											instance.FieldGameStatus = ProtocolParser.ReadBool(stream);
										}
									}
									else
									{
										instance.FieldGameLevelInfo = ProtocolParser.ReadBool(stream);
									}
								}
								else
								{
									instance.FieldParentalControlInfo = ProtocolParser.ReadBool(stream);
								}
							}
							else
							{
								instance.FieldPrivacyInfo = ProtocolParser.ReadBool(stream);
							}
						}
						else
						{
							instance.FieldAccountLevelInfo = ProtocolParser.ReadBool(stream);
						}
					}
					else
					{
						instance.AllFields = ProtocolParser.ReadBool(stream);
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
			AccountFieldOptions.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, AccountFieldOptions instance)
		{
			if (instance.HasAllFields)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteBool(stream, instance.AllFields);
			}
			if (instance.HasFieldAccountLevelInfo)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteBool(stream, instance.FieldAccountLevelInfo);
			}
			if (instance.HasFieldPrivacyInfo)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteBool(stream, instance.FieldPrivacyInfo);
			}
			if (instance.HasFieldParentalControlInfo)
			{
				stream.WriteByte(32);
				ProtocolParser.WriteBool(stream, instance.FieldParentalControlInfo);
			}
			if (instance.HasFieldGameLevelInfo)
			{
				stream.WriteByte(48);
				ProtocolParser.WriteBool(stream, instance.FieldGameLevelInfo);
			}
			if (instance.HasFieldGameStatus)
			{
				stream.WriteByte(56);
				ProtocolParser.WriteBool(stream, instance.FieldGameStatus);
			}
			if (instance.HasFieldGameAccounts)
			{
				stream.WriteByte(64);
				ProtocolParser.WriteBool(stream, instance.FieldGameAccounts);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (this.HasAllFields)
			{
				num += 1u;
				num += 1u;
			}
			if (this.HasFieldAccountLevelInfo)
			{
				num += 1u;
				num += 1u;
			}
			if (this.HasFieldPrivacyInfo)
			{
				num += 1u;
				num += 1u;
			}
			if (this.HasFieldParentalControlInfo)
			{
				num += 1u;
				num += 1u;
			}
			if (this.HasFieldGameLevelInfo)
			{
				num += 1u;
				num += 1u;
			}
			if (this.HasFieldGameStatus)
			{
				num += 1u;
				num += 1u;
			}
			if (this.HasFieldGameAccounts)
			{
				num += 1u;
				num += 1u;
			}
			return num;
		}

		public bool AllFields
		{
			get
			{
				return this._AllFields;
			}
			set
			{
				this._AllFields = value;
				this.HasAllFields = true;
			}
		}

		public void SetAllFields(bool val)
		{
			this.AllFields = val;
		}

		public bool FieldAccountLevelInfo
		{
			get
			{
				return this._FieldAccountLevelInfo;
			}
			set
			{
				this._FieldAccountLevelInfo = value;
				this.HasFieldAccountLevelInfo = true;
			}
		}

		public void SetFieldAccountLevelInfo(bool val)
		{
			this.FieldAccountLevelInfo = val;
		}

		public bool FieldPrivacyInfo
		{
			get
			{
				return this._FieldPrivacyInfo;
			}
			set
			{
				this._FieldPrivacyInfo = value;
				this.HasFieldPrivacyInfo = true;
			}
		}

		public void SetFieldPrivacyInfo(bool val)
		{
			this.FieldPrivacyInfo = val;
		}

		public bool FieldParentalControlInfo
		{
			get
			{
				return this._FieldParentalControlInfo;
			}
			set
			{
				this._FieldParentalControlInfo = value;
				this.HasFieldParentalControlInfo = true;
			}
		}

		public void SetFieldParentalControlInfo(bool val)
		{
			this.FieldParentalControlInfo = val;
		}

		public bool FieldGameLevelInfo
		{
			get
			{
				return this._FieldGameLevelInfo;
			}
			set
			{
				this._FieldGameLevelInfo = value;
				this.HasFieldGameLevelInfo = true;
			}
		}

		public void SetFieldGameLevelInfo(bool val)
		{
			this.FieldGameLevelInfo = val;
		}

		public bool FieldGameStatus
		{
			get
			{
				return this._FieldGameStatus;
			}
			set
			{
				this._FieldGameStatus = value;
				this.HasFieldGameStatus = true;
			}
		}

		public void SetFieldGameStatus(bool val)
		{
			this.FieldGameStatus = val;
		}

		public bool FieldGameAccounts
		{
			get
			{
				return this._FieldGameAccounts;
			}
			set
			{
				this._FieldGameAccounts = value;
				this.HasFieldGameAccounts = true;
			}
		}

		public void SetFieldGameAccounts(bool val)
		{
			this.FieldGameAccounts = val;
		}

		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasAllFields)
			{
				num ^= this.AllFields.GetHashCode();
			}
			if (this.HasFieldAccountLevelInfo)
			{
				num ^= this.FieldAccountLevelInfo.GetHashCode();
			}
			if (this.HasFieldPrivacyInfo)
			{
				num ^= this.FieldPrivacyInfo.GetHashCode();
			}
			if (this.HasFieldParentalControlInfo)
			{
				num ^= this.FieldParentalControlInfo.GetHashCode();
			}
			if (this.HasFieldGameLevelInfo)
			{
				num ^= this.FieldGameLevelInfo.GetHashCode();
			}
			if (this.HasFieldGameStatus)
			{
				num ^= this.FieldGameStatus.GetHashCode();
			}
			if (this.HasFieldGameAccounts)
			{
				num ^= this.FieldGameAccounts.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			AccountFieldOptions accountFieldOptions = obj as AccountFieldOptions;
			return accountFieldOptions != null && this.HasAllFields == accountFieldOptions.HasAllFields && (!this.HasAllFields || this.AllFields.Equals(accountFieldOptions.AllFields)) && this.HasFieldAccountLevelInfo == accountFieldOptions.HasFieldAccountLevelInfo && (!this.HasFieldAccountLevelInfo || this.FieldAccountLevelInfo.Equals(accountFieldOptions.FieldAccountLevelInfo)) && this.HasFieldPrivacyInfo == accountFieldOptions.HasFieldPrivacyInfo && (!this.HasFieldPrivacyInfo || this.FieldPrivacyInfo.Equals(accountFieldOptions.FieldPrivacyInfo)) && this.HasFieldParentalControlInfo == accountFieldOptions.HasFieldParentalControlInfo && (!this.HasFieldParentalControlInfo || this.FieldParentalControlInfo.Equals(accountFieldOptions.FieldParentalControlInfo)) && this.HasFieldGameLevelInfo == accountFieldOptions.HasFieldGameLevelInfo && (!this.HasFieldGameLevelInfo || this.FieldGameLevelInfo.Equals(accountFieldOptions.FieldGameLevelInfo)) && this.HasFieldGameStatus == accountFieldOptions.HasFieldGameStatus && (!this.HasFieldGameStatus || this.FieldGameStatus.Equals(accountFieldOptions.FieldGameStatus)) && this.HasFieldGameAccounts == accountFieldOptions.HasFieldGameAccounts && (!this.HasFieldGameAccounts || this.FieldGameAccounts.Equals(accountFieldOptions.FieldGameAccounts));
		}

		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		public static AccountFieldOptions ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<AccountFieldOptions>(bs, 0, -1);
		}

		public bool HasAllFields;

		private bool _AllFields;

		public bool HasFieldAccountLevelInfo;

		private bool _FieldAccountLevelInfo;

		public bool HasFieldPrivacyInfo;

		private bool _FieldPrivacyInfo;

		public bool HasFieldParentalControlInfo;

		private bool _FieldParentalControlInfo;

		public bool HasFieldGameLevelInfo;

		private bool _FieldGameLevelInfo;

		public bool HasFieldGameStatus;

		private bool _FieldGameStatus;

		public bool HasFieldGameAccounts;

		private bool _FieldGameAccounts;
	}
}
