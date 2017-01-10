using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages
{
	[System.Runtime.Serialization.DataContract]
	[FlexJamStruct(Name = "JamPartyMemberAuraState", Version = 28333852u)]
	public class JamPartyMemberAuraState
	{
		[FlexJamMember(Name = "activeFlags", Type = FlexJamType.UInt32)]
		[System.Runtime.Serialization.DataMember(Name = "activeFlags")]
		public uint ActiveFlags { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "aura")]
		[FlexJamMember(Name = "aura", Type = FlexJamType.Int32)]
		public int Aura { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "flags")]
		[FlexJamMember(Name = "flags", Type = FlexJamType.UInt8)]
		public byte Flags { get; set; }

		[FlexJamMember(ArrayDimensions = 1, Name = "points", Type = FlexJamType.Float)]
		[System.Runtime.Serialization.DataMember(Name = "points")]
		public float[] Points { get; set; }
	}
}
