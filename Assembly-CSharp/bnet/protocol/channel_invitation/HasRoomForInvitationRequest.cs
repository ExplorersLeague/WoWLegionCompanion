using System;
using System.IO;
using System.Text;

namespace bnet.protocol.channel_invitation
{
	public class HasRoomForInvitationRequest : IProtoBuf
	{
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

		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.ServiceType.GetHashCode();
			if (this.HasProgram)
			{
				num ^= this.Program.GetHashCode();
			}
			if (this.HasChannelType)
			{
				num ^= this.ChannelType.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			HasRoomForInvitationRequest hasRoomForInvitationRequest = obj as HasRoomForInvitationRequest;
			return hasRoomForInvitationRequest != null && this.ServiceType.Equals(hasRoomForInvitationRequest.ServiceType) && this.HasProgram == hasRoomForInvitationRequest.HasProgram && (!this.HasProgram || this.Program.Equals(hasRoomForInvitationRequest.Program)) && this.HasChannelType == hasRoomForInvitationRequest.HasChannelType && (!this.HasChannelType || this.ChannelType.Equals(hasRoomForInvitationRequest.ChannelType));
		}

		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		public static HasRoomForInvitationRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<HasRoomForInvitationRequest>(bs, 0, -1);
		}

		public void Deserialize(Stream stream)
		{
			HasRoomForInvitationRequest.Deserialize(stream, this);
		}

		public static HasRoomForInvitationRequest Deserialize(Stream stream, HasRoomForInvitationRequest instance)
		{
			return HasRoomForInvitationRequest.Deserialize(stream, instance, -1L);
		}

		public static HasRoomForInvitationRequest DeserializeLengthDelimited(Stream stream)
		{
			HasRoomForInvitationRequest hasRoomForInvitationRequest = new HasRoomForInvitationRequest();
			HasRoomForInvitationRequest.DeserializeLengthDelimited(stream, hasRoomForInvitationRequest);
			return hasRoomForInvitationRequest;
		}

		public static HasRoomForInvitationRequest DeserializeLengthDelimited(Stream stream, HasRoomForInvitationRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return HasRoomForInvitationRequest.Deserialize(stream, instance, num);
		}

		public static HasRoomForInvitationRequest Deserialize(Stream stream, HasRoomForInvitationRequest instance, long limit)
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
			HasRoomForInvitationRequest.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, HasRoomForInvitationRequest instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			stream.WriteByte(8);
			ProtocolParser.WriteUInt32(stream, instance.ServiceType);
			if (instance.HasProgram)
			{
				stream.WriteByte(21);
				binaryWriter.Write(instance.Program);
			}
			if (instance.HasChannelType)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.ChannelType));
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			num += ProtocolParser.SizeOfUInt32(this.ServiceType);
			if (this.HasProgram)
			{
				num += 1u;
				num += 4u;
			}
			if (this.HasChannelType)
			{
				num += 1u;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.ChannelType);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			return num + 1u;
		}

		public bool HasProgram;

		private uint _Program;

		public bool HasChannelType;

		private string _ChannelType;
	}
}
