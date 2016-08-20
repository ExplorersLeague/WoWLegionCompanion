using System;

namespace WowStaticData
{
	public class GarrAbilityRec
	{
		public int ID { get; private set; }

		public uint Flags { get; private set; }

		public string Name { get; private set; }

		public string Description { get; private set; }

		public int IconFileDataID { get; private set; }

		public uint FactionChangeGarrAbilityID { get; private set; }

		public uint GarrAbilityCategoryID { get; private set; }

		public uint GarrFollowerTypeID { get; private set; }

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
				this.Flags = Convert.ToUInt32(valueText);
				break;
			case 2:
				this.Name = valueText;
				break;
			case 3:
				this.Description = valueText;
				break;
			case 4:
				this.IconFileDataID = Convert.ToInt32(valueText);
				break;
			case 5:
				this.FactionChangeGarrAbilityID = Convert.ToUInt32(valueText);
				break;
			case 6:
				this.GarrAbilityCategoryID = Convert.ToUInt32(valueText);
				break;
			case 7:
				this.GarrFollowerTypeID = Convert.ToUInt32(valueText);
				break;
			}
		}
	}
}
