using System;
using UnityEngine;

public class DelayedObjectEnable : MonoBehaviour
{
	public void Init(float delayTime, GameObject objectToEnable)
	{
		this.m_delayTime = delayTime;
		this.m_objectToEnable = objectToEnable;
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
				this.m_objectToEnable.SetActive(true);
				Object.DestroyImmediate(this);
			}
		}
	}

	private float m_delayTime;

	private GameObject m_objectToEnable;

	private float m_timeRemaining;

	private bool m_enabled;
}
