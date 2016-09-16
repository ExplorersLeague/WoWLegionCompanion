using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobileClientJSON
{
	[FlexJamStruct(Name = "MobileEmissaryFaction", Version = 28333852u)]
	[System.Runtime.Serialization.DataContract]
	public class MobileEmissaryFaction
	{
		[System.Runtime.Serialization.DataMember(Name = "factionID")]
		[FlexJamMember(Name = "factionID", Type = FlexJamType.UInt16)]
		public ushort FactionID { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "factionAmount")]
		[FlexJamMember(Name = "factionAmount", Type = FlexJamType.Int32)]
		public int FactionAmount { get; set; }
	}
}
