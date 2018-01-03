using CasheManager;
using NorthwindLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DbCaching
{
    public class DbManager<T> where T : class
    {
        private ICache<T> cache;

        public DbManager(ICache<T> cache)
        {
            this.cache = cache;
        }

        public IEnumerable<T> GetEntities()
        {
            Console.WriteLine($"Get {typeof(T)} entities");

            var user = Thread.CurrentPrincipal.Identity.Name;
            var categories = cache.Get(user);

            if (categories == null)
            {
                Console.WriteLine("From DB");

                using (var dbContext = new Northwind())
                {
                    dbContext.Configuration.LazyLoadingEnabled = false;
                    dbContext.Configuration.ProxyCreationEnabled = false;
                    categories = dbContext.Set<T>().ToList();
                    cache.Set(user, categories);
                }
            }

            return categories;
        }
    }
}
