using System;
using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.connection
{
	public class BindRequest : IProtoBuf
	{
		public List<uint> ImportedServiceHash
		{
			get
			{
				return this._ImportedServiceHash;
			}
			set
			{
				this._ImportedServiceHash = value;
			}
		}

		public List<uint> ImportedServiceHashList
		{
			get
			{
				return this._ImportedServiceHash;
			}
		}

		public int ImportedServiceHashCount
		{
			get
			{
				return this._ImportedServiceHash.Count;
			}
		}

		public void AddImportedServiceHash(uint val)
		{
			this._ImportedServiceHash.Add(val);
		}

		public void ClearImportedServiceHash()
		{
			this._ImportedServiceHash.Clear();
		}

		public void SetImportedServiceHash(List<uint> val)
		{
			this.ImportedServiceHash = val;
		}

		public List<BoundService> ExportedService
		{
			get
			{
				return this._ExportedService;
			}
			set
			{
				this._ExportedService = value;
			}
		}

		public List<BoundService> ExportedServiceList
		{
			get
			{
				return this._ExportedService;
			}
		}

		public int ExportedServiceCount
		{
			get
			{
				return this._ExportedService.Count;
			}
		}

		public void AddExportedService(BoundService val)
		{
			this._ExportedService.Add(val);
		}

		public void ClearExportedService()
		{
			this._ExportedService.Clear();
		}

		public void SetExportedService(List<BoundService> val)
		{
			this.ExportedService = val;
		}

		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			foreach (uint num2 in this.ImportedServiceHash)
			{
				num ^= num2.GetHashCode();
			}
			foreach (BoundService boundService in this.ExportedService)
			{
				num ^= boundService.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			BindRequest bindRequest = obj as BindRequest;
			if (bindRequest == null)
			{
				return false;
			}
			if (this.ImportedServiceHash.Count != bindRequest.ImportedServiceHash.Count)
			{
				return false;
			}
			for (int i = 0; i < this.ImportedServiceHash.Count; i++)
			{
				if (!this.ImportedServiceHash[i].Equals(bindRequest.ImportedServiceHash[i]))
				{
					return false;
				}
			}
			if (this.ExportedService.Count != bindRequest.ExportedService.Count)
			{
				return false;
			}
			for (int j = 0; j < this.ExportedService.Count; j++)
			{
				if (!this.ExportedService[j].Equals(bindRequest.ExportedService[j]))
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

		public static BindRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<BindRequest>(bs, 0, -1);
		}

		public void Deserialize(Stream stream)
		{
			BindRequest.Deserialize(stream, this);
		}

		public static BindRequest Deserialize(Stream stream, BindRequest instance)
		{
			return BindRequest.Deserialize(stream, instance, -1L);
		}

		public static BindRequest DeserializeLengthDelimited(Stream stream)
		{
			BindRequest bindRequest = new BindRequest();
			BindRequest.DeserializeLengthDelimited(stream, bindRequest);
			return bindRequest;
		}

		public static BindRequest DeserializeLengthDelimited(Stream stream, BindRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return BindRequest.Deserialize(stream, instance, num);
		}

		public static BindRequest Deserialize(Stream stream, BindRequest instance, long limit)
		{
			BinaryReader binaryReader = new BinaryReader(stream);
			if (instance.ImportedServiceHash == null)
			{
				instance.ImportedServiceHash = new List<uint>();
			}
			if (instance.ExportedService == null)
			{
				instance.ExportedService = new List<BoundService>();
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
						instance.ExportedService.Add(BoundService.DeserializeLengthDelimited(stream));
					}
				}
				else
				{
					long num2 = (long)((ulong)ProtocolParser.ReadUInt32(stream));
					num2 += stream.Position;
					while (stream.Position < num2)
					{
						instance.ImportedServiceHash.Add(binaryReader.ReadUInt32());
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
			BindRequest.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, BindRequest instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.ImportedServiceHash.Count > 0)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, (uint)(4 * instance.ImportedServiceHash.Count));
				foreach (uint value in instance.ImportedServiceHash)
				{
					binaryWriter.Write(value);
				}
			}
			if (instance.ExportedService.Count > 0)
			{
				foreach (BoundService boundService in instance.ExportedService)
				{
					stream.WriteByte(18);
					ProtocolParser.WriteUInt32(stream, boundService.GetSerializedSize());
					BoundService.Serialize(stream, boundService);
				}
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (this.ImportedServiceHash.Count > 0)
			{
				num += 1u;
				uint num2 = num;
				foreach (uint num3 in this.ImportedServiceHash)
				{
					num += 4u;
				}
				num += ProtocolParser.SizeOfUInt32(num - num2);
			}
			if (this.ExportedService.Count > 0)
			{
				foreach (BoundService boundService in this.ExportedService)
				{
					num += 1u;
					uint serializedSize = boundService.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			return num;
		}

		private List<uint> _ImportedServiceHash = new List<uint>();

		private List<BoundService> _ExportedService = new List<BoundService>();
	}
}
