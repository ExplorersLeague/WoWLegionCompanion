using System;
using System.IO;

namespace bnet.protocol.authentication
{
	public class GameAccountSelectedRequest : IProtoBuf
	{
		public uint Result { get; set; }

		public void SetResult(uint val)
		{
			this.Result = val;
		}

		public EntityId GameAccount
		{
			get
			{
				return this._GameAccount;
			}
			set
			{
				this._GameAccount = value;
				this.HasGameAccount = (value != null);
			}
		}

		public void SetGameAccount(EntityId val)
		{
			this.GameAccount = val;
		}

		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.Result.GetHashCode();
			if (this.HasGameAccount)
			{
				num ^= this.GameAccount.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			GameAccountSelectedRequest gameAccountSelectedRequest = obj as GameAccountSelectedRequest;
			return gameAccountSelectedRequest != null && this.Result.Equals(gameAccountSelectedRequest.Result) && this.HasGameAccount == gameAccountSelectedRequest.HasGameAccount && (!this.HasGameAccount || this.GameAccount.Equals(gameAccountSelectedRequest.GameAccount));
		}

		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		public static GameAccountSelectedRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GameAccountSelectedRequest>(bs, 0, -1);
		}

		public void Deserialize(Stream stream)
		{
			GameAccountSelectedRequest.Deserialize(stream, this);
		}

		public static GameAccountSelectedRequest Deserialize(Stream stream, GameAccountSelectedRequest instance)
		{
			return GameAccountSelectedRequest.Deserialize(stream, instance, -1L);
		}

		public static GameAccountSelectedRequest DeserializeLengthDelimited(Stream stream)
		{
			GameAccountSelectedRequest gameAccountSelectedRequest = new GameAccountSelectedRequest();
			GameAccountSelectedRequest.DeserializeLengthDelimited(stream, gameAccountSelectedRequest);
			return gameAccountSelectedRequest;
		}

		public static GameAccountSelectedRequest DeserializeLengthDelimited(Stream stream, GameAccountSelectedRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GameAccountSelectedRequest.Deserialize(stream, instance, num);
		}

		public static GameAccountSelectedRequest Deserialize(Stream stream, GameAccountSelectedRequest instance, long limit)
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
				else if (num != 8)
				{
					if (num != 18)
					{
						Key key = ProtocolParser.ReadKey((byte)num, stream);
						uint field = key.Field;
						if (field == 0u)
						{
							throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
						}
						ProtocolParser.SkipKey(stream, key);
					}
					else if (instance.GameAccount == null)
					{
						instance.GameAccount = EntityId.DeserializeLengthDelimited(stream);
					}
					else
					{
						EntityId.DeserializeLengthDelimited(stream, instance.GameAccount);
					}
				}
				else
				{
					instance.Result = ProtocolParser.ReadUInt32(stream);
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
			GameAccountSelectedRequest.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, GameAccountSelectedRequest instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt32(stream, instance.Result);
			if (instance.HasGameAccount)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.GameAccount.GetSerializedSize());
				EntityId.Serialize(stream, instance.GameAccount);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			num += ProtocolParser.SizeOfUInt32(this.Result);
			if (this.HasGameAccount)
			{
				num += 1u;
				uint serializedSize = this.GameAccount.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			return num + 1u;
		}

		public bool HasGameAccount;

		private EntityId _GameAccount;
	}
}
