using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobileClientJSON
{
	[System.Runtime.Serialization.DataContract]
	[FlexJamMessage(Id = 4866, Name = "MobileClientBountiesByWorldQuestUpdate", Version = 39869590u)]
	public class MobileClientBountiesByWorldQuestUpdate
	{
		[System.Runtime.Serialization.DataMember(Name = "quest")]
		[FlexJamMember(ArrayDimensions = 1, Name = "quest", Type = FlexJamType.Struct)]
		public MobileBountiesByWorldQuest[] Quest { get; set; }
	}
}
