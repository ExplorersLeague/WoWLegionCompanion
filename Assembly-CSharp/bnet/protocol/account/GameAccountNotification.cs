using System;
using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.account
{
	public class GameAccountNotification : IProtoBuf
	{
		public List<GameAccountList> RegionDelta
		{
			get
			{
				return this._RegionDelta;
			}
			set
			{
				this._RegionDelta = value;
			}
		}

		public List<GameAccountList> RegionDeltaList
		{
			get
			{
				return this._RegionDelta;
			}
		}

		public int RegionDeltaCount
		{
			get
			{
				return this._RegionDelta.Count;
			}
		}

		public void AddRegionDelta(GameAccountList val)
		{
			this._RegionDelta.Add(val);
		}

		public void ClearRegionDelta()
		{
			this._RegionDelta.Clear();
		}

		public void SetRegionDelta(List<GameAccountList> val)
		{
			this.RegionDelta = val;
		}

		public ulong SubscriberId
		{
			get
			{
				return this._SubscriberId;
			}
			set
			{
				this._SubscriberId = value;
				this.HasSubscriberId = true;
			}
		}

		public void SetSubscriberId(ulong val)
		{
			this.SubscriberId = val;
		}

		public AccountFieldTags AccountTags
		{
			get
			{
				return this._AccountTags;
			}
			set
			{
				this._AccountTags = value;
				this.HasAccountTags = (value != null);
			}
		}

		public void SetAccountTags(AccountFieldTags val)
		{
			this.AccountTags = val;
		}

		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			foreach (GameAccountList gameAccountList in this.RegionDelta)
			{
				num ^= gameAccountList.GetHashCode();
			}
			if (this.HasSubscriberId)
			{
				num ^= this.SubscriberId.GetHashCode();
			}
			if (this.HasAccountTags)
			{
				num ^= this.AccountTags.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			GameAccountNotification gameAccountNotification = obj as GameAccountNotification;
			if (gameAccountNotification == null)
			{
				return false;
			}
			if (this.RegionDelta.Count != gameAccountNotification.RegionDelta.Count)
			{
				return false;
			}
			for (int i = 0; i < this.RegionDelta.Count; i++)
			{
				if (!this.RegionDelta[i].Equals(gameAccountNotification.RegionDelta[i]))
				{
					return false;
				}
			}
			return this.HasSubscriberId == gameAccountNotification.HasSubscriberId && (!this.HasSubscriberId || this.SubscriberId.Equals(gameAccountNotification.SubscriberId)) && this.HasAccountTags == gameAccountNotification.HasAccountTags && (!this.HasAccountTags || this.AccountTags.Equals(gameAccountNotification.AccountTags));
		}

		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		public static GameAccountNotification ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GameAccountNotification>(bs, 0, -1);
		}

		public void Deserialize(Stream stream)
		{
			GameAccountNotification.Deserialize(stream, this);
		}

		public static GameAccountNotification Deserialize(Stream stream, GameAccountNotification instance)
		{
			return GameAccountNotification.Deserialize(stream, instance, -1L);
		}

		public static GameAccountNotification DeserializeLengthDelimited(Stream stream)
		{
			GameAccountNotification gameAccountNotification = new GameAccountNotification();
			GameAccountNotification.DeserializeLengthDelimited(stream, gameAccountNotification);
			return gameAccountNotification;
		}

		public static GameAccountNotification DeserializeLengthDelimited(Stream stream, GameAccountNotification instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GameAccountNotification.Deserialize(stream, instance, num);
		}

		public static GameAccountNotification Deserialize(Stream stream, GameAccountNotification instance, long limit)
		{
			if (instance.RegionDelta == null)
			{
				instance.RegionDelta = new List<GameAccountList>();
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
					if (num != 16)
					{
						if (num != 26)
						{
							Key key = ProtocolParser.ReadKey((byte)num, stream);
							uint field = key.Field;
							if (field == 0u)
							{
								throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
							}
							ProtocolParser.SkipKey(stream, key);
						}
						else if (instance.AccountTags == null)
						{
							instance.AccountTags = AccountFieldTags.DeserializeLengthDelimited(stream);
						}
						else
						{
							AccountFieldTags.DeserializeLengthDelimited(stream, instance.AccountTags);
						}
					}
					else
					{
						instance.SubscriberId = ProtocolParser.ReadUInt64(stream);
					}
				}
				else
				{
					instance.RegionDelta.Add(GameAccountList.DeserializeLengthDelimited(stream));
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
			GameAccountNotification.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, GameAccountNotification instance)
		{
			if (instance.RegionDelta.Count > 0)
			{
				foreach (GameAccountList gameAccountList in instance.RegionDelta)
				{
					stream.WriteByte(10);
					ProtocolParser.WriteUInt32(stream, gameAccountList.GetSerializedSize());
					GameAccountList.Serialize(stream, gameAccountList);
				}
			}
			if (instance.HasSubscriberId)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, instance.SubscriberId);
			}
			if (instance.HasAccountTags)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteUInt32(stream, instance.AccountTags.GetSerializedSize());
				AccountFieldTags.Serialize(stream, instance.AccountTags);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (this.RegionDelta.Count > 0)
			{
				foreach (GameAccountList gameAccountList in this.RegionDelta)
				{
					num += 1u;
					uint serializedSize = gameAccountList.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			if (this.HasSubscriberId)
			{
				num += 1u;
				num += ProtocolParser.SizeOfUInt64(this.SubscriberId);
			}
			if (this.HasAccountTags)
			{
				num += 1u;
				uint serializedSize2 = this.AccountTags.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			return num;
		}

		private List<GameAccountList> _RegionDelta = new List<GameAccountList>();

		public bool HasSubscriberId;

		private ulong _SubscriberId;

		public bool HasAccountTags;

		private AccountFieldTags _AccountTags;
	}
}
