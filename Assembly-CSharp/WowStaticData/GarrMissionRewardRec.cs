using System;

namespace WowStaticData
{
	public class GarrMissionRewardRec
	{
		public int ID { get; private set; }

		public uint GarrMissionID { get; private set; }

		public uint FollowerXP { get; private set; }

		public int ItemID { get; private set; }

		public uint ItemQuantity { get; private set; }

		public uint CurrencyType { get; private set; }

		public uint CurrencyQuantity { get; private set; }

		public uint TreasureChance { get; private set; }

		public uint TreasureXP { get; private set; }

		public uint TreasureQuality { get; private set; }

		public uint GarrMssnBonusAbilityID { get; private set; }

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
				this.GarrMissionID = Convert.ToUInt32(valueText);
				break;
			case 2:
				this.FollowerXP = Convert.ToUInt32(valueText);
				break;
			case 3:
				this.ItemID = Convert.ToInt32(valueText);
				break;
			case 4:
				this.ItemQuantity = Convert.ToUInt32(valueText);
				break;
			case 5:
				this.CurrencyType = Convert.ToUInt32(valueText);
				break;
			case 6:
				this.CurrencyQuantity = Convert.ToUInt32(valueText);
				break;
			case 7:
				this.TreasureChance = Convert.ToUInt32(valueText);
				break;
			case 8:
				this.TreasureXP = Convert.ToUInt32(valueText);
				break;
			case 9:
				this.TreasureQuality = Convert.ToUInt32(valueText);
				break;
			case 10:
				this.GarrMssnBonusAbilityID = Convert.ToUInt32(valueText);
				break;
			}
		}
	}
}
