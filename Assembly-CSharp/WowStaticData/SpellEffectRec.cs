using System;

namespace WowStaticData
{
	public class SpellEffectRec : MODBRec
	{
		public int ID { get; private set; }

		public int SpellID { get; private set; }

		public int EffectIndex { get; private set; }

		public int Effect { get; private set; }

		public int EffectBasePoints { get; private set; }

		protected override void DeserializeIndex(int index, string valueText)
		{
			switch (index)
			{
			case 0:
				this.ID = Convert.ToInt32(valueText);
				break;
			case 1:
				this.SpellID = Convert.ToInt32(valueText);
				break;
			case 2:
				this.EffectIndex = Convert.ToInt32(valueText);
				break;
			case 3:
				this.Effect = Convert.ToInt32(valueText);
				break;
			case 4:
				this.EffectBasePoints = Convert.ToInt32(valueText);
				break;
			}
		}
	}
}
