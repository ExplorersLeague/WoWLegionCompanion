using System;
using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.channel_invitation
{
	public class IncrementChannelCountResponse : IProtoBuf
	{
		public void Deserialize(Stream stream)
		{
			IncrementChannelCountResponse.Deserialize(stream, this);
		}

		public static IncrementChannelCountResponse Deserialize(Stream stream, IncrementChannelCountResponse instance)
		{
			return IncrementChannelCountResponse.Deserialize(stream, instance, -1L);
		}

		public static IncrementChannelCountResponse DeserializeLengthDelimited(Stream stream)
		{
			IncrementChannelCountResponse incrementChannelCountResponse = new IncrementChannelCountResponse();
			IncrementChannelCountResponse.DeserializeLengthDelimited(stream, incrementChannelCountResponse);
			return incrementChannelCountResponse;
		}

		public static IncrementChannelCountResponse DeserializeLengthDelimited(Stream stream, IncrementChannelCountResponse instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return IncrementChannelCountResponse.Deserialize(stream, instance, num);
		}

		public static IncrementChannelCountResponse Deserialize(Stream stream, IncrementChannelCountResponse instance, long limit)
		{
			if (instance.ReservationTokens == null)
			{
				instance.ReservationTokens = new List<ulong>();
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
					if (num2 != 8)
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
						instance.ReservationTokens.Add(ProtocolParser.ReadUInt64(stream));
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
			IncrementChannelCountResponse.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, IncrementChannelCountResponse instance)
		{
			if (instance.ReservationTokens.Count > 0)
			{
				foreach (ulong val in instance.ReservationTokens)
				{
					stream.WriteByte(8);
					ProtocolParser.WriteUInt64(stream, val);
				}
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (this.ReservationTokens.Count > 0)
			{
				foreach (ulong val in this.ReservationTokens)
				{
					num += 1u;
					num += ProtocolParser.SizeOfUInt64(val);
				}
			}
			return num;
		}

		public List<ulong> ReservationTokens
		{
			get
			{
				return this._ReservationTokens;
			}
			set
			{
				this._ReservationTokens = value;
			}
		}

		public List<ulong> ReservationTokensList
		{
			get
			{
				return this._ReservationTokens;
			}
		}

		public int ReservationTokensCount
		{
			get
			{
				return this._ReservationTokens.Count;
			}
		}

		public void AddReservationTokens(ulong val)
		{
			this._ReservationTokens.Add(val);
		}

		public void ClearReservationTokens()
		{
			this._ReservationTokens.Clear();
		}

		public void SetReservationTokens(List<ulong> val)
		{
			this.ReservationTokens = val;
		}

		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			foreach (ulong num2 in this.ReservationTokens)
			{
				num ^= num2.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			IncrementChannelCountResponse incrementChannelCountResponse = obj as IncrementChannelCountResponse;
			if (incrementChannelCountResponse == null)
			{
				return false;
			}
			if (this.ReservationTokens.Count != incrementChannelCountResponse.ReservationTokens.Count)
			{
				return false;
			}
			for (int i = 0; i < this.ReservationTokens.Count; i++)
			{
				if (!this.ReservationTokens[i].Equals(incrementChannelCountResponse.ReservationTokens[i]))
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

		public static IncrementChannelCountResponse ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<IncrementChannelCountResponse>(bs, 0, -1);
		}

		private List<ulong> _ReservationTokens = new List<ulong>();
	}
}
