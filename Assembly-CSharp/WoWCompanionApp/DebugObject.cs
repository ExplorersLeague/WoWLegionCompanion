using System;
using UnityEngine;

namespace WoWCompanionApp
{
	public class DebugObject : MonoBehaviour
	{
		private void Awake()
		{
			Object.Destroy(base.gameObject);
		}
	}
}
