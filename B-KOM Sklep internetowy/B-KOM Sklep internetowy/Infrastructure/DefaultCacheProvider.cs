using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Caching;

namespace B_KOM_Sklep_internetowy.Infrastructure
{
    public class DefaultCacheProvider : ICacheProvider
    {
        private Cache cache { get { return HttpContext.Current.Cache; } }

        public object Get(string key)
        {
            return cache[key];
        }

        public void Invalidate(string key)
        {
            cache.Remove(key);
        }

        public bool IsSet(string key)
        {
            return (cache[key] != null);
        }

        public void Set(string key, object data, int cacheTimeMin)
        {
            var expirationTime = DateTime.Now + TimeSpan.FromMinutes(cacheTimeMin);
            cache.Insert(key, data, null, expirationTime, Cache.NoSlidingExpiration);
        }
    }
}