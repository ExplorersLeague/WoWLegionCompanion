using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages
{
	[FlexJamStruct(Name = "JamCurrencySimple", Version = 28333852u)]
	[System.Runtime.Serialization.DataContract]
	public class JamCurrencySimple
	{
		[FlexJamMember(Name = "type", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "type")]
		public int Type { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "quantity")]
		[FlexJamMember(Name = "quantity", Type = FlexJamType.Int32)]
		public int Quantity { get; set; }
	}
}
