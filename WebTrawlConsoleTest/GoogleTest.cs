using System;
using System.Threading.Tasks;
using NUnit.Framework;
using WebTrawlConsole;

namespace WebTrawlConsoleTest
{
	internal class GoogleTests
	{
		[Test]
		public async Task GoogleTrawlerReturnsValidResultGivenAGoodSearchString()
		{
			// Given
			var googleTrawler = new GoogleTrawler(new SeleniumTestClass());
			var expected =
				@"https://mgw.us.com/" + Environment.NewLine + "Martin Goodrich Real Top Chap(RTC)." +
				Environment.NewLine;

			// When
			var actual = await googleTrawler.GetHtmlAsync("Martin Goodrich");

			// Then
			Assert.AreEqual(expected, actual[0]);
		}

		[Test]
		public async Task GoogleTrawlerReturnsAnEmptyArrayIfANullSearchStringIsGiven()
		{
			// Given
			var googleTrawler = new GoogleTrawler(new SeleniumTestClass());

			// When
			var actual = await googleTrawler.GetHtmlAsync(null);

			// Then
			Assert.IsTrue(actual.Count == 0);
		}

		[Test]
		public async Task GoogleTrawlerReturnsAnEmptyArrayIfABlankStringContainingOnlyWhitespaceIsGiven()
		{
			// Given
			var duckDuckGoTrawler = new GoogleTrawler(new SeleniumTestClass());

			// When
			var actual = await duckDuckGoTrawler.GetHtmlAsync("   ");

			// Then
			Assert.IsTrue(actual.Count == 0);
		}

		// TODO This should ideally be mocked by an IoC container or by using something like Moque
		// TODO The class mocks what the production class would do.
		private class SeleniumTestClass : ISeleniumWrapper
		{
			// TODO This looks awful and might benefit from being put in to an external file and read in to the test
			// Essentially the following snippet of HTML has been taken from a genuine search with some text replaced for simplicity of testing.
			// I wonder if you can spot which text?
			private static readonly string TestHtml = @"div class='srg'>
<div class='g' data-hveid='CAQQAA' data-ved='2ahUKEwi95O7FxqTnAhXBiFwKHXNyAlUQyDgoAHoECAQQAA'>
    <!--m--><link href='https://mgw.us.com/' rel='prerender'>
    <div data-hveid='CAQQAQ' data-ved='2ahUKEwi95O7FxqTnAhXBiFwKHXNyAlUQFSgAMAB6BAgEEAE'>
        <div class='rc'>
            <div class='r'>
                <a href='https://mgw.us.com/' ping='/url?sa=t&amp;source=web&amp;rct=j&amp;url=https://mgw.us.com/&amp;ved=2ahUKEwi95O7FxqTnAhXBiFwKHXNyAlUQFjAAegQIBBAC'>
                    <div class='TbwUpd'>
                        <cite class='iUh30'>mgw.us.com</cite>
                    </div><br>
                    <h3 class='LC20lb'>Martin, Goodrich &amp; Waddell, Inc. Land Investment</h3>
                </a>
                <div class='B6fmyf'>
                    <div class='qks8td TbwUpd'>
                        <cite class='iUh30'>mgw.us.com</cite>
                    </div>
                    <div class='yWc32e'>
                        <span>
                            <div class='action-menu ab_ctl'>
                                <a class='GHDvEf ab_button' href='#' id='am-b0' aria-label='Result options' aria-expanded='false' aria-haspopup='true' role='button' jsaction='m.tdd;keydown:m.hbke;keypress:m.mskpe' data-ved='2ahUKEwi95O7FxqTnAhXBiFwKHXNyAlUQ7B0wAHoECAQQAw'>
                                    <span class='mn-dwn-arw'></span>
                                </a>
                                <div class='action-menu-panel ab_dropdown' role='menu' tabindex='-1' jsaction='keydown:m.hdke;mouseover:m.hdhne;mouseout:m.hdhue' data-ved='2ahUKEwi95O7FxqTnAhXBiFwKHXNyAlUQqR8wAHoECAQQBA'>
                                    <ol>
                                        <li class='action-menu-item ab_dropdownitem' role='menuitem'>
                                            <a class='fl' href='https://webcache.googleusercontent.com/search?q=cache:N6380oeilL8J:https://mgw.us.com/+&amp;cd=1&amp;hl=en&amp;ct=clnk&amp;gl=uk' ping='/url?sa=t&amp;source=web&amp;rct=j&amp;url=https://webcache.googleusercontent.com/search%3Fq%3Dcache:N6380oeilL8J:https://mgw.us.com/%2B%26cd%3D1%26hl%3Den%26ct%3Dclnk%26gl%3Duk&amp;ved=2ahUKEwi95O7FxqTnAhXBiFwKHXNyAlUQIDAAegQIBBAF'>Cached</a>
                                        </li>
                                    </ol>
                                </div>
                            </div>
                        </span>
                    </div>
                </div>
            </div>
            <div class='s'>
                <div>
                    <span class='st'><b>Martin</b> <b>Goodrich</b> Real Top Chap(RTC).</span>
                    <div class='osl'>‎<a class='fl' href='https://mgw.us.com/real-estate/' ping='/url?sa=t&amp;source=web&amp;rct=j&amp;url=https://mgw.us.com/real-estate/&amp;ved=2ahUKEwi95O7FxqTnAhXBiFwKHXNyAlUQ0gIoADAAegQIBBAH'>Real Estate</a> ·&nbsp;‎<a class='fl' href='https://mgw.us.com/contact/' ping='/url?sa=t&amp;source=web&amp;rct=j&amp;url=https://mgw.us.com/contact/&amp;ved=2ahUKEwi95O7FxqTnAhXBiFwKHXNyAlUQ0gIoATAAegQIBBAI'>Contact</a> ·&nbsp;‎<a class='fl' href='https://mgw.us.com/appraisal-services/' ping='/url?sa=t&amp;source=web&amp;rct=j&amp;url=https://mgw.us.com/appraisal-services/&amp;ved=2ahUKEwi95O7FxqTnAhXBiFwKHXNyAlUQ0gIoAjAAegQIBBAJ'>Appraisal Services</a> ·&nbsp;‎<a class='fl' href='https://mgw.us.com/seasons-newsletter/' ping='/url?sa=t&amp;source=web&amp;rct=j&amp;url=https://mgw.us.com/seasons-newsletter/&amp;ved=2ahUKEwi95O7FxqTnAhXBiFwKHXNyAlUQ0gIoAzAAegQIBBAK'>Newsletter</a></div>
                </div>
            </div>
        </div>
    </div><!--n-->
</div>";

			public string LoadWebSearchHtml(string url)
			{
				return TestHtml;
			}
		}
	}
}