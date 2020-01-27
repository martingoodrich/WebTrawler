using System;
using System.Text.RegularExpressions;

namespace WebTrawlConsole
{
	public static class WebUtility
	{
		public static string ConvertToUrlSearchString(string searchString)
		{
			if (string.IsNullOrEmpty(searchString))
				return String.Empty;

			searchString = searchString.Trim();

			return Regex.Replace(searchString, @"\s+", "+");
		}
		public static string ConstructSearchUrl(string baseUrl, string postAmble, string searchString)
		{
			var url = baseUrl;

			url += WebUtility.ConvertToUrlSearchString(searchString) + postAmble;
			return url;
		}

	}
}
