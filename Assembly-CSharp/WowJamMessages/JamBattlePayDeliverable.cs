using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages
{
	[System.Runtime.Serialization.DataContract]
	[FlexJamStruct(Name = "JamBattlePayDeliverable", Version = 28333852u)]
	public class JamBattlePayDeliverable
	{
		[FlexJamMember(Name = "alreadyOwns", Type = FlexJamType.Bool)]
		[System.Runtime.Serialization.DataMember(Name = "alreadyOwns")]
		public bool AlreadyOwns { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "choices")]
		[FlexJamMember(ArrayDimensions = 1, Name = "choices", Type = FlexJamType.Struct)]
		public JamBattlePayDeliverableChoice[] Choices { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "deliverableID")]
		[FlexJamMember(Name = "deliverableID", Type = FlexJamType.UInt32)]
		public uint DeliverableID { get; set; }

		[FlexJamMember(Name = "battlePetCreatureID", Type = FlexJamType.UInt32)]
		[System.Runtime.Serialization.DataMember(Name = "battlePetCreatureID")]
		public uint BattlePetCreatureID { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "petResult")]
		[FlexJamMember(Optional = true, Name = "petResult", Type = FlexJamType.Enum)]
		public BATTLEPETRESULT[] PetResult { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "flags")]
		[FlexJamMember(Name = "flags", Type = FlexJamType.UInt32)]
		public uint Flags { get; set; }

		[FlexJamMember(Optional = true, Name = "displayInfo", Type = FlexJamType.Struct)]
		[System.Runtime.Serialization.DataMember(Name = "displayInfo")]
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

		[FlexJamMember(Name = "quantity", Type = FlexJamType.UInt32)]
		[System.Runtime.Serialization.DataMember(Name = "quantity")]
		public uint Quantity { get; set; }
	}
}
