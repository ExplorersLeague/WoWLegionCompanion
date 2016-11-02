using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobileClientJSON
{
	[FlexJamMessage(Id = 4854, Name = "MobileClientShipmentPushResult", Version = 33577221u)]
	[System.Runtime.Serialization.DataContract]
	public class MobileClientShipmentPushResult
	{
		[FlexJamMember(Name = "charShipmentID", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "charShipmentID")]
		public int CharShipmentID { get; set; }

		[FlexJamMember(ArrayDimensions = 1, Name = "items", Type = FlexJamType.Struct)]
		[System.Runtime.Serialization.DataMember(Name = "items")]
		public MobileClientShipmentItem[] Items { get; set; }
	}
}
