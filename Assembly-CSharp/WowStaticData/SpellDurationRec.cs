using System;

namespace WowStaticData
{
	public class SpellDurationRec : MODBRec
	{
		public int ID { get; private set; }

		public int Duration { get; private set; }

		public ushort DurationPerLevel { get; private set; }

		public int MaxDuration { get; private set; }

		protected override void DeserializeIndex(int index, string valueText)
		{
			switch (index)
			{
			case 0:
				this.ID = Convert.ToInt32(valueText);
				break;
			case 1:
				this.Duration = Convert.ToInt32(valueText);
				break;
			case 2:
				this.DurationPerLevel = Convert.ToUInt16(valueText);
				break;
			case 3:
				this.MaxDuration = Convert.ToInt32(valueText);
				break;
			}
		}
	}
}
