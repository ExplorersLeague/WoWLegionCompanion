using System;
using System.IO;

namespace bnet.protocol.game_master
{
	public class GameStatsBucket : IProtoBuf
	{
		public float BucketMin
		{
			get
			{
				return this._BucketMin;
			}
			set
			{
				this._BucketMin = value;
				this.HasBucketMin = true;
			}
		}

		public void SetBucketMin(float val)
		{
			this.BucketMin = val;
		}

		public float BucketMax
		{
			get
			{
				return this._BucketMax;
			}
			set
			{
				this._BucketMax = value;
				this.HasBucketMax = true;
			}
		}

		public void SetBucketMax(float val)
		{
			this.BucketMax = val;
		}

		public uint WaitMilliseconds
		{
			get
			{
				return this._WaitMilliseconds;
			}
			set
			{
				this._WaitMilliseconds = value;
				this.HasWaitMilliseconds = true;
			}
		}

		public void SetWaitMilliseconds(uint val)
		{
			this.WaitMilliseconds = val;
		}

		public uint GamesPerHour
		{
			get
			{
				return this._GamesPerHour;
			}
			set
			{
				this._GamesPerHour = value;
				this.HasGamesPerHour = true;
			}
		}

		public void SetGamesPerHour(uint val)
		{
			this.GamesPerHour = val;
		}

		public uint ActiveGames
		{
			get
			{
				return this._ActiveGames;
			}
			set
			{
				this._ActiveGames = value;
				this.HasActiveGames = true;
			}
		}

		public void SetActiveGames(uint val)
		{
			this.ActiveGames = val;
		}

		public uint ActivePlayers
		{
			get
			{
				return this._ActivePlayers;
			}
			set
			{
				this._ActivePlayers = value;
				this.HasActivePlayers = true;
			}
		}

		public void SetActivePlayers(uint val)
		{
			this.ActivePlayers = val;
		}

		public uint FormingGames
		{
			get
			{
				return this._FormingGames;
			}
			set
			{
				this._FormingGames = value;
				this.HasFormingGames = true;
			}
		}

		public void SetFormingGames(uint val)
		{
			this.FormingGames = val;
		}

		public uint WaitingPlayers
		{
			get
			{
				return this._WaitingPlayers;
			}
			set
			{
				this._WaitingPlayers = value;
				this.HasWaitingPlayers = true;
			}
		}

		public void SetWaitingPlayers(uint val)
		{
			this.WaitingPlayers = val;
		}

		public uint OpenJoinableGames
		{
			get
			{
				return this._OpenJoinableGames;
			}
			set
			{
				this._OpenJoinableGames = value;
				this.HasOpenJoinableGames = true;
			}
		}

		public void SetOpenJoinableGames(uint val)
		{
			this.OpenJoinableGames = val;
		}

		public uint PlayersInOpenJoinableGames
		{
			get
			{
				return this._PlayersInOpenJoinableGames;
			}
			set
			{
				this._PlayersInOpenJoinableGames = value;
				this.HasPlayersInOpenJoinableGames = true;
			}
		}

		public void SetPlayersInOpenJoinableGames(uint val)
		{
			this.PlayersInOpenJoinableGames = val;
		}

		public uint OpenGamesTotal
		{
			get
			{
				return this._OpenGamesTotal;
			}
			set
			{
				this._OpenGamesTotal = value;
				this.HasOpenGamesTotal = true;
			}
		}

		public void SetOpenGamesTotal(uint val)
		{
			this.OpenGamesTotal = val;
		}

		public uint PlayersInOpenGamesTotal
		{
			get
			{
				return this._PlayersInOpenGamesTotal;
			}
			set
			{
				this._PlayersInOpenGamesTotal = value;
				this.HasPlayersInOpenGamesTotal = true;
			}
		}

