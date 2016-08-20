using System;
using System.IO;

namespace bnet.protocol.authentication
{
	public class ModuleLoadRequest : IProtoBuf
	{
		public void Deserialize(Stream stream)
		{
			ModuleLoadRequest.Deserialize(stream, this);
		}

		public static ModuleLoadRequest Deserialize(Stream stream, ModuleLoadRequest instance)
		{
			return ModuleLoadRequest.Deserialize(stream, instance, -1L);
		}

		public static ModuleLoadRequest DeserializeLengthDelimited(Stream stream)
		{
			ModuleLoadRequest moduleLoadRequest = new ModuleLoadRequest();
			ModuleLoadRequest.DeserializeLengthDelimited(stream, moduleLoadRequest);
			return moduleLoadRequest;
		}

		public static ModuleLoadRequest DeserializeLengthDelimited(Stream stream, ModuleLoadRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return ModuleLoadRequest.Deserialize(stream, instance, num);
		}

		public static ModuleLoadRequest Deserialize(Stream stream, ModuleLoadRequest instance, long limit)
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
				else
				{
					int num2 = num;
					if (num2 != 10)
					{
						if (num2 != 18)
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
					else if (instance.ModuleHandle == null)
					{
						instance.ModuleHandle = ContentHandle.DeserializeLengthDelimited(stream);
					}
					else
					{
						ContentHandle.DeserializeLengthDelimited(stream, instance.ModuleHandle);
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
			ModuleLoadRequest.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, ModuleLoadRequest instance)
		{
			if (instance.ModuleHandle == null)
			{
				throw new ArgumentNullException("ModuleHandle", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteUInt32(stream, instance.ModuleHandle.GetSerializedSize());
			ContentHandle.Serialize(stream, instance.ModuleHandle);
			if (instance.HasMessage)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteBytes(stream, instance.Message);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			uint serializedSize = this.ModuleHandle.GetSerializedSize();
			num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			if (this.HasMessage)
			{
				num += 1u;
				num += ProtocolParser.SizeOfUInt32(this.Message.Length) + (uint)this.Message.Length;
			}
			return num + 1u;
		}

		public ContentHandle ModuleHandle { get; set; }

		public void SetModuleHandle(ContentHandle val)
		{
			this.ModuleHandle = val;
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
			num ^= this.ModuleHandle.GetHashCode();
			if (this.HasMessage)
			{
				num ^= this.Message.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			ModuleLoadRequest moduleLoadRequest = obj as ModuleLoadRequest;
			return moduleLoadRequest != null && this.ModuleHandle.Equals(moduleLoadRequest.ModuleHandle) && this.HasMessage == moduleLoadRequest.HasMessage && (!this.HasMessage || this.Message.Equals(moduleLoadRequest.Message));
		}

		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		public static ModuleLoadRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<ModuleLoadRequest>(bs, 0, -1);
		}

		public bool HasMessage;

		private byte[] _Message;
	}
}
