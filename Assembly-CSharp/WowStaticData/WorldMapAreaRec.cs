using System;

namespace WowStaticData
{
	public class WorldMapAreaRec : MODBRec
	{
		public int ID { get; private set; }

		public int AreaID { get; private set; }

		protected override void DeserializeIndex(int index, string valueText)
		{
			if (index != 0)
			{
				if (index == 1)
				{
					this.AreaID = Convert.ToInt32(valueText);
				}
			}
			else
			{
				this.ID = Convert.ToInt32(valueText);
			}
		}
	}
}
