using System;
using System.IO;

namespace bnet.protocol.account
{
	public class GameTimeInfo : IProtoBuf
	{
		public void Deserialize(Stream stream)
		{
			GameTimeInfo.Deserialize(stream, this);
		}

		public static GameTimeInfo Deserialize(Stream stream, GameTimeInfo instance)
		{
			return GameTimeInfo.Deserialize(stream, instance, -1L);
		}

		public static GameTimeInfo DeserializeLengthDelimited(Stream stream)
		{
			GameTimeInfo gameTimeInfo = new GameTimeInfo();
			GameTimeInfo.DeserializeLengthDelimited(stream, gameTimeInfo);
			return gameTimeInfo;
		}

		public static GameTimeInfo DeserializeLengthDelimited(Stream stream, GameTimeInfo instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GameTimeInfo.Deserialize(stream, instance, num);
		}

		public static GameTimeInfo Deserialize(Stream stream, GameTimeInfo instance, long limit)
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
				else if (num != 24)
				{
					if (num != 40)
					{
						if (num != 48)
						{
							if (num != 56)
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
								instance.IsRecurringSubscription = ProtocolParser.ReadBool(stream);
							}
						}
						else
						{
							instance.IsSubscription = ProtocolParser.ReadBool(stream);
						}
					}
					else
					{
						instance.PlayTimeExpires = ProtocolParser.ReadUInt64(stream);
					}
				}
				else
				{
					instance.IsUnlimitedPlayTime = ProtocolParser.ReadBool(stream);
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
			GameTimeInfo.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, GameTimeInfo instance)
		{
			if (instance.HasIsUnlimitedPlayTime)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteBool(stream, instance.IsUnlimitedPlayTime);
			}
			if (instance.HasPlayTimeExpires)
			{
				stream.WriteByte(40);
				ProtocolParser.WriteUInt64(stream, instance.PlayTimeExpires);
			}
			if (instance.HasIsSubscription)
			{
				stream.WriteByte(48);
				ProtocolParser.WriteBool(stream, instance.IsSubscription);
			}
			if (instance.HasIsRecurringSubscription)
			{
				stream.WriteByte(56);
				ProtocolParser.WriteBool(stream, instance.IsRecurringSubscription);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (this.HasIsUnlimitedPlayTime)
			{
				num += 1u;
				num += 1u;
			}
			if (this.HasPlayTimeExpires)
			{
				num += 1u;
				num += ProtocolParser.SizeOfUInt64(this.PlayTimeExpires);
			}
			if (this.HasIsSubscription)
			{
				num += 1u;
				num += 1u;
			}
			if (this.HasIsRecurringSubscription)
			{
				num += 1u;
				num += 1u;
			}
			return num;
		}

		public bool IsUnlimitedPlayTime
		{
			get
			{
				return this._IsUnlimitedPlayTime;
			}
			set
			{
				this._IsUnlimitedPlayTime = value;
				this.HasIsUnlimitedPlayTime = true;
			}
		}

		public void SetIsUnlimitedPlayTime(bool val)
		{
			this.IsUnlimitedPlayTime = val;
		}

		public ulong PlayTimeExpires
		{
			get
			{
				return this._PlayTimeExpires;
			}
			set
			{
				this._PlayTimeExpires = value;
				this.HasPlayTimeExpires = true;
			}
		}

		public void SetPlayTimeExpires(ulong val)
		{
			this.PlayTimeExpires = val;
		}

		public bool IsSubscription
		{
			get
			{
				return this._IsSubscription;
			}
			set
			{
				this._IsSubscription = value;
				this.HasIsSubscription = true;
			}
		}

		public void SetIsSubscription(bool val)
		{
			this.IsSubscription = val;
		}

		public bool IsRecurringSubscription
		{
			get
			{
				return this._IsRecurringSubscription;
			}
			set
			{
				this._IsRecurringSubscription = value;
				this.HasIsRecurringSubscription = true;
			}
		}

		public void SetIsRecurringSubscription(bool val)
		{
			this.IsRecurringSubscription = val;
		}

		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasIsUnlimitedPlayTime)
			{
				num ^= this.IsUnlimitedPlayTime.GetHashCode();
			}
			if (this.HasPlayTimeExpires)
			{
				num ^= this.PlayTimeExpires.GetHashCode();
			}
			if (this.HasIsSubscription)
			{
				num ^= this.IsSubscription.GetHashCode();
			}
			if (this.HasIsRecurringSubscription)
			{
				num ^= this.IsRecurringSubscription.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			GameTimeInfo gameTimeInfo = obj as GameTimeInfo;
			return gameTimeInfo != null && this.HasIsUnlimitedPlayTime == gameTimeInfo.HasIsUnlimitedPlayTime && (!this.HasIsUnlimitedPlayTime || this.IsUnlimitedPlayTime.Equals(gameTimeInfo.IsUnlimitedPlayTime)) && this.HasPlayTimeExpires == gameTimeInfo.HasPlayTimeExpires && (!this.HasPlayTimeExpires || this.PlayTimeExpires.Equals(gameTimeInfo.PlayTimeExpires)) && this.HasIsSubscription == gameTimeInfo.HasIsSubscription && (!this.HasIsSubscription || this.IsSubscription.Equals(gameTimeInfo.IsSubscription)) && this.HasIsRecurringSubscription == gameTimeInfo.HasIsRecurringSubscription && (!this.HasIsRecurringSubscription || this.IsRecurringSubscription.Equals(gameTimeInfo.IsRecurringSubscription));
		}

		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		public static GameTimeInfo ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GameTimeInfo>(bs, 0, -1);
		}

		public bool HasIsUnlimitedPlayTime;

		private bool _IsUnlimitedPlayTime;

		public bool HasPlayTimeExpires;

		private ulong _PlayTimeExpires;

		public bool HasIsSubscription;

		private bool _IsSubscription;

		public bool HasIsRecurringSubscription;

		private bool _IsRecurringSubscription;
	}
}
