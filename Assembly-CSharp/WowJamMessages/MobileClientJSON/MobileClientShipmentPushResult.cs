using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobileClientJSON
{
	[System.Runtime.Serialization.DataContract]
	[FlexJamMessage(Id = 4864, Name = "MobileClientShipmentPushResult", Version = 39869590u)]
	public class MobileClientShipmentPushResult
	{
		[System.Runtime.Serialization.DataMember(Name = "charShipmentID")]
		[FlexJamMember(Name = "charShipmentID", Type = FlexJamType.Int32)]
		public int CharShipmentID { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "items")]
		[FlexJamMember(ArrayDimensions = 1, Name = "items", Type = FlexJamType.Struct)]
		public MobileClientShipmentItem[] Items { get; set; }
	}
}
