using System;
using System.Collections.Generic;
using System.IO;
using bnet.protocol.account;

namespace bnet.protocol.authentication
{
	public class AccountSettingsNotification : IProtoBuf
	{
		public void Deserialize(Stream stream)
		{
			AccountSettingsNotification.Deserialize(stream, this);
		}

		public static AccountSettingsNotification Deserialize(Stream stream, AccountSettingsNotification instance)
		{
			return AccountSettingsNotification.Deserialize(stream, instance, -1L);
		}

		public static AccountSettingsNotification DeserializeLengthDelimited(Stream stream)
		{
			AccountSettingsNotification accountSettingsNotification = new AccountSettingsNotification();
			AccountSettingsNotification.DeserializeLengthDelimited(stream, accountSettingsNotification);
			return accountSettingsNotification;
		}

		public static AccountSettingsNotification DeserializeLengthDelimited(Stream stream, AccountSettingsNotification instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return AccountSettingsNotification.Deserialize(stream, instance, num);
		}

		public static AccountSettingsNotification Deserialize(Stream stream, AccountSettingsNotification instance, long limit)
		{
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
					if (num != 16)
					{
						if (num != 24)
						{
							if (num != 32)
							{
								if (num != 40)
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
									instance.CanSendVoice = ProtocolParser.ReadBool(stream);
								}
							}
							else
							{
								instance.CanReceiveVoice = ProtocolParser.ReadBool(stream);
							}
						}
						else
						{
							instance.IsPlayingFromIgr = ProtocolParser.ReadBool(stream);
						}
					}
					else
					{
						instance.IsUsingRid = ProtocolParser.ReadBool(stream);
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
			AccountSettingsNotification.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, AccountSettingsNotification instance)
		{
			if (instance.Licenses.Count > 0)
			{
				foreach (AccountLicense accountLicense in instance.Licenses)
				{
					stream.WriteByte(10);
					ProtocolParser.WriteUInt32(stream, accountLicense.GetSerializedSize());
					AccountLicense.Serialize(stream, accountLicense);
				}
			}
			if (instance.HasIsUsingRid)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteBool(stream, instance.IsUsingRid);
			}
			if (instance.HasIsPlayingFromIgr)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteBool(stream, instance.IsPlayingFromIgr);
			}
			if (instance.HasCanReceiveVoice)
			{
				stream.WriteByte(32);
				ProtocolParser.WriteBool(stream, instance.CanReceiveVoice);
			}
			if (instance.HasCanSendVoice)
			{
				stream.WriteByte(40);
				ProtocolParser.WriteBool(stream, instance.CanSendVoice);
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
			if (this.HasIsUsingRid)
			{
				num += 1u;
				num += 1u;
			}
			if (this.HasIsPlayingFromIgr)
			{
				num += 1u;
				num += 1u;
			}
			if (this.HasCanReceiveVoice)
			{
				num += 1u;
				num += 1u;
			}
			if (this.HasCanSendVoice)
			{
				num += 1u;
				num += 1u;
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

		public bool IsUsingRid
		{
			get
			{
				return this._IsUsingRid;
			}
			set
			{
				this._IsUsingRid = value;
				this.HasIsUsingRid = true;
			}
		}

		public void SetIsUsingRid(bool val)
		{
			this.IsUsingRid = val;
		}

		public bool IsPlayingFromIgr
		{
			get
			{
				return this._IsPlayingFromIgr;
			}
			set
			{
				this._IsPlayingFromIgr = value;
				this.HasIsPlayingFromIgr = true;
			}
		}

		public void SetIsPlayingFromIgr(bool val)
		{
			this.IsPlayingFromIgr = val;
		}

		public bool CanReceiveVoice
		{
			get
			{
				return this._CanReceiveVoice;
			}
			set
			{
				this._CanReceiveVoice = value;
				this.HasCanReceiveVoice = true;
			}
		}

		public void SetCanReceiveVoice(bool val)
		{
			this.CanReceiveVoice = val;
		}

		public bool CanSendVoice
		{
			get
			{
				return this._CanSendVoice;
			}
			set
			{
				this._CanSendVoice = value;
				this.HasCanSendVoice = true;
			}
		}

		public void SetCanSendVoice(bool val)
		{
			this.CanSendVoice = val;
		}

		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			foreach (AccountLicense accountLicense in this.Licenses)
			{
				num ^= accountLicense.GetHashCode();
			}
			if (this.HasIsUsingRid)
			{
				num ^= this.IsUsingRid.GetHashCode();
			}
			if (this.HasIsPlayingFromIgr)
			{
				num ^= this.IsPlayingFromIgr.GetHashCode();
			}
			if (this.HasCanReceiveVoice)
			{
				num ^= this.CanReceiveVoice.GetHashCode();
			}
			if (this.HasCanSendVoice)
			{
				num ^= this.CanSendVoice.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			AccountSettingsNotification accountSettingsNotification = obj as AccountSettingsNotification;
			if (accountSettingsNotification == null)
			{
				return false;
			}
			if (this.Licenses.Count != accountSettingsNotification.Licenses.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Licenses.Count; i++)
			{
				if (!this.Licenses[i].Equals(accountSettingsNotification.Licenses[i]))
				{
					return false;
				}
			}
			return this.HasIsUsingRid == accountSettingsNotification.HasIsUsingRid && (!this.HasIsUsingRid || this.IsUsingRid.Equals(accountSettingsNotification.IsUsingRid)) && this.HasIsPlayingFromIgr == accountSettingsNotification.HasIsPlayingFromIgr && (!this.HasIsPlayingFromIgr || this.IsPlayingFromIgr.Equals(accountSettingsNotification.IsPlayingFromIgr)) && this.HasCanReceiveVoice == accountSettingsNotification.HasCanReceiveVoice && (!this.HasCanReceiveVoice || this.CanReceiveVoice.Equals(accountSettingsNotification.CanReceiveVoice)) && this.HasCanSendVoice == accountSettingsNotification.HasCanSendVoice && (!this.HasCanSendVoice || this.CanSendVoice.Equals(accountSettingsNotification.CanSendVoice));
		}

		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		public static AccountSettingsNotification ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<AccountSettingsNotification>(bs, 0, -1);
		}

		private List<AccountLicense> _Licenses = new List<AccountLicense>();

		public bool HasIsUsingRid;

		private bool _IsUsingRid;

		public bool HasIsPlayingFromIgr;

		private bool _IsPlayingFromIgr;

		public bool HasCanReceiveVoice;

		private bool _CanReceiveVoice;

		public bool HasCanSendVoice;

		private bool _CanSendVoice;
	}
}
