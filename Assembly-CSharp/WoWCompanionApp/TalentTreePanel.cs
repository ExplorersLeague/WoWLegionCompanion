using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using WowStaticData;

namespace WoWCompanionApp
{
	public class TalentTreePanel : MonoBehaviour
	{
		private void Start()
		{
		}

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
					LegionCompanionWrapper.RequestCanResearchGarrisonTalent(talentTreeItem.m_talentButtonLeft.GetTalentID());
					LegionCompanionWrapper.RequestCanResearchGarrisonTalent(talentTreeItem.m_talentButtonRight.GetTalentID());
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

		public void SetNeedsFullInit()
		{
			this.m_needsFullInit = true;
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
				Object.Destroy(talentTreeItem.gameObject);
			}
			Image[] componentsInChildren2 = this.m_romanNumeralRoot.GetComponentsInChildren<Image>(true);
			foreach (Image image in componentsInChildren2)
			{
				Object.Destroy(image.gameObject);
			}
			this.m_talentTreeItems.Clear();
			GarrTalentTreeRec recordFirstOrDefault = StaticDB.garrTalentTreeDB.GetRecordFirstOrDefault((GarrTalentTreeRec garrTalentTreeRec) => garrTalentTreeRec.ClassID == GarrisonStatus.CharacterClassID());
			if (recordFirstOrDefault == null)
			{
				Debug.LogError("No GarrTalentTree record found for class " + GarrisonStatus.CharacterClassID());
				return;
			}
			for (int k = 0; k < recordFirstOrDefault.MaxTiers; k++)
			{
				GameObject gameObject = Object.Instantiate<GameObject>(this.m_talentTreeItemPrefab);
				gameObject.transform.SetParent(this.m_talentTreeItemRoot.transform, false);
				TalentTreeItem component = gameObject.GetComponent<TalentTreeItem>();
				this.m_talentTreeItems.Add(component);
				switch (k)
				{
				case 0:
					component.m_talentTier.sprite = Resources.Load<Sprite>("OrderAdvancement/Number-One");
					break;
				case 1:
					component.m_talentTier.sprite = Resources.Load<Sprite>("OrderAdvancement/Number-Two");
					break;
				case 2:
					component.m_talentTier.sprite = Resources.Load<Sprite>("OrderAdvancement/Number-Three");
					break;
				case 3:
					component.m_talentTier.sprite = Resources.Load<Sprite>("OrderAdvancement/Number-Four");
					break;
				case 4:
					component.m_talentTier.sprite = Resources.Load<Sprite>("OrderAdvancement/Number-Five");
					break;
				case 5:
					component.m_talentTier.sprite = Resources.Load<Sprite>("OrderAdvancement/Number-Six");
					break;
				case 6:
					component.m_talentTier.sprite = Resources.Load<Sprite>("OrderAdvancement/Number-Seven");
					break;
				case 7:
					component.m_talentTier.sprite = Resources.Load<Sprite>("OrderAdvancement/Number-Eight");
					break;
				}
			}
			foreach (GarrTalentRec garrTalentRec in StaticDB.garrTalentDB.GetRecordsByParentID(recordFirstOrDefault.ID))
			{
				this.m_talentTreeItems[garrTalentRec.Tier].SetTalent(garrTalentRec);
				LegionCompanionWrapper.RequestCanResearchGarrisonTalent(garrTalentRec.ID);
			}
			foreach (TalentTreeItem talentTreeItem2 in this.m_talentTreeItems)
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
		}

		public bool TalentIsReadyToPlayGreenCheckAnim()
		{
			foreach (TalentTreeItem talentTreeItem in this.m_talentTreeItems)
			{
				if (talentTreeItem.m_talentButtonLeft.IsReadyToShowGreenCheckAnim() || talentTreeItem.m_talentButtonRight.IsReadyToShowGreenCheckAnim() || talentTreeItem.m_talentButtonSolo.IsReadyToShowGreenCheckAnim())
				{
					return true;
				}
			}
			return false;
		}

		public Image m_classBG;

		public GameObject m_talentTreeItemRoot;

		public GameObject m_talentTreeItemPrefab;

		public GameObject m_romanNumeralRoot;

		private Vector2 m_multiPanelViewSizeDelta;

		private bool m_needsFullInit = true;

		private List<TalentTreeItem> m_talentTreeItems = new List<TalentTreeItem>();

		public GameObject m_resourcesDisplay;
	}
}
