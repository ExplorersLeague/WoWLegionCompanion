using System;
using UnityEngine;

public class BuildNum : MonoBehaviour
{
	public static int CodeBuildNum
	{
		get
		{
			return 114;
		}
	}

	public static int DataBuildNum
	{
		get
		{
			return 57;
		}
	}

	private const int s_buildNum = 114;

	private const int s_dataBuildNum = 57;
}
