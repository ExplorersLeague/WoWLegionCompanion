using System;
using System.IO;

namespace bnet.protocol.channel
{
	public class SubscribeChannelResponse : IProtoBuf
	{
		public void Deserialize(Stream stream)
		{
			SubscribeChannelResponse.Deserialize(stream, this);
		}

		public static SubscribeChannelResponse Deserialize(Stream stream, SubscribeChannelResponse instance)
		{
			return SubscribeChannelResponse.Deserialize(stream, instance, -1L);
		}

		public static SubscribeChannelResponse DeserializeLengthDelimited(Stream stream)
		{
			SubscribeChannelResponse subscribeChannelResponse = new SubscribeChannelResponse();
			SubscribeChannelResponse.DeserializeLengthDelimited(stream, subscribeChannelResponse);
			return subscribeChannelResponse;
		}

		public static SubscribeChannelResponse DeserializeLengthDelimited(Stream stream, SubscribeChannelResponse instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return SubscribeChannelResponse.Deserialize(stream, instance, num);
		}

		public static SubscribeChannelResponse Deserialize(Stream stream, SubscribeChannelResponse instance, long limit)
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
				else if (num != 8)
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
					instance.ObjectId = ProtocolParser.ReadUInt64(stream);
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
			SubscribeChannelResponse.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, SubscribeChannelResponse instance)
		{
			if (instance.HasObjectId)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt64(stream, instance.ObjectId);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (this.HasObjectId)
			{
				num += 1u;
				num += ProtocolParser.SizeOfUInt64(this.ObjectId);
			}
			return num;
		}

		public ulong ObjectId
		{
			get
			{
				return this._ObjectId;
			}
			set
			{
				this._ObjectId = value;
				this.HasObjectId = true;
			}
		}

		public void SetObjectId(ulong val)
		{
			this.ObjectId = val;
		}

		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasObjectId)
			{
				num ^= this.ObjectId.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			SubscribeChannelResponse subscribeChannelResponse = obj as SubscribeChannelResponse;
			return subscribeChannelResponse != null && this.HasObjectId == subscribeChannelResponse.HasObjectId && (!this.HasObjectId || this.ObjectId.Equals(subscribeChannelResponse.ObjectId));
		}

		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		public static SubscribeChannelResponse ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<SubscribeChannelResponse>(bs, 0, -1);
		}

		public bool HasObjectId;

		private ulong _ObjectId;
	}
}
