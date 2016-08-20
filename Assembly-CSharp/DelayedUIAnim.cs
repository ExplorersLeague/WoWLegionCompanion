using System;
using UnityEngine;

public class DelayedUIAnim : MonoBehaviour
{
	public void Init(float delayTime, string animName, string sfxName, Transform parentTransform, float scale)
	{
		this.m_delayTime = delayTime;
		this.m_animName = animName;
		this.m_sfxName = sfxName;
		this.m_parentTransform = parentTransform;
		this.m_scale = scale;
		this.m_enabled = true;
		this.m_timeRemaining = this.m_delayTime;
	}

	private void Update()
	{
		if (this.m_enabled)
		{
			this.m_timeRemaining -= Time.deltaTime;
			if (this.m_timeRemaining <= 0f)
			{
				if (this.m_sfxName != null)
				{
					Main.instance.m_UISound.PlayUISound(this.m_sfxName, 1f);
				}
				UiAnimMgr.instance.PlayAnim(this.m_animName, this.m_parentTransform, Vector3.zero, this.m_scale, 0f);
				Object.DestroyImmediate(this);
			}
		}
	}

	private float m_delayTime;

	private string m_animName;

	private string m_sfxName;

	private Transform m_parentTransform;

	private float m_scale;

	private float m_timeRemaining;

	private bool m_enabled;
}
