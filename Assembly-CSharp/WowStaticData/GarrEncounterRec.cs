using System;

namespace WowStaticData
{
	public class GarrEncounterRec
	{
		public int ID { get; private set; }

		public int CreatureID { get; private set; }

		public string Name { get; private set; }

		public float UiAnimScale { get; private set; }

		public float UiAnimHeight { get; private set; }

		public int PortraitFileDataID { get; private set; }

		public uint UiTextureKitID { get; private set; }

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
				this.CreatureID = Convert.ToInt32(valueText);
				break;
			case 2:
				this.Name = valueText;
				break;
			case 3:
				this.UiAnimScale = (float)Convert.ToDouble(valueText);
				break;
			case 4:
				this.UiAnimHeight = (float)Convert.ToDouble(valueText);
				break;
			case 5:
				this.PortraitFileDataID = Convert.ToInt32(valueText);
				break;
			case 6:
				this.UiTextureKitID = Convert.ToUInt32(valueText);
				break;
			}
		}
	}
}
