using System;
using System.IO;

namespace bnet.protocol.channel
{
	public class Member : IProtoBuf
	{
		public Identity Identity { get; set; }

		public void SetIdentity(Identity val)
		{
			this.Identity = val;
		}

		public MemberState State { get; set; }

		public void SetState(MemberState val)
		{
			this.State = val;
		}

		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.Identity.GetHashCode();
			return num ^ this.State.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			Member member = obj as Member;
			return member != null && this.Identity.Equals(member.Identity) && this.State.Equals(member.State);
		}

		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		public static Member ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<Member>(bs, 0, -1);
		}

		public void Deserialize(Stream stream)
		{
			Member.Deserialize(stream, this);
		}

		public static Member Deserialize(Stream stream, Member instance)
		{
			return Member.Deserialize(stream, instance, -1L);
		}

		public static Member DeserializeLengthDelimited(Stream stream)
		{
			Member member = new Member();
			Member.DeserializeLengthDelimited(stream, member);
			return member;
		}

		public static Member DeserializeLengthDelimited(Stream stream, Member instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return Member.Deserialize(stream, instance, num);
		}

		public static Member Deserialize(Stream stream, Member instance, long limit)
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
					else if (instance.State == null)
					{
						instance.State = MemberState.DeserializeLengthDelimited(stream);
					}
					else
					{
						MemberState.DeserializeLengthDelimited(stream, instance.State);
					}
				}
				else if (instance.Identity == null)
				{
					instance.Identity = Identity.DeserializeLengthDelimited(stream);
				}
				else
				{
					Identity.DeserializeLengthDelimited(stream, instance.Identity);
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
			Member.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, Member instance)
		{
			if (instance.Identity == null)
			{
				throw new ArgumentNullException("Identity", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteUInt32(stream, instance.Identity.GetSerializedSize());
			Identity.Serialize(stream, instance.Identity);
			if (instance.State == null)
			{
				throw new ArgumentNullException("State", "Required by proto specification.");
			}
			stream.WriteByte(18);
			ProtocolParser.WriteUInt32(stream, instance.State.GetSerializedSize());
			MemberState.Serialize(stream, instance.State);
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			uint serializedSize = this.Identity.GetSerializedSize();
			num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			uint serializedSize2 = this.State.GetSerializedSize();
			num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			return num + 2u;
		}
	}
}
