using System;
using System.Globalization;

namespace WowStaticData
{
	public class GarrAbilityEffectRec : MODBRec
	{
		public int ID { get; private set; }

		public byte AbilityAction { get; private set; }

		public ushort GarrAbilityID { get; private set; }

		public byte AbilityTargetType { get; private set; }

		public byte GarrMechanicTypeID { get; private set; }

		public byte Flags { get; private set; }

		public float CombatWeightBase { get; private set; }

		public float CombatWeightMax { get; private set; }

		public float ActionValueFlat { get; private set; }

		public byte ActionRace { get; private set; }

		public byte ActionHours { get; private set; }

		public int ActionRecordID { get; private set; }

		protected override void DeserializeIndex(int index, string valueText)
		{
			switch (index)
			{
			case 0:
				this.ID = Convert.ToInt32(valueText);
				break;
			case 1:
				this.AbilityAction = Convert.ToByte(valueText);
				break;
			case 2:
				this.GarrAbilityID = Convert.ToUInt16(valueText);
				break;
			case 3:
				this.AbilityTargetType = Convert.ToByte(valueText);
				break;
			case 4:
				this.GarrMechanicTypeID = Convert.ToByte(valueText);
				break;
			case 5:
				this.Flags = Convert.ToByte(valueText);
				break;
			case 6:
				this.CombatWeightBase = (float)Convert.ToDouble(valueText, CultureInfo.InvariantCulture);
				break;
			case 7:
				this.CombatWeightMax = (float)Convert.ToDouble(valueText, CultureInfo.InvariantCulture);
				break;
			case 8:
				this.ActionValueFlat = (float)Convert.ToDouble(valueText, CultureInfo.InvariantCulture);
				break;
			case 9:
				this.ActionRace = Convert.ToByte(valueText);
				break;
			case 10:
				this.ActionHours = Convert.ToByte(valueText);
				break;
			case 11:
				this.ActionRecordID = Convert.ToInt32(valueText);
				break;
			}
		}
	}
}
