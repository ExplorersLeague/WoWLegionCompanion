using System;
using System.Collections.Generic;
using System.IO;

namespace bnet.protocol
{
	public class Path : IProtoBuf
	{
		public List<uint> Ordinal
		{
			get
			{
				return this._Ordinal;
			}
			set
			{
				this._Ordinal = value;
			}
		}

		public List<uint> OrdinalList
		{
			get
			{
				return this._Ordinal;
			}
		}

		public int OrdinalCount
		{
			get
			{
				return this._Ordinal.Count;
			}
		}

		public void AddOrdinal(uint val)
		{
			this._Ordinal.Add(val);
		}

		public void ClearOrdinal()
		{
			this._Ordinal.Clear();
		}

		public void SetOrdinal(List<uint> val)
		{
			this.Ordinal = val;
		}

		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			foreach (uint num2 in this.Ordinal)
			{
				num ^= num2.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			Path path = obj as Path;
			if (path == null)
			{
				return false;
			}
			if (this.Ordinal.Count != path.Ordinal.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Ordinal.Count; i++)
			{
				if (!this.Ordinal[i].Equals(path.Ordinal[i]))
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

		public static Path ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<Path>(bs, 0, -1);
		}

		public void Deserialize(Stream stream)
		{
			Path.Deserialize(stream, this);
		}

		public static Path Deserialize(Stream stream, Path instance)
		{
			return Path.Deserialize(stream, instance, -1L);
		}

		public static Path DeserializeLengthDelimited(Stream stream)
		{
			Path path = new Path();
			Path.DeserializeLengthDelimited(stream, path);
			return path;
		}

		public static Path DeserializeLengthDelimited(Stream stream, Path instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return Path.Deserialize(stream, instance, num);
		}

		public static Path Deserialize(Stream stream, Path instance, long limit)
		{
			if (instance.Ordinal == null)
			{
				instance.Ordinal = new List<uint>();
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
						instance.Ordinal.Add(ProtocolParser.ReadUInt32(stream));
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
			Path.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, Path instance)
		{
			if (instance.Ordinal.Count > 0)
			{
				stream.WriteByte(10);
				uint num = 0u;
				foreach (uint val in instance.Ordinal)
				{
					num += ProtocolParser.SizeOfUInt32(val);
				}
				ProtocolParser.WriteUInt32(stream, num);
				foreach (uint val2 in instance.Ordinal)
				{
					ProtocolParser.WriteUInt32(stream, val2);
				}
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (this.Ordinal.Count > 0)
			{
				num += 1u;
				uint num2 = num;
				foreach (uint val in this.Ordinal)
				{
					num += ProtocolParser.SizeOfUInt32(val);
				}
				num += ProtocolParser.SizeOfUInt32(num - num2);
			}
			return num;
		}

		private List<uint> _Ordinal = new List<uint>();
	}
}