		public void SetPlayersInOpenGamesTotal(uint val)
		{
			this.PlayersInOpenGamesTotal = val;
		}

		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasBucketMin)
			{
				num ^= this.BucketMin.GetHashCode();
			}
			if (this.HasBucketMax)
			{
				num ^= this.BucketMax.GetHashCode();
			}
			if (this.HasWaitMilliseconds)
			{
				num ^= this.WaitMilliseconds.GetHashCode();
			}
			if (this.HasGamesPerHour)
			{
				num ^= this.GamesPerHour.GetHashCode();
			}
			if (this.HasActiveGames)
			{
				num ^= this.ActiveGames.GetHashCode();
			}
			if (this.HasActivePlayers)
			{
				num ^= this.ActivePlayers.GetHashCode();
			}
			if (this.HasFormingGames)
			{
				num ^= this.FormingGames.GetHashCode();
			}
			if (this.HasWaitingPlayers)
			{
				num ^= this.WaitingPlayers.GetHashCode();
			}
			if (this.HasOpenJoinableGames)
			{
				num ^= this.OpenJoinableGames.GetHashCode();
			}
			if (this.HasPlayersInOpenJoinableGames)
			{
				num ^= this.PlayersInOpenJoinableGames.GetHashCode();
			}
			if (this.HasOpenGamesTotal)
			{
				num ^= this.OpenGamesTotal.GetHashCode();
			}
			if (this.HasPlayersInOpenGamesTotal)
			{
				num ^= this.PlayersInOpenGamesTotal.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			GameStatsBucket gameStatsBucket = obj as GameStatsBucket;
			return gameStatsBucket != null && this.HasBucketMin == gameStatsBucket.HasBucketMin && (!this.HasBucketMin || this.BucketMin.Equals(gameStatsBucket.BucketMin)) && this.HasBucketMax == gameStatsBucket.HasBucketMax && (!this.HasBucketMax || this.BucketMax.Equals(gameStatsBucket.BucketMax)) && this.HasWaitMilliseconds == gameStatsBucket.HasWaitMilliseconds && (!this.HasWaitMilliseconds || this.WaitMilliseconds.Equals(gameStatsBucket.WaitMilliseconds)) && this.HasGamesPerHour == gameStatsBucket.HasGamesPerHour && (!this.HasGamesPerHour || this.GamesPerHour.Equals(gameStatsBucket.GamesPerHour)) && this.HasActiveGames == gameStatsBucket.HasActiveGames && (!this.HasActiveGames || this.ActiveGames.Equals(gameStatsBucket.ActiveGames)) && this.HasActivePlayers == gameStatsBucket.HasActivePlayers && (!this.HasActivePlayers || this.ActivePlayers.Equals(gameStatsBucket.ActivePlayers)) && this.HasFormingGames == gameStatsBucket.HasFormingGames && (!this.HasFormingGames || this.FormingGames.Equals(gameStatsBucket.FormingGames)) && this.HasWaitingPlayers == gameStatsBucket.HasWaitingPlayers && (!this.HasWaitingPlayers || this.WaitingPlayers.Equals(gameStatsBucket.WaitingPlayers)) && this.HasOpenJoinableGames == gameStatsBucket.HasOpenJoinableGames && (!this.HasOpenJoinableGames || this.OpenJoinableGames.Equals(gameStatsBucket.OpenJoinableGames)) && this.HasPlayersInOpenJoinableGames == gameStatsBucket.HasPlayersInOpenJoinableGames && (!this.HasPlayersInOpenJoinableGames || this.PlayersInOpenJoinableGames.Equals(gameStatsBucket.PlayersInOpenJoinableGames)) && this.HasOpenGamesTotal == gameStatsBucket.HasOpenGamesTotal && (!this.HasOpenGamesTotal || this.OpenGamesTotal.Equals(gameStatsBucket.OpenGamesTotal)) && this.HasPlayersInOpenGamesTotal == gameStatsBucket.HasPlayersInOpenGamesTotal && (!this.HasPlayersInOpenGamesTotal || this.PlayersInOpenGamesTotal.Equals(gameStatsBucket.PlayersInOpenGamesTotal));
		}

		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		public static GameStatsBucket ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GameStatsBucket>(bs, 0, -1);
		}

		public void Deserialize(Stream stream)
		{
			GameStatsBucket.Deserialize(stream, this);
		}

		public static GameStatsBucket Deserialize(Stream stream, GameStatsBucket instance)
		{
			return GameStatsBucket.Deserialize(stream, instance, -1L);
		}

		public static GameStatsBucket DeserializeLengthDelimited(Stream stream)
		{
			GameStatsBucket gameStatsBucket = new GameStatsBucket();
			GameStatsBucket.DeserializeLengthDelimited(stream, gameStatsBucket);
			return gameStatsBucket;
		}

		public static GameStatsBucket DeserializeLengthDelimited(Stream stream, GameStatsBucket instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GameStatsBucket.Deserialize(stream, instance, num);
		}

		public static GameStatsBucket Deserialize(Stream stream, GameStatsBucket instance, long limit)
		{
			BinaryReader binaryReader = new BinaryReader(stream);
			instance.BucketMin = 0f;
			instance.BucketMax = 4.2949673E+09f;
			instance.WaitMilliseconds = 0u;
			instance.GamesPerHour = 0u;
			instance.ActiveGames = 0u;
			instance.ActivePlayers = 0u;
			instance.FormingGames = 0u;
			instance.WaitingPlayers = 0u;
			instance.OpenJoinableGames = 0u;
			instance.PlayersInOpenJoinableGames = 0u;
			instance.OpenGamesTotal = 0u;
			instance.PlayersInOpenGamesTotal = 0u;
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
					switch (num)
					{
					case 21:
						instance.BucketMax = binaryReader.ReadSingle();
						break;
					default:
						if (num != 13)
						{
							if (num != 32)
							{
								if (num != 40)
								{
									if (num != 48)
									{
										if (num != 56)
										{
											if (num != 64)
											{
												if (num != 72)
												{
													if (num != 80)
													{
														if (num != 88)
														{
															if (num != 96)
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
																instance.PlayersInOpenGamesTotal = ProtocolParser.ReadUInt32(stream);
															}
														}
														else
														{
															instance.OpenGamesTotal = ProtocolParser.ReadUInt32(stream);
														}
													}
													else
													{
														instance.PlayersInOpenJoinableGames = ProtocolParser.ReadUInt32(stream);
													}
												}
												else
												{
													instance.OpenJoinableGames = ProtocolParser.ReadUInt32(stream);
												}
											}
											else
											{
												instance.WaitingPlayers = ProtocolParser.ReadUInt32(stream);
											}
										}
										else
										{
											instance.FormingGames = ProtocolParser.ReadUInt32(stream);
										}
									}
									else
									{
										instance.ActivePlayers = ProtocolParser.ReadUInt32(stream);
									}
								}
								else
								{
									instance.ActiveGames = ProtocolParser.ReadUInt32(stream);
								}
							}
							else
							{
								instance.GamesPerHour = ProtocolParser.ReadUInt32(stream);
							}
						}
						else
						{
							instance.BucketMin = binaryReader.ReadSingle();
						}
						break;
					case 24:
						instance.WaitMilliseconds = ProtocolParser.ReadUInt32(stream);
						break;
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
			GameStatsBucket.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, GameStatsBucket instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.HasBucketMin)
			{
				stream.WriteByte(13);
				binaryWriter.Write(instance.BucketMin);
			}
			if (instance.HasBucketMax)
			{
				stream.WriteByte(21);
				binaryWriter.Write(instance.BucketMax);
			}
			if (instance.HasWaitMilliseconds)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt32(stream, instance.WaitMilliseconds);
			}
			if (instance.HasGamesPerHour)
			{
				stream.WriteByte(32);
				ProtocolParser.WriteUInt32(stream, instance.GamesPerHour);
			}
			if (instance.HasActiveGames)
			{
				stream.WriteByte(40);
				ProtocolParser.WriteUInt32(stream, instance.ActiveGames);
			}
			if (instance.HasActivePlayers)
			{
				stream.WriteByte(48);
				ProtocolParser.WriteUInt32(stream, instance.ActivePlayers);
			}
			if (instance.HasFormingGames)
			{
				stream.WriteByte(56);
				ProtocolParser.WriteUInt32(stream, instance.FormingGames);
			}
			if (instance.HasWaitingPlayers)
			{
				stream.WriteByte(64);
				ProtocolParser.WriteUInt32(stream, instance.WaitingPlayers);
			}
			if (instance.HasOpenJoinableGames)
			{
				stream.WriteByte(72);
				ProtocolParser.WriteUInt32(stream, instance.OpenJoinableGames);
			}
			if (instance.HasPlayersInOpenJoinableGames)
			{
				stream.WriteByte(80);
				ProtocolParser.WriteUInt32(stream, instance.PlayersInOpenJoinableGames);
			}
			if (instance.HasOpenGamesTotal)
			{
				stream.WriteByte(88);
				ProtocolParser.WriteUInt32(stream, instance.OpenGamesTotal);
			}
			if (instance.HasPlayersInOpenGamesTotal)
			{
				stream.WriteByte(96);
				ProtocolParser.WriteUInt32(stream, instance.PlayersInOpenGamesTotal);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (this.HasBucketMin)
			{
				num += 1u;
				num += 4u;
			}
			if (this.HasBucketMax)
			{
				num += 1u;
				num += 4u;
			}
			if (this.HasWaitMilliseconds)
			{
				num += 1u;
				num += ProtocolParser.SizeOfUInt32(this.WaitMilliseconds);
			}
			if (this.HasGamesPerHour)
			{
				num += 1u;
				num += ProtocolParser.SizeOfUInt32(this.GamesPerHour);
			}
			if (this.HasActiveGames)
			{
				num += 1u;
				num += ProtocolParser.SizeOfUInt32(this.ActiveGames);
			}
			if (this.HasActivePlayers)
			{
				num += 1u;
				num += ProtocolParser.SizeOfUInt32(this.ActivePlayers);
			}
			if (this.HasFormingGames)
			{
				num += 1u;
				num += ProtocolParser.SizeOfUInt32(this.FormingGames);
			}
			if (this.HasWaitingPlayers)
			{
				num += 1u;
				num += ProtocolParser.SizeOfUInt32(this.WaitingPlayers);
			}
			if (this.HasOpenJoinableGames)
			{
				num += 1u;
				num += ProtocolParser.SizeOfUInt32(this.OpenJoinableGames);
			}
			if (this.HasPlayersInOpenJoinableGames)
			{
				num += 1u;
				num += ProtocolParser.SizeOfUInt32(this.PlayersInOpenJoinableGames);
			}
			if (this.HasOpenGamesTotal)
			{
				num += 1u;
				num += ProtocolParser.SizeOfUInt32(this.OpenGamesTotal);
			}
			if (this.HasPlayersInOpenGamesTotal)
			{
				num += 1u;
				num += ProtocolParser.SizeOfUInt32(this.PlayersInOpenGamesTotal);
			}
			return num;
		}

		public bool HasBucketMin;

		private float _BucketMin;

		public bool HasBucketMax;

		private float _BucketMax;

		public bool HasWaitMilliseconds;

		private uint _WaitMilliseconds;

		public bool HasGamesPerHour;

		private uint _GamesPerHour;

		public bool HasActiveGames;

		private uint _ActiveGames;

		public bool HasActivePlayers;

		private uint _ActivePlayers;

		public bool HasFormingGames;

		private uint _FormingGames;

		public bool HasWaitingPlayers;

		private uint _WaitingPlayers;

		public bool HasOpenJoinableGames;

		private uint _OpenJoinableGames;

		public bool HasPlayersInOpenJoinableGames;

		private uint _PlayersInOpenJoinableGames;

		public bool HasOpenGamesTotal;

		private uint _OpenGamesTotal;

		public bool HasPlayersInOpenGamesTotal;

		private uint _PlayersInOpenGamesTotal;
	}
}
