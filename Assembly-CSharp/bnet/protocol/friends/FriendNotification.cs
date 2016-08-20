using System;
using System.IO;

namespace bnet.protocol.friends
{
	public class FriendNotification : IProtoBuf
	{
		public void Deserialize(Stream stream)
		{
			FriendNotification.Deserialize(stream, this);
		}

		public static FriendNotification Deserialize(Stream stream, FriendNotification instance)
		{
			return FriendNotification.Deserialize(stream, instance, -1L);
		}

		public static FriendNotification DeserializeLengthDelimited(Stream stream)
		{
			FriendNotification friendNotification = new FriendNotification();
			FriendNotification.DeserializeLengthDelimited(stream, friendNotification);
			return friendNotification;
		}

		public static FriendNotification DeserializeLengthDelimited(Stream stream, FriendNotification instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return FriendNotification.Deserialize(stream, instance, num);
		}

		public static FriendNotification Deserialize(Stream stream, FriendNotification instance, long limit)
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
						if (num2 != 18)
						{
							Key key = ProtocolParser.ReadKey((byte)num, stream);
							uint field = key.Field;
							if (field == 0u)
							{
								throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
							}
							ProtocolParser.SkipKey(stream, key);
						}
						else if (instance.GameAccountId == null)
						{
							instance.GameAccountId = EntityId.DeserializeLengthDelimited(stream);
						}
						else
						{
							EntityId.DeserializeLengthDelimited(stream, instance.GameAccountId);
						}
					}
					else if (instance.Target == null)
					{
						instance.Target = Friend.DeserializeLengthDelimited(stream);
					}
					else
					{
						Friend.DeserializeLengthDelimited(stream, instance.Target);
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
			FriendNotification.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, FriendNotification instance)
		{
			if (instance.Target == null)
			{
				throw new ArgumentNullException("Target", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteUInt32(stream, instance.Target.GetSerializedSize());
			Friend.Serialize(stream, instance.Target);
			if (instance.HasGameAccountId)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.GameAccountId.GetSerializedSize());
				EntityId.Serialize(stream, instance.GameAccountId);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			uint serializedSize = this.Target.GetSerializedSize();
			num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			if (this.HasGameAccountId)
			{
				num += 1u;
				uint serializedSize2 = this.GameAccountId.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			return num + 1u;
		}

		public Friend Target { get; set; }

		public void SetTarget(Friend val)
		{
			this.Target = val;
		}

		public EntityId GameAccountId
		{
			get
			{
				return this._GameAccountId;
			}
			set
			{
				this._GameAccountId = value;
				this.HasGameAccountId = (value != null);
			}
		}

		public void SetGameAccountId(EntityId val)
		{
			this.GameAccountId = val;
		}

		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.Target.GetHashCode();
			if (this.HasGameAccountId)
			{
				num ^= this.GameAccountId.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			FriendNotification friendNotification = obj as FriendNotification;
			return friendNotification != null && this.Target.Equals(friendNotification.Target) && this.HasGameAccountId == friendNotification.HasGameAccountId && (!this.HasGameAccountId || this.GameAccountId.Equals(friendNotification.GameAccountId));
		}

		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		public static FriendNotification ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<FriendNotification>(bs, 0, -1);
		}

		public bool HasGameAccountId;

		private EntityId _GameAccountId;
	}
}
