using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobileClientJSON
{
	[FlexJamMessage(Id = 4843, Name = "MobileClientGarrisonDataRequestResult", Version = 39869590u)]
	[System.Runtime.Serialization.DataContract]
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

		[FlexJamMember(Name = "characterLevel", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "characterLevel")]
		public int CharacterLevel { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "serverTime")]
		[FlexJamMember(Name = "serverTime", Type = FlexJamType.Int64)]
		public long ServerTime { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "dailyMissionCount")]
		[FlexJamMember(Name = "dailyMissionCount", Type = FlexJamType.Int32)]
		public int DailyMissionCount { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "resourcesCurrency")]
		[FlexJamMember(Name = "resourcesCurrency", Type = FlexJamType.Int32)]
		public int ResourcesCurrency { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "talent")]
		[FlexJamMember(ArrayDimensions = 1, Name = "talent", Type = FlexJamType.Struct)]
		public JamGarrisonTalent[] Talent { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "characterName")]
		[FlexJamMember(Name = "characterName", Type = FlexJamType.String)]
		public string CharacterName { get; set; }

		[FlexJamMember(Name = "garrTypeID", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "garrTypeID")]
		public int GarrTypeID { get; set; }
	}
}
