using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using WowStatConstants;
using WowStaticData;

namespace WoWCompanionApp
{
	public class MissionMechanic : MonoBehaviour
	{
		public void SetCountered(bool isCountered, bool counteringFollowerIsBusy = false, bool playCounteredEffect = true)
		{
			if (isCountered && this.m_isCountered)
			{
				return;
			}
			if (counteringFollowerIsBusy)
			{
				if (!this.m_counteredIcon.gameObject.activeSelf)
				{
					this.m_canCounterButBusyIcon.gameObject.SetActive(true);
				}
				return;
			}
			this.m_isCountered = isCountered;
			this.m_counteredIcon.gameObject.SetActive(isCountered);
			if (this.m_isCountered && this.m_canCounterButBusyIcon != null)
			{
				this.m_canCounterButBusyIcon.gameObject.SetActive(false);
			}
			this.m_missionMechanicIcon.material.SetFloat("_GrayscaleAmount", (!this.m_isCountered) ? 1f : 0f);
			if (this.m_isCountered && playCounteredEffect)
			{
				UiAnimMgr.instance.PlayAnim("GreenCheck", base.transform, Vector3.zero, 1.8f, 0f);
				Main.instance.m_UISound.Play_GreenCheck();
			}
		}

		public bool IsCountered()
		{
			return this.m_isCountered;
		}

		public void SetMechanicTypeWithMechanicID(int missionMechanicID, bool hideBorder = false)
		{
			GarrMechanicRec record = StaticDB.garrMechanicDB.GetRecord(missionMechanicID);
			if (record == null)
			{
				Debug.LogWarning("Invalid MissionMechanicID " + missionMechanicID);
				return;
			}
			this.SetMechanicType((int)record.GarrMechanicTypeID, record.GarrAbilityID, hideBorder);
		}

		public static List<int> GetUsefulBuffAbilitiesForFollower(int garrFollowerID)
		{
			List<int> list = new List<int>();
			using (List<int>.Enumerator enumerator = PersistentFollowerData.followerDictionary[garrFollowerID].AbilityIDs.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					int abilityID = enumerator.Current;
					GarrAbilityRec record = StaticDB.garrAbilityDB.GetRecord(abilityID);
					if (record != null)
					{
						if ((record.Flags & 1024u) != 0u)
						{
							list.AddRange(from rec in StaticDB.garrAbilityEffectDB.GetRecordsByParentID(abilityID)
							where rec.AbilityAction != 0u
							select abilityID);
						}
					}
				}
			}
			return list;
		}

		public static int GetAbilityToCounterMechanicType(int garrMechanicTypeID)
		{
			Func<GarrFollowerRec, bool> matchesExpansionLevel = (GarrFollowerRec rec) => rec.GarrFollowerTypeID == (uint)GarrisonStatus.GarrisonFollowerType;
			Func<GarrFollowerRec, bool> matchesMyClass = (GarrFollowerRec rec) => rec.ChrClassID == GarrisonStatus.CharacterClassID() || rec.ChrClassID == 0;
			HashSet<int> garrFollowerXAbilities = new HashSet<int>(StaticDB.garrFollowerDB.GetRecordsWhere((GarrFollowerRec rec) => matchesExpansionLevel(rec) && matchesMyClass(rec)).SelectMany((GarrFollowerRec garrFollowerRec) => from followerXAbilityRec in StaticDB.garrFollowerXAbilityDB.GetRecordsByParentID(garrFollowerRec.ID)
			select followerXAbilityRec.GarrAbilityID));
			GarrAbilityEffectRec recordFirstOrDefault = StaticDB.garrAbilityEffectDB.GetRecordFirstOrDefault((GarrAbilityEffectRec rec) => garrFollowerXAbilities.Contains((int)rec.GarrAbilityID) && (ulong)rec.GarrMechanicTypeID == (ulong)((long)garrMechanicTypeID));
			return (int)((recordFirstOrDefault == null) ? 0u : recordFirstOrDefault.GarrAbilityID);
		}

		public int AbilityID()
		{
			return this.m_garrAbilityID;
		}

		public void SetMechanicType(int missionMechanicTypeID, int mechanicAbilityID, bool hideBorder = false)
		{
			this.m_garrAbilityID = mechanicAbilityID;
			GarrMechanicTypeRec record = StaticDB.garrMechanicTypeDB.GetRecord(missionMechanicTypeID);
			if (record != null)
			{
				this.m_missionMechanicIcon.gameObject.SetActive(true);
				this.m_missionMechanicTypeID = record.ID;
				this.m_counterWithThisAbilityID = MissionMechanic.GetAbilityToCounterMechanicType(missionMechanicTypeID);
				if (this.m_counterWithThisAbilityID != 0)
				{
					GarrAbilityRec record2 = StaticDB.garrAbilityDB.GetRecord(this.m_counterWithThisAbilityID);
					if (record2 != null)
					{
						Sprite sprite = GeneralHelpers.LoadIconAsset(AssetBundleType.Icons, record2.IconFileDataID);
						if (sprite != null)
						{
							this.m_missionMechanicIcon.sprite = sprite;
							if (this.m_grayscaleShader != null)
							{
								Material material = new Material(this.m_grayscaleShader);
								this.m_missionMechanicIcon.material = material;
							}
						}
					}
				}
			}
			else
			{
				this.m_missionMechanicIcon.gameObject.SetActive(false);
			}
			this.SetCountered(false, false, true);
			this.m_missionMechanicIconBorder.gameObject.SetActive(!hideBorder);
		}

		public void ShowTooltip()
		{
			Main.instance.allPopups.ShowAbilityInfoPopup(this.m_counterWithThisAbilityID);
		}

		public int m_missionMechanicTypeID;

		public Image m_missionMechanicIcon;

		public Image m_missionMechanicIconBorder;

		public Image m_counteredIcon;

		public Image m_canCounterButBusyIcon;

		public Shader m_grayscaleShader;

		private bool m_isCountered;

		private int m_garrAbilityID;

		private int m_counterWithThisAbilityID;
	}
}
