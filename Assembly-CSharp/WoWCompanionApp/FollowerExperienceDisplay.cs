using System;
using UnityEngine;
using UnityEngine.UI;
using WowStatConstants;
using WowStaticData;

namespace WoWCompanionApp
{
	public class FollowerExperienceDisplay : MonoBehaviour
	{
		private void Start()
		{
			this.m_followerNameText.font = GeneralHelpers.LoadStandardFont();
			this.m_iLevelText.font = GeneralHelpers.LoadStandardFont();
			this.m_classText.font = GeneralHelpers.LoadStandardFont();
			this.m_xpAmountText.font = GeneralHelpers.LoadStandardFont();
			this.m_toNextLevelOrUpgradeText.font = GeneralHelpers.LoadStandardFont();
		}

		private void OnEnable()
		{
			FancyNumberDisplay fancyNumberDisplay = this.m_fancyNumberDisplay;
			fancyNumberDisplay.TimerUpdateAction = (Action<int>)Delegate.Combine(fancyNumberDisplay.TimerUpdateAction, new Action<int>(this.SetFillValue));
		}

		private void OnDisable()
		{
			FancyNumberDisplay fancyNumberDisplay = this.m_fancyNumberDisplay;
			fancyNumberDisplay.TimerUpdateAction = (Action<int>)Delegate.Remove(fancyNumberDisplay.TimerUpdateAction, new Action<int>(this.SetFillValue));
		}

