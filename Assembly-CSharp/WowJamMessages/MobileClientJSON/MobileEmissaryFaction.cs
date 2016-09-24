using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobileClientJSON
{
	[System.Runtime.Serialization.DataContract]
	[FlexJamStruct(Name = "MobileEmissaryFaction", Version = 28333852u)]
	public class MobileEmissaryFaction
	{
		[System.Runtime.Serialization.DataMember(Name = "factionID")]
		[FlexJamMember(Name = "factionID", Type = FlexJamType.UInt16)]
		public ushort FactionID { get; set; }

		[FlexJamMember(Name = "factionAmount", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "factionAmount")]
		public int FactionAmount { get; set; }
	}
}
