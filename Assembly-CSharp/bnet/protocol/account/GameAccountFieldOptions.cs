using System;
using System.IO;

namespace bnet.protocol.account
{
	public class GameAccountFieldOptions : IProtoBuf
	{
		public bool AllFields
		{
			get
			{
				return this._AllFields;
			}
			set
			{
				this._AllFields = value;
				this.HasAllFields = true;
			}
		}

		public void SetAllFields(bool val)
		{
			this.AllFields = val;
		}

		public bool FieldGameLevelInfo
		{
			get
			{
				return this._FieldGameLevelInfo;
			}
			set
			{
				this._FieldGameLevelInfo = value;
				this.HasFieldGameLevelInfo = true;
			}
		}

		public void SetFieldGameLevelInfo(bool val)
		{
			this.FieldGameLevelInfo = val;
		}

		public bool FieldGameTimeInfo
		{
			get
			{
				return this._FieldGameTimeInfo;
			}
			set
			{
				this._FieldGameTimeInfo = value;
				this.HasFieldGameTimeInfo = true;
			}
		}

		public void SetFieldGameTimeInfo(bool val)
		{
			this.FieldGameTimeInfo = val;
		}

		public bool FieldGameStatus
		{
			get
			{
				return this._FieldGameStatus;
			}
			set
			{
				this._FieldGameStatus = value;
				this.HasFieldGameStatus = true;
			}
		}

		public void SetFieldGameStatus(bool val)
		{
			this.FieldGameStatus = val;
		}

		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasAllFields)
			{
				num ^= this.AllFields.GetHashCode();
			}
			if (this.HasFieldGameLevelInfo)
			{
				num ^= this.FieldGameLevelInfo.GetHashCode();
			}
			if (this.HasFieldGameTimeInfo)
			{
				num ^= this.FieldGameTimeInfo.GetHashCode();
			}
			if (this.HasFieldGameStatus)
			{
				num ^= this.FieldGameStatus.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			GameAccountFieldOptions gameAccountFieldOptions = obj as GameAccountFieldOptions;
			return gameAccountFieldOptions != null && this.HasAllFields == gameAccountFieldOptions.HasAllFields && (!this.HasAllFields || this.AllFields.Equals(gameAccountFieldOptions.AllFields)) && this.HasFieldGameLevelInfo == gameAccountFieldOptions.HasFieldGameLevelInfo && (!this.HasFieldGameLevelInfo || this.FieldGameLevelInfo.Equals(gameAccountFieldOptions.FieldGameLevelInfo)) && this.HasFieldGameTimeInfo == gameAccountFieldOptions.HasFieldGameTimeInfo && (!this.HasFieldGameTimeInfo || this.FieldGameTimeInfo.Equals(gameAccountFieldOptions.FieldGameTimeInfo)) && this.HasFieldGameStatus == gameAccountFieldOptions.HasFieldGameStatus && (!this.HasFieldGameStatus || this.FieldGameStatus.Equals(gameAccountFieldOptions.FieldGameStatus));
		}

		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		public static GameAccountFieldOptions ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GameAccountFieldOptions>(bs, 0, -1);
		}

		public void Deserialize(Stream stream)
		{
			GameAccountFieldOptions.Deserialize(stream, this);
		}

		public static GameAccountFieldOptions Deserialize(Stream stream, GameAccountFieldOptions instance)
		{
			return GameAccountFieldOptions.Deserialize(stream, instance, -1L);
		}

		public static GameAccountFieldOptions DeserializeLengthDelimited(Stream stream)
		{
			GameAccountFieldOptions gameAccountFieldOptions = new GameAccountFieldOptions();
			GameAccountFieldOptions.DeserializeLengthDelimited(stream, gameAccountFieldOptions);
			return gameAccountFieldOptions;
		}

		public static GameAccountFieldOptions DeserializeLengthDelimited(Stream stream, GameAccountFieldOptions instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GameAccountFieldOptions.Deserialize(stream, instance, num);
		}

		public static GameAccountFieldOptions Deserialize(Stream stream, GameAccountFieldOptions instance, long limit)
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
								instance.FieldGameStatus = ProtocolParser.ReadBool(stream);
							}
						}
						else
						{
							instance.FieldGameTimeInfo = ProtocolParser.ReadBool(stream);
						}
					}
					else
					{
						instance.FieldGameLevelInfo = ProtocolParser.ReadBool(stream);
					}
				}
				else
				{
					instance.AllFields = ProtocolParser.ReadBool(stream);
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
			GameAccountFieldOptions.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, GameAccountFieldOptions instance)
		{
			if (instance.HasAllFields)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteBool(stream, instance.AllFields);
			}
			if (instance.HasFieldGameLevelInfo)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteBool(stream, instance.FieldGameLevelInfo);
			}
			if (instance.HasFieldGameTimeInfo)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteBool(stream, instance.FieldGameTimeInfo);
			}
			if (instance.HasFieldGameStatus)
			{
				stream.WriteByte(32);
				ProtocolParser.WriteBool(stream, instance.FieldGameStatus);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (this.HasAllFields)
			{
				num += 1u;
				num += 1u;
			}
			if (this.HasFieldGameLevelInfo)
			{
				num += 1u;
				num += 1u;
			}
			if (this.HasFieldGameTimeInfo)
			{
				num += 1u;
				num += 1u;
			}
			if (this.HasFieldGameStatus)
			{
				num += 1u;
				num += 1u;
			}
			return num;
		}

		public bool HasAllFields;

		private bool _AllFields;

		public bool HasFieldGameLevelInfo;

		private bool _FieldGameLevelInfo;

		public bool HasFieldGameTimeInfo;

		private bool _FieldGameTimeInfo;

		public bool HasFieldGameStatus;

		private bool _FieldGameStatus;
	}
}
