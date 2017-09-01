using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages
{
	[FlexJamStruct(Name = "JamPartyMemberAuraState", Version = 28333852u)]
	[System.Runtime.Serialization.DataContract]
	public class JamPartyMemberAuraState
	{
		[FlexJamMember(Name = "activeFlags", Type = FlexJamType.UInt32)]
		[System.Runtime.Serialization.DataMember(Name = "activeFlags")]
		public uint ActiveFlags { get; set; }

		[FlexJamMember(Name = "aura", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "aura")]
		public int Aura { get; set; }

		[FlexJamMember(Name = "flags", Type = FlexJamType.UInt8)]
		[System.Runtime.Serialization.DataMember(Name = "flags")]
		public byte Flags { get; set; }

		[FlexJamMember(ArrayDimensions = 1, Name = "points", Type = FlexJamType.Float)]
		[System.Runtime.Serialization.DataMember(Name = "points")]
		public float[] Points { get; set; }
	}
}
