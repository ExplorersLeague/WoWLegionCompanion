using System;

namespace WowStaticData
{
	public class Cfg_LanguagesRec : MODBRec
	{
		public int ID { get; private set; }

		public int Wowlocale_ID { get; private set; }

		public string Wowlocale_code { get; private set; }

		protected override void DeserializeIndex(int index, string valueText)
		{
			if (index != 0)
			{
				if (index != 1)
				{
					if (index == 2)
					{
						this.Wowlocale_code = valueText;
					}
				}
				else
				{
					this.Wowlocale_ID = Convert.ToInt32(valueText);
				}
			}
			else
			{
				this.ID = Convert.ToInt32(valueText);
			}
		}
	}
}
