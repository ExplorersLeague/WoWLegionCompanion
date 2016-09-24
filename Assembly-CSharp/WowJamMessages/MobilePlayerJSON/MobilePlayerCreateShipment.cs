using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobilePlayerJSON
{
	[FlexJamMessage(Id = 4791, Name = "MobilePlayerCreateShipment", Version = 28333852u)]
	[System.Runtime.Serialization.DataContract]
	public class MobilePlayerCreateShipment
	{
		[System.Runtime.Serialization.DataMember(Name = "charShipmentID")]
		[FlexJamMember(Name = "charShipmentID", Type = FlexJamType.Int32)]
		public int CharShipmentID { get; set; }

		[FlexJamMember(Name = "numShipments", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "numShipments")]
		public int NumShipments { get; set; }
	}
}
