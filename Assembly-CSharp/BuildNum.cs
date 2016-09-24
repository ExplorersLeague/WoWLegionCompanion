using System;
using UnityEngine;

public class BuildNum : MonoBehaviour
{
	public static int CodeBuildNum
	{
		get
		{
			return 71;
		}
	}

	public static int DataBuildNum
	{
		get
		{
			return 31;
		}
	}

	private const int s_buildNum = 71;

	private const int s_dataBuildNum = 31;
}
