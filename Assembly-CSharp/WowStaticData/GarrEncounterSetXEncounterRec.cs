using System;

namespace WowStaticData
{
	public class GarrEncounterSetXEncounterRec : MODBRec
	{
		public int ID { get; private set; }

		public int GarrEncounterSetID { get; private set; }

		public int GarrEncounterID { get; private set; }

		protected override void DeserializeIndex(int index, string valueText)
		{
			if (index != 0)
			{
				if (index != 1)
				{
					if (index == 2)
					{
						this.GarrEncounterID = Convert.ToInt32(valueText);
					}
				}
				else
				{
					this.GarrEncounterSetID = Convert.ToInt32(valueText);
				}
			}
			else
			{
				this.ID = Convert.ToInt32(valueText);
			}
		}
	}
}
