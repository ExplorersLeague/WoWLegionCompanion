using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobileClientJSON
{
	[System.Runtime.Serialization.DataContract]
	[FlexJamStruct(Name = "MobileContribution", Version = 39869590u)]
	public class MobileContribution
	{
		[FlexJamMember(Name = "contributionCurrencyType", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "contributionCurrencyType")]
		public int ContributionCurrencyType { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "unitCompletion")]
		[FlexJamMember(Name = "unitCompletion", Type = FlexJamType.Float)]
		public float UnitCompletion { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "currentValue")]
		[FlexJamMember(Name = "currentValue", Type = FlexJamType.Float)]
		public float CurrentValue { get; set; }

		[FlexJamMember(Name = "state", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "state")]
		public int State { get; set; }

		[FlexJamMember(Name = "uitextureAtlasMemberIDDestroyed", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "uitextureAtlasMemberIDDestroyed")]
		public int UitextureAtlasMemberIDDestroyed { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "upperValue")]
		[FlexJamMember(Name = "upperValue", Type = FlexJamType.Float)]
		public float UpperValue { get; set; }

		[FlexJamMember(Name = "uitextureAtlasMemberIDUnderConstruction", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "uitextureAtlasMemberIDUnderConstruction")]
		public int UitextureAtlasMemberIDUnderConstruction { get; set; }

		[FlexJamMember(Name = "uitextureAtlasMemberIDActive", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "uitextureAtlasMemberIDActive")]
		public int UitextureAtlasMemberIDActive { get; set; }

		[FlexJamMember(ArrayDimensions = 1, Name = "spell", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "spell")]
		public int[] Spell { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "contributionID")]
		[FlexJamMember(Name = "contributionID", Type = FlexJamType.Int32)]
		public int ContributionID { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "description")]
		[FlexJamMember(Name = "description", Type = FlexJamType.String)]
		public string Description { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "name")]
		[FlexJamMember(Name = "name", Type = FlexJamType.String)]
		public string Name { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "uitextureAtlasMemberIDUnderAttack")]
		[FlexJamMember(Name = "uitextureAtlasMemberIDUnderAttack", Type = FlexJamType.Int32)]
		public int UitextureAtlasMemberIDUnderAttack { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "contributionCurrencyCost")]
		[FlexJamMember(Name = "contributionCurrencyCost", Type = FlexJamType.Int32)]
		public int ContributionCurrencyCost { get; set; }
	}
}
