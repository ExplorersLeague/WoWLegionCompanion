using System;
using System.IO;

namespace bnet.protocol.account
{
	public class GameSessionUpdateInfo : IProtoBuf
	{
		public CAIS Cais
		{
			get
			{
				return this._Cais;
			}
			set
			{
				this._Cais = value;
				this.HasCais = (value != null);
			}
		}

		public void SetCais(CAIS val)
		{
			this.Cais = val;
		}

		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasCais)
			{
				num ^= this.Cais.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			GameSessionUpdateInfo gameSessionUpdateInfo = obj as GameSessionUpdateInfo;
			return gameSessionUpdateInfo != null && this.HasCais == gameSessionUpdateInfo.HasCais && (!this.HasCais || this.Cais.Equals(gameSessionUpdateInfo.Cais));
		}

		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		public static GameSessionUpdateInfo ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GameSessionUpdateInfo>(bs, 0, -1);
		}

		public void Deserialize(Stream stream)
		{
			GameSessionUpdateInfo.Deserialize(stream, this);
		}

		public static GameSessionUpdateInfo Deserialize(Stream stream, GameSessionUpdateInfo instance)
		{
			return GameSessionUpdateInfo.Deserialize(stream, instance, -1L);
		}

		public static GameSessionUpdateInfo DeserializeLengthDelimited(Stream stream)
		{
			GameSessionUpdateInfo gameSessionUpdateInfo = new GameSessionUpdateInfo();
			GameSessionUpdateInfo.DeserializeLengthDelimited(stream, gameSessionUpdateInfo);
			return gameSessionUpdateInfo;
		}

		public static GameSessionUpdateInfo DeserializeLengthDelimited(Stream stream, GameSessionUpdateInfo instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GameSessionUpdateInfo.Deserialize(stream, instance, num);
		}

		public static GameSessionUpdateInfo Deserialize(Stream stream, GameSessionUpdateInfo instance, long limit)
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
				else if (num != 66)
				{
					Key key = ProtocolParser.ReadKey((byte)num, stream);
					uint field = key.Field;
					if (field == 0u)
					{
						throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
					}
					ProtocolParser.SkipKey(stream, key);
				}
				else if (instance.Cais == null)
				{
					instance.Cais = CAIS.DeserializeLengthDelimited(stream);
				}
				else
				{
					CAIS.DeserializeLengthDelimited(stream, instance.Cais);
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
			GameSessionUpdateInfo.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, GameSessionUpdateInfo instance)
		{
			if (instance.HasCais)
			{
				stream.WriteByte(66);
				ProtocolParser.WriteUInt32(stream, instance.Cais.GetSerializedSize());
				CAIS.Serialize(stream, instance.Cais);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (this.HasCais)
			{
				num += 1u;
				uint serializedSize = this.Cais.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			return num;
		}

		public bool HasCais;

		private CAIS _Cais;
	}
}
