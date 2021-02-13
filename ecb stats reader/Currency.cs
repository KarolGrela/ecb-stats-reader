using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;


namespace ecb_stats_reader
{
    /// <summary>
    /// List of rates of a given currency
    /// </summary>
    class Currency
    {
        private List<Entry> entries;    // to delete
        private List<double> rates;    // list of rate values
        private List<DateTime> dates;   // lsit of dates
        private string name;            // name of currency
        private string abbreviation;    // abbreviation of currency name
        private DateTime fromDate;      // "from" date
        private DateTime toDate;        // "to" date

        private string earliest_date; // to delete
        /// <summary>
        /// 
        /// </summary>
        /// <param name="n"> name of currency </param>
        /// <param name="a"> three letter Aabbreviation</param>
        /// <param name="to"> data will be gathered up to this date </param>
        /// <param name="from"> data will be gather to this date </param>
        public Currency(string n, string a, DateTime to, DateTime from)
        {
            name = n;
            abbreviation = a;

            // verify if dates are adecuate, move them if neccessary
            DateTime newTo = DateVerification(to);
            DateTime newFrom = DateVerification(from);
            // if newFrom is after newTo
            if (DateTime.Compare(newTo, newFrom) == -1)
            {
                newFrom = newTo;
            }

            // save values of dates
            toDate = newTo;
            fromDate = newFrom;

            // create List<Entry> entries
            CreateEntries(to, from);
        }

        #region Getters and Setter
        /*
         * entries
         */
        // add entry to the end of the list
        public void AddEntry(Entry e)
        {
            entries.Add(e);
        }
        // get amount of entries (length of list)
        public int GetEntriesLength()
        {
            return entries.Count();
        }
        // get entry by index
        public Entry GetEntryIndex(int index)
        {
            // if index is bigger than Count (bigger or equal because indexing from 0)
            // if index < 0 (inadequate value)
            // if Count == 0 (empty list)
            if(index >= entries.Count() || index < 0 || entries.Count() == 0)
            {
                DateTime now = DateTime.MinValue;
                return new Entry(now.ToString(), 0.0);   // return empty entry
            }
            else
            {
                return entries[index];      // return entry
            }
        }
        // get last entry
        public Entry GetLastEntry()
        {
            if(entries.Count() == 0)
            {
                DateTime now = DateTime.MinValue;
                return new Entry(now.ToString(), 0.0);   // return empty entry
            }
            else
            {
                return entries[entries.Count - 1];          // return last entry
            }
        }

        /*
         * Name
         */
        public string Name
        {
            get
            {
                return name;
            }
        }

        /*
         * Abbreviation
         */
        public string Abbreviation
        {
            get
            {
                return Abbreviation;
            }
        }

        /*
         * Dates
         */
        public DateTime GetFromDate()
        {
            return fromDate;
        }
        
        public DateTime GetToDate()
        {
            return toDate;
        }

        #endregion

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
            if(result==-1)
            {
                return _earliest;   // set the date as the earliest possible
            }
            else
            {
                // check other possibilities for inadequate date

                // CHECK IF date IS TODAY OR LATER
                result = DateTime.Compare(date, DateTime.Today);
                // date is after or equal to today
                if(result==1 || result==0)
                {
                    date = DateTime.Today.AddDays(-1);  // set date to day before today
                }

                // CHECK IF date IS ON THE WEEKEND
                if(date.DayOfWeek == DayOfWeek.Saturday)
                {
                    date = DateTime.Today.AddDays(-1);
                }
                else if (date.DayOfWeek == DayOfWeek.Sunday)
                {
                    date = DateTime.Today.AddDays(-2);
                }

                return date;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="to"></param>
        /// <param name="from"></param>
        private void CreateEntries(DateTime to, DateTime from)
        {

            

        }
        
        #endregion


    }
}
