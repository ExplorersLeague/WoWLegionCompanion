using System;
using UnityEngine;

public class BuildNum : MonoBehaviour
{
	public static int CodeBuildNum
	{
		get
		{
			return 107;
		}
	}

	public static int DataBuildNum
	{
		get
		{
			return 54;
		}
	}

	private const int s_buildNum = 107;

	private const int s_dataBuildNum = 54;
}
