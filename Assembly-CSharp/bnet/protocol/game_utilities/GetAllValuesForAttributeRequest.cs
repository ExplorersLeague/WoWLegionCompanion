using System;
using System.IO;
using System.Text;

namespace bnet.protocol.game_utilities
{
	public class GetAllValuesForAttributeRequest : IProtoBuf
	{
		public string AttributeKey
		{
			get
			{
				return this._AttributeKey;
			}
			set
			{
				this._AttributeKey = value;
				this.HasAttributeKey = (value != null);
			}
		}

		public EntityId AgentId
		{
			get
			{
				return this._AgentId;
			}
			set
			{
				this._AgentId = value;
				this.HasAgentId = (value != null);
			}
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

		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasAttributeKey)
			{
				num ^= this.AttributeKey.GetHashCode();
			}
			if (this.HasAgentId)
			{
				num ^= this.AgentId.GetHashCode();
			}
			if (this.HasProgram)
			{
				num ^= this.Program.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			GetAllValuesForAttributeRequest getAllValuesForAttributeRequest = obj as GetAllValuesForAttributeRequest;
			return getAllValuesForAttributeRequest != null && this.HasAttributeKey == getAllValuesForAttributeRequest.HasAttributeKey && (!this.HasAttributeKey || this.AttributeKey.Equals(getAllValuesForAttributeRequest.AttributeKey)) && this.HasAgentId == getAllValuesForAttributeRequest.HasAgentId && (!this.HasAgentId || this.AgentId.Equals(getAllValuesForAttributeRequest.AgentId)) && this.HasProgram == getAllValuesForAttributeRequest.HasProgram && (!this.HasProgram || this.Program.Equals(getAllValuesForAttributeRequest.Program));
		}

		public void Deserialize(Stream stream)
		{
			GetAllValuesForAttributeRequest.Deserialize(stream, this);
		}

		public static GetAllValuesForAttributeRequest Deserialize(Stream stream, GetAllValuesForAttributeRequest instance)
		{
			return GetAllValuesForAttributeRequest.Deserialize(stream, instance, -1L);
		}

		public static GetAllValuesForAttributeRequest DeserializeLengthDelimited(Stream stream)
		{
			GetAllValuesForAttributeRequest getAllValuesForAttributeRequest = new GetAllValuesForAttributeRequest();
			GetAllValuesForAttributeRequest.DeserializeLengthDelimited(stream, getAllValuesForAttributeRequest);
			return getAllValuesForAttributeRequest;
		}

		public static GetAllValuesForAttributeRequest DeserializeLengthDelimited(Stream stream, GetAllValuesForAttributeRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GetAllValuesForAttributeRequest.Deserialize(stream, instance, num);
		}

		public static GetAllValuesForAttributeRequest Deserialize(Stream stream, GetAllValuesForAttributeRequest instance, long limit)
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
				else if (num != 10)
				{
					if (num != 18)
					{
						if (num != 45)
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
					else if (instance.AgentId == null)
					{
						instance.AgentId = EntityId.DeserializeLengthDelimited(stream);
					}
					else
					{
						EntityId.DeserializeLengthDelimited(stream, instance.AgentId);
					}
				}
				else
				{
					instance.AttributeKey = ProtocolParser.ReadString(stream);
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
			GetAllValuesForAttributeRequest.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, GetAllValuesForAttributeRequest instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.HasAttributeKey)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.AttributeKey));
			}
			if (instance.HasAgentId)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.AgentId.GetSerializedSize());
				EntityId.Serialize(stream, instance.AgentId);
			}
			if (instance.HasProgram)
			{
				stream.WriteByte(45);
				binaryWriter.Write(instance.Program);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (this.HasAttributeKey)
			{
				num += 1u;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.AttributeKey);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (this.HasAgentId)
			{
				num += 1u;
				uint serializedSize = this.AgentId.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasProgram)
			{
				num += 1u;
				num += 4u;
			}
			return num;
		}

		public bool HasAttributeKey;

		private string _AttributeKey;

		public bool HasAgentId;

		private EntityId _AgentId;

		public bool HasProgram;

		private uint _Program;
	}
}
