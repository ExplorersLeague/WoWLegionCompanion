using System;
using System.IO;

namespace bnet.protocol.account
{
	public class FlagUpdateRequest : IProtoBuf
	{
		public void Deserialize(Stream stream)
		{
			FlagUpdateRequest.Deserialize(stream, this);
		}

		public static FlagUpdateRequest Deserialize(Stream stream, FlagUpdateRequest instance)
		{
			return FlagUpdateRequest.Deserialize(stream, instance, -1L);
		}

		public static FlagUpdateRequest DeserializeLengthDelimited(Stream stream)
		{
			FlagUpdateRequest flagUpdateRequest = new FlagUpdateRequest();
			FlagUpdateRequest.DeserializeLengthDelimited(stream, flagUpdateRequest);
			return flagUpdateRequest;
		}

		public static FlagUpdateRequest DeserializeLengthDelimited(Stream stream, FlagUpdateRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return FlagUpdateRequest.Deserialize(stream, instance, num);
		}

		public static FlagUpdateRequest Deserialize(Stream stream, FlagUpdateRequest instance, long limit)
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
				else if (num != 10)
				{
					if (num != 16)
					{
						if (num != 24)
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
								instance.Active = ProtocolParser.ReadBool(stream);
							}
						}
						else
						{
							instance.Flag = ProtocolParser.ReadUInt64(stream);
						}
					}
					else
					{
						instance.Region = ProtocolParser.ReadUInt32(stream);
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
			FlagUpdateRequest.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, FlagUpdateRequest instance)
		{
			if (instance.Account == null)
			{
				throw new ArgumentNullException("Account", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteUInt32(stream, instance.Account.GetSerializedSize());
			AccountId.Serialize(stream, instance.Account);
			if (instance.HasRegion)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt32(stream, instance.Region);
			}
			stream.WriteByte(24);
			ProtocolParser.WriteUInt64(stream, instance.Flag);
			stream.WriteByte(32);
			ProtocolParser.WriteBool(stream, instance.Active);
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			uint serializedSize = this.Account.GetSerializedSize();
			num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			if (this.HasRegion)
			{
				num += 1u;
				num += ProtocolParser.SizeOfUInt32(this.Region);
			}
			num += ProtocolParser.SizeOfUInt64(this.Flag);
			num += 1u;
			return num + 3u;
		}

		public AccountId Account { get; set; }

		public void SetAccount(AccountId val)
		{
			this.Account = val;
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

		public ulong Flag { get; set; }

		public void SetFlag(ulong val)
		{
			this.Flag = val;
		}

		public bool Active { get; set; }

		public void SetActive(bool val)
		{
			this.Active = val;
		}

		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.Account.GetHashCode();
			if (this.HasRegion)
			{
				num ^= this.Region.GetHashCode();
			}
			num ^= this.Flag.GetHashCode();
			return num ^ this.Active.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			FlagUpdateRequest flagUpdateRequest = obj as FlagUpdateRequest;
			return flagUpdateRequest != null && this.Account.Equals(flagUpdateRequest.Account) && this.HasRegion == flagUpdateRequest.HasRegion && (!this.HasRegion || this.Region.Equals(flagUpdateRequest.Region)) && this.Flag.Equals(flagUpdateRequest.Flag) && this.Active.Equals(flagUpdateRequest.Active);
		}

		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		public static FlagUpdateRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<FlagUpdateRequest>(bs, 0, -1);
		}

		public bool HasRegion;

		private uint _Region;
	}
}
