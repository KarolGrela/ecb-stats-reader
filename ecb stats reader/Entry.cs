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
        private string abbreviation;    // three-letter abbreviation
        private double rate;            // value of rate
   
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="a"> passed abbreviation </param>
        /// <param name="r"> passed value of rate </param>
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
