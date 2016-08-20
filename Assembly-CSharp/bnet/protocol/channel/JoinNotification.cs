using System;
using System.IO;

namespace bnet.protocol.channel
{
	public class JoinNotification : IProtoBuf
	{
		public void Deserialize(Stream stream)
		{
			JoinNotification.Deserialize(stream, this);
		}

		public static JoinNotification Deserialize(Stream stream, JoinNotification instance)
		{
			return JoinNotification.Deserialize(stream, instance, -1L);
		}

		public static JoinNotification DeserializeLengthDelimited(Stream stream)
		{
			JoinNotification joinNotification = new JoinNotification();
			JoinNotification.DeserializeLengthDelimited(stream, joinNotification);
			return joinNotification;
		}

		public static JoinNotification DeserializeLengthDelimited(Stream stream, JoinNotification instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return JoinNotification.Deserialize(stream, instance, num);
		}

		public static JoinNotification Deserialize(Stream stream, JoinNotification instance, long limit)
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
						Key key = ProtocolParser.ReadKey((byte)num, stream);
						uint field = key.Field;
						if (field == 0u)
						{
							throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
						}
						ProtocolParser.SkipKey(stream, key);
					}
					else if (instance.Member == null)
					{
						instance.Member = Member.DeserializeLengthDelimited(stream);
					}
					else
					{
						Member.DeserializeLengthDelimited(stream, instance.Member);
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
			JoinNotification.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, JoinNotification instance)
		{
			if (instance.Member == null)
			{
				throw new ArgumentNullException("Member", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteUInt32(stream, instance.Member.GetSerializedSize());
			Member.Serialize(stream, instance.Member);
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			uint serializedSize = this.Member.GetSerializedSize();
			num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			return num + 1u;
		}

		public Member Member { get; set; }

		public void SetMember(Member val)
		{
			this.Member = val;
		}

		public override int GetHashCode()
		{
			int hashCode = base.GetType().GetHashCode();
			return hashCode ^ this.Member.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			JoinNotification joinNotification = obj as JoinNotification;
			return joinNotification != null && this.Member.Equals(joinNotification.Member);
		}

		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		public static JoinNotification ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<JoinNotification>(bs, 0, -1);
		}
	}
}
