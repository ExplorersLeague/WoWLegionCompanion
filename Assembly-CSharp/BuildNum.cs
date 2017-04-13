using System;
using UnityEngine;

public class BuildNum : MonoBehaviour
{
	public static int CodeBuildNum
	{
		get
		{
			return 94;
		}
	}

	public static int DataBuildNum
	{
		get
		{
			return 43;
		}
	}

	private const int s_buildNum = 94;

	private const int s_dataBuildNum = 43;
}
