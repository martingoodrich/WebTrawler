using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace WebTrawlConsole
{
	public class DuckDuckGoTrawler : IWebTrawl
	{
		private readonly ISeleniumWrapper _seleniumWrapper;
		private readonly List<string> _textList = new List<string>();

		private static readonly string _duckDuckGoUrl = "https://duckduckgo.com/?q=";
		private static readonly string _postAmble = "&atb=v205-3zk&iar=news&ia=web";

		public DuckDuckGoTrawler(ISeleniumWrapper seleniumWrapper)
		{
			_seleniumWrapper = seleniumWrapper;
		}

		public async Task<List<string>> GetHtmlAsync(string searchString)
		{
			if (string.IsNullOrEmpty(searchString) || string.IsNullOrWhiteSpace(searchString))
				return _textList;

			var url = WebUtility.ConstructSearchUrl(_duckDuckGoUrl, _postAmble, searchString);

			var html = _seleniumWrapper.LoadWebSearchHtml(url);

			var htmlDocument = new HtmlDocument();
			htmlDocument.LoadHtml(html);

			var newsItemsList = RetrieveNewsItems(htmlDocument);
			BuildNewsItems(newsItemsList);

			return _textList;
		}


		private void BuildNewsItems(IEnumerable<HtmlNode> newsItems)
		{
			foreach (var newsItem in newsItems)
			{
				var articleUrl = ExtractArticleUrlFromHtml(newsItem);

				var snippet = ExtractNewsSnippetFromHtml(newsItem);

				_textList.Add(articleUrl + Environment.NewLine + snippet + Environment.NewLine);
			}
		}

		private static string ExtractNewsSnippetFromHtml(HtmlNode newsItem)
		{
			var snippetNode = newsItem.Descendants("div")
				.Where(node => node.GetAttributeValue("class", "")
					.Equals("result__snippet js-result-snippet")).ToList();

			// Get the news snippet
			var snippet = snippetNode[0].InnerHtml;
			return snippet;
		}

		private static string ExtractArticleUrlFromHtml(HtmlNode newsItem)
		{
			var dataHostname = newsItem.Descendants("a")
				.Where(node => node.GetAttributeValue("class", "")
					.Equals("result__a")).ToList();
			var hostName = dataHostname[0].GetAttributeValue("href", "");
			return hostName;
		}

		private static List<HtmlNode> RetrieveNewsItems(HtmlDocument htmlDocument)
		{
			var divResultsMain = htmlDocument.DocumentNode.Descendants("div")
				.Where(node => node.GetAttributeValue("id", "")
					.Equals("links")).ToList();

			var newsItems = divResultsMain[0].Descendants("div")
				.Where(node => node.GetAttributeValue("id", "")
					.Contains("r1-")).ToList();
			return newsItems;
		}
	}
}