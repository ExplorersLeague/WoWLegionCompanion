using System;

namespace WowStaticData
{
	public class CurrencyContainerRec
	{
		public int ID { get; private set; }

		public int CurrencyTypeId { get; private set; }

		public int MinAmount { get; private set; }

		public int MaxAmount { get; private set; }

		public int ContainerIconID { get; private set; }

		public string ContainerName { get; private set; }

		public string ContainerDescription { get; private set; }

		public int ContainerQuality { get; private set; }

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
				this.CurrencyTypeId = Convert.ToInt32(valueText);
				break;
			case 2:
				this.MinAmount = Convert.ToInt32(valueText);
				break;
			case 3:
				this.MaxAmount = Convert.ToInt32(valueText);
				break;
			case 4:
				this.ContainerIconID = Convert.ToInt32(valueText);
				break;
			case 5:
				this.ContainerName = valueText;
				break;
			case 6:
				this.ContainerDescription = valueText;
				break;
			case 7:
				this.ContainerQuality = Convert.ToInt32(valueText);
				break;
			}
		}
	}
}
