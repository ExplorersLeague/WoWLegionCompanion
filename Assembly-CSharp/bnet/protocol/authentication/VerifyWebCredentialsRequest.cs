using System;
using System.IO;

namespace bnet.protocol.authentication
{
	public class VerifyWebCredentialsRequest : IProtoBuf
	{
		public byte[] WebCredentials
		{
			get
			{
				return this._WebCredentials;
			}
			set
			{
				this._WebCredentials = value;
				this.HasWebCredentials = (value != null);
			}
		}

		public void SetWebCredentials(byte[] val)
		{
			this.WebCredentials = val;
		}

		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasWebCredentials)
			{
				num ^= this.WebCredentials.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			VerifyWebCredentialsRequest verifyWebCredentialsRequest = obj as VerifyWebCredentialsRequest;
			return verifyWebCredentialsRequest != null && this.HasWebCredentials == verifyWebCredentialsRequest.HasWebCredentials && (!this.HasWebCredentials || this.WebCredentials.Equals(verifyWebCredentialsRequest.WebCredentials));
		}

		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		public static VerifyWebCredentialsRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<VerifyWebCredentialsRequest>(bs, 0, -1);
		}

		public void Deserialize(Stream stream)
		{
			VerifyWebCredentialsRequest.Deserialize(stream, this);
		}

		public static VerifyWebCredentialsRequest Deserialize(Stream stream, VerifyWebCredentialsRequest instance)
		{
			return VerifyWebCredentialsRequest.Deserialize(stream, instance, -1L);
		}

		public static VerifyWebCredentialsRequest DeserializeLengthDelimited(Stream stream)
		{
			VerifyWebCredentialsRequest verifyWebCredentialsRequest = new VerifyWebCredentialsRequest();
			VerifyWebCredentialsRequest.DeserializeLengthDelimited(stream, verifyWebCredentialsRequest);
			return verifyWebCredentialsRequest;
		}

		public static VerifyWebCredentialsRequest DeserializeLengthDelimited(Stream stream, VerifyWebCredentialsRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return VerifyWebCredentialsRequest.Deserialize(stream, instance, num);
		}

		public static VerifyWebCredentialsRequest Deserialize(Stream stream, VerifyWebCredentialsRequest instance, long limit)
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
					instance.WebCredentials = ProtocolParser.ReadBytes(stream);
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
			VerifyWebCredentialsRequest.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, VerifyWebCredentialsRequest instance)
		{
			if (instance.HasWebCredentials)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteBytes(stream, instance.WebCredentials);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (this.HasWebCredentials)
			{
				num += 1u;
				num += ProtocolParser.SizeOfUInt32(this.WebCredentials.Length) + (uint)this.WebCredentials.Length;
			}
			return num;
		}

		public bool HasWebCredentials;

		private byte[] _WebCredentials;
	}
}
