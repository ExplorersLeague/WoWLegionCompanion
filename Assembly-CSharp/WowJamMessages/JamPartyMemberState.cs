using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages
{
	[System.Runtime.Serialization.DataContract]
	[FlexJamStruct(Name = "JamPartyMemberState", Version = 28333852u)]
	public class JamPartyMemberState
	{
		public JamPartyMemberState()
		{
			this.PartyType = new byte[2];
		}

		[System.Runtime.Serialization.DataMember(Name = "level")]
		[FlexJamMember(Name = "level", Type = FlexJamType.UInt16)]
		public ushort Level { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "overrideDisplayPower")]
		[FlexJamMember(Name = "overrideDisplayPower", Type = FlexJamType.UInt16)]
		public ushort OverrideDisplayPower { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "maxHealth")]
		[FlexJamMember(Name = "maxHealth", Type = FlexJamType.Int32)]
		public int MaxHealth { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "maxPower")]
		[FlexJamMember(Name = "maxPower", Type = FlexJamType.UInt16)]
		public ushort MaxPower { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "displayPower")]
		[FlexJamMember(Name = "displayPower", Type = FlexJamType.UInt8)]
		public byte DisplayPower { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "wmoDoodadPlacementID")]
		[FlexJamMember(Name = "wmoDoodadPlacementID", Type = FlexJamType.UInt32)]
		public uint WmoDoodadPlacementID { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "auras")]
		[FlexJamMember(ArrayDimensions = 1, Name = "auras", Type = FlexJamType.Struct)]
		public JamPartyMemberAuraState[] Auras { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "spec")]
		[FlexJamMember(Name = "spec", Type = FlexJamType.UInt16)]
		public ushort Spec { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "vehicleSeatRecID")]
		[FlexJamMember(Name = "vehicleSeatRecID", Type = FlexJamType.Int32)]
		public int VehicleSeatRecID { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "phase")]
		[FlexJamMember(Name = "phase", Type = FlexJamType.Struct)]
		public PhaseShiftData Phase { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "loc")]
		[FlexJamMember(Name = "loc", Type = FlexJamType.Struct)]
		public JamShortVec3 Loc { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "areaID")]
		[FlexJamMember(Name = "areaID", Type = FlexJamType.UInt16)]
		public ushort AreaID { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "wmoGroupID")]
		[FlexJamMember(Name = "wmoGroupID", Type = FlexJamType.UInt16)]
		public ushort WmoGroupID { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "health")]
		[FlexJamMember(Name = "health", Type = FlexJamType.Int32)]
		public int Health { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "partyType")]
		[FlexJamMember(ArrayDimensions = 1, Name = "partyType", Type = FlexJamType.UInt8)]
		public byte[] PartyType { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "flags")]
		[FlexJamMember(Name = "flags", Type = FlexJamType.UInt16)]
		public ushort Flags { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "pet")]
		[FlexJamMember(Optional = true, Name = "pet", Type = FlexJamType.Struct)]
		public JamPartyMemberPetState[] Pet { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "power")]
		[FlexJamMember(Name = "power", Type = FlexJamType.UInt16)]
		public ushort Power { get; set; }
	}
}
