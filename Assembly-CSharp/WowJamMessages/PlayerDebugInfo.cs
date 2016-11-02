using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages
{
	[FlexJamStruct(Name = "PlayerDebugInfo", Version = 28333852u)]
	[System.Runtime.Serialization.DataContract]
	public class PlayerDebugInfo
	{
		public PlayerDebugInfo()
		{
			this.CombatRatings = new int[32];
		}

		[System.Runtime.Serialization.DataMember(Name = "combatRatings")]
		[FlexJamMember(ArrayDimensions = 1, Name = "combatRatings", Type = FlexJamType.Int32)]
		public int[] CombatRatings { get; set; }
	}
}
