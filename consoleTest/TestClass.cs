using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace consoleTest
{
    class TestClass
    {
        private DateTime _date;
        public DateTime Date
        {
            get { return _date; }
            set
            {
                _date = value;
                _tspan = DateTime.Now - _date;
            }
        }

        private TimeSpan _tspan;
        public TimeSpan Tspan { get { return _tspan; } }
    }
}
