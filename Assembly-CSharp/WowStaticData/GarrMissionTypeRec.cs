using System;

namespace WowStaticData
{
	public class GarrMissionTypeRec : MODBRec
	{
		public int ID { get; private set; }

		public string Name { get; private set; }

		public ushort UiTextureAtlasMemberID { get; private set; }

		public byte UiTextureKitID { get; private set; }

		protected override void DeserializeIndex(int index, string valueText)
		{
			switch (index)
			{
			case 0:
				this.ID = Convert.ToInt32(valueText);
				break;
			case 1:
				this.Name = valueText;
				break;
			case 2:
				this.UiTextureAtlasMemberID = Convert.ToUInt16(valueText);
				break;
			case 3:
				this.UiTextureKitID = Convert.ToByte(valueText);
				break;
			}
		}
	}
}
