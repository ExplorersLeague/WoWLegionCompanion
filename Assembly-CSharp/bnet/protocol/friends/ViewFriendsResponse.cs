using System;
using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.friends
{
	public class ViewFriendsResponse : IProtoBuf
	{
		public List<Friend> Friends
		{
			get
			{
				return this._Friends;
			}
			set
			{
				this._Friends = value;
			}
		}

		public List<Friend> FriendsList
		{
			get
			{
				return this._Friends;
			}
		}

		public int FriendsCount
		{
			get
			{
				return this._Friends.Count;
			}
		}

		public void AddFriends(Friend val)
		{
			this._Friends.Add(val);
		}

		public void ClearFriends()
		{
			this._Friends.Clear();
		}

		public void SetFriends(List<Friend> val)
		{
			this.Friends = val;
		}

		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			foreach (Friend friend in this.Friends)
			{
				num ^= friend.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			ViewFriendsResponse viewFriendsResponse = obj as ViewFriendsResponse;
			if (viewFriendsResponse == null)
			{
				return false;
			}
			if (this.Friends.Count != viewFriendsResponse.Friends.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Friends.Count; i++)
			{
				if (!this.Friends[i].Equals(viewFriendsResponse.Friends[i]))
				{
					return false;
				}
			}
			return true;
		}

		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		public static ViewFriendsResponse ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<ViewFriendsResponse>(bs, 0, -1);
		}

		public void Deserialize(Stream stream)
		{
			ViewFriendsResponse.Deserialize(stream, this);
		}

		public static ViewFriendsResponse Deserialize(Stream stream, ViewFriendsResponse instance)
		{
			return ViewFriendsResponse.Deserialize(stream, instance, -1L);
		}

		public static ViewFriendsResponse DeserializeLengthDelimited(Stream stream)
		{
			ViewFriendsResponse viewFriendsResponse = new ViewFriendsResponse();
			ViewFriendsResponse.DeserializeLengthDelimited(stream, viewFriendsResponse);
			return viewFriendsResponse;
		}

		public static ViewFriendsResponse DeserializeLengthDelimited(Stream stream, ViewFriendsResponse instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return ViewFriendsResponse.Deserialize(stream, instance, num);
		}

		public static ViewFriendsResponse Deserialize(Stream stream, ViewFriendsResponse instance, long limit)
		{
			if (instance.Friends == null)
			{
				instance.Friends = new List<Friend>();
			}
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
					instance.Friends.Add(Friend.DeserializeLengthDelimited(stream));
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
			ViewFriendsResponse.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, ViewFriendsResponse instance)
		{
			if (instance.Friends.Count > 0)
			{
				foreach (Friend friend in instance.Friends)
				{
					stream.WriteByte(10);
					ProtocolParser.WriteUInt32(stream, friend.GetSerializedSize());
					Friend.Serialize(stream, friend);
				}
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (this.Friends.Count > 0)
			{
				foreach (Friend friend in this.Friends)
				{
					num += 1u;
					uint serializedSize = friend.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			return num;
		}

		private List<Friend> _Friends = new List<Friend>();
	}
}
