using System;
using UnityEngine;
using UnityEngine.UI;
using WowStatConstants;
using WowStaticData;

namespace WoWCompanionApp
{
	public class FollowerDetailView : MonoBehaviour
	{
		private void Start()
		{
			this.m_equipmentSlotsText.font = GeneralHelpers.LoadStandardFont();
			this.m_equipmentSlotsText.text = StaticDB.GetString("EQUIPMENT", "Equipment PH");
			Text componentInChildren = this.m_activateChampionButton.GetComponentInChildren<Text>();
			if (componentInChildren != null)
			{
				componentInChildren.text = StaticDB.GetString("ACTIVATE", null);
			}
			componentInChildren = this.m_deactivateChampionButton.GetComponentInChildren<Text>();
			if (componentInChildren != null)
			{
				componentInChildren.text = StaticDB.GetString("DEACTIVATE", null);
			}
		}

		private void OnEnable()
		{
			Main instance = Main.instance;
			instance.UseEquipmentResultAction = (Action<WrapperGarrisonFollower, WrapperGarrisonFollower>)Delegate.Combine(instance.UseEquipmentResultAction, new Action<WrapperGarrisonFollower, WrapperGarrisonFollower>(this.HandleUseEquipmentResult));
			AdventureMapPanel instance2 = AdventureMapPanel.instance;
			instance2.FollowerToInspectChangedAction = (Action<int>)Delegate.Combine(instance2.FollowerToInspectChangedAction, new Action<int>(this.HandleFollowerToInspectChanged));
		}

		private void OnDisable()
		{
			Main instance = Main.instance;
			instance.UseEquipmentResultAction = (Action<WrapperGarrisonFollower, WrapperGarrisonFollower>)Delegate.Remove(instance.UseEquipmentResultAction, new Action<WrapperGarrisonFollower, WrapperGarrisonFollower>(this.HandleUseEquipmentResult));
			AdventureMapPanel instance2 = AdventureMapPanel.instance;
			instance2.FollowerToInspectChangedAction = (Action<int>)Delegate.Remove(instance2.FollowerToInspectChangedAction, new Action<int>(this.HandleFollowerToInspectChanged));
		}

		private void HandleFollowerToInspectChanged(int garrFollowerID)
		{
			this.SetFollower(garrFollowerID);
		}

		public void HandleFollowerDataChanged()
		{
			this.SetFollower(this.m_garrFollowerID);
		}

		private void HandleUseEquipmentResult(WrapperGarrisonFollower oldFollower, WrapperGarrisonFollower newFollower)
		{
			if (this.m_garrFollowerID != newFollower.GarrFollowerID)
			{
				return;
			}
			foreach (int num in newFollower.AbilityIDs)
			{
				GarrAbilityRec record = StaticDB.garrAbilityDB.GetRecord(num);
				if ((record.Flags & 1u) != 0u)
				{
					bool flag = true;
					foreach (int num2 in oldFollower.AbilityIDs)
					{
						if (num2 == num)
						{
							flag = false;
							break;
						}
					}
					if (flag)
					{
						AbilityDisplay[] componentsInChildren = this.m_equipmentSlotsRootObject.GetComponentsInChildren<AbilityDisplay>(true);
						foreach (AbilityDisplay abilityDisplay in componentsInChildren)
						{
							bool flag2 = true;
							foreach (int num3 in newFollower.AbilityIDs)
							{
								if (abilityDisplay.GetAbilityID() == num3)
								{
									flag2 = false;
									break;
								}
							}
							if (flag2)
							{
								Debug.Log(string.Concat(new object[]
								{
									"New ability is ",
									num,
									" replacing ability ID ",
									abilityDisplay.GetAbilityID()
								}));
								abilityDisplay.SetAbility(num, true, true, this);
								Main.instance.m_UISound.Play_UpgradeEquipment();
								UiAnimMgr.instance.PlayAnim("FlameGlowPulse", abilityDisplay.transform, Vector3.zero, 1f, 0f);
							}
						}
					}
				}
			}
		}

		public int GetCurrentFollower()
		{
			return this.m_garrFollowerID;
		}

		private void InitEquipmentSlots(WrapperGarrisonFollower follower)
		{
			AbilityDisplay[] componentsInChildren = this.m_equipmentSlotsRootObject.GetComponentsInChildren<AbilityDisplay>(true);
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				Object.Destroy(componentsInChildren[i].gameObject);
			}
			bool active = false;
			for (int j = 0; j < follower.AbilityIDs.Count; j++)
			{
				GarrAbilityRec record = StaticDB.garrAbilityDB.GetRecord(follower.AbilityIDs[j]);
				if ((record.Flags & 1u) != 0u)
				{
					active = true;
					GameObject gameObject = Object.Instantiate<GameObject>(this.m_equipmentSlotPrefab);
					gameObject.transform.SetParent(this.m_equipmentSlotsRootObject.transform, false);
					AbilityDisplay component = gameObject.GetComponent<AbilityDisplay>();
					component.SetAbility(follower.AbilityIDs[j], true, true, this);
				}
			}
			this.m_equipmentSlotsText.gameObject.SetActive(active);
		}

