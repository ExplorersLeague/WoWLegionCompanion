using System;

namespace WowStaticData
{
	public class GarrFollowerXAbilityRec : MODBRec
	{
		public int ID { get; private set; }

		public int GarrFollowerID { get; private set; }

		public int GarrAbilityID { get; private set; }

		public int FactionIndex { get; private set; }

		protected override void DeserializeIndex(int index, string valueText)
		{
			switch (index)
			{
			case 0:
				this.ID = Convert.ToInt32(valueText);
				break;
			case 1:
				this.GarrFollowerID = Convert.ToInt32(valueText);
				break;
			case 2:
				this.GarrAbilityID = Convert.ToInt32(valueText);
				break;
			case 3:
				this.FactionIndex = Convert.ToInt32(valueText);
				break;
			}
		}
	}
}
