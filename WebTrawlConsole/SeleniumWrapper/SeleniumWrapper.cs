using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.Extensions;

//using OpenQA.Selenium.Firefox;


namespace WebTrawlConsole
{
	public class SeleniumWrapper : ISeleniumWrapper
	{
		public string LoadWebSearchHtml(string url)
		{
			string html;
			FirefoxOptions options = new FirefoxOptions();
			options.AddArguments("--headless");

			using (var driver = new  FirefoxDriver(options))
			{
				driver.Navigate().GoToUrl(url);
				var screenShot = driver.TakeScreenshot();

				html = driver.PageSource;
			}

			return html;
		}
	}
}