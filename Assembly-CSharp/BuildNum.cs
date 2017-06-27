using System;
using UnityEngine;

public class BuildNum : MonoBehaviour
{
	public static int CodeBuildNum
	{
		get
		{
			return 99;
		}
	}

	public static int DataBuildNum
	{
		get
		{
			return 45;
		}
	}

	private const int s_buildNum = 99;

	private const int s_dataBuildNum = 45;
}