		private void SetFollowerAppearance(WrapperGarrisonFollower follower, bool nextCapIsForQuality, bool isMaxLevelAndMaxQuality, bool isTroop, float initialEntranceDelay)
		{
			GarrFollowerRec record = StaticDB.garrFollowerDB.GetRecord(follower.GarrFollowerID);
			this.m_troopHeartContainerEmpty.SetActive(isTroop);
			this.m_troopHeartContainerFull.SetActive(isTroop);
			this.m_expiredPortraitX.SetActive(false);
			if (isTroop)
			{
				this.m_levelBorder.gameObject.SetActive(false);
				this.m_progressBarObj.SetActive(false);
				this.m_portraitBG.gameObject.SetActive(false);
				this.m_troopHeartContainerEmpty.SetActive(true);
				this.m_troopHeartContainerFull.SetActive(true);
				Transform[] componentsInChildren = this.m_troopHeartContainerEmpty.GetComponentsInChildren<Transform>(true);
				foreach (Transform transform in componentsInChildren)
				{
					if (transform != this.m_troopHeartContainerEmpty.transform)
					{
						Object.Destroy(transform.gameObject);
					}
				}
				Transform[] componentsInChildren2 = this.m_troopHeartContainerFull.GetComponentsInChildren<Transform>(true);
				foreach (Transform transform2 in componentsInChildren2)
				{
					if (transform2 != this.m_troopHeartContainerFull.transform)
					{
						Object.Destroy(transform2.gameObject);
					}
				}
				float num = 0.15f;
				WrapperGarrisonFollower wrapperGarrisonFollower = PersistentFollowerData.preMissionFollowerDictionary[follower.GarrFollowerID];
				for (int k = 0; k < wrapperGarrisonFollower.Durability; k++)
				{
					GameObject gameObject = Object.Instantiate<GameObject>(this.m_troopHeartPrefab);
					gameObject.transform.SetParent(this.m_troopHeartContainerFull.transform, false);
					if (k >= follower.Durability)
					{
						float num2 = initialEntranceDelay + (float)(wrapperGarrisonFollower.Durability - (k - follower.Durability)) * num;
						float num3 = 2f;
						iTween.ValueTo(gameObject, iTween.Hash(new object[]
						{
							"name",
							"fade",
							"from",
							0f,
							"to",
							1f,
							"time",
							num3,
							"easetype",
							iTween.EaseType.easeOutCubic,
							"delay",
							num2,
							"onupdatetarget",
							gameObject,
							"onupdate",
							"SetHeartEffectProgress",
							"oncomplete",
							"FinishHeartEffect"
						}));
					}
				}
				for (int l = 0; l < record.Vitality; l++)
				{
					GameObject gameObject2 = Object.Instantiate<GameObject>(this.m_troopEmptyHeartPrefab);
					gameObject2.transform.SetParent(this.m_troopHeartContainerEmpty.transform, false);
				}
				if (follower.Durability <= 0)
				{
					DelayedUIAnim delayedUIAnim = base.gameObject.AddComponent<DelayedUIAnim>();
					float num4 = initialEntranceDelay + (float)(wrapperGarrisonFollower.Durability - follower.Durability) * num + 1f;
					delayedUIAnim.Init(num4, "RedFailX", "SFX/UI_Mission_Fail_Red_X", this.m_followerPortrait.transform, 1.5f, 0.3f);
					DelayedObjectEnable delayedObjectEnable = base.gameObject.AddComponent<DelayedObjectEnable>();
					delayedObjectEnable.Init(num4 + 0.25f, this.m_expiredPortraitX);
				}
			}
			int iconFileDataID = (GarrisonStatus.Faction() != PVP_FACTION.HORDE) ? record.AllianceIconFileDataID : record.HordeIconFileDataID;
			Sprite sprite = GeneralHelpers.LoadIconAsset(AssetBundleType.PortraitIcons, iconFileDataID);
			if (sprite != null)
			{
				this.m_followerPortrait.sprite = sprite;
			}
			if (isTroop)
			{
				this.m_qualityBorder_TitleQuality.gameObject.SetActive(false);
				this.m_levelBorder_TitleQuality.gameObject.SetActive(false);
				this.m_qualityBorder.gameObject.SetActive(false);
				this.m_levelBorder.gameObject.SetActive(false);
				this.m_followerNameText.color = Color.white;
				this.m_iLevelText.gameObject.SetActive(false);
			}
			else
			{
				if (follower.Quality == 6)
				{
					this.m_qualityBorder_TitleQuality.gameObject.SetActive(true);
					this.m_levelBorder_TitleQuality.gameObject.SetActive(true);
					this.m_qualityBorder.gameObject.SetActive(false);
					this.m_levelBorder.gameObject.SetActive(false);
				}
				else
				{
					this.m_qualityBorder_TitleQuality.gameObject.SetActive(false);
					this.m_levelBorder_TitleQuality.gameObject.SetActive(false);
					this.m_qualityBorder.gameObject.SetActive(true);
					this.m_levelBorder.gameObject.SetActive(true);
				}
				Color qualityColor = GeneralHelpers.GetQualityColor(follower.Quality);
				this.m_qualityBorder.color = qualityColor;
				this.m_levelBorder.color = qualityColor;
				this.m_followerNameText.color = qualityColor;
			}
			CreatureRec record2 = StaticDB.creatureDB.GetRecord((GarrisonStatus.Faction() != PVP_FACTION.HORDE) ? record.AllianceCreatureID : record.HordeCreatureID);
			if (follower.Quality == 6 && record.TitleName != null && record.TitleName.Length > 0)
			{
				this.m_followerNameText.text = record.TitleName;
			}
			else if (record != null)
			{
				this.m_followerNameText.text = record2.Name;
			}
			if (follower.FollowerLevel < 110)
			{
				this.m_iLevelText.text = GeneralHelpers.TextOrderString(StaticDB.GetString("LEVEL", null), follower.FollowerLevel.ToString());
			}
			else
			{
				this.m_iLevelText.text = StaticDB.GetString("ILVL", null) + " " + ((follower.ItemLevelArmor + follower.ItemLevelWeapon) / 2).ToString();
			}
			GarrClassSpecRec record3 = StaticDB.garrClassSpecDB.GetRecord((int)((GarrisonStatus.Faction() != PVP_FACTION.HORDE) ? record.AllianceGarrClassSpecID : record.HordeGarrClassSpecID));
			this.m_classText.text = record3.ClassSpec;
			Sprite atlasSprite = TextureAtlas.instance.GetAtlasSprite((int)record3.UiTextureAtlasMemberID);
			if (atlasSprite != null)
			{
				this.m_classIcon.sprite = atlasSprite;
			}
			if (!isTroop)
			{
				if (isMaxLevelAndMaxQuality)
				{
					this.m_progressBarObj.SetActive(false);
					this.m_toNextLevelOrUpgradeText.text = string.Empty;
				}
				else if (nextCapIsForQuality)
				{
					this.m_progressBarObj.SetActive(true);
					this.m_toNextLevelOrUpgradeText.text = StaticDB.GetString("TO_NEXT_UPGRADE", string.Empty);
				}
				else
				{
					this.m_progressBarObj.SetActive(true);
					this.m_toNextLevelOrUpgradeText.text = StaticDB.GetString("TO_NEXT_LEVEL", string.Empty);
				}
			}
		}

