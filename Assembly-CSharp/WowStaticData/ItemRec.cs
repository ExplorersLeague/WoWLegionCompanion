using System;

namespace WowStaticData
{
	public class ItemRec : MODBRec
	{
		public ItemRec()
		{
			this.Flags = new int[3];
			this.StatModifier_bonus = new int[10];
			this.StatModifier_bonusStat = new int[10];
		}

		public int ID { get; private set; }

		public int IconFileDataID { get; private set; }

		public int OverallQualityID { get; private set; }

		public string Display { get; private set; }

		public string Description { get; private set; }

		public int[] Flags { get; private set; }

		public int[] StatModifier_bonus { get; private set; }

		public int[] StatModifier_bonusStat { get; private set; }

		public int ItemNameDescriptionID { get; private set; }

		public int ClassID { get; private set; }

		public int SubclassID { get; private set; }

		public int ItemLevel { get; private set; }

		public int Bonding { get; private set; }

		public int InventoryType { get; private set; }

		public int RequiredLevel { get; private set; }

		protected override void DeserializeIndex(int index, string valueText)
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
				this.Flags[0] = Convert.ToInt32(valueText);
				break;
			case 6:
				this.Flags[1] = Convert.ToInt32(valueText);
				break;
			case 7:
				this.Flags[2] = Convert.ToInt32(valueText);
				break;
			case 8:
				this.StatModifier_bonus[0] = Convert.ToInt32(valueText);
				break;
			case 9:
				this.StatModifier_bonus[1] = Convert.ToInt32(valueText);
				break;
			case 10:
				this.StatModifier_bonus[2] = Convert.ToInt32(valueText);
				break;
			case 11:
				this.StatModifier_bonus[3] = Convert.ToInt32(valueText);
				break;
			case 12:
				this.StatModifier_bonus[4] = Convert.ToInt32(valueText);
				break;
			case 13:
				this.StatModifier_bonus[5] = Convert.ToInt32(valueText);
				break;
			case 14:
				this.StatModifier_bonus[6] = Convert.ToInt32(valueText);
				break;
			case 15:
				this.StatModifier_bonus[7] = Convert.ToInt32(valueText);
				break;
			case 16:
				this.StatModifier_bonus[8] = Convert.ToInt32(valueText);
				break;
			case 17:
				this.StatModifier_bonus[9] = Convert.ToInt32(valueText);
				break;
			case 18:
				this.StatModifier_bonusStat[0] = Convert.ToInt32(valueText);
				break;
			case 19:
				this.StatModifier_bonusStat[1] = Convert.ToInt32(valueText);
				break;
			case 20:
				this.StatModifier_bonusStat[2] = Convert.ToInt32(valueText);
				break;
			case 21:
				this.StatModifier_bonusStat[3] = Convert.ToInt32(valueText);
				break;
			case 22:
				this.StatModifier_bonusStat[4] = Convert.ToInt32(valueText);
				break;
			case 23:
				this.StatModifier_bonusStat[5] = Convert.ToInt32(valueText);
				break;
			case 24:
				this.StatModifier_bonusStat[6] = Convert.ToInt32(valueText);
				break;
			case 25:
				this.StatModifier_bonusStat[7] = Convert.ToInt32(valueText);
				break;
			case 26:
				this.StatModifier_bonusStat[8] = Convert.ToInt32(valueText);
				break;
			case 27:
				this.StatModifier_bonusStat[9] = Convert.ToInt32(valueText);
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
