using System;
using UnityEngine;
using UnityEngine.UI;

namespace WoWCompanionApp
{
	public abstract class RosterPageItem<MemberInfoType> : MonoBehaviour
	{
		public abstract void PopulateMemberInfo(MemberInfoType member);

		public Text m_characterName;

		public Image m_classImage;

		protected MemberInfoType m_memberInfo;
	}
}
