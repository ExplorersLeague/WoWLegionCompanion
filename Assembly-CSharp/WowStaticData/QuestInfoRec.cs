using System;

namespace WowStaticData
{
	public class QuestInfoRec : MODBRec
	{
		public int ID { get; private set; }

		public string InfoName { get; private set; }

		public int Type { get; private set; }

		public int Modifiers { get; private set; }

		public int Profession { get; private set; }

		protected override void DeserializeIndex(int index, string valueText)
		{
			switch (index)
			{
			case 0:
				this.ID = Convert.ToInt32(valueText);
				break;
			case 1:
				this.InfoName = valueText;
				break;
			case 2:
				this.Type = Convert.ToInt32(valueText);
				break;
			case 3:
				this.Modifiers = Convert.ToInt32(valueText);
				break;
			case 4:
				this.Profession = Convert.ToInt32(valueText);
				break;
			}
		}
	}
}
