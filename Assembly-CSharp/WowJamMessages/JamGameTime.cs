using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages
{
	[FlexJamStruct(Name = "JamGameTime", Version = 28333852u)]
	[System.Runtime.Serialization.DataContract]
	public class JamGameTime
	{
		public JamGameTime()
		{
			this.BillingType = 0;
			this.MinutesRemaining = 0u;
			this.IsInIGR = false;
			this.IsPaidForByIGR = false;
			this.IsCAISEnabled = false;
		}

		[FlexJamMember(Name = "minutesRemaining", Type = FlexJamType.UInt32)]
		[System.Runtime.Serialization.DataMember(Name = "minutesRemaining")]
		public uint MinutesRemaining { get; set; }

		[FlexJamMember(Name = "isInIGR", Type = FlexJamType.Bool)]
		[System.Runtime.Serialization.DataMember(Name = "isInIGR")]
		public bool IsInIGR { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "isCAISEnabled")]
		[FlexJamMember(Name = "isCAISEnabled", Type = FlexJamType.Bool)]
		public bool IsCAISEnabled { get; set; }

		[FlexJamMember(Name = "billingType", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "billingType")]
		public int BillingType { get; set; }

		[FlexJamMember(Name = "isPaidForByIGR", Type = FlexJamType.Bool)]
		[System.Runtime.Serialization.DataMember(Name = "isPaidForByIGR")]
		public bool IsPaidForByIGR { get; set; }
	}
}
