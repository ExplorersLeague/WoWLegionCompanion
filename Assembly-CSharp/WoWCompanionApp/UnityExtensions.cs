using System;
using System.Text;
using UnityEngine;

namespace WoWCompanionApp
{
	public static class UnityExtensions
	{
		public static string GetCanonicalName(this GameObject gameObj, string fieldName = null)
		{
			StringBuilder stringBuilder = new StringBuilder();
			int num = 0;
			while (gameObj.transform != gameObj.transform.root && num < 10)
			{
				stringBuilder.Insert(0, ".").Insert(0, gameObj.name);
				gameObj = gameObj.transform.parent.gameObject;
				num++;
			}
			stringBuilder.Insert(0, ".").Insert(0, gameObj.name);
			stringBuilder.Insert(0, "::").Insert(0, gameObj.scene.name ?? "<Prefab>");
			if (!string.IsNullOrEmpty(fieldName))
			{
				stringBuilder.Append(fieldName);
			}
			else
			{
				stringBuilder.Remove(stringBuilder.Length - 1, 1);
			}
			return stringBuilder.ToString();
		}

		public static T AddAsChildObject<T>(this GameObject gameObj, T prefab) where T : Component
		{
			T result = Object.Instantiate<T>(prefab);
			result.transform.SetParent(gameObj.transform);
			result.transform.localScale = prefab.transform.localScale;
			result.transform.SetAsLastSibling();
			return result;
		}

		public static GameObject AddAsChildObject(this GameObject gameObj, GameObject prefab)
		{
			GameObject gameObject = Object.Instantiate<GameObject>(prefab);
			gameObject.transform.SetParent(gameObj.transform);
			gameObject.transform.localScale = prefab.transform.localScale;
			gameObject.transform.SetAsLastSibling();
			return gameObject;
		}

		public static void DetachAllChildren(this GameObject gameObj)
		{
			for (int i = gameObj.transform.childCount - 1; i >= 0; i--)
			{
				Object.Destroy(gameObj.transform.GetChild(i).gameObject);
			}
			gameObj.transform.DetachChildren();
		}
	}
}
