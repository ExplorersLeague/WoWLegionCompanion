using System;
using System.IO;

namespace bnet.protocol.friends
{
	public class UpdateFriendStateNotification : IProtoBuf
	{
		public Friend ChangedFriend { get; set; }

		public void SetChangedFriend(Friend val)
		{
			this.ChangedFriend = val;
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
			num ^= this.ChangedFriend.GetHashCode();
			if (this.HasGameAccountId)
			{
				num ^= this.GameAccountId.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			UpdateFriendStateNotification updateFriendStateNotification = obj as UpdateFriendStateNotification;
			return updateFriendStateNotification != null && this.ChangedFriend.Equals(updateFriendStateNotification.ChangedFriend) && this.HasGameAccountId == updateFriendStateNotification.HasGameAccountId && (!this.HasGameAccountId || this.GameAccountId.Equals(updateFriendStateNotification.GameAccountId));
		}

		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		public static UpdateFriendStateNotification ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<UpdateFriendStateNotification>(bs, 0, -1);
		}

		public void Deserialize(Stream stream)
		{
			UpdateFriendStateNotification.Deserialize(stream, this);
		}

		public static UpdateFriendStateNotification Deserialize(Stream stream, UpdateFriendStateNotification instance)
		{
			return UpdateFriendStateNotification.Deserialize(stream, instance, -1L);
		}

		public static UpdateFriendStateNotification DeserializeLengthDelimited(Stream stream)
		{
			UpdateFriendStateNotification updateFriendStateNotification = new UpdateFriendStateNotification();
			UpdateFriendStateNotification.DeserializeLengthDelimited(stream, updateFriendStateNotification);
			return updateFriendStateNotification;
		}

		public static UpdateFriendStateNotification DeserializeLengthDelimited(Stream stream, UpdateFriendStateNotification instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return UpdateFriendStateNotification.Deserialize(stream, instance, num);
		}

		public static UpdateFriendStateNotification Deserialize(Stream stream, UpdateFriendStateNotification instance, long limit)
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
					else if (instance.GameAccountId == null)
					{
						instance.GameAccountId = EntityId.DeserializeLengthDelimited(stream);
					}
					else
					{
						EntityId.DeserializeLengthDelimited(stream, instance.GameAccountId);
					}
				}
				else if (instance.ChangedFriend == null)
				{
					instance.ChangedFriend = Friend.DeserializeLengthDelimited(stream);
				}
				else
				{
					Friend.DeserializeLengthDelimited(stream, instance.ChangedFriend);
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
			UpdateFriendStateNotification.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, UpdateFriendStateNotification instance)
		{
			if (instance.ChangedFriend == null)
			{
				throw new ArgumentNullException("ChangedFriend", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteUInt32(stream, instance.ChangedFriend.GetSerializedSize());
			Friend.Serialize(stream, instance.ChangedFriend);
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
			uint serializedSize = this.ChangedFriend.GetSerializedSize();
			num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			if (this.HasGameAccountId)
			{
				num += 1u;
				uint serializedSize2 = this.GameAccountId.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			return num + 1u;
		}

		public bool HasGameAccountId;

		private EntityId _GameAccountId;
	}
}
