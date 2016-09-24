using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages
{
	[FlexJamStruct(Name = "JamPartyMemberPetState", Version = 28333852u)]
	[System.Runtime.Serialization.DataContract]
	public class JamPartyMemberPetState
	{
		public JamPartyMemberPetState()
		{
			this.Guid = "0000000000000000";
		}

		[FlexJamMember(Name = "guid", Type = FlexJamType.WowGuid)]
		[System.Runtime.Serialization.DataMember(Name = "guid")]
		public string Guid { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "health")]
		[FlexJamMember(Name = "health", Type = FlexJamType.Int32)]
		public int Health { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "maxHealth")]
		[FlexJamMember(Name = "maxHealth", Type = FlexJamType.Int32)]
		public int MaxHealth { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "name")]
		[FlexJamMember(Name = "name", Type = FlexJamType.String)]
		public string Name { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "auras")]
		[FlexJamMember(ArrayDimensions = 1, Name = "auras", Type = FlexJamType.Struct)]
		public JamPartyMemberAuraState[] Auras { get; set; }

		[FlexJamMember(Name = "displayID", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "displayID")]
		public int DisplayID { get; set; }
	}
}
