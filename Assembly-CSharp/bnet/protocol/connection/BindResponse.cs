using System;
using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.connection
{
	public class BindResponse : IProtoBuf
	{
		public void Deserialize(Stream stream)
		{
			BindResponse.Deserialize(stream, this);
		}

		public static BindResponse Deserialize(Stream stream, BindResponse instance)
		{
			return BindResponse.Deserialize(stream, instance, -1L);
		}

		public static BindResponse DeserializeLengthDelimited(Stream stream)
		{
			BindResponse bindResponse = new BindResponse();
			BindResponse.DeserializeLengthDelimited(stream, bindResponse);
			return bindResponse;
		}

		public static BindResponse DeserializeLengthDelimited(Stream stream, BindResponse instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return BindResponse.Deserialize(stream, instance, num);
		}

		public static BindResponse Deserialize(Stream stream, BindResponse instance, long limit)
		{
			if (instance.ImportedServiceId == null)
			{
				instance.ImportedServiceId = new List<uint>();
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
					long num2 = (long)((ulong)ProtocolParser.ReadUInt32(stream));
					num2 += stream.Position;
					while (stream.Position < num2)
					{
						instance.ImportedServiceId.Add(ProtocolParser.ReadUInt32(stream));
					}
					if (stream.Position != num2)
					{
						throw new ProtocolBufferException("Read too many bytes in packed data");
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
			BindResponse.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, BindResponse instance)
		{
			if (instance.ImportedServiceId.Count > 0)
			{
				stream.WriteByte(10);
				uint num = 0u;
				foreach (uint val in instance.ImportedServiceId)
				{
					num += ProtocolParser.SizeOfUInt32(val);
				}
				ProtocolParser.WriteUInt32(stream, num);
				foreach (uint val2 in instance.ImportedServiceId)
				{
					ProtocolParser.WriteUInt32(stream, val2);
				}
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (this.ImportedServiceId.Count > 0)
			{
				num += 1u;
				uint num2 = num;
				foreach (uint val in this.ImportedServiceId)
				{
					num += ProtocolParser.SizeOfUInt32(val);
				}
				num += ProtocolParser.SizeOfUInt32(num - num2);
			}
			return num;
		}

		public List<uint> ImportedServiceId
		{
			get
			{
				return this._ImportedServiceId;
			}
			set
			{
				this._ImportedServiceId = value;
			}
		}

		public List<uint> ImportedServiceIdList
		{
			get
			{
				return this._ImportedServiceId;
			}
		}

		public int ImportedServiceIdCount
		{
			get
			{
				return this._ImportedServiceId.Count;
			}
		}

		public void AddImportedServiceId(uint val)
		{
			this._ImportedServiceId.Add(val);
		}

		public void ClearImportedServiceId()
		{
			this._ImportedServiceId.Clear();
		}

		public void SetImportedServiceId(List<uint> val)
		{
			this.ImportedServiceId = val;
		}

		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			foreach (uint num2 in this.ImportedServiceId)
			{
				num ^= num2.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			BindResponse bindResponse = obj as BindResponse;
			if (bindResponse == null)
			{
				return false;
			}
			if (this.ImportedServiceId.Count != bindResponse.ImportedServiceId.Count)
			{
				return false;
			}
			for (int i = 0; i < this.ImportedServiceId.Count; i++)
			{
				if (!this.ImportedServiceId[i].Equals(bindResponse.ImportedServiceId[i]))
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

		public static BindResponse ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<BindResponse>(bs, 0, -1);
		}

		private List<uint> _ImportedServiceId = new List<uint>();
	}
}
