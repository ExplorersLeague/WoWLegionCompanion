using System;
using System.IO;

namespace bnet.protocol.account
{
	public class GetAccountRequest : IProtoBuf
	{
		public void Deserialize(Stream stream)
		{
			GetAccountRequest.Deserialize(stream, this);
		}

		public static GetAccountRequest Deserialize(Stream stream, GetAccountRequest instance)
		{
			return GetAccountRequest.Deserialize(stream, instance, -1L);
		}

		public static GetAccountRequest DeserializeLengthDelimited(Stream stream)
		{
			GetAccountRequest getAccountRequest = new GetAccountRequest();
			GetAccountRequest.DeserializeLengthDelimited(stream, getAccountRequest);
			return getAccountRequest;
		}

		public static GetAccountRequest DeserializeLengthDelimited(Stream stream, GetAccountRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GetAccountRequest.Deserialize(stream, instance, num);
		}

		public static GetAccountRequest Deserialize(Stream stream, GetAccountRequest instance, long limit)
		{
			instance.FetchAll = false;
			instance.FetchBlob = false;
			instance.FetchId = false;
			instance.FetchEmail = false;
			instance.FetchBattleTag = false;
			instance.FetchFullName = false;
			instance.FetchLinks = false;
			instance.FetchParentalControls = false;
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
					if (num2 != 10)
					{
						if (num2 != 80)
						{
							if (num2 != 88)
							{
								if (num2 != 96)
								{
									if (num2 != 104)
									{
										if (num2 != 112)
										{
											if (num2 != 120)
											{
												Key key = ProtocolParser.ReadKey((byte)num, stream);
												uint field = key.Field;
												if (field != 16u)
												{
													if (field != 17u)
													{
														if (field == 0u)
														{
															throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
														}
														ProtocolParser.SkipKey(stream, key);
													}
													else if (key.WireType == Wire.Varint)
													{
														instance.FetchParentalControls = ProtocolParser.ReadBool(stream);
													}
												}
												else if (key.WireType == Wire.Varint)
												{
													instance.FetchLinks = ProtocolParser.ReadBool(stream);
												}
											}
											else
											{
												instance.FetchFullName = ProtocolParser.ReadBool(stream);
											}
										}
										else
										{
											instance.FetchBattleTag = ProtocolParser.ReadBool(stream);
										}
									}
									else
									{
										instance.FetchEmail = ProtocolParser.ReadBool(stream);
									}
								}
								else
								{
									instance.FetchId = ProtocolParser.ReadBool(stream);
								}
							}
							else
							{
								instance.FetchBlob = ProtocolParser.ReadBool(stream);
							}
						}
						else
						{
							instance.FetchAll = ProtocolParser.ReadBool(stream);
						}
					}
					else if (instance.Ref == null)
					{
						instance.Ref = AccountReference.DeserializeLengthDelimited(stream);
					}
					else
					{
						AccountReference.DeserializeLengthDelimited(stream, instance.Ref);
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
			GetAccountRequest.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, GetAccountRequest instance)
		{
			if (instance.HasRef)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.Ref.GetSerializedSize());
				AccountReference.Serialize(stream, instance.Ref);
			}
			if (instance.HasFetchAll)
			{
				stream.WriteByte(80);
				ProtocolParser.WriteBool(stream, instance.FetchAll);
			}
			if (instance.HasFetchBlob)
			{
				stream.WriteByte(88);
				ProtocolParser.WriteBool(stream, instance.FetchBlob);
			}
			if (instance.HasFetchId)
			{
				stream.WriteByte(96);
				ProtocolParser.WriteBool(stream, instance.FetchId);
			}
			if (instance.HasFetchEmail)
			{
				stream.WriteByte(104);
				ProtocolParser.WriteBool(stream, instance.FetchEmail);
			}
			if (instance.HasFetchBattleTag)
			{
				stream.WriteByte(112);
				ProtocolParser.WriteBool(stream, instance.FetchBattleTag);
			}
			if (instance.HasFetchFullName)
			{
				stream.WriteByte(120);
				ProtocolParser.WriteBool(stream, instance.FetchFullName);
			}
			if (instance.HasFetchLinks)
			{
				stream.WriteByte(128);
				stream.WriteByte(1);
				ProtocolParser.WriteBool(stream, instance.FetchLinks);
			}
			if (instance.HasFetchParentalControls)
			{
				stream.WriteByte(136);
				stream.WriteByte(1);
				ProtocolParser.WriteBool(stream, instance.FetchParentalControls);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (this.HasRef)
			{
				num += 1u;
				uint serializedSize = this.Ref.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasFetchAll)
			{
				num += 1u;
				num += 1u;
			}
			if (this.HasFetchBlob)
			{
				num += 1u;
				num += 1u;
			}
			if (this.HasFetchId)
			{
				num += 1u;
				num += 1u;
			}
			if (this.HasFetchEmail)
			{
				num += 1u;
				num += 1u;
			}
			if (this.HasFetchBattleTag)
			{
				num += 1u;
				num += 1u;
			}
			if (this.HasFetchFullName)
			{
				num += 1u;
				num += 1u;
			}
			if (this.HasFetchLinks)
			{
				num += 2u;
				num += 1u;
			}
			if (this.HasFetchParentalControls)
			{
				num += 2u;
				num += 1u;
			}
			return num;
		}

		public AccountReference Ref
		{
			get
			{
				return this._Ref;
			}
			set
			{
				this._Ref = value;
				this.HasRef = (value != null);
			}
		}

		public void SetRef(AccountReference val)
		{
			this.Ref = val;
		}

