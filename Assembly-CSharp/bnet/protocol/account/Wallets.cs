using System;
using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.account
{
	public class Wallets : IProtoBuf
	{
		public void Deserialize(Stream stream)
		{
			Wallets.Deserialize(stream, this);
		}

		public static Wallets Deserialize(Stream stream, Wallets instance)
		{
			return Wallets.Deserialize(stream, instance, -1L);
		}

		public static Wallets DeserializeLengthDelimited(Stream stream)
		{
			Wallets wallets = new Wallets();
			Wallets.DeserializeLengthDelimited(stream, wallets);
			return wallets;
		}

		public static Wallets DeserializeLengthDelimited(Stream stream, Wallets instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return Wallets.Deserialize(stream, instance, num);
		}

		public static Wallets Deserialize(Stream stream, Wallets instance, long limit)
		{
			if (instance.Wallets_ == null)
			{
				instance.Wallets_ = new List<Wallet>();
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
						instance.Wallets_.Add(Wallet.DeserializeLengthDelimited(stream));
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
			Wallets.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, Wallets instance)
		{
			if (instance.Wallets_.Count > 0)
			{
				foreach (Wallet wallet in instance.Wallets_)
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
			if (this.Wallets_.Count > 0)
			{
				foreach (Wallet wallet in this.Wallets_)
				{
					num += 1u;
					uint serializedSize = wallet.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			return num;
		}

		public List<Wallet> Wallets_
		{
			get
			{
				return this._Wallets_;
			}
			set
			{
				this._Wallets_ = value;
			}
		}

		public List<Wallet> Wallets_List
		{
			get
			{
				return this._Wallets_;
			}
		}

		public int Wallets_Count
		{
			get
			{
				return this._Wallets_.Count;
			}
		}

		public void AddWallets_(Wallet val)
		{
			this._Wallets_.Add(val);
		}

		public void ClearWallets_()
		{
			this._Wallets_.Clear();
		}

		public void SetWallets_(List<Wallet> val)
		{
			this.Wallets_ = val;
		}

		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			foreach (Wallet wallet in this.Wallets_)
			{
				num ^= wallet.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			Wallets wallets = obj as Wallets;
			if (wallets == null)
			{
				return false;
			}
			if (this.Wallets_.Count != wallets.Wallets_.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Wallets_.Count; i++)
			{
				if (!this.Wallets_[i].Equals(wallets.Wallets_[i]))
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

		public static Wallets ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<Wallets>(bs, 0, -1);
		}

		private List<Wallet> _Wallets_ = new List<Wallet>();
	}
}
