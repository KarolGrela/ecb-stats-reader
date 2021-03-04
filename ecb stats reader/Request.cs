using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ecb_stats_reader
{
    /// <summary>
    /// Static class - and interface for communication with Ecb reader library
    /// Inputs: from date, to date, abbreviation(s)
    /// Outputs: instance(s) of Currency class
    /// </summary>
    static class Request
    {
        /// <summary>
        /// Create lists of rates for requested currencies
        /// </summary>
        /// <param name="from"> from date </param>
        /// <param name="to"> to date </param>
        /// <param name="requestedAbbs"> list of abbreviation of requested currencies</param>
        /// <returns></returns>
        static public List<Currency> Make(DateTime from, DateTime to, List<string> requestedAbbs)
        {
            List<Currency> currencies = new List<Currency>();   // create output variable
            // loop through passed variables
            foreach(string abb in requestedAbbs)
            {
                Currency currency = OneCurrency(from, to, abb); // call method for each currency
                currencies.Add(currency);                       // add output to the list
            }

            return currencies;  // return created list
        }

        /// <summary>
        /// Create list of rates for requested currency
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="abb"></param>
        /// <returns></returns>
        static public Currency Make(DateTime from, DateTime to, string abb)
        {
            Currency currency = OneCurrency(from, to, abb); // create output variable and call the method containing all the logic required for gathering data
            return currency;                                // return requested value 
        }

        private static Currency OneCurrency(DateTime from, DateTime to, string abb)
        {
            /*
            * Generate List of names and abbreviations
            */
            CurrencyNameList.Generate();

            /*
             * Generate Range
             */
            Range range = new Range(from, to);

            /*
             * Generate Currency List
             */
            Currency currency = new Currency(abb, range);

            return currency;
        }


    }
}
