using System;
using System.IO;

namespace bnet.protocol.account
{
	public class GameSessionInfo : IProtoBuf
	{
		public uint StartTime
		{
			get
			{
				return this._StartTime;
			}
			set
			{
				this._StartTime = value;
				this.HasStartTime = true;
			}
		}

		public void SetStartTime(uint val)
		{
			this.StartTime = val;
		}

		public GameSessionLocation Location
		{
			get
			{
				return this._Location;
			}
			set
			{
				this._Location = value;
				this.HasLocation = (value != null);
			}
		}

		public void SetLocation(GameSessionLocation val)
		{
			this.Location = val;
		}

		public bool HasBenefactor
		{
			get
			{
				return this._HasBenefactor;
			}
			set
			{
				this._HasBenefactor = value;
				this.HasHasBenefactor = true;
			}
		}

		public void SetHasBenefactor(bool val)
		{
			this.HasBenefactor = val;
		}

		public bool IsUsingIgr
		{
			get
			{
				return this._IsUsingIgr;
			}
			set
			{
				this._IsUsingIgr = value;
				this.HasIsUsingIgr = true;
			}
		}

		public void SetIsUsingIgr(bool val)
		{
			this.IsUsingIgr = val;
		}

		public bool ParentalControlsActive
		{
			get
			{
				return this._ParentalControlsActive;
			}
			set
			{
				this._ParentalControlsActive = value;
				this.HasParentalControlsActive = true;
			}
		}

		public void SetParentalControlsActive(bool val)
		{
			this.ParentalControlsActive = val;
		}

		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasStartTime)
			{
				num ^= this.StartTime.GetHashCode();
			}
			if (this.HasLocation)
			{
				num ^= this.Location.GetHashCode();
			}
			if (this.HasHasBenefactor)
			{
				num ^= this.HasBenefactor.GetHashCode();
			}
			if (this.HasIsUsingIgr)
			{
				num ^= this.IsUsingIgr.GetHashCode();
			}
			if (this.HasParentalControlsActive)
			{
				num ^= this.ParentalControlsActive.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			GameSessionInfo gameSessionInfo = obj as GameSessionInfo;
			return gameSessionInfo != null && this.HasStartTime == gameSessionInfo.HasStartTime && (!this.HasStartTime || this.StartTime.Equals(gameSessionInfo.StartTime)) && this.HasLocation == gameSessionInfo.HasLocation && (!this.HasLocation || this.Location.Equals(gameSessionInfo.Location)) && this.HasHasBenefactor == gameSessionInfo.HasHasBenefactor && (!this.HasHasBenefactor || this.HasBenefactor.Equals(gameSessionInfo.HasBenefactor)) && this.HasIsUsingIgr == gameSessionInfo.HasIsUsingIgr && (!this.HasIsUsingIgr || this.IsUsingIgr.Equals(gameSessionInfo.IsUsingIgr)) && this.HasParentalControlsActive == gameSessionInfo.HasParentalControlsActive && (!this.HasParentalControlsActive || this.ParentalControlsActive.Equals(gameSessionInfo.ParentalControlsActive));
		}

		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		public static GameSessionInfo ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GameSessionInfo>(bs, 0, -1);
		}

		public void Deserialize(Stream stream)
		{
			GameSessionInfo.Deserialize(stream, this);
		}

		public static GameSessionInfo Deserialize(Stream stream, GameSessionInfo instance)
		{
			return GameSessionInfo.Deserialize(stream, instance, -1L);
		}

		public static GameSessionInfo DeserializeLengthDelimited(Stream stream)
		{
			GameSessionInfo gameSessionInfo = new GameSessionInfo();
			GameSessionInfo.DeserializeLengthDelimited(stream, gameSessionInfo);
			return gameSessionInfo;
		}

		public static GameSessionInfo DeserializeLengthDelimited(Stream stream, GameSessionInfo instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GameSessionInfo.Deserialize(stream, instance, num);
		}

		public static GameSessionInfo Deserialize(Stream stream, GameSessionInfo instance, long limit)
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
				else if (num != 24)
				{
					if (num != 34)
					{
						if (num != 40)
						{
							if (num != 48)
							{
								if (num != 56)
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
									instance.ParentalControlsActive = ProtocolParser.ReadBool(stream);
								}
							}
							else
							{
								instance.IsUsingIgr = ProtocolParser.ReadBool(stream);
							}
						}
						else
						{
							instance.HasBenefactor = ProtocolParser.ReadBool(stream);
						}
					}
					else if (instance.Location == null)
					{
						instance.Location = GameSessionLocation.DeserializeLengthDelimited(stream);
					}
					else
					{
						GameSessionLocation.DeserializeLengthDelimited(stream, instance.Location);
					}
				}
				else
				{
					instance.StartTime = ProtocolParser.ReadUInt32(stream);
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
			GameSessionInfo.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, GameSessionInfo instance)
		{
			if (instance.HasStartTime)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt32(stream, instance.StartTime);
			}
			if (instance.HasLocation)
			{
				stream.WriteByte(34);
				ProtocolParser.WriteUInt32(stream, instance.Location.GetSerializedSize());
				GameSessionLocation.Serialize(stream, instance.Location);
			}
			if (instance.HasHasBenefactor)
			{
				stream.WriteByte(40);
				ProtocolParser.WriteBool(stream, instance.HasBenefactor);
			}
			if (instance.HasIsUsingIgr)
			{
				stream.WriteByte(48);
				ProtocolParser.WriteBool(stream, instance.IsUsingIgr);
			}
			if (instance.HasParentalControlsActive)
			{
				stream.WriteByte(56);
				ProtocolParser.WriteBool(stream, instance.ParentalControlsActive);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (this.HasStartTime)
			{
				num += 1u;
				num += ProtocolParser.SizeOfUInt32(this.StartTime);
			}
			if (this.HasLocation)
			{
				num += 1u;
				uint serializedSize = this.Location.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasHasBenefactor)
			{
				num += 1u;
				num += 1u;
			}
			if (this.HasIsUsingIgr)
			{
				num += 1u;
				num += 1u;
			}
			if (this.HasParentalControlsActive)
			{
				num += 1u;
				num += 1u;
			}
			return num;
		}

		public bool HasStartTime;

		private uint _StartTime;

		public bool HasLocation;

		private GameSessionLocation _Location;

		public bool HasHasBenefactor;

		private bool _HasBenefactor;

		public bool HasIsUsingIgr;

		private bool _IsUsingIgr;

		public bool HasParentalControlsActive;

		private bool _ParentalControlsActive;
	}
}
