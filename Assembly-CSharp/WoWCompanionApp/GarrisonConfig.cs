using System;
using UnityEngine;
using WowStatConstants;

namespace WoWCompanionApp
{
	[CreateAssetMenu(menuName = "WoWCompanion/GarrisonConfig")]
	internal class GarrisonConfig : ScriptableObject
	{
		public GARR_TYPE m_garrisonType;

		public GARR_FOLLOWER_TYPE m_garrrisonFollowerType;
	}
}
