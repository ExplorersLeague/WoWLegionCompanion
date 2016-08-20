using System;
using System.Runtime.Serialization;
using bnet.protocol;
using WowJamMessages.JSONRealmList;

namespace WowJamMessages
{
	[System.Runtime.Serialization.DataContract]
	public class RecentCharacter
	{
		[System.Runtime.Serialization.DataMember(Name = "entry")]
		public JamJSONCharacterEntry Entry { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "gameAccount")]
		public EntityId GameAccount { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "unixTime")]
		public int UnixTime { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "webToken")]
		public string WebToken { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "subRegion")]
		public string SubRegion { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "version")]
		public int Version { get; set; }
	}
}
