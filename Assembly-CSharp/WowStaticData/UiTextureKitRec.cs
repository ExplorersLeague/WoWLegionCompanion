using System;

namespace WowStaticData
{
	public class UiTextureKitRec : MODBRec
	{
		public int ID { get; private set; }

		public string KitPrefix { get; private set; }

		protected override void DeserializeIndex(int index, string valueText)
		{
			if (index != 0)
			{
				if (index == 1)
				{
					this.KitPrefix = valueText;
				}
			}
			else
			{
				this.ID = Convert.ToInt32(valueText);
			}
		}
	}
}