		public bool FetchAll
		{
			get
			{
				return this._FetchAll;
			}
			set
			{
				this._FetchAll = value;
				this.HasFetchAll = true;
			}
		}

		public void SetFetchAll(bool val)
		{
			this.FetchAll = val;
		}

		public bool FetchBlob
		{
			get
			{
				return this._FetchBlob;
			}
			set
			{
				this._FetchBlob = value;
				this.HasFetchBlob = true;
			}
		}

		public void SetFetchBlob(bool val)
		{
			this.FetchBlob = val;
		}

		public bool FetchId
		{
			get
			{
				return this._FetchId;
			}
			set
			{
				this._FetchId = value;
				this.HasFetchId = true;
			}
		}

		public void SetFetchId(bool val)
		{
			this.FetchId = val;
		}

		public bool FetchEmail
		{
			get
			{
				return this._FetchEmail;
			}
			set
			{
				this._FetchEmail = value;
				this.HasFetchEmail = true;
			}
		}

		public void SetFetchEmail(bool val)
		{
			this.FetchEmail = val;
		}

		public bool FetchBattleTag
		{
			get
			{
				return this._FetchBattleTag;
			}
			set
			{
				this._FetchBattleTag = value;
				this.HasFetchBattleTag = true;
			}
		}

		public void SetFetchBattleTag(bool val)
		{
			this.FetchBattleTag = val;
		}

		public bool FetchFullName
		{
			get
			{
				return this._FetchFullName;
			}
			set
			{
				this._FetchFullName = value;
				this.HasFetchFullName = true;
			}
		}

		public void SetFetchFullName(bool val)
		{
			this.FetchFullName = val;
		}

		public bool FetchLinks
		{
			get
			{
				return this._FetchLinks;
			}
			set
			{
				this._FetchLinks = value;
				this.HasFetchLinks = true;
			}
		}

		public void SetFetchLinks(bool val)
		{
			this.FetchLinks = val;
		}

		public bool FetchParentalControls
		{
			get
			{
				return this._FetchParentalControls;
			}
			set
			{
				this._FetchParentalControls = value;
				this.HasFetchParentalControls = true;
			}
		}

		public void SetFetchParentalControls(bool val)
		{
			this.FetchParentalControls = val;
		}

		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasRef)
			{
				num ^= this.Ref.GetHashCode();
			}
			if (this.HasFetchAll)
			{
				num ^= this.FetchAll.GetHashCode();
			}
			if (this.HasFetchBlob)
			{
				num ^= this.FetchBlob.GetHashCode();
			}
			if (this.HasFetchId)
			{
				num ^= this.FetchId.GetHashCode();
			}
			if (this.HasFetchEmail)
			{
				num ^= this.FetchEmail.GetHashCode();
			}
			if (this.HasFetchBattleTag)
			{
				num ^= this.FetchBattleTag.GetHashCode();
			}
			if (this.HasFetchFullName)
			{
				num ^= this.FetchFullName.GetHashCode();
			}
			if (this.HasFetchLinks)
			{
				num ^= this.FetchLinks.GetHashCode();
			}
			if (this.HasFetchParentalControls)
			{
				num ^= this.FetchParentalControls.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			GetAccountRequest getAccountRequest = obj as GetAccountRequest;
			return getAccountRequest != null && this.HasRef == getAccountRequest.HasRef && (!this.HasRef || this.Ref.Equals(getAccountRequest.Ref)) && this.HasFetchAll == getAccountRequest.HasFetchAll && (!this.HasFetchAll || this.FetchAll.Equals(getAccountRequest.FetchAll)) && this.HasFetchBlob == getAccountRequest.HasFetchBlob && (!this.HasFetchBlob || this.FetchBlob.Equals(getAccountRequest.FetchBlob)) && this.HasFetchId == getAccountRequest.HasFetchId && (!this.HasFetchId || this.FetchId.Equals(getAccountRequest.FetchId)) && this.HasFetchEmail == getAccountRequest.HasFetchEmail && (!this.HasFetchEmail || this.FetchEmail.Equals(getAccountRequest.FetchEmail)) && this.HasFetchBattleTag == getAccountRequest.HasFetchBattleTag && (!this.HasFetchBattleTag || this.FetchBattleTag.Equals(getAccountRequest.FetchBattleTag)) && this.HasFetchFullName == getAccountRequest.HasFetchFullName && (!this.HasFetchFullName || this.FetchFullName.Equals(getAccountRequest.FetchFullName)) && this.HasFetchLinks == getAccountRequest.HasFetchLinks && (!this.HasFetchLinks || this.FetchLinks.Equals(getAccountRequest.FetchLinks)) && this.HasFetchParentalControls == getAccountRequest.HasFetchParentalControls && (!this.HasFetchParentalControls || this.FetchParentalControls.Equals(getAccountRequest.FetchParentalControls));
		}

		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		public static GetAccountRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GetAccountRequest>(bs, 0, -1);
		}

		public bool HasRef;

		private AccountReference _Ref;

		public bool HasFetchAll;

		private bool _FetchAll;

		public bool HasFetchBlob;

		private bool _FetchBlob;

		public bool HasFetchId;

		private bool _FetchId;

		public bool HasFetchEmail;

		private bool _FetchEmail;

		public bool HasFetchBattleTag;

		private bool _FetchBattleTag;

		public bool HasFetchFullName;

		private bool _FetchFullName;

		public bool HasFetchLinks;

		private bool _FetchLinks;

		public bool HasFetchParentalControls;

		private bool _FetchParentalControls;
	}
}
