using System;

namespace WowStaticData
{
	public class GarrFollowerLevelXPRec : MODBRec
	{
		public int ID { get; private set; }

		public byte FollowerLevel { get; private set; }

		public ushort XpToNextLevel { get; private set; }

		public ushort ShipmentXP { get; private set; }

		public byte GarrFollowerTypeID { get; private set; }

		protected override void DeserializeIndex(int index, string valueText)
		{
			switch (index)
			{
			case 0:
				this.ID = Convert.ToInt32(valueText);
				break;
			case 1:
				this.FollowerLevel = Convert.ToByte(valueText);
				break;
			case 2:
				this.XpToNextLevel = Convert.ToUInt16(valueText);
				break;
			case 3:
				this.ShipmentXP = Convert.ToUInt16(valueText);
				break;
			case 4:
				this.GarrFollowerTypeID = Convert.ToByte(valueText);
				break;
			}
		}
	}
}
