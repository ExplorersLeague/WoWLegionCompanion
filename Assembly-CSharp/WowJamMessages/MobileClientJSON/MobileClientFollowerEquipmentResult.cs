using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobileClientJSON
{
	[System.Runtime.Serialization.DataContract]
	[FlexJamMessage(Id = 4872, Name = "MobileClientFollowerEquipmentResult", Version = 39869590u)]
	public class MobileClientFollowerEquipmentResult
	{
		[FlexJamMember(ArrayDimensions = 1, Name = "equipment", Type = FlexJamType.Struct)]
		[System.Runtime.Serialization.DataMember(Name = "equipment")]
		public MobileFollowerEquipment[] Equipment { get; set; }
	}
}
