using System;

namespace WowStaticData
{
	public class GarrAbilityCategoryRec : MODBRec
	{
		public int ID { get; private set; }

		public string Name { get; private set; }

		protected override void DeserializeIndex(int index, string valueText)
		{
			if (index != 0)
			{
				if (index == 1)
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
