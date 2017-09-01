using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobileClientJSON
{
	[FlexJamStruct(Name = "MobileEmissaryFaction", Version = 39869590u)]
	[System.Runtime.Serialization.DataContract]
	public class MobileEmissaryFaction
	{
		[FlexJamMember(Name = "factionID", Type = FlexJamType.UInt16)]
		[System.Runtime.Serialization.DataMember(Name = "factionID")]
		public ushort FactionID { get; set; }

		[FlexJamMember(Name = "factionAmount", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "factionAmount")]
		public int FactionAmount { get; set; }
	}
}
