using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ecb_stats_reader
{
    /// <summary>
    /// Simple structure containing name and abbreviation of the currency
    /// </summary>
    struct CurrencyName
    {
        public string abbreviation;
        public string name;
    }

    /// <summary>
    /// Class geberating list of CurrencyName
    /// This class utilizes static mrthods and variables
    /// </summary>
    class CurrencyNameList
    {
        private const string path = @"../../../ecb stats reader/currencies.csv";    // path to .csv file
        static private List<CurrencyName> names;                                    // list of CurrencyName instances

        /// <summary>
        /// Static function saving data from .csv file to List
        /// </summary>
        static public void Generate()
        {
            // read .csv file
            // output is saved to array of strings, where each line is a string
            // easch line consist of abbreviation and name of currency separated with a semicolon (;)
            // e.g. CRR;Currency Name
            string[] lines = System.IO.File.ReadAllLines(path);     // read .csv file and save it to array of strings     
            names = new List<CurrencyName>();                       // initialize final List
            foreach (string line in lines)                          // Loop through "lines" array
            {
                string[] words = line.Split(';');                   // split a given line into two strings, split on semicolon; semicolon itself is deleted             
                CurrencyName name = new CurrencyName();             // create a new instance of "CurrencyName" object
                name.abbreviation = words[0];                       // save abbreviation to the new instance of "CurrencyName" object
                name.name = words[1];                               // save name to the new instance of "CurrencyName" object
                names.Add(name);                                    // add this instance of "CurrencyName" to the list
            }
        }

        /// <summary>
        /// Static function returning list of currencies names and its abbreviations
        /// </summary>
        /// <returns> list of CurrencyName instances </returns>
        public static List<CurrencyName> Get()
        {
            return names;
        }
    }
}
