using System;
using UnityEngine;
using WowJamMessages;

public class TestFollowerExperiencePanel : MonoBehaviour
{
	public void Test()
	{
		JamGarrisonFollower jamGarrisonFollower = new JamGarrisonFollower();
		JamGarrisonFollower jamGarrisonFollower2 = new JamGarrisonFollower();
		jamGarrisonFollower.Quality = 1;
		jamGarrisonFollower.FollowerLevel = 108;
		jamGarrisonFollower.Xp = 2400;
		jamGarrisonFollower.GarrFollowerID = 616;
		jamGarrisonFollower2.Quality = 1;
		jamGarrisonFollower2.FollowerLevel = 109;
		jamGarrisonFollower2.Xp = 124;
		jamGarrisonFollower2.GarrFollowerID = 616;
		this.m_testXPDisplay[0].SetFollower(jamGarrisonFollower, jamGarrisonFollower2, 0f);
		JamGarrisonFollower jamGarrisonFollower3 = new JamGarrisonFollower();
		JamGarrisonFollower jamGarrisonFollower4 = new JamGarrisonFollower();
		jamGarrisonFollower3.Quality = 2;
		jamGarrisonFollower3.FollowerLevel = 109;
		jamGarrisonFollower3.Xp = 2650;
		jamGarrisonFollower3.GarrFollowerID = 621;
		jamGarrisonFollower4.Quality = 2;
		jamGarrisonFollower4.FollowerLevel = 110;
		jamGarrisonFollower4.Xp = 777;
		jamGarrisonFollower4.GarrFollowerID = 621;
		this.m_testXPDisplay[1].SetFollower(jamGarrisonFollower3, jamGarrisonFollower4, 0.5f);
		JamGarrisonFollower jamGarrisonFollower5 = new JamGarrisonFollower();
		JamGarrisonFollower jamGarrisonFollower6 = new JamGarrisonFollower();
		jamGarrisonFollower5.Quality = 3;
		jamGarrisonFollower5.FollowerLevel = 110;
		jamGarrisonFollower5.Xp = 57000;
		jamGarrisonFollower5.GarrFollowerID = 617;
		jamGarrisonFollower6.Quality = 4;
		jamGarrisonFollower6.FollowerLevel = 110;
		jamGarrisonFollower6.Xp = 0;
		jamGarrisonFollower6.GarrFollowerID = 617;
		this.m_testXPDisplay[2].SetFollower(jamGarrisonFollower5, jamGarrisonFollower6, 1f);
		JamGarrisonFollower jamGarrisonFollower7 = new JamGarrisonFollower();
		jamGarrisonFollower7.Quality = 3;
		jamGarrisonFollower7.FollowerLevel = 110;
		jamGarrisonFollower7.Xp = 57000;
		jamGarrisonFollower7.GarrFollowerID = 729;
		jamGarrisonFollower7.Flags = 8;
		jamGarrisonFollower7.Durability = 0;
		this.m_testXPDisplay[3].SetFollower(jamGarrisonFollower7, null, 1.5f);
	}

	public FollowerExperienceDisplay[] m_testXPDisplay;
}
