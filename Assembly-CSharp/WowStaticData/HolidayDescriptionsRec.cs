using System;

namespace WowStaticData
{
	public class HolidayDescriptionsRec : MODBRec
	{
		public int ID { get; private set; }

		public string Description { get; private set; }

		protected override void DeserializeIndex(int index, string valueText)
		{
			if (index != 0)
			{
				if (index == 1)
				{
					this.Description = valueText;
				}
			}
			else
			{
				this.ID = Convert.ToInt32(valueText);
			}
		}
	}
}
