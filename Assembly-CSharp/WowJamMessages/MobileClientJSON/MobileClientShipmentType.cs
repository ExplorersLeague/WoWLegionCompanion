using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobileClientJSON
{
	[System.Runtime.Serialization.DataContract]
	[FlexJamStruct(Name = "MobileClientShipmentType", Version = 39869590u)]
	public class MobileClientShipmentType
	{
		[System.Runtime.Serialization.DataMember(Name = "canOrder")]
		[FlexJamMember(Name = "canOrder", Type = FlexJamType.Bool)]
		public bool CanOrder { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "charShipmentID")]
		[FlexJamMember(Name = "charShipmentID", Type = FlexJamType.Int32)]
		public int CharShipmentID { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "canPickup")]
		[FlexJamMember(Name = "canPickup", Type = FlexJamType.Bool)]
		public bool CanPickup { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "currencyTypeID")]
		[FlexJamMember(Name = "currencyTypeID", Type = FlexJamType.Int32)]
		public int CurrencyTypeID { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "currencyCost")]
		[FlexJamMember(Name = "currencyCost", Type = FlexJamType.Int32)]
		public int CurrencyCost { get; set; }
	}
}
