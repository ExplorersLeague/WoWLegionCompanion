using System;

namespace WowStaticData
{
	public class TreasureRec : MODBRec
	{
		public int ID { get; private set; }

		public int CoverItemID { get; private set; }

		protected override void DeserializeIndex(int index, string valueText)
		{
			if (index != 0)
			{
				if (index == 1)
				{
					this.CoverItemID = Convert.ToInt32(valueText);
				}
			}
			else
			{
				this.ID = Convert.ToInt32(valueText);
			}
		}
	}
}
