using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages
{
	[System.Runtime.Serialization.DataContract]
	[FlexJamStruct(Name = "PhaseShiftDataVersion1", Version = 28333852u)]
	public class PhaseShiftDataVersion1
	{
		[System.Runtime.Serialization.DataMember(Name = "phaseShiftFlags")]
		[FlexJamMember(Name = "phaseShiftFlags", Type = FlexJamType.UInt32)]
		public uint PhaseShiftFlags { get; set; }

		[FlexJamMember(Name = "personalGUID", Type = FlexJamType.WowGuid)]
		[System.Runtime.Serialization.DataMember(Name = "personalGUID")]
		public string PersonalGUID { get; set; }

		[FlexJamMember(ArrayDimensions = 1, Name = "phaseID", Type = FlexJamType.UInt16)]
		[System.Runtime.Serialization.DataMember(Name = "phaseID")]
		public ushort[] PhaseID { get; set; }
	}
}
