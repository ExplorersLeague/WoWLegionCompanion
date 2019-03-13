﻿using System;
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
			}
			(base.transform as RectTransform).sizeDelta = sizeDelta;
		}

		private void Update()
		{
		}

		protected override void OnRectTransformDimensionsChange()
		{
			base.OnRectTransformDimensionsChange();
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
			for (int i = num3; i < this.textures.Count; i++)
			{
				Object.Destroy(this.textures[i].gameObject);
			}
			for (int j = this.textures.Count; j < num3; j++)
			{
				Object @object = Object.Instantiate(this.randomTexturePrefab, base.transform, false);
				@object.name = this.randomTexturePrefab.GetType().Name + " " + j;
			}
			for (int k = 0; k < this.textures.Count; k++)
			{
				this.textures[k].RotateImage((float)this.rotation);
			}
		}

		public RandomTexture randomTexturePrefab;

		public TiledRandomTexture.TilingDirection tilingDirection;

		public TiledRandomTexture.Rotation rotation;

		private List<RandomTexture> textures;

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
