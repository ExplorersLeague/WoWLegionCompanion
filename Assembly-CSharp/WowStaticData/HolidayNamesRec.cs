using System;

namespace WowStaticData
{
	public class HolidayNamesRec : MODBRec
	{
		public int ID { get; private set; }

		public string Name { get; private set; }

		public int Name_flag { get; private set; }

		protected override void DeserializeIndex(int index, string valueText)
		{
			if (index != 0)
			{
				if (index != 1)
				{
					if (index == 2)
					{
						this.Name_flag = Convert.ToInt32(valueText);
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
