using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobileClientJSON
{
	[FlexJamStruct(Name = "MobileClientShipmentType", Version = 39869590u)]
	[System.Runtime.Serialization.DataContract]
	public class MobileClientShipmentType
	{
		[System.Runtime.Serialization.DataMember(Name = "canOrder")]
		[FlexJamMember(Name = "canOrder", Type = FlexJamType.Bool)]
		public bool CanOrder { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "charShipmentID")]
		[FlexJamMember(Name = "charShipmentID", Type = FlexJamType.Int32)]
		public int CharShipmentID { get; set; }

		[FlexJamMember(Name = "canPickup", Type = FlexJamType.Bool)]
		[System.Runtime.Serialization.DataMember(Name = "canPickup")]
		public bool CanPickup { get; set; }

		[FlexJamMember(Name = "currencyTypeID", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "currencyTypeID")]
		public int CurrencyTypeID { get; set; }

		[FlexJamMember(Name = "currencyCost", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "currencyCost")]
		public int CurrencyCost { get; set; }
	}
}
