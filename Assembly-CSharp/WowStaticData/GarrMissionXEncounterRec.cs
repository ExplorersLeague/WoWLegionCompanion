using System;

namespace WowStaticData
{
	public class GarrMissionXEncounterRec : MODBRec
	{
		public int ID { get; private set; }

		public ushort GarrMissionID { get; private set; }

		public ushort GarrEncounterID { get; private set; }

		public ushort GarrEncounterSetID { get; private set; }

		protected override void DeserializeIndex(int index, string valueText)
		{
			switch (index)
			{
			case 0:
				this.ID = Convert.ToInt32(valueText);
				break;
			case 1:
				this.GarrMissionID = Convert.ToUInt16(valueText);
				break;
			case 2:
				this.GarrEncounterID = Convert.ToUInt16(valueText);
				break;
			case 3:
				this.GarrEncounterSetID = Convert.ToUInt16(valueText);
				break;
			}
		}
	}
}
