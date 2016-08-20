using System;
using System.IO;

namespace bnet.protocol.authentication
{
	public class ModuleNotification : IProtoBuf
	{
		public void Deserialize(Stream stream)
		{
			ModuleNotification.Deserialize(stream, this);
		}

		public static ModuleNotification Deserialize(Stream stream, ModuleNotification instance)
		{
			return ModuleNotification.Deserialize(stream, instance, -1L);
		}

		public static ModuleNotification DeserializeLengthDelimited(Stream stream)
		{
			ModuleNotification moduleNotification = new ModuleNotification();
			ModuleNotification.DeserializeLengthDelimited(stream, moduleNotification);
			return moduleNotification;
		}

		public static ModuleNotification DeserializeLengthDelimited(Stream stream, ModuleNotification instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return ModuleNotification.Deserialize(stream, instance, num);
		}

		public static ModuleNotification Deserialize(Stream stream, ModuleNotification instance, long limit)
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
					if (num2 != 16)
					{
						if (num2 != 24)
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
							instance.Result = ProtocolParser.ReadUInt32(stream);
						}
					}
					else
					{
						instance.ModuleId = (int)ProtocolParser.ReadUInt64(stream);
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
			ModuleNotification.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, ModuleNotification instance)
		{
			if (instance.HasModuleId)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.ModuleId));
			}
			if (instance.HasResult)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt32(stream, instance.Result);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (this.HasModuleId)
			{
				num += 1u;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.ModuleId));
			}
			if (this.HasResult)
			{
				num += 1u;
				num += ProtocolParser.SizeOfUInt32(this.Result);
			}
			return num;
		}

		public int ModuleId
		{
			get
			{
				return this._ModuleId;
			}
			set
			{
				this._ModuleId = value;
				this.HasModuleId = true;
			}
		}

		public void SetModuleId(int val)
		{
			this.ModuleId = val;
		}

		public uint Result
		{
			get
			{
				return this._Result;
			}
			set
			{
				this._Result = value;
				this.HasResult = true;
			}
		}

		public void SetResult(uint val)
		{
			this.Result = val;
		}

		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasModuleId)
			{
				num ^= this.ModuleId.GetHashCode();
			}
			if (this.HasResult)
			{
				num ^= this.Result.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			ModuleNotification moduleNotification = obj as ModuleNotification;
			return moduleNotification != null && this.HasModuleId == moduleNotification.HasModuleId && (!this.HasModuleId || this.ModuleId.Equals(moduleNotification.ModuleId)) && this.HasResult == moduleNotification.HasResult && (!this.HasResult || this.Result.Equals(moduleNotification.Result));
		}

		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		public static ModuleNotification ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<ModuleNotification>(bs, 0, -1);
		}

		public bool HasModuleId;

		private int _ModuleId;

		public bool HasResult;

		private uint _Result;
	}
}
