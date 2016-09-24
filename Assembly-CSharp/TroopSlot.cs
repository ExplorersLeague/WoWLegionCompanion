using System;
using UnityEngine;
using UnityEngine.UI;
using WowJamMessages;
using WowJamMessages.MobilePlayerJSON;
using WowStatConstants;
using WowStaticData;

public class TroopSlot : MonoBehaviour
{
	private void Awake()
	{
		this.m_collected = false;
		this.m_collectingSpinner.SetActive(false);
	}

	private void Start()
	{
		this.m_timeRemainingText.font = GeneralHelpers.LoadStandardFont();
	}

	private void OnEnable()
	{
		if (Main.instance != null)
		{
			Main instance = Main.instance;
			instance.CompleteShipmentResultAction = (Action<SHIPMENT_RESULT, ulong>)Delegate.Combine(instance.CompleteShipmentResultAction, new Action<SHIPMENT_RESULT, ulong>(this.HandleCollectTroopResult));
		}
	}

	private void OnDisable()
	{
		if (Main.instance != null)
		{
			Main instance = Main.instance;
			instance.CompleteShipmentResultAction = (Action<SHIPMENT_RESULT, ulong>)Delegate.Remove(instance.CompleteShipmentResultAction, new Action<SHIPMENT_RESULT, ulong>(this.HandleCollectTroopResult));
		}
	}

	public void SetCharShipment(int charShipmentID, ulong shipmentDBID, int ownedGarrFollowerID, bool training, int iconFileDataID = 0)
	{
		CharShipmentRec record = StaticDB.charShipmentDB.GetRecord(charShipmentID);
		if (record == null)
		{
			Debug.LogError("Invalid Shipment ID: " + charShipmentID);
			return;
		}
		if (this.m_glowLoopHandle != null)
		{
			UiAnimation anim = this.m_glowLoopHandle.GetAnim();
			if (anim != null)
			{
				anim.Stop(0.5f);
			}
			this.m_glowLoopHandle = null;
		}
		this.m_collected = false;
		this.m_collectingSpinner.SetActive(false);
		this.m_ownedGarrFollowerID = ownedGarrFollowerID;
		this.m_training = training;
		this.m_shipmentDBID = shipmentDBID;
		if (training)
		{
			if (!PersistentShipmentData.shipmentDictionary.ContainsKey(shipmentDBID))
			{
				training = false;
				Debug.LogWarning("Shipment not found in Persistent: " + charShipmentID);
			}
			else
			{
				JamCharacterShipment jamCharacterShipment = (JamCharacterShipment)PersistentShipmentData.shipmentDictionary[shipmentDBID];
				this.m_shipmentCreationTime = jamCharacterShipment.CreationTime;
				this.m_shipmentDuration = jamCharacterShipment.ShipmentDuration;
			}
		}
		if (record.GarrFollowerID > 0u)
		{
			this.SetCharShipmentTroop(record, iconFileDataID);
		}
		else if (record.DummyItemID > 0)
		{
			this.SetCharShipmentItem(record);
		}
		if (ownedGarrFollowerID != 0)
		{
			this.m_troopBuildProgressRing.gameObject.SetActive(false);
			this.m_troopBuildEmptyRing.gameObject.SetActive(false);
			this.m_troopBuildProgressFill.gameObject.SetActive(false);
			this.m_troopOwnedCheckmark.gameObject.SetActive(true);
			this.m_troopPortraitImage.gameObject.SetActive(true);
			this.m_timeRemainingText.gameObject.SetActive(false);
			return;
		}
		if (training)
		{
			this.m_troopBuildEmptyRing.gameObject.SetActive(true);
			this.m_troopBuildProgressRing.gameObject.SetActive(true);
			this.m_troopBuildProgressRing.fillAmount = 0f;
			this.m_troopBuildProgressFill.gameObject.SetActive(true);
			this.m_troopBuildProgressFill.fillAmount = 0f;
			this.m_troopOwnedCheckmark.gameObject.SetActive(false);
			this.m_troopPortraitImage.gameObject.SetActive(true);
			this.m_timeRemainingText.gameObject.SetActive(true);
			this.m_timeRemainingText.text = string.Empty;
			if (this.m_grayscaleShader != null)
			{
				Material material = new Material(this.m_grayscaleShader);
				this.m_troopPortraitImage.material = material;
			}
		}
		else
		{
			this.m_troopBuildEmptyRing.gameObject.SetActive(false);
			this.m_troopBuildProgressRing.gameObject.SetActive(false);
			this.m_troopBuildProgressFill.gameObject.SetActive(false);
			this.m_troopOwnedCheckmark.gameObject.SetActive(false);
			this.m_troopPortraitImage.gameObject.SetActive(false);
			this.m_timeRemainingText.gameObject.SetActive(false);
		}
	}

	private void SetCharShipmentItem(CharShipmentRec charShipmentRec)
	{
		ItemRec record = StaticDB.itemDB.GetRecord(charShipmentRec.DummyItemID);
		if (record == null)
		{
			Debug.LogError("Invalid Item ID: " + charShipmentRec.DummyItemID);
			return;
		}
		Sprite sprite = GeneralHelpers.LoadIconAsset(AssetBundleType.Icons, record.IconFileDataID);
		if (sprite != null)
		{
			this.m_troopPortraitImage.sprite = sprite;
		}
	}

