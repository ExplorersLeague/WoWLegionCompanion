using System;

namespace WowStaticData
{
	public class ItemNameDescriptionRec : MODBRec
	{
		public int ID { get; private set; }

		public string Description { get; private set; }

		public int Color { get; private set; }

		protected override void DeserializeIndex(int index, string valueText)
		{
			if (index != 0)
			{
				if (index != 1)
				{
					if (index == 2)
					{
						this.Color = Convert.ToInt32(valueText);
					}
				}
				else
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
