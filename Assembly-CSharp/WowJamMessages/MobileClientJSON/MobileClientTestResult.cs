using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobileClientJSON
{
	[FlexJamMessage(Id = 4831, Name = "MobileClientTestResult", Version = 33577221u)]
	[System.Runtime.Serialization.DataContract]
	public class MobileClientTestResult
	{
		[System.Runtime.Serialization.DataMember(Name = "result")]
		[FlexJamMember(Name = "result", Type = FlexJamType.String)]
		public string Result { get; set; }
	}
}
