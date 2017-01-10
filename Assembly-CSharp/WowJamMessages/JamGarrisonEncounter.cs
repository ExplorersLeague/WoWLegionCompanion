using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages
{
	[FlexJamStruct(Name = "JamGarrisonEncounter", Version = 28333852u)]
	[System.Runtime.Serialization.DataContract]
	public class JamGarrisonEncounter
	{
		[System.Runtime.Serialization.DataMember(Name = "encounterID")]
		[FlexJamMember(Name = "encounterID", Type = FlexJamType.Int32)]
		public int EncounterID { get; set; }

		[FlexJamMember(ArrayDimensions = 1, Name = "mechanicID", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "mechanicID")]
		public int[] MechanicID { get; set; }
	}
}
