using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobilePlayerJSON
{
	[System.Runtime.Serialization.DataContract]
	[FlexJamMessage(Id = 4791, Name = "MobilePlayerCreateShipment", Version = 33577221u)]
	public class MobilePlayerCreateShipment
	{
		[FlexJamMember(Name = "charShipmentID", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "charShipmentID")]
		public int CharShipmentID { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "numShipments")]
		[FlexJamMember(Name = "numShipments", Type = FlexJamType.Int32)]
		public int NumShipments { get; set; }
	}
}
