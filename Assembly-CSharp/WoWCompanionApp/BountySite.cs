using System;
using UnityEngine;
using UnityEngine.UI;
using WowStatConstants;

namespace WoWCompanionApp
{
	public class BountySite : MonoBehaviour
	{
		private void Awake()
		{
			this.originalScale = base.transform.localScale;
		}

		private void OnEnable()
		{
			base.transform.localScale = this.originalScale;
		}

		public void SetBounty(WrapperWorldQuestBounty bounty)
		{
			this.m_bounty = bounty;
			Sprite sprite = GeneralHelpers.LoadIconAsset(AssetBundleType.Icons, bounty.IconFileDataID);
			if (sprite != null)
			{
				this.m_invalidFileDataID.gameObject.SetActive(false);
				this.m_emissaryIcon.sprite = sprite;
			}
			else
			{
				this.m_invalidFileDataID.gameObject.SetActive(true);
				this.m_invalidFileDataID.text = string.Empty + bounty.IconFileDataID;
			}
		}

		public void OnTap()
		{
			UiAnimMgr.instance.PlayAnim("MinimapPulseAnim", base.transform, Vector3.zero, 3f, 0f);
			AllPopups.instance.ShowBountyInfoTooltip(this.m_bounty);
			Main.instance.m_UISound.Play_SelectWorldQuest();
		}

		private void Update()
		{
		}

		public Image m_emissaryIcon;

		public Text m_invalidFileDataID;

		public Image m_errorImage;

		private WrapperWorldQuestBounty m_bounty;

		private Vector3 originalScale;
	}
}
