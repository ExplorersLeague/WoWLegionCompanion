using System;

namespace WowStaticData
{
	public class GarrMissionRec
	{
		public int ID { get; private set; }

		public int TargetLevel { get; private set; }

		public uint TargetItemLevel { get; private set; }

		public uint EnvGarrMechanicTypeID { get; private set; }

		public uint MaxFollowers { get; private set; }

		public uint TravelDuration { get; private set; }

		public int MissionDuration { get; private set; }

		public uint OfferDuration { get; private set; }

		public uint UiTextureKitID { get; private set; }

		public uint OfferedGarrMissionTextureID { get; private set; }

		public uint GarrMissionTypeID { get; private set; }

		public uint GarrFollowerTypeID { get; private set; }

		public string Name { get; private set; }

		public string Description { get; private set; }

		public string Location { get; private set; }

		public uint PlayerConditionID { get; private set; }

		public uint GarrMissionSetID { get; private set; }

		public uint MissionCost { get; private set; }

		public uint MissionCostCurrencyTypesID { get; private set; }

		public uint Flags { get; private set; }

		public uint BaseFollowerXP { get; private set; }

		public uint BaseCompletionChance { get; private set; }

		public uint FollowerDeathChance { get; private set; }

		public uint GarrTypeID { get; private set; }

		public float Mappos_x { get; private set; }

		public float Mappos_y { get; private set; }

		public int PrevGarrMissionID { get; private set; }

		public int AreaID { get; private set; }

		public int OvermaxRewardPackID { get; private set; }

		public int EnvGarrMechanicID { get; private set; }

		public void Deserialize(string valueLine)
		{
			int num = 0;
			int num2 = 0;
			int num3;
			do
			{
				num3 = valueLine.IndexOf('\t', num);
				if (num3 >= 0)
				{
					string valueText = valueLine.Substring(num, num3 - num).Trim();
					this.DeserializeIndex(num2, valueText);
					num2++;
				}
				num = num3 + 1;
			}
			while (num3 > 0);
		}

		private void DeserializeIndex(int index, string valueText)
		{
			switch (index)
			{
			case 0:
				this.ID = Convert.ToInt32(valueText);
				break;
			case 1:
				this.TargetLevel = Convert.ToInt32(valueText);
				break;
			case 2:
				this.TargetItemLevel = Convert.ToUInt32(valueText);
				break;
			case 3:
				this.EnvGarrMechanicTypeID = Convert.ToUInt32(valueText);
				break;
			case 4:
				this.MaxFollowers = Convert.ToUInt32(valueText);
				break;
			case 5:
				this.TravelDuration = Convert.ToUInt32(valueText);
				break;
			case 6:
				this.MissionDuration = Convert.ToInt32(valueText);
				break;
			case 7:
				this.OfferDuration = Convert.ToUInt32(valueText);
				break;
			case 8:
				this.UiTextureKitID = Convert.ToUInt32(valueText);
				break;
			case 9:
				this.OfferedGarrMissionTextureID = Convert.ToUInt32(valueText);
				break;
			case 10:
				this.GarrMissionTypeID = Convert.ToUInt32(valueText);
				break;
			case 11:
				this.GarrFollowerTypeID = Convert.ToUInt32(valueText);
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
				this.GarrMissionSetID = Convert.ToUInt32(valueText);
				break;
			case 17:
				this.MissionCost = Convert.ToUInt32(valueText);
				break;
			case 18:
				this.MissionCostCurrencyTypesID = Convert.ToUInt32(valueText);
				break;
			case 19:
				this.Flags = Convert.ToUInt32(valueText);
				break;
			case 20:
				this.BaseFollowerXP = Convert.ToUInt32(valueText);
				break;
			case 21:
				this.BaseCompletionChance = Convert.ToUInt32(valueText);
				break;
			case 22:
				this.FollowerDeathChance = Convert.ToUInt32(valueText);
				break;
			case 23:
				this.GarrTypeID = Convert.ToUInt32(valueText);
				break;
			case 24:
				this.Mappos_x = (float)Convert.ToDouble(valueText);
				break;
			case 25:
				this.Mappos_y = (float)Convert.ToDouble(valueText);
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
