using System;
using UnityEngine;

public class BuildNum : MonoBehaviour
{
	public static int CodeBuildNum
	{
		get
		{
			return 85;
		}
	}

	public static int DataBuildNum
	{
		get
		{
			return 35;
		}
	}

	private const int s_buildNum = 85;

	private const int s_dataBuildNum = 35;
}
