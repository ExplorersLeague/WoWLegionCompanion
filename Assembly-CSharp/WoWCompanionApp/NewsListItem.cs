using System;
using UnityEngine;
using UnityEngine.UI;

namespace WoWCompanionApp
{
	public class NewsListItem : MonoBehaviour
	{
		private void Start()
		{
		}

		private void Update()
		{
		}

		public void SetNewsArticle(NewsArticle article)
		{
			this.article = article;
			this.title.text = article.Title;
			this.body.text = article.Body;
			this.image.texture = article.Image;
		}

		public void ShowArticle()
		{
			this.newsPanel.ExpandArticleView(this.article);
		}

		private NewsArticle article;

		public Text title;

		public Text body;

		public RawImage image;

		[HideInInspector]
		public NewsPanel newsPanel;
	}
}
