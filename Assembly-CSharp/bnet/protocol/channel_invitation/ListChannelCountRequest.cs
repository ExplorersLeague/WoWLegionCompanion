using System;
using System.IO;

namespace bnet.protocol.channel_invitation
{
	public class ListChannelCountRequest : IProtoBuf
	{
		public void Deserialize(Stream stream)
		{
			ListChannelCountRequest.Deserialize(stream, this);
		}

		public static ListChannelCountRequest Deserialize(Stream stream, ListChannelCountRequest instance)
		{
			return ListChannelCountRequest.Deserialize(stream, instance, -1L);
		}

		public static ListChannelCountRequest DeserializeLengthDelimited(Stream stream)
		{
			ListChannelCountRequest listChannelCountRequest = new ListChannelCountRequest();
			ListChannelCountRequest.DeserializeLengthDelimited(stream, listChannelCountRequest);
			return listChannelCountRequest;
		}

		public static ListChannelCountRequest DeserializeLengthDelimited(Stream stream, ListChannelCountRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return ListChannelCountRequest.Deserialize(stream, instance, num);
		}

		public static ListChannelCountRequest Deserialize(Stream stream, ListChannelCountRequest instance, long limit)
		{
			BinaryReader binaryReader = new BinaryReader(stream);
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
							if (num2 != 29)
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
								instance.Program = binaryReader.ReadUInt32();
							}
						}
						else
						{
							instance.ServiceType = ProtocolParser.ReadUInt32(stream);
						}
					}
					else if (instance.MemberId == null)
					{
						instance.MemberId = EntityId.DeserializeLengthDelimited(stream);
					}
					else
					{
						EntityId.DeserializeLengthDelimited(stream, instance.MemberId);
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
			ListChannelCountRequest.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, ListChannelCountRequest instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.MemberId == null)
			{
				throw new ArgumentNullException("MemberId", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteUInt32(stream, instance.MemberId.GetSerializedSize());
			EntityId.Serialize(stream, instance.MemberId);
			stream.WriteByte(16);
			ProtocolParser.WriteUInt32(stream, instance.ServiceType);
			if (instance.HasProgram)
			{
				stream.WriteByte(29);
				binaryWriter.Write(instance.Program);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			uint serializedSize = this.MemberId.GetSerializedSize();
			num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			num += ProtocolParser.SizeOfUInt32(this.ServiceType);
			if (this.HasProgram)
			{
				num += 1u;
				num += 4u;
			}
			return num + 2u;
		}

		public EntityId MemberId { get; set; }

		public void SetMemberId(EntityId val)
		{
			this.MemberId = val;
		}

		public uint ServiceType { get; set; }

		public void SetServiceType(uint val)
		{
			this.ServiceType = val;
		}

		public uint Program
		{
			get
			{
				return this._Program;
			}
			set
			{
				this._Program = value;
				this.HasProgram = true;
			}
		}

		public void SetProgram(uint val)
		{
			this.Program = val;
		}

		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.MemberId.GetHashCode();
			num ^= this.ServiceType.GetHashCode();
			if (this.HasProgram)
			{
				num ^= this.Program.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			ListChannelCountRequest listChannelCountRequest = obj as ListChannelCountRequest;
			return listChannelCountRequest != null && this.MemberId.Equals(listChannelCountRequest.MemberId) && this.ServiceType.Equals(listChannelCountRequest.ServiceType) && this.HasProgram == listChannelCountRequest.HasProgram && (!this.HasProgram || this.Program.Equals(listChannelCountRequest.Program));
		}

		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		public static ListChannelCountRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<ListChannelCountRequest>(bs, 0, -1);
		}

		public bool HasProgram;

		private uint _Program;
	}
}
