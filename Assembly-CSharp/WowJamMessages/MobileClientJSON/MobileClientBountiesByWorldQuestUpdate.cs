using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobileClientJSON
{
	[System.Runtime.Serialization.DataContract]
	[FlexJamMessage(Id = 4856, Name = "MobileClientBountiesByWorldQuestUpdate", Version = 33577221u)]
	public class MobileClientBountiesByWorldQuestUpdate
	{
		[System.Runtime.Serialization.DataMember(Name = "quest")]
		[FlexJamMember(ArrayDimensions = 1, Name = "quest", Type = FlexJamType.Struct)]
		public MobileBountiesByWorldQuest[] Quest { get; set; }
	}
}
