using System;
using UnityEngine;
using UnityEngine.UI;

namespace WoWCompanionApp
{
	public class PartyBuffsPopup : BaseDialog
	{
		private void Awake()
		{
		}

		public void Init(int[] buffIDs)
		{
			PartyBuffDisplay[] componentsInChildren = this.m_partyBuffRoot.GetComponentsInChildren<PartyBuffDisplay>(true);
			foreach (PartyBuffDisplay partyBuffDisplay in componentsInChildren)
			{
				Object.Destroy(partyBuffDisplay.gameObject);
			}
			foreach (int ability in buffIDs)
			{
				PartyBuffDisplay partyBuffDisplay2 = Object.Instantiate<PartyBuffDisplay>(this.m_partyBuffDisplayPrefab);
				partyBuffDisplay2.transform.SetParent(this.m_partyBuffRoot.transform, false);
				partyBuffDisplay2.SetAbility(ability);
				if (buffIDs.Length > 7)
				{
					partyBuffDisplay2.UseReducedHeight();
					if (buffIDs.Length > 9)
					{
						VerticalLayoutGroup component = this.m_partyBuffRoot.GetComponent<VerticalLayoutGroup>();
						if (component != null)
						{
							component.spacing = 3f;
						}
					}
				}
			}
		}

		public PartyBuffDisplay m_partyBuffDisplayPrefab;

		public GameObject m_partyBuffRoot;
	}
}
