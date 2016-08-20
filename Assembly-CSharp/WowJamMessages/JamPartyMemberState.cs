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

		[FlexJamMember(Name = "level", Type = FlexJamType.UInt16)]
		[System.Runtime.Serialization.DataMember(Name = "level")]
		public ushort Level { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "overrideDisplayPower")]
		[FlexJamMember(Name = "overrideDisplayPower", Type = FlexJamType.UInt16)]
		public ushort OverrideDisplayPower { get; set; }

		[FlexJamMember(Name = "maxHealth", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "maxHealth")]
		public int MaxHealth { get; set; }

		[FlexJamMember(Name = "maxPower", Type = FlexJamType.UInt16)]
		[System.Runtime.Serialization.DataMember(Name = "maxPower")]
		public ushort MaxPower { get; set; }

		[FlexJamMember(Name = "displayPower", Type = FlexJamType.UInt8)]
		[System.Runtime.Serialization.DataMember(Name = "displayPower")]
		public byte DisplayPower { get; set; }

		[FlexJamMember(Name = "wmoDoodadPlacementID", Type = FlexJamType.UInt32)]
		[System.Runtime.Serialization.DataMember(Name = "wmoDoodadPlacementID")]
		public uint WmoDoodadPlacementID { get; set; }

		[FlexJamMember(ArrayDimensions = 1, Name = "auras", Type = FlexJamType.Struct)]
		[System.Runtime.Serialization.DataMember(Name = "auras")]
		public JamPartyMemberAuraState[] Auras { get; set; }

		[FlexJamMember(Name = "spec", Type = FlexJamType.UInt16)]
		[System.Runtime.Serialization.DataMember(Name = "spec")]
		public ushort Spec { get; set; }

		[FlexJamMember(Name = "vehicleSeatRecID", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "vehicleSeatRecID")]
		public int VehicleSeatRecID { get; set; }

		[FlexJamMember(Name = "phase", Type = FlexJamType.Struct)]
		[System.Runtime.Serialization.DataMember(Name = "phase")]
		public PhaseShiftData Phase { get; set; }

		[FlexJamMember(Name = "loc", Type = FlexJamType.Struct)]
		[System.Runtime.Serialization.DataMember(Name = "loc")]
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

		[FlexJamMember(ArrayDimensions = 1, Name = "partyType", Type = FlexJamType.UInt8)]
		[System.Runtime.Serialization.DataMember(Name = "partyType")]
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
