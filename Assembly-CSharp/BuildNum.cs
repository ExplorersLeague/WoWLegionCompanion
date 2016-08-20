using System;
using UnityEngine;

public class BuildNum : MonoBehaviour
{
	public static int CodeBuildNum
	{
		get
		{
			return 62;
		}
	}

	public static int DataBuildNum
	{
		get
		{
			return 29;
		}
	}

	private const int s_buildNum = 62;

	private const int s_dataBuildNum = 29;
}
