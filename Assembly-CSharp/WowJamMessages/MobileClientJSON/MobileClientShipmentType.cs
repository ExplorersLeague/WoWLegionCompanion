using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobileClientJSON
{
	[FlexJamStruct(Name = "MobileClientShipmentType", Version = 33577221u)]
	[System.Runtime.Serialization.DataContract]
	public class MobileClientShipmentType
	{
		[System.Runtime.Serialization.DataMember(Name = "currencyTypeID")]
		[FlexJamMember(Name = "currencyTypeID", Type = FlexJamType.Int32)]
		public int CurrencyTypeID { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "charShipmentID")]
		[FlexJamMember(Name = "charShipmentID", Type = FlexJamType.Int32)]
		public int CharShipmentID { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "currencyCost")]
		[FlexJamMember(Name = "currencyCost", Type = FlexJamType.Int32)]
		public int CurrencyCost { get; set; }
	}
}
