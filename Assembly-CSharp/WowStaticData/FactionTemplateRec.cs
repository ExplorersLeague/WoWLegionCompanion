using System;

namespace WowStaticData
{
	public class FactionTemplateRec : MODBRec
	{
		public FactionTemplateRec()
		{
			this.Enemies = new int[4];
			this.Friend = new int[4];
		}

		public int ID { get; private set; }

		public int Faction { get; private set; }

		public int Flags { get; private set; }

		public int FactionGroup { get; private set; }

		public int FriendGroup { get; private set; }

		public int EnemyGroup { get; private set; }

		public int[] Enemies { get; private set; }

		public int[] Friend { get; private set; }

		protected override void DeserializeIndex(int index, string valueText)
		{
			switch (index)
			{
			case 0:
				this.ID = Convert.ToInt32(valueText);
				break;
			case 1:
				this.Faction = Convert.ToInt32(valueText);
				break;
			case 2:
				this.Flags = Convert.ToInt32(valueText);
				break;
			case 3:
				this.FactionGroup = Convert.ToInt32(valueText);
				break;
			case 4:
				this.FriendGroup = Convert.ToInt32(valueText);
				break;
			case 5:
				this.EnemyGroup = Convert.ToInt32(valueText);
				break;
			case 6:
				this.Enemies[0] = Convert.ToInt32(valueText);
				break;
			case 7:
				this.Enemies[1] = Convert.ToInt32(valueText);
				break;
			case 8:
				this.Enemies[2] = Convert.ToInt32(valueText);
				break;
			case 9:
				this.Enemies[3] = Convert.ToInt32(valueText);
				break;
			case 10:
				this.Friend[0] = Convert.ToInt32(valueText);
				break;
			case 11:
				this.Friend[1] = Convert.ToInt32(valueText);
				break;
			case 12:
				this.Friend[2] = Convert.ToInt32(valueText);
				break;
			case 13:
				this.Friend[3] = Convert.ToInt32(valueText);
				break;
			}
		}
	}
}
