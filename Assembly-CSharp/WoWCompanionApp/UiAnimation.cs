using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI;
using WowStaticData;

namespace WoWCompanionApp
{
	public class UiAnimation : MonoBehaviour
	{
		private void serializer_UnknownNode(object sender, XmlNodeEventArgs e)
		{
		}

		private void serializer_UnknownAttribute(object sender, XmlAttributeEventArgs e)
		{
		}

		public float GetFrameWidth()
		{
			return this.m_frame.size.x;
		}

		public float GetFrameHeight()
		{
			return this.m_frame.size.y;
		}

		public void Deserialize(string animName)
		{
			XmlSerializer xmlSerializer = new XmlSerializer(typeof(UiAnimation.UiSourceAnimation));
			xmlSerializer.UnknownNode += this.serializer_UnknownNode;
			xmlSerializer.UnknownAttribute += this.serializer_UnknownAttribute;
			TextAsset sourceData = UiAnimMgr.instance.GetSourceData(animName);
			if (sourceData == null)
			{
				Debug.Log("Could not find asset " + animName);
				return;
			}
			MemoryStream memoryStream = new MemoryStream(sourceData.bytes);
			UiAnimation.UiSourceAnimation uiSourceAnimation = xmlSerializer.Deserialize(memoryStream) as UiAnimation.UiSourceAnimation;
			memoryStream.Close();
			if (uiSourceAnimation == null)
			{
				Debug.Log("No ui animation.");
				return;
			}
			this.m_frame = uiSourceAnimation.frame;
			foreach (UiAnimation.UiSourceAnimGroup uiSourceAnimGroup in uiSourceAnimation.frame.animation.groups)
			{
				UiAnimation.UiAnimGroup uiAnimGroup = new UiAnimation.UiAnimGroup();
				uiAnimGroup.m_parentKey = uiSourceAnimGroup.parentKey;
				uiAnimGroup.m_bounceBack = false;
				if (uiSourceAnimGroup.looping == null)
				{
					uiAnimGroup.m_looping = false;
					uiAnimGroup.m_bounce = false;
				}
				else if (uiSourceAnimGroup.looping == "REPEAT")
				{
					uiAnimGroup.m_looping = true;
					uiAnimGroup.m_bounce = false;
				}
				else if (uiSourceAnimGroup.looping == "BOUNCE")
				{
					uiAnimGroup.m_looping = true;
					uiAnimGroup.m_bounce = true;
				}
				foreach (UiAnimation.UiScale uiScale in uiSourceAnimGroup.m_scales)
				{
					if (uiScale.m_childKey != null)
					{
						uiScale.SetSmoothing();
						uiAnimGroup.m_elements.Add(uiScale);
					}
				}
				foreach (UiAnimation.UiAlpha uiAlpha in uiSourceAnimGroup.m_alphas)
				{
					if (uiAlpha.m_childKey != null)
					{
						uiAlpha.SetSmoothing();
						uiAnimGroup.m_elements.Add(uiAlpha);
					}
				}
				foreach (UiAnimation.UiRotation uiRotation in uiSourceAnimGroup.m_rotations)
				{
					if (uiRotation.m_childKey != null)
					{
						uiRotation.SetSmoothing();
						uiAnimGroup.m_elements.Add(uiRotation);
					}
				}
				foreach (UiAnimation.UiTranslation uiTranslation in uiSourceAnimGroup.m_translations)
				{
					if (uiTranslation.m_childKey != null)
					{
						uiTranslation.SetSmoothing();
						uiAnimGroup.m_elements.Add(uiTranslation);
					}
				}
				this.m_groups.Add(uiAnimGroup);
			}
			foreach (UiAnimation.UiLayer uiLayer in uiSourceAnimation.frame.layers)
			{
				using (List<UiAnimation.UiSourceTexture>.Enumerator enumerator7 = uiLayer.textures.GetEnumerator())
				{
					while (enumerator7.MoveNext())
					{
						UiAnimation.UiSourceTexture texture = enumerator7.Current;
						if (texture.m_parentKey != null)
						{
							UiAnimation.UiTexture uiTexture;
							this.m_textures.TryGetValue(texture.m_parentKey, out uiTexture);
							if (uiTexture != null)
							{
								Debug.Log("Found duplicate texture " + texture.m_parentKey);
							}
							else
							{
								int num = 0;
								UiTextureAtlasMemberRec recordFirstOrDefault = StaticDB.uiTextureAtlasMemberDB.GetRecordFirstOrDefault((UiTextureAtlasMemberRec memberRec) => memberRec.CommittedName != null && texture.m_atlas != null && memberRec.CommittedName.Equals(texture.m_atlas, StringComparison.OrdinalIgnoreCase));
								if (recordFirstOrDefault != null)
								{
									num = recordFirstOrDefault.ID;
								}
								Sprite sprite = null;
								if (num > 0)
								{
									sprite = TextureAtlas.GetSprite(num);
								}
								else if (texture.m_resourceImage != null)
								{
									sprite = Resources.Load<Sprite>(texture.m_resourceImage);
								}
								if (sprite != null)
								{
									UiAnimation.UiTexture uiTexture2 = new UiAnimation.UiTexture();
									uiTexture2.m_alpha = texture.m_alpha;
									uiTexture2.m_alphaMode = texture.m_alphaMode;
									uiTexture2.m_anchor = ((texture.m_anchors.Count <= 0) ? null : texture.m_anchors.ToArray()[0]);
									uiTexture2.m_atlas = texture.m_atlas;
									uiTexture2.m_resourceImage = texture.m_resourceImage;
									uiTexture2.m_width = texture.m_width;
									uiTexture2.m_height = texture.m_height;
									uiTexture2.m_hidden = texture.m_hidden;
									uiTexture2.m_parentKey = texture.m_parentKey;
									uiTexture2.m_sprite = sprite;
									this.m_textures.Add(texture.m_parentKey, uiTexture2);
								}
								else
								{
									Debug.Log(string.Concat(new object[]
									{
										"Could not find sprite for textureAtlasMemberID ",
										num,
										" resourceImage ",
										texture.m_resourceImage,
										" in Ui Animation ",
										animName
									}));
								}
							}
						}
					}
				}
			}
			List<UiAnimation.UiAnimElement> list = new List<UiAnimation.UiAnimElement>();
			foreach (UiAnimation.UiAnimGroup uiAnimGroup2 in this.m_groups)
			{
				uiAnimGroup2.m_maxTime = 0f;
				foreach (UiAnimation.UiAnimElement uiAnimElement in uiAnimGroup2.m_elements)
				{
					UiAnimation.UiTexture uiTexture3 = null;
					this.m_textures.TryGetValue(uiAnimElement.m_childKey, out uiTexture3);
					if (uiTexture3 != null)
					{
						uiAnimElement.m_texture = uiTexture3;
						float totalTime = uiAnimElement.GetTotalTime();
						if (totalTime > uiAnimGroup2.m_maxTime)
						{
							uiAnimGroup2.m_maxTime = totalTime;
						}
					}
					else
					{
						list.Add(uiAnimElement);
						Debug.Log("Removing element with childKey " + uiAnimElement.m_childKey + ", no associated texture was found.");
					}
				}
				foreach (UiAnimation.UiAnimElement item in list)
				{
					uiAnimGroup2.m_elements.Remove(item);
				}
			}
		}

