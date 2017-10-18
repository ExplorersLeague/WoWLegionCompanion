using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobilePlayerJSON
{
	[System.Runtime.Serialization.DataContract]
	[FlexJamMessage(Id = 4790, Name = "MobilePlayerCreateShipment", Version = 38820897u)]
	public class MobilePlayerCreateShipment
	{
		[System.Runtime.Serialization.DataMember(Name = "charShipmentID")]
		[FlexJamMember(Name = "charShipmentID", Type = FlexJamType.Int32)]
		public int CharShipmentID { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "numShipments")]
		[FlexJamMember(Name = "numShipments", Type = FlexJamType.Int32)]
		public int NumShipments { get; set; }
	}
}
