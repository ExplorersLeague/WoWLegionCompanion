using System;
using System.IO;

namespace bnet.protocol.friends
{
	public class GenericFriendResponse : IProtoBuf
	{
		public void Deserialize(Stream stream)
		{
			GenericFriendResponse.Deserialize(stream, this);
		}

		public static GenericFriendResponse Deserialize(Stream stream, GenericFriendResponse instance)
		{
			return GenericFriendResponse.Deserialize(stream, instance, -1L);
		}

		public static GenericFriendResponse DeserializeLengthDelimited(Stream stream)
		{
			GenericFriendResponse genericFriendResponse = new GenericFriendResponse();
			GenericFriendResponse.DeserializeLengthDelimited(stream, genericFriendResponse);
			return genericFriendResponse;
		}

		public static GenericFriendResponse DeserializeLengthDelimited(Stream stream, GenericFriendResponse instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GenericFriendResponse.Deserialize(stream, instance, num);
		}

		public static GenericFriendResponse Deserialize(Stream stream, GenericFriendResponse instance, long limit)
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
				else if (instance.TargetFriend == null)
				{
					instance.TargetFriend = Friend.DeserializeLengthDelimited(stream);
				}
				else
				{
					Friend.DeserializeLengthDelimited(stream, instance.TargetFriend);
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
			GenericFriendResponse.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, GenericFriendResponse instance)
		{
			if (instance.HasTargetFriend)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.TargetFriend.GetSerializedSize());
				Friend.Serialize(stream, instance.TargetFriend);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (this.HasTargetFriend)
			{
				num += 1u;
				uint serializedSize = this.TargetFriend.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			return num;
		}

		public Friend TargetFriend
		{
			get
			{
				return this._TargetFriend;
			}
			set
			{
				this._TargetFriend = value;
				this.HasTargetFriend = (value != null);
			}
		}

		public void SetTargetFriend(Friend val)
		{
			this.TargetFriend = val;
		}

		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasTargetFriend)
			{
				num ^= this.TargetFriend.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			GenericFriendResponse genericFriendResponse = obj as GenericFriendResponse;
			return genericFriendResponse != null && this.HasTargetFriend == genericFriendResponse.HasTargetFriend && (!this.HasTargetFriend || this.TargetFriend.Equals(genericFriendResponse.TargetFriend));
		}

		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		public static GenericFriendResponse ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GenericFriendResponse>(bs, 0, -1);
		}

		public bool HasTargetFriend;

		private Friend _TargetFriend;
	}
}
