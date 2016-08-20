using System;

namespace WowStaticData
{
	public class GarrFollowerLevelXPRec
	{
		public int ID { get; private set; }

		public uint FollowerLevel { get; private set; }

		public uint XpToNextLevel { get; private set; }

		public uint ShipmentXP { get; private set; }

		public uint GarrFollowerTypeID { get; private set; }

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
				this.FollowerLevel = Convert.ToUInt32(valueText);
				break;
			case 2:
				this.XpToNextLevel = Convert.ToUInt32(valueText);
				break;
			case 3:
				this.ShipmentXP = Convert.ToUInt32(valueText);
				break;
			case 4:
				this.GarrFollowerTypeID = Convert.ToUInt32(valueText);
				break;
			}
		}
	}
}
