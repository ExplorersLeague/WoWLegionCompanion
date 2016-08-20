using System;
using System.IO;
using System.Text;

namespace bnet.protocol
{
	public class AccountInfo : IProtoBuf
	{
		public void Deserialize(Stream stream)
		{
			AccountInfo.Deserialize(stream, this);
		}

		public static AccountInfo Deserialize(Stream stream, AccountInfo instance)
		{
			return AccountInfo.Deserialize(stream, instance, -1L);
		}

		public static AccountInfo DeserializeLengthDelimited(Stream stream)
		{
			AccountInfo accountInfo = new AccountInfo();
			AccountInfo.DeserializeLengthDelimited(stream, accountInfo);
			return accountInfo;
		}

		public static AccountInfo DeserializeLengthDelimited(Stream stream, AccountInfo instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return AccountInfo.Deserialize(stream, instance, num);
		}

		public static AccountInfo Deserialize(Stream stream, AccountInfo instance, long limit)
		{
			BinaryReader binaryReader = new BinaryReader(stream);
			instance.AccountPaid = false;
			instance.CountryId = 0u;
			instance.ManualReview = false;
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
						if (num2 != 21)
						{
							if (num2 != 26)
							{
								if (num2 != 32)
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
									instance.ManualReview = ProtocolParser.ReadBool(stream);
								}
							}
							else
							{
								instance.BattleTag = ProtocolParser.ReadString(stream);
							}
						}
						else
						{
							instance.CountryId = binaryReader.ReadUInt32();
						}
					}
					else
					{
						instance.AccountPaid = ProtocolParser.ReadBool(stream);
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
			AccountInfo.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, AccountInfo instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.HasAccountPaid)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteBool(stream, instance.AccountPaid);
			}
			if (instance.HasCountryId)
			{
				stream.WriteByte(21);
				binaryWriter.Write(instance.CountryId);
			}
			if (instance.HasBattleTag)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.BattleTag));
			}
			if (instance.HasManualReview)
			{
				stream.WriteByte(32);
				ProtocolParser.WriteBool(stream, instance.ManualReview);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (this.HasAccountPaid)
			{
				num += 1u;
				num += 1u;
			}
			if (this.HasCountryId)
			{
				num += 1u;
				num += 4u;
			}
			if (this.HasBattleTag)
			{
				num += 1u;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.BattleTag);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (this.HasManualReview)
			{
				num += 1u;
				num += 1u;
			}
			return num;
		}

		public bool AccountPaid
		{
			get
			{
				return this._AccountPaid;
			}
			set
			{
				this._AccountPaid = value;
				this.HasAccountPaid = true;
			}
		}

		public void SetAccountPaid(bool val)
		{
			this.AccountPaid = val;
		}

		public uint CountryId
		{
			get
			{
				return this._CountryId;
			}
			set
			{
				this._CountryId = value;
				this.HasCountryId = true;
			}
		}

		public void SetCountryId(uint val)
		{
			this.CountryId = val;
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

		public bool ManualReview
		{
			get
			{
				return this._ManualReview;
			}
			set
			{
				this._ManualReview = value;
				this.HasManualReview = true;
			}
		}

		public void SetManualReview(bool val)
		{
			this.ManualReview = val;
		}

		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasAccountPaid)
			{
				num ^= this.AccountPaid.GetHashCode();
			}
			if (this.HasCountryId)
			{
				num ^= this.CountryId.GetHashCode();
			}
			if (this.HasBattleTag)
			{
				num ^= this.BattleTag.GetHashCode();
			}
			if (this.HasManualReview)
			{
				num ^= this.ManualReview.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			AccountInfo accountInfo = obj as AccountInfo;
			return accountInfo != null && this.HasAccountPaid == accountInfo.HasAccountPaid && (!this.HasAccountPaid || this.AccountPaid.Equals(accountInfo.AccountPaid)) && this.HasCountryId == accountInfo.HasCountryId && (!this.HasCountryId || this.CountryId.Equals(accountInfo.CountryId)) && this.HasBattleTag == accountInfo.HasBattleTag && (!this.HasBattleTag || this.BattleTag.Equals(accountInfo.BattleTag)) && this.HasManualReview == accountInfo.HasManualReview && (!this.HasManualReview || this.ManualReview.Equals(accountInfo.ManualReview));
		}

		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		public static AccountInfo ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<AccountInfo>(bs, 0, -1);
		}

		public bool HasAccountPaid;

		private bool _AccountPaid;

		public bool HasCountryId;

		private uint _CountryId;

		public bool HasBattleTag;

		private string _BattleTag;

		public bool HasManualReview;

		private bool _ManualReview;
	}
}
