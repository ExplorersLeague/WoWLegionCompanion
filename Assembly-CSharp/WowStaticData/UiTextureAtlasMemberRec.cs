using System;

namespace WowStaticData
{
	public class UiTextureAtlasMemberRec : MODBRec
	{
		public int ID { get; private set; }

		public string CommittedName { get; private set; }

		public ushort UiTextureAtlasID { get; private set; }

		public ushort CommittedLeft { get; private set; }

		public ushort CommittedRight { get; private set; }

		public ushort CommittedTop { get; private set; }

		public ushort CommittedBottom { get; private set; }

		public byte CommittedFlags { get; private set; }

		public byte Priority { get; private set; }

		protected override void DeserializeIndex(int index, string valueText)
		{
			switch (index)
			{
			case 0:
				this.ID = Convert.ToInt32(valueText);
				break;
			case 1:
				this.CommittedName = valueText;
				break;
			case 2:
				this.UiTextureAtlasID = Convert.ToUInt16(valueText);
				break;
			case 3:
				this.CommittedLeft = Convert.ToUInt16(valueText);
				break;
			case 4:
				this.CommittedRight = Convert.ToUInt16(valueText);
				break;
			case 5:
				this.CommittedTop = Convert.ToUInt16(valueText);
				break;
			case 6:
				this.CommittedBottom = Convert.ToUInt16(valueText);
				break;
			case 7:
				this.CommittedFlags = Convert.ToByte(valueText);
				break;
			case 8:
				this.Priority = Convert.ToByte(valueText);
				break;
			}
		}
	}
}
