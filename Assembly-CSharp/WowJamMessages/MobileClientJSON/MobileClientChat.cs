using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobileClientJSON
{
	[System.Runtime.Serialization.DataContract]
	[FlexJamMessage(Id = 4854, Name = "MobileClientChat", Version = 39869590u)]
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

		[System.Runtime.Serialization.DataMember(Name = "senderName")]
		[FlexJamMember(Name = "senderName", Type = FlexJamType.String)]
		public string SenderName { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "senderGUID")]
		[FlexJamMember(Name = "senderGUID", Type = FlexJamType.WowGuid)]
		public string SenderGUID { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "chatText")]
		[FlexJamMember(Name = "chatText", Type = FlexJamType.String)]
		public string ChatText { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "prefix")]
		[FlexJamMember(Name = "prefix", Type = FlexJamType.String)]
		public string Prefix { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "channel")]
		[FlexJamMember(Name = "channel", Type = FlexJamType.String)]
		public string Channel { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "slashCmd")]
		[FlexJamMember(Name = "slashCmd", Type = FlexJamType.UInt8)]
		public byte SlashCmd { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "chatFlags")]
		[FlexJamMember(Name = "chatFlags", Type = FlexJamType.UInt16)]
		public ushort ChatFlags { get; set; }
	}
}
