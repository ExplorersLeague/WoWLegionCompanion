using System;

namespace WowStaticData
{
	public class GarrMechanicTypeRec : MODBRec
	{
		public int ID { get; private set; }

		public byte Category { get; private set; }

		public string Name { get; private set; }

		public string Description { get; private set; }

		public int IconFileDataID { get; private set; }

		protected override void DeserializeIndex(int index, string valueText)
		{
			switch (index)
			{
			case 0:
				this.ID = Convert.ToInt32(valueText);
				break;
			case 1:
				this.Category = Convert.ToByte(valueText);
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
			}
		}
	}
}
