using System;
using UnityEngine;

public class MultipanelDefaultPositioner : MonoBehaviour
{
	private void Start()
	{
		Vector3 localPosition = base.gameObject.transform.localPosition;
		localPosition.x = -1690f;
		base.gameObject.transform.localPosition = localPosition;
	}
}
