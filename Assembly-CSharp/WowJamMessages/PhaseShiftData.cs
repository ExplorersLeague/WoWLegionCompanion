using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages
{
	[FlexJamStruct(Name = "PhaseShiftData", Version = 28333852u)]
	[System.Runtime.Serialization.DataContract]
	public class PhaseShiftData
	{
		[System.Runtime.Serialization.DataMember(Name = "phaseShiftFlags")]
		[FlexJamMember(Name = "phaseShiftFlags", Type = FlexJamType.UInt32)]
		public uint PhaseShiftFlags { get; set; }

		[FlexJamMember(ArrayDimensions = 1, Name = "phases", Type = FlexJamType.Struct)]
		[System.Runtime.Serialization.DataMember(Name = "phases")]
		public PhaseShiftDataPhase[] Phases { get; set; }

		[FlexJamMember(Name = "personalGUID", Type = FlexJamType.WowGuid)]
		[System.Runtime.Serialization.DataMember(Name = "personalGUID")]
		public string PersonalGUID { get; set; }
	}
}
