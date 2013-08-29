using System;
using System.Collections.Generic;
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
            
        static void Main(string[] args)
        {
            //timeSpanTest();
            classTest();
            Console.Read();
        }
    }
}
