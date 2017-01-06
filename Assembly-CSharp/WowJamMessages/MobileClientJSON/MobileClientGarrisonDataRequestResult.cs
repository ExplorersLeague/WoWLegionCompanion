using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobileClientJSON
{
	[System.Runtime.Serialization.DataContract]
	[FlexJamMessage(Id = 4833, Name = "MobileClientGarrisonDataRequestResult", Version = 33577221u)]
	public class MobileClientGarrisonDataRequestResult
	{
		[FlexJamMember(Name = "orderhallResourcesCurrency", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "orderhallResourcesCurrency")]
		public int OrderhallResourcesCurrency { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "pvpFaction")]
		[FlexJamMember(Name = "pvpFaction", Type = FlexJamType.Int32)]
		public int PvpFaction { get; set; }

		[FlexJamMember(ArrayDimensions = 1, Name = "mission", Type = FlexJamType.Struct)]
		[System.Runtime.Serialization.DataMember(Name = "mission")]
		public JamGarrisonMobileMission[] Mission { get; set; }

		[FlexJamMember(Name = "oilCurrency", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "oilCurrency")]
		public int OilCurrency { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "follower")]
		[FlexJamMember(ArrayDimensions = 1, Name = "follower", Type = FlexJamType.Struct)]
		public JamGarrisonFollower[] Follower { get; set; }

		[FlexJamMember(Name = "characterClassID", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "characterClassID")]
		public int CharacterClassID { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "goldCurrency")]
		[FlexJamMember(Name = "goldCurrency", Type = FlexJamType.Int32)]
		public int GoldCurrency { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "characterLevel")]
		[FlexJamMember(Name = "characterLevel", Type = FlexJamType.Int32)]
		public int CharacterLevel { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "serverTime")]
		[FlexJamMember(Name = "serverTime", Type = FlexJamType.Int64)]
		public long ServerTime { get; set; }

		[FlexJamMember(Name = "dailyMissionCount", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "dailyMissionCount")]
		public int DailyMissionCount { get; set; }

		[FlexJamMember(Name = "resourcesCurrency", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "resourcesCurrency")]
		public int ResourcesCurrency { get; set; }

		[FlexJamMember(ArrayDimensions = 1, Name = "talent", Type = FlexJamType.Struct)]
		[System.Runtime.Serialization.DataMember(Name = "talent")]
		public JamGarrisonTalent[] Talent { get; set; }

		[FlexJamMember(Name = "characterName", Type = FlexJamType.String)]
		[System.Runtime.Serialization.DataMember(Name = "characterName")]
		public string CharacterName { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "garrTypeID")]
		[FlexJamMember(Name = "garrTypeID", Type = FlexJamType.Int32)]
		public int GarrTypeID { get; set; }
	}
}
