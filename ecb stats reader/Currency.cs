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
        // Input Fields
        private string name;            // name of currency
        private string abbreviation;    // abbreviation of currency name
        private Range range;
        private DateTime toDate;        // "to" date (adjusted)
        private DateTime fromDate;      // "from" date (adjusted)
        
        // output fields
        private List<double> rates;     // list of rate values
        private List<DateTime> dates;   // list of dates



        /// <summary>
        /// Inputs: Range, abbreviation of vurrency and other auxiliary data
        /// Outputs: Lists of dates (X axis) and rate values (Y axis)
        /// </summary>
        /// <param name="a">three letter Aabbreviation</param>
        /// <param name="r">range of data</param>
        public Currency(string a, Range r)
        {
            /// Get name of the currency
            CurrencyNameList.Generate();            // create list of names and abbreviation
            var nameList = CurrencyNameList.Get();  // save list to variable
            // Loop through list of names and currencies
            int j = 0;  // index
            foreach(CurrencyName currencyName in nameList)
            {
                j++;    // increment index, in the beginning, because we refer to first position as 1, and to the last one as equal to nameList.Count
                // if passed abbreviation and the abbreviation from the list are matching, save name to field and break the Loop
                if (currencyName.abbreviation == a)
                {
                    name = currencyName.name;   // save name to field
                    break;                      // break the loop
                }
                // if last currency in list has been checkd and it doesn't match with passed abbreviation - it means that passed abbreviation doesn't match with any in the list and name should be set to a dummy value
                if(j==nameList.Count)
                {
                    name = "000_dummy_000";
                }
            }

            // save constructor inputs to fields
            abbreviation = a;
            range = r;
            toDate = range.AdjustedTo;
            fromDate = range.AdjustedFrom;



            // create and return Lists of dates and rates
            (rates, dates) =  CreateEntries(abbreviation, range);
        }

        #region Getters and Setter
        /*
         * dates
         */
        // get amount of dates in the list (length of list)
        public int GetDatesCount()
        {
            return dates.Count();
        }
        // get date by index
        public DateTime GetDateByIndex(int index)
        {
            // if index is bigger than Count (bigger or equal because indexing from 0)
            // if index < 0 (inadequate value)
            // if Count == 0 (empty list)
            if(index >= dates.Count() || index < 0 || dates.Count() == 0)
            {
                return DateTime.MinValue;   // return min value
            }
            else
            {
                return dates[index];      // return date
            }
        }

        /*
         * rates
         */
        // get amount of rates in the list (length of list)
        public int GetRatesCount()
        {
            return rates.Count();
        }
        // get rate by index
        public double GetRateByIndex(int index)
        {
            // if index is bigger than Count (bigger or equal because indexing from 0)
            // if index < 0 (inadequate value)
            // if Count == 0 (empty list)
            if (index >= rates.Count() || index < 0 || rates.Count() == 0)
            {
                return 0.0;   // return min value
            }
            else
            {
                return rates[index];      // return date
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
            get => abbreviation;         
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
        /// Loops through Cubes and entries in order to extract lists of rates and dates
        /// </summary>
        /// <param name="abb"></param>
        /// <param name="r"></param>
        /// <returns>
        /// Item1 - rateList - list of rate values
        /// Item2 - dateList - list of dates values
        /// </returns>
        private (List<double> rateList, List<DateTime> dateList) CreateEntries(string abb, Range r)
        {
            #region Create local variables
            List<double> rts = new List<double>();              // local list of rates - return
            List<DateTime> dts = new List<DateTime>();          // local list of dates - return
            #endregion

            #region Loop through Cubes and Entries in passed Range

            /// Loop through cubes
            for (int i = 0; i < range.GetCubesCount; i++)
            {
                Cube currentCube = range.GetCubeByIndex(i);    // local variable for current cube (shorten lines of code)

                /// Add date to the list
                dts.Add(currentCube.Date);      // add date from current cube

                /// Add rate to the list of rates
                /// Loop through entries                
                for (int j = 0; j < currentCube.GetEntriesCount; j++)
                {
                    Entry currentEntry = currentCube.GetEntryByIndex(j);    // local variable for current entry (shorten lines of code)


                    // if abbreviation of chosen currency has been found
                    if (currentEntry.Abbreviation == abb)
                    {                      
                        rts.Add(currentCube.GetEntryByIndex(j).Rate);       // save rate of chosen currency into list
                        break;                                              // break the loop - it in not necesarry to continue the loop
                    }
                    
                    // if it is last entry in the cube and matching abbreviation hasn't been found
                    // it means the abbreviation isn't mentioned on the list
                    // save a dummy value to the list
                    if(j == currentCube.GetEntriesCount - 1)
                    {
                        rts.Add(0.0);
                    }
                   
                }// end of second for Loop (entries, index = j)

            }// end of first for Loop (cubes, index = i)

            dts.Reverse();
            rts.Reverse();

            #endregion

            #region Return region
            return (rts, dts);
            #endregion
        }

        #endregion


    }
}
