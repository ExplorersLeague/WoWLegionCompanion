using System;
using UnityEngine;
using UnityEngine.UI;
using WowStatConstants;
using WowStaticData;

public class MissionEncounter : MonoBehaviour
{
	public int GetEncounterID()
	{
		return this.m_garrEncounterID;
	}

	public void SetEncounter(int garrEncounterID, int garrMechanicID)
	{
		this.m_garrEncounterID = garrEncounterID;
		GarrEncounterRec record = StaticDB.garrEncounterDB.GetRecord(garrEncounterID);
		if (record == null)
		{
			this.nameText.text = string.Empty + garrEncounterID;
			return;
		}
		this.nameText.text = record.Name;
		Sprite sprite = GeneralHelpers.LoadIconAsset(AssetBundleType.PortraitIcons, record.PortraitFileDataID);
		if (sprite != null)
		{
			this.portraitImage.sprite = sprite;
		}
		else
		{
			this.missingIconText.gameObject.SetActive(true);
			this.missingIconText.text = string.Empty + record.PortraitFileDataID;
		}
		GameObject gameObject = Object.Instantiate<GameObject>(this.m_missionMechanicPrefab);
		gameObject.transform.SetParent(this.m_mechanicRoot.transform, false);
		MissionMechanic component = gameObject.GetComponent<MissionMechanic>();
		component.SetMechanicTypeWithMechanicID(garrMechanicID, false);
		GarrMechanicRec record2 = StaticDB.garrMechanicDB.GetRecord(garrMechanicID);
		if (record2 == null)
		{
			this.m_mechanicRoot.SetActive(false);
		}
		if (record2 != null && record2.GarrAbilityID != 0)
		{
			GameObject gameObject2 = Object.Instantiate<GameObject>(this.m_mechanicEffectDisplayPrefab);
			gameObject2.transform.SetParent(this.m_mechanicEffectRoot.transform, false);
			AbilityDisplay component2 = gameObject2.GetComponent<AbilityDisplay>();
			component2.SetAbility(record2.GarrAbilityID, false, false, null);
		}
	}

	public Text nameText;

	public Text missingIconText;

	public Image portraitImage;

	public GameObject m_mechanicRoot;

	public GameObject m_mechanicEffectRoot;

	public GameObject m_missionMechanicPrefab;

	public GameObject m_mechanicEffectDisplayPrefab;

	private int m_garrEncounterID;
}
