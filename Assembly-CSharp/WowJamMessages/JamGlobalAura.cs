using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages
{
	[System.Runtime.Serialization.DataContract]
	[FlexJamStruct(Name = "JamGlobalAura", Version = 28333852u)]
	public class JamGlobalAura
	{
		[FlexJamMember(Name = "spellID", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "spellID")]
		public int SpellID { get; set; }

		[FlexJamMember(Name = "playerConditionID", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "playerConditionID")]
		public int PlayerConditionID { get; set; }
	}
}
