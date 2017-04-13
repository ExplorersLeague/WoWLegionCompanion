using System;

namespace WowStaticData
{
	public class GarrFollowerRec
	{
		public int ID { get; private set; }

		public uint GarrFollowerTypeID { get; private set; }

		public int HordeCreatureID { get; private set; }

		public int AllianceCreatureID { get; private set; }

		public uint HordeGarrFollRaceID { get; private set; }

		public uint AllianceGarrFollRaceID { get; private set; }

		public uint Quality { get; private set; }

		public uint HordeGarrClassSpecID { get; private set; }

		public uint AllianceGarrClassSpecID { get; private set; }

		public uint HordeGarrFollItemSetID { get; private set; }

		public uint AllianceGarrFollItemSetID { get; private set; }

		public uint FollowerLevel { get; private set; }

		public uint ItemLevelWeapon { get; private set; }

		public uint ItemLevelArmor { get; private set; }

		public uint Gender { get; private set; }

		public uint Flags { get; private set; }

		public string HordeSourceText { get; private set; }

		public string AllianceSourceText { get; private set; }

		public int HordeSourceTypeEnum { get; private set; }

		public int AllianceSourceTypeEnum { get; private set; }

		public int HordeIconFileDataID { get; private set; }

		public int AllianceIconFileDataID { get; private set; }

		public uint HordeUITextureKitID { get; private set; }

		public uint AllianceUITextureKitID { get; private set; }

		public uint GarrTypeID { get; private set; }

		public int Vitality { get; private set; }

		public int ChrClassID { get; private set; }

		public int HordeFlavorGarrStringID { get; private set; }

		public int AllianceFlavorGarrStringID { get; private set; }

		public string TitleName { get; private set; }

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
				this.GarrFollowerTypeID = Convert.ToUInt32(valueText);
				break;
			case 2:
				this.HordeCreatureID = Convert.ToInt32(valueText);
				break;
			case 3:
				this.AllianceCreatureID = Convert.ToInt32(valueText);
				break;
			case 4:
				this.HordeGarrFollRaceID = Convert.ToUInt32(valueText);
				break;
			case 5:
				this.AllianceGarrFollRaceID = Convert.ToUInt32(valueText);
				break;
			case 6:
				this.Quality = Convert.ToUInt32(valueText);
				break;
			case 7:
				this.HordeGarrClassSpecID = Convert.ToUInt32(valueText);
				break;
			case 8:
				this.AllianceGarrClassSpecID = Convert.ToUInt32(valueText);
				break;
			case 9:
				this.HordeGarrFollItemSetID = Convert.ToUInt32(valueText);
				break;
			case 10:
				this.AllianceGarrFollItemSetID = Convert.ToUInt32(valueText);
				break;
			case 11:
				this.FollowerLevel = Convert.ToUInt32(valueText);
				break;
			case 12:
				this.ItemLevelWeapon = Convert.ToUInt32(valueText);
				break;
			case 13:
				this.ItemLevelArmor = Convert.ToUInt32(valueText);
				break;
			case 14:
				this.Gender = Convert.ToUInt32(valueText);
				break;
			case 15:
				this.Flags = Convert.ToUInt32(valueText);
				break;
			case 16:
				this.HordeSourceText = valueText;
				break;
			case 17:
				this.AllianceSourceText = valueText;
				break;
			case 18:
				this.HordeSourceTypeEnum = Convert.ToInt32(valueText);
				break;
			case 19:
				this.AllianceSourceTypeEnum = Convert.ToInt32(valueText);
				break;
			case 20:
				this.HordeIconFileDataID = Convert.ToInt32(valueText);
				break;
			case 21:
				this.AllianceIconFileDataID = Convert.ToInt32(valueText);
				break;
			case 22:
				this.HordeUITextureKitID = Convert.ToUInt32(valueText);
				break;
			case 23:
				this.AllianceUITextureKitID = Convert.ToUInt32(valueText);
				break;
			case 24:
				this.GarrTypeID = Convert.ToUInt32(valueText);
				break;
			case 25:
				this.Vitality = Convert.ToInt32(valueText);
				break;
			case 26:
				this.ChrClassID = Convert.ToInt32(valueText);
				break;
			case 27:
				this.HordeFlavorGarrStringID = Convert.ToInt32(valueText);
				break;
			case 28:
				this.AllianceFlavorGarrStringID = Convert.ToInt32(valueText);
				break;
			case 29:
				this.TitleName = valueText;
				break;
			}
		}
	}
}
