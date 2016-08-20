using System;

namespace WowStaticData
{
	public class MobileStringsRec
	{
		public int ID { get; private set; }

		public string BaseTag { get; private set; }

		public string TagText { get; private set; }

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
				this.BaseTag = valueText;
				break;
			case 2:
				this.TagText = valueText;
				break;
			}
		}
	}
}
