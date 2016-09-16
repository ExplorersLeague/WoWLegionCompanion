using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages
{
	[FlexJamStruct(Name = "JamCliPetitionInfo", Version = 28333852u)]
	[System.Runtime.Serialization.DataContract]
	public class JamCliPetitionInfo
	{
		public JamCliPetitionInfo()
		{
			this.M_choicetext = new string[10];
		}

		[System.Runtime.Serialization.DataMember(Name = "m_allowedMinLevel")]
		[FlexJamMember(Name = "m_allowedMinLevel", Type = FlexJamType.Int32)]
		public int M_allowedMinLevel { get; set; }

		[FlexJamMember(Name = "m_allowedClasses", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "m_allowedClasses")]
		public int M_allowedClasses { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "m_allowedGender")]
		[FlexJamMember(Name = "m_allowedGender", Type = FlexJamType.Int16)]
		public short M_allowedGender { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "m_deadLine")]
		[FlexJamMember(Name = "m_deadLine", Type = FlexJamType.Int32)]
		public int M_deadLine { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "m_maxSignatures")]
		[FlexJamMember(Name = "m_maxSignatures", Type = FlexJamType.Int32)]
		public int M_maxSignatures { get; set; }

		[FlexJamMember(Name = "m_title", Type = FlexJamType.String)]
		[System.Runtime.Serialization.DataMember(Name = "m_title")]
		public string M_title { get; set; }

		[FlexJamMember(Name = "m_muid", Type = FlexJamType.UInt32)]
		[System.Runtime.Serialization.DataMember(Name = "m_muid")]
		public uint M_muid { get; set; }

		[FlexJamMember(Name = "m_petitioner", Type = FlexJamType.WowGuid)]
		[System.Runtime.Serialization.DataMember(Name = "m_petitioner")]
		public string M_petitioner { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "m_bodyText")]
		[FlexJamMember(Name = "m_bodyText", Type = FlexJamType.String)]
		public string M_bodyText { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "m_allowedMaxLevel")]
		[FlexJamMember(Name = "m_allowedMaxLevel", Type = FlexJamType.Int32)]
		public int M_allowedMaxLevel { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "m_minSignatures")]
		[FlexJamMember(Name = "m_minSignatures", Type = FlexJamType.Int32)]
		public int M_minSignatures { get; set; }

		[FlexJamMember(Name = "m_staticType", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "m_staticType")]
		public int M_staticType { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "m_numChoices")]
		[FlexJamMember(Name = "m_numChoices", Type = FlexJamType.Int32)]
		public int M_numChoices { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "m_issueDate")]
		[FlexJamMember(Name = "m_issueDate", Type = FlexJamType.Int32)]
		public int M_issueDate { get; set; }

		[FlexJamMember(Name = "m_allowedRaces", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "m_allowedRaces")]
		public int M_allowedRaces { get; set; }

		[FlexJamMember(Name = "m_petitionID", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "m_petitionID")]
		public int M_petitionID { get; set; }

		[FlexJamMember(Name = "m_allowedGuildID", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "m_allowedGuildID")]
		public int M_allowedGuildID { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "m_choicetext")]
		[FlexJamMember(ArrayDimensions = 1, Name = "m_choicetext", Type = FlexJamType.String)]
		public string[] M_choicetext { get; set; }
	}
}
