using System;

namespace WowStaticData
{
	public class ChrClassesRec : MODBRec
	{
		public int ID { get; private set; }

		public string Name { get; private set; }

		public int IconFileDataID { get; private set; }

		public string Filename { get; private set; }

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
				this.IconFileDataID = Convert.ToInt32(valueText);
				break;
			case 3:
				this.Filename = valueText;
				break;
			}
		}
	}
}
