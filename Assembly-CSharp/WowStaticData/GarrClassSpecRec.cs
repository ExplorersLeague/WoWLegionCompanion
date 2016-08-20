using System;

namespace WowStaticData
{
	public class GarrClassSpecRec
	{
		public int ID { get; private set; }

		public string ClassSpec { get; private set; }

		public string ClassSpec_Male { get; private set; }

		public string ClassSpec_Female { get; private set; }

		public uint UiTextureAtlasMemberID { get; private set; }

		public uint GarrFollItemSetID { get; private set; }

		public int Flags { get; private set; }

		public uint FollowerClassLimit { get; private set; }

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
				this.ClassSpec = valueText;
				break;
			case 2:
				this.ClassSpec_Male = valueText;
				break;
			case 3:
				this.ClassSpec_Female = valueText;
				break;
			case 4:
				this.UiTextureAtlasMemberID = Convert.ToUInt32(valueText);
				break;
			case 5:
				this.GarrFollItemSetID = Convert.ToUInt32(valueText);
				break;
			case 6:
				this.Flags = Convert.ToInt32(valueText);
				break;
			case 7:
				this.FollowerClassLimit = Convert.ToUInt32(valueText);
				break;
			}
		}
	}
}
