using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace WoWCompanionApp
{
	public class TiledRandomTexture : UIBehaviour
	{
		protected override void Start()
		{
			base.Start();
			Vector2 sizeDelta = (base.transform as RectTransform).sizeDelta;
			Vector2 sizeDelta2 = (this.randomTexturePrefab.transform as RectTransform).sizeDelta;
			if (this.tilingDirection == TiledRandomTexture.TilingDirection.Horizontal)
			{
				if (this.rotation == TiledRandomTexture.Rotation.Clockwise90 || this.rotation == TiledRandomTexture.Rotation.CounterClockwise90)
				{
					sizeDelta.y = sizeDelta2.x;
				}
				else
				{
					sizeDelta.y = sizeDelta2.y;
				}
				LayoutGroup layoutGroup = base.gameObject.GetComponent<LayoutGroup>() ?? base.gameObject.AddComponent<HorizontalLayoutGroup>();
				layoutGroup.childAlignment = 3;
				layoutGroup.enabled = true;
			}
			else
			{
				if (this.rotation == TiledRandomTexture.Rotation.Clockwise90 || this.rotation == TiledRandomTexture.Rotation.CounterClockwise90)
				{
					sizeDelta.x = sizeDelta2.y;
				}
				else
				{
					sizeDelta.x = sizeDelta2.x;
				}
				LayoutGroup layoutGroup2 = base.gameObject.GetComponent<LayoutGroup>() ?? base.gameObject.AddComponent<VerticalLayoutGroup>();
				layoutGroup2.childAlignment = 1;
				layoutGroup2.enabled = true;
			}
			(base.transform as RectTransform).sizeDelta = sizeDelta;
		}

		private void Update()
		{
			if (this.updateCounter < 5 || this.queueTextureUpdate)
			{
				this.UpdateRandomTexture();
				this.updateCounter++;
			}
		}

		protected override void OnRectTransformDimensionsChange()
		{
			base.OnRectTransformDimensionsChange();
			this.queueTextureUpdate = true;
		}

		private void UpdateRandomTexture()
		{
			RectTransform rectTransform = base.transform as RectTransform;
			float num = (this.tilingDirection != TiledRandomTexture.TilingDirection.Horizontal) ? rectTransform.rect.height : rectTransform.rect.width;
			float num2;
			if (this.tilingDirection == TiledRandomTexture.TilingDirection.Horizontal)
			{
				if (this.rotation == TiledRandomTexture.Rotation.Clockwise90 || this.rotation == TiledRandomTexture.Rotation.CounterClockwise90)
				{
					num2 = this.randomTexturePrefab.Height;
				}
				else
				{
					num2 = this.randomTexturePrefab.Width;
				}
			}
			else if (this.rotation == TiledRandomTexture.Rotation.Clockwise90 || this.rotation == TiledRandomTexture.Rotation.CounterClockwise90)
			{
				num2 = this.randomTexturePrefab.Width;
			}
			else
			{
				num2 = this.randomTexturePrefab.Height;
			}
			int num3 = Mathf.CeilToInt(num / num2);
			this.textures = base.GetComponentsInChildren<RandomTexture>().ToList<RandomTexture>();
			if (this.endcap != null && num3 < this.textures.Count && num3 > 1)
			{
				this.textures[num3 - 1].GetComponentInChildren<Image>().sprite = this.endcap;
			}
			int num4 = this.textures.Count - 1;
			while (num4 >= num3 && num4 >= 0)
			{
				Object.Destroy(this.textures[num4].gameObject);
				num4--;
			}
			if (this.endcap != null && num3 > this.textures.Count && this.textures.Count > 1)
			{
				this.textures[this.textures.Count - 1].GetComponent<RandomTexture>().ChangeTexture();
			}
			for (int i = this.textures.Count; i < num3; i++)
			{
				RandomTexture randomTexture = Object.Instantiate<RandomTexture>(this.randomTexturePrefab, base.transform, false);
				randomTexture.name = this.randomTexturePrefab.GetType().Name + " " + i;
				this.textures.Add(randomTexture);
			}
			if (this.endcap != null)
			{
				if (this.textures.Count > 0)
				{
					this.textures[0].GetComponentInChildren<Image>().sprite = this.endcap;
				}
				if (this.textures.Count > 1)
				{
					this.textures[this.textures.Count - 1].GetComponentInChildren<Image>().sprite = this.endcap;
				}
			}
			for (int j = 0; j < this.textures.Count; j++)
			{
				this.textures[j].RotateImage((float)this.rotation);
			}
			if (this.endcap != null && this.textures.Count > 1)
			{
				Vector3 localScale = this.textures[this.textures.Count - 1].GetComponentInChildren<Image>().transform.localScale;
				localScale.x = this.textures[0].GetComponentInChildren<Image>().transform.localScale.x * -1f;
				this.textures[this.textures.Count - 1].GetComponentInChildren<Image>().transform.localScale = localScale;
			}
			this.queueTextureUpdate = false;
		}

		public Sprite endcap;

		public RandomTexture randomTexturePrefab;

		public TiledRandomTexture.TilingDirection tilingDirection;

		public TiledRandomTexture.Rotation rotation;

		private List<RandomTexture> textures;

		private int updateCounter;

		private bool queueTextureUpdate;

		public enum TilingDirection
		{
			Horizontal,
			Vertical
		}

		public enum Rotation
		{
			None,
			Clockwise90 = 90,
			CounterClockwise90 = 270,
			Rotate180 = 180
		}
	}
}
