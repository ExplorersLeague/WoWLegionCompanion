using System;
using System.IO;
using System.Text;

namespace bnet.protocol.channel_invitation
{
	public class ChannelCountDescription : IProtoBuf
	{
		public void Deserialize(Stream stream)
		{
			ChannelCountDescription.Deserialize(stream, this);
		}

		public static ChannelCountDescription Deserialize(Stream stream, ChannelCountDescription instance)
		{
			return ChannelCountDescription.Deserialize(stream, instance, -1L);
		}

		public static ChannelCountDescription DeserializeLengthDelimited(Stream stream)
		{
			ChannelCountDescription channelCountDescription = new ChannelCountDescription();
			ChannelCountDescription.DeserializeLengthDelimited(stream, channelCountDescription);
			return channelCountDescription;
		}

		public static ChannelCountDescription DeserializeLengthDelimited(Stream stream, ChannelCountDescription instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return ChannelCountDescription.Deserialize(stream, instance, num);
		}

		public static ChannelCountDescription Deserialize(Stream stream, ChannelCountDescription instance, long limit)
		{
			BinaryReader binaryReader = new BinaryReader(stream);
			instance.ChannelType = "default";
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
					if (num != 21)
					{
						if (num != 26)
						{
							if (num != 34)
							{
								Key key = ProtocolParser.ReadKey((byte)num, stream);
								uint field = key.Field;
								if (field == 0u)
								{
									throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
								}
								ProtocolParser.SkipKey(stream, key);
							}
							else if (instance.ChannelId == null)
							{
								instance.ChannelId = EntityId.DeserializeLengthDelimited(stream);
							}
							else
							{
								EntityId.DeserializeLengthDelimited(stream, instance.ChannelId);
							}
						}
						else
						{
							instance.ChannelType = ProtocolParser.ReadString(stream);
						}
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
			if (stream.Position == limit)
			{
				return instance;
			}
			throw new ProtocolBufferException("Read past max limit");
		}

		public void Serialize(Stream stream)
		{
			ChannelCountDescription.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, ChannelCountDescription instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			stream.WriteByte(8);
			ProtocolParser.WriteUInt32(stream, instance.ServiceType);
			stream.WriteByte(21);
			binaryWriter.Write(instance.Program);
			if (instance.HasChannelType)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.ChannelType));
			}
			if (instance.HasChannelId)
			{
				stream.WriteByte(34);
				ProtocolParser.WriteUInt32(stream, instance.ChannelId.GetSerializedSize());
				EntityId.Serialize(stream, instance.ChannelId);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			num += ProtocolParser.SizeOfUInt32(this.ServiceType);
			num += 4u;
			if (this.HasChannelType)
			{
				num += 1u;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.ChannelType);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (this.HasChannelId)
			{
				num += 1u;
				uint serializedSize = this.ChannelId.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			return num + 2u;
		}

		public uint ServiceType { get; set; }

		public void SetServiceType(uint val)
		{
			this.ServiceType = val;
		}

		public uint Program { get; set; }

		public void SetProgram(uint val)
		{
			this.Program = val;
		}

		public string ChannelType
		{
			get
			{
				return this._ChannelType;
			}
			set
			{
				this._ChannelType = value;
				this.HasChannelType = (value != null);
			}
		}

		public void SetChannelType(string val)
		{
			this.ChannelType = val;
		}

		public EntityId ChannelId
		{
			get
			{
				return this._ChannelId;
			}
			set
			{
				this._ChannelId = value;
				this.HasChannelId = (value != null);
			}
		}

		public void SetChannelId(EntityId val)
		{
			this.ChannelId = val;
		}

		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.ServiceType.GetHashCode();
			num ^= this.Program.GetHashCode();
			if (this.HasChannelType)
			{
				num ^= this.ChannelType.GetHashCode();
			}
			if (this.HasChannelId)
			{
				num ^= this.ChannelId.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			ChannelCountDescription channelCountDescription = obj as ChannelCountDescription;
			return channelCountDescription != null && this.ServiceType.Equals(channelCountDescription.ServiceType) && this.Program.Equals(channelCountDescription.Program) && this.HasChannelType == channelCountDescription.HasChannelType && (!this.HasChannelType || this.ChannelType.Equals(channelCountDescription.ChannelType)) && this.HasChannelId == channelCountDescription.HasChannelId && (!this.HasChannelId || this.ChannelId.Equals(channelCountDescription.ChannelId));
		}

		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		public static ChannelCountDescription ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<ChannelCountDescription>(bs, 0, -1);
		}

		public bool HasChannelType;

		private string _ChannelType;

		public bool HasChannelId;

		private EntityId _ChannelId;
	}
}
