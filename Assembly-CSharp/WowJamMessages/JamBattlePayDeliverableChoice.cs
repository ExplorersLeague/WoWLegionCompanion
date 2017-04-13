using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages
{
	[System.Runtime.Serialization.DataContract]
	[FlexJamStruct(Name = "JamBattlePayDeliverableChoice", Version = 28333852u)]
	public class JamBattlePayDeliverableChoice
	{
		[System.Runtime.Serialization.DataMember(Name = "alreadyOwns")]
		[FlexJamMember(Name = "alreadyOwns", Type = FlexJamType.Bool)]
		public bool AlreadyOwns { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "battlePetCreatureID")]
		[FlexJamMember(Name = "battlePetCreatureID", Type = FlexJamType.UInt32)]
		public uint BattlePetCreatureID { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "petResult")]
		[FlexJamMember(Optional = true, Name = "petResult", Type = FlexJamType.Enum)]
		public BATTLEPETRESULT[] PetResult { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "displayInfo")]
		[FlexJamMember(Optional = true, Name = "displayInfo", Type = FlexJamType.Struct)]
		public JamBattlepayDisplayInfo[] DisplayInfo { get; set; }

		[FlexJamMember(Name = "itemID", Type = FlexJamType.UInt32)]
		[System.Runtime.Serialization.DataMember(Name = "itemID")]
		public uint ItemID { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "type")]
		[FlexJamMember(Name = "type", Type = FlexJamType.UInt8)]
		public byte Type { get; set; }

		[FlexJamMember(Name = "mountSpellID", Type = FlexJamType.UInt32)]
		[System.Runtime.Serialization.DataMember(Name = "mountSpellID")]
		public uint MountSpellID { get; set; }

		[FlexJamMember(Name = "ID", Type = FlexJamType.UInt32)]
		[System.Runtime.Serialization.DataMember(Name = "ID")]
		public uint ID { get; set; }

		[FlexJamMember(Name = "quantity", Type = FlexJamType.UInt32)]
		[System.Runtime.Serialization.DataMember(Name = "quantity")]
		public uint Quantity { get; set; }
	}
}