		private bool IsTextureReferenced(string parentKey)
		{
			foreach (UiAnimation.UiAnimGroup uiAnimGroup in this.m_groups)
			{
				foreach (UiAnimation.UiAnimElement uiAnimElement in uiAnimGroup.m_elements)
				{
					if (uiAnimElement.m_childKey == parentKey)
					{
						return true;
					}
				}
			}
			return false;
		}

		public void Reset()
		{
			foreach (UiAnimation.UiAnimGroup uiAnimGroup in this.m_groups)
			{
				uiAnimGroup.Reset();
			}
		}

		public void Play(float fadeTime = 0f)
		{
			switch (this.m_state)
			{
			case UiAnimation.State.Stopped:
			case UiAnimation.State.Stopping:
				this.Reset();
				break;
			case UiAnimation.State.Playing:
				return;
			}
			this.m_state = UiAnimation.State.Playing;
			this.m_fadeTime = fadeTime;
			if (fadeTime > 0f)
			{
				this.m_fadeAlphaScalar = 0f;
				this.m_fadeStart = Time.timeSinceLevelLoad;
				this.Update();
			}
		}

		public void Stop(float fadeTime = 0f)
		{
			if (fadeTime > Mathf.Epsilon)
			{
				this.m_state = UiAnimation.State.Stopping;
				this.m_fadeTime = fadeTime;
				this.m_fadeStart = Time.timeSinceLevelLoad;
			}
			else
			{
				this.m_state = UiAnimation.State.Stopped;
				this.m_fadeTime = 0f;
				UiAnimMgr.instance.AnimComplete(this);
			}
		}

