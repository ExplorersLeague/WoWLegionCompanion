using System;

namespace WowStaticData
{
	public class CharShipmentRec
	{
		public int ID { get; private set; }

		public uint ContainerID { get; private set; }

		public uint GarrFollowerID { get; private set; }

		public uint MaxShipments { get; private set; }

		public uint Duration { get; private set; }

		public int DummyItemID { get; private set; }

		public uint Flags { get; private set; }

		public void Deserialize(string valueLine)
		{
			int num = 0;
			int num2 = 0;
			int num3;
			do
			{
				num3 = valueLine.IndexOf('\t', num);
				if (num3 >= 0)
				{
					string valueText = valueLine.Substring(num, num3 - num).Trim();
					this.DeserializeIndex(num2, valueText);
					num2++;
				}
				num = num3 + 1;
			}
			while (num3 > 0);
		}

		private void DeserializeIndex(int index, string valueText)
		{
			switch (index)
			{
			case 0:
				this.ID = Convert.ToInt32(valueText);
				break;
			case 1:
				this.ContainerID = Convert.ToUInt32(valueText);
				break;
			case 2:
				this.GarrFollowerID = Convert.ToUInt32(valueText);
				break;
			case 3:
				this.MaxShipments = Convert.ToUInt32(valueText);
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
