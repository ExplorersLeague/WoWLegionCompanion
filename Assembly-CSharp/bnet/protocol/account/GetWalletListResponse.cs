using System;
using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.account
{
	public class GetWalletListResponse : IProtoBuf
	{
		public void Deserialize(Stream stream)
		{
			GetWalletListResponse.Deserialize(stream, this);
		}

		public static GetWalletListResponse Deserialize(Stream stream, GetWalletListResponse instance)
		{
			return GetWalletListResponse.Deserialize(stream, instance, -1L);
		}

		public static GetWalletListResponse DeserializeLengthDelimited(Stream stream)
		{
			GetWalletListResponse getWalletListResponse = new GetWalletListResponse();
			GetWalletListResponse.DeserializeLengthDelimited(stream, getWalletListResponse);
			return getWalletListResponse;
		}

		public static GetWalletListResponse DeserializeLengthDelimited(Stream stream, GetWalletListResponse instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GetWalletListResponse.Deserialize(stream, instance, num);
		}

		public static GetWalletListResponse Deserialize(Stream stream, GetWalletListResponse instance, long limit)
		{
			if (instance.Wallets == null)
			{
				instance.Wallets = new List<Wallet>();
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
					if (num2 != 10)
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
						instance.Wallets.Add(Wallet.DeserializeLengthDelimited(stream));
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
			GetWalletListResponse.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, GetWalletListResponse instance)
		{
			if (instance.Wallets.Count > 0)
			{
				foreach (Wallet wallet in instance.Wallets)
				{
					stream.WriteByte(10);
					ProtocolParser.WriteUInt32(stream, wallet.GetSerializedSize());
					Wallet.Serialize(stream, wallet);
				}
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (this.Wallets.Count > 0)
			{
				foreach (Wallet wallet in this.Wallets)
				{
					num += 1u;
					uint serializedSize = wallet.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			return num;
		}

		public List<Wallet> Wallets
		{
			get
			{
				return this._Wallets;
			}
			set
			{
				this._Wallets = value;
			}
		}

		public List<Wallet> WalletsList
		{
			get
			{
				return this._Wallets;
			}
		}

		public int WalletsCount
		{
			get
			{
				return this._Wallets.Count;
			}
		}

		public void AddWallets(Wallet val)
		{
			this._Wallets.Add(val);
		}

		public void ClearWallets()
		{
			this._Wallets.Clear();
		}

		public void SetWallets(List<Wallet> val)
		{
			this.Wallets = val;
		}

		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			foreach (Wallet wallet in this.Wallets)
			{
				num ^= wallet.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			GetWalletListResponse getWalletListResponse = obj as GetWalletListResponse;
			if (getWalletListResponse == null)
			{
				return false;
			}
			if (this.Wallets.Count != getWalletListResponse.Wallets.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Wallets.Count; i++)
			{
				if (!this.Wallets[i].Equals(getWalletListResponse.Wallets[i]))
				{
					return false;
				}
			}
			return true;
		}

		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		public static GetWalletListResponse ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GetWalletListResponse>(bs, 0, -1);
		}

		private List<Wallet> _Wallets = new List<Wallet>();
	}
}
