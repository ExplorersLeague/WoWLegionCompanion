using System;

namespace WowStaticData
{
	public class ItemEffectRec : MODBRec
	{
		public int ID { get; private set; }

		public int ParentItemID { get; private set; }

		public int SpellID { get; private set; }

		public sbyte TriggerType { get; private set; }

		protected override void DeserializeIndex(int index, string valueText)
		{
			switch (index)
			{
			case 0:
				this.ID = Convert.ToInt32(valueText);
				break;
			case 1:
				this.ParentItemID = Convert.ToInt32(valueText);
				break;
			case 2:
				this.SpellID = Convert.ToInt32(valueText);
				break;
			case 3:
				this.TriggerType = Convert.ToSByte(valueText);
				break;
			}
		}
	}
}
