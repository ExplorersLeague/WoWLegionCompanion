using System;

namespace WowStaticData
{
	public class MobileStringsRec : MODBRec
	{
		public int ID { get; private set; }

		public string BaseTag { get; private set; }

		public string TagText { get; private set; }

		protected override void DeserializeIndex(int index, string valueText)
		{
			if (index != 0)
			{
				if (index != 1)
				{
					if (index == 2)
					{
						this.TagText = valueText;
					}
				}
				else
				{
					this.BaseTag = valueText;
				}
			}
			else
			{
				this.ID = Convert.ToInt32(valueText);
			}
		}
	}
}
