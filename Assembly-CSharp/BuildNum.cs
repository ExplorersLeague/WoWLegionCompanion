using System;
using UnityEngine;

public class BuildNum : MonoBehaviour
{
	public static int CodeBuildNum
	{
		get
		{
			return 125;
		}
	}

	public static int DataBuildNum
	{
		get
		{
			return 61;
		}
	}

	private const int s_buildNum = 125;

	private const int s_dataBuildNum = 61;
}
