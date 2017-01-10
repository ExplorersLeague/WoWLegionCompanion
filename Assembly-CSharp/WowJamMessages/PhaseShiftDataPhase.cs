using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages
{
	[System.Runtime.Serialization.DataContract]
	[FlexJamStruct(Name = "PhaseShiftDataPhase", Version = 28333852u)]
	public class PhaseShiftDataPhase
	{
		[FlexJamMember(Name = "id", Type = FlexJamType.UInt16)]
		[System.Runtime.Serialization.DataMember(Name = "id")]
		public ushort Id { get; set; }

		[FlexJamMember(Name = "phaseFlags", Type = FlexJamType.UInt16)]
		[System.Runtime.Serialization.DataMember(Name = "phaseFlags")]
		public ushort PhaseFlags { get; set; }
	}
}
