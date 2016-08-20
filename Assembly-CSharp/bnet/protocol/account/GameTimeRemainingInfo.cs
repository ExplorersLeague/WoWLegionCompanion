using System;
using System.IO;

namespace bnet.protocol.account
{
	public class GameTimeRemainingInfo : IProtoBuf
	{
		public void Deserialize(Stream stream)
		{
			GameTimeRemainingInfo.Deserialize(stream, this);
		}

		public static GameTimeRemainingInfo Deserialize(Stream stream, GameTimeRemainingInfo instance)
		{
			return GameTimeRemainingInfo.Deserialize(stream, instance, -1L);
		}

		public static GameTimeRemainingInfo DeserializeLengthDelimited(Stream stream)
		{
			GameTimeRemainingInfo gameTimeRemainingInfo = new GameTimeRemainingInfo();
			GameTimeRemainingInfo.DeserializeLengthDelimited(stream, gameTimeRemainingInfo);
			return gameTimeRemainingInfo;
		}

		public static GameTimeRemainingInfo DeserializeLengthDelimited(Stream stream, GameTimeRemainingInfo instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GameTimeRemainingInfo.Deserialize(stream, instance, num);
		}

		public static GameTimeRemainingInfo Deserialize(Stream stream, GameTimeRemainingInfo instance, long limit)
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
				else
				{
					int num2 = num;
					if (num2 != 8)
					{
						if (num2 != 16)
						{
							if (num2 != 24)
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
								instance.ParentalWeeklyMinutesRemaining = ProtocolParser.ReadUInt32(stream);
							}
						}
						else
						{
							instance.ParentalDailyMinutesRemaining = ProtocolParser.ReadUInt32(stream);
						}
					}
					else
					{
						instance.MinutesRemaining = ProtocolParser.ReadUInt32(stream);
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
			GameTimeRemainingInfo.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, GameTimeRemainingInfo instance)
		{
			if (instance.HasMinutesRemaining)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt32(stream, instance.MinutesRemaining);
			}
			if (instance.HasParentalDailyMinutesRemaining)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt32(stream, instance.ParentalDailyMinutesRemaining);
			}
			if (instance.HasParentalWeeklyMinutesRemaining)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt32(stream, instance.ParentalWeeklyMinutesRemaining);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (this.HasMinutesRemaining)
			{
				num += 1u;
				num += ProtocolParser.SizeOfUInt32(this.MinutesRemaining);
			}
			if (this.HasParentalDailyMinutesRemaining)
			{
				num += 1u;
				num += ProtocolParser.SizeOfUInt32(this.ParentalDailyMinutesRemaining);
			}
			if (this.HasParentalWeeklyMinutesRemaining)
			{
				num += 1u;
				num += ProtocolParser.SizeOfUInt32(this.ParentalWeeklyMinutesRemaining);
			}
			return num;
		}

		public uint MinutesRemaining
		{
			get
			{
				return this._MinutesRemaining;
			}
			set
			{
				this._MinutesRemaining = value;
				this.HasMinutesRemaining = true;
			}
		}

		public void SetMinutesRemaining(uint val)
		{
			this.MinutesRemaining = val;
		}

		public uint ParentalDailyMinutesRemaining
		{
			get
			{
				return this._ParentalDailyMinutesRemaining;
			}
			set
			{
				this._ParentalDailyMinutesRemaining = value;
				this.HasParentalDailyMinutesRemaining = true;
			}
		}

		public void SetParentalDailyMinutesRemaining(uint val)
		{
			this.ParentalDailyMinutesRemaining = val;
		}

		public uint ParentalWeeklyMinutesRemaining
		{
			get
			{
				return this._ParentalWeeklyMinutesRemaining;
			}
			set
			{
				this._ParentalWeeklyMinutesRemaining = value;
				this.HasParentalWeeklyMinutesRemaining = true;
			}
		}

		public void SetParentalWeeklyMinutesRemaining(uint val)
		{
			this.ParentalWeeklyMinutesRemaining = val;
		}

		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasMinutesRemaining)
			{
				num ^= this.MinutesRemaining.GetHashCode();
			}
			if (this.HasParentalDailyMinutesRemaining)
			{
				num ^= this.ParentalDailyMinutesRemaining.GetHashCode();
			}
			if (this.HasParentalWeeklyMinutesRemaining)
			{
				num ^= this.ParentalWeeklyMinutesRemaining.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			GameTimeRemainingInfo gameTimeRemainingInfo = obj as GameTimeRemainingInfo;
			return gameTimeRemainingInfo != null && this.HasMinutesRemaining == gameTimeRemainingInfo.HasMinutesRemaining && (!this.HasMinutesRemaining || this.MinutesRemaining.Equals(gameTimeRemainingInfo.MinutesRemaining)) && this.HasParentalDailyMinutesRemaining == gameTimeRemainingInfo.HasParentalDailyMinutesRemaining && (!this.HasParentalDailyMinutesRemaining || this.ParentalDailyMinutesRemaining.Equals(gameTimeRemainingInfo.ParentalDailyMinutesRemaining)) && this.HasParentalWeeklyMinutesRemaining == gameTimeRemainingInfo.HasParentalWeeklyMinutesRemaining && (!this.HasParentalWeeklyMinutesRemaining || this.ParentalWeeklyMinutesRemaining.Equals(gameTimeRemainingInfo.ParentalWeeklyMinutesRemaining));
		}

		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		public static GameTimeRemainingInfo ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GameTimeRemainingInfo>(bs, 0, -1);
		}

		public bool HasMinutesRemaining;

		private uint _MinutesRemaining;

		public bool HasParentalDailyMinutesRemaining;

		private uint _ParentalDailyMinutesRemaining;

		public bool HasParentalWeeklyMinutesRemaining;

		private uint _ParentalWeeklyMinutesRemaining;
	}
}
