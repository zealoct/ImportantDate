using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace consoleTest
{
    class Program
    {
        static void timeSpanTest()
        {
            Console.WriteLine("============== Time Span ================");
            DateTime dt = new DateTime(2013, 8, 28);
            TimeSpan days = DateTime.Now - dt;
            Console.WriteLine("days:" + days.TotalDays);
        }

        static void classTest()
        {
            TestClass tc = new TestClass { Date = new DateTime(2013, 1, 13, 23, 17, 37) };
            Console.WriteLine("days:" + Math.Floor(tc.Tspan.TotalDays));
            Console.WriteLine("hours:" + Math.Floor(tc.Tspan.TotalHours));
        }

        static void CollectionTest()
        {
            Collection<Int32> p = new Collection<int>();
            Collection<Int32> n = new Collection<int>();
            p.Add(1);
            p.Add(2);
            p.Add(3);
            n.Add(3);
            n.Add(4);
            IEnumerable<int> remove = p.Except(n);
            IEnumerable<int> add = n.Except(p);
            Console.Write("R:");
            foreach (int i in remove)
            {
                Console.Write(":" + i);
            }
            Console.Write("\nA:");
            foreach (int i in add)
            {
                Console.Write(":" + i);
            }
            Console.WriteLine();
        }
            
        static void Main(string[] args)
        {
            //timeSpanTest();
            //classTest();
            CollectionTest();
            Console.Read();
        }
    }
}
