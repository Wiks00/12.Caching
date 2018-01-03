using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CasheManager;

namespace Fibonacci
{
    public class HashedFibonacci
    {
        private ICache<int> cache;

        public HashedFibonacci(ICache<int> cache)
        {
            cache.Set("1", new List<int>(1) { 1 });
            this.cache = cache;
        }

        public int Fibonacci(int n)
        {
            if (n <= 0)
            {
                return 0;
            }

            var items = cache.Get(n.ToString());

            int item = items != null ? items.First() : 0;

            if (item != 0)
            {
                return item;
            }

            int result = Fibonacci(n - 1) + Fibonacci(n - 2);

            cache.Set(n.ToString(), new List<int>(1) { result });

            return result;
        }
    }
}
