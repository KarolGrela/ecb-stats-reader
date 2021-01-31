using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ecb_stats_reader
{
    class Entry
    {
        private string abbreviation;
        private double rate;

        public Entry(string a, double r)
        {
            abbreviation = a;
            rate = r;
        }

        public string Abbreviation
        {
            get
            {
                return abbreviation;
            }
            set
            {
                abbreviation = value;
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
