using System;
using UnityEngine;
using UnityEngine.UI;
using WowStatConstants;

public class PartyBuffsPopup : MonoBehaviour
{
	private void Awake()
	{
		this.m_partyBuffsLabel.font = GeneralHelpers.LoadStandardFont();
		this.m_partyBuffsLabel.text = StaticDB.GetString("PARTY_BUFFS", "Party Buffs [PH]");
	}

	public void OnEnable()
	{
		Main.instance.m_UISound.Play_ShowGenericTooltip();
		Main.instance.m_canvasBlurManager.AddBlurRef_MainCanvas();
		Main.instance.m_canvasBlurManager.AddBlurRef_Level2Canvas();
		Main.instance.m_backButtonManager.PushBackAction(BackAction.hideAllPopups, null);
	}

	private void OnDisable()
	{
		Main.instance.m_canvasBlurManager.RemoveBlurRef_MainCanvas();
		Main.instance.m_canvasBlurManager.RemoveBlurRef_Level2Canvas();
		Main.instance.m_backButtonManager.PopBackAction();
	}

	public void Init(int[] buffIDs)
	{
		PartyBuffDisplay[] componentsInChildren = this.m_partyBuffRoot.GetComponentsInChildren<PartyBuffDisplay>(true);
		foreach (PartyBuffDisplay partyBuffDisplay in componentsInChildren)
		{
			Object.DestroyImmediate(partyBuffDisplay.gameObject);
		}
		foreach (int ability in buffIDs)
		{
			PartyBuffDisplay partyBuffDisplay2 = Object.Instantiate<PartyBuffDisplay>(this.m_partyBuffDisplayPrefab);
			partyBuffDisplay2.transform.SetParent(this.m_partyBuffRoot.transform, false);
			partyBuffDisplay2.SetAbility(ability);
		}
	}

	public Text m_partyBuffsLabel;

	public PartyBuffDisplay m_partyBuffDisplayPrefab;

	public GameObject m_partyBuffRoot;
}
