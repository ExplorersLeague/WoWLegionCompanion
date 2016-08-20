using System;
using System.IO;

namespace bnet.protocol.game_master
{
	public class SubscribeResponse : IProtoBuf
	{
		public void Deserialize(Stream stream)
		{
			SubscribeResponse.Deserialize(stream, this);
		}

		public static SubscribeResponse Deserialize(Stream stream, SubscribeResponse instance)
		{
			return SubscribeResponse.Deserialize(stream, instance, -1L);
		}

		public static SubscribeResponse DeserializeLengthDelimited(Stream stream)
		{
			SubscribeResponse subscribeResponse = new SubscribeResponse();
			SubscribeResponse.DeserializeLengthDelimited(stream, subscribeResponse);
			return subscribeResponse;
		}

		public static SubscribeResponse DeserializeLengthDelimited(Stream stream, SubscribeResponse instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return SubscribeResponse.Deserialize(stream, instance, num);
		}

		public static SubscribeResponse Deserialize(Stream stream, SubscribeResponse instance, long limit)
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
					if (num2 != 8)
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
						instance.SubscriptionId = ProtocolParser.ReadUInt64(stream);
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
			SubscribeResponse.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, SubscribeResponse instance)
		{
			if (instance.HasSubscriptionId)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt64(stream, instance.SubscriptionId);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (this.HasSubscriptionId)
			{
				num += 1u;
				num += ProtocolParser.SizeOfUInt64(this.SubscriptionId);
			}
			return num;
		}

		public ulong SubscriptionId
		{
			get
			{
				return this._SubscriptionId;
			}
			set
			{
				this._SubscriptionId = value;
				this.HasSubscriptionId = true;
			}
		}

		public void SetSubscriptionId(ulong val)
		{
			this.SubscriptionId = val;
		}

		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasSubscriptionId)
			{
				num ^= this.SubscriptionId.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			SubscribeResponse subscribeResponse = obj as SubscribeResponse;
			return subscribeResponse != null && this.HasSubscriptionId == subscribeResponse.HasSubscriptionId && (!this.HasSubscriptionId || this.SubscriptionId.Equals(subscribeResponse.SubscriptionId));
		}

		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		public static SubscribeResponse ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<SubscribeResponse>(bs, 0, -1);
		}

		public bool HasSubscriptionId;

		private ulong _SubscriptionId;
	}
}
