using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace bnet.protocol.account
{
	public class GameLevelInfo : IProtoBuf
	{
		public void Deserialize(Stream stream)
		{
			GameLevelInfo.Deserialize(stream, this);
		}

		public static GameLevelInfo Deserialize(Stream stream, GameLevelInfo instance)
		{
			return GameLevelInfo.Deserialize(stream, instance, -1L);
		}

		public static GameLevelInfo DeserializeLengthDelimited(Stream stream)
		{
			GameLevelInfo gameLevelInfo = new GameLevelInfo();
			GameLevelInfo.DeserializeLengthDelimited(stream, gameLevelInfo);
			return gameLevelInfo;
		}

		public static GameLevelInfo DeserializeLengthDelimited(Stream stream, GameLevelInfo instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GameLevelInfo.Deserialize(stream, instance, num);
		}

		public static GameLevelInfo Deserialize(Stream stream, GameLevelInfo instance, long limit)
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
				else
				{
					int num2 = num;
					if (num2 != 24)
					{
						if (num2 != 32)
						{
							if (num2 != 40)
							{
								if (num2 != 48)
								{
									if (num2 != 56)
									{
										if (num2 != 66)
										{
											if (num2 != 77)
											{
												if (num2 != 82)
												{
													if (num2 != 88)
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
														instance.RealmPermissions = ProtocolParser.ReadUInt32(stream);
													}
												}
												else
												{
													instance.Licenses.Add(AccountLicense.DeserializeLengthDelimited(stream));
												}
											}
											else
											{
												instance.Program = binaryReader.ReadUInt32();
											}
										}
										else
										{
											instance.Name = ProtocolParser.ReadString(stream);
										}
									}
									else
									{
										instance.IsBeta = ProtocolParser.ReadBool(stream);
									}
								}
								else
								{
									instance.IsRestricted = ProtocolParser.ReadBool(stream);
								}
							}
							else
							{
								instance.IsLifetime = ProtocolParser.ReadBool(stream);
							}
						}
						else
						{
							instance.IsTrial = ProtocolParser.ReadBool(stream);
						}
					}
					else
					{
						instance.IsStarterEdition = ProtocolParser.ReadBool(stream);
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
			GameLevelInfo.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, GameLevelInfo instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.HasIsStarterEdition)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteBool(stream, instance.IsStarterEdition);
			}
			if (instance.HasIsTrial)
			{
				stream.WriteByte(32);
				ProtocolParser.WriteBool(stream, instance.IsTrial);
			}
			if (instance.HasIsLifetime)
			{
				stream.WriteByte(40);
				ProtocolParser.WriteBool(stream, instance.IsLifetime);
			}
			if (instance.HasIsRestricted)
			{
				stream.WriteByte(48);
				ProtocolParser.WriteBool(stream, instance.IsRestricted);
			}
			if (instance.HasIsBeta)
			{
				stream.WriteByte(56);
				ProtocolParser.WriteBool(stream, instance.IsBeta);
			}
			if (instance.HasName)
			{
				stream.WriteByte(66);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Name));
			}
			if (instance.HasProgram)
			{
				stream.WriteByte(77);
				binaryWriter.Write(instance.Program);
			}
			if (instance.Licenses.Count > 0)
			{
				foreach (AccountLicense accountLicense in instance.Licenses)
				{
					stream.WriteByte(82);
					ProtocolParser.WriteUInt32(stream, accountLicense.GetSerializedSize());
					AccountLicense.Serialize(stream, accountLicense);
				}
			}
			if (instance.HasRealmPermissions)
			{
				stream.WriteByte(88);
				ProtocolParser.WriteUInt32(stream, instance.RealmPermissions);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (this.HasIsStarterEdition)
			{
				num += 1u;
				num += 1u;
			}
			if (this.HasIsTrial)
			{
				num += 1u;
				num += 1u;
			}
			if (this.HasIsLifetime)
			{
				num += 1u;
				num += 1u;
			}
			if (this.HasIsRestricted)
			{
				num += 1u;
				num += 1u;
			}
			if (this.HasIsBeta)
			{
				num += 1u;
				num += 1u;
			}
			if (this.HasName)
			{
				num += 1u;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.Name);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (this.HasProgram)
			{
				num += 1u;
				num += 4u;
			}
			if (this.Licenses.Count > 0)
			{
				foreach (AccountLicense accountLicense in this.Licenses)
				{
					num += 1u;
					uint serializedSize = accountLicense.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			if (this.HasRealmPermissions)
			{
				num += 1u;
				num += ProtocolParser.SizeOfUInt32(this.RealmPermissions);
			}
			return num;
		}

		public bool IsStarterEdition
		{
			get
			{
				return this._IsStarterEdition;
			}
			set
			{
				this._IsStarterEdition = value;
				this.HasIsStarterEdition = true;
			}
		}

		public void SetIsStarterEdition(bool val)
		{
			this.IsStarterEdition = val;
		}

		public bool IsTrial
		{
			get
			{
				return this._IsTrial;
			}
			set
			{
				this._IsTrial = value;
				this.HasIsTrial = true;
			}
		}

		public void SetIsTrial(bool val)
		{
			this.IsTrial = val;
		}

		public bool IsLifetime
		{
			get
			{
				return this._IsLifetime;
			}
			set
			{
				this._IsLifetime = value;
				this.HasIsLifetime = true;
			}
		}

		public void SetIsLifetime(bool val)
		{
			this.IsLifetime = val;
		}

		public bool IsRestricted
		{
			get
			{
				return this._IsRestricted;
			}
			set
			{
				this._IsRestricted = value;
				this.HasIsRestricted = true;
			}
		}

		public void SetIsRestricted(bool val)
		{
			this.IsRestricted = val;
		}

		public bool IsBeta
		{
			get
			{
				return this._IsBeta;
			}
			set
			{
				this._IsBeta = value;
				this.HasIsBeta = true;
			}
		}

		public void SetIsBeta(bool val)
		{
			this.IsBeta = val;
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

		public uint Program
		{
			get
			{
				return this._Program;
			}
			set
			{
				this._Program = value;
				this.HasProgram = true;
			}
		}

		public void SetProgram(uint val)
		{
			this.Program = val;
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

		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasIsStarterEdition)
			{
				num ^= this.IsStarterEdition.GetHashCode();
			}
			if (this.HasIsTrial)
			{
				num ^= this.IsTrial.GetHashCode();
			}
			if (this.HasIsLifetime)
			{
				num ^= this.IsLifetime.GetHashCode();
			}
			if (this.HasIsRestricted)
			{
				num ^= this.IsRestricted.GetHashCode();
			}
			if (this.HasIsBeta)
			{
				num ^= this.IsBeta.GetHashCode();
			}
			if (this.HasName)
			{
				num ^= this.Name.GetHashCode();
			}
			if (this.HasProgram)
			{
				num ^= this.Program.GetHashCode();
			}
			foreach (AccountLicense accountLicense in this.Licenses)
			{
				num ^= accountLicense.GetHashCode();
			}
			if (this.HasRealmPermissions)
			{
				num ^= this.RealmPermissions.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			GameLevelInfo gameLevelInfo = obj as GameLevelInfo;
			if (gameLevelInfo == null)
			{
				return false;
			}
			if (this.HasIsStarterEdition != gameLevelInfo.HasIsStarterEdition || (this.HasIsStarterEdition && !this.IsStarterEdition.Equals(gameLevelInfo.IsStarterEdition)))
			{
				return false;
			}
			if (this.HasIsTrial != gameLevelInfo.HasIsTrial || (this.HasIsTrial && !this.IsTrial.Equals(gameLevelInfo.IsTrial)))
			{
				return false;
			}
			if (this.HasIsLifetime != gameLevelInfo.HasIsLifetime || (this.HasIsLifetime && !this.IsLifetime.Equals(gameLevelInfo.IsLifetime)))
			{
				return false;
			}
			if (this.HasIsRestricted != gameLevelInfo.HasIsRestricted || (this.HasIsRestricted && !this.IsRestricted.Equals(gameLevelInfo.IsRestricted)))
			{
				return false;
			}
			if (this.HasIsBeta != gameLevelInfo.HasIsBeta || (this.HasIsBeta && !this.IsBeta.Equals(gameLevelInfo.IsBeta)))
			{
				return false;
			}
			if (this.HasName != gameLevelInfo.HasName || (this.HasName && !this.Name.Equals(gameLevelInfo.Name)))
			{
				return false;
			}
			if (this.HasProgram != gameLevelInfo.HasProgram || (this.HasProgram && !this.Program.Equals(gameLevelInfo.Program)))
			{
				return false;
			}
			if (this.Licenses.Count != gameLevelInfo.Licenses.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Licenses.Count; i++)
			{
				if (!this.Licenses[i].Equals(gameLevelInfo.Licenses[i]))
				{
					return false;
				}
			}
			return this.HasRealmPermissions == gameLevelInfo.HasRealmPermissions && (!this.HasRealmPermissions || this.RealmPermissions.Equals(gameLevelInfo.RealmPermissions));
		}

		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		public static GameLevelInfo ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GameLevelInfo>(bs, 0, -1);
		}

		public bool HasIsStarterEdition;

		private bool _IsStarterEdition;

		public bool HasIsTrial;

		private bool _IsTrial;

		public bool HasIsLifetime;

		private bool _IsLifetime;

		public bool HasIsRestricted;

		private bool _IsRestricted;

		public bool HasIsBeta;

		private bool _IsBeta;

		public bool HasName;

		private string _Name;

		public bool HasProgram;

		private uint _Program;

		private List<AccountLicense> _Licenses = new List<AccountLicense>();

		public bool HasRealmPermissions;

		private uint _RealmPermissions;
	}
}
