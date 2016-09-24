﻿using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages
{
	[FlexJamStruct(Name = "JamGlobalAura", Version = 28333852u)]
	[System.Runtime.Serialization.DataContract]
	public class JamGlobalAura
	{
		[System.Runtime.Serialization.DataMember(Name = "spellID")]
		[FlexJamMember(Name = "spellID", Type = FlexJamType.Int32)]
		public int SpellID { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "playerConditionID")]
		[FlexJamMember(Name = "playerConditionID", Type = FlexJamType.Int32)]
		public int PlayerConditionID { get; set; }
	}
}
