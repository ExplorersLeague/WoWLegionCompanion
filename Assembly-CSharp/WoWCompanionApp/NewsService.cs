using System;
using System.Collections.Generic;
using UnityEngine;

namespace WoWCompanionApp
{
	public abstract class NewsService : MonoBehaviour
	{
		public DateTime FetchTime { get; protected set; }

		public List<NewsArticle> NewsArticles
		{
			get
			{
				return this.m_NewsArticles;
			}
		}

		private void Start()
		{
		}

		private void Update()
		{
		}

		private List<NewsArticle> m_NewsArticles = new List<NewsArticle>();
	}
}
