using System;
using System.IO;

namespace bnet.protocol.account
{
	public class GetWalletListRequest : IProtoBuf
	{
		public void Deserialize(Stream stream)
		{
			GetWalletListRequest.Deserialize(stream, this);
		}

		public static GetWalletListRequest Deserialize(Stream stream, GetWalletListRequest instance)
		{
			return GetWalletListRequest.Deserialize(stream, instance, -1L);
		}

		public static GetWalletListRequest DeserializeLengthDelimited(Stream stream)
		{
			GetWalletListRequest getWalletListRequest = new GetWalletListRequest();
			GetWalletListRequest.DeserializeLengthDelimited(stream, getWalletListRequest);
			return getWalletListRequest;
		}

		public static GetWalletListRequest DeserializeLengthDelimited(Stream stream, GetWalletListRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GetWalletListRequest.Deserialize(stream, instance, num);
		}

		public static GetWalletListRequest Deserialize(Stream stream, GetWalletListRequest instance, long limit)
		{
			instance.Refresh = false;
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
						if (num2 != 16)
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
							instance.Refresh = ProtocolParser.ReadBool(stream);
						}
					}
					else if (instance.AccountId == null)
					{
						instance.AccountId = AccountId.DeserializeLengthDelimited(stream);
					}
					else
					{
						AccountId.DeserializeLengthDelimited(stream, instance.AccountId);
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
			GetWalletListRequest.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, GetWalletListRequest instance)
		{
			if (instance.AccountId == null)
			{
				throw new ArgumentNullException("AccountId", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteUInt32(stream, instance.AccountId.GetSerializedSize());
			AccountId.Serialize(stream, instance.AccountId);
			if (instance.HasRefresh)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteBool(stream, instance.Refresh);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			uint serializedSize = this.AccountId.GetSerializedSize();
			num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			if (this.HasRefresh)
			{
				num += 1u;
				num += 1u;
			}
			return num + 1u;
		}

		public AccountId AccountId { get; set; }

		public void SetAccountId(AccountId val)
		{
			this.AccountId = val;
		}

		public bool Refresh
		{
			get
			{
				return this._Refresh;
			}
			set
			{
				this._Refresh = value;
				this.HasRefresh = true;
			}
		}

		public void SetRefresh(bool val)
		{
			this.Refresh = val;
		}

		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.AccountId.GetHashCode();
			if (this.HasRefresh)
			{
				num ^= this.Refresh.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			GetWalletListRequest getWalletListRequest = obj as GetWalletListRequest;
			return getWalletListRequest != null && this.AccountId.Equals(getWalletListRequest.AccountId) && this.HasRefresh == getWalletListRequest.HasRefresh && (!this.HasRefresh || this.Refresh.Equals(getWalletListRequest.Refresh));
		}

		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		public static GetWalletListRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GetWalletListRequest>(bs, 0, -1);
		}

		public bool HasRefresh;

		private bool _Refresh;
	}
}
