using System;

namespace WowStaticData
{
	public class CharShipmentContainerRec : MODBRec
	{
		public int ID { get; private set; }

		public byte BaseCapacity { get; private set; }

		public string PendingText { get; private set; }

		public string Description { get; private set; }

		protected override void DeserializeIndex(int index, string valueText)
		{
			switch (index)
			{
			case 0:
				this.ID = Convert.ToInt32(valueText);
				break;
			case 1:
				this.BaseCapacity = Convert.ToByte(valueText);
				break;
			case 2:
				this.PendingText = valueText;
				break;
			case 3:
				this.Description = valueText;
				break;
			}
		}
	}
}
