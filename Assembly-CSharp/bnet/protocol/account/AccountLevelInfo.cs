using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace bnet.protocol.account
{
	public class AccountLevelInfo : IProtoBuf
	{
		public void Deserialize(Stream stream)
		{
			AccountLevelInfo.Deserialize(stream, this);
		}

		public static AccountLevelInfo Deserialize(Stream stream, AccountLevelInfo instance)
		{
			return AccountLevelInfo.Deserialize(stream, instance, -1L);
		}

		public static AccountLevelInfo DeserializeLengthDelimited(Stream stream)
		{
			AccountLevelInfo accountLevelInfo = new AccountLevelInfo();
			AccountLevelInfo.DeserializeLengthDelimited(stream, accountLevelInfo);
			return accountLevelInfo;
		}

		public static AccountLevelInfo DeserializeLengthDelimited(Stream stream, AccountLevelInfo instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return AccountLevelInfo.Deserialize(stream, instance, num);
		}

		public static AccountLevelInfo Deserialize(Stream stream, AccountLevelInfo instance, long limit)
		{
			BinaryReader binaryReader = new BinaryReader(stream);
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
				else if (num != 26)
				{
					if (num != 37)
					{
						if (num != 42)
						{
							if (num != 48)
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
								instance.PreferredRegion = ProtocolParser.ReadUInt32(stream);
							}
						}
						else
						{
							instance.Country = ProtocolParser.ReadString(stream);
						}
					}
					else
					{
						instance.DefaultCurrency = binaryReader.ReadUInt32();
					}
				}
				else
				{
					instance.Licenses.Add(AccountLicense.DeserializeLengthDelimited(stream));
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
			AccountLevelInfo.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, AccountLevelInfo instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.Licenses.Count > 0)
			{
				foreach (AccountLicense accountLicense in instance.Licenses)
				{
					stream.WriteByte(26);
					ProtocolParser.WriteUInt32(stream, accountLicense.GetSerializedSize());
					AccountLicense.Serialize(stream, accountLicense);
				}
			}
			if (instance.HasDefaultCurrency)
			{
				stream.WriteByte(37);
				binaryWriter.Write(instance.DefaultCurrency);
			}
			if (instance.HasCountry)
			{
				stream.WriteByte(42);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Country));
			}
			if (instance.HasPreferredRegion)
			{
				stream.WriteByte(48);
				ProtocolParser.WriteUInt32(stream, instance.PreferredRegion);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (this.Licenses.Count > 0)
			{
				foreach (AccountLicense accountLicense in this.Licenses)
				{
					num += 1u;
					uint serializedSize = accountLicense.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			if (this.HasDefaultCurrency)
			{
				num += 1u;
				num += 4u;
			}
			if (this.HasCountry)
			{
				num += 1u;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.Country);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (this.HasPreferredRegion)
			{
				num += 1u;
				num += ProtocolParser.SizeOfUInt32(this.PreferredRegion);
			}
			return num;
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
			foreach (AccountLicense accountLicense in this.Licenses)
			{
				num ^= accountLicense.GetHashCode();
			}
			if (this.HasDefaultCurrency)
			{
				num ^= this.DefaultCurrency.GetHashCode();
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
			AccountLevelInfo accountLevelInfo = obj as AccountLevelInfo;
			if (accountLevelInfo == null)
			{
				return false;
			}
			if (this.Licenses.Count != accountLevelInfo.Licenses.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Licenses.Count; i++)
			{
				if (!this.Licenses[i].Equals(accountLevelInfo.Licenses[i]))
				{
					return false;
				}
			}
			return this.HasDefaultCurrency == accountLevelInfo.HasDefaultCurrency && (!this.HasDefaultCurrency || this.DefaultCurrency.Equals(accountLevelInfo.DefaultCurrency)) && this.HasCountry == accountLevelInfo.HasCountry && (!this.HasCountry || this.Country.Equals(accountLevelInfo.Country)) && this.HasPreferredRegion == accountLevelInfo.HasPreferredRegion && (!this.HasPreferredRegion || this.PreferredRegion.Equals(accountLevelInfo.PreferredRegion));
		}

		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		public static AccountLevelInfo ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<AccountLevelInfo>(bs, 0, -1);
		}

		private List<AccountLicense> _Licenses = new List<AccountLicense>();

		public bool HasDefaultCurrency;

		private uint _DefaultCurrency;

		public bool HasCountry;

		private string _Country;

		public bool HasPreferredRegion;

		private uint _PreferredRegion;
	}
}
