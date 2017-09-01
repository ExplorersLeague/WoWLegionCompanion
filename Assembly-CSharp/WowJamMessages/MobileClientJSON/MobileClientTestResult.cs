using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobileClientJSON
{
	[FlexJamMessage(Id = 4841, Name = "MobileClientTestResult", Version = 39869590u)]
	[System.Runtime.Serialization.DataContract]
	public class MobileClientTestResult
	{
		[System.Runtime.Serialization.DataMember(Name = "result")]
		[FlexJamMember(Name = "result", Type = FlexJamType.String)]
		public string Result { get; set; }
	}
}
