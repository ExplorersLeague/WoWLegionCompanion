using System;
using UnityEngine;
using UnityEngine.UI;
using WowStatConstants;
using WowStaticData;

namespace WoWCompanionApp
{
	public class ArmamentPopup : MonoBehaviour
	{
		public void OnEnable()
		{
			Main.instance.m_canvasBlurManager.AddBlurRef_MainCanvas();
		}

		private void OnDisable()
		{
			Main.instance.m_canvasBlurManager.RemoveBlurRef_MainCanvas();
		}

		public void SetArmament(WrapperFollowerArmamentExt item, int garrFollowerID)
		{
			this.m_garrFollowerID = garrFollowerID;
			this.m_item = item;
			ItemRec record = StaticDB.itemDB.GetRecord(item.ItemID);
			this.m_armamentName.text = record.Display;
			SpellTooltipRec record2 = StaticDB.spellTooltipDB.GetRecord(item.SpellID);
			if (record2 != null)
			{
				this.m_armamentDescription.text = record2.Description;
			}
			else
			{
				this.m_armamentDescription.text = string.Concat(new object[]
				{
					"ERROR. Unknown Spell ID: ",
					item.SpellID,
					" Item ID:",
					item.ItemID
				});
			}
			this.m_armamentDescription.text = WowTextParser.parser.Parse(this.m_armamentDescription.text, item.SpellID);
			if (this.m_iconErrorText != null)
			{
				this.m_iconErrorText.gameObject.SetActive(false);
			}
			Sprite sprite = GeneralHelpers.LoadIconAsset(AssetBundleType.Icons, record.IconFileDataID);
			if (sprite != null)
			{
				this.m_armamentIcon.sprite = sprite;
			}
			else if (this.m_iconErrorText != null)
			{
				this.m_iconErrorText.gameObject.SetActive(true);
				this.m_iconErrorText.text = string.Empty + record.IconFileDataID;
			}
			this.m_armamentQuantity.text = ((item.Quantity <= 1) ? string.Empty : (string.Empty + item.Quantity));
		}

		public void UseArmament()
		{
			Debug.Log(string.Concat(new object[]
			{
				"Attempting to use armament item ",
				this.m_item.ItemID,
				" for follower ",
				this.m_garrFollowerID
			}));
			Singleton<GarrisonWrapper>.Instance.UseArmament(this.m_garrFollowerID, this.m_item.ItemID);
			base.gameObject.SetActive(false);
		}

		[Header("Armament")]
		public Image m_armamentIcon;

		public Text m_armamentName;

		public Text m_armamentQuantity;

		public Text m_armamentDescription;

		[Header("Error reporting")]
		public Text m_iconErrorText;

		private int m_garrFollowerID;

		private WrapperFollowerArmamentExt m_item;
	}
}
