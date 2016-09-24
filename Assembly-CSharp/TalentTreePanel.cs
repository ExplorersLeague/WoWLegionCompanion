using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using WowJamMessages.MobilePlayerJSON;
using WowStaticData;

public class TalentTreePanel : MonoBehaviour
{
	private void OnEnable()
	{
		Main instance = Main.instance;
		instance.GarrisonDataResetFinishedAction = (Action)Delegate.Combine(instance.GarrisonDataResetFinishedAction, new Action(this.HandleGarrisonDataResetFinished));
		this.HandleEnteredWorld();
		this.InitTalentTree();
	}

	private void OnDisable()
	{
		Main instance = Main.instance;
		instance.GarrisonDataResetFinishedAction = (Action)Delegate.Remove(instance.GarrisonDataResetFinishedAction, new Action(this.HandleGarrisonDataResetFinished));
	}

	private void HandleGarrisonDataResetFinished()
	{
		if (this.m_needsFullInit)
		{
			this.InitTalentTree();
		}
		else
		{
			TalentTreeItem[] componentsInChildren = this.m_talentTreeItemRoot.GetComponentsInChildren<TalentTreeItem>(true);
			foreach (TalentTreeItem talentTreeItem in componentsInChildren)
			{
				talentTreeItem.HandleGarrisonDataResetFinished();
				MobilePlayerCanResearchGarrisonTalent mobilePlayerCanResearchGarrisonTalent = new MobilePlayerCanResearchGarrisonTalent();
				mobilePlayerCanResearchGarrisonTalent.GarrTalentID = talentTreeItem.m_talentButtonLeft.GetTalentID();
				Login.instance.SendToMobileServer(mobilePlayerCanResearchGarrisonTalent);
				MobilePlayerCanResearchGarrisonTalent mobilePlayerCanResearchGarrisonTalent2 = new MobilePlayerCanResearchGarrisonTalent();
				mobilePlayerCanResearchGarrisonTalent2.GarrTalentID = talentTreeItem.m_talentButtonRight.GetTalentID();
				Login.instance.SendToMobileServer(mobilePlayerCanResearchGarrisonTalent2);
			}
		}
	}

	private Sprite LoadTalengBGForClass(int classID)
	{
		Sprite result = null;
		switch (classID)
		{
		case 1:
			result = Resources.Load<Sprite>("OrderAdvancement/OrderAdvancement-WarriorBG");
			break;
		case 2:
			result = Resources.Load<Sprite>("OrderAdvancement/OrderAdvancement-PaladinBG");
			break;
		case 3:
			result = Resources.Load<Sprite>("OrderAdvancement/OrderAdvancement-HunterBG");
			break;
		case 4:
			result = Resources.Load<Sprite>("OrderAdvancement/OrderAdvancement-RogueBG");
			break;
		case 5:
			result = Resources.Load<Sprite>("OrderAdvancement/OrderAdvancement-PriestBG");
			break;
		case 6:
			result = Resources.Load<Sprite>("OrderAdvancement/OrderAdvancement-DKBG");
			break;
		case 7:
			result = Resources.Load<Sprite>("OrderAdvancement/OrderAdvancement-ShamanBG");
			break;
		case 8:
			result = Resources.Load<Sprite>("OrderAdvancement/OrderAdvancement-MageBG");
			break;
		case 9:
			result = Resources.Load<Sprite>("OrderAdvancement/OrderAdvancement-WarlockBG");
			break;
		case 10:
			result = Resources.Load<Sprite>("OrderAdvancement/OrderAdvancement-MonkBG");
			break;
		case 11:
			result = Resources.Load<Sprite>("OrderAdvancement/OrderAdvancement-DruidBG");
			break;
		case 12:
			result = Resources.Load<Sprite>("OrderAdvancement/OrderAdvancement-DHBG");
			break;
		}
		return result;
	}

	private void HandleEnteredWorld()
	{
		this.m_needsFullInit = true;
	}