		private void Update()
		{
			if (this.m_state != UiAnimation.State.Playing && this.m_state != UiAnimation.State.Stopping)
			{
				return;
			}
			bool flag = true;
			foreach (UiAnimation.UiAnimGroup uiAnimGroup in this.m_groups)
			{
				if (!uiAnimGroup.Update(this.m_state == UiAnimation.State.Stopping))
				{
					flag = false;
				}
			}
			bool flag2 = false;
			if (this.m_state == UiAnimation.State.Playing && this.m_fadeTime > 0f)
			{
				float num = (Time.timeSinceLevelLoad - this.m_fadeStart) / this.m_fadeTime;
				if (num >= 1f)
				{
					this.m_fadeTime = 0f;
					num = 1f;
				}
				flag2 = true;
				this.m_fadeAlphaScalar = num;
			}
			if (this.m_state == UiAnimation.State.Stopping)
			{
				flag = false;
				float num2 = (Time.timeSinceLevelLoad - this.m_fadeStart) / this.m_fadeTime;
				if (num2 >= 1f)
				{
					num2 = 1f;
					flag = true;
				}
				num2 = 1f - num2;
				this.m_fadeAlphaScalar = num2;
				flag2 = true;
			}
			if (flag2)
			{
				foreach (UiAnimation.UiTexture uiTexture in this.m_textures.Values)
				{
					uiTexture.m_image.canvasRenderer.SetAlpha(uiTexture.m_alpha * this.m_fadeAlphaScalar);
				}
			}
			if (flag)
			{
				this.m_state = UiAnimation.State.Stopped;
				this.m_fadeTime = 0f;
				UiAnimMgr.instance.AnimComplete(this);
			}
		}

		public Dictionary<string, UiAnimation.UiTexture> m_textures = new Dictionary<string, UiAnimation.UiTexture>();

		private List<UiAnimation.UiAnimGroup> m_groups = new List<UiAnimation.UiAnimGroup>();

		private UiAnimation.UiFrame m_frame;

		private UiAnimation.State m_state;

		private float m_fadeTime;

		private float m_fadeStart;

		public int m_ID;

		private float m_fadeAlphaScalar = 1f;

		private enum State
		{
			Stopped,
			Stopping,
			Paused,
			Playing
		}

		public abstract class UiAnimElement
		{
			public void SetSmoothing()
			{
				if (this.m_smoothing == "IN")
				{
					this.m_smoothIn = true;
					this.m_smoothOut = false;
				}
				else if (this.m_smoothing == "OUT")
				{
					this.m_smoothIn = false;
					this.m_smoothOut = true;
				}
				else if (this.m_smoothing == "IN_OUT")
				{
					this.m_smoothIn = true;
					this.m_smoothOut = true;
				}
				else
				{
					this.m_smoothIn = false;
					this.m_smoothOut = false;
				}
			}

