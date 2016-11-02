using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobileClientJSON
{
	[System.Runtime.Serialization.DataContract]
	[FlexJamMessage(Id = 4831, Name = "MobileClientTestResult", Version = 33577221u)]
	public class MobileClientTestResult
	{
		[FlexJamMember(Name = "result", Type = FlexJamType.String)]
		[System.Runtime.Serialization.DataMember(Name = "result")]
		public string Result { get; set; }
	}
}
