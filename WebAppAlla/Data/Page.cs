using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppAlla.Data
{
    public class Page
    {
        public string Url { get; set; }
        public string Text { get; set; }
        public List<string> InnerUrls { get; set; }
        public Dictionary<string, int> Matches { get; set; }
        public DateTime CreationTime { get; set; }
    }
}
