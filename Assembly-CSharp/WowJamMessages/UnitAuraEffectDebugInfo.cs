using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages
{
	[FlexJamStruct(Name = "UnitAuraEffectDebugInfo", Version = 28333852u)]
	[System.Runtime.Serialization.DataContract]
	public class UnitAuraEffectDebugInfo
	{
		[System.Runtime.Serialization.DataMember(Name = "active")]
		[FlexJamMember(Name = "active", Type = FlexJamType.Bool)]
		public bool Active { get; set; }

		[FlexJamMember(Name = "effectIndex", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "effectIndex")]
		public int EffectIndex { get; set; }

		[FlexJamMember(Name = "amount", Type = FlexJamType.Float)]
		[System.Runtime.Serialization.DataMember(Name = "amount")]
		public float Amount { get; set; }
	}
}
