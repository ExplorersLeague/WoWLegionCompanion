using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages
{
	[System.Runtime.Serialization.DataContract]
	[FlexJamStruct(Name = "CreatureSpellDebugInfo", Version = 28333852u)]
	public class CreatureSpellDebugInfo
	{
		[FlexJamMember(Name = "spellID", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "spellID")]
		public int SpellID { get; set; }

		[FlexJamMember(Name = "availability", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "availability")]
		public int Availability { get; set; }

		[FlexJamMember(Name = "initialDelayMax", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "initialDelayMax")]
		public int InitialDelayMax { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "spellName")]
		[FlexJamMember(Name = "spellName", Type = FlexJamType.String)]
		public string SpellName { get; set; }

		[FlexJamMember(Name = "repeatFrequencyMin", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "repeatFrequencyMin")]
		public int RepeatFrequencyMin { get; set; }

		[FlexJamMember(Name = "priority", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "priority")]
		public int Priority { get; set; }

		[FlexJamMember(Name = "initialDelayMin", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "initialDelayMin")]
		public int InitialDelayMin { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "repeatFrequencyMax")]
		[FlexJamMember(Name = "repeatFrequencyMax", Type = FlexJamType.Int32)]
		public int RepeatFrequencyMax { get; set; }
	}
}
