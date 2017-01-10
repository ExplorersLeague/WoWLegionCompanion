using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobileClientJSON
{
	[System.Runtime.Serialization.DataContract]
	[FlexJamMessage(Id = 4854, Name = "MobileClientShipmentPushResult", Version = 33577221u)]
	public class MobileClientShipmentPushResult
	{
		[System.Runtime.Serialization.DataMember(Name = "charShipmentID")]
		[FlexJamMember(Name = "charShipmentID", Type = FlexJamType.Int32)]
		public int CharShipmentID { get; set; }

		[FlexJamMember(ArrayDimensions = 1, Name = "items", Type = FlexJamType.Struct)]
		[System.Runtime.Serialization.DataMember(Name = "items")]
		public MobileClientShipmentItem[] Items { get; set; }
	}
}
