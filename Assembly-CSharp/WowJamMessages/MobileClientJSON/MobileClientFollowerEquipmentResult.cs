using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobileClientJSON
{
	[System.Runtime.Serialization.DataContract]
	[FlexJamMessage(Id = 4872, Name = "MobileClientFollowerEquipmentResult", Version = 39869590u)]
	public class MobileClientFollowerEquipmentResult
	{
		[System.Runtime.Serialization.DataMember(Name = "equipment")]
		[FlexJamMember(ArrayDimensions = 1, Name = "equipment", Type = FlexJamType.Struct)]
		public MobileFollowerEquipment[] Equipment { get; set; }
	}
}
