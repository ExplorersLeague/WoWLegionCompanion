using System;

namespace WowStaticData
{
	public class RewardPackXItemRec : MODBRec
	{
		public int ID { get; private set; }

		public int RewardPackID { get; private set; }

		public int ItemID { get; private set; }

		public int ItemQuantity { get; private set; }

		protected override void DeserializeIndex(int index, string valueText)
		{
			switch (index)
			{
			case 0:
				this.ID = Convert.ToInt32(valueText);
				break;
			case 1:
				this.RewardPackID = Convert.ToInt32(valueText);
				break;
			case 2:
				this.ItemID = Convert.ToInt32(valueText);
				break;
			case 3:
				this.ItemQuantity = Convert.ToInt32(valueText);
				break;
			}
		}
	}
}
