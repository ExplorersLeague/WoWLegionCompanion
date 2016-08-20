using System;
using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.game_master
{
	public class JoinGameResponse : IProtoBuf
	{
		public void Deserialize(Stream stream)
		{
			JoinGameResponse.Deserialize(stream, this);
		}

		public static JoinGameResponse Deserialize(Stream stream, JoinGameResponse instance)
		{
			return JoinGameResponse.Deserialize(stream, instance, -1L);
		}

		public static JoinGameResponse DeserializeLengthDelimited(Stream stream)
		{
			JoinGameResponse joinGameResponse = new JoinGameResponse();
			JoinGameResponse.DeserializeLengthDelimited(stream, joinGameResponse);
			return joinGameResponse;
		}

		public static JoinGameResponse DeserializeLengthDelimited(Stream stream, JoinGameResponse instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return JoinGameResponse.Deserialize(stream, instance, num);
		}

		public static JoinGameResponse Deserialize(Stream stream, JoinGameResponse instance, long limit)
		{
			BinaryReader binaryReader = new BinaryReader(stream);
			instance.Queued = false;
			if (instance.ConnectInfo == null)
			{
				instance.ConnectInfo = new List<ConnectInfo>();
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
					if (num2 != 9)
					{
						if (num2 != 16)
						{
							if (num2 != 26)
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
								instance.ConnectInfo.Add(bnet.protocol.game_master.ConnectInfo.DeserializeLengthDelimited(stream));
							}
						}
						else
						{
							instance.Queued = ProtocolParser.ReadBool(stream);
						}
					}
					else
					{
						instance.RequestId = binaryReader.ReadUInt64();
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
			JoinGameResponse.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, JoinGameResponse instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.HasRequestId)
			{
				stream.WriteByte(9);
				binaryWriter.Write(instance.RequestId);
			}
			if (instance.HasQueued)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteBool(stream, instance.Queued);
			}
			if (instance.ConnectInfo.Count > 0)
			{
				foreach (ConnectInfo connectInfo in instance.ConnectInfo)
				{
					stream.WriteByte(26);
					ProtocolParser.WriteUInt32(stream, connectInfo.GetSerializedSize());
					bnet.protocol.game_master.ConnectInfo.Serialize(stream, connectInfo);
				}
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
			if (this.HasQueued)
			{
				num += 1u;
				num += 1u;
			}
			if (this.ConnectInfo.Count > 0)
			{
				foreach (ConnectInfo connectInfo in this.ConnectInfo)
				{
					num += 1u;
					uint serializedSize = connectInfo.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			return num;
		}

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

		public List<ConnectInfo> ConnectInfo
		{
			get
			{
				return this._ConnectInfo;
			}
			set
			{
				this._ConnectInfo = value;
			}
		}

		public List<ConnectInfo> ConnectInfoList
		{
			get
			{
				return this._ConnectInfo;
			}
		}

		public int ConnectInfoCount
		{
			get
			{
				return this._ConnectInfo.Count;
			}
		}

		public void AddConnectInfo(ConnectInfo val)
		{
			this._ConnectInfo.Add(val);
		}

		public void ClearConnectInfo()
		{
			this._ConnectInfo.Clear();
		}

		public void SetConnectInfo(List<ConnectInfo> val)
		{
			this.ConnectInfo = val;
		}

		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasRequestId)
			{
				num ^= this.RequestId.GetHashCode();
			}
			if (this.HasQueued)
			{
				num ^= this.Queued.GetHashCode();
			}
			foreach (ConnectInfo connectInfo in this.ConnectInfo)
			{
				num ^= connectInfo.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			JoinGameResponse joinGameResponse = obj as JoinGameResponse;
			if (joinGameResponse == null)
			{
				return false;
			}
			if (this.HasRequestId != joinGameResponse.HasRequestId || (this.HasRequestId && !this.RequestId.Equals(joinGameResponse.RequestId)))
			{
				return false;
			}
			if (this.HasQueued != joinGameResponse.HasQueued || (this.HasQueued && !this.Queued.Equals(joinGameResponse.Queued)))
			{
				return false;
			}
			if (this.ConnectInfo.Count != joinGameResponse.ConnectInfo.Count)
			{
				return false;
			}
			for (int i = 0; i < this.ConnectInfo.Count; i++)
			{
				if (!this.ConnectInfo[i].Equals(joinGameResponse.ConnectInfo[i]))
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

		public static JoinGameResponse ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<JoinGameResponse>(bs, 0, -1);
		}

		public bool HasRequestId;

		private ulong _RequestId;

		public bool HasQueued;

		private bool _Queued;

		private List<ConnectInfo> _ConnectInfo = new List<ConnectInfo>();
	}
}
