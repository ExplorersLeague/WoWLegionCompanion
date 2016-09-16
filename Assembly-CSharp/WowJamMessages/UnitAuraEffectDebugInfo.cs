using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages
{
	[FlexJamStruct(Name = "UnitAuraEffectDebugInfo", Version = 28333852u)]
	[System.Runtime.Serialization.DataContract]
	public class UnitAuraEffectDebugInfo
	{
		[FlexJamMember(Name = "active", Type = FlexJamType.Bool)]
		[System.Runtime.Serialization.DataMember(Name = "active")]
		public bool Active { get; set; }

		[FlexJamMember(Name = "effectIndex", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "effectIndex")]
		public int EffectIndex { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "amount")]
		[FlexJamMember(Name = "amount", Type = FlexJamType.Float)]
		public float Amount { get; set; }
	}
}
