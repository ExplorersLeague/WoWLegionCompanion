using System;
using UnityEngine;
using UnityEngine.UI;

namespace WoWCompanionApp
{
	[RequireComponent(typeof(Text))]
	[RequireComponent(typeof(DebugObject))]
	public class BuildNumDisplay : MonoBehaviour
	{
		private void Start()
		{
			base.GetComponent<Text>().text = "Build num: " + MobileBuild.GetBuildNum();
		}
	}
}
