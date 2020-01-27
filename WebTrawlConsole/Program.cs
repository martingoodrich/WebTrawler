using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using static System.Console;

//using HtmlAgilityPack;

namespace WebTrawlConsole
{
	public class Program
	{
		private static void Main(string[] args)
		{
			var duckDuckGo = new DuckDuckGoTrawler(new SeleniumWrapper());
			var duckDuckGoItems = duckDuckGo.GetHtmlAsync("Martin Goodrich").GetAwaiter().GetResult();

			Write("Type the return key to see search results from DuckAndGo:");
			ReadLine();
			WriteLine("SearchOption results from DuckDuckGo");
			foreach (var item in duckDuckGoItems)
			{
				WriteLine("--------------------------------------------------------------------------------");
				WriteLine(item);
				WriteLine();
			}

			var googleTrawler = new GoogleTrawler(new SeleniumWrapper());
			var GoogleItems = googleTrawler.GetHtmlAsync("Martin Goodrich").GetAwaiter().GetResult();

			Write("Type the return key to see search results from DuckAndGo:");
			ReadLine();
			WriteLine("Search Option results from Google");

			foreach (var item in GoogleItems)
			{
				WriteLine("--------------------------------------------------------------------------------");
				WriteLine(item);
				WriteLine();
			}

			Write("Type the return key to finish:");

			ReadLine();
		}
	}
}