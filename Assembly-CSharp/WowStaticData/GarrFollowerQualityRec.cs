using System;

namespace WowStaticData
{
	public class GarrFollowerQualityRec : MODBRec
	{
		public int ID { get; private set; }

		public byte Quality { get; private set; }

		public uint XpToNextQuality { get; private set; }

		public ushort ShipmentXP { get; private set; }

		public byte GarrFollowerTypeID { get; private set; }

		public int ClassSpecID { get; private set; }

		protected override void DeserializeIndex(int index, string valueText)
		{
			switch (index)
			{
			case 0:
				this.ID = Convert.ToInt32(valueText);
				break;
			case 1:
				this.Quality = Convert.ToByte(valueText);
				break;
			case 2:
				this.XpToNextQuality = Convert.ToUInt32(valueText);
				break;
			case 3:
				this.ShipmentXP = Convert.ToUInt16(valueText);
				break;
			case 4:
				this.GarrFollowerTypeID = Convert.ToByte(valueText);
				break;
			case 5:
				this.ClassSpecID = Convert.ToInt32(valueText);
				break;
			}
		}
	}
}
