using System;
using UnityEngine;

namespace WoWCompanionApp
{
	public class FollowerDetailListFakeRandomTiling : MonoBehaviour
	{
		private void Start()
		{
			RectTransform component = base.gameObject.GetComponent<RectTransform>();
			component.offsetMin = new Vector2(Random.Range(-300f, -2000f), component.offsetMin.y);
			component.offsetMax = new Vector2(-component.offsetMin.x, component.offsetMax.y);
			if (Random.Range(0, 2) == 0)
			{
				component.localScale = new Vector3(-1f * component.localScale.x, component.localScale.y, component.localScale.z);
			}
			if (Random.Range(0, 2) == 0)
			{
				component.localScale = new Vector3(component.localScale.x, -1f * component.localScale.y, component.localScale.z);
			}
		}
	}
}
