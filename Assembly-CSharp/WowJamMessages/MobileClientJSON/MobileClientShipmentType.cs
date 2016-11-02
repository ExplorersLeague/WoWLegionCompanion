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

		[FlexJamMember(Name = "charShipmentID", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "charShipmentID")]
		public int CharShipmentID { get; set; }

		[FlexJamMember(Name = "currencyCost", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "currencyCost")]
		public int CurrencyCost { get; set; }
	}
}
