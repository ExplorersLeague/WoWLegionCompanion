using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobilePlayerJSON
{
	[FlexJamMessage(Id = 4789, Name = "MobilePlayerChat", Version = 33577221u)]
	[System.Runtime.Serialization.DataContract]
	public class MobilePlayerChat
	{
		public MobilePlayerChat()
		{
			this.TargetName = string.Empty;
			this.ChatText = string.Empty;
		}

		[FlexJamMember(Name = "slashCmd", Type = FlexJamType.UInt8)]
		[System.Runtime.Serialization.DataMember(Name = "slashCmd")]
		public byte SlashCmd { get; set; }

		[FlexJamMember(Name = "chatText", Type = FlexJamType.String)]
		[System.Runtime.Serialization.DataMember(Name = "chatText")]
		public string ChatText { get; set; }

		[FlexJamMember(Name = "targetName", Type = FlexJamType.String)]
		[System.Runtime.Serialization.DataMember(Name = "targetName")]
		public string TargetName { get; set; }
	}
}
