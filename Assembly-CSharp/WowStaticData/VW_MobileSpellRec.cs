using System;

namespace WowStaticData
{
	public class VW_MobileSpellRec : MODBRec
	{
		public int ID { get; private set; }

		public string Name { get; private set; }

		public int SpellIconFileDataID { get; private set; }

		protected override void DeserializeIndex(int index, string valueText)
		{
			if (index != 0)
			{
				if (index != 1)
				{
					if (index == 2)
					{
						this.SpellIconFileDataID = Convert.ToInt32(valueText);
					}
				}
				else
				{
					this.Name = valueText;
				}
			}
			else
			{
				this.ID = Convert.ToInt32(valueText);
			}
		}
	}
}
