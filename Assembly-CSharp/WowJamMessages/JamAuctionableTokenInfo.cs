using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages
{
	[System.Runtime.Serialization.DataContract]
	[FlexJamStruct(Name = "JamAuctionableTokenInfo", Version = 28333852u)]
	public class JamAuctionableTokenInfo
	{
		[FlexJamMember(Name = "price", Type = FlexJamType.UInt64)]
		[System.Runtime.Serialization.DataMember(Name = "price")]
		public ulong Price { get; set; }

		[FlexJamMember(Name = "status", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "status")]
		public int Status { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "expectedSecondsUntilSold")]
		[FlexJamMember(Name = "expectedSecondsUntilSold", Type = FlexJamType.UInt32)]
		public uint ExpectedSecondsUntilSold { get; set; }

		[FlexJamMember(Name = "id", Type = FlexJamType.UInt64)]
		[System.Runtime.Serialization.DataMember(Name = "id")]
		public ulong Id { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "lastUpdate")]
		[FlexJamMember(Name = "lastUpdate", Type = FlexJamType.Int32)]
		public int LastUpdate { get; set; }
	}
}
