using System;

namespace WowStaticData
{
	public class GarrMissionRewardRec : MODBRec
	{
		public int ID { get; private set; }

		public ushort GarrMissionID { get; private set; }

		public ushort FollowerXP { get; private set; }

		public int ItemID { get; private set; }

		public byte ItemQuantity { get; private set; }

		public ushort CurrencyType { get; private set; }

		public uint CurrencyQuantity { get; private set; }

		public byte TreasureChance { get; private set; }

		public byte TreasureXP { get; private set; }

		public byte TreasureQuality { get; private set; }

		public byte GarrMssnBonusAbilityID { get; private set; }

		protected override void DeserializeIndex(int index, string valueText)
		{
			switch (index)
			{
			case 0:
				this.ID = Convert.ToInt32(valueText);
				break;
			case 1:
				this.GarrMissionID = Convert.ToUInt16(valueText);
				break;
			case 2:
				this.FollowerXP = Convert.ToUInt16(valueText);
				break;
			case 3:
				this.ItemID = Convert.ToInt32(valueText);
				break;
			case 4:
				this.ItemQuantity = Convert.ToByte(valueText);
				break;
			case 5:
				this.CurrencyType = Convert.ToUInt16(valueText);
				break;
			case 6:
				this.CurrencyQuantity = Convert.ToUInt32(valueText);
				break;
			case 7:
				this.TreasureChance = Convert.ToByte(valueText);
				break;
			case 8:
				this.TreasureXP = Convert.ToByte(valueText);
				break;
			case 9:
				this.TreasureQuality = Convert.ToByte(valueText);
				break;
			case 10:
				this.GarrMssnBonusAbilityID = Convert.ToByte(valueText);
				break;
			}
		}
	}
}
