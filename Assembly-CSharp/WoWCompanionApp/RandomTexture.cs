using System;
using UnityEngine;
using UnityEngine.UI;

namespace WoWCompanionApp
{
	[ExecuteInEditMode]
	public class RandomTexture : MonoBehaviour
	{
		public float Width
		{
			get
			{
				return (this.sprites == null || this.sprites.Length <= 0 || !(this.sprites[0] != null)) ? (base.transform as RectTransform).rect.width : ((float)this.sprites[0].texture.width);
			}
		}

		public float Height
		{
			get
			{
				return (this.sprites == null || this.sprites.Length <= 0 || !(this.sprites[0] != null)) ? (base.transform as RectTransform).rect.height : ((float)this.sprites[0].texture.height);
			}
		}

		private void Start()
		{
			this.image = base.GetComponentInChildren<Image>();
			this.ChangeTexture();
		}

		private void Update()
		{
		}

		public void ChangeTexture()
		{
			if (this.sprites == null || this.sprites.Length == 0)
			{
				return;
			}
			this.image.sprite = this.sprites[Random.Range(0, this.sprites.Length - 1)];
			this.image.preserveAspect = this.preserveAspect;
			RectTransform rectTransform = base.transform as RectTransform;
			rectTransform.sizeDelta = new Vector2((float)this.image.sprite.texture.width, (float)this.image.sprite.texture.height);
		}

		public void RotateImage(float degrees)
		{
			if (this.image == null)
			{
				this.image = base.GetComponentInChildren<Image>();
			}
			RectTransform rectTransform = this.image.transform as RectTransform;
			rectTransform.rotation = Quaternion.identity;
			rectTransform.Rotate(Vector3.forward, degrees);
		}

		public Sprite[] sprites;

		private Image image;

		public bool preserveAspect = true;
	}
}
