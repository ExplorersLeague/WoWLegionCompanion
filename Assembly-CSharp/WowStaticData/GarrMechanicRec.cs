using System;
using System.Globalization;

namespace WowStaticData
{
	public class GarrMechanicRec : MODBRec
	{
		public int ID { get; private set; }

		public byte GarrMechanicTypeID { get; private set; }

		public int GarrAbilityID { get; private set; }

		public float Factor { get; private set; }

		protected override void DeserializeIndex(int index, string valueText)
		{
			switch (index)
			{
			case 0:
				this.ID = Convert.ToInt32(valueText);
				break;
			case 1:
				this.GarrMechanicTypeID = Convert.ToByte(valueText);
				break;
			case 2:
				this.GarrAbilityID = Convert.ToInt32(valueText);
				break;
			case 3:
				this.Factor = (float)Convert.ToDouble(valueText, CultureInfo.InvariantCulture);
				break;
			}
		}
	}
}
