using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobileClientJSON
{
	[System.Runtime.Serialization.DataContract]
	[FlexJamMessage(Id = 4843, Name = "MobileClientGarrisonDataRequestResult", Version = 39869590u)]
	public class MobileClientGarrisonDataRequestResult
	{
		[FlexJamMember(Name = "orderhallResourcesCurrency", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "orderhallResourcesCurrency")]
		public int OrderhallResourcesCurrency { get; set; }

		[FlexJamMember(Name = "pvpFaction", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "pvpFaction")]
		public int PvpFaction { get; set; }

		[FlexJamMember(ArrayDimensions = 1, Name = "mission", Type = FlexJamType.Struct)]
		[System.Runtime.Serialization.DataMember(Name = "mission")]
		public JamGarrisonMobileMission[] Mission { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "oilCurrency")]
		[FlexJamMember(Name = "oilCurrency", Type = FlexJamType.Int32)]
		public int OilCurrency { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "follower")]
		[FlexJamMember(ArrayDimensions = 1, Name = "follower", Type = FlexJamType.Struct)]
		public JamGarrisonFollower[] Follower { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "characterClassID")]
		[FlexJamMember(Name = "characterClassID", Type = FlexJamType.Int32)]
		public int CharacterClassID { get; set; }

		[FlexJamMember(Name = "goldCurrency", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "goldCurrency")]
		public int GoldCurrency { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "characterLevel")]
		[FlexJamMember(Name = "characterLevel", Type = FlexJamType.Int32)]
		public int CharacterLevel { get; set; }

		[FlexJamMember(Name = "serverTime", Type = FlexJamType.Int64)]
		[System.Runtime.Serialization.DataMember(Name = "serverTime")]
		public long ServerTime { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "dailyMissionCount")]
		[FlexJamMember(Name = "dailyMissionCount", Type = FlexJamType.Int32)]
		public int DailyMissionCount { get; set; }

		[FlexJamMember(Name = "resourcesCurrency", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "resourcesCurrency")]
		public int ResourcesCurrency { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "talent")]
		[FlexJamMember(ArrayDimensions = 1, Name = "talent", Type = FlexJamType.Struct)]
		public JamGarrisonTalent[] Talent { get; set; }

		[FlexJamMember(Name = "characterName", Type = FlexJamType.String)]
		[System.Runtime.Serialization.DataMember(Name = "characterName")]
		public string CharacterName { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "garrTypeID")]
		[FlexJamMember(Name = "garrTypeID", Type = FlexJamType.Int32)]
		public int GarrTypeID { get; set; }
	}
}
