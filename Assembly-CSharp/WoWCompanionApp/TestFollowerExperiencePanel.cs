using System;
using UnityEngine;

namespace WoWCompanionApp
{
	public class TestFollowerExperiencePanel : MonoBehaviour
	{
		public void Test()
		{
			WrapperGarrisonFollower oldFollower = default(WrapperGarrisonFollower);
			WrapperGarrisonFollower newFollower = default(WrapperGarrisonFollower);
			oldFollower.Quality = 1;
			oldFollower.FollowerLevel = 108;
			oldFollower.Xp = 2400;
			oldFollower.GarrFollowerID = 616;
			newFollower.Quality = 1;
			newFollower.FollowerLevel = 109;
			newFollower.Xp = 124;
			newFollower.GarrFollowerID = 616;
			this.m_testXPDisplay[0].SetFollower(oldFollower, newFollower, 0f);
			WrapperGarrisonFollower oldFollower2 = default(WrapperGarrisonFollower);
			WrapperGarrisonFollower newFollower2 = default(WrapperGarrisonFollower);
			oldFollower2.Quality = 2;
			oldFollower2.FollowerLevel = 109;
			oldFollower2.Xp = 2650;
			oldFollower2.GarrFollowerID = 621;
			newFollower2.Quality = 2;
			newFollower2.FollowerLevel = 110;
			newFollower2.Xp = 777;
			newFollower2.GarrFollowerID = 621;
			this.m_testXPDisplay[1].SetFollower(oldFollower2, newFollower2, 0.5f);
			WrapperGarrisonFollower oldFollower3 = default(WrapperGarrisonFollower);
			WrapperGarrisonFollower newFollower3 = default(WrapperGarrisonFollower);
			oldFollower3.Quality = 3;
			oldFollower3.FollowerLevel = 110;
			oldFollower3.Xp = 57000;
			oldFollower3.GarrFollowerID = 617;
			newFollower3.Quality = 4;
			newFollower3.FollowerLevel = 110;
			newFollower3.Xp = 0;
			newFollower3.GarrFollowerID = 617;
			this.m_testXPDisplay[2].SetFollower(oldFollower3, newFollower3, 1f);
			WrapperGarrisonFollower oldFollower4 = default(WrapperGarrisonFollower);
			WrapperGarrisonFollower newFollower4 = default(WrapperGarrisonFollower);
			newFollower4 = default(WrapperGarrisonFollower);
			newFollower4.GarrFollowerID = oldFollower4.GarrFollowerID;
			newFollower4.Quality = oldFollower4.Quality;
			newFollower4.Durability = 0;
			oldFollower4.Quality = 3;
			oldFollower4.FollowerLevel = 110;
			oldFollower4.Xp = 57000;
			oldFollower4.GarrFollowerID = 729;
			oldFollower4.Flags = 8;
			oldFollower4.Durability = 0;
			this.m_testXPDisplay[3].SetFollower(oldFollower4, newFollower4, 1.5f);
		}

		public FollowerExperienceDisplay[] m_testXPDisplay;
	}
}
