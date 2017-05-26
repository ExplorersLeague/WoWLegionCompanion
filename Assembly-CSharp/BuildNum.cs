using System;
using UnityEngine;

public class BuildNum : MonoBehaviour
{
	public static int CodeBuildNum
	{
		get
		{
			return 96;
		}
	}

	public static int DataBuildNum
	{
		get
		{
			return 44;
		}
	}

	private const int s_buildNum = 96;

	private const int s_dataBuildNum = 44;
}
