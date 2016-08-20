using System;
using UnityEngine;

public class MissionFollowerSlotGroup : MonoBehaviour
{
	private void Start()
	{
	}

	private void Update()
	{
	}

	public bool SetFollower(int garrFollowerID, Sprite followerPortraitImage, Color qualityColor, bool forceReplaceFirstSlot = false)
	{
		MissionFollowerSlot[] componentsInChildren = base.gameObject.GetComponentsInChildren<MissionFollowerSlot>(true);
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			if (componentsInChildren[i].GetCurrentGarrFollowerID() == garrFollowerID)
			{
				componentsInChildren[i].SetFollower(0);
				return false;
			}
		}
		if (forceReplaceFirstSlot)
		{
			if (componentsInChildren[0].IsOccupied())
			{
				componentsInChildren[0].ClearFollower();
			}
			componentsInChildren[0].SetFollower(garrFollowerID);
			return true;
		}
		for (int j = 0; j < componentsInChildren.Length; j++)
		{
			if (!componentsInChildren[j].IsOccupied())
			{
				componentsInChildren[j].SetFollower(garrFollowerID);
				return true;
			}
		}
		return false;
	}
}
