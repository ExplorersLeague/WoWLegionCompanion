using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobileClientJSON
{
	[FlexJamMessage(Id = 4854, Name = "MobileClientWorldQuestUpdate", Version = 28333852u)]
	[System.Runtime.Serialization.DataContract]
	public class MobileClientWorldQuestUpdate
	{
		[FlexJamMember(ArrayDimensions = 1, Name = "quest", Type = FlexJamType.Struct)]
		[System.Runtime.Serialization.DataMember(Name = "quest")]
		public MobileWorldQuest[] Quest { get; set; }
	}
}
