using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebAppAlla.Data;

namespace WebAppAlla.Services
{
    public class CrawlerService
    {
        private Cache _cache;

        public Dictionary<string, int> _result = new();

        public CrawlerService(Cache cache)
        {
            _cache = cache;
        }

        public int MaxDepth { private get; set; }

        public Dictionary<string, int> ProcessUrl(string url, int currentDepth = 0) 
        {
            if (currentDepth < MaxDepth)
            {
                var page = _cache[url];
                if (page == null)
                    page = LoadPage(url);

                if (page != null)
                {
                    ProcessPage(page);

                    _cache.AddPage(page);
                    page.InnerUrls?.ForEach(u => ProcessUrl(u, currentDepth + 1));
                }
            }

            return _result;
        }

        private Page LoadPage(string url) 
        {
            return new Page
            {
                Url = url,
                Text = "dadad ewqwqeqe wqeqwewqe wqeqeq",
                InnerUrls = new() { "aa.com", "bb.com" },
                Matches = new() { { "slonik", 10 } },
                CreationTime = DateTime.Now
            };
        }

        private void ProcessPage(Page page)
        {
            _result["word"] = 10;
        }
    }
}
