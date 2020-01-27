using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace WebTrawlConsole
{
	public interface IWebTrawl
	{ 
		Task<List<string>> GetHtmlAsync(string searchString);
	}
}
