using System;

namespace WowStaticData
{
	public class GarrTalentTreeRec : MODBRec
	{
		public int ID { get; private set; }

		public int ClassID { get; private set; }

		public sbyte MaxTiers { get; private set; }

		public sbyte UiOrder { get; private set; }

		public int GarrTypeID { get; private set; }

		protected override void DeserializeIndex(int index, string valueText)
		{
			switch (index)
			{
			case 0:
				this.ID = Convert.ToInt32(valueText);
				break;
			case 1:
				this.ClassID = Convert.ToInt32(valueText);
				break;
			case 2:
				this.MaxTiers = Convert.ToSByte(valueText);
				break;
			case 3:
				this.UiOrder = Convert.ToSByte(valueText);
				break;
			case 4:
				this.GarrTypeID = Convert.ToInt32(valueText);
				break;
			}
		}
	}
}
