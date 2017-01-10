using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages
{
	[System.Runtime.Serialization.DataContract]
	[FlexJamStruct(Name = "ObjectPhaseDebugInfo", Version = 28333852u)]
	public class ObjectPhaseDebugInfo
	{
		[System.Runtime.Serialization.DataMember(Name = "phaseName")]
		[FlexJamMember(Name = "phaseName", Type = FlexJamType.String)]
		public string PhaseName { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "phaseID")]
		[FlexJamMember(Name = "phaseID", Type = FlexJamType.Int32)]
		public int PhaseID { get; set; }
	}
}
