using System;
using UnityEngine;

namespace WoWCompanionApp
{
	public class DebugNewsService : NewsService
	{
		private void Start()
		{
			base.NewsArticles.Add(new NewsArticle("Article 1", this.lipsum1, Texture2D.blackTexture, DateTime.Now));
			base.NewsArticles.Add(new NewsArticle("Article 2", "Text for article 2", Texture2D.whiteTexture, DateTime.Now));
			base.NewsArticles.Add(new NewsArticle("Article 3", "Text for article 3", Texture2D.blackTexture, DateTime.Now));
			base.NewsArticles.Add(new NewsArticle("Article 4", "Text for article 4", Texture2D.blackTexture, DateTime.Now));
			base.NewsArticles.Add(new NewsArticle("Article 5", "Text for article 5", Texture2D.blackTexture, DateTime.Now));
			base.FetchTime = DateTime.Now;
		}

		private void Update()
		{
		}

		private readonly string lipsum1 = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Morbi lacinia aliquam dictum. Sed id odio at libero efficitur dignissim. Suspendisse condimentum mi bibendum, aliquam turpis vel, luctus mi. Vestibulum elementum, ex pulvinar interdum porttitor, eros tellus egestas velit, at venenatis augue arcu nec metus. Pellentesque dictum venenatis leo, eu sodales dui tempor nec. Etiam a mollis arcu. Maecenas cursus diam vel augue posuere, euismod imperdiet ligula convallis. Ut in mauris sit amet augue fermentum tincidunt. Duis sollicitudin ante ac tincidunt posuere.\r\n\r\nSuspendisse eleifend vestibulum dolor, ut aliquet nisi elementum vitae.Sed fringilla libero at mollis dapibus.Cras tincidunt magna efficitur cursus vulputate.Proin quis fringilla enim.In egestas nisi et imperdiet consectetur.Nam porta, erat vitae posuere egestas, odio sapien lacinia dolor, in scelerisque mi est a risus.Vivamus nec pellentesque dui, at aliquet dui.\r\n\r\nSed quis consectetur dui, quis ullamcorper erat.Proin eget magna turpis.Morbi ullamcorper aliquam erat.In hac habitasse platea dictumst.Proin nec leo dapibus, viverra sapien eget, bibendum augue.Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Maecenas vitae sapien non metus auctor suscipit et non felis. Nullam arcu eros, interdum a iaculis at, dictum id odio. Etiam aliquet tortor a diam scelerisque viverra.Proin libero elit, sagittis vitae consectetur id, pulvinar nec massa. Quisque condimentum, mauris et lobortis rutrum, arcu erat feugiat diam, non facilisis nisl justo vel metus.In sit amet interdum massa.\r\n\r\nUt ultrices lacinia erat, vitae pellentesque est condimentum ut. Aliquam a pulvinar metus, sed porta nisl. Suspendisse vulputate mauris ac auctor ornare. Aliquam vel massa sed eros pretium aliquet quis et elit. Aliquam pellentesque, diam ut feugiat placerat, enim tortor dapibus risus, lacinia convallis lectus elit sed orci.Phasellus turpis nibh, efficitur eu sem vitae, bibendum faucibus nulla. Duis euismod molestie erat, nec pretium sem viverra vitae. Quisque eget tellus vitae lacus sagittis varius.Praesent magna neque, placerat eget feugiat ut, semper at massa. Sed non quam feugiat, laoreet erat ut, sagittis orci.\r\n\r\nUt nec lorem ac felis volutpat pharetra sed eget purus. In efficitur ipsum est, ac sodales nulla bibendum tincidunt. Suspendisse ultricies dolor a risus mollis malesuada.Vestibulum pulvinar magna id urna varius varius.Praesent fringilla odio nec odio fringilla sodales.Phasellus nec odio purus. Nullam congue sit amet massa in congue.Vestibulum porta, leo semper suscipit dignissim, lacus ex suscipit purus, id pulvinar odio augue sit amet arcu. Cras mattis mauris ut lorem vehicula, vitae euismod ex fermentum.Mauris fringilla ex in lorem pretium, sed faucibus eros ultricies.";
	}
}