	private void InitTalentTree()
	{
		this.m_needsFullInit = false;
		Sprite sprite = this.LoadTalengBGForClass(GarrisonStatus.CharacterClassID());
		if (sprite != null)
		{
			this.m_classBG.sprite = sprite;
		}
		TalentTreeItem[] componentsInChildren = this.m_talentTreeItemRoot.GetComponentsInChildren<TalentTreeItem>(true);
		foreach (TalentTreeItem talentTreeItem in componentsInChildren)
		{
			Object.DestroyImmediate(talentTreeItem.gameObject);
		}
		Image[] componentsInChildren2 = this.m_romanNumeralRoot.GetComponentsInChildren<Image>(true);
		foreach (Image image in componentsInChildren2)
		{
			Object.DestroyImmediate(image.gameObject);
		}
		GarrTalentTreeRec treeRec = null;
		StaticDB.garrTalentTreeDB.EnumRecords(delegate(GarrTalentTreeRec garrTalentTreeRec)
		{
			if (garrTalentTreeRec.ClassID == GarrisonStatus.CharacterClassID())
			{
				treeRec = garrTalentTreeRec;
				return false;
			}
			return true;
		});
		if (treeRec == null)
		{
			Debug.LogError("No GarrTalentTree record found for class " + GarrisonStatus.CharacterClassID());
			return;
		}
		List<TalentTreeItem> talentTreeItems = new List<TalentTreeItem>();
		for (int k = 0; k < treeRec.MaxTiers; k++)
		{
			GameObject gameObject = Object.Instantiate<GameObject>(this.m_talentTreeItemPrefab);
			gameObject.transform.SetParent(this.m_talentTreeItemRoot.transform, false);
			TalentTreeItem component = gameObject.GetComponent<TalentTreeItem>();
			talentTreeItems.Add(component);
			if (k < this.m_romanNumeralPrefabs.Length)
			{
				GameObject gameObject2 = Object.Instantiate<GameObject>(this.m_romanNumeralPrefabs[k]);
				gameObject2.transform.SetParent(this.m_romanNumeralRoot.transform, false);
			}
		}
		StaticDB.garrTalentDB.EnumRecordsByParentID(treeRec.ID, delegate(GarrTalentRec garrTalentRec)
		{
			talentTreeItems[garrTalentRec.Tier].SetTalent(garrTalentRec);
			MobilePlayerCanResearchGarrisonTalent mobilePlayerCanResearchGarrisonTalent = new MobilePlayerCanResearchGarrisonTalent();
			mobilePlayerCanResearchGarrisonTalent.GarrTalentID = garrTalentRec.ID;
			Login.instance.SendToMobileServer(mobilePlayerCanResearchGarrisonTalent);
			return true;
		});
		foreach (TalentTreeItem talentTreeItem2 in talentTreeItems)
		{
			talentTreeItem2.UpdateVisualStates();
		}
	}

	public bool AnyTalentIsResearching()
	{
		TalentTreeItem[] componentsInChildren = this.m_talentTreeItemRoot.GetComponentsInChildren<TalentTreeItem>(true);
		foreach (TalentTreeItem talentTreeItem in componentsInChildren)
		{
			if (talentTreeItem.m_talentButtonLeft.IsResearching() || talentTreeItem.m_talentButtonRight.IsResearching())
			{
				return true;
			}
		}
		return false;
	}

	private void Update()
	{
		if (this.m_panelViewRT.sizeDelta.x != this.m_parentViewRT.rect.width)
		{
			this.m_multiPanelViewSizeDelta = this.m_panelViewRT.sizeDelta;
			this.m_multiPanelViewSizeDelta.x = this.m_parentViewRT.rect.width;
			this.m_panelViewRT.sizeDelta = this.m_multiPanelViewSizeDelta;
		}
	}

	public Image m_classBG;

	public GameObject m_talentTreeItemRoot;

	public GameObject m_talentTreeItemPrefab;

	public GameObject m_romanNumeralRoot;

	public GameObject[] m_romanNumeralPrefabs;

	public RectTransform m_parentViewRT;

	public RectTransform m_panelViewRT;

	private Vector2 m_multiPanelViewSizeDelta;

	private bool m_needsFullInit = true;
}