		public void SetFollower(WrapperGarrisonFollower oldFollower, WrapperGarrisonFollower newFollower, float initialEffectDelay)
		{
			this.m_followerID = oldFollower.GarrFollowerID;
			bool flag = (oldFollower.Flags & 8) != 0;
			if (flag)
			{
				this.SetFollowerAppearance(newFollower, false, false, true, initialEffectDelay);
				return;
			}
			this.m_showedLevelUpEffect = false;
			bool isMaxLevelAndMaxQuality = false;
			bool nextCapIsForQuality = false;
			GeneralHelpers.GetXpCapInfo(oldFollower.FollowerLevel, oldFollower.Quality, out this.m_currentCap, out nextCapIsForQuality, out isMaxLevelAndMaxQuality);
			this.SetFollowerAppearance(oldFollower, nextCapIsForQuality, isMaxLevelAndMaxQuality, false, initialEffectDelay);
			GeneralHelpers.GetXpCapInfo(newFollower.FollowerLevel, newFollower.Quality, out this.m_newCap, out this.m_newCapIsQuality, out this.m_newFollowerIsMaxLevelAndMaxQuality);
			this.m_fancyNumberDisplay.SetNumberLabel(StaticDB.GetString("XP2", null));
			this.m_fancyNumberDisplay.SetValue((int)(this.m_currentCap - (uint)oldFollower.Xp), true, 0f);
			if (oldFollower.FollowerLevel != newFollower.FollowerLevel || oldFollower.Quality != newFollower.Quality)
			{
				this.m_fancyNumberDisplay.SetValue(0, initialEffectDelay);
			}
			else
			{
				this.m_fancyNumberDisplay.SetValue((int)(this.m_currentCap - (uint)newFollower.Xp), initialEffectDelay);
			}
		}

		private void SetFillValue(int newXPRemainingUntilNextLevel)
		{
			if (newXPRemainingUntilNextLevel == 0 && this.m_currentCap != this.m_newCap)
			{
				if (!this.m_showedLevelUpEffect)
				{
					Main.instance.m_UISound.Play_ChampionLevelUp();
					UiAnimMgr.instance.PlayAnim("FlameGlowPulse", this.m_followerPortrait.transform, Vector3.zero, 2f, 0f);
					UiAnimMgr.instance.PlayAnim("MinimapPulseAnim", this.m_followerPortrait.transform, Vector3.zero, 2f, 0f);
					this.m_showedLevelUpEffect = true;
				}
				WrapperGarrisonFollower follower = PersistentFollowerData.followerDictionary[this.m_followerID];
				this.SetFollowerAppearance(follower, this.m_newCapIsQuality, this.m_newFollowerIsMaxLevelAndMaxQuality, false, 0f);
				this.m_currentCap = this.m_newCap;
				this.m_fancyNumberDisplay.SetValue((int)this.m_newCap, true, 0f);
				this.m_fancyNumberDisplay.SetValue((int)(this.m_newCap - (uint)follower.Xp), 0f);
			}
			this.m_xpAmountText.text = string.Concat(new object[]
			{
				string.Empty,
				(long)((ulong)this.m_currentCap - (ulong)((long)newXPRemainingUntilNextLevel)),
				"\\",
				this.m_currentCap
			});
			float fillAmount = Mathf.Clamp01((float)((ulong)this.m_currentCap - (ulong)((long)newXPRemainingUntilNextLevel)) / this.m_currentCap);
			this.m_progressBarFillImage.fillAmount = fillAmount;
		}

		public int GetFollowerID()
		{
			return this.m_followerID;
		}

		[Header("Portrait")]
		public Image m_portraitBG;

		public Image m_followerPortrait;

		public Image m_qualityBorder;

		public Image m_qualityBorder_TitleQuality;

		public Image m_levelBorder;

		public Image m_levelBorder_TitleQuality;

		public Text m_followerNameText;

		public Text m_iLevelText;

		public Image m_classIcon;

		public Text m_classText;

		[Header("Troop Specific")]
		public GameObject m_troopHeartContainerEmpty;

		public GameObject m_troopHeartContainerFull;

		public GameObject m_troopHeartPrefab;

		public GameObject m_troopEmptyHeartPrefab;

		public GameObject m_expiredPortraitX;

		[Header("XP Bar")]
		public GameObject m_progressBarObj;

		public FancyNumberDisplay m_fancyNumberDisplay;

		public Image m_progressBarFillImage;

		public Text m_xpAmountText;

		public Text m_toNextLevelOrUpgradeText;

		private int m_followerID;

		private bool m_showedLevelUpEffect;

		private uint m_currentCap;

		private bool m_newCapIsQuality;

		private uint m_newCap;

		private bool m_newFollowerIsMaxLevelAndMaxQuality;
	}
}
