using System;

namespace WowStaticData
{
	public class GarrClassSpecRec : MODBRec
	{
		public int ID { get; private set; }

		public string ClassSpec { get; private set; }

		public string ClassSpec_Male { get; private set; }

		public string ClassSpec_Female { get; private set; }

		public ushort UiTextureAtlasMemberID { get; private set; }

		public ushort GarrFollItemSetID { get; private set; }

		public byte Flags { get; private set; }

		public byte FollowerClassLimit { get; private set; }

		protected override void DeserializeIndex(int index, string valueText)
		{
			switch (index)
			{
			case 0:
				this.ID = Convert.ToInt32(valueText);
				break;
			case 1:
				this.ClassSpec = valueText;
				break;
			case 2:
				this.ClassSpec_Male = valueText;
				break;
			case 3:
				this.ClassSpec_Female = valueText;
				break;
			case 4:
				this.UiTextureAtlasMemberID = Convert.ToUInt16(valueText);
				break;
			case 5:
				this.GarrFollItemSetID = Convert.ToUInt16(valueText);
				break;
			case 6:
				this.Flags = Convert.ToByte(valueText);
				break;
			case 7:
				this.FollowerClassLimit = Convert.ToByte(valueText);
				break;
			}
		}
	}
}
