using System;

namespace WowStaticData
{
	public class Cfg_RegionsRec : MODBRec
	{
		public int ID { get; private set; }

		public string Name { get; private set; }

		public int Region_ID { get; private set; }

		public int Region_group_mask { get; private set; }

		protected override void DeserializeIndex(int index, string valueText)
		{
			switch (index)
			{
			case 0:
				this.ID = Convert.ToInt32(valueText);
				break;
			case 1:
				this.Name = valueText;
				break;
			case 2:
				this.Region_ID = Convert.ToInt32(valueText);
				break;
			case 3:
				this.Region_group_mask = Convert.ToInt32(valueText);
				break;
			}
		}
	}
}
