using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobileClientJSON
{
	[FlexJamMessage(Id = 4849, Name = "MobileClientEmissaryFactionUpdate", Version = 28333852u)]
	[System.Runtime.Serialization.DataContract]
	public class MobileClientEmissaryFactionUpdate
	{
		[FlexJamMember(ArrayDimensions = 1, Name = "faction", Type = FlexJamType.Struct)]
		[System.Runtime.Serialization.DataMember(Name = "faction")]
		public MobileEmissaryFaction[] Faction { get; set; }
	}
}
