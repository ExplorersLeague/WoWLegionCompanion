using System;
using WowJamMessages.JSONRealmList;

namespace WoWCompanionApp
{
	internal class CharacterData : Singleton<CharacterData>
	{
		public string PlayerGuid { get; private set; }

		public string CharacterName { get; private set; }

		public int LastActiveTime { get; private set; }

		public bool HasMobileAccess { get; private set; }

		public byte RaceID { get; private set; }

		public byte SexID { get; private set; }

		public uint VirtualRealmAddress { get; private set; }

		public ulong CommunityID { get; private set; }

		public int Level { get; private set; }

		public void CopyCharacterEntry(JamJSONCharacterEntry characterEntry)
		{
			this.PlayerGuid = characterEntry.PlayerGuid;
			this.CharacterName = characterEntry.Name;
			this.LastActiveTime = characterEntry.LastActiveTime;
			this.HasMobileAccess = characterEntry.HasMobileAccess;
			this.RaceID = characterEntry.RaceID;
			this.SexID = characterEntry.SexID;
			this.VirtualRealmAddress = characterEntry.VirtualRealmAddress;
			this.CommunityID = characterEntry.CommunityID;
			this.Level = (int)characterEntry.ExperienceLevel;
		}
	}
}
