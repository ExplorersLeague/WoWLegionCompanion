using System;
using System.IO;

namespace bnet.protocol.account
{
	public class CAIS : IProtoBuf
	{
		public uint PlayedMinutes
		{
			get
			{
				return this._PlayedMinutes;
			}
			set
			{
				this._PlayedMinutes = value;
				this.HasPlayedMinutes = true;
			}
		}

		public void SetPlayedMinutes(uint val)
		{
			this.PlayedMinutes = val;
		}

		public uint RestedMinutes
		{
			get
			{
				return this._RestedMinutes;
			}
			set
			{
				this._RestedMinutes = value;
				this.HasRestedMinutes = true;
			}
		}

		public void SetRestedMinutes(uint val)
		{
			this.RestedMinutes = val;
		}

		public ulong LastHeardTime
		{
			get
			{
				return this._LastHeardTime;
			}
			set
			{
				this._LastHeardTime = value;
				this.HasLastHeardTime = true;
			}
		}

		public void SetLastHeardTime(ulong val)
		{
			this.LastHeardTime = val;
		}

		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasPlayedMinutes)
			{
				num ^= this.PlayedMinutes.GetHashCode();
			}
			if (this.HasRestedMinutes)
			{
				num ^= this.RestedMinutes.GetHashCode();
			}
			if (this.HasLastHeardTime)
			{
				num ^= this.LastHeardTime.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			CAIS cais = obj as CAIS;
			return cais != null && this.HasPlayedMinutes == cais.HasPlayedMinutes && (!this.HasPlayedMinutes || this.PlayedMinutes.Equals(cais.PlayedMinutes)) && this.HasRestedMinutes == cais.HasRestedMinutes && (!this.HasRestedMinutes || this.RestedMinutes.Equals(cais.RestedMinutes)) && this.HasLastHeardTime == cais.HasLastHeardTime && (!this.HasLastHeardTime || this.LastHeardTime.Equals(cais.LastHeardTime));
		}

		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		public static CAIS ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<CAIS>(bs, 0, -1);
		}

		public void Deserialize(Stream stream)
		{
			CAIS.Deserialize(stream, this);
		}

		public static CAIS Deserialize(Stream stream, CAIS instance)
		{
			return CAIS.Deserialize(stream, instance, -1L);
		}

		public static CAIS DeserializeLengthDelimited(Stream stream)
		{
			CAIS cais = new CAIS();
			CAIS.DeserializeLengthDelimited(stream, cais);
			return cais;
		}

		public static CAIS DeserializeLengthDelimited(Stream stream, CAIS instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return CAIS.Deserialize(stream, instance, num);
		}

		public static CAIS Deserialize(Stream stream, CAIS instance, long limit)
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
							instance.LastHeardTime = ProtocolParser.ReadUInt64(stream);
						}
					}
					else
					{
						instance.RestedMinutes = ProtocolParser.ReadUInt32(stream);
					}
				}
				else
				{
					instance.PlayedMinutes = ProtocolParser.ReadUInt32(stream);
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
			CAIS.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, CAIS instance)
		{
			if (instance.HasPlayedMinutes)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt32(stream, instance.PlayedMinutes);
			}
			if (instance.HasRestedMinutes)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt32(stream, instance.RestedMinutes);
			}
			if (instance.HasLastHeardTime)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt64(stream, instance.LastHeardTime);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (this.HasPlayedMinutes)
			{
				num += 1u;
				num += ProtocolParser.SizeOfUInt32(this.PlayedMinutes);
			}
			if (this.HasRestedMinutes)
			{
				num += 1u;
				num += ProtocolParser.SizeOfUInt32(this.RestedMinutes);
			}
			if (this.HasLastHeardTime)
			{
				num += 1u;
				num += ProtocolParser.SizeOfUInt64(this.LastHeardTime);
			}
			return num;
		}

		public bool HasPlayedMinutes;

		private uint _PlayedMinutes;

		public bool HasRestedMinutes;

		private uint _RestedMinutes;

		public bool HasLastHeardTime;

		private ulong _LastHeardTime;
	}
}
