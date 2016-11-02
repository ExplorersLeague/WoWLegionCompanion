using System;
using UnityEngine;

public class BuildNum : MonoBehaviour
{
	public static int CodeBuildNum
	{
		get
		{
			return 81;
		}
	}

	public static int DataBuildNum
	{
		get
		{
			return 33;
		}
	}

	private const int s_buildNum = 81;

	private const int s_dataBuildNum = 33;
}
