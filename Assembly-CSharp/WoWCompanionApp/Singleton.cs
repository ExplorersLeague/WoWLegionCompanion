using System;
using UnityEngine;

namespace WoWCompanionApp
{
	public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
	{
		public static T Instance
		{
			get
			{
				if (Singleton<T>.s_instance == null)
				{
					Singleton<T>.s_instance = Object.FindObjectOfType<T>();
					if (Singleton<T>.s_instance == null)
					{
						GameObject gameObject = new GameObject(typeof(T).Name);
						Singleton<T>.s_instance = gameObject.AddComponent<T>();
					}
				}
				return Singleton<T>.s_instance;
			}
		}

		public static T instance
		{
			get
			{
				return Singleton<T>.Instance;
			}
		}

		protected bool IsCloneGettingRemoved { get; private set; }

		protected void Awake()
		{
			if (Singleton<T>.s_instance != null && Singleton<T>.s_instance != this)
			{
				Object.Destroy(base.gameObject);
				this.IsCloneGettingRemoved = true;
			}
			else
			{
				Object.DontDestroyOnLoad(base.gameObject);
				Singleton<T>.s_instance = base.gameObject.GetComponent<T>();
			}
		}

		private static T s_instance;
	}
}
