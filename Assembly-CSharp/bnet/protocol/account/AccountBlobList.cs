using System;
using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.account
{
	public class AccountBlobList : IProtoBuf
	{
		public void Deserialize(Stream stream)
		{
			AccountBlobList.Deserialize(stream, this);
		}

		public static AccountBlobList Deserialize(Stream stream, AccountBlobList instance)
		{
			return AccountBlobList.Deserialize(stream, instance, -1L);
		}

		public static AccountBlobList DeserializeLengthDelimited(Stream stream)
		{
			AccountBlobList accountBlobList = new AccountBlobList();
			AccountBlobList.DeserializeLengthDelimited(stream, accountBlobList);
			return accountBlobList;
		}

		public static AccountBlobList DeserializeLengthDelimited(Stream stream, AccountBlobList instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return AccountBlobList.Deserialize(stream, instance, num);
		}

		public static AccountBlobList Deserialize(Stream stream, AccountBlobList instance, long limit)
		{
			if (instance.Blob == null)
			{
				instance.Blob = new List<AccountBlob>();
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
					instance.Blob.Add(AccountBlob.DeserializeLengthDelimited(stream));
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
			AccountBlobList.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, AccountBlobList instance)
		{
			if (instance.Blob.Count > 0)
			{
				foreach (AccountBlob accountBlob in instance.Blob)
				{
					stream.WriteByte(10);
					ProtocolParser.WriteUInt32(stream, accountBlob.GetSerializedSize());
					AccountBlob.Serialize(stream, accountBlob);
				}
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (this.Blob.Count > 0)
			{
				foreach (AccountBlob accountBlob in this.Blob)
				{
					num += 1u;
					uint serializedSize = accountBlob.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			return num;
		}

		public List<AccountBlob> Blob
		{
			get
			{
				return this._Blob;
			}
			set
			{
				this._Blob = value;
			}
		}

		public List<AccountBlob> BlobList
		{
			get
			{
				return this._Blob;
			}
		}

		public int BlobCount
		{
			get
			{
				return this._Blob.Count;
			}
		}

		public void AddBlob(AccountBlob val)
		{
			this._Blob.Add(val);
		}

		public void ClearBlob()
		{
			this._Blob.Clear();
		}

		public void SetBlob(List<AccountBlob> val)
		{
			this.Blob = val;
		}

		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			foreach (AccountBlob accountBlob in this.Blob)
			{
				num ^= accountBlob.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			AccountBlobList accountBlobList = obj as AccountBlobList;
			if (accountBlobList == null)
			{
				return false;
			}
			if (this.Blob.Count != accountBlobList.Blob.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Blob.Count; i++)
			{
				if (!this.Blob[i].Equals(accountBlobList.Blob[i]))
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

		public static AccountBlobList ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<AccountBlobList>(bs, 0, -1);
		}

		private List<AccountBlob> _Blob = new List<AccountBlob>();
	}
}
