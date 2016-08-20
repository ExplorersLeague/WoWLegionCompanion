using System;
using System.IO;

namespace bnet.protocol.account
{
	public class GameAccountFieldTags : IProtoBuf
	{
		public void Deserialize(Stream stream)
		{
			GameAccountFieldTags.Deserialize(stream, this);
		}

		public static GameAccountFieldTags Deserialize(Stream stream, GameAccountFieldTags instance)
		{
			return GameAccountFieldTags.Deserialize(stream, instance, -1L);
		}

		public static GameAccountFieldTags DeserializeLengthDelimited(Stream stream)
		{
			GameAccountFieldTags gameAccountFieldTags = new GameAccountFieldTags();
			GameAccountFieldTags.DeserializeLengthDelimited(stream, gameAccountFieldTags);
			return gameAccountFieldTags;
		}

		public static GameAccountFieldTags DeserializeLengthDelimited(Stream stream, GameAccountFieldTags instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GameAccountFieldTags.Deserialize(stream, instance, num);
		}

		public static GameAccountFieldTags Deserialize(Stream stream, GameAccountFieldTags instance, long limit)
		{
			BinaryReader binaryReader = new BinaryReader(stream);
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
					if (num2 != 21)
					{
						if (num2 != 29)
						{
							if (num2 != 37)
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
								instance.GameStatusTag = binaryReader.ReadUInt32();
							}
						}
						else
						{
							instance.GameTimeInfoTag = binaryReader.ReadUInt32();
						}
					}
					else
					{
						instance.GameLevelInfoTag = binaryReader.ReadUInt32();
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
			GameAccountFieldTags.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, GameAccountFieldTags instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.HasGameLevelInfoTag)
			{
				stream.WriteByte(21);
				binaryWriter.Write(instance.GameLevelInfoTag);
			}
			if (instance.HasGameTimeInfoTag)
			{
				stream.WriteByte(29);
				binaryWriter.Write(instance.GameTimeInfoTag);
			}
			if (instance.HasGameStatusTag)
			{
				stream.WriteByte(37);
				binaryWriter.Write(instance.GameStatusTag);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (this.HasGameLevelInfoTag)
			{
				num += 1u;
				num += 4u;
			}
			if (this.HasGameTimeInfoTag)
			{
				num += 1u;
				num += 4u;
			}
			if (this.HasGameStatusTag)
			{
				num += 1u;
				num += 4u;
			}
			return num;
		}

		public uint GameLevelInfoTag
		{
			get
			{
				return this._GameLevelInfoTag;
			}
			set
			{
				this._GameLevelInfoTag = value;
				this.HasGameLevelInfoTag = true;
			}
		}

		public void SetGameLevelInfoTag(uint val)
		{
			this.GameLevelInfoTag = val;
		}

		public uint GameTimeInfoTag
		{
			get
			{
				return this._GameTimeInfoTag;
			}
			set
			{
				this._GameTimeInfoTag = value;
				this.HasGameTimeInfoTag = true;
			}
		}

		public void SetGameTimeInfoTag(uint val)
		{
			this.GameTimeInfoTag = val;
		}

		public uint GameStatusTag
		{
			get
			{
				return this._GameStatusTag;
			}
			set
			{
				this._GameStatusTag = value;
				this.HasGameStatusTag = true;
			}
		}

		public void SetGameStatusTag(uint val)
		{
			this.GameStatusTag = val;
		}

		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasGameLevelInfoTag)
			{
				num ^= this.GameLevelInfoTag.GetHashCode();
			}
			if (this.HasGameTimeInfoTag)
			{
				num ^= this.GameTimeInfoTag.GetHashCode();
			}
			if (this.HasGameStatusTag)
			{
				num ^= this.GameStatusTag.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			GameAccountFieldTags gameAccountFieldTags = obj as GameAccountFieldTags;
			return gameAccountFieldTags != null && this.HasGameLevelInfoTag == gameAccountFieldTags.HasGameLevelInfoTag && (!this.HasGameLevelInfoTag || this.GameLevelInfoTag.Equals(gameAccountFieldTags.GameLevelInfoTag)) && this.HasGameTimeInfoTag == gameAccountFieldTags.HasGameTimeInfoTag && (!this.HasGameTimeInfoTag || this.GameTimeInfoTag.Equals(gameAccountFieldTags.GameTimeInfoTag)) && this.HasGameStatusTag == gameAccountFieldTags.HasGameStatusTag && (!this.HasGameStatusTag || this.GameStatusTag.Equals(gameAccountFieldTags.GameStatusTag));
		}

		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		public static GameAccountFieldTags ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GameAccountFieldTags>(bs, 0, -1);
		}

		public bool HasGameLevelInfoTag;

		private uint _GameLevelInfoTag;

		public bool HasGameTimeInfoTag;

		private uint _GameTimeInfoTag;

		public bool HasGameStatusTag;

		private uint _GameStatusTag;
	}
}
