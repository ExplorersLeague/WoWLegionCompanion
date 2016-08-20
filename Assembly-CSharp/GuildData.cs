using System;
using System.Collections.Generic;
using WowJamMessages.MobileClientJSON;

public class GuildData
{
	public static GuildData instance
	{
		get
		{
			if (GuildData.s_instance == null)
			{
				GuildData.s_instance = new GuildData();
				GuildData.s_instance.m_guildMemberDictionary = new Dictionary<string, GuildData.GuildMember>();
			}
			return GuildData.s_instance;
		}
	}

	public static Dictionary<string, GuildData.GuildMember> guildMemberDictionary
	{
		get
		{
			return GuildData.instance.m_guildMemberDictionary;
		}
	}

	public static void AddGuildMember(MobileGuildMember mobileGuildMember)
	{
		if (!GuildData.instance.m_guildMemberDictionary.ContainsKey(mobileGuildMember.Guid))
		{
			GuildData.GuildMember guildMember = new GuildData.GuildMember();
			guildMember.m_mobileGuildMember = mobileGuildMember;
			guildMember.m_isLoggedIn = true;
			GuildData.instance.m_guildMemberDictionary.Add(mobileGuildMember.Guid, guildMember);
		}
	}

	public static void RemoveGuildMember(string mobileGuildMemberGUID)
	{
		if (GuildData.instance.m_guildMemberDictionary.ContainsKey(mobileGuildMemberGUID))
		{
			GuildData.instance.m_guildMemberDictionary.Remove(mobileGuildMemberGUID);
		}
	}

	public static void ClearData()
	{
		GuildData.instance.m_guildMemberDictionary.Clear();
	}

	private static GuildData s_instance;

	private Dictionary<string, GuildData.GuildMember> m_guildMemberDictionary;

	public class GuildMember
	{
		public MobileGuildMember m_mobileGuildMember;

		public bool m_isLoggedIn;
	}
}
