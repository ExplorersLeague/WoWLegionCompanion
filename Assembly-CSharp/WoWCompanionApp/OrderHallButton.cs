using System;
using UnityEngine;
using UnityEngine.UI;

namespace WoWCompanionApp
{
	public class OrderHallButton : MonoBehaviour
	{
		private void Start()
		{
			this.m_numReadyTroops = 0;
			this.m_numReadyTroopsText.text = string.Empty;
			this.m_numReadyTroopsTextBG.SetActive(false);
			int num = 0;
			switch (GarrisonStatus.CharacterClassID())
			{
			case 1:
				num = 6001;
				break;
			case 2:
				num = 6002;
				break;
			case 3:
				num = 5998;
				break;
			case 4:
				num = 6007;
				break;
			case 5:
				num = 5999;
				break;
			case 6:
				num = 6000;
				break;
			case 7:
				num = 0;
				break;
			case 8:
				num = 6006;
				break;
			case 9:
				num = 6003;
				break;
			case 10:
				num = 6005;
				break;
			case 11:
				num = 6004;
				break;
			case 12:
				num = 0;
				break;
			}
			if (num > 0)
			{
				Sprite atlasSprite = TextureAtlas.instance.GetAtlasSprite(num);
				if (atlasSprite != null)
				{
					Image component = this.m_theActualButton.GetComponent<Image>();
					component.sprite = atlasSprite;
				}
			}
		}

		private void ClearEffects()
		{
			iTween.StopByName(this.m_theActualButton, "RecruitWobble");
			iTween.StopByName(this.m_theActualButton, "RecruitWobbleL");
			iTween.StopByName(this.m_theActualButton, "RecruitButtonSwing");
			this.m_theActualButton.transform.localScale = Vector3.one;
			iTween.StopByName(this.m_numReadyTroopsTextBG, "RecruitNumSwing");
			this.m_numReadyTroopsTextBG.transform.localRotation = Quaternion.identity;
			if (this.m_glowHandle != null)
			{
				UiAnimation anim = this.m_glowHandle.GetAnim();
				if (anim != null)
				{
					anim.Stop(0.5f);
				}
			}
			if (this.m_glowLoopHandle != null)
			{
				UiAnimation anim2 = this.m_glowLoopHandle.GetAnim();
				if (anim2 != null)
				{
					anim2.Stop(0.5f);
				}
			}
		}

		private void Update()
		{
			int num = 0;
			foreach (WrapperCharacterShipment wrapperCharacterShipment in PersistentShipmentData.shipmentDictionary.Values)
			{
				TimeSpan t = GarrisonStatus.CurrentTime() - wrapperCharacterShipment.CreationTime;
				if ((wrapperCharacterShipment.ShipmentDuration - t).TotalSeconds <= 0.0)
				{
					num++;
				}
			}
			if (num != this.m_numReadyTroops)
			{
				if (num == 0 || num < this.m_numReadyTroops)
				{
					this.ClearEffects();
				}
				if (num > this.m_numReadyTroops)
				{
					this.ClearEffects();
					this.m_glowHandle = UiAnimMgr.instance.PlayAnim("MinimapPulseAnim", base.transform, Vector3.zero, 3f, 0f);
					this.m_glowLoopHandle = UiAnimMgr.instance.PlayAnim("MinimapLoopPulseAnim", base.transform, Vector3.zero, 3f, 0f);
					iTween.PunchScale(this.m_theActualButton, iTween.Hash(new object[]
					{
						"name",
						"RecruitWobble",
						"x",
						this.amount,
						"y",
						this.amount,
						"time",
						this.duration,
						"delay",
						0.1f,
						"looptype",
						iTween.LoopType.none
					}));
					iTween.PunchScale(this.m_theActualButton, iTween.Hash(new object[]
					{
						"name",
						"RecruitWobbleL",
						"x",
						this.amount,
						"y",
						this.amount,
						"time",
						this.duration,
						"delay",
						this.delay,
						"looptype",
						iTween.LoopType.loop
					}));
					iTween.PunchRotation(this.m_theActualButton, iTween.Hash(new object[]
					{
						"name",
						"RecruitButtonSwing",
						"z",
						30f,
						"time",
						2f
					}));
					iTween.PunchRotation(this.m_numReadyTroopsTextBG, iTween.Hash(new object[]
					{
						"name",
						"RecruitNumSwing",
						"z",
						50f,
						"time",
						3f
					}));
				}
				this.m_numReadyTroops = num;
				this.m_numReadyTroopsText.text = string.Empty + ((this.m_numReadyTroops <= 0) ? string.Empty : (string.Empty + this.m_numReadyTroops));
				this.m_numReadyTroopsTextBG.SetActive(this.m_numReadyTroops > 0);
			}
		}

		public GameObject m_numReadyTroopsTextBG;

		public Text m_numReadyTroopsText;

		public GameObject m_theActualButton;

		public float amount = 0.1f;

		public float duration = 1.2f;

		public float delay = 1.8f;

		public int m_numReadyTroops;

		private UiAnimMgr.UiAnimHandle m_glowHandle;

		private UiAnimMgr.UiAnimHandle m_glowLoopHandle;
	}
}
