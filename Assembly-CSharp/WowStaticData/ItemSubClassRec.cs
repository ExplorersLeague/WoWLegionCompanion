using System;

namespace WowStaticData
{
	public class ItemSubClassRec : MODBRec
	{
		public int ID { get; private set; }

		public sbyte ClassID { get; private set; }

		public sbyte SubClassID { get; private set; }

		public short Flags { get; private set; }

		public sbyte DisplayFlags { get; private set; }

		public string DisplayName { get; private set; }

		protected override void DeserializeIndex(int index, string valueText)
		{
			switch (index)
			{
			case 0:
				this.ID = Convert.ToInt32(valueText);
				break;
			case 1:
				this.ClassID = Convert.ToSByte(valueText);
				break;
			case 2:
				this.SubClassID = Convert.ToSByte(valueText);
				break;
			case 3:
				this.Flags = Convert.ToInt16(valueText);
				break;
			case 4:
				this.DisplayFlags = Convert.ToSByte(valueText);
				break;
			case 5:
				this.DisplayName = valueText;
				break;
			}
		}
	}
}
