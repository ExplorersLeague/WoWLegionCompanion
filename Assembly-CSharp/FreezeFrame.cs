using System;
using UnityEngine;

public class FreezeFrame : MonoBehaviour
{
	private void Start()
	{
		this.MakeSnapshot();
	}

	private void MakeSnapshot()
	{
		DarkRoom.MakeSnapshot(this);
	}
}
