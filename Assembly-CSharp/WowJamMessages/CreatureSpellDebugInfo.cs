﻿using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages
{
	[System.Runtime.Serialization.DataContract]
	[FlexJamStruct(Name = "CreatureSpellDebugInfo", Version = 28333852u)]
	public class CreatureSpellDebugInfo
	{
		[System.Runtime.Serialization.DataMember(Name = "spellID")]
		[FlexJamMember(Name = "spellID", Type = FlexJamType.Int32)]
		public int SpellID { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "availability")]
		[FlexJamMember(Name = "availability", Type = FlexJamType.Int32)]
		public int Availability { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "initialDelayMax")]
		[FlexJamMember(Name = "initialDelayMax", Type = FlexJamType.Int32)]
		public int InitialDelayMax { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "spellName")]
		[FlexJamMember(Name = "spellName", Type = FlexJamType.String)]
		public string SpellName { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "repeatFrequencyMin")]
		[FlexJamMember(Name = "repeatFrequencyMin", Type = FlexJamType.Int32)]
		public int RepeatFrequencyMin { get; set; }

		[FlexJamMember(Name = "priority", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "priority")]
		public int Priority { get; set; }

		[FlexJamMember(Name = "initialDelayMin", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "initialDelayMin")]
		public int InitialDelayMin { get; set; }

		[FlexJamMember(Name = "repeatFrequencyMax", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "repeatFrequencyMax")]
		public int RepeatFrequencyMax { get; set; }
	}
}
