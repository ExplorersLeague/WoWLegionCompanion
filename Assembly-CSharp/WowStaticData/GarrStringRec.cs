using System;

namespace WowStaticData
{
	public class GarrStringRec : MODBRec
	{
		public int ID { get; private set; }

		public string Text { get; private set; }

		protected override void DeserializeIndex(int index, string valueText)
		{
			if (index != 0)
			{
				if (index == 1)
				{
					this.Text = valueText;
				}
			}
			else
			{
				this.ID = Convert.ToInt32(valueText);
			}
		}
	}
}
