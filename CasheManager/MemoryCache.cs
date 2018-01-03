using System;
using System.Collections.Generic;
using System.Runtime.Caching;

namespace CasheManager
{
	public class MemoryCache<T> : ICache<T>
	{
		ObjectCache cache;
		string prefix;

	    public MemoryCache(string prefix)
	    {
	        if(string.IsNullOrEmpty(prefix))
                throw new ArgumentException("Can't be null", nameof(prefix));

	        cache = MemoryCache.Default;
            this.prefix = prefix;
	    }

		public IEnumerable<T> Get(string id)
		{
            return (IEnumerable<T>)cache.Get(prefix + id);
        }

		//public void Set(string id, T item)
		//{
		//	cache.Set(prefix + id, item, ObjectCache.InfiniteAbsoluteExpiration);
		//}

        public void Set(string id, IEnumerable<T> item)
        {
            cache.Set(prefix + id, item, ObjectCache.InfiniteAbsoluteExpiration);
        }
    }
}
