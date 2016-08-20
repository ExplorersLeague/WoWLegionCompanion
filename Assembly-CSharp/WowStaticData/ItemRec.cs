using System;

namespace WowStaticData
{
	public class ItemRec
	{
		public int ID { get; private set; }

		public int IconFileDataID { get; private set; }

		public int OverallQualityID { get; private set; }

		public string Display { get; private set; }

		public string Description { get; private set; }

		public int Flags { get; private set; }

		public int Flags1 { get; private set; }

		public int Flags2 { get; private set; }

		public int StatModifier_bonus0 { get; private set; }

		public int StatModifier_bonus1 { get; private set; }

		public int StatModifier_bonus2 { get; private set; }

		public int StatModifier_bonus3 { get; private set; }

		public int StatModifier_bonus4 { get; private set; }

		public int StatModifier_bonus5 { get; private set; }

		public int StatModifier_bonus6 { get; private set; }

		public int StatModifier_bonus7 { get; private set; }

		public int StatModifier_bonus8 { get; private set; }

		public int StatModifier_bonus9 { get; private set; }

		public int StatModifier_bonusStat0 { get; private set; }

		public int StatModifier_bonusStat1 { get; private set; }

		public int StatModifier_bonusStat2 { get; private set; }

		public int StatModifier_bonusStat3 { get; private set; }

		public int StatModifier_bonusStat4 { get; private set; }

		public int StatModifier_bonusStat5 { get; private set; }

		public int StatModifier_bonusStat6 { get; private set; }

		public int StatModifier_bonusStat7 { get; private set; }

		public int StatModifier_bonusStat8 { get; private set; }

		public int StatModifier_bonusStat9 { get; private set; }

		public int ItemNameDescriptionID { get; private set; }

		public int ClassID { get; private set; }

		public int SubclassID { get; private set; }

		public int ItemLevel { get; private set; }

		public int Bonding { get; private set; }

		public int InventoryType { get; private set; }

		public int RequiredLevel { get; private set; }

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
				this.IconFileDataID = Convert.ToInt32(valueText);
				break;
			case 2:
				this.OverallQualityID = Convert.ToInt32(valueText);
				break;
			case 3:
				this.Display = valueText;
				break;
			case 4:
				this.Description = valueText;
				break;
			case 5:
				this.Flags = Convert.ToInt32(valueText);
				break;
			case 6:
				this.Flags1 = Convert.ToInt32(valueText);
				break;
			case 7:
				this.Flags2 = Convert.ToInt32(valueText);
				break;
			case 8:
				this.StatModifier_bonus0 = Convert.ToInt32(valueText);
				break;
			case 9:
				this.StatModifier_bonus1 = Convert.ToInt32(valueText);
				break;
			case 10:
				this.StatModifier_bonus2 = Convert.ToInt32(valueText);
				break;
			case 11:
				this.StatModifier_bonus3 = Convert.ToInt32(valueText);
				break;
			case 12:
				this.StatModifier_bonus4 = Convert.ToInt32(valueText);
				break;
			case 13:
				this.StatModifier_bonus5 = Convert.ToInt32(valueText);
				break;
			case 14:
				this.StatModifier_bonus6 = Convert.ToInt32(valueText);
				break;
			case 15:
				this.StatModifier_bonus7 = Convert.ToInt32(valueText);
				break;
			case 16:
				this.StatModifier_bonus8 = Convert.ToInt32(valueText);
				break;
			case 17:
				this.StatModifier_bonus9 = Convert.ToInt32(valueText);
				break;
			case 18:
				this.StatModifier_bonusStat0 = Convert.ToInt32(valueText);
				break;
			case 19:
				this.StatModifier_bonusStat1 = Convert.ToInt32(valueText);
				break;
			case 20:
				this.StatModifier_bonusStat2 = Convert.ToInt32(valueText);
				break;
			case 21:
				this.StatModifier_bonusStat3 = Convert.ToInt32(valueText);
				break;
			case 22:
				this.StatModifier_bonusStat4 = Convert.ToInt32(valueText);
				break;
			case 23:
				this.StatModifier_bonusStat5 = Convert.ToInt32(valueText);
				break;
			case 24:
				this.StatModifier_bonusStat6 = Convert.ToInt32(valueText);
				break;
			case 25:
				this.StatModifier_bonusStat7 = Convert.ToInt32(valueText);
				break;
			case 26:
				this.StatModifier_bonusStat8 = Convert.ToInt32(valueText);
				break;
			case 27:
				this.StatModifier_bonusStat9 = Convert.ToInt32(valueText);
				break;
			case 28:
				this.ItemNameDescriptionID = Convert.ToInt32(valueText);
				break;
			case 29:
				this.ClassID = Convert.ToInt32(valueText);
				break;
			case 30:
				this.SubclassID = Convert.ToInt32(valueText);
				break;
			case 31:
				this.ItemLevel = Convert.ToInt32(valueText);
				break;
			case 32:
				this.Bonding = Convert.ToInt32(valueText);
				break;
			case 33:
				this.InventoryType = Convert.ToInt32(valueText);
				break;
			case 34:
				this.RequiredLevel = Convert.ToInt32(valueText);
				break;
			}
		}
	}
}
