using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobileClientJSON
{
	[FlexJamMessage(Id = 4872, Name = "MobileClientFollowerEquipmentResult", Version = 39869590u)]
	[System.Runtime.Serialization.DataContract]
	public class MobileClientFollowerEquipmentResult
	{
		[FlexJamMember(ArrayDimensions = 1, Name = "equipment", Type = FlexJamType.Struct)]
		[System.Runtime.Serialization.DataMember(Name = "equipment")]
		public MobileFollowerEquipment[] Equipment { get; set; }
	}
}
