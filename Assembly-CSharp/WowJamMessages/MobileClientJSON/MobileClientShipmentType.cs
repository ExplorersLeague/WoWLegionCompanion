using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobileClientJSON
{
	[System.Runtime.Serialization.DataContract]
	[FlexJamStruct(Name = "MobileClientShipmentType", Version = 28333852u)]
	public class MobileClientShipmentType
	{
		[FlexJamMember(Name = "currencyTypeID", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "currencyTypeID")]
		public int CurrencyTypeID { get; set; }

		[FlexJamMember(Name = "charShipmentID", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "charShipmentID")]
		public int CharShipmentID { get; set; }

		[FlexJamMember(Name = "currencyCost", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "currencyCost")]
		public int CurrencyCost { get; set; }
	}
}
