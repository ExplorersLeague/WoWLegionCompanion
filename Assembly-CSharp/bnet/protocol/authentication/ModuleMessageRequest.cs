using System;
using System.IO;

namespace bnet.protocol.authentication
{
	public class ModuleMessageRequest : IProtoBuf
	{
		public void Deserialize(Stream stream)
		{
			ModuleMessageRequest.Deserialize(stream, this);
		}

		public static ModuleMessageRequest Deserialize(Stream stream, ModuleMessageRequest instance)
		{
			return ModuleMessageRequest.Deserialize(stream, instance, -1L);
		}

		public static ModuleMessageRequest DeserializeLengthDelimited(Stream stream)
		{
			ModuleMessageRequest moduleMessageRequest = new ModuleMessageRequest();
			ModuleMessageRequest.DeserializeLengthDelimited(stream, moduleMessageRequest);
			return moduleMessageRequest;
		}

		public static ModuleMessageRequest DeserializeLengthDelimited(Stream stream, ModuleMessageRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return ModuleMessageRequest.Deserialize(stream, instance, num);
		}

		public static ModuleMessageRequest Deserialize(Stream stream, ModuleMessageRequest instance, long limit)
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
					if (num != 18)
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
						instance.Message = ProtocolParser.ReadBytes(stream);
					}
				}
				else
				{
					instance.ModuleId = (int)ProtocolParser.ReadUInt64(stream);
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
			ModuleMessageRequest.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, ModuleMessageRequest instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.ModuleId));
			if (instance.HasMessage)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteBytes(stream, instance.Message);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			num += ProtocolParser.SizeOfUInt64((ulong)((long)this.ModuleId));
			if (this.HasMessage)
			{
				num += 1u;
				num += ProtocolParser.SizeOfUInt32(this.Message.Length) + (uint)this.Message.Length;
			}
			return num + 1u;
		}

		public int ModuleId { get; set; }

		public void SetModuleId(int val)
		{
			this.ModuleId = val;
		}

		public byte[] Message
		{
			get
			{
				return this._Message;
			}
			set
			{
				this._Message = value;
				this.HasMessage = (value != null);
			}
		}

		public void SetMessage(byte[] val)
		{
			this.Message = val;
		}

		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.ModuleId.GetHashCode();
			if (this.HasMessage)
			{
				num ^= this.Message.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			ModuleMessageRequest moduleMessageRequest = obj as ModuleMessageRequest;
			return moduleMessageRequest != null && this.ModuleId.Equals(moduleMessageRequest.ModuleId) && this.HasMessage == moduleMessageRequest.HasMessage && (!this.HasMessage || this.Message.Equals(moduleMessageRequest.Message));
		}

		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		public static ModuleMessageRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<ModuleMessageRequest>(bs, 0, -1);
		}

		public bool HasMessage;

		private byte[] _Message;
	}
}
