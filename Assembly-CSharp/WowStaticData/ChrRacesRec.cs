using System;

namespace WowStaticData
{
	public class ChrRacesRec : MODBRec
	{
		public int ID { get; private set; }

		public int Flags { get; private set; }

		public int FactionID { get; private set; }

		protected override void DeserializeIndex(int index, string valueText)
		{
			if (index != 0)
			{
				if (index != 1)
				{
					if (index == 2)
					{
						this.FactionID = Convert.ToInt32(valueText);
					}
				}
				else
				{
					this.Flags = Convert.ToInt32(valueText);
				}
			}
			else
			{
				this.ID = Convert.ToInt32(valueText);
			}
		}
	}
}
