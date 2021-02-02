using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ecb_stats_reader
{
    /// <summary>
    /// List of cubes taken from adjusted ranges of time
    /// </summary>
    class Range
    {
        // constant parameters
        private const string earliest_date = "1999-01-04";  // earliest possible date
        private const string xmlPath = "https://www.ecb.europa.eu/stats/eurofxref/eurofxref-hist.xml";  
        // list of cubes (date + entry) in range
        private List<Cube> cubes;
        //
        private XmlNode node;
        // range of dates requested by user (input)
        private DateTime to;        
        private DateTime from;
        // range of dates after ruling out weekends and dates exceeding thresholds
        private DateTime adjustedTo;
        private DateTime adjustedFrom;

        #region Getters and setters
        
        public List<Cube> Cubes
        {
            get
            {
                return cubes;
            }
        }

        public DateTime To
        {
            get
            {
                return to;
            }
        }

        public DateTime From
        {
            get
            {
                return from;
            }
        }

        public DateTime AdjustedTo
        {
            get
            {
                return adjustedTo;
            }
        }

        public DateTime AdjustedFrom
        {
            get
            {
                return adjustedFrom;
            }
        }

        #endregion

        public Range(DateTime t, DateTime f)
        {
            // save dates
            to = t;
            from = f;
            // adjust dates
            adjustedTo = DateVerification(to);
            adjustedFrom = DateVerification(from);
            // initialize list
            cubes = new List<Cube>();
            // read from XML
            // find first cube
            XmlNode currentCube = node.FirstChild.NextSibling.NextSibling.FirstChild;
            // get attributes
            XmlAttributeCollection attr = currentCube.Attributes;
            // find "to" date
            while()

            // date 
        }


        #region Private Methods

        /// <summary>
        /// Verifies if date is:
        ///     latter (or equal to) earliest date (if not will be moved to first appropriate date) - DONE
        ///     before today (if not will be moved to first appropriate date)
        ///     at the weekend (if not will be moved to first appropriate date)
        /// Direction of movement of dates (on days weekend) depends to value of char "mode"
        /// </summary>
        /// <param name="date"> verified date </param>
        /// <returns></returns>
        private DateTime DateVerification(DateTime date)
        {
            // CHECK IF date IS BEFORE EARLIEST POSSIBLE DATE
            DateTime _earliest = DateTime.Parse(earliest_date);     // create DateTime with earliest possible date
            int result = DateTime.Compare(date, _earliest);         // compare passed date with the earliest possible date
            // date is before the _earliest
            if (result == -1)
            {
                return _earliest;   // set the date as the earliest possible
            }
            else
            {
                // check other possibilities for inadequate date

                // CHECK IF date IS TODAY OR LATER
                result = DateTime.Compare(date, DateTime.Today);
                // date is after or equal to today
                if (result == 1 || result == 0)
                {
                    date = DateTime.Today.AddDays(-1);  // set date to day before today
                }

                // CHECK IF date IS ON THE WEEKEND
                if (date.DayOfWeek == DayOfWeek.Saturday)
                {
                    date = DateTime.Today.AddDays(-1);  // set date to firday
                }
                else if (date.DayOfWeek == DayOfWeek.Sunday)
                {
                    date = DateTime.Today.AddDays(-2);  // set date to firday
                }

                return date;
            }
        }

        #endregion

    }
}
