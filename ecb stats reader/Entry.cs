using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ecb_stats_reader
{
    /// <summary>
    /// Entry class;
    /// stores data (abbreviation and rate) from XML file of one currency from a single date
    /// </summary>
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