	public void SetCharShipmentTroop(CharShipmentRec charShipmentRec, int iconFileDataID = 0)
	{
		GarrFollowerRec record = StaticDB.garrFollowerDB.GetRecord((int)charShipmentRec.GarrFollowerID);
		if (record == null)
		{
			Debug.LogError("Invalid Follower ID: " + charShipmentRec.GarrFollowerID);
			return;
		}
		if (iconFileDataID <= 0)
		{
			iconFileDataID = ((GarrisonStatus.Faction() != PVP_FACTION.HORDE) ? record.AllianceIconFileDataID : record.HordeIconFileDataID);
		}
		Sprite sprite = GeneralHelpers.LoadIconAsset(AssetBundleType.PortraitIcons, iconFileDataID);
		if (sprite != null)
		{
			this.m_troopPortraitImage.sprite = sprite;
		}
	}

	private void Update()
	{
		if (this.m_training)
		{
			long num = GarrisonStatus.CurrentTime() - (long)this.m_shipmentCreationTime;
			float fillAmount = Mathf.Clamp((float)num / (float)this.m_shipmentDuration, 0f, 1f);
			this.m_troopBuildProgressRing.fillAmount = fillAmount;
			this.m_troopBuildProgressFill.fillAmount = fillAmount;
			long num2 = (long)this.m_shipmentDuration - num;
			if (num2 < 0L)
			{
				num2 = 0L;
			}
			if (num2 > 0L)
			{
				Duration duration = new Duration((int)num2, true);
				this.m_timeRemainingText.text = duration.DurationString;
			}
			else if (this.m_glowLoopHandle == null)
			{
				this.m_glowLoopHandle = UiAnimMgr.instance.PlayAnim("MinimapLoopPulseAnim", base.transform, Vector3.zero, 3f, 0f);
				this.m_timeRemainingText.text = StaticDB.GetString("COLLECT", null);
				Main.instance.m_UISound.Play_TroopsReadyToast();
			}
		}
	}

	public bool IsEmpty()
	{
		return !this.m_training && this.m_ownedGarrFollowerID == 0;
	}

	public bool IsTraining()
	{
		return this.m_training;
	}

	public bool IsOwned()
	{
		return this.m_ownedGarrFollowerID != 0;
	}

	public bool IsCollected()
	{
		return this.m_collected;
	}

	public int GetOwnedFollowerID()
	{
		return this.m_ownedGarrFollowerID;
	}

	public ulong GetDBID()
	{
		return this.m_shipmentDBID;
	}

	public void OnCollectTroop()
	{
		long num = GarrisonStatus.CurrentTime() - (long)this.m_shipmentCreationTime;
		long num2 = (long)this.m_shipmentDuration - num;
		if (num2 > 0L)
		{
			return;
		}
		if (this.m_shipmentDBID != 0UL && !this.m_collected)
		{
			this.m_collected = true;
			this.m_collectingSpinner.SetActive(true);
			UiAnimMgr.instance.PlayAnim("MinimapPulseAnim", base.transform, Vector3.zero, 2f, 0f);
			Main.instance.m_UISound.Play_CollectTroop();
			MobilePlayerCompleteShipment mobilePlayerCompleteShipment = new MobilePlayerCompleteShipment();
			mobilePlayerCompleteShipment.ShipmentID = this.m_shipmentDBID;
			Login.instance.SendToMobileServer(mobilePlayerCompleteShipment);
		}
	}

	private void HandleCollectTroopResult(SHIPMENT_RESULT result, ulong shipmentDBID)
	{
		if (result == SHIPMENT_RESULT.SUCCESS && shipmentDBID == this.m_shipmentDBID)
		{
			if (this.m_glowLoopHandle != null)
			{
				UiAnimation anim = this.m_glowLoopHandle.GetAnim();
				if (anim != null)
				{
					anim.Stop(0.5f);
				}
				this.m_glowLoopHandle = null;
			}
			UiAnimMgr.instance.PlayAnim("GreenCheck", this.m_greenCheckEffectRoot, Vector3.zero, 1.8f, 0f);
			Main.instance.m_UISound.Play_GreenCheck();
			this.m_training = false;
			this.m_troopBuildProgressRing.gameObject.SetActive(false);
			this.m_troopBuildProgressFill.gameObject.SetActive(false);
			this.m_troopOwnedCheckmark.gameObject.SetActive(true);
			this.m_troopPortraitImage.gameObject.SetActive(true);
			this.m_timeRemainingText.gameObject.SetActive(false);
			this.m_troopPortraitImage.material = null;
			PersistentShipmentData.shipmentDictionary.Remove(shipmentDBID);
			MobilePlayerRequestShipmentTypes obj = new MobilePlayerRequestShipmentTypes();
			Login.instance.SendToMobileServer(obj);
			MobilePlayerRequestShipments obj2 = new MobilePlayerRequestShipments();
			Login.instance.SendToMobileServer(obj2);
			MobilePlayerGarrisonDataRequest mobilePlayerGarrisonDataRequest = new MobilePlayerGarrisonDataRequest();
			mobilePlayerGarrisonDataRequest.GarrTypeID = 3;
			Login.instance.SendToMobileServer(mobilePlayerGarrisonDataRequest);
		}
	}

	public Image m_troopPortraitImage;

	public Image m_troopBuildEmptyRing;

	public Image m_troopBuildProgressRing;

	public Image m_troopBuildProgressFill;

	public Image m_troopOwnedCheckmark;

	public Text m_timeRemainingText;

	public Shader m_grayscaleShader;

	public Transform m_greenCheckEffectRoot;

	public GameObject m_collectingSpinner;

	private int m_ownedGarrFollowerID;

	private bool m_training;

	private bool m_collected;

	private int m_shipmentCreationTime;

	private int m_shipmentDuration;

	private ulong m_shipmentDBID;

	private UiAnimMgr.UiAnimHandle m_glowLoopHandle;
}
