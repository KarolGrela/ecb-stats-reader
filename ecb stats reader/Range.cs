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
    /// Also contains and manages to- and from- dates
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
        
        public Cube GetCubeNo(int i)
        {
            return cubes[i];            
        }

        public int GetCubesCount
        {
            get
            {
                return cubes.Count;
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

        public Range(DateTime f, DateTime t)
        {
            // save dates
            to = t;
            from = f;
            // adjust dates
            adjustedTo = DateVerification(to, true);
            adjustedFrom = DateVerification(from, false);
            // initialize list
            cubes = new List<Cube>();
            // read from XML
            // create and initialize XML Document variable
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlPath);
            // initialize node variable
            node = xmlDoc.DocumentElement;
            // set currentCube as <Cube time="..."> node
            XmlNode currentCube = node.FirstChild.NextSibling.NextSibling.FirstChild;
            // get attributes
            XmlAttributeCollection attr = currentCube.Attributes;
            // find "to" date in XML
            // while date of current cube date is later than adjustedTo
            // if stops if current cube has date equal to adjustedTo
            while(DateTime.Compare(DateTime.Parse(attr[0].Value), adjustedTo)  == 1 )
            {
                // go to next sibling
                currentCube = currentCube.NextSibling;
                attr = currentCube.Attributes;
            }

            // go through file and save following cubes untill finding from date cube (exactly - after finding first earlier date than adjustedFrom)
            while (DateTime.Compare(DateTime.Parse(attr[0].Value), AdjustedFrom) != -1)
            {
                // save data to cube
                cubes.Add(new Cube(currentCube));
                // go to next sibling
                currentCube = currentCube.NextSibling;
                attr = currentCube.Attributes;
            }
        }


        #region Private Methods

        /// <summary>
        /// Verifies if date is:
        ///     latter (or equal to) earliest date (if not will be moved to first appropriate date) - DONE
        ///     before today (if not will be moved to first appropriate date)
        ///     at the weekend ("from" date will be moved to next monday (provided it is not in the future), "to" date will be moved to last friday)
        /// Direction of movement of dates (on days weekend) depends to value of char "mode"
        /// </summary>
        /// <param name="date"> verified date </param>
        /// <param name="fromDate"> true if passed date is "from" date, false if the date is a "to" date </param>
        /// <returns></returns>
        private DateTime DateVerification(DateTime date, bool fromDate)
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
                    // "from" date is moved backward, "to" date is moved forward
                    if(fromDate)
                    {
                        date = date.AddDays(-1);  // set date to last friday
                    }
                    else
                    // "to" date - is moved forward, unless moving forward means reading future data
                    {
                        // if next monday IS NOT today or IS NOT in the future (new date is before today)
                        if (DateTime.Compare(date.AddDays(1), DateTime.Today) == -1)
                        {
                            date = date.AddDays(2);  // set date to next (following) monday
                        }
                        else
                        {
                            date = date.AddDays(-1);  // set date to last firday
                        }
                    }
                }
                else if (date.DayOfWeek == DayOfWeek.Sunday)
                {
                    // "from" date is moved backward, "to" date is moved forward
                    if (fromDate)   // "from" date
                    {
                        date = date.AddDays(-2);  // set date to last firday
                    }
                    else
                    // "to" date - is moved forward, unless moving forward means reading future data
                    {
                        // if next monday IS NOT today or IS NOT in the future (new date is before today)
                        if (DateTime.Compare(date.AddDays(1), DateTime.Today)== -1)
                        {
                            date = date.AddDays(1);  // set date to next (following) monday
                        }                           
                        else
                        {
                            date = date.AddDays(-2);  // set date to last firday
                        }
                    }
                }

                return date;
            }
        }

        #endregion

    }
}
