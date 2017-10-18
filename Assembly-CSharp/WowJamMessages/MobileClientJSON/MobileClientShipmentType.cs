using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobileClientJSON
{
	[System.Runtime.Serialization.DataContract]
	[FlexJamStruct(Name = "MobileClientShipmentType", Version = 39869590u)]
	public class MobileClientShipmentType
	{
		[FlexJamMember(Name = "canOrder", Type = FlexJamType.Bool)]
		[System.Runtime.Serialization.DataMember(Name = "canOrder")]
		public bool CanOrder { get; set; }

		[FlexJamMember(Name = "charShipmentID", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "charShipmentID")]
		public int CharShipmentID { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "canPickup")]
		[FlexJamMember(Name = "canPickup", Type = FlexJamType.Bool)]
		public bool CanPickup { get; set; }

		[FlexJamMember(Name = "currencyTypeID", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "currencyTypeID")]
		public int CurrencyTypeID { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "currencyCost")]
		[FlexJamMember(Name = "currencyCost", Type = FlexJamType.Int32)]
		public int CurrencyCost { get; set; }
	}
}
