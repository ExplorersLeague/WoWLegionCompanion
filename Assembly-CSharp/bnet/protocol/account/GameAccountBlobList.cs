using System;
using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.account
{
	public class GameAccountBlobList : IProtoBuf
	{
		public List<GameAccountBlob> Blob
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

		public List<GameAccountBlob> BlobList
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

		public void AddBlob(GameAccountBlob val)
		{
			this._Blob.Add(val);
		}

		public void ClearBlob()
		{
			this._Blob.Clear();
		}

		public void SetBlob(List<GameAccountBlob> val)
		{
			this.Blob = val;
		}

		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			foreach (GameAccountBlob gameAccountBlob in this.Blob)
			{
				num ^= gameAccountBlob.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			GameAccountBlobList gameAccountBlobList = obj as GameAccountBlobList;
			if (gameAccountBlobList == null)
			{
				return false;
			}
			if (this.Blob.Count != gameAccountBlobList.Blob.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Blob.Count; i++)
			{
				if (!this.Blob[i].Equals(gameAccountBlobList.Blob[i]))
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

		public static GameAccountBlobList ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GameAccountBlobList>(bs, 0, -1);
		}

		public void Deserialize(Stream stream)
		{
			GameAccountBlobList.Deserialize(stream, this);
		}

		public static GameAccountBlobList Deserialize(Stream stream, GameAccountBlobList instance)
		{
			return GameAccountBlobList.Deserialize(stream, instance, -1L);
		}

		public static GameAccountBlobList DeserializeLengthDelimited(Stream stream)
		{
			GameAccountBlobList gameAccountBlobList = new GameAccountBlobList();
			GameAccountBlobList.DeserializeLengthDelimited(stream, gameAccountBlobList);
			return gameAccountBlobList;
		}

		public static GameAccountBlobList DeserializeLengthDelimited(Stream stream, GameAccountBlobList instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GameAccountBlobList.Deserialize(stream, instance, num);
		}

		public static GameAccountBlobList Deserialize(Stream stream, GameAccountBlobList instance, long limit)
		{
			if (instance.Blob == null)
			{
				instance.Blob = new List<GameAccountBlob>();
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
					instance.Blob.Add(GameAccountBlob.DeserializeLengthDelimited(stream));
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
			GameAccountBlobList.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, GameAccountBlobList instance)
		{
			if (instance.Blob.Count > 0)
			{
				foreach (GameAccountBlob gameAccountBlob in instance.Blob)
				{
					stream.WriteByte(10);
					ProtocolParser.WriteUInt32(stream, gameAccountBlob.GetSerializedSize());
					GameAccountBlob.Serialize(stream, gameAccountBlob);
				}
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (this.Blob.Count > 0)
			{
				foreach (GameAccountBlob gameAccountBlob in this.Blob)
				{
					num += 1u;
					uint serializedSize = gameAccountBlob.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			return num;
		}

		private List<GameAccountBlob> _Blob = new List<GameAccountBlob>();
	}
}
