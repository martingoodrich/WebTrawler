using System;
using System.Collections.Generic;
using System.Text;

namespace WebTrawlConsole
{
	public interface ISeleniumWrapper
	{
		string LoadWebSearchHtml(string url);
	}
}
