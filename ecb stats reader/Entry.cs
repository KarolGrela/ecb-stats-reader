using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ecb_stats_reader
{
    class Entry
    {
        private DateTime date;
        private double rate;

        public Entry(DateTime d, double r)
        {
            date = d;
            rate = r;
        }

        public DateTime Date
        {
            get
            {
                return date;
            }
            set
            {
                date = value;
            }
        }

        public double Rate
        {
            get
            {
                return rate;
            }
            set
            {
                rate = value;
            }
        }
    }
}
