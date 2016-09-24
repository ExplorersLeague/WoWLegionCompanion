using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages
{
	[FlexJamStruct(Name = "JamBattlePayDeliverableChoice", Version = 28333852u)]
	[System.Runtime.Serialization.DataContract]
	public class JamBattlePayDeliverableChoice
	{
		[FlexJamMember(Name = "alreadyOwns", Type = FlexJamType.Bool)]
		[System.Runtime.Serialization.DataMember(Name = "alreadyOwns")]
		public bool AlreadyOwns { get; set; }

		[FlexJamMember(Name = "battlePetCreatureID", Type = FlexJamType.UInt32)]
		[System.Runtime.Serialization.DataMember(Name = "battlePetCreatureID")]
		public uint BattlePetCreatureID { get; set; }

		[FlexJamMember(Optional = true, Name = "petResult", Type = FlexJamType.Enum)]
		[System.Runtime.Serialization.DataMember(Name = "petResult")]
		public BATTLEPETRESULT[] PetResult { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "displayInfo")]
		[FlexJamMember(Optional = true, Name = "displayInfo", Type = FlexJamType.Struct)]
		public JamBattlepayDisplayInfo[] DisplayInfo { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "itemID")]
		[FlexJamMember(Name = "itemID", Type = FlexJamType.UInt32)]
		public uint ItemID { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "type")]
		[FlexJamMember(Name = "type", Type = FlexJamType.UInt8)]
		public byte Type { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "mountSpellID")]
		[FlexJamMember(Name = "mountSpellID", Type = FlexJamType.UInt32)]
		public uint MountSpellID { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "ID")]
		[FlexJamMember(Name = "ID", Type = FlexJamType.UInt32)]
		public uint ID { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "quantity")]
		[FlexJamMember(Name = "quantity", Type = FlexJamType.UInt32)]
		public uint Quantity { get; set; }
	}
}
