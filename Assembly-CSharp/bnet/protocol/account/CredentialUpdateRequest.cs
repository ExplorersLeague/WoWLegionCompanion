using System;
using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.account
{
	public class CredentialUpdateRequest : IProtoBuf
	{
		public void Deserialize(Stream stream)
		{
			CredentialUpdateRequest.Deserialize(stream, this);
		}

		public static CredentialUpdateRequest Deserialize(Stream stream, CredentialUpdateRequest instance)
		{
			return CredentialUpdateRequest.Deserialize(stream, instance, -1L);
		}

		public static CredentialUpdateRequest DeserializeLengthDelimited(Stream stream)
		{
			CredentialUpdateRequest credentialUpdateRequest = new CredentialUpdateRequest();
			CredentialUpdateRequest.DeserializeLengthDelimited(stream, credentialUpdateRequest);
			return credentialUpdateRequest;
		}

		public static CredentialUpdateRequest DeserializeLengthDelimited(Stream stream, CredentialUpdateRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return CredentialUpdateRequest.Deserialize(stream, instance, num);
		}

		public static CredentialUpdateRequest Deserialize(Stream stream, CredentialUpdateRequest instance, long limit)
		{
			if (instance.OldCredentials == null)
			{
				instance.OldCredentials = new List<AccountCredential>();
			}
			if (instance.NewCredentials == null)
			{
				instance.NewCredentials = new List<AccountCredential>();
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
							if (num != 32)
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
								instance.Region = ProtocolParser.ReadUInt32(stream);
							}
						}
						else
						{
							instance.NewCredentials.Add(AccountCredential.DeserializeLengthDelimited(stream));
						}
					}
					else
					{
						instance.OldCredentials.Add(AccountCredential.DeserializeLengthDelimited(stream));
					}
				}
				else if (instance.Account == null)
				{
					instance.Account = AccountId.DeserializeLengthDelimited(stream);
				}
				else
				{
					AccountId.DeserializeLengthDelimited(stream, instance.Account);
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
			CredentialUpdateRequest.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, CredentialUpdateRequest instance)
		{
			if (instance.Account == null)
			{
				throw new ArgumentNullException("Account", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteUInt32(stream, instance.Account.GetSerializedSize());
			AccountId.Serialize(stream, instance.Account);
			if (instance.OldCredentials.Count > 0)
			{
				foreach (AccountCredential accountCredential in instance.OldCredentials)
				{
					stream.WriteByte(18);
					ProtocolParser.WriteUInt32(stream, accountCredential.GetSerializedSize());
					AccountCredential.Serialize(stream, accountCredential);
				}
			}
			if (instance.NewCredentials.Count > 0)
			{
				foreach (AccountCredential accountCredential2 in instance.NewCredentials)
				{
					stream.WriteByte(26);
					ProtocolParser.WriteUInt32(stream, accountCredential2.GetSerializedSize());
					AccountCredential.Serialize(stream, accountCredential2);
				}
			}
			if (instance.HasRegion)
			{
				stream.WriteByte(32);
				ProtocolParser.WriteUInt32(stream, instance.Region);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			uint serializedSize = this.Account.GetSerializedSize();
			num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			if (this.OldCredentials.Count > 0)
			{
				foreach (AccountCredential accountCredential in this.OldCredentials)
				{
					num += 1u;
					uint serializedSize2 = accountCredential.GetSerializedSize();
					num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
				}
			}
			if (this.NewCredentials.Count > 0)
			{
				foreach (AccountCredential accountCredential2 in this.NewCredentials)
				{
					num += 1u;
					uint serializedSize3 = accountCredential2.GetSerializedSize();
					num += serializedSize3 + ProtocolParser.SizeOfUInt32(serializedSize3);
				}
			}
			if (this.HasRegion)
			{
				num += 1u;
				num += ProtocolParser.SizeOfUInt32(this.Region);
			}
			num += 1u;
			return num;
		}

		public AccountId Account { get; set; }

		public void SetAccount(AccountId val)
		{
			this.Account = val;
		}

		public List<AccountCredential> OldCredentials
		{
			get
			{
				return this._OldCredentials;
			}
			set
			{
				this._OldCredentials = value;
			}
		}

		public List<AccountCredential> OldCredentialsList
		{
			get
			{
				return this._OldCredentials;
			}
		}

		public int OldCredentialsCount
		{
			get
			{
				return this._OldCredentials.Count;
			}
		}

		public void AddOldCredentials(AccountCredential val)
		{
			this._OldCredentials.Add(val);
		}

		public void ClearOldCredentials()
		{
			this._OldCredentials.Clear();
		}

		public void SetOldCredentials(List<AccountCredential> val)
		{
			this.OldCredentials = val;
		}

		public List<AccountCredential> NewCredentials
		{
			get
			{
				return this._NewCredentials;
			}
			set
			{
				this._NewCredentials = value;
			}
		}

		public List<AccountCredential> NewCredentialsList
		{
			get
			{
				return this._NewCredentials;
			}
		}

		public int NewCredentialsCount
		{
			get
			{
				return this._NewCredentials.Count;
			}
		}

		public void AddNewCredentials(AccountCredential val)
		{
			this._NewCredentials.Add(val);
		}

		public void ClearNewCredentials()
		{
			this._NewCredentials.Clear();
		}

		public void SetNewCredentials(List<AccountCredential> val)
		{
			this.NewCredentials = val;
		}

		public uint Region
		{
			get
			{
				return this._Region;
			}
			set
			{
				this._Region = value;
				this.HasRegion = true;
			}
		}

		public void SetRegion(uint val)
		{
			this.Region = val;
		}

		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.Account.GetHashCode();
			foreach (AccountCredential accountCredential in this.OldCredentials)
			{
				num ^= accountCredential.GetHashCode();
			}
			foreach (AccountCredential accountCredential2 in this.NewCredentials)
			{
				num ^= accountCredential2.GetHashCode();
			}
			if (this.HasRegion)
			{
				num ^= this.Region.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			CredentialUpdateRequest credentialUpdateRequest = obj as CredentialUpdateRequest;
			if (credentialUpdateRequest == null)
			{
				return false;
			}
			if (!this.Account.Equals(credentialUpdateRequest.Account))
			{
				return false;
			}
			if (this.OldCredentials.Count != credentialUpdateRequest.OldCredentials.Count)
			{
				return false;
			}
			for (int i = 0; i < this.OldCredentials.Count; i++)
			{
				if (!this.OldCredentials[i].Equals(credentialUpdateRequest.OldCredentials[i]))
				{
					return false;
				}
			}
			if (this.NewCredentials.Count != credentialUpdateRequest.NewCredentials.Count)
			{
				return false;
			}
			for (int j = 0; j < this.NewCredentials.Count; j++)
			{
				if (!this.NewCredentials[j].Equals(credentialUpdateRequest.NewCredentials[j]))
				{
					return false;
				}
			}
			return this.HasRegion == credentialUpdateRequest.HasRegion && (!this.HasRegion || this.Region.Equals(credentialUpdateRequest.Region));
		}

		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		public static CredentialUpdateRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<CredentialUpdateRequest>(bs, 0, -1);
		}

		private List<AccountCredential> _OldCredentials = new List<AccountCredential>();

		private List<AccountCredential> _NewCredentials = new List<AccountCredential>();

		public bool HasRegion;

		private uint _Region;
	}
}
