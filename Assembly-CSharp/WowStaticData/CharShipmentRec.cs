using System;

namespace WowStaticData
{
	public class CharShipmentRec : MODBRec
	{
		public int ID { get; private set; }

		public byte ContainerID { get; private set; }

		public ushort GarrFollowerID { get; private set; }

		public byte MaxShipments { get; private set; }

		public uint Duration { get; private set; }

		public int DummyItemID { get; private set; }

		public uint Flags { get; private set; }

		protected override void DeserializeIndex(int index, string valueText)
		{
			switch (index)
			{
			case 0:
				this.ID = Convert.ToInt32(valueText);
				break;
			case 1:
				this.ContainerID = Convert.ToByte(valueText);
				break;
			case 2:
				this.GarrFollowerID = Convert.ToUInt16(valueText);
				break;
			case 3:
				this.MaxShipments = Convert.ToByte(valueText);
				break;
			case 4:
				this.Duration = Convert.ToUInt32(valueText);
				break;
			case 5:
				this.DummyItemID = Convert.ToInt32(valueText);
				break;
			case 6:
				this.Flags = Convert.ToUInt32(valueText);
				break;
			}
		}
	}
}
