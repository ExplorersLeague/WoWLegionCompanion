using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobileClientJSON
{
	[System.Runtime.Serialization.DataContract]
	[FlexJamMessage(Id = 4859, Name = "MobileClientEmissaryFactionUpdate", Version = 39869590u)]
	public class MobileClientEmissaryFactionUpdate
	{
		[System.Runtime.Serialization.DataMember(Name = "faction")]
		[FlexJamMember(ArrayDimensions = 1, Name = "faction", Type = FlexJamType.Struct)]
		public MobileEmissaryFaction[] Faction { get; set; }
	}
}
