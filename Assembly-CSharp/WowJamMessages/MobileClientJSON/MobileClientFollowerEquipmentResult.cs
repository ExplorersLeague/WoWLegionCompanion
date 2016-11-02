using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobileClientJSON
{
	[FlexJamMessage(Id = 4862, Name = "MobileClientFollowerEquipmentResult", Version = 33577221u)]
	[System.Runtime.Serialization.DataContract]
	public class MobileClientFollowerEquipmentResult
	{
		[System.Runtime.Serialization.DataMember(Name = "equipment")]
		[FlexJamMember(ArrayDimensions = 1, Name = "equipment", Type = FlexJamType.Struct)]
		public MobileFollowerEquipment[] Equipment { get; set; }
	}
}
