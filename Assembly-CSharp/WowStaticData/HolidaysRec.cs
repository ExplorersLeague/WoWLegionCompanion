using System;

namespace WowStaticData
{
	public class HolidaysRec : MODBRec
	{
		public HolidaysRec()
		{
			this.Duration = new ushort[10];
			this.Date = new uint[26];
			this.CalendarFlags = new byte[10];
			this.TextureFileDataID = new int[3];
		}

		public int ID { get; private set; }

		public ushort[] Duration { get; private set; }

		public uint[] Date { get; private set; }

		public ushort Region { get; private set; }

		public byte Looping { get; private set; }

		public byte[] CalendarFlags { get; private set; }

		public uint HolidayNameID { get; private set; }

		public uint HolidayDescriptionID { get; private set; }

		public string TextureFileName { get; private set; }

		public int[] TextureFileDataID { get; private set; }

		public byte Priority { get; private set; }

		public sbyte CalendarFilterType { get; private set; }

		public byte Flags { get; private set; }

		protected override void DeserializeIndex(int index, string valueText)
		{
			switch (index)
			{
			case 0:
				this.ID = Convert.ToInt32(valueText);
				break;
			case 1:
				this.Duration[0] = Convert.ToUInt16(valueText);
				break;
			case 2:
				this.Duration[1] = Convert.ToUInt16(valueText);
				break;
			case 3:
				this.Duration[2] = Convert.ToUInt16(valueText);
				break;
			case 4:
				this.Duration[3] = Convert.ToUInt16(valueText);
				break;
			case 5:
				this.Duration[4] = Convert.ToUInt16(valueText);
				break;
			case 6:
				this.Duration[5] = Convert.ToUInt16(valueText);
				break;
			case 7:
				this.Duration[6] = Convert.ToUInt16(valueText);
				break;
			case 8:
				this.Duration[7] = Convert.ToUInt16(valueText);
				break;
			case 9:
				this.Duration[8] = Convert.ToUInt16(valueText);
				break;
			case 10:
				this.Duration[9] = Convert.ToUInt16(valueText);
				break;
			case 11:
				this.Date[0] = Convert.ToUInt32(valueText);
				break;
			case 12:
				this.Date[1] = Convert.ToUInt32(valueText);
				break;
			case 13:
				this.Date[2] = Convert.ToUInt32(valueText);
				break;
			case 14:
				this.Date[3] = Convert.ToUInt32(valueText);
				break;
			case 15:
				this.Date[4] = Convert.ToUInt32(valueText);
				break;
			case 16:
				this.Date[5] = Convert.ToUInt32(valueText);
				break;
			case 17:
				this.Date[6] = Convert.ToUInt32(valueText);
				break;
			case 18:
				this.Date[7] = Convert.ToUInt32(valueText);
				break;
			case 19:
				this.Date[8] = Convert.ToUInt32(valueText);
				break;
			case 20:
				this.Date[9] = Convert.ToUInt32(valueText);
				break;
			case 21:
				this.Date[10] = Convert.ToUInt32(valueText);
				break;
			case 22:
				this.Date[11] = Convert.ToUInt32(valueText);
				break;
			case 23:
				this.Date[12] = Convert.ToUInt32(valueText);
				break;
			case 24:
				this.Date[13] = Convert.ToUInt32(valueText);
				break;
			case 25:
				this.Date[14] = Convert.ToUInt32(valueText);
				break;
			case 26:
				this.Date[15] = Convert.ToUInt32(valueText);
				break;
			case 27:
				this.Date[16] = Convert.ToUInt32(valueText);
				break;
			case 28:
				this.Date[17] = Convert.ToUInt32(valueText);
				break;
			case 29:
				this.Date[18] = Convert.ToUInt32(valueText);
				break;
			case 30:
				this.Date[19] = Convert.ToUInt32(valueText);
				break;
			case 31:
				this.Date[20] = Convert.ToUInt32(valueText);
				break;
			case 32:
				this.Date[21] = Convert.ToUInt32(valueText);
				break;
			case 33:
				this.Date[22] = Convert.ToUInt32(valueText);
				break;
			case 34:
				this.Date[23] = Convert.ToUInt32(valueText);
				break;
			case 35:
				this.Date[24] = Convert.ToUInt32(valueText);
				break;
			case 36:
				this.Date[25] = Convert.ToUInt32(valueText);
				break;
			case 37:
				this.Region = Convert.ToUInt16(valueText);
				break;
			case 38:
				this.Looping = Convert.ToByte(valueText);
				break;
			case 39:
				this.CalendarFlags[0] = Convert.ToByte(valueText);
				break;
			case 40:
				this.CalendarFlags[1] = Convert.ToByte(valueText);
				break;
			case 41:
				this.CalendarFlags[2] = Convert.ToByte(valueText);
				break;
			case 42:
				this.CalendarFlags[3] = Convert.ToByte(valueText);
				break;
			case 43:
				this.CalendarFlags[4] = Convert.ToByte(valueText);
				break;
			case 44:
				this.CalendarFlags[5] = Convert.ToByte(valueText);
				break;
			case 45:
				this.CalendarFlags[6] = Convert.ToByte(valueText);
				break;
			case 46:
				this.CalendarFlags[7] = Convert.ToByte(valueText);
				break;
			case 47:
				this.CalendarFlags[8] = Convert.ToByte(valueText);
				break;
			case 48:
				this.CalendarFlags[9] = Convert.ToByte(valueText);
				break;
			case 49:
				this.HolidayNameID = Convert.ToUInt32(valueText);
				break;
			case 50:
				this.HolidayDescriptionID = Convert.ToUInt32(valueText);
				break;
			case 51:
				this.TextureFileName = valueText;
				break;
			case 52:
				this.TextureFileDataID[0] = Convert.ToInt32(valueText);
				break;
			case 53:
				this.TextureFileDataID[1] = Convert.ToInt32(valueText);
				break;
			case 54:
				this.TextureFileDataID[2] = Convert.ToInt32(valueText);
				break;
			case 55:
				this.Priority = Convert.ToByte(valueText);
				break;
			case 56:
				this.CalendarFilterType = Convert.ToSByte(valueText);
				break;
			case 57:
				this.Flags = Convert.ToByte(valueText);
				break;
			}
		}
	}
}
