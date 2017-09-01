using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages
{
	[FlexJamStruct(Name = "PhaseShiftDataVersion0", Version = 28333852u)]
	[System.Runtime.Serialization.DataContract]
	public class PhaseShiftDataVersion0
	{
		[System.Runtime.Serialization.DataMember(Name = "phaseShiftFlags")]
		[FlexJamMember(Name = "phaseShiftFlags", Type = FlexJamType.UInt32)]
		public uint PhaseShiftFlags { get; set; }

		[FlexJamMember(ArrayDimensions = 1, Name = "phaseID", Type = FlexJamType.UInt16)]
		[System.Runtime.Serialization.DataMember(Name = "phaseID")]
		public ushort[] PhaseID { get; set; }
	}
}
