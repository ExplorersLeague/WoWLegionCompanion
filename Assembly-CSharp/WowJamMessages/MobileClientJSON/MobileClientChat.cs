using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobileClientJSON
{
	[FlexJamMessage(Id = 4844, Name = "MobileClientChat", Version = 28333852u)]
	[System.Runtime.Serialization.DataContract]
	public class MobileClientChat
	{
		public MobileClientChat()
		{
			this.SenderGUID = "0000000000000000";
			this.SenderName = string.Empty;
			this.Prefix = string.Empty;
			this.Channel = string.Empty;
			this.ChatText = string.Empty;
			this.ChatFlags = 0;
		}

		[FlexJamMember(Name = "senderName", Type = FlexJamType.String)]
		[System.Runtime.Serialization.DataMember(Name = "senderName")]
		public string SenderName { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "senderGUID")]
		[FlexJamMember(Name = "senderGUID", Type = FlexJamType.WowGuid)]
		public string SenderGUID { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "chatText")]
		[FlexJamMember(Name = "chatText", Type = FlexJamType.String)]
		public string ChatText { get; set; }

		[FlexJamMember(Name = "prefix", Type = FlexJamType.String)]
		[System.Runtime.Serialization.DataMember(Name = "prefix")]
		public string Prefix { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "channel")]
		[FlexJamMember(Name = "channel", Type = FlexJamType.String)]
		public string Channel { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "slashCmd")]
		[FlexJamMember(Name = "slashCmd", Type = FlexJamType.UInt8)]
		public byte SlashCmd { get; set; }

		[FlexJamMember(Name = "chatFlags", Type = FlexJamType.UInt16)]
		[System.Runtime.Serialization.DataMember(Name = "chatFlags")]
		public ushort ChatFlags { get; set; }
	}
}
