using System;
using UnityEngine;

namespace WoWCompanionApp
{
	public class NewsPanel : MonoBehaviour
	{
		private void Start()
		{
			this.newsService = base.GetComponent<NewsService>();
		}

		private void Update()
		{
			if (this.newsService != null && this.newsService.FetchTime > this.updatedTime)
			{
				foreach (NewsListItem newsListItem in this.NewsListContent.GetComponentsInChildren<NewsListItem>())
				{
					Object.Destroy(newsListItem.gameObject);
				}
				foreach (NewsArticle newsArticle in this.newsService.NewsArticles)
				{
					NewsListItem newsListItem2 = Object.Instantiate<NewsListItem>(this.NewsListItemPrefab, this.NewsListContent.transform, false);
					newsListItem2.SetNewsArticle(newsArticle);
					newsListItem2.newsPanel = this;
				}
				this.updatedTime = this.newsService.FetchTime;
			}
			if (Input.GetKeyDown(27) && this.ArticleViewPanel.gameObject.activeSelf)
			{
				this.CloseArticleView();
			}
		}

		public void ExpandArticleView(NewsArticle article)
		{
			this.NewsListPanel.SetActive(false);
			this.ArticleViewPanel.gameObject.SetActive(true);
			this.ArticleViewPanel.SetArticle(article);
		}

		public void CloseArticleView()
		{
			this.ArticleViewPanel.gameObject.SetActive(false);
			this.NewsListPanel.SetActive(true);
		}

		public NewsListItem NewsListItemPrefab;

		public GameObject NewsListPanel;

		public GameObject NewsListContent;

		public ArticleView ArticleViewPanel;

		public GameObject ArticleViewContent;

		private NewsService newsService;

		private DateTime updatedTime = DateTime.MinValue;
	}
}