			public abstract void Update(float elapsedTime, float maxTime, bool reverse);

			public abstract void Reset();

			public float GetUnitProgress(float elapsedTime, float maxTime, bool reverse, out bool update)
			{
				update = true;
				float num2;
				if (reverse)
				{
					float num = maxTime - (this.m_startDelay + this.m_duration);
					num2 = elapsedTime - num;
					if (num2 < 0f)
					{
						update = false;
						return 0f;
					}
					if (num2 > this.m_duration)
					{
						update = false;
						return 1f;
					}
				}
				else
				{
					num2 = elapsedTime - this.m_startDelay;
					if (num2 < 0f)
					{
						update = false;
						return 0f;
					}
					if (num2 > this.m_duration)
					{
						update = false;
						return 1f;
					}
				}
				if (num2 <= 0f)
				{
					return 0f;
				}
				if (num2 < Mathf.Epsilon)
				{
					return 1f;
				}
				float num3 = num2 / this.m_duration;
				num3 = Mathf.Clamp01(num3);
				if (!this.m_smoothIn && !this.m_smoothOut)
				{
					if (reverse)
					{
						num3 = 1f - num3;
					}
					return num3;
				}
				if (this.m_smoothIn && num3 <= 0.5f)
				{
					num3 = 0.5f * (1f + Mathf.Sin((1f - 2f * num3) * -0.5f * 3.14159274f));
				}
				else if (this.m_smoothOut && num3 > 0.5f)
				{
					num3 = 0.5f + 0.5f * Mathf.Sin(2f * (num3 - 0.5f) * 0.5f * 3.14159274f);
				}
				num3 = Mathf.Clamp01(num3);
				if (reverse)
				{
					num3 = 1f - num3;
				}
				return num3;
			}

			public float GetTotalTime()
			{
				return this.m_startDelay + this.m_duration;
			}

			[XmlAttribute("childKey")]
			public string m_childKey;

			[XmlAttribute("smoothing")]
			public string m_smoothing;

			[XmlAttribute("duration")]
			public float m_duration;

			[XmlAttribute("startDelay")]
			public float m_startDelay;

			[XmlAttribute("order")]
			public int m_order;

			public object m_texture;

			public bool m_smoothIn;

			public bool m_smoothOut;
		}

		public class UiRotation : UiAnimation.UiAnimElement
		{
			public override void Update(float elapsedTime, float maxTime, bool reverse)
			{
				bool flag;
				float unitProgress = base.GetUnitProgress(elapsedTime, maxTime, reverse, out flag);
				if (!flag)
				{
					return;
				}
				UiAnimation.UiTexture uiTexture = (UiAnimation.UiTexture)this.m_texture;
				Quaternion localRotation = uiTexture.m_image.transform.localRotation;
				Vector3 eulerAngles = localRotation.eulerAngles;
				eulerAngles.z = this.m_degrees * unitProgress;
				localRotation.eulerAngles = eulerAngles;
				uiTexture.m_image.transform.localRotation = localRotation;
			}

			public override void Reset()
			{
			}

			[XmlAttribute("degrees")]
			public float m_degrees;
		}

		public class UiScale : UiAnimation.UiAnimElement
		{
			public override void Update(float elapsedTime, float maxTime, bool reverse)
			{
				bool flag;
				float unitProgress = base.GetUnitProgress(elapsedTime, maxTime, reverse, out flag);
				if (!flag)
				{
					return;
				}
				Vector3 localScale;
				localScale.x = this.m_fromScaleX + (this.m_toScaleX - this.m_fromScaleX) * unitProgress;
				localScale.y = this.m_fromScaleY + (this.m_toScaleY - this.m_fromScaleY) * unitProgress;
				localScale.z = 1f;
				UiAnimation.UiTexture uiTexture = (UiAnimation.UiTexture)this.m_texture;
				uiTexture.m_image.transform.localScale = localScale;
			}

