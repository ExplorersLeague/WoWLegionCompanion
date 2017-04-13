using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobileJSON
{
	[System.Runtime.Serialization.DataContract]
	[FlexJamMessage(Id = 4741, Name = "MobileServerDevLogin", Version = 28333852u)]
	public class MobileServerDevLogin
	{
		[FlexJamMember(Name = "locale", Type = FlexJamType.String)]
		[System.Runtime.Serialization.DataMember(Name = "locale")]
		public string Locale { get; set; }

		[FlexJamMember(Name = "wowAccount", Type = FlexJamType.WowGuid)]
		[System.Runtime.Serialization.DataMember(Name = "wowAccount")]
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
