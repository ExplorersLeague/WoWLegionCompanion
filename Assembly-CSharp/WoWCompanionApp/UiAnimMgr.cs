using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace WoWCompanionApp
{
	public class UiAnimMgr
	{
		public Material m_blendMaterial { get; private set; }

		public Material m_additiveMaterial { get; private set; }

		public static UiAnimMgr instance
		{
			get
			{
				if (UiAnimMgr.s_instance == null)
				{
					UiAnimMgr.s_instance = new UiAnimMgr();
					UiAnimMgr.s_instance.InitAnimMgr();
				}
				return UiAnimMgr.s_instance;
			}
		}

		private void InitAnimMgr()
		{
			if (UiAnimMgr.s_initialized)
			{
				Debug.Log("Warning: AnimMgr already initialized.");
				return;
			}
			this.m_parentObj = new GameObject();
			this.m_parentObj.name = "UiAnimMgr Parent";
			this.m_additiveMaterial = (Resources.Load("Materials/UiAdditive") as Material);
			this.m_blendMaterial = (Resources.Load("Materials/UiBlend") as Material);
			this.m_animData = new Dictionary<string, UiAnimMgr.AnimData>();
			TextAsset[] array = Resources.LoadAll<TextAsset>("UiAnimations");
			uint num = 0u;
			while ((ulong)num < (ulong)((long)array.Length))
			{
				UiAnimMgr.AnimData animData = new UiAnimMgr.AnimData();
				animData.m_sourceData = array[(int)((UIntPtr)num)];
				animData.m_animName = array[(int)((UIntPtr)num)].name;
				animData.m_activeObjects = new List<GameObject>();
				animData.m_availableObjects = new Stack<GameObject>();
				this.m_animData.Add(array[(int)((UIntPtr)num)].name, animData);
				GameObject gameObject = this.CreateAnimObj(array[(int)((UIntPtr)num)].name, true);
				gameObject.SetActive(false);
				gameObject.transform.SetParent(this.m_parentObj.transform);
				num += 1u;
			}
			this.m_idIndex = 0;
			UiAnimMgr.s_initialized = true;
		}

		public TextAsset GetSourceData(string key)
		{
			UiAnimMgr.AnimData animData = null;
			this.m_animData.TryGetValue(key, out animData);
			if (animData == null)
			{
				return null;
			}
			return animData.m_sourceData;
		}

		public UiAnimMgr.UiAnimHandle PlayAnim(string animName, Transform parent, Vector3 localPos, float localScale, float fadeTime = 0f)
		{
			GameObject gameObject = this.CreateAnimObj(animName, false);
			if (gameObject == null)
			{
				return null;
			}
			gameObject.transform.SetParent(parent, false);
			gameObject.transform.localPosition = localPos;
			gameObject.transform.localScale = new Vector3(localScale, localScale, localScale);
			UiAnimation component = gameObject.GetComponent<UiAnimation>();
			component.Play(fadeTime);
			return new UiAnimMgr.UiAnimHandle(component);
		}

		private GameObject CreateAnimObj(string animName, bool createForInit = false)
		{
			UiAnimMgr.AnimData animData;
			this.m_animData.TryGetValue(animName, out animData);
			if (animData == null)
			{
				return null;
			}
			GameObject gameObject = null;
			if (!createForInit && animData.m_availableObjects.Count > 0)
			{
				gameObject = animData.m_availableObjects.Pop();
			}
			if (gameObject != null)
			{
				if (animData.m_activeObjects.Contains(gameObject))
				{
					Debug.Log("Error! new anim object already in active object list.");
				}
				else
				{
					animData.m_activeObjects.Add(gameObject);
				}
				gameObject.SetActive(true);
				UiAnimation component = gameObject.GetComponent<UiAnimation>();
				component.Reset();
				component.m_ID = this.GetNextID();
				return gameObject;
			}
			gameObject = new GameObject();
			if (createForInit)
			{
				animData.m_availableObjects.Push(gameObject);
			}
			else if (animData.m_activeObjects.Contains(gameObject))
			{
				Debug.Log("Error! new anim object already in active object list.");
			}
			else
			{
				animData.m_activeObjects.Add(gameObject);
			}
			CanvasGroup canvasGroup = gameObject.AddComponent<CanvasGroup>();
			canvasGroup.blocksRaycasts = false;
			canvasGroup.interactable = false;
			gameObject.name = animName;
			UiAnimation uiAnimation = gameObject.AddComponent<UiAnimation>();
			uiAnimation.m_ID = this.GetNextID();
			uiAnimation.Deserialize(animName);
			RectTransform rectTransform = gameObject.AddComponent<RectTransform>();
			rectTransform.SetSizeWithCurrentAnchors(0, uiAnimation.GetFrameWidth());
			rectTransform.SetSizeWithCurrentAnchors(1, uiAnimation.GetFrameHeight());
			foreach (UiAnimation.UiTexture uiTexture in uiAnimation.m_textures.Values)
			{
				GameObject gameObject2 = new GameObject();
				gameObject2.name = uiTexture.m_parentKey + "_Texture";
				gameObject2.transform.SetParent(gameObject.transform, false);
				uiTexture.m_image = gameObject2.AddComponent<Image>();
				uiTexture.m_image.sprite = uiTexture.m_sprite;
				uiTexture.m_image.canvasRenderer.SetAlpha(uiTexture.m_alpha);
				if (uiTexture.m_alphaMode == "ADD")
				{
					uiTexture.m_image.material = new Material(UiAnimMgr.instance.m_additiveMaterial);
				}
				else
				{
					uiTexture.m_image.material = new Material(UiAnimMgr.instance.m_blendMaterial);
				}
				uiTexture.m_image.material.mainTexture = uiTexture.m_sprite.texture;
				RectTransform component2 = gameObject2.GetComponent<RectTransform>();
				if (uiTexture.m_anchor != null && uiTexture.m_anchor.relativePoint != null)
				{
					string relativePoint = uiTexture.m_anchor.relativePoint;
					switch (relativePoint)
					{
					case "TOP":
						component2.anchorMin = new Vector2(0.5f, 1f);
						component2.anchorMax = component2.anchorMin;
						break;
					case "BOTTOM":
						component2.anchorMin = new Vector2(0.5f, 0f);
						component2.anchorMax = component2.anchorMin;
						break;
					case "LEFT":
						component2.anchorMin = new Vector2(0f, 0.5f);
						component2.anchorMax = component2.anchorMin;
						break;
					case "RIGHT":
						component2.anchorMin = new Vector2(1f, 0.5f);
						component2.anchorMax = component2.anchorMin;
						break;
					case "CENTER":
						component2.anchorMin = new Vector2(0.5f, 0.5f);
						component2.anchorMax = component2.anchorMin;
						break;
					case "TOPLEFT":
						component2.anchorMin = new Vector2(0f, 1f);
						component2.anchorMax = component2.anchorMin;
						break;
					case "TOPRIGHT":
						component2.anchorMin = new Vector2(1f, 1f);
						component2.anchorMax = component2.anchorMin;
						break;
					case "BOTTOMLEFT":
						component2.anchorMin = new Vector2(0f, 0f);
						component2.anchorMax = component2.anchorMin;
						break;
					case "BOTTOMRIGHT":
						component2.anchorMin = new Vector2(1f, 0f);
						component2.anchorMax = component2.anchorMin;
						break;
					}
				}
				Vector2 vector = default(Vector2);
				if (uiTexture.m_anchor != null && uiTexture.m_anchor.point != null)
				{
					string point = uiTexture.m_anchor.point;
					switch (point)
					{
					case "TOP":
						vector.Set(0f, -0.5f * uiTexture.m_image.sprite.rect.height);
						break;
					case "BOTTOM":
						vector.Set(0f, 0.5f * uiTexture.m_image.sprite.rect.height);
						break;
					case "LEFT":
						vector.Set(0.5f * uiTexture.m_image.sprite.rect.width, 0f);
						break;
					case "RIGHT":
						vector.Set(-0.5f * uiTexture.m_image.sprite.rect.width, 0f);
						break;
					case "TOPLEFT":
						vector.Set(0.5f * uiTexture.m_image.sprite.rect.width, -0.5f * uiTexture.m_image.sprite.rect.height);
						break;
					case "TOPRIGHT":
						vector.Set(-0.5f * uiTexture.m_image.sprite.rect.width, -0.5f * uiTexture.m_image.sprite.rect.height);
						break;
					case "BOTTOMLEFT":
						vector.Set(0.5f * uiTexture.m_image.sprite.rect.width, 0.5f * uiTexture.m_image.sprite.rect.height);
						break;
					case "BOTTOMRIGHT":
						vector.Set(-0.5f * uiTexture.m_image.sprite.rect.width, 0.5f * uiTexture.m_image.sprite.rect.height);
						break;
					}
				}
				component2.anchoredPosition = new Vector2(vector.x + uiTexture.m_anchor.x, vector.y + uiTexture.m_anchor.y);
				int num2 = 0;
				int num3 = 0;
				int.TryParse(uiTexture.m_width, out num2);
				int.TryParse(uiTexture.m_height, out num3);
				component2.SetSizeWithCurrentAnchors(0, (num2 <= 0) ? uiTexture.m_image.sprite.rect.width : ((float)num2));
				component2.SetSizeWithCurrentAnchors(1, (num3 <= 0) ? uiTexture.m_image.sprite.rect.height : ((float)num3));
			}
			foreach (UiAnimation.UiTexture uiTexture2 in uiAnimation.m_textures.Values)
			{
				RectTransform rectTransform2 = uiTexture2.m_image.rectTransform;
				uiTexture2.m_localPosition = rectTransform2.localPosition;
			}
			return gameObject;
		}

		public void AnimComplete(UiAnimation script)
		{
			GameObject gameObject = script.gameObject;
			UiAnimMgr.AnimData animData;
			this.m_animData.TryGetValue(gameObject.name, out animData);
			if (animData == null)
			{
				Debug.Log("Error! UiAnimMgr could not find completed anim " + gameObject.name);
				return;
			}
			if (!animData.m_activeObjects.Remove(gameObject))
			{
				Debug.Log("Error! anim obj " + gameObject.name + "not in UiAnimMgr active list");
			}
			animData.m_availableObjects.Push(gameObject);
			gameObject.SetActive(false);
			if (this.m_parentObj == null)
			{
				gameObject.transform.SetParent(null);
			}
			else
			{
				gameObject.transform.SetParent(this.m_parentObj.transform);
			}
			UiAnimation component = gameObject.GetComponent<UiAnimation>();
			component.m_ID = 0;
		}

		public int GetNumActiveAnims()
		{
			int num = 0;
			foreach (KeyValuePair<string, UiAnimMgr.AnimData> keyValuePair in this.m_animData)
			{
				num += keyValuePair.Value.m_activeObjects.Count;
			}
			return num;
		}

		public int GetNumAvailableAnims()
		{
			int num = 0;
			foreach (KeyValuePair<string, UiAnimMgr.AnimData> keyValuePair in this.m_animData)
			{
				num += keyValuePair.Value.m_availableObjects.Count;
			}
			return num;
		}

		private int GetNextID()
		{
			this.m_idIndex++;
			return this.m_idIndex;
		}

		private static UiAnimMgr s_instance;

		private static bool s_initialized;

		private Dictionary<string, UiAnimMgr.AnimData> m_animData;

		private int m_idIndex;

		private GameObject m_parentObj;

		private class AnimData
		{
			public TextAsset m_sourceData;

			public string m_animName;

			public List<GameObject> m_activeObjects;

			public Stack<GameObject> m_availableObjects;
		}

		public class UiAnimHandle
		{
			public UiAnimHandle(UiAnimation anim)
			{
				this.m_anim = anim;
				this.m_ID = anim.m_ID;
			}

			public UiAnimation GetAnim()
			{
				if (this.m_anim == null)
				{
					return null;
				}
				if (this.m_anim.m_ID == this.m_ID)
				{
					return this.m_anim;
				}
				return null;
			}

			private UiAnimation m_anim;

			private int m_ID;
		}
	}
}
