using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages
{
	[System.Runtime.Serialization.DataContract]
	[FlexJamStruct(Name = "PhaseShiftData", Version = 28333852u)]
	public class PhaseShiftData
	{
		[FlexJamMember(Name = "phaseShiftFlags", Type = FlexJamType.UInt32)]
		[System.Runtime.Serialization.DataMember(Name = "phaseShiftFlags")]
		public uint PhaseShiftFlags { get; set; }

		[FlexJamMember(ArrayDimensions = 1, Name = "phases", Type = FlexJamType.Struct)]
		[System.Runtime.Serialization.DataMember(Name = "phases")]
		public PhaseShiftDataPhase[] Phases { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "personalGUID")]
		[FlexJamMember(Name = "personalGUID", Type = FlexJamType.WowGuid)]
		public string PersonalGUID { get; set; }
	}
}
