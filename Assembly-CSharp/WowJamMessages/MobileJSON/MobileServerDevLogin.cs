using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobileJSON
{
	[FlexJamMessage(Id = 4741, Name = "MobileServerDevLogin", Version = 28333852u)]
	[System.Runtime.Serialization.DataContract]
	public class MobileServerDevLogin
	{
		[FlexJamMember(Name = "locale", Type = FlexJamType.String)]
		[System.Runtime.Serialization.DataMember(Name = "locale")]
		public string Locale { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "wowAccount")]
		[FlexJamMember(Name = "wowAccount", Type = FlexJamType.WowGuid)]
		public string WowAccount { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "characterID")]
		[FlexJamMember(Name = "characterID", Type = FlexJamType.WowGuid)]
		public string CharacterID { get; set; }

		[FlexJamMember(Name = "virtualRealmAddress", Type = FlexJamType.UInt32)]
		[System.Runtime.Serialization.DataMember(Name = "virtualRealmAddress")]
		public uint VirtualRealmAddress { get; set; }

		[FlexJamMember(Name = "bnetAccount", Type = FlexJamType.WowGuid)]
		[System.Runtime.Serialization.DataMember(Name = "bnetAccount")]
		public string BnetAccount { get; set; }
	}
}
