using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages
{
	[FlexJamStruct(Name = "PhaseShiftDataVersion1", Version = 28333852u)]
	[System.Runtime.Serialization.DataContract]
	public class PhaseShiftDataVersion1
	{
		[FlexJamMember(Name = "phaseShiftFlags", Type = FlexJamType.UInt32)]
		[System.Runtime.Serialization.DataMember(Name = "phaseShiftFlags")]
		public uint PhaseShiftFlags { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "personalGUID")]
		[FlexJamMember(Name = "personalGUID", Type = FlexJamType.WowGuid)]
		public string PersonalGUID { get; set; }

		[FlexJamMember(ArrayDimensions = 1, Name = "phaseID", Type = FlexJamType.UInt16)]
		[System.Runtime.Serialization.DataMember(Name = "phaseID")]
		public ushort[] PhaseID { get; set; }
	}
}
