using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace bnet.protocol.account
{
	public class ParentalControlInfo : IProtoBuf
	{
		public string Timezone
		{
			get
			{
				return this._Timezone;
			}
			set
			{
				this._Timezone = value;
				this.HasTimezone = (value != null);
			}
		}

		public void SetTimezone(string val)
		{
			this.Timezone = val;
		}

		public uint MinutesPerDay
		{
			get
			{
				return this._MinutesPerDay;
			}
			set
			{
				this._MinutesPerDay = value;
				this.HasMinutesPerDay = true;
			}
		}

		public void SetMinutesPerDay(uint val)
		{
			this.MinutesPerDay = val;
		}

		public uint MinutesPerWeek
		{
			get
			{
				return this._MinutesPerWeek;
			}
			set
			{
				this._MinutesPerWeek = value;
				this.HasMinutesPerWeek = true;
			}
		}

		public void SetMinutesPerWeek(uint val)
		{
			this.MinutesPerWeek = val;
		}

		public bool CanReceiveVoice
		{
			get
			{
				return this._CanReceiveVoice;
			}
			set
			{
				this._CanReceiveVoice = value;
				this.HasCanReceiveVoice = true;
			}
		}

		public void SetCanReceiveVoice(bool val)
		{
			this.CanReceiveVoice = val;
		}

		public bool CanSendVoice
		{
			get
			{
				return this._CanSendVoice;
			}
			set
			{
				this._CanSendVoice = value;
				this.HasCanSendVoice = true;
			}
		}

		public void SetCanSendVoice(bool val)
		{
			this.CanSendVoice = val;
		}

		public List<bool> PlaySchedule
		{
			get
			{
				return this._PlaySchedule;
			}
			set
			{
				this._PlaySchedule = value;
			}
		}

		public List<bool> PlayScheduleList
		{
			get
			{
				return this._PlaySchedule;
			}
		}

		public int PlayScheduleCount
		{
			get
			{
				return this._PlaySchedule.Count;
			}
		}

		public void AddPlaySchedule(bool val)
		{
			this._PlaySchedule.Add(val);
		}

		public void ClearPlaySchedule()
		{
			this._PlaySchedule.Clear();
		}

		public void SetPlaySchedule(List<bool> val)
		{
			this.PlaySchedule = val;
		}

		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasTimezone)
			{
				num ^= this.Timezone.GetHashCode();
			}
			if (this.HasMinutesPerDay)
			{
				num ^= this.MinutesPerDay.GetHashCode();
			}
			if (this.HasMinutesPerWeek)
			{
				num ^= this.MinutesPerWeek.GetHashCode();
			}
			if (this.HasCanReceiveVoice)
			{
				num ^= this.CanReceiveVoice.GetHashCode();
			}
			if (this.HasCanSendVoice)
			{
				num ^= this.CanSendVoice.GetHashCode();
			}
			foreach (bool flag in this.PlaySchedule)
			{
				num ^= flag.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			ParentalControlInfo parentalControlInfo = obj as ParentalControlInfo;
			if (parentalControlInfo == null)
			{
				return false;
			}
			if (this.HasTimezone != parentalControlInfo.HasTimezone || (this.HasTimezone && !this.Timezone.Equals(parentalControlInfo.Timezone)))
			{
				return false;
			}
			if (this.HasMinutesPerDay != parentalControlInfo.HasMinutesPerDay || (this.HasMinutesPerDay && !this.MinutesPerDay.Equals(parentalControlInfo.MinutesPerDay)))
			{
				return false;
			}
			if (this.HasMinutesPerWeek != parentalControlInfo.HasMinutesPerWeek || (this.HasMinutesPerWeek && !this.MinutesPerWeek.Equals(parentalControlInfo.MinutesPerWeek)))
			{
				return false;
			}
			if (this.HasCanReceiveVoice != parentalControlInfo.HasCanReceiveVoice || (this.HasCanReceiveVoice && !this.CanReceiveVoice.Equals(parentalControlInfo.CanReceiveVoice)))
			{
				return false;
			}
			if (this.HasCanSendVoice != parentalControlInfo.HasCanSendVoice || (this.HasCanSendVoice && !this.CanSendVoice.Equals(parentalControlInfo.CanSendVoice)))
			{
				return false;
			}
			if (this.PlaySchedule.Count != parentalControlInfo.PlaySchedule.Count)
			{
				return false;
			}
			for (int i = 0; i < this.PlaySchedule.Count; i++)
			{
				if (!this.PlaySchedule[i].Equals(parentalControlInfo.PlaySchedule[i]))
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

		public static ParentalControlInfo ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<ParentalControlInfo>(bs, 0, -1);
		}

		public void Deserialize(Stream stream)
		{
			ParentalControlInfo.Deserialize(stream, this);
		}

		public static ParentalControlInfo Deserialize(Stream stream, ParentalControlInfo instance)
		{
			return ParentalControlInfo.Deserialize(stream, instance, -1L);
		}

		public static ParentalControlInfo DeserializeLengthDelimited(Stream stream)
		{
			ParentalControlInfo parentalControlInfo = new ParentalControlInfo();
			ParentalControlInfo.DeserializeLengthDelimited(stream, parentalControlInfo);
			return parentalControlInfo;
		}

		public static ParentalControlInfo DeserializeLengthDelimited(Stream stream, ParentalControlInfo instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return ParentalControlInfo.Deserialize(stream, instance, num);
		}

		public static ParentalControlInfo Deserialize(Stream stream, ParentalControlInfo instance, long limit)
		{
			if (instance.PlaySchedule == null)
			{
				instance.PlaySchedule = new List<bool>();
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
				else if (num != 26)
				{
					if (num != 32)
					{
						if (num != 40)
						{
							if (num != 48)
							{
								if (num != 56)
								{
									if (num != 64)
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
										instance.PlaySchedule.Add(ProtocolParser.ReadBool(stream));
									}
								}
								else
								{
									instance.CanSendVoice = ProtocolParser.ReadBool(stream);
								}
							}
							else
							{
								instance.CanReceiveVoice = ProtocolParser.ReadBool(stream);
							}
						}
						else
						{
							instance.MinutesPerWeek = ProtocolParser.ReadUInt32(stream);
						}
					}
					else
					{
						instance.MinutesPerDay = ProtocolParser.ReadUInt32(stream);
					}
				}
				else
				{
					instance.Timezone = ProtocolParser.ReadString(stream);
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
			ParentalControlInfo.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, ParentalControlInfo instance)
		{
			if (instance.HasTimezone)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Timezone));
			}
			if (instance.HasMinutesPerDay)
			{
				stream.WriteByte(32);
				ProtocolParser.WriteUInt32(stream, instance.MinutesPerDay);
			}
			if (instance.HasMinutesPerWeek)
			{
				stream.WriteByte(40);
				ProtocolParser.WriteUInt32(stream, instance.MinutesPerWeek);
			}
			if (instance.HasCanReceiveVoice)
			{
				stream.WriteByte(48);
				ProtocolParser.WriteBool(stream, instance.CanReceiveVoice);
			}
			if (instance.HasCanSendVoice)
			{
				stream.WriteByte(56);
				ProtocolParser.WriteBool(stream, instance.CanSendVoice);
			}
			if (instance.PlaySchedule.Count > 0)
			{
				foreach (bool val in instance.PlaySchedule)
				{
					stream.WriteByte(64);
					ProtocolParser.WriteBool(stream, val);
				}
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (this.HasTimezone)
			{
				num += 1u;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.Timezone);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (this.HasMinutesPerDay)
			{
				num += 1u;
				num += ProtocolParser.SizeOfUInt32(this.MinutesPerDay);
			}
			if (this.HasMinutesPerWeek)
			{
				num += 1u;
				num += ProtocolParser.SizeOfUInt32(this.MinutesPerWeek);
			}
			if (this.HasCanReceiveVoice)
			{
				num += 1u;
				num += 1u;
			}
			if (this.HasCanSendVoice)
			{
				num += 1u;
				num += 1u;
			}
			if (this.PlaySchedule.Count > 0)
			{
				foreach (bool flag in this.PlaySchedule)
				{
					num += 1u;
					num += 1u;
				}
			}
			return num;
		}

		public bool HasTimezone;

		private string _Timezone;

		public bool HasMinutesPerDay;

		private uint _MinutesPerDay;

		public bool HasMinutesPerWeek;

		private uint _MinutesPerWeek;

		public bool HasCanReceiveVoice;

		private bool _CanReceiveVoice;

		public bool HasCanSendVoice;

		private bool _CanSendVoice;

		private List<bool> _PlaySchedule = new List<bool>();
	}
}
