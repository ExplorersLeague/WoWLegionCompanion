using System;

namespace WowStaticData
{
	public class QuestV2Rec
	{
		public int ID { get; private set; }

		public string QuestTitle { get; private set; }

		public string Description { get; private set; }

		public string LogDescription { get; private set; }

		public string RewardText { get; private set; }

		public int AreaID { get; private set; }

		public int QuestInfoID { get; private set; }

		public int QuestSortID { get; private set; }

		public void Deserialize(string valueLine)
		{
			int num = 0;
			int num2 = 0;
			int num3;
			do
			{
				num3 = valueLine.IndexOf('\t', num);
				if (num3 >= 0)
				{
					string valueText = valueLine.Substring(num, num3 - num).Trim();
					this.DeserializeIndex(num2, valueText);
					num2++;
				}
				num = num3 + 1;
			}
			while (num3 > 0);
		}

		private void DeserializeIndex(int index, string valueText)
		{
			switch (index)
			{
			case 0:
				this.ID = Convert.ToInt32(valueText);
				break;
			case 1:
				this.QuestTitle = valueText;
				break;
			case 2:
				this.Description = valueText;
				break;
			case 3:
				this.LogDescription = valueText;
				break;
			case 4:
				this.RewardText = valueText;
				break;
			case 5:
				this.AreaID = Convert.ToInt32(valueText);
				break;
			case 6:
				this.QuestInfoID = Convert.ToInt32(valueText);
				break;
			case 7:
				this.QuestSortID = Convert.ToInt32(valueText);
				break;
			}
		}
	}
}
