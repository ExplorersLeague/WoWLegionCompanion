using System;
using UnityEngine;

public class BuildNum : MonoBehaviour
{
	public static int CodeBuildNum
	{
		get
		{
			return 84;
		}
	}

	public static int DataBuildNum
	{
		get
		{
			return 35;
		}
	}

	private const int s_buildNum = 84;

	private const int s_dataBuildNum = 35;
}
