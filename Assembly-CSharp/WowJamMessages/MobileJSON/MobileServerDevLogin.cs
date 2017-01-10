using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobileJSON
{
	[FlexJamMessage(Id = 4741, Name = "MobileServerDevLogin", Version = 28333852u)]
	[System.Runtime.Serialization.DataContract]
	public class MobileServerDevLogin
	{
		[System.Runtime.Serialization.DataMember(Name = "locale")]
		[FlexJamMember(Name = "locale", Type = FlexJamType.String)]
		public string Locale { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "wowAccount")]
		[FlexJamMember(Name = "wowAccount", Type = FlexJamType.WowGuid)]
		public string WowAccount { get; set; }

		[FlexJamMember(Name = "characterID", Type = FlexJamType.WowGuid)]
		[System.Runtime.Serialization.DataMember(Name = "characterID")]
		public string CharacterID { get; set; }

		[FlexJamMember(Name = "virtualRealmAddress", Type = FlexJamType.UInt32)]
		[System.Runtime.Serialization.DataMember(Name = "virtualRealmAddress")]
		public uint VirtualRealmAddress { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "bnetAccount")]
		[FlexJamMember(Name = "bnetAccount", Type = FlexJamType.WowGuid)]
		public string BnetAccount { get; set; }
	}
}
