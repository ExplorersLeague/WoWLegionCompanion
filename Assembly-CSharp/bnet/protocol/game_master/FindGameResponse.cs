using System;
using System.IO;

namespace bnet.protocol.game_master
{
	public class FindGameResponse : IProtoBuf
	{
		public ulong RequestId
		{
			get
			{
				return this._RequestId;
			}
			set
			{
				this._RequestId = value;
				this.HasRequestId = true;
			}
		}

		public void SetRequestId(ulong val)
		{
			this.RequestId = val;
		}

		public ulong FactoryId
		{
			get
			{
				return this._FactoryId;
			}
			set
			{
				this._FactoryId = value;
				this.HasFactoryId = true;
			}
		}

		public void SetFactoryId(ulong val)
		{
			this.FactoryId = val;
		}

		public bool Queued
		{
			get
			{
				return this._Queued;
			}
			set
			{
				this._Queued = value;
				this.HasQueued = true;
			}
		}

		public void SetQueued(bool val)
		{
			this.Queued = val;
		}

		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasRequestId)
			{
				num ^= this.RequestId.GetHashCode();
			}
			if (this.HasFactoryId)
			{
				num ^= this.FactoryId.GetHashCode();
			}
			if (this.HasQueued)
			{
				num ^= this.Queued.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			FindGameResponse findGameResponse = obj as FindGameResponse;
			return findGameResponse != null && this.HasRequestId == findGameResponse.HasRequestId && (!this.HasRequestId || this.RequestId.Equals(findGameResponse.RequestId)) && this.HasFactoryId == findGameResponse.HasFactoryId && (!this.HasFactoryId || this.FactoryId.Equals(findGameResponse.FactoryId)) && this.HasQueued == findGameResponse.HasQueued && (!this.HasQueued || this.Queued.Equals(findGameResponse.Queued));
		}

		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		public static FindGameResponse ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<FindGameResponse>(bs, 0, -1);
		}

		public void Deserialize(Stream stream)
		{
			FindGameResponse.Deserialize(stream, this);
		}

		public static FindGameResponse Deserialize(Stream stream, FindGameResponse instance)
		{
			return FindGameResponse.Deserialize(stream, instance, -1L);
		}

		public static FindGameResponse DeserializeLengthDelimited(Stream stream)
		{
			FindGameResponse findGameResponse = new FindGameResponse();
			FindGameResponse.DeserializeLengthDelimited(stream, findGameResponse);
			return findGameResponse;
		}

		public static FindGameResponse DeserializeLengthDelimited(Stream stream, FindGameResponse instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return FindGameResponse.Deserialize(stream, instance, num);
		}

		public static FindGameResponse Deserialize(Stream stream, FindGameResponse instance, long limit)
		{
			BinaryReader binaryReader = new BinaryReader(stream);
			instance.Queued = false;
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
				else if (num != 9)
				{
					if (num != 17)
					{
						if (num != 24)
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
							instance.Queued = ProtocolParser.ReadBool(stream);
						}
					}
					else
					{
						instance.FactoryId = binaryReader.ReadUInt64();
					}
				}
				else
				{
					instance.RequestId = binaryReader.ReadUInt64();
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
			FindGameResponse.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, FindGameResponse instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.HasRequestId)
			{
				stream.WriteByte(9);
				binaryWriter.Write(instance.RequestId);
			}
			if (instance.HasFactoryId)
			{
				stream.WriteByte(17);
				binaryWriter.Write(instance.FactoryId);
			}
			if (instance.HasQueued)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteBool(stream, instance.Queued);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (this.HasRequestId)
			{
				num += 1u;
				num += 8u;
			}
			if (this.HasFactoryId)
			{
				num += 1u;
				num += 8u;
			}
			if (this.HasQueued)
			{
				num += 1u;
				num += 1u;
			}
			return num;
		}

		public bool HasRequestId;

		private ulong _RequestId;

		public bool HasFactoryId;

		private ulong _FactoryId;

		public bool HasQueued;

		private bool _Queued;
	}
}
