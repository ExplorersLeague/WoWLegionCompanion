using System;

namespace WowStaticData
{
	public class MapRec : MODBRec
	{
		public int ID { get; private set; }

		public string MapName { get; private set; }

		protected override void DeserializeIndex(int index, string valueText)
		{
			if (index != 0)
			{
				if (index == 1)
				{
					this.MapName = valueText;
				}
			}
			else
			{
				this.ID = Convert.ToInt32(valueText);
			}
		}
	}
}
