using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobileClientJSON
{
	[System.Runtime.Serialization.DataContract]
	[FlexJamMessage(Id = 4865, Name = "MobileClientWorldQuestUpdate", Version = 39869590u)]
	public class MobileClientWorldQuestUpdate
	{
		[FlexJamMember(ArrayDimensions = 1, Name = "quest", Type = FlexJamType.Struct)]
		[System.Runtime.Serialization.DataMember(Name = "quest")]
		public MobileWorldQuest[] Quest { get; set; }
	}
}
