using System;
using UnityEngine;

public class BuildNum : MonoBehaviour
{
	public static int CodeBuildNum
	{
		get
		{
			return 124;
		}
	}

	public static int DataBuildNum
	{
		get
		{
			return 60;
		}
	}

	private const int s_buildNum = 124;

	private const int s_dataBuildNum = 60;
}
