using System;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.PhantomJS;
using WebTrawlConsole;

namespace WebTrawlConsoleTest
{
	internal class DuckDuckGoTests
	{
		[Test]
		public void myTest()
		{
			IWebDriver driver = new PhantomJSDriver();
		}

		[Test]
		public async Task DuckAndGoTrawlerReturnsValidResultGivenAGoodSerachString()
		{
			// Given
			var duckDuckGoTrawler = new DuckDuckGoTrawler(new SeleniumTestClass());
			var expected =
				@"https://mgw.us.com/" + Environment.NewLine + "<b>Martin</b>, <b>Goodrich</b> Real Top Chap(RTP)" +
				Environment.NewLine;

			// When
			var actual = await duckDuckGoTrawler.GetHtmlAsync("Martin Goodrich");

			// Then
			Assert.AreEqual(expected, actual[0]);
		}

		[Test]
		public async Task DuckAndGoTrawlerReturnsAnEmptyArrayIfANullSearchStringIsGiven()
		{
			// Given
			var duckDuckGoTrawler = new DuckDuckGoTrawler(new SeleniumTestClass());

			// When
			var actual = await duckDuckGoTrawler.GetHtmlAsync(null);

			// Then
			Assert.IsTrue(actual.Count == 0);
		}

		[Test]
		public async Task DuckAndGoTrawlerReturnsAnEmptyArrayIfABlankStringContainingOnlyWhitespaceIsGiven()
		{
			// Given
			var duckDuckGoTrawler = new DuckDuckGoTrawler(new SeleniumTestClass());

			// When
			var actual = await duckDuckGoTrawler.GetHtmlAsync("   ");

			// Then
			Assert.IsTrue(actual.Count == 0);
		}

		// TODO this should ideally be mocked by an IoC container or by using something like Moque 
		private class SeleniumTestClass : ISeleniumWrapper
		{
			// TODO This looks awful and might benefit from being put in to an external file and read in to the test

			private static readonly string TestHtml = @"<div id='links' class='results js-results'>
<div id='r1-0' class='result results_links_deep highlight_d result--url-above-snippet' data-domain='mgw.us.com' data-hostname='mgw.us.com' data-nir='1'>
    <div class='result__body links_main links_deep'>
        <h2 class='result__title'>
            <a class='result__a' rel='noopener' href='https://mgw.us.com/'><b>Martin</b>, <b>Goodrich</b> &amp; Waddell, Inc. Land Investment</a>
            <a rel='noopener' class='result__check' href='https://mgw.us.com/'>
                <span class='result__check__tt'>Your browser indicates if you've visited this link</span>
            </a>
        </h2>
        <div class='result__extras js-result-extras'>
            <div class='result__extras__url'>
                <span class='result__icon '>
                    <a href='/?q=martin%20goodrich+site:mgw.us.com&amp;atb =v205-3zk' title='Search domain mgw.us.com' class='js-result-extras-site_search'>
                        <img data-src='//external-content.duckduckgo.com/ip3/mgw.us.com.ico' title='Search domain mgw.us.com' class='result__icon__img js-lazyload-icons' src='//external-content.duckduckgo.com/ip3/mgw.us.com.ico' width='16' height='16'>
                    </a>
                </span>
                <a href='https://mgw.us.com/' rel='noopener' class='result__url js-result-extras-url'>
                    <span class='result__url__domain'>https://mgw.us.com</span><span class='result__url__full'></span>
                </a>
            </div>
        </div>
        <div class='result__snippet js-result-snippet'><b>Martin</b>, <b>Goodrich</b> Real Top Chap(RTP)</div>
    </div>
</div>
<div id='organic-module'></div>";

			public string LoadWebSearchHtml(string url)
			{
				return TestHtml;
			}
		}
	}
}