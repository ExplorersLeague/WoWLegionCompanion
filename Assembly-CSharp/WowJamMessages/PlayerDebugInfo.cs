using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages
{
	[System.Runtime.Serialization.DataContract]
	[FlexJamStruct(Name = "PlayerDebugInfo", Version = 28333852u)]
	public class PlayerDebugInfo
	{
		public PlayerDebugInfo()
		{
			this.CombatRatings = new int[32];
		}

		[FlexJamMember(ArrayDimensions = 1, Name = "combatRatings", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "combatRatings")]
		public int[] CombatRatings { get; set; }
	}
}
