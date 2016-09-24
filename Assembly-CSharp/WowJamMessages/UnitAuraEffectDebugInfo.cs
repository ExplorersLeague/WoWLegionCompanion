using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages
{
	[System.Runtime.Serialization.DataContract]
	[FlexJamStruct(Name = "UnitAuraEffectDebugInfo", Version = 28333852u)]
	public class UnitAuraEffectDebugInfo
	{
		[FlexJamMember(Name = "active", Type = FlexJamType.Bool)]
		[System.Runtime.Serialization.DataMember(Name = "active")]
		public bool Active { get; set; }

		[FlexJamMember(Name = "effectIndex", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "effectIndex")]
		public int EffectIndex { get; set; }

		[FlexJamMember(Name = "amount", Type = FlexJamType.Float)]
		[System.Runtime.Serialization.DataMember(Name = "amount")]
		public float Amount { get; set; }
	}
}
