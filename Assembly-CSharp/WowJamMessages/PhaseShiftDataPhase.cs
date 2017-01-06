using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages
{
	[FlexJamStruct(Name = "PhaseShiftDataPhase", Version = 28333852u)]
	[System.Runtime.Serialization.DataContract]
	public class PhaseShiftDataPhase
	{
		[System.Runtime.Serialization.DataMember(Name = "id")]
		[FlexJamMember(Name = "id", Type = FlexJamType.UInt16)]
		public ushort Id { get; set; }

		[FlexJamMember(Name = "phaseFlags", Type = FlexJamType.UInt16)]
		[System.Runtime.Serialization.DataMember(Name = "phaseFlags")]
		public ushort PhaseFlags { get; set; }
	}
}
