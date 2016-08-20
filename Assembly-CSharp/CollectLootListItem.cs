using System;
using UnityEngine;
using UnityEngine.UI;

public class CollectLootListItem : MonoBehaviour
{
	private void Start()
	{
	}

	private void Update()
	{
	}

	public void CompleteAllMissions()
	{
		Main.instance.CompleteAllMissions();
	}

	public Text completedMissionsText;
}
