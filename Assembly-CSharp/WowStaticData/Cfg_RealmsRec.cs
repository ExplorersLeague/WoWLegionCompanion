using System;

namespace WowStaticData
{
	public class Cfg_RealmsRec : MODBRec
	{
		public int ID { get; private set; }

		public int Region_ID { get; private set; }

		protected override void DeserializeIndex(int index, string valueText)
		{
			if (index != 0)
			{
				if (index == 1)
				{
					this.Region_ID = Convert.ToInt32(valueText);
				}
			}
			else
			{
				this.ID = Convert.ToInt32(valueText);
			}
		}
	}
}
