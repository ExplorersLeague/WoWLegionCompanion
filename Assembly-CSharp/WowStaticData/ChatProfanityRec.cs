using System;

namespace WowStaticData
{
	public class ChatProfanityRec : MODBRec
	{
		public int ID { get; private set; }

		public string Text { get; private set; }

		public int Language { get; private set; }

		protected override void DeserializeIndex(int index, string valueText)
		{
			if (index != 0)
			{
				if (index != 1)
				{
					if (index == 2)
					{
						this.Language = Convert.ToInt32(valueText);
					}
				}
				else
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
