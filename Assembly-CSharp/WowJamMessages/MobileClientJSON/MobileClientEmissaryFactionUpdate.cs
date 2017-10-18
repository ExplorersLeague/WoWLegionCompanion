using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobileClientJSON
{
	[FlexJamMessage(Id = 4859, Name = "MobileClientEmissaryFactionUpdate", Version = 39869590u)]
	[System.Runtime.Serialization.DataContract]
	public class MobileClientEmissaryFactionUpdate
	{
		[FlexJamMember(ArrayDimensions = 1, Name = "faction", Type = FlexJamType.Struct)]
		[System.Runtime.Serialization.DataMember(Name = "faction")]
		public MobileEmissaryFaction[] Faction { get; set; }
	}
}
