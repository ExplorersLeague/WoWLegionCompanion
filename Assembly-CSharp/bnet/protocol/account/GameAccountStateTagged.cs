using System;
using System.IO;

namespace bnet.protocol.account
{
	public class GameAccountStateTagged : IProtoBuf
	{
		public void Deserialize(Stream stream)
		{
			GameAccountStateTagged.Deserialize(stream, this);
		}

		public static GameAccountStateTagged Deserialize(Stream stream, GameAccountStateTagged instance)
		{
			return GameAccountStateTagged.Deserialize(stream, instance, -1L);
		}

		public static GameAccountStateTagged DeserializeLengthDelimited(Stream stream)
		{
			GameAccountStateTagged gameAccountStateTagged = new GameAccountStateTagged();
			GameAccountStateTagged.DeserializeLengthDelimited(stream, gameAccountStateTagged);
			return gameAccountStateTagged;
		}

		public static GameAccountStateTagged DeserializeLengthDelimited(Stream stream, GameAccountStateTagged instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GameAccountStateTagged.Deserialize(stream, instance, num);
		}

		public static GameAccountStateTagged Deserialize(Stream stream, GameAccountStateTagged instance, long limit)
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
					else if (instance.GameAccountTags == null)
					{
						instance.GameAccountTags = GameAccountFieldTags.DeserializeLengthDelimited(stream);
					}
					else
					{
						GameAccountFieldTags.DeserializeLengthDelimited(stream, instance.GameAccountTags);
					}
				}
				else if (instance.GameAccountState == null)
				{
					instance.GameAccountState = GameAccountState.DeserializeLengthDelimited(stream);
				}
				else
				{
					GameAccountState.DeserializeLengthDelimited(stream, instance.GameAccountState);
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
			GameAccountStateTagged.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, GameAccountStateTagged instance)
		{
			if (instance.HasGameAccountState)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.GameAccountState.GetSerializedSize());
				GameAccountState.Serialize(stream, instance.GameAccountState);
			}
			if (instance.HasGameAccountTags)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.GameAccountTags.GetSerializedSize());
				GameAccountFieldTags.Serialize(stream, instance.GameAccountTags);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (this.HasGameAccountState)
			{
				num += 1u;
				uint serializedSize = this.GameAccountState.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasGameAccountTags)
			{
				num += 1u;
				uint serializedSize2 = this.GameAccountTags.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			return num;
		}

		public GameAccountState GameAccountState
		{
			get
			{
				return this._GameAccountState;
			}
			set
			{
				this._GameAccountState = value;
				this.HasGameAccountState = (value != null);
			}
		}

		public void SetGameAccountState(GameAccountState val)
		{
			this.GameAccountState = val;
		}

		public GameAccountFieldTags GameAccountTags
		{
			get
			{
				return this._GameAccountTags;
			}
			set
			{
				this._GameAccountTags = value;
				this.HasGameAccountTags = (value != null);
			}
		}

		public void SetGameAccountTags(GameAccountFieldTags val)
		{
			this.GameAccountTags = val;
		}

		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasGameAccountState)
			{
				num ^= this.GameAccountState.GetHashCode();
			}
			if (this.HasGameAccountTags)
			{
				num ^= this.GameAccountTags.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			GameAccountStateTagged gameAccountStateTagged = obj as GameAccountStateTagged;
			return gameAccountStateTagged != null && this.HasGameAccountState == gameAccountStateTagged.HasGameAccountState && (!this.HasGameAccountState || this.GameAccountState.Equals(gameAccountStateTagged.GameAccountState)) && this.HasGameAccountTags == gameAccountStateTagged.HasGameAccountTags && (!this.HasGameAccountTags || this.GameAccountTags.Equals(gameAccountStateTagged.GameAccountTags));
		}

		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		public static GameAccountStateTagged ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GameAccountStateTagged>(bs, 0, -1);
		}

		public bool HasGameAccountState;

		private GameAccountState _GameAccountState;

		public bool HasGameAccountTags;

		private GameAccountFieldTags _GameAccountTags;
	}
}
