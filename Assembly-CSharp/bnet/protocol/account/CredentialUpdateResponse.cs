using System;
using System.IO;

namespace bnet.protocol.account
{
	public class CredentialUpdateResponse : IProtoBuf
	{
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode();
		}

		public override bool Equals(object obj)
		{
			return obj is CredentialUpdateResponse;
		}

		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		public static CredentialUpdateResponse ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<CredentialUpdateResponse>(bs, 0, -1);
		}

		public void Deserialize(Stream stream)
		{
			CredentialUpdateResponse.Deserialize(stream, this);
		}

		public static CredentialUpdateResponse Deserialize(Stream stream, CredentialUpdateResponse instance)
		{
			return CredentialUpdateResponse.Deserialize(stream, instance, -1L);
		}

		public static CredentialUpdateResponse DeserializeLengthDelimited(Stream stream)
		{
			CredentialUpdateResponse credentialUpdateResponse = new CredentialUpdateResponse();
			CredentialUpdateResponse.DeserializeLengthDelimited(stream, credentialUpdateResponse);
			return credentialUpdateResponse;
		}

		public static CredentialUpdateResponse DeserializeLengthDelimited(Stream stream, CredentialUpdateResponse instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return CredentialUpdateResponse.Deserialize(stream, instance, num);
		}

		public static CredentialUpdateResponse Deserialize(Stream stream, CredentialUpdateResponse instance, long limit)
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
					Key key = ProtocolParser.ReadKey((byte)num, stream);
					uint field = key.Field;
					if (field == 0u)
					{
						throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
					}
					ProtocolParser.SkipKey(stream, key);
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
			CredentialUpdateResponse.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, CredentialUpdateResponse instance)
		{
		}

		public uint GetSerializedSize()
		{
			return 0u;
		}
	}
}
