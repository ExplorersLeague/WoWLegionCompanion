using System;

namespace WowStaticData
{
	public class LFGDungeonsRec : MODBRec
	{
		public LFGDungeonsRec()
		{
			this.Flags = new int[2];
		}

		public int ID { get; private set; }

		public string Name { get; private set; }

		public byte TypeID { get; private set; }

		public byte Subtype { get; private set; }

		public sbyte Faction { get; private set; }

		public byte ExpansionLevel { get; private set; }

		public byte DifficultyID { get; private set; }

		public int[] Flags { get; private set; }

		public int IconTextureFileID { get; private set; }

		public short MapID { get; private set; }

		protected override void DeserializeIndex(int index, string valueText)
		{
			switch (index)
			{
			case 0:
				this.ID = Convert.ToInt32(valueText);
				break;
			case 1:
				this.Name = valueText;
				break;
			case 2:
				this.TypeID = Convert.ToByte(valueText);
				break;
			case 3:
				this.Subtype = Convert.ToByte(valueText);
				break;
			case 4:
				this.Faction = Convert.ToSByte(valueText);
				break;
			case 5:
				this.ExpansionLevel = Convert.ToByte(valueText);
				break;
			case 6:
				this.DifficultyID = Convert.ToByte(valueText);
				break;
			case 7:
				this.Flags[0] = Convert.ToInt32(valueText);
				break;
			case 8:
				this.Flags[1] = Convert.ToInt32(valueText);
				break;
			case 9:
				this.IconTextureFileID = Convert.ToInt32(valueText);
				break;
			case 10:
				this.MapID = Convert.ToInt16(valueText);
				break;
			}
		}
	}
}
