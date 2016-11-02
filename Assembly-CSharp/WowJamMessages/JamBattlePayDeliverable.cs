using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages
{
	[System.Runtime.Serialization.DataContract]
	[FlexJamStruct(Name = "JamBattlePayDeliverable", Version = 28333852u)]
	public class JamBattlePayDeliverable
	{
		[System.Runtime.Serialization.DataMember(Name = "alreadyOwns")]
		[FlexJamMember(Name = "alreadyOwns", Type = FlexJamType.Bool)]
		public bool AlreadyOwns { get; set; }

		[FlexJamMember(ArrayDimensions = 1, Name = "choices", Type = FlexJamType.Struct)]
		[System.Runtime.Serialization.DataMember(Name = "choices")]
		public JamBattlePayDeliverableChoice[] Choices { get; set; }

		[FlexJamMember(Name = "deliverableID", Type = FlexJamType.UInt32)]
		[System.Runtime.Serialization.DataMember(Name = "deliverableID")]
		public uint DeliverableID { get; set; }

		[FlexJamMember(Name = "battlePetCreatureID", Type = FlexJamType.UInt32)]
		[System.Runtime.Serialization.DataMember(Name = "battlePetCreatureID")]
		public uint BattlePetCreatureID { get; set; }

		[FlexJamMember(Optional = true, Name = "petResult", Type = FlexJamType.Enum)]
		[System.Runtime.Serialization.DataMember(Name = "petResult")]
		public BATTLEPETRESULT[] PetResult { get; set; }

		[FlexJamMember(Name = "flags", Type = FlexJamType.UInt32)]
		[System.Runtime.Serialization.DataMember(Name = "flags")]
		public uint Flags { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "displayInfo")]
		[FlexJamMember(Optional = true, Name = "displayInfo", Type = FlexJamType.Struct)]
		public JamBattlepayDisplayInfo[] DisplayInfo { get; set; }

		[FlexJamMember(Name = "itemID", Type = FlexJamType.UInt32)]
		[System.Runtime.Serialization.DataMember(Name = "itemID")]
		public uint ItemID { get; set; }

		[FlexJamMember(Name = "type", Type = FlexJamType.UInt8)]
		[System.Runtime.Serialization.DataMember(Name = "type")]
		public byte Type { get; set; }

		[FlexJamMember(Name = "mountSpellID", Type = FlexJamType.UInt32)]
		[System.Runtime.Serialization.DataMember(Name = "mountSpellID")]
		public uint MountSpellID { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "quantity")]
		[FlexJamMember(Name = "quantity", Type = FlexJamType.UInt32)]
		public uint Quantity { get; set; }
	}
}
