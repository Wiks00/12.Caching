using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fibonacci;
using CasheManager;
using Console = System.Console;
using DbCaching;
using NorthwindLibrary;
using System.Threading;

namespace ConsoleUI.Tests
{
    class Program
    {
        static void Main(string[] args)
        {
            var fib = new HashedFibonacci(new RedisCache<int>("localhost", "fibonacci"));

            var sw = new Stopwatch();

            sw.Start();

            Console.WriteLine(fib.Fibonacci(20));

            sw.Stop();
            Console.WriteLine(sw.ElapsedTicks);
            sw.Reset();

            sw.Start();

            Console.WriteLine(fib.Fibonacci(5));
   
            sw.Stop();
            Console.WriteLine(sw.ElapsedTicks);
            sw.Reset();

            var categoryManager = new DbManager<Category>(new MemoryCache<Category>("Category"));

            for (var i = 0; i < 2; i++)
            {
                Console.WriteLine(categoryManager.GetEntities().Count());
                Thread.Sleep(100);
            }

            var employeeManager = new DbManager<Employee>(new MemoryCache<Employee>("Employee"));

            for (var i = 0; i < 2; i++)
            {
                Console.WriteLine(employeeManager.GetEntities().Count());
                Thread.Sleep(100);
            }

            var orderManager = new DbManager<Order>(new MemoryCache<Order>("Order"));

            for (var i = 0; i < 2; i++)
            {
                Console.WriteLine(orderManager.GetEntities().Count());
                Thread.Sleep(100);
            }

            Console.ReadKey();
        }
    }
}
