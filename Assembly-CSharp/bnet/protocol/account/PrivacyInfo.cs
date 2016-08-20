using System;
using System.IO;

namespace bnet.protocol.account
{
	public class PrivacyInfo : IProtoBuf
	{
		public void Deserialize(Stream stream)
		{
			PrivacyInfo.Deserialize(stream, this);
		}

		public static PrivacyInfo Deserialize(Stream stream, PrivacyInfo instance)
		{
			return PrivacyInfo.Deserialize(stream, instance, -1L);
		}

		public static PrivacyInfo DeserializeLengthDelimited(Stream stream)
		{
			PrivacyInfo privacyInfo = new PrivacyInfo();
			PrivacyInfo.DeserializeLengthDelimited(stream, privacyInfo);
			return privacyInfo;
		}

		public static PrivacyInfo DeserializeLengthDelimited(Stream stream, PrivacyInfo instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return PrivacyInfo.Deserialize(stream, instance, num);
		}

		public static PrivacyInfo Deserialize(Stream stream, PrivacyInfo instance, long limit)
		{
			instance.GameInfoPrivacy = PrivacyInfo.Types.GameInfoPrivacy.PRIVACY_FRIENDS;
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
					if (num2 != 24)
					{
						if (num2 != 32)
						{
							if (num2 != 40)
							{
								if (num2 != 48)
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
									instance.GameInfoPrivacy = (PrivacyInfo.Types.GameInfoPrivacy)ProtocolParser.ReadUInt64(stream);
								}
							}
							else
							{
								instance.IsHiddenFromFriendFinder = ProtocolParser.ReadBool(stream);
							}
						}
						else
						{
							instance.IsRealIdVisibleForViewFriends = ProtocolParser.ReadBool(stream);
						}
					}
					else
					{
						instance.IsUsingRid = ProtocolParser.ReadBool(stream);
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
			PrivacyInfo.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, PrivacyInfo instance)
		{
			if (instance.HasIsUsingRid)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteBool(stream, instance.IsUsingRid);
			}
			if (instance.HasIsRealIdVisibleForViewFriends)
			{
				stream.WriteByte(32);
				ProtocolParser.WriteBool(stream, instance.IsRealIdVisibleForViewFriends);
			}
			if (instance.HasIsHiddenFromFriendFinder)
			{
				stream.WriteByte(40);
				ProtocolParser.WriteBool(stream, instance.IsHiddenFromFriendFinder);
			}
			if (instance.HasGameInfoPrivacy)
			{
				stream.WriteByte(48);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.GameInfoPrivacy));
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (this.HasIsUsingRid)
			{
				num += 1u;
				num += 1u;
			}
			if (this.HasIsRealIdVisibleForViewFriends)
			{
				num += 1u;
				num += 1u;
			}
			if (this.HasIsHiddenFromFriendFinder)
			{
				num += 1u;
				num += 1u;
			}
			if (this.HasGameInfoPrivacy)
			{
				num += 1u;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.GameInfoPrivacy));
			}
			return num;
		}

		public bool IsUsingRid
		{
			get
			{
				return this._IsUsingRid;
			}
			set
			{
				this._IsUsingRid = value;
				this.HasIsUsingRid = true;
			}
		}

		public void SetIsUsingRid(bool val)
		{
			this.IsUsingRid = val;
		}

		public bool IsRealIdVisibleForViewFriends
		{
			get
			{
				return this._IsRealIdVisibleForViewFriends;
			}
			set
			{
				this._IsRealIdVisibleForViewFriends = value;
				this.HasIsRealIdVisibleForViewFriends = true;
			}
		}

		public void SetIsRealIdVisibleForViewFriends(bool val)
		{
			this.IsRealIdVisibleForViewFriends = val;
		}

		public bool IsHiddenFromFriendFinder
		{
			get
			{
				return this._IsHiddenFromFriendFinder;
			}
			set
			{
				this._IsHiddenFromFriendFinder = value;
				this.HasIsHiddenFromFriendFinder = true;
			}
		}

		public void SetIsHiddenFromFriendFinder(bool val)
		{
			this.IsHiddenFromFriendFinder = val;
		}

		public PrivacyInfo.Types.GameInfoPrivacy GameInfoPrivacy
		{
			get
			{
				return this._GameInfoPrivacy;
			}
			set
			{
				this._GameInfoPrivacy = value;
				this.HasGameInfoPrivacy = true;
			}
		}

		public void SetGameInfoPrivacy(PrivacyInfo.Types.GameInfoPrivacy val)
		{
			this.GameInfoPrivacy = val;
		}

		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasIsUsingRid)
			{
				num ^= this.IsUsingRid.GetHashCode();
			}
			if (this.HasIsRealIdVisibleForViewFriends)
			{
				num ^= this.IsRealIdVisibleForViewFriends.GetHashCode();
			}
			if (this.HasIsHiddenFromFriendFinder)
			{
				num ^= this.IsHiddenFromFriendFinder.GetHashCode();
			}
			if (this.HasGameInfoPrivacy)
			{
				num ^= this.GameInfoPrivacy.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			PrivacyInfo privacyInfo = obj as PrivacyInfo;
			return privacyInfo != null && this.HasIsUsingRid == privacyInfo.HasIsUsingRid && (!this.HasIsUsingRid || this.IsUsingRid.Equals(privacyInfo.IsUsingRid)) && this.HasIsRealIdVisibleForViewFriends == privacyInfo.HasIsRealIdVisibleForViewFriends && (!this.HasIsRealIdVisibleForViewFriends || this.IsRealIdVisibleForViewFriends.Equals(privacyInfo.IsRealIdVisibleForViewFriends)) && this.HasIsHiddenFromFriendFinder == privacyInfo.HasIsHiddenFromFriendFinder && (!this.HasIsHiddenFromFriendFinder || this.IsHiddenFromFriendFinder.Equals(privacyInfo.IsHiddenFromFriendFinder)) && this.HasGameInfoPrivacy == privacyInfo.HasGameInfoPrivacy && (!this.HasGameInfoPrivacy || this.GameInfoPrivacy.Equals(privacyInfo.GameInfoPrivacy));
		}

		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		public static PrivacyInfo ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<PrivacyInfo>(bs, 0, -1);
		}

		public bool HasIsUsingRid;

		private bool _IsUsingRid;

		public bool HasIsRealIdVisibleForViewFriends;

		private bool _IsRealIdVisibleForViewFriends;

		public bool HasIsHiddenFromFriendFinder;

		private bool _IsHiddenFromFriendFinder;

		public bool HasGameInfoPrivacy;

		private PrivacyInfo.Types.GameInfoPrivacy _GameInfoPrivacy;

		public static class Types
		{
			public enum GameInfoPrivacy
			{
				PRIVACY_ME,
				PRIVACY_FRIENDS,
				PRIVACY_EVERYONE
			}
		}
	}
}
