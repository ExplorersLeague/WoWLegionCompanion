using System;
using System.IO;

namespace bnet.protocol.authentication
{
	public class GenerateSSOTokenResponse : IProtoBuf
	{
		public byte[] SsoId
		{
			get
			{
				return this._SsoId;
			}
			set
			{
				this._SsoId = value;
				this.HasSsoId = (value != null);
			}
		}

		public void SetSsoId(byte[] val)
		{
			this.SsoId = val;
		}

		public byte[] SsoSecret
		{
			get
			{
				return this._SsoSecret;
			}
			set
			{
				this._SsoSecret = value;
				this.HasSsoSecret = (value != null);
			}
		}

		public void SetSsoSecret(byte[] val)
		{
			this.SsoSecret = val;
		}

		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasSsoId)
			{
				num ^= this.SsoId.GetHashCode();
			}
			if (this.HasSsoSecret)
			{
				num ^= this.SsoSecret.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			GenerateSSOTokenResponse generateSSOTokenResponse = obj as GenerateSSOTokenResponse;
			return generateSSOTokenResponse != null && this.HasSsoId == generateSSOTokenResponse.HasSsoId && (!this.HasSsoId || this.SsoId.Equals(generateSSOTokenResponse.SsoId)) && this.HasSsoSecret == generateSSOTokenResponse.HasSsoSecret && (!this.HasSsoSecret || this.SsoSecret.Equals(generateSSOTokenResponse.SsoSecret));
		}

		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		public static GenerateSSOTokenResponse ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GenerateSSOTokenResponse>(bs, 0, -1);
		}

		public void Deserialize(Stream stream)
		{
			GenerateSSOTokenResponse.Deserialize(stream, this);
		}

		public static GenerateSSOTokenResponse Deserialize(Stream stream, GenerateSSOTokenResponse instance)
		{
			return GenerateSSOTokenResponse.Deserialize(stream, instance, -1L);
		}

		public static GenerateSSOTokenResponse DeserializeLengthDelimited(Stream stream)
		{
			GenerateSSOTokenResponse generateSSOTokenResponse = new GenerateSSOTokenResponse();
			GenerateSSOTokenResponse.DeserializeLengthDelimited(stream, generateSSOTokenResponse);
			return generateSSOTokenResponse;
		}

		public static GenerateSSOTokenResponse DeserializeLengthDelimited(Stream stream, GenerateSSOTokenResponse instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GenerateSSOTokenResponse.Deserialize(stream, instance, num);
		}

		public static GenerateSSOTokenResponse Deserialize(Stream stream, GenerateSSOTokenResponse instance, long limit)
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
					else
					{
						instance.SsoSecret = ProtocolParser.ReadBytes(stream);
					}
				}
				else
				{
					instance.SsoId = ProtocolParser.ReadBytes(stream);
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
			GenerateSSOTokenResponse.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, GenerateSSOTokenResponse instance)
		{
			if (instance.HasSsoId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteBytes(stream, instance.SsoId);
			}
			if (instance.HasSsoSecret)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteBytes(stream, instance.SsoSecret);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (this.HasSsoId)
			{
				num += 1u;
				num += ProtocolParser.SizeOfUInt32(this.SsoId.Length) + (uint)this.SsoId.Length;
			}
			if (this.HasSsoSecret)
			{
				num += 1u;
				num += ProtocolParser.SizeOfUInt32(this.SsoSecret.Length) + (uint)this.SsoSecret.Length;
			}
			return num;
		}

		public bool HasSsoId;

		private byte[] _SsoId;

		public bool HasSsoSecret;

		private byte[] _SsoSecret;
	}
}