			public override void Reset()
			{
			}

			[XmlAttribute("fromScaleX")]
			public float m_fromScaleX;

			[XmlAttribute("toScaleX")]
			public float m_toScaleX;

			[XmlAttribute("fromScaleY")]
			public float m_fromScaleY;

			[XmlAttribute("toScaleY")]
			public float m_toScaleY;
		}

		public class UiAlpha : UiAnimation.UiAnimElement
		{
			public override void Update(float elapsedTime, float maxTime, bool reverse)
			{
				bool flag;
				float unitProgress = base.GetUnitProgress(elapsedTime, maxTime, reverse, out flag);
				if (!flag)
				{
					return;
				}
				float alpha = this.m_fromAlpha + (this.m_toAlpha - this.m_fromAlpha) * unitProgress;
				UiAnimation.UiTexture uiTexture = (UiAnimation.UiTexture)this.m_texture;
				uiTexture.m_image.canvasRenderer.SetAlpha(alpha);
			}

			public override void Reset()
			{
			}

			[XmlAttribute("fromAlpha")]
			public float m_fromAlpha;

			[XmlAttribute("toAlpha")]
			public float m_toAlpha;
		}

		public class UiTranslation : UiAnimation.UiAnimElement
		{
			public override void Update(float elapsedTime, float maxTime, bool reverse)
			{
				bool flag;
				float unitProgress = base.GetUnitProgress(elapsedTime, maxTime, reverse, out flag);
				if (!flag)
				{
					return;
				}
				UiAnimation.UiTexture uiTexture = (UiAnimation.UiTexture)this.m_texture;
				RectTransform rectTransform = uiTexture.m_image.rectTransform;
				Vector2 localPosition = uiTexture.m_localPosition;
				localPosition.x += this.m_offsetX * unitProgress;
				localPosition.y += this.m_offsetY * unitProgress;
				rectTransform.localPosition = localPosition;
			}

			public override void Reset()
			{
				UiAnimation.UiTexture uiTexture = (UiAnimation.UiTexture)this.m_texture;
				Vector2 localPosition = uiTexture.m_localPosition;
				RectTransform rectTransform = uiTexture.m_image.rectTransform;
				rectTransform.localPosition = localPosition;
			}

			[XmlAttribute("offsetX")]
			public float m_offsetX;

			[XmlAttribute("offsetY")]
			public float m_offsetY;
		}

		public class UiSourceAnimGroup
		{
			[XmlAttribute("parentKey")]
			public string parentKey;

			[XmlAttribute("looping")]
			public string looping;

			[XmlElement("Alpha")]
			public List<UiAnimation.UiAlpha> m_alphas = new List<UiAnimation.UiAlpha>();

			[XmlElement("Scale")]
			public List<UiAnimation.UiScale> m_scales = new List<UiAnimation.UiScale>();

			[XmlElement("Rotation")]
			public List<UiAnimation.UiRotation> m_rotations = new List<UiAnimation.UiRotation>();

			[XmlElement("Translation")]
			public List<UiAnimation.UiTranslation> m_translations = new List<UiAnimation.UiTranslation>();
		}

		public class UiAnimGroup
		{
			public void Reset()
			{
				this.m_startTime = Time.timeSinceLevelLoad;
				this.m_bounceBack = false;
				foreach (UiAnimation.UiAnimElement uiAnimElement in this.m_elements)
				{
					uiAnimElement.Reset();
				}
			}

