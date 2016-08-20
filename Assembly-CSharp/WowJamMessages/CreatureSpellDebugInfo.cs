using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages
{
	[FlexJamStruct(Name = "CreatureSpellDebugInfo", Version = 28333852u)]
	[System.Runtime.Serialization.DataContract]
	public class CreatureSpellDebugInfo
	{
		[System.Runtime.Serialization.DataMember(Name = "spellID")]
		[FlexJamMember(Name = "spellID", Type = FlexJamType.Int32)]
		public int SpellID { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "availability")]
		[FlexJamMember(Name = "availability", Type = FlexJamType.Int32)]
		public int Availability { get; set; }

		[FlexJamMember(Name = "initialDelayMax", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "initialDelayMax")]
		public int InitialDelayMax { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "spellName")]
		[FlexJamMember(Name = "spellName", Type = FlexJamType.String)]
		public string SpellName { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "repeatFrequencyMin")]
		[FlexJamMember(Name = "repeatFrequencyMin", Type = FlexJamType.Int32)]
		public int RepeatFrequencyMin { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "priority")]
		[FlexJamMember(Name = "priority", Type = FlexJamType.Int32)]
		public int Priority { get; set; }

		[FlexJamMember(Name = "initialDelayMin", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "initialDelayMin")]
		public int InitialDelayMin { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "repeatFrequencyMax")]
		[FlexJamMember(Name = "repeatFrequencyMax", Type = FlexJamType.Int32)]
		public int RepeatFrequencyMax { get; set; }
	}
}
