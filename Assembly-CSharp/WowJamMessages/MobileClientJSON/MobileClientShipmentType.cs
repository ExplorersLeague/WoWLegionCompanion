using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobileClientJSON
{
	[System.Runtime.Serialization.DataContract]
	[FlexJamStruct(Name = "MobileClientShipmentType", Version = 28333852u)]
	public class MobileClientShipmentType
	{
		[System.Runtime.Serialization.DataMember(Name = "currencyTypeID")]
		[FlexJamMember(Name = "currencyTypeID", Type = FlexJamType.Int32)]
		public int CurrencyTypeID { get; set; }

		[FlexJamMember(Name = "charShipmentID", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "charShipmentID")]
		public int CharShipmentID { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "currencyCost")]
		[FlexJamMember(Name = "currencyCost", Type = FlexJamType.Int32)]
		public int CurrencyCost { get; set; }
	}
}
