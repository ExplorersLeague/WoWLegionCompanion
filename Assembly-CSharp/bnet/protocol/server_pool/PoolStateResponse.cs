using System;
using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.server_pool
{
	public class PoolStateResponse : IProtoBuf
	{
		public void Deserialize(Stream stream)
		{
			PoolStateResponse.Deserialize(stream, this);
		}

		public static PoolStateResponse Deserialize(Stream stream, PoolStateResponse instance)
		{
			return PoolStateResponse.Deserialize(stream, instance, -1L);
		}

		public static PoolStateResponse DeserializeLengthDelimited(Stream stream)
		{
			PoolStateResponse poolStateResponse = new PoolStateResponse();
			PoolStateResponse.DeserializeLengthDelimited(stream, poolStateResponse);
			return poolStateResponse;
		}

		public static PoolStateResponse DeserializeLengthDelimited(Stream stream, PoolStateResponse instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return PoolStateResponse.Deserialize(stream, instance, num);
		}

		public static PoolStateResponse Deserialize(Stream stream, PoolStateResponse instance, long limit)
		{
			if (instance.Info == null)
			{
				instance.Info = new List<ServerInfo>();
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
				else
				{
					int num2 = num;
					if (num2 != 10)
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
						instance.Info.Add(ServerInfo.DeserializeLengthDelimited(stream));
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
			PoolStateResponse.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, PoolStateResponse instance)
		{
			if (instance.Info.Count > 0)
			{
				foreach (ServerInfo serverInfo in instance.Info)
				{
					stream.WriteByte(10);
					ProtocolParser.WriteUInt32(stream, serverInfo.GetSerializedSize());
					ServerInfo.Serialize(stream, serverInfo);
				}
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (this.Info.Count > 0)
			{
				foreach (ServerInfo serverInfo in this.Info)
				{
					num += 1u;
					uint serializedSize = serverInfo.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			return num;
		}

		public List<ServerInfo> Info
		{
			get
			{
				return this._Info;
			}
			set
			{
				this._Info = value;
			}
		}

		public List<ServerInfo> InfoList
		{
			get
			{
				return this._Info;
			}
		}

		public int InfoCount
		{
			get
			{
				return this._Info.Count;
			}
		}

		public void AddInfo(ServerInfo val)
		{
			this._Info.Add(val);
		}

		public void ClearInfo()
		{
			this._Info.Clear();
		}

		public void SetInfo(List<ServerInfo> val)
		{
			this.Info = val;
		}

		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			foreach (ServerInfo serverInfo in this.Info)
			{
				num ^= serverInfo.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			PoolStateResponse poolStateResponse = obj as PoolStateResponse;
			if (poolStateResponse == null)
			{
				return false;
			}
			if (this.Info.Count != poolStateResponse.Info.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Info.Count; i++)
			{
				if (!this.Info[i].Equals(poolStateResponse.Info[i]))
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

		public static PoolStateResponse ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<PoolStateResponse>(bs, 0, -1);
		}

		private List<ServerInfo> _Info = new List<ServerInfo>();
	}
}
