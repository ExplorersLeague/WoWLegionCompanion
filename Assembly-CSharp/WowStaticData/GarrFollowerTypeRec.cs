using System;

namespace WowStaticData
{
	public class GarrFollowerTypeRec : MODBRec
	{
		public int ID { get; private set; }

		public byte MaxFollowers { get; private set; }

		public byte MaxFollowerBuildingType { get; private set; }

		public ushort MaxItemLevel { get; private set; }

		public byte GarrTypeID { get; private set; }

		public byte LevelRangeBias { get; private set; }

		public byte ItemLevelRangeBias { get; private set; }

		public byte Flags { get; private set; }

		protected override void DeserializeIndex(int index, string valueText)
		{
			switch (index)
			{
			case 0:
				this.ID = Convert.ToInt32(valueText);
				break;
			case 1:
				this.MaxFollowers = Convert.ToByte(valueText);
				break;
			case 2:
				this.MaxFollowerBuildingType = Convert.ToByte(valueText);
				break;
			case 3:
				this.MaxItemLevel = Convert.ToUInt16(valueText);
				break;
			case 4:
				this.GarrTypeID = Convert.ToByte(valueText);
				break;
			case 5:
				this.LevelRangeBias = Convert.ToByte(valueText);
				break;
			case 6:
				this.ItemLevelRangeBias = Convert.ToByte(valueText);
				break;
			case 7:
				this.Flags = Convert.ToByte(valueText);
				break;
			}
		}
	}
}
