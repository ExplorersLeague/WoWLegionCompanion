using System;
using UnityEngine;

public class CogRotationConnectingScreen : MonoBehaviour
{
	private void Update()
	{
		base.transform.Rotate(Vector3.back, this.speed * Time.deltaTime);
	}

	public float speed;
}
