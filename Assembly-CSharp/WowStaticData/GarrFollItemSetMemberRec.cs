using System;

namespace WowStaticData
{
	public class GarrFollItemSetMemberRec : MODBRec
	{
		public int ID { get; private set; }

		public ushort GarrFollItemSetID { get; private set; }

		public int ItemID { get; private set; }

		public byte ItemSlot { get; private set; }

		public ushort MinItemLevel { get; private set; }

		protected override void DeserializeIndex(int index, string valueText)
		{
			switch (index)
			{
			case 0:
				this.ID = Convert.ToInt32(valueText);
				break;
			case 1:
				this.GarrFollItemSetID = Convert.ToUInt16(valueText);
				break;
			case 2:
				this.ItemID = Convert.ToInt32(valueText);
				break;
			case 3:
				this.ItemSlot = Convert.ToByte(valueText);
				break;
			case 4:
				this.MinItemLevel = Convert.ToUInt16(valueText);
				break;
			}
		}
	}
}
