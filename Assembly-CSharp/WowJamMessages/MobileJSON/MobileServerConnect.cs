using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobileJSON
{
	[System.Runtime.Serialization.DataContract]
	[FlexJamMessage(Id = 4743, Name = "MobileServerConnect", Version = 28333852u)]
	public class MobileServerConnect
	{
		public MobileServerConnect()
		{
			this.ClientChallenge = new byte[16];
			this.Proof = new byte[24];
		}

		[FlexJamMember(ArrayDimensions = 1, Name = "proof", Type = FlexJamType.UInt8)]
		[System.Runtime.Serialization.DataMember(Name = "proof")]
		public byte[] Proof { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "realmAddress")]
		[FlexJamMember(Name = "realmAddress", Type = FlexJamType.UInt32)]
		public uint RealmAddress { get; set; }

		[FlexJamMember(ArrayDimensions = 1, Name = "joinTicket", Type = FlexJamType.UInt8)]
		[System.Runtime.Serialization.DataMember(Name = "joinTicket")]
		public byte[] JoinTicket { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "characterID")]
		[FlexJamMember(Name = "characterID", Type = FlexJamType.WowGuid)]
		public string CharacterID { get; set; }

		[FlexJamMember(Name = "build", Type = FlexJamType.UInt16)]
		[System.Runtime.Serialization.DataMember(Name = "build")]
		public ushort Build { get; set; }

		[FlexJamMember(Name = "buildType", Type = FlexJamType.UInt32)]
		[System.Runtime.Serialization.DataMember(Name = "buildType")]
		public uint BuildType { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "clientChallenge")]
		[FlexJamMember(ArrayDimensions = 1, Name = "clientChallenge", Type = FlexJamType.UInt8)]
		public byte[] ClientChallenge { get; set; }
	}
}
