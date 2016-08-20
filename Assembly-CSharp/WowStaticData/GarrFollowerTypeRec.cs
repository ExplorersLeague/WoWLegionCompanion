using System;

namespace WowStaticData
{
	public class GarrFollowerTypeRec
	{
		public int ID { get; private set; }

		public uint MaxFollowers { get; private set; }

		public uint MaxFollowerBuildingType { get; private set; }

		public uint MaxItemLevel { get; private set; }

		public uint GarrTypeID { get; private set; }

		public uint LevelRangeBias { get; private set; }

		public uint ItemLevelRangeBias { get; private set; }

		public uint Flags { get; private set; }

		public void Deserialize(string valueLine)
		{
			int num = 0;
			int num2 = 0;
			int num3;
			do
			{
				num3 = valueLine.IndexOf('\t', num);
				if (num3 >= 0)
				{
					string valueText = valueLine.Substring(num, num3 - num).Trim();
					this.DeserializeIndex(num2, valueText);
					num2++;
				}
				num = num3 + 1;
			}
			while (num3 > 0);
		}

		private void DeserializeIndex(int index, string valueText)
		{
			switch (index)
			{
			case 0:
				this.ID = Convert.ToInt32(valueText);
				break;
			case 1:
				this.MaxFollowers = Convert.ToUInt32(valueText);
				break;
			case 2:
				this.MaxFollowerBuildingType = Convert.ToUInt32(valueText);
				break;
			case 3:
				this.MaxItemLevel = Convert.ToUInt32(valueText);
				break;
			case 4:
				this.GarrTypeID = Convert.ToUInt32(valueText);
				break;
			case 5:
				this.LevelRangeBias = Convert.ToUInt32(valueText);
				break;
			case 6:
				this.ItemLevelRangeBias = Convert.ToUInt32(valueText);
				break;
			case 7:
				this.Flags = Convert.ToUInt32(valueText);
				break;
			}
		}
	}
}
