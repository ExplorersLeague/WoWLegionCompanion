using System;
using System.IO;
using System.Text;

namespace bnet.protocol.account
{
	public class GameSessionLocation : IProtoBuf
	{
		public void Deserialize(Stream stream)
		{
			GameSessionLocation.Deserialize(stream, this);
		}

		public static GameSessionLocation Deserialize(Stream stream, GameSessionLocation instance)
		{
			return GameSessionLocation.Deserialize(stream, instance, -1L);
		}

		public static GameSessionLocation DeserializeLengthDelimited(Stream stream)
		{
			GameSessionLocation gameSessionLocation = new GameSessionLocation();
			GameSessionLocation.DeserializeLengthDelimited(stream, gameSessionLocation);
			return gameSessionLocation;
		}

		public static GameSessionLocation DeserializeLengthDelimited(Stream stream, GameSessionLocation instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GameSessionLocation.Deserialize(stream, instance, num);
		}

		public static GameSessionLocation Deserialize(Stream stream, GameSessionLocation instance, long limit)
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
					if (num2 != 10)
					{
						if (num2 != 16)
						{
							if (num2 != 26)
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
								instance.City = ProtocolParser.ReadString(stream);
							}
						}
						else
						{
							instance.Country = ProtocolParser.ReadUInt32(stream);
						}
					}
					else
					{
						instance.IpAddress = ProtocolParser.ReadString(stream);
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
			GameSessionLocation.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, GameSessionLocation instance)
		{
			if (instance.HasIpAddress)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.IpAddress));
			}
			if (instance.HasCountry)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt32(stream, instance.Country);
			}
			if (instance.HasCity)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.City));
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (this.HasIpAddress)
			{
				num += 1u;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.IpAddress);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (this.HasCountry)
			{
				num += 1u;
				num += ProtocolParser.SizeOfUInt32(this.Country);
			}
			if (this.HasCity)
			{
				num += 1u;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(this.City);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			}
			return num;
		}

		public string IpAddress
		{
			get
			{
				return this._IpAddress;
			}
			set
			{
				this._IpAddress = value;
				this.HasIpAddress = (value != null);
			}
		}

		public void SetIpAddress(string val)
		{
			this.IpAddress = val;
		}

		public uint Country
		{
			get
			{
				return this._Country;
			}
			set
			{
				this._Country = value;
				this.HasCountry = true;
			}
		}

		public void SetCountry(uint val)
		{
			this.Country = val;
		}

		public string City
		{
			get
			{
				return this._City;
			}
			set
			{
				this._City = value;
				this.HasCity = (value != null);
			}
		}

		public void SetCity(string val)
		{
			this.City = val;
		}

		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasIpAddress)
			{
				num ^= this.IpAddress.GetHashCode();
			}
			if (this.HasCountry)
			{
				num ^= this.Country.GetHashCode();
			}
			if (this.HasCity)
			{
				num ^= this.City.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			GameSessionLocation gameSessionLocation = obj as GameSessionLocation;
			return gameSessionLocation != null && this.HasIpAddress == gameSessionLocation.HasIpAddress && (!this.HasIpAddress || this.IpAddress.Equals(gameSessionLocation.IpAddress)) && this.HasCountry == gameSessionLocation.HasCountry && (!this.HasCountry || this.Country.Equals(gameSessionLocation.Country)) && this.HasCity == gameSessionLocation.HasCity && (!this.HasCity || this.City.Equals(gameSessionLocation.City));
		}

		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		public static GameSessionLocation ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GameSessionLocation>(bs, 0, -1);
		}

		public bool HasIpAddress;

		private string _IpAddress;

		public bool HasCountry;

		private uint _Country;

		public bool HasCity;

		private string _City;
	}
}
