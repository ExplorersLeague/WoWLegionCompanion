using System;
using System.IO;

namespace bnet.protocol
{
	public class ObjectAddress : IProtoBuf
	{
		public void Deserialize(Stream stream)
		{
			ObjectAddress.Deserialize(stream, this);
		}

		public static ObjectAddress Deserialize(Stream stream, ObjectAddress instance)
		{
			return ObjectAddress.Deserialize(stream, instance, -1L);
		}

		public static ObjectAddress DeserializeLengthDelimited(Stream stream)
		{
			ObjectAddress objectAddress = new ObjectAddress();
			ObjectAddress.DeserializeLengthDelimited(stream, objectAddress);
			return objectAddress;
		}

		public static ObjectAddress DeserializeLengthDelimited(Stream stream, ObjectAddress instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return ObjectAddress.Deserialize(stream, instance, num);
		}

		public static ObjectAddress Deserialize(Stream stream, ObjectAddress instance, long limit)
		{
			instance.ObjectId = 0UL;
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
						if (num2 != 16)
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
					else if (instance.Host == null)
					{
						instance.Host = ProcessId.DeserializeLengthDelimited(stream);
					}
					else
					{
						ProcessId.DeserializeLengthDelimited(stream, instance.Host);
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
			ObjectAddress.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, ObjectAddress instance)
		{
			if (instance.Host == null)
			{
				throw new ArgumentNullException("Host", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteUInt32(stream, instance.Host.GetSerializedSize());
			ProcessId.Serialize(stream, instance.Host);
			if (instance.HasObjectId)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, instance.ObjectId);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			uint serializedSize = this.Host.GetSerializedSize();
			num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			if (this.HasObjectId)
			{
				num += 1u;
				num += ProtocolParser.SizeOfUInt64(this.ObjectId);
			}
			return num + 1u;
		}

		public ProcessId Host { get; set; }

		public void SetHost(ProcessId val)
		{
			this.Host = val;
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
			num ^= this.Host.GetHashCode();
			if (this.HasObjectId)
			{
				num ^= this.ObjectId.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			ObjectAddress objectAddress = obj as ObjectAddress;
			return objectAddress != null && this.Host.Equals(objectAddress.Host) && this.HasObjectId == objectAddress.HasObjectId && (!this.HasObjectId || this.ObjectId.Equals(objectAddress.ObjectId));
		}

		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		public static ObjectAddress ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<ObjectAddress>(bs, 0, -1);
		}

		public bool HasObjectId;

		private ulong _ObjectId;
	}
}
