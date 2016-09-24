using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages
{
	[System.Runtime.Serialization.DataContract]
	[FlexJamStruct(Name = "PhaseShiftDataVersion0", Version = 28333852u)]
	public class PhaseShiftDataVersion0
	{
		[FlexJamMember(Name = "phaseShiftFlags", Type = FlexJamType.UInt32)]
		[System.Runtime.Serialization.DataMember(Name = "phaseShiftFlags")]
		public uint PhaseShiftFlags { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "phaseID")]
		[FlexJamMember(ArrayDimensions = 1, Name = "phaseID", Type = FlexJamType.UInt16)]
		public ushort[] PhaseID { get; set; }
	}
}
