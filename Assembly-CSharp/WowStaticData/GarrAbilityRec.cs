using System;

namespace WowStaticData
{
	public class GarrAbilityRec : MODBRec
	{
		public int ID { get; private set; }

		public ushort Flags { get; private set; }

		public string Name { get; private set; }

		public string Description { get; private set; }

		public int IconFileDataID { get; private set; }

		public ushort FactionChangeGarrAbilityID { get; private set; }

		public byte GarrAbilityCategoryID { get; private set; }

		public byte GarrFollowerTypeID { get; private set; }

		protected override void DeserializeIndex(int index, string valueText)
		{
			switch (index)
			{
			case 0:
				this.ID = Convert.ToInt32(valueText);
				break;
			case 1:
				this.Flags = Convert.ToUInt16(valueText);
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
				this.FactionChangeGarrAbilityID = Convert.ToUInt16(valueText);
				break;
			case 6:
				this.GarrAbilityCategoryID = Convert.ToByte(valueText);
				break;
			case 7:
				this.GarrFollowerTypeID = Convert.ToByte(valueText);
				break;
			}
		}
	}
}
