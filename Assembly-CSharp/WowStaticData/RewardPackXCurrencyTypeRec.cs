using System;

namespace WowStaticData
{
	public class RewardPackXCurrencyTypeRec : MODBRec
	{
		public int ID { get; private set; }

		public int RewardPackID { get; private set; }

		public int CurrencyTypeID { get; private set; }

		public int Quantity { get; private set; }

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
				this.CurrencyTypeID = Convert.ToInt32(valueText);
				break;
			case 3:
				this.Quantity = Convert.ToInt32(valueText);
				break;
			}
		}
	}
}
