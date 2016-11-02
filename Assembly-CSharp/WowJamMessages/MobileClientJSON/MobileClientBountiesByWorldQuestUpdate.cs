using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobileClientJSON
{
	[FlexJamMessage(Id = 4856, Name = "MobileClientBountiesByWorldQuestUpdate", Version = 33577221u)]
	[System.Runtime.Serialization.DataContract]
	public class MobileClientBountiesByWorldQuestUpdate
	{
		[FlexJamMember(ArrayDimensions = 1, Name = "quest", Type = FlexJamType.Struct)]
		[System.Runtime.Serialization.DataMember(Name = "quest")]
		public MobileBountiesByWorldQuest[] Quest { get; set; }
	}
}