			public bool Update(bool stopping)
			{
				float num = Time.timeSinceLevelLoad - this.m_startTime;
				foreach (UiAnimation.UiAnimElement uiAnimElement in this.m_elements)
				{
					if (!stopping || !(uiAnimElement is UiAnimation.UiAlpha))
					{
						uiAnimElement.Update(num, this.m_maxTime, this.m_bounce && this.m_bounceBack);
					}
				}
				if (num >= this.m_maxTime && this.m_looping)
				{
					this.m_startTime = Time.timeSinceLevelLoad;
					if (this.m_bounce)
					{
						this.m_bounceBack = !this.m_bounceBack;
					}
					else
					{
						this.Reset();
					}
					return false;
				}
				return num >= this.m_maxTime;
			}

			public string m_parentKey;

			public bool m_looping;

			public bool m_bounce;

			public bool m_bounceBack;

			public float m_startTime;

			public float m_maxTime;

			public List<UiAnimation.UiAnimElement> m_elements = new List<UiAnimation.UiAnimElement>();
		}

		public class UiAnim
		{
			[XmlElement("AnimationGroup")]
			public List<UiAnimation.UiSourceAnimGroup> groups = new List<UiAnimation.UiSourceAnimGroup>();
		}

		public class UiAnchor
		{
			[XmlAttribute("point")]
			public string point;

			[XmlAttribute("relativePoint")]
			public string relativePoint;

			[XmlAttribute("x")]
			public float x;

			[XmlAttribute("y")]
			public float y;
		}

		public class UiSourceTexture
		{
			[XmlAttribute("parentKey")]
			public string m_parentKey;

			[XmlAttribute("hidden")]
			public bool m_hidden;

			[XmlAttribute("alpha")]
			public float m_alpha;

			[XmlAttribute("alphaMode")]
			public string m_alphaMode;

			[XmlAttribute("atlas")]
			public string m_atlas;

			[XmlAttribute("useAtlasSize")]
			public bool m_useAtlasSize;

			[XmlAttribute("resourceImage")]
			public string m_resourceImage;

			[XmlAttribute("w")]
			public string m_width;

			[XmlAttribute("h")]
			public string m_height;

			[XmlArray("Anchors")]
			[XmlArrayItem("Anchor")]
			public List<UiAnimation.UiAnchor> m_anchors = new List<UiAnimation.UiAnchor>();
		}

		public class UiTexture
		{
			public string m_parentKey;

			public bool m_hidden;

			public float m_alpha;

			public string m_alphaMode;

			public string m_atlas;

			public bool m_useAtlasSize;

			public string m_resourceImage;

			public string m_width;

			public string m_height;

			public UiAnimation.UiAnchor m_anchor;

			public int m_textureAtlasMemberID;

			public Sprite m_sprite;

			public Image m_image;

			public Vector2 m_localPosition;
		}

		public class UiLayer
		{
			[XmlAttribute("level")]
			public string level;

			[XmlElement("Texture")]
			public List<UiAnimation.UiSourceTexture> textures = new List<UiAnimation.UiSourceTexture>();
		}

		public class UiFrame
		{
			[XmlAttribute("hidden")]
			public bool hidden;

			[XmlAttribute("parent")]
			public string parent;

			[XmlAttribute("parentKey")]
			public string parentKey;

			[XmlAttribute("alpha")]
			public float alpha;

			[XmlElement("Size")]
			public UiAnimation.UiSize size = new UiAnimation.UiSize();

			[XmlArray("Layers")]
			[XmlArrayItem("Layer")]
			public List<UiAnimation.UiLayer> layers = new List<UiAnimation.UiLayer>();

			[XmlElement("Animations")]
			public UiAnimation.UiAnim animation = new UiAnimation.UiAnim();

			[XmlAttribute("name")]
			public string name;
		}

		public class UiSize
		{
			[XmlAttribute("x")]
			public float x;

			[XmlAttribute("y")]
			public float y;
		}

		[XmlRoot("Ui")]
		public class UiSourceAnimation
		{
			[XmlElement("Frame")]
			public UiAnimation.UiFrame frame;
		}
	}
}
