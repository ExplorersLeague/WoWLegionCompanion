using System;

namespace WowStaticData
{
	public class CurrencyTypesRec : MODBRec
	{
		public int ID { get; private set; }

		public uint Flags { get; private set; }

		public string Name { get; private set; }

		public string Description { get; private set; }

		public uint FactionID { get; private set; }

		protected override void DeserializeIndex(int index, string valueText)
		{
			switch (index)
			{
			case 0:
				this.ID = Convert.ToInt32(valueText);
				break;
			case 1:
				this.Flags = Convert.ToUInt32(valueText);
				break;
			case 2:
				this.Name = valueText;
				break;
			case 3:
				this.Description = valueText;
				break;
			case 4:
				this.FactionID = Convert.ToUInt32(valueText);
				break;
			}
		}
	}
}
