using System;
using System.IO;

namespace bnet.protocol.account
{
	public class GameStatus : IProtoBuf
	{
		public void Deserialize(Stream stream)
		{
			GameStatus.Deserialize(stream, this);
		}

		public static GameStatus Deserialize(Stream stream, GameStatus instance)
		{
			return GameStatus.Deserialize(stream, instance, -1L);
		}

		public static GameStatus DeserializeLengthDelimited(Stream stream)
		{
			GameStatus gameStatus = new GameStatus();
			GameStatus.DeserializeLengthDelimited(stream, gameStatus);
			return gameStatus;
		}

		public static GameStatus DeserializeLengthDelimited(Stream stream, GameStatus instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GameStatus.Deserialize(stream, instance, num);
		}

		public static GameStatus Deserialize(Stream stream, GameStatus instance, long limit)
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
					if (num2 != 32)
					{
						if (num2 != 40)
						{
							if (num2 != 48)
							{
								if (num2 != 61)
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
									instance.Program = binaryReader.ReadUInt32();
								}
							}
							else
							{
								instance.SuspensionExpires = ProtocolParser.ReadUInt64(stream);
							}
						}
						else
						{
							instance.IsBanned = ProtocolParser.ReadBool(stream);
						}
					}
					else
					{
						instance.IsSuspended = ProtocolParser.ReadBool(stream);
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
			GameStatus.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, GameStatus instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.HasIsSuspended)
			{
				stream.WriteByte(32);
				ProtocolParser.WriteBool(stream, instance.IsSuspended);
			}
			if (instance.HasIsBanned)
			{
				stream.WriteByte(40);
				ProtocolParser.WriteBool(stream, instance.IsBanned);
			}
			if (instance.HasSuspensionExpires)
			{
				stream.WriteByte(48);
				ProtocolParser.WriteUInt64(stream, instance.SuspensionExpires);
			}
			if (instance.HasProgram)
			{
				stream.WriteByte(61);
				binaryWriter.Write(instance.Program);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (this.HasIsSuspended)
			{
				num += 1u;
				num += 1u;
			}
			if (this.HasIsBanned)
			{
				num += 1u;
				num += 1u;
			}
			if (this.HasSuspensionExpires)
			{
				num += 1u;
				num += ProtocolParser.SizeOfUInt64(this.SuspensionExpires);
			}
			if (this.HasProgram)
			{
				num += 1u;
				num += 4u;
			}
			return num;
		}

		public bool IsSuspended
		{
			get
			{
				return this._IsSuspended;
			}
			set
			{
				this._IsSuspended = value;
				this.HasIsSuspended = true;
			}
		}

		public void SetIsSuspended(bool val)
		{
			this.IsSuspended = val;
		}

		public bool IsBanned
		{
			get
			{
				return this._IsBanned;
			}
			set
			{
				this._IsBanned = value;
				this.HasIsBanned = true;
			}
		}

		public void SetIsBanned(bool val)
		{
			this.IsBanned = val;
		}

		public ulong SuspensionExpires
		{
			get
			{
				return this._SuspensionExpires;
			}
			set
			{
				this._SuspensionExpires = value;
				this.HasSuspensionExpires = true;
			}
		}

		public void SetSuspensionExpires(ulong val)
		{
			this.SuspensionExpires = val;
		}

		public uint Program
		{
			get
			{
				return this._Program;
			}
			set
			{
				this._Program = value;
				this.HasProgram = true;
			}
		}

		public void SetProgram(uint val)
		{
			this.Program = val;
		}

		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasIsSuspended)
			{
				num ^= this.IsSuspended.GetHashCode();
			}
			if (this.HasIsBanned)
			{
				num ^= this.IsBanned.GetHashCode();
			}
			if (this.HasSuspensionExpires)
			{
				num ^= this.SuspensionExpires.GetHashCode();
			}
			if (this.HasProgram)
			{
				num ^= this.Program.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			GameStatus gameStatus = obj as GameStatus;
			return gameStatus != null && this.HasIsSuspended == gameStatus.HasIsSuspended && (!this.HasIsSuspended || this.IsSuspended.Equals(gameStatus.IsSuspended)) && this.HasIsBanned == gameStatus.HasIsBanned && (!this.HasIsBanned || this.IsBanned.Equals(gameStatus.IsBanned)) && this.HasSuspensionExpires == gameStatus.HasSuspensionExpires && (!this.HasSuspensionExpires || this.SuspensionExpires.Equals(gameStatus.SuspensionExpires)) && this.HasProgram == gameStatus.HasProgram && (!this.HasProgram || this.Program.Equals(gameStatus.Program));
		}

		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		public static GameStatus ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GameStatus>(bs, 0, -1);
		}

		public bool HasIsSuspended;

		private bool _IsSuspended;

		public bool HasIsBanned;

		private bool _IsBanned;

		public bool HasSuspensionExpires;

		private ulong _SuspensionExpires;

		public bool HasProgram;

		private uint _Program;
	}
}
