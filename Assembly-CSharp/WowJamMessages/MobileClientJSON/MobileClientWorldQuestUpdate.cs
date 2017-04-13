using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobileClientJSON
{
	[FlexJamMessage(Id = 4865, Name = "MobileClientWorldQuestUpdate", Version = 39869590u)]
	[System.Runtime.Serialization.DataContract]
	public class MobileClientWorldQuestUpdate
	{
		[System.Runtime.Serialization.DataMember(Name = "quest")]
		[FlexJamMember(ArrayDimensions = 1, Name = "quest", Type = FlexJamType.Struct)]
		public MobileWorldQuest[] Quest { get; set; }
	}
}
