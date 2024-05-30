using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.VisualBasic.CompilerServices;
using StackExchange.Redis;
using MemoryCache = Microsoft.Extensions.Caching.Memory.MemoryCache;

namespace nextech.news.Core.Utils
{
    public class MemoryCacheUtil
    {
        private readonly IDistributedCache _cache;

        public MemoryCacheUtil(IDistributedCache cache)
        {
            _cache = cache;
        }

        public string Get(string key)
        {
            try
            {
                var cacheData = _cache.GetString(key);
                return cacheData ?? default;
            }
            catch (RedisConnectionException ex)
            {
                Console.WriteLine("Error de conexión con Redis: " + ex.Message);
                throw; 
            }
        }

        public void Set(string key, string value, TimeSpan expirationTime)
        {
            var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = expirationTime
            };

            _cache.SetString(key,value,options);
        }

        public void Remove(string key)
        {
            _cache.Remove(key);
        }

        public string[] GetAllKeys()
        {
            var redis = ConnectionMultiplexer.Connect("redis");
            var server = redis.GetServer("redis", 6379);

            return server.Keys().Select(key => key.ToString()).ToArray();
        }
    }
}
