using System;
using System.IO;

namespace bnet.protocol.invitation
{
	public class UpdateInvitationRequest : IProtoBuf
	{
		public void Deserialize(Stream stream)
		{
			UpdateInvitationRequest.Deserialize(stream, this);
		}

		public static UpdateInvitationRequest Deserialize(Stream stream, UpdateInvitationRequest instance)
		{
			return UpdateInvitationRequest.Deserialize(stream, instance, -1L);
		}

		public static UpdateInvitationRequest DeserializeLengthDelimited(Stream stream)
		{
			UpdateInvitationRequest updateInvitationRequest = new UpdateInvitationRequest();
			UpdateInvitationRequest.DeserializeLengthDelimited(stream, updateInvitationRequest);
			return updateInvitationRequest;
		}

		public static UpdateInvitationRequest DeserializeLengthDelimited(Stream stream, UpdateInvitationRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return UpdateInvitationRequest.Deserialize(stream, instance, num);
		}

		public static UpdateInvitationRequest Deserialize(Stream stream, UpdateInvitationRequest instance, long limit)
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
						if (num2 != 17)
						{
							if (num2 != 26)
							{
								Key key = ProtocolParser.ReadKey((byte)num, stream);
								uint field = key.Field;
								if (field == 0u)
								{
									throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
								}
								ProtocolParser.SkipKey(stream, key);
							}
							else if (instance.Params == null)
							{
								instance.Params = InvitationParams.DeserializeLengthDelimited(stream);
							}
							else
							{
								InvitationParams.DeserializeLengthDelimited(stream, instance.Params);
							}
						}
						else
						{
							instance.InvitationId = binaryReader.ReadUInt64();
						}
					}
					else if (instance.AgentIdentity == null)
					{
						instance.AgentIdentity = Identity.DeserializeLengthDelimited(stream);
					}
					else
					{
						Identity.DeserializeLengthDelimited(stream, instance.AgentIdentity);
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
			UpdateInvitationRequest.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, UpdateInvitationRequest instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.HasAgentIdentity)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.AgentIdentity.GetSerializedSize());
				Identity.Serialize(stream, instance.AgentIdentity);
			}
			stream.WriteByte(17);
			binaryWriter.Write(instance.InvitationId);
			if (instance.Params == null)
			{
				throw new ArgumentNullException("Params", "Required by proto specification.");
			}
			stream.WriteByte(26);
			ProtocolParser.WriteUInt32(stream, instance.Params.GetSerializedSize());
			InvitationParams.Serialize(stream, instance.Params);
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (this.HasAgentIdentity)
			{
				num += 1u;
				uint serializedSize = this.AgentIdentity.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			num += 8u;
			uint serializedSize2 = this.Params.GetSerializedSize();
			num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			return num + 2u;
		}

		public Identity AgentIdentity
		{
			get
			{
				return this._AgentIdentity;
			}
			set
			{
				this._AgentIdentity = value;
				this.HasAgentIdentity = (value != null);
			}
		}

		public void SetAgentIdentity(Identity val)
		{
			this.AgentIdentity = val;
		}

		public ulong InvitationId { get; set; }

		public void SetInvitationId(ulong val)
		{
			this.InvitationId = val;
		}

		public InvitationParams Params { get; set; }

		public void SetParams(InvitationParams val)
		{
			this.Params = val;
		}

		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasAgentIdentity)
			{
				num ^= this.AgentIdentity.GetHashCode();
			}
			num ^= this.InvitationId.GetHashCode();
			return num ^ this.Params.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			UpdateInvitationRequest updateInvitationRequest = obj as UpdateInvitationRequest;
			return updateInvitationRequest != null && this.HasAgentIdentity == updateInvitationRequest.HasAgentIdentity && (!this.HasAgentIdentity || this.AgentIdentity.Equals(updateInvitationRequest.AgentIdentity)) && this.InvitationId.Equals(updateInvitationRequest.InvitationId) && this.Params.Equals(updateInvitationRequest.Params);
		}

		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		public static UpdateInvitationRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<UpdateInvitationRequest>(bs, 0, -1);
		}

		public bool HasAgentIdentity;

		private Identity _AgentIdentity;
	}
}
