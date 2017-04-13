using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobileClientJSON
{
	[FlexJamStruct(Name = "MobileContribution", Version = 39869590u)]
	[System.Runtime.Serialization.DataContract]
	public class MobileContribution
	{
		[System.Runtime.Serialization.DataMember(Name = "contributionCurrencyType")]
		[FlexJamMember(Name = "contributionCurrencyType", Type = FlexJamType.Int32)]
		public int ContributionCurrencyType { get; set; }

		[FlexJamMember(Name = "unitCompletion", Type = FlexJamType.Float)]
		[System.Runtime.Serialization.DataMember(Name = "unitCompletion")]
		public float UnitCompletion { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "currentValue")]
		[FlexJamMember(Name = "currentValue", Type = FlexJamType.Float)]
		public float CurrentValue { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "state")]
		[FlexJamMember(Name = "state", Type = FlexJamType.Int32)]
		public int State { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "uitextureAtlasMemberIDDestroyed")]
		[FlexJamMember(Name = "uitextureAtlasMemberIDDestroyed", Type = FlexJamType.Int32)]
		public int UitextureAtlasMemberIDDestroyed { get; set; }

		[FlexJamMember(Name = "upperValue", Type = FlexJamType.Float)]
		[System.Runtime.Serialization.DataMember(Name = "upperValue")]
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

		[FlexJamMember(Name = "contributionID", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "contributionID")]
		public int ContributionID { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "description")]
		[FlexJamMember(Name = "description", Type = FlexJamType.String)]
		public string Description { get; set; }

		[FlexJamMember(Name = "name", Type = FlexJamType.String)]
		[System.Runtime.Serialization.DataMember(Name = "name")]
		public string Name { get; set; }

		[FlexJamMember(Name = "uitextureAtlasMemberIDUnderAttack", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "uitextureAtlasMemberIDUnderAttack")]
		public int UitextureAtlasMemberIDUnderAttack { get; set; }

		[FlexJamMember(Name = "contributionCurrencyCost", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "contributionCurrencyCost")]
		public int ContributionCurrencyCost { get; set; }
	}
}
