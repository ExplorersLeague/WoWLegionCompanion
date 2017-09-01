using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages
{
	[FlexJamStruct(Name = "JamPartyMemberState", Version = 28333852u)]
	[System.Runtime.Serialization.DataContract]
	public class JamPartyMemberState
	{
		public JamPartyMemberState()
		{
			this.PartyType = new byte[2];
		}

		[System.Runtime.Serialization.DataMember(Name = "level")]
		[FlexJamMember(Name = "level", Type = FlexJamType.UInt16)]
		public ushort Level { get; set; }

		[FlexJamMember(Name = "overrideDisplayPower", Type = FlexJamType.UInt16)]
		[System.Runtime.Serialization.DataMember(Name = "overrideDisplayPower")]
		public ushort OverrideDisplayPower { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "maxHealth")]
		[FlexJamMember(Name = "maxHealth", Type = FlexJamType.Int32)]
		public int MaxHealth { get; set; }

		[FlexJamMember(Name = "maxPower", Type = FlexJamType.UInt16)]
		[System.Runtime.Serialization.DataMember(Name = "maxPower")]
		public ushort MaxPower { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "displayPower")]
		[FlexJamMember(Name = "displayPower", Type = FlexJamType.UInt8)]
		public byte DisplayPower { get; set; }

		[FlexJamMember(Name = "wmoDoodadPlacementID", Type = FlexJamType.UInt32)]
		[System.Runtime.Serialization.DataMember(Name = "wmoDoodadPlacementID")]
		public uint WmoDoodadPlacementID { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "auras")]
		[FlexJamMember(ArrayDimensions = 1, Name = "auras", Type = FlexJamType.Struct)]
		public JamPartyMemberAuraState[] Auras { get; set; }

		[FlexJamMember(Name = "spec", Type = FlexJamType.UInt16)]
		[System.Runtime.Serialization.DataMember(Name = "spec")]
		public ushort Spec { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "vehicleSeatRecID")]
		[FlexJamMember(Name = "vehicleSeatRecID", Type = FlexJamType.Int32)]
		public int VehicleSeatRecID { get; set; }

		[FlexJamMember(Name = "phase", Type = FlexJamType.Struct)]
		[System.Runtime.Serialization.DataMember(Name = "phase")]
		public PhaseShiftData Phase { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "loc")]
		[FlexJamMember(Name = "loc", Type = FlexJamType.Struct)]
		public JamShortVec3 Loc { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "areaID")]
		[FlexJamMember(Name = "areaID", Type = FlexJamType.UInt16)]
		public ushort AreaID { get; set; }

		[FlexJamMember(Name = "wmoGroupID", Type = FlexJamType.UInt16)]
		[System.Runtime.Serialization.DataMember(Name = "wmoGroupID")]
		public ushort WmoGroupID { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "health")]
		[FlexJamMember(Name = "health", Type = FlexJamType.Int32)]
		public int Health { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "partyType")]
		[FlexJamMember(ArrayDimensions = 1, Name = "partyType", Type = FlexJamType.UInt8)]
		public byte[] PartyType { get; set; }

		[FlexJamMember(Name = "flags", Type = FlexJamType.UInt16)]
		[System.Runtime.Serialization.DataMember(Name = "flags")]
		public ushort Flags { get; set; }

		[FlexJamMember(Optional = true, Name = "pet", Type = FlexJamType.Struct)]
		[System.Runtime.Serialization.DataMember(Name = "pet")]
		public JamPartyMemberPetState[] Pet { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "power")]
		[FlexJamMember(Name = "power", Type = FlexJamType.UInt16)]
		public ushort Power { get; set; }
	}
}
