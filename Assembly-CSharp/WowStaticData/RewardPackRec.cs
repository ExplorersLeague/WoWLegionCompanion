using System;

namespace WowStaticData
{
	public class RewardPackRec : MODBRec
	{
		public int ID { get; private set; }

		public int Money { get; private set; }

		protected override void DeserializeIndex(int index, string valueText)
		{
			if (index != 0)
			{
				if (index == 1)
				{
					this.Money = Convert.ToInt32(valueText);
				}
			}
			else
			{
				this.ID = Convert.ToInt32(valueText);
			}
		}
	}
}
