using System;

namespace WowStaticData
{
	public class CreatureRec : MODBRec
	{
		public int ID { get; private set; }

		public int DisplayID { get; private set; }

		public string Name { get; private set; }

		protected override void DeserializeIndex(int index, string valueText)
		{
			if (index != 0)
			{
				if (index != 1)
				{
					if (index == 2)
					{
						this.Name = valueText;
					}
				}
				else
				{
					this.DisplayID = Convert.ToInt32(valueText);
				}
			}
			else
			{
				this.ID = Convert.ToInt32(valueText);
			}
		}
	}
}
