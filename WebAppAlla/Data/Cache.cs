using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace WebAppAlla.Data
{
    public class Cache
    {
        private Dictionary<string, Page> Pages { get; set; } = new();
        private Timer _timer;
        private object _locker = new();
        private TimeSpan _expitationTimeSpan;

        public Cache(int pageCacheExpirationInMin)
        {
            _expitationTimeSpan = TimeSpan.FromMinutes(pageCacheExpirationInMin);
            _timer = new Timer(state => PurgePages(), null, TimeSpan.Zero, _expitationTimeSpan);
        }

        public void AddPage(Page page)
        {
            lock (_locker)
            {
                Pages[page.Url] = page;
            }
        }

        public Page this[string url]
        {
            get 
            {
                lock (_locker)
                {
                    Pages.TryGetValue(url, out Page page);
                    return page;
                } 
            }
        }

        private void PurgePages()
        {
            lock (_locker)
            {
                foreach (var key in Pages.Keys.ToArray())
                    if (Pages[key].CreationTime + _expitationTimeSpan > DateTime.Now)
                        Pages.Remove(key);
            }
        }

    }
}
