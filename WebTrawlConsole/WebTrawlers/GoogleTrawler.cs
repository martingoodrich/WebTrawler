using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace WebTrawlConsole
{
	public class GoogleTrawler : IWebTrawl
	{
		private readonly ISeleniumWrapper _seleniumWrapper;
		private readonly List<string> _textList = new List<string>();

		private static readonly string _googleUrl = "https://www.google.com/search?q=";
		private static readonly string _postAmble = "";

		public GoogleTrawler(ISeleniumWrapper seleniumWrapper)
		{
			_seleniumWrapper = seleniumWrapper;
		}

		public async Task<List<string>> GetHtmlAsync(string searchString)
		{
			if (string.IsNullOrEmpty(searchString) || string.IsNullOrWhiteSpace(searchString))
				return _textList;

			var url = WebUtility.ConstructSearchUrl(_googleUrl, _postAmble, searchString);

			var html = _seleniumWrapper.LoadWebSearchHtml(url);

			var htmlDocument = new HtmlDocument();
			htmlDocument.LoadHtml(html);

			var htmlNodesList = htmlDocument.DocumentNode.Descendants("div")
				.Where(node => node.GetAttributeValue("class", "")
					.Equals("g")).ToList();

			foreach (var htmlNode in htmlNodesList)
			{
				var articleUrl = htmlNode.Descendants("a").FirstOrDefault()?.GetAttributeValue("href","");
				
				var snippet = htmlNode.Descendants("span")
					.Where(node => node.GetAttributeValue("class", "")
						.Equals("st")).ToList()[0].InnerText;

				_textList.Add(articleUrl + Environment.NewLine + snippet + Environment.NewLine);
			}

			return _textList;
		}
	}
}