		public void SetFollower(int followerID)
		{
			this.m_garrFollowerID = followerID;
			if (followerID == 0)
			{
				RectTransform[] componentsInChildren = this.traitsAndAbilitiesRootObject.GetComponentsInChildren<RectTransform>(true);
				for (int i = 0; i < componentsInChildren.Length; i++)
				{
					if (componentsInChildren[i] != null && componentsInChildren[i] != this.traitsAndAbilitiesRootObject.transform)
					{
						Object.Destroy(componentsInChildren[i].gameObject);
					}
				}
				AbilityDisplay[] componentsInChildren2 = this.m_equipmentSlotsRootObject.GetComponentsInChildren<AbilityDisplay>(true);
				for (int j = 0; j < componentsInChildren2.Length; j++)
				{
					Object.Destroy(componentsInChildren2[j].gameObject);
				}
			}
			if (!PersistentFollowerData.followerDictionary.ContainsKey(followerID))
			{
				return;
			}
			GarrFollowerRec record = StaticDB.garrFollowerDB.GetRecord(followerID);
			if (record == null)
			{
				return;
			}
			CreatureRec record2 = StaticDB.creatureDB.GetRecord((GarrisonStatus.Faction() != PVP_FACTION.HORDE) ? record.AllianceCreatureID : record.HordeCreatureID);
			if (record2 == null)
			{
				return;
			}
			WrapperGarrisonFollower follower = PersistentFollowerData.followerDictionary[followerID];
			string text = "Assets/BundleAssets/PortraitIcons/cid_" + record2.ID.ToString("D8") + ".png";
			Sprite sprite = AssetBundleManager.PortraitIcons.LoadAsset<Sprite>(text);
			if (sprite != null)
			{
				this.followerSnapshot.sprite = sprite;
			}
			RectTransform[] componentsInChildren3 = this.traitsAndAbilitiesRootObject.GetComponentsInChildren<RectTransform>(true);
			for (int k = 0; k < componentsInChildren3.Length; k++)
			{
				if (componentsInChildren3[k] != null && componentsInChildren3[k] != this.traitsAndAbilitiesRootObject.transform)
				{
					Object.Destroy(componentsInChildren3[k].gameObject);
				}
			}
			bool flag = false;
			for (int l = 0; l < follower.AbilityIDs.Count; l++)
			{
				GarrAbilityRec record3 = StaticDB.garrAbilityDB.GetRecord(follower.AbilityIDs[l]);
				if ((record3.Flags & 512u) != 0u)
				{
					if (!flag)
					{
						GameObject gameObject = Object.Instantiate<GameObject>(this.specializationHeaderPrefab);
						gameObject.transform.SetParent(this.traitsAndAbilitiesRootObject.transform, false);
						flag = true;
						Text component = gameObject.GetComponent<Text>();
						if (component != null)
						{
							component.text = StaticDB.GetString("SPECIALIZATION", null);
						}
					}
					GameObject gameObject2 = Object.Instantiate<GameObject>(this.abilityDisplayPrefab);
					gameObject2.transform.SetParent(this.traitsAndAbilitiesRootObject.transform, false);
					AbilityDisplay component2 = gameObject2.GetComponent<AbilityDisplay>();
					component2.SetAbility(record3.ID, false, false, null);
				}
			}
			bool flag2 = false;
			for (int m = 0; m < follower.AbilityIDs.Count; m++)
			{
				GarrAbilityRec record4 = StaticDB.garrAbilityDB.GetRecord(follower.AbilityIDs[m]);
				if ((record4.Flags & 1u) == 0u)
				{
					if ((record4.Flags & 512u) == 0u)
					{
						if (!flag2)
						{
							GameObject gameObject3 = Object.Instantiate<GameObject>(this.abilitiesHeaderPrefab);
							gameObject3.transform.SetParent(this.traitsAndAbilitiesRootObject.transform, false);
							flag2 = true;
							Text component3 = gameObject3.GetComponent<Text>();
							if (component3 != null)
							{
								component3.text = StaticDB.GetString("ABILITIES", null);
							}
						}
						GameObject gameObject4 = Object.Instantiate<GameObject>(this.abilityDisplayPrefab);
						gameObject4.transform.SetParent(this.traitsAndAbilitiesRootObject.transform, false);
						AbilityDisplay component4 = gameObject4.GetComponent<AbilityDisplay>();
						component4.SetAbility(follower.AbilityIDs[m], false, false, null);
					}
				}
			}
			if (follower.ZoneSupportSpellID > 0)
			{
				GameObject gameObject5 = Object.Instantiate<GameObject>(this.zoneSupportAbilityHeaderPrefab);
				gameObject5.transform.SetParent(this.traitsAndAbilitiesRootObject.transform, false);
				GameObject gameObject6 = Object.Instantiate<GameObject>(this.m_spellDisplayPrefab);
				gameObject6.transform.SetParent(this.traitsAndAbilitiesRootObject.transform, false);
				SpellDisplay component5 = gameObject6.GetComponent<SpellDisplay>();
				component5.SetSpell(follower.ZoneSupportSpellID);
				Text componentInChildren = gameObject5.GetComponentInChildren<Text>();
				if (componentInChildren != null)
				{
					componentInChildren.text = StaticDB.GetString("COMBAT_ALLY", null);
				}
			}
			bool flag3 = (follower.Flags & 8) != 0;
			if (flag3)
			{
				GarrStringRec record5 = StaticDB.garrStringDB.GetRecord((GarrisonStatus.Faction() != PVP_FACTION.ALLIANCE) ? record.HordeFlavorGarrStringID : record.AllianceFlavorGarrStringID);
				if (record5 != null)
				{
					Text text2 = Object.Instantiate<Text>(this.m_troopDescriptionPrefab);
					text2.transform.SetParent(this.traitsAndAbilitiesRootObject.transform, false);
					text2.text = record5.Text;
					text2.font = FontLoader.LoadStandardFont();
				}
			}
			this.InitEquipmentSlots(follower);
			this.UpdateChampionButtons(follower);
		}

		public void ShowChampionActivationConfirmationDialog()
		{
			Singleton<DialogFactory>.Instance.CreateChampionActivationConfirmationDialog(this);
		}

		public void ShowChampionDeactivationConfirmationDialog()
		{
			Singleton<DialogFactory>.Instance.CreateChampionDeactivationConfirmationDialog(this);
		}

		public void ActivateFollower()
		{
			Main.instance.m_UISound.Play_ActivateChampion();
			Debug.Log("Attempting to Activate follower " + this.m_garrFollowerID);
			LegionCompanionWrapper.ChangeFollowerActive(this.m_garrFollowerID, false);
		}

		public void DeactivateFollower()
		{
			Main.instance.m_UISound.Play_DeactivateChampion();
			LegionCompanionWrapper.ChangeFollowerActive(this.m_garrFollowerID, true);
		}

		private void UpdateChampionButtons(WrapperGarrisonFollower follower)
		{
			if (this.m_activateChampionButton == null || this.m_deactivateChampionButton == null)
			{
				return;
			}
			bool flag = (follower.Flags & 8) != 0;
			if (flag)
			{
				this.m_activateChampionButton.SetActive(false);
				this.m_deactivateChampionButton.SetActive(false);
			}
			else
			{
				bool flag2 = (follower.Flags & 4) != 0;
				bool flag3 = GarrisonStatus.GetRemainingFollowerActivations() > 0;
				this.m_activateChampionButton.SetActive(flag2 && flag3);
				int numActiveChampions = GeneralHelpers.GetNumActiveChampions();
				int maxActiveFollowers = GarrisonStatus.GetMaxActiveFollowers();
				bool flag4 = follower.CurrentMissionID != 0;
				this.m_deactivateChampionButton.SetActive(!flag2 && !flag4 && numActiveChampions > maxActiveFollowers);
			}
		}

		[Header("Spec and Abilities")]
		public GameObject traitsAndAbilitiesRootObject;

		public GameObject specializationHeaderPrefab;

		public GameObject abilitiesHeaderPrefab;

		public GameObject zoneSupportAbilityHeaderPrefab;

		public GameObject abilityDisplayPrefab;

		public GameObject m_spellDisplayPrefab;

		public GameObject m_equipmentSlotPrefab;

		[Header("Follower Snapshot")]
		public Image followerSnapshot;

		[Header("Equipment Slots")]
		public GameObject m_equipmentSlotsRootObject;

		[Header("Misc")]
		public FollowerListView m_followerListView;

		public FollowerInventoryListView m_followerInventoryListView;

		[Header("More Text")]
		public Text m_equipmentSlotsText;

		[Header("Champion Only")]
		public GameObject m_activateChampionButton;

		public GameObject m_deactivateChampionButton;

		[Header("Troops Only")]
		public Text m_troopDescriptionPrefab;

		private int m_garrFollowerID;
	}
}
