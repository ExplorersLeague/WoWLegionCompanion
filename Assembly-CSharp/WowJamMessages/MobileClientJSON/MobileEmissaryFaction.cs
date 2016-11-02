using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobileClientJSON
{
	[System.Runtime.Serialization.DataContract]
	[FlexJamStruct(Name = "MobileEmissaryFaction", Version = 33577221u)]
	public class MobileEmissaryFaction
	{
		[FlexJamMember(Name = "factionID", Type = FlexJamType.UInt16)]
		[System.Runtime.Serialization.DataMember(Name = "factionID")]
		public ushort FactionID { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "factionAmount")]
		[FlexJamMember(Name = "factionAmount", Type = FlexJamType.Int32)]
		public int FactionAmount { get; set; }
	}
}
