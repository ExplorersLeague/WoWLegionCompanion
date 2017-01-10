using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages
{
	[FlexJamStruct(Name = "CreatureSpellDebugInfo", Version = 28333852u)]
	[System.Runtime.Serialization.DataContract]
	public class CreatureSpellDebugInfo
	{
		[FlexJamMember(Name = "spellID", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "spellID")]
		public int SpellID { get; set; }

		[FlexJamMember(Name = "availability", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "availability")]
		public int Availability { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "initialDelayMax")]
		[FlexJamMember(Name = "initialDelayMax", Type = FlexJamType.Int32)]
		public int InitialDelayMax { get; set; }

		[FlexJamMember(Name = "spellName", Type = FlexJamType.String)]
		[System.Runtime.Serialization.DataMember(Name = "spellName")]
		public string SpellName { get; set; }

		[FlexJamMember(Name = "repeatFrequencyMin", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "repeatFrequencyMin")]
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
