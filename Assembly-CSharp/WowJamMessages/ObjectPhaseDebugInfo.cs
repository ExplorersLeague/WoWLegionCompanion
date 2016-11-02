using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages
{
	[FlexJamStruct(Name = "ObjectPhaseDebugInfo", Version = 28333852u)]
	[System.Runtime.Serialization.DataContract]
	public class ObjectPhaseDebugInfo
	{
		[FlexJamMember(Name = "phaseName", Type = FlexJamType.String)]
		[System.Runtime.Serialization.DataMember(Name = "phaseName")]
		public string PhaseName { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "phaseID")]
		[FlexJamMember(Name = "phaseID", Type = FlexJamType.Int32)]
		public int PhaseID { get; set; }
	}
}
