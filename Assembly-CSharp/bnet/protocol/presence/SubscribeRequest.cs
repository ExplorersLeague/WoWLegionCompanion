using System;
using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.presence
{
	public class SubscribeRequest : IProtoBuf
	{
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

		public void SetAgentId(EntityId val)
		{
			this.AgentId = val;
		}

		public EntityId EntityId { get; set; }

		public void SetEntityId(EntityId val)
		{
			this.EntityId = val;
		}

		public ulong ObjectId { get; set; }

		public void SetObjectId(ulong val)
		{
			this.ObjectId = val;
		}

		public List<uint> ProgramId
		{
			get
			{
				return this._ProgramId;
			}
			set
			{
				this._ProgramId = value;
			}
		}

		public List<uint> ProgramIdList
		{
			get
			{
				return this._ProgramId;
			}
		}

		public int ProgramIdCount
		{
			get
			{
				return this._ProgramId.Count;
			}
		}

		public void AddProgramId(uint val)
		{
			this._ProgramId.Add(val);
		}

		public void ClearProgramId()
		{
			this._ProgramId.Clear();
		}

		public void SetProgramId(List<uint> val)
		{
			this.ProgramId = val;
		}

		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasAgentId)
			{
				num ^= this.AgentId.GetHashCode();
			}
			num ^= this.EntityId.GetHashCode();
			num ^= this.ObjectId.GetHashCode();
			foreach (uint num2 in this.ProgramId)
			{
				num ^= num2.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			SubscribeRequest subscribeRequest = obj as SubscribeRequest;
			if (subscribeRequest == null)
			{
				return false;
			}
			if (this.HasAgentId != subscribeRequest.HasAgentId || (this.HasAgentId && !this.AgentId.Equals(subscribeRequest.AgentId)))
			{
				return false;
			}
			if (!this.EntityId.Equals(subscribeRequest.EntityId))
			{
				return false;
			}
			if (!this.ObjectId.Equals(subscribeRequest.ObjectId))
			{
				return false;
			}
			if (this.ProgramId.Count != subscribeRequest.ProgramId.Count)
			{
				return false;
			}
			for (int i = 0; i < this.ProgramId.Count; i++)
			{
				if (!this.ProgramId[i].Equals(subscribeRequest.ProgramId[i]))
				{
					return false;
				}
			}
			return true;
		}

		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		public static SubscribeRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<SubscribeRequest>(bs, 0, -1);
		}

		public void Deserialize(Stream stream)
		{
			SubscribeRequest.Deserialize(stream, this);
		}

		public static SubscribeRequest Deserialize(Stream stream, SubscribeRequest instance)
		{
			return SubscribeRequest.Deserialize(stream, instance, -1L);
		}

		public static SubscribeRequest DeserializeLengthDelimited(Stream stream)
		{
			SubscribeRequest subscribeRequest = new SubscribeRequest();
			SubscribeRequest.DeserializeLengthDelimited(stream, subscribeRequest);
			return subscribeRequest;
		}

		public static SubscribeRequest DeserializeLengthDelimited(Stream stream, SubscribeRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return SubscribeRequest.Deserialize(stream, instance, num);
		}

		public static SubscribeRequest Deserialize(Stream stream, SubscribeRequest instance, long limit)
		{
			BinaryReader binaryReader = new BinaryReader(stream);
			if (instance.ProgramId == null)
			{
				instance.ProgramId = new List<uint>();
			}
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
						if (num != 24)
						{
							if (num != 37)
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
								instance.ProgramId.Add(binaryReader.ReadUInt32());
							}
						}
						else
						{
							instance.ObjectId = ProtocolParser.ReadUInt64(stream);
						}
					}
					else if (instance.EntityId == null)
					{
						instance.EntityId = EntityId.DeserializeLengthDelimited(stream);
					}
					else
					{
						EntityId.DeserializeLengthDelimited(stream, instance.EntityId);
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
			if (stream.Position == limit)
			{
				return instance;
			}
			throw new ProtocolBufferException("Read past max limit");
		}

		public void Serialize(Stream stream)
		{
			SubscribeRequest.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, SubscribeRequest instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.HasAgentId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.AgentId.GetSerializedSize());
				EntityId.Serialize(stream, instance.AgentId);
			}
			if (instance.EntityId == null)
			{
				throw new ArgumentNullException("EntityId", "Required by proto specification.");
			}
			stream.WriteByte(18);
			ProtocolParser.WriteUInt32(stream, instance.EntityId.GetSerializedSize());
			EntityId.Serialize(stream, instance.EntityId);
			stream.WriteByte(24);
			ProtocolParser.WriteUInt64(stream, instance.ObjectId);
			if (instance.ProgramId.Count > 0)
			{
				foreach (uint value in instance.ProgramId)
				{
					stream.WriteByte(37);
					binaryWriter.Write(value);
				}
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (this.HasAgentId)
			{
				num += 1u;
				uint serializedSize = this.AgentId.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			uint serializedSize2 = this.EntityId.GetSerializedSize();
			num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			num += ProtocolParser.SizeOfUInt64(this.ObjectId);
			if (this.ProgramId.Count > 0)
			{
				foreach (uint num2 in this.ProgramId)
				{
					num += 1u;
					num += 4u;
				}
			}
			num += 2u;
			return num;
		}

		public bool HasAgentId;

		private EntityId _AgentId;

		private List<uint> _ProgramId = new List<uint>();
	}
}
