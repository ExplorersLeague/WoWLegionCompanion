using System;
using System.Globalization;

namespace WowStaticData
{
	public class GarrEncounterRec : MODBRec
	{
		public int ID { get; private set; }

		public int CreatureID { get; private set; }

		public string Name { get; private set; }

		public float UiAnimScale { get; private set; }

		public float UiAnimHeight { get; private set; }

		public int PortraitFileDataID { get; private set; }

		public byte UiTextureKitID { get; private set; }

		protected override void DeserializeIndex(int index, string valueText)
		{
			switch (index)
			{
			case 0:
				this.ID = Convert.ToInt32(valueText);
				break;
			case 1:
				this.CreatureID = Convert.ToInt32(valueText);
				break;
			case 2:
				this.Name = valueText;
				break;
			case 3:
				this.UiAnimScale = (float)Convert.ToDouble(valueText, CultureInfo.InvariantCulture);
				break;
			case 4:
				this.UiAnimHeight = (float)Convert.ToDouble(valueText, CultureInfo.InvariantCulture);
				break;
			case 5:
				this.PortraitFileDataID = Convert.ToInt32(valueText);
				break;
			case 6:
				this.UiTextureKitID = Convert.ToByte(valueText);
				break;
			}
		}
	}
}
