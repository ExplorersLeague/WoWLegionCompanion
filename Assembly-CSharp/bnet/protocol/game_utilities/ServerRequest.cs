using System;
using System.Collections.Generic;
using System.IO;
using bnet.protocol.attribute;

namespace bnet.protocol.game_utilities
{
	public class ServerRequest : IProtoBuf
	{
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

		public uint Program { get; set; }

		public void SetProgram(uint val)
		{
			this.Program = val;
		}

		public ProcessId Host
		{
			get
			{
				return this._Host;
			}
			set
			{
				this._Host = value;
				this.HasHost = (value != null);
			}
		}

		public void SetHost(ProcessId val)
		{
			this.Host = val;
		}

		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			foreach (bnet.protocol.attribute.Attribute attribute in this.Attribute)
			{
				num ^= attribute.GetHashCode();
			}
			num ^= this.Program.GetHashCode();
			if (this.HasHost)
			{
				num ^= this.Host.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			ServerRequest serverRequest = obj as ServerRequest;
			if (serverRequest == null)
			{
				return false;
			}
			if (this.Attribute.Count != serverRequest.Attribute.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Attribute.Count; i++)
			{
				if (!this.Attribute[i].Equals(serverRequest.Attribute[i]))
				{
					return false;
				}
			}
			return this.Program.Equals(serverRequest.Program) && this.HasHost == serverRequest.HasHost && (!this.HasHost || this.Host.Equals(serverRequest.Host));
		}

		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		public static ServerRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<ServerRequest>(bs, 0, -1);
		}

		public void Deserialize(Stream stream)
		{
			ServerRequest.Deserialize(stream, this);
		}

		public static ServerRequest Deserialize(Stream stream, ServerRequest instance)
		{
			return ServerRequest.Deserialize(stream, instance, -1L);
		}

		public static ServerRequest DeserializeLengthDelimited(Stream stream)
		{
			ServerRequest serverRequest = new ServerRequest();
			ServerRequest.DeserializeLengthDelimited(stream, serverRequest);
			return serverRequest;
		}

		public static ServerRequest DeserializeLengthDelimited(Stream stream, ServerRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return ServerRequest.Deserialize(stream, instance, num);
		}

		public static ServerRequest Deserialize(Stream stream, ServerRequest instance, long limit)
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
						else if (instance.Host == null)
						{
							instance.Host = ProcessId.DeserializeLengthDelimited(stream);
						}
						else
						{
							ProcessId.DeserializeLengthDelimited(stream, instance.Host);
						}
					}
					else
					{
						instance.Program = binaryReader.ReadUInt32();
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
			ServerRequest.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, ServerRequest instance)
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
			stream.WriteByte(21);
			binaryWriter.Write(instance.Program);
			if (instance.HasHost)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteUInt32(stream, instance.Host.GetSerializedSize());
				ProcessId.Serialize(stream, instance.Host);
			}
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
			num += 4u;
			if (this.HasHost)
			{
				num += 1u;
				uint serializedSize2 = this.Host.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			num += 1u;
			return num;
		}

		private List<bnet.protocol.attribute.Attribute> _Attribute = new List<bnet.protocol.attribute.Attribute>();

		public bool HasHost;

		private ProcessId _Host;
	}
}
