using System;
using UnityEngine;

public class BuildNum : MonoBehaviour
{
	public static int CodeBuildNum
	{
		get
		{
			return 65;
		}
	}

	public static int DataBuildNum
	{
		get
		{
			return 30;
		}
	}

	private const int s_buildNum = 65;

	private const int s_dataBuildNum = 30;
}
