using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace WoWCompanionApp
{
	public class MapSelectDropdown : Dropdown
	{
		protected override void Awake()
		{
			base.Awake();
			this.closeButton.gameObject.SetActive(false);
		}

		protected override void OnEnable()
		{
			Transform transform = base.transform.Find("Dropdown List");
			if (transform != null)
			{
				this.DestroyDropdownList(transform.gameObject);
			}
		}

		protected override void OnDisable()
		{
			this.CloseDropdown();
		}

		private void Update()
		{
		}

		public void PopulateDropdown()
		{
			if (base.options.Count != MapInfo.GetAllMapInfos().Count)
			{
				base.ClearOptions();
				List<Dropdown.OptionData> list = (from mapInfo in MapInfo.GetAllMapInfos()
				select new Dropdown.OptionData
				{
					image = mapInfo.m_mapSwapButtonSprite,
					text = StaticDB.GetString(mapInfo.m_zoneNameKey, "[PH] " + mapInfo.m_zoneNameKey)
				}).ToList<Dropdown.OptionData>();
				list.Add(new Dropdown.OptionData
				{
					image = null,
					text = string.Empty
				});
				base.AddOptions(list);
				base.RefreshShownValue();
				base.value = MapInfo.GetAllMapInfos().IndexOf(AdventureMapPanel.instance.m_activeMapInfo);
			}
		}

		public override void OnPointerClick(PointerEventData eventData)
		{
			base.OnPointerClick(eventData);
			if (this.buttonBanner != null)
			{
				this.buttonBanner.gameObject.SetActive(false);
			}
			if (this.buttonIcon != null)
			{
				this.buttonIcon.gameObject.SetActive(false);
			}
			Transform transform = base.transform.Find("Dropdown List");
			if (transform != null)
			{
				Toggle[] componentsInChildren = transform.gameObject.GetComponentsInChildren<Toggle>(false);
				RectTransform rectTransform = transform as RectTransform;
				rectTransform.pivot = Vector2.right;
				int num = 3;
				RectTransform rectTransform2 = base.transform.parent as RectTransform;
				Vector2 anchoredPosition = default(Vector2);
				anchoredPosition.x = Mathf.Abs(rectTransform2.sizeDelta.x / 2f);
				anchoredPosition.y = -(Mathf.Abs(rectTransform.sizeDelta.y) - Mathf.Abs(rectTransform2.sizeDelta.y) + Mathf.Abs(rectTransform2.anchoredPosition.y)) + (float)num;
				rectTransform.anchoredPosition = anchoredPosition;
				if (this.bannerPieces.Length > 1)
				{
					int num2 = this.bannerPieces.Length - 2;
					GameObject gameObject;
					for (int i = componentsInChildren.Length - 2; i >= 0; i--)
					{
						gameObject = Object.Instantiate<GameObject>(this.bannerPieces[num2], componentsInChildren[i].transform, false);
						gameObject.transform.SetAsFirstSibling();
						num2--;
						if (num2 < 0)
						{
							num2 = this.bannerPieces.Length - 2;
						}
					}
					gameObject = Object.Instantiate<GameObject>(this.bannerPieces.Last<GameObject>(), componentsInChildren.Last<Toggle>().transform, false);
					gameObject.transform.SetAsFirstSibling();
				}
				componentsInChildren.Last<Toggle>().interactable = false;
				if (base.value >= 0 && base.value < componentsInChildren.Length)
				{
					componentsInChildren[base.value].targetGraphic = componentsInChildren[base.value].GetComponent<Dropdown.DropdownItem>().text;
					ColorBlock colors = componentsInChildren[base.value].colors;
					colors.normalColor = Color.gray;
					colors.highlightedColor = Color.gray;
					colors.pressedColor = Color.gray;
					componentsInChildren[base.value].colors = colors;
					componentsInChildren[base.value].GetComponent<Dropdown.DropdownItem>().image.color = Color.gray;
				}
			}
			Main.instance.m_UISound.Play_ButtonBlackClick();
		}

		public void OnDropdownValueChanged(int value)
		{
			AdventureMapPanel.instance.SetMap(MapInfo.GetAllMapInfos()[value].m_zone);
		}

		public void CloseDropdown()
		{
			base.Hide();
		}

		protected override GameObject CreateBlocker(Canvas rootCanvas)
		{
			RectTransform rectTransform = this.closeButton.transform as RectTransform;
			if (AdventureMapPanel.instance != null)
			{
				RectTransform rectTransform2 = AdventureMapPanel.instance.transform as RectTransform;
				rectTransform.position = rectTransform2.position;
				rectTransform.SetSizeWithCurrentAnchors(0, rectTransform2.rect.width);
				rectTransform.SetSizeWithCurrentAnchors(1, rectTransform2.rect.height);
			}
			this.closeButton.gameObject.SetActive(true);
			return this.closeButton.gameObject;
		}

		protected override void DestroyBlocker(GameObject blocker)
		{
			this.closeButton.gameObject.SetActive(false);
			if (this.buttonBanner != null)
			{
				this.buttonBanner.gameObject.SetActive(true);
			}
			if (this.buttonIcon != null)
			{
				this.buttonIcon.gameObject.SetActive(true);
			}
		}

		public Button closeButton;

		public GameObject[] bannerPieces;

		public GameObject buttonIcon;

		public GameObject buttonBanner;
	}
}
