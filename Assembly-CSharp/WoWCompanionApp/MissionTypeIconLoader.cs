using System;
using UnityEngine;
using WowStaticData;

namespace WoWCompanionApp
{
	public class MissionTypeIconLoader : Singleton<MissionTypeIconLoader>
	{
		public Sprite GetMissionTypeIconByMissionID(int missionID)
		{
			GarrMissionRec record = StaticDB.garrMissionDB.GetRecord(missionID);
			if (record == null)
			{
				return this.m_normalSprite;
			}
			GarrMissionTypeRec record2 = StaticDB.garrMissionTypeDB.GetRecord((int)record.GarrMissionTypeID);
			ushort uiTextureAtlasMemberID = record2.UiTextureAtlasMemberID;
			switch (uiTextureAtlasMemberID)
			{
			case 7746:
				return this.m_starSprite;
			case 7747:
				return this.m_swordSprite;
			case 7748:
				return this.m_bootSprite;
			case 7749:
				return this.m_stealthSprite;
			default:
				if (uiTextureAtlasMemberID == 8046)
				{
					return this.m_scrollSprite;
				}
				if (uiTextureAtlasMemberID != 8778)
				{
					return this.m_normalSprite;
				}
				return this.m_treasureChestSprite;
			}
		}

		public Sprite m_starSprite;

		public Sprite m_swordSprite;

		public Sprite m_bootSprite;

		public Sprite m_stealthSprite;

		public Sprite m_scrollSprite;

		public Sprite m_treasureChestSprite;

		public Sprite m_normalSprite;
	}
}
