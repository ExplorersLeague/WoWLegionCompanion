using System;
using System.IO;

namespace bnet.protocol.account
{
	public class GetGameTimeRemainingInfoRequest : IProtoBuf
	{
		public void Deserialize(Stream stream)
		{
			GetGameTimeRemainingInfoRequest.Deserialize(stream, this);
		}

		public static GetGameTimeRemainingInfoRequest Deserialize(Stream stream, GetGameTimeRemainingInfoRequest instance)
		{
			return GetGameTimeRemainingInfoRequest.Deserialize(stream, instance, -1L);
		}

		public static GetGameTimeRemainingInfoRequest DeserializeLengthDelimited(Stream stream)
		{
			GetGameTimeRemainingInfoRequest getGameTimeRemainingInfoRequest = new GetGameTimeRemainingInfoRequest();
			GetGameTimeRemainingInfoRequest.DeserializeLengthDelimited(stream, getGameTimeRemainingInfoRequest);
			return getGameTimeRemainingInfoRequest;
		}

		public static GetGameTimeRemainingInfoRequest DeserializeLengthDelimited(Stream stream, GetGameTimeRemainingInfoRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GetGameTimeRemainingInfoRequest.Deserialize(stream, instance, num);
		}

		public static GetGameTimeRemainingInfoRequest Deserialize(Stream stream, GetGameTimeRemainingInfoRequest instance, long limit)
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
						else if (instance.AccountId == null)
						{
							instance.AccountId = EntityId.DeserializeLengthDelimited(stream);
						}
						else
						{
							EntityId.DeserializeLengthDelimited(stream, instance.AccountId);
						}
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
			}
			if (stream.Position == limit)
			{
				return instance;
			}
			throw new ProtocolBufferException("Read past max limit");
		}

		public void Serialize(Stream stream)
		{
			GetGameTimeRemainingInfoRequest.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, GetGameTimeRemainingInfoRequest instance)
		{
			if (instance.HasGameAccountId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.GameAccountId.GetSerializedSize());
				EntityId.Serialize(stream, instance.GameAccountId);
			}
			if (instance.HasAccountId)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.AccountId.GetSerializedSize());
				EntityId.Serialize(stream, instance.AccountId);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (this.HasGameAccountId)
			{
				num += 1u;
				uint serializedSize = this.GameAccountId.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasAccountId)
			{
				num += 1u;
				uint serializedSize2 = this.AccountId.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			return num;
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

		public EntityId AccountId
		{
			get
			{
				return this._AccountId;
			}
			set
			{
				this._AccountId = value;
				this.HasAccountId = (value != null);
			}
		}

		public void SetAccountId(EntityId val)
		{
			this.AccountId = val;
		}

		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasGameAccountId)
			{
				num ^= this.GameAccountId.GetHashCode();
			}
			if (this.HasAccountId)
			{
				num ^= this.AccountId.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			GetGameTimeRemainingInfoRequest getGameTimeRemainingInfoRequest = obj as GetGameTimeRemainingInfoRequest;
			return getGameTimeRemainingInfoRequest != null && this.HasGameAccountId == getGameTimeRemainingInfoRequest.HasGameAccountId && (!this.HasGameAccountId || this.GameAccountId.Equals(getGameTimeRemainingInfoRequest.GameAccountId)) && this.HasAccountId == getGameTimeRemainingInfoRequest.HasAccountId && (!this.HasAccountId || this.AccountId.Equals(getGameTimeRemainingInfoRequest.AccountId));
		}

		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		public static GetGameTimeRemainingInfoRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GetGameTimeRemainingInfoRequest>(bs, 0, -1);
		}

		public bool HasGameAccountId;

		private EntityId _GameAccountId;

		public bool HasAccountId;

		private EntityId _AccountId;
	}
}
