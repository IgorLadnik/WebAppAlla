using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAppAlla.Services;

namespace WebAppAlla.Controllers
{
    //[ApiController]
    //[Route("[controller]")]
    public class CrawlerController : Controller
    {
        private CrawlerService _crawlerService;

        public CrawlerController(CrawlerService crawlerService)
        {
            _crawlerService = crawlerService;
        }

        [HttpGet("/{url}/{matchWords}/{maxDepth}")]
        public string ExtractInfoFromWebsite(string url, string matchWords, int maxDepth)
        {
            _crawlerService.MaxDepth = maxDepth;
            StringBuilder sb = new();
            var dct = _crawlerService.ProcessUrl(url);
            foreach (var word in dct.Keys)
                sb.Append($"{word}: {dct[word]}");

            return sb.ToString();
        }
    }
}
