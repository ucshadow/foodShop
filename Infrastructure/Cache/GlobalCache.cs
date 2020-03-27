using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace FoodStore.Infrastructure.Cache
{
    public class GlobalCache
    {
        private static GlobalCache _self;
        private static Dictionary<string, List<ICacheable>> _cache;
        private static Dictionary<string, List<string>> _cacheMetaData;
        public static int MaxItemsPerCacheList = 10;

        private GlobalCache() { }

        public static GlobalCache GetCache()
        {
            if(_self == null)
            {
                _self = new GlobalCache();
                _cache = new Dictionary<string, List<ICacheable>>();
                _cacheMetaData = new Dictionary<string, List<string>>();
                return _self;
            }
            return _self;
        }

        public ICacheable GetCachedItem<T>(int uniqueIdentifier)
        {
            if (!_cache.ContainsKey(typeof(T).Name)) return null;
            var itemArray = _cache[typeof(T).Name];
            foreach(var item in itemArray)
            {
                if((int) item.GetUniqueIdentifier<int>() == uniqueIdentifier)
                {
                    return item;
                }
            }
            return null;
        }

        public ICacheable GetCachedItem<T>(string uniqueIdentifier)
        {
            if (!_cache.ContainsKey(typeof(T).Name)) return null;
            var itemArray = _cache[typeof(T).Name];
            foreach (var item in itemArray)
            {
                if ((string)item.GetUniqueIdentifier<string>() == uniqueIdentifier)
                {
                    return item;
                }
            }
            return null;
        }

        public List<T> GetCachedItems<T>()
        {
            Debug.WriteLine(typeof(T).Name);
            if (!_cache.ContainsKey(typeof(T).Name)) return null;
            var res = new List<T>();
            foreach(var i in _cache[typeof(T).Name])
            {
                res.Add((T)i);
            }
            return res;
        }

        public ICacheable CacheItem<T>(ICacheable item)
        {
            if(_cache.Count >= MaxItemsPerCacheList)
            {
                RemoveOldest(typeof(T).Name);
            }

            if (!_cache.ContainsKey(typeof(T).Name))
            {
                _cache.Add(typeof(T).Name, new List<ICacheable> { item });
                _cacheMetaData.Add(typeof(T).Name, new List<string>() { (string)item.GetUniqueIdentifier<string>() });
                return item;
            }

            _cache[typeof(T).Name].Add(item);
            _cacheMetaData[typeof(T).Name].Add((string) item.GetUniqueIdentifier<string>());
            return item;
        }

        private void RemoveOldest(string key)
        {
            _cache[key].RemoveAt(0);
            _cacheMetaData[key].RemoveAt(0);
        }

        public int CacheCount(string key)
        {
            if (!_cache.ContainsKey(key)) return 0;
            return _cache[key].Count();
        }

        public int GetMaxNumberOfItemsPerCache()
        {
            return MaxItemsPerCacheList;
        }

        public bool ClearCache<T>()
        {
            if (!_cache.ContainsKey(typeof(T).Name)) return false;
            _cache[typeof(T).Name].Clear();
            return true;
        }

        public bool ClearCachedItem<T>(int uniqueIdentifier)
        {
            if (!_cache.ContainsKey(typeof(T).Name)) return false;
            foreach (var t in _cache[typeof(T).Name]) 
            { 
                if((int)t.GetUniqueIdentifier<int>() == uniqueIdentifier)
                {
                    _cache[typeof(T).Name].Remove(t);
                    return true;
                }
            }
            return false;
        }

        public bool ClearCache(string key)
        {
            if (!_cache.ContainsKey(key)) return false;
            _cache[key].Clear();
            return true;
        }
    }
}