using System;
using System.Globalization;

namespace WowStaticData
{
	public class GarrMissionRec : MODBRec
	{
		public int ID { get; private set; }

		public sbyte TargetLevel { get; private set; }

		public ushort TargetItemLevel { get; private set; }

		public byte EnvGarrMechanicTypeID { get; private set; }

		public byte MaxFollowers { get; private set; }

		public byte TravelDuration { get; private set; }

		public int MissionDuration { get; private set; }

		public uint OfferDuration { get; private set; }

		public ushort UiTextureKitID { get; private set; }

		public byte OfferedGarrMissionTextureID { get; private set; }

		public byte GarrMissionTypeID { get; private set; }

		public byte GarrFollowerTypeID { get; private set; }

		public string Name { get; private set; }

		public string Description { get; private set; }

		public string Location { get; private set; }

		public uint PlayerConditionID { get; private set; }

		public int GarrMissionSetID { get; private set; }

		public uint MissionCost { get; private set; }

		public ushort MissionCostCurrencyTypesID { get; private set; }

		public uint Flags { get; private set; }

		public uint BaseFollowerXP { get; private set; }

		public byte BaseCompletionChance { get; private set; }

		public byte FollowerDeathChance { get; private set; }

		public byte GarrTypeID { get; private set; }

		public float Mappos_x { get; private set; }

		public float Mappos_y { get; private set; }

		public int PrevGarrMissionID { get; private set; }

		public int AreaID { get; private set; }

		public int OvermaxRewardPackID { get; private set; }

		public int EnvGarrMechanicID { get; private set; }

		protected override void DeserializeIndex(int index, string valueText)
		{
			switch (index)
			{
			case 0:
				this.ID = Convert.ToInt32(valueText);
				break;
			case 1:
				this.TargetLevel = Convert.ToSByte(valueText);
				break;
			case 2:
				this.TargetItemLevel = Convert.ToUInt16(valueText);
				break;
			case 3:
				this.EnvGarrMechanicTypeID = Convert.ToByte(valueText);
				break;
			case 4:
				this.MaxFollowers = Convert.ToByte(valueText);
				break;
			case 5:
				this.TravelDuration = Convert.ToByte(valueText);
				break;
			case 6:
				this.MissionDuration = Convert.ToInt32(valueText);
				break;
			case 7:
				this.OfferDuration = Convert.ToUInt32(valueText);
				break;
			case 8:
				this.UiTextureKitID = Convert.ToUInt16(valueText);
				break;
			case 9:
				this.OfferedGarrMissionTextureID = Convert.ToByte(valueText);
				break;
			case 10:
				this.GarrMissionTypeID = Convert.ToByte(valueText);
				break;
			case 11:
				this.GarrFollowerTypeID = Convert.ToByte(valueText);
				break;
			case 12:
				this.Name = valueText;
				break;
			case 13:
				this.Description = valueText;
				break;
			case 14:
				this.Location = valueText;
				break;
			case 15:
				this.PlayerConditionID = Convert.ToUInt32(valueText);
				break;
			case 16:
				this.GarrMissionSetID = Convert.ToInt32(valueText);
				break;
			case 17:
				this.MissionCost = Convert.ToUInt32(valueText);
				break;
			case 18:
				this.MissionCostCurrencyTypesID = Convert.ToUInt16(valueText);
				break;
			case 19:
				this.Flags = Convert.ToUInt32(valueText);
				break;
			case 20:
				this.BaseFollowerXP = Convert.ToUInt32(valueText);
				break;
			case 21:
				this.BaseCompletionChance = Convert.ToByte(valueText);
				break;
			case 22:
				this.FollowerDeathChance = Convert.ToByte(valueText);
				break;
			case 23:
				this.GarrTypeID = Convert.ToByte(valueText);
				break;
			case 24:
				this.Mappos_x = (float)Convert.ToDouble(valueText, CultureInfo.InvariantCulture);
				break;
			case 25:
				this.Mappos_y = (float)Convert.ToDouble(valueText, CultureInfo.InvariantCulture);
				break;
			case 26:
				this.PrevGarrMissionID = Convert.ToInt32(valueText);
				break;
			case 27:
				this.AreaID = Convert.ToInt32(valueText);
				break;
			case 28:
				this.OvermaxRewardPackID = Convert.ToInt32(valueText);
				break;
			case 29:
				this.EnvGarrMechanicID = Convert.ToInt32(valueText);
				break;
			}
		}
	}
}
