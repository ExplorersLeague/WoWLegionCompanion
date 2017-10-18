using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages
{
	[System.Runtime.Serialization.DataContract]
	[FlexJamStruct(Name = "UnitAuraEffectDebugInfo", Version = 28333852u)]
	public class UnitAuraEffectDebugInfo
	{
		[System.Runtime.Serialization.DataMember(Name = "active")]
		[FlexJamMember(Name = "active", Type = FlexJamType.Bool)]
		public bool Active { get; set; }

		[FlexJamMember(Name = "effectIndex", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "effectIndex")]
		public int EffectIndex { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "amount")]
		[FlexJamMember(Name = "amount", Type = FlexJamType.Float)]
		public float Amount { get; set; }
	}
}
