using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages
{
	[System.Runtime.Serialization.DataContract]
	[FlexJamStruct(Name = "JamGarrisonEncounter", Version = 28333852u)]
	public class JamGarrisonEncounter
	{
		[FlexJamMember(Name = "encounterID", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "encounterID")]
		public int EncounterID { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "mechanicID")]
		[FlexJamMember(ArrayDimensions = 1, Name = "mechanicID", Type = FlexJamType.Int32)]
		public int[] MechanicID { get; set; }
	}
}
