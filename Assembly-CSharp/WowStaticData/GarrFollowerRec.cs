using System;

namespace WowStaticData
{
	public class GarrFollowerRec : MODBRec
	{
		public int ID { get; private set; }

		public byte GarrFollowerTypeID { get; private set; }

		public int HordeCreatureID { get; private set; }

		public int AllianceCreatureID { get; private set; }

		public byte HordeGarrFollRaceID { get; private set; }

		public byte AllianceGarrFollRaceID { get; private set; }

		public byte Quality { get; private set; }

		public byte HordeGarrClassSpecID { get; private set; }

		public byte AllianceGarrClassSpecID { get; private set; }

		public ushort HordeGarrFollItemSetID { get; private set; }

		public ushort AllianceGarrFollItemSetID { get; private set; }

		public byte FollowerLevel { get; private set; }

		public ushort ItemLevelWeapon { get; private set; }

		public ushort ItemLevelArmor { get; private set; }

		public byte Gender { get; private set; }

		public byte Flags { get; private set; }

		public string HordeSourceText { get; private set; }

		public string AllianceSourceText { get; private set; }

		public sbyte HordeSourceTypeEnum { get; private set; }

		public sbyte AllianceSourceTypeEnum { get; private set; }

		public int HordeIconFileDataID { get; private set; }

		public int AllianceIconFileDataID { get; private set; }

		public ushort HordeUITextureKitID { get; private set; }

		public ushort AllianceUITextureKitID { get; private set; }

		public byte GarrTypeID { get; private set; }

		public byte Vitality { get; private set; }

		public byte ChrClassID { get; private set; }

		public byte HordeFlavorGarrStringID { get; private set; }

		public byte AllianceFlavorGarrStringID { get; private set; }

		public string TitleName { get; private set; }

		protected override void DeserializeIndex(int index, string valueText)
		{
			switch (index)
			{
			case 0:
				this.ID = Convert.ToInt32(valueText);
				break;
			case 1:
				this.GarrFollowerTypeID = Convert.ToByte(valueText);
				break;
			case 2:
				this.HordeCreatureID = Convert.ToInt32(valueText);
				break;
			case 3:
				this.AllianceCreatureID = Convert.ToInt32(valueText);
				break;
			case 4:
				this.HordeGarrFollRaceID = Convert.ToByte(valueText);
				break;
			case 5:
				this.AllianceGarrFollRaceID = Convert.ToByte(valueText);
				break;
			case 6:
				this.Quality = Convert.ToByte(valueText);
				break;
			case 7:
				this.HordeGarrClassSpecID = Convert.ToByte(valueText);
				break;
			case 8:
				this.AllianceGarrClassSpecID = Convert.ToByte(valueText);
				break;
			case 9:
				this.HordeGarrFollItemSetID = Convert.ToUInt16(valueText);
				break;
			case 10:
				this.AllianceGarrFollItemSetID = Convert.ToUInt16(valueText);
				break;
			case 11:
				this.FollowerLevel = Convert.ToByte(valueText);
				break;
			case 12:
				this.ItemLevelWeapon = Convert.ToUInt16(valueText);
				break;
			case 13:
				this.ItemLevelArmor = Convert.ToUInt16(valueText);
				break;
			case 14:
				this.Gender = Convert.ToByte(valueText);
				break;
			case 15:
				this.Flags = Convert.ToByte(valueText);
				break;
			case 16:
				this.HordeSourceText = valueText;
				break;
			case 17:
				this.AllianceSourceText = valueText;
				break;
			case 18:
				this.HordeSourceTypeEnum = Convert.ToSByte(valueText);
				break;
			case 19:
				this.AllianceSourceTypeEnum = Convert.ToSByte(valueText);
				break;
			case 20:
				this.HordeIconFileDataID = Convert.ToInt32(valueText);
				break;
			case 21:
				this.AllianceIconFileDataID = Convert.ToInt32(valueText);
				break;
			case 22:
				this.HordeUITextureKitID = Convert.ToUInt16(valueText);
				break;
			case 23:
				this.AllianceUITextureKitID = Convert.ToUInt16(valueText);
				break;
			case 24:
				this.GarrTypeID = Convert.ToByte(valueText);
				break;
			case 25:
				this.Vitality = Convert.ToByte(valueText);
				break;
			case 26:
				this.ChrClassID = Convert.ToByte(valueText);
				break;
			case 27:
				this.HordeFlavorGarrStringID = Convert.ToByte(valueText);
				break;
			case 28:
				this.AllianceFlavorGarrStringID = Convert.ToByte(valueText);
				break;
			case 29:
				this.TitleName = valueText;
				break;
			}
		}
	}
}
