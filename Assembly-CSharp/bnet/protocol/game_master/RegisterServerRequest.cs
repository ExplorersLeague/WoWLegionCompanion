using System;
using System.Collections.Generic;
using System.IO;
using bnet.protocol.attribute;
using bnet.protocol.server_pool;

namespace bnet.protocol.game_master
{
	public class RegisterServerRequest : IProtoBuf
	{
		public void Deserialize(Stream stream)
		{
			RegisterServerRequest.Deserialize(stream, this);
		}

		public static RegisterServerRequest Deserialize(Stream stream, RegisterServerRequest instance)
		{
			return RegisterServerRequest.Deserialize(stream, instance, -1L);
		}

		public static RegisterServerRequest DeserializeLengthDelimited(Stream stream)
		{
			RegisterServerRequest registerServerRequest = new RegisterServerRequest();
			RegisterServerRequest.DeserializeLengthDelimited(stream, registerServerRequest);
			return registerServerRequest;
		}

		public static RegisterServerRequest DeserializeLengthDelimited(Stream stream, RegisterServerRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return RegisterServerRequest.Deserialize(stream, instance, num);
		}

		public static RegisterServerRequest Deserialize(Stream stream, RegisterServerRequest instance, long limit)
		{
			BinaryReader binaryReader = new BinaryReader(stream);
			if (instance.Attribute == null)
			{
				instance.Attribute = new List<bnet.protocol.attribute.Attribute>();
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
						if (num != 29)
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
							instance.ProgramId = binaryReader.ReadUInt32();
						}
					}
					else if (instance.State == null)
					{
						instance.State = ServerState.DeserializeLengthDelimited(stream);
					}
					else
					{
						ServerState.DeserializeLengthDelimited(stream, instance.State);
					}
				}
				else
				{
					instance.Attribute.Add(bnet.protocol.attribute.Attribute.DeserializeLengthDelimited(stream));
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
			RegisterServerRequest.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, RegisterServerRequest instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.Attribute.Count > 0)
			{
				foreach (bnet.protocol.attribute.Attribute attribute in instance.Attribute)
				{
					stream.WriteByte(10);
					ProtocolParser.WriteUInt32(stream, attribute.GetSerializedSize());
					bnet.protocol.attribute.Attribute.Serialize(stream, attribute);
				}
			}
			if (instance.HasState)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.State.GetSerializedSize());
				ServerState.Serialize(stream, instance.State);
			}
			stream.WriteByte(29);
			binaryWriter.Write(instance.ProgramId);
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (this.Attribute.Count > 0)
			{
				foreach (bnet.protocol.attribute.Attribute attribute in this.Attribute)
				{
					num += 1u;
					uint serializedSize = attribute.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			if (this.HasState)
			{
				num += 1u;
				uint serializedSize2 = this.State.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			num += 4u;
			num += 1u;
			return num;
		}

		public List<bnet.protocol.attribute.Attribute> Attribute
		{
			get
			{
				return this._Attribute;
			}
			set
			{
				this._Attribute = value;
			}
		}

		public List<bnet.protocol.attribute.Attribute> AttributeList
		{
			get
			{
				return this._Attribute;
			}
		}

		public int AttributeCount
		{
			get
			{
				return this._Attribute.Count;
			}
		}

		public void AddAttribute(bnet.protocol.attribute.Attribute val)
		{
			this._Attribute.Add(val);
		}

		public void ClearAttribute()
		{
			this._Attribute.Clear();
		}

		public void SetAttribute(List<bnet.protocol.attribute.Attribute> val)
		{
			this.Attribute = val;
		}

		public ServerState State
		{
			get
			{
				return this._State;
			}
			set
			{
				this._State = value;
				this.HasState = (value != null);
			}
		}

		public void SetState(ServerState val)
		{
			this.State = val;
		}

		public uint ProgramId { get; set; }

		public void SetProgramId(uint val)
		{
			this.ProgramId = val;
		}

		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			foreach (bnet.protocol.attribute.Attribute attribute in this.Attribute)
			{
				num ^= attribute.GetHashCode();
			}
			if (this.HasState)
			{
				num ^= this.State.GetHashCode();
			}
			num ^= this.ProgramId.GetHashCode();
			return num;
		}

		public override bool Equals(object obj)
		{
			RegisterServerRequest registerServerRequest = obj as RegisterServerRequest;
			if (registerServerRequest == null)
			{
				return false;
			}
			if (this.Attribute.Count != registerServerRequest.Attribute.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Attribute.Count; i++)
			{
				if (!this.Attribute[i].Equals(registerServerRequest.Attribute[i]))
				{
					return false;
				}
			}
			return this.HasState == registerServerRequest.HasState && (!this.HasState || this.State.Equals(registerServerRequest.State)) && this.ProgramId.Equals(registerServerRequest.ProgramId);
		}

		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		public static RegisterServerRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<RegisterServerRequest>(bs, 0, -1);
		}

		private List<bnet.protocol.attribute.Attribute> _Attribute = new List<bnet.protocol.attribute.Attribute>();

		public bool HasState;

		private ServerState _State;
	}
}
