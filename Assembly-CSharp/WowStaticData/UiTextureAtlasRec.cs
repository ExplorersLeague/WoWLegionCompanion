using System;

namespace WowStaticData
{
	public class UiTextureAtlasRec : MODBRec
	{
		public int ID { get; private set; }

		public int FileDataID { get; private set; }

		public ushort AtlasHeight { get; private set; }

		public ushort AtlasWidth { get; private set; }

		protected override void DeserializeIndex(int index, string valueText)
		{
			switch (index)
			{
			case 0:
				this.ID = Convert.ToInt32(valueText);
				break;
			case 1:
				this.FileDataID = Convert.ToInt32(valueText);
				break;
			case 2:
				this.AtlasHeight = Convert.ToUInt16(valueText);
				break;
			case 3:
				this.AtlasWidth = Convert.ToUInt16(valueText);
				break;
			}
		}
	}
}
