using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;


// https://www.ecb.europa.eu/stats/eurofxref/eurofxref-hist.xml <- link to historical data
// https://www.ecb.europa.eu/stats/eurofxref/eurofxref-daily.xml <- link to daily data

namespace ecb_stats_reader
{
    class Program
    {
        static void Main(string[] args)
        {

            // download xml document
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load("https://www.ecb.europa.eu/stats/eurofxref/eurofxref-hist.xml");

            #region Litter
            /*
            // get first element
            XmlNode node = xmlDoc.DocumentElement;
            Console.WriteLine(node.Name);
            // get node subject
            node = node.FirstChild;
            Console.WriteLine(node.Name);
            // get node sender
            node = node.NextSibling;
            Console.WriteLine(node.Name);
            // get node cube
            node = node.NextSibling;
            Console.WriteLine(node.Name);
            // get node cube /w time
            node = node.FirstChild;
            Console.WriteLine(node.Name);
            XmlAttributeCollection attr = node.Attributes;
            Console.WriteLine(attr[0].Name);
            Console.WriteLine(attr[0].Value);
            var parsedDate = DateTime.Parse(attr[0].Value);
            Console.WriteLine(parsedDate.DayOfWeek);
            */
            /*
            XmlNode node = xmlDoc.DocumentElement;
            XmlNode currentCube = node.FirstChild.NextSibling.NextSibling.FirstChild;
            
            

            var from = DateTime.Parse("2021-01-21");
            var to = DateTime.Parse("2021-01-26");
            var indexDate = to;

            List<Cube> cubes = new List<Cube>();

            XmlAttributeCollection currentAtributeCollection = currentCube.Attributes;
            // do the loop as long as currentCube is after or equal to from
            while (DateTime.Compare(DateTime.Parse(currentAtributeCollection[0].Value), from) != -1)
            {

                // if index and currentCube attribute are equal, we can:
                // 1) save data
                // 2) go 4 ward (both index and current)
                if (DateTime.Compare(indexDate, DateTime.Parse(currentAtributeCollection[0].Value)) == 0)
                {
                    Console.WriteLine("If was true!");
                    cubes.Add(new Cube(currentCube));   // save data to Cube
                    Console.WriteLine("current: " + DateTime.Parse(currentAtributeCollection[0].Value));    // "save" data (dummy code)
                    Console.WriteLine("index: " + indexDate);                               // "save" data (dummy code)

                    currentCube = currentCube.NextSibling;                  // move current
                    if(indexDate.DayOfWeek != DayOfWeek.Monday)
                    {
                        indexDate = indexDate.AddDays(-1);                                  // move index one day backwards
                    }
                    else
                    {
                        indexDate = indexDate.AddDays(-3);                                  // move index (from monday to friday)
                    }
                    
                }
                else
                {
                    // move only current cube
                    Console.WriteLine("If was false!");
                    Console.WriteLine("current: " + currentAtributeCollection[0].Value);    // "save" data (dummy code)
                    Console.WriteLine("index: " + indexDate);                               // "save" data (dummy code)
                    currentCube = currentCube.NextSibling;                  // move current
                }


                

                Console.WriteLine("_______________________");
                currentAtributeCollection = currentCube.Attributes;
            }


            Console.WriteLine("Cubes count: " + cubes.Count());

            foreach (Cube cube in cubes)
            {
                Console.WriteLine("date: " + cube.Date);
                foreach (Entry entry in cube.Entries)
                {
                    Console.WriteLine(entry.Abbreviation + " rate: " + entry.Rate);
                }
                Console.WriteLine("_______________________");
            }
            */

            /*
            // date time
            var date1 = DateTime.Parse("2021-01-26");
            var date2 = DateTime.Parse("2021-01-21");
            int result = DateTime.Compare(date1, date2); // result = 1 if date1 is AFTER date2, 0 - the same day, -1 when date1 is BEFORE
            Console.WriteLine(result);
            */
            /*
            var from = DateTime.Today.AddDays(-2);
            var to = DateTime.Today.AddDays(-1);

            Range request = new Range(from, to);

            Console.WriteLine(request.GetCubesCount);
            */
            /*
            string[] lines = System.IO.File.ReadAllLines(@"../../../ecb stats reader/currencies.csv");
            List<CurrencyName> Names = new List<CurrencyName>();
            foreach (string line in lines)
            {
                string[] words = line.Split(';');

                CurrencyName name = new CurrencyName();
                name.abbreviation = words[0];
                name.name = words[1];
                Names.Add(name);
            }
            */
            #endregion


            /*
             * Generate List of names and abbreviations
             *
            CurrencyNameList.Generate();
            foreach (CurrencyName name in CurrencyNameList.Get())
            {
                Console.WriteLine(name.abbreviation + " is " + name.name);
            }

            /*
             * Generate Range
             *
            Range range = new Range(new DateTime(2021, 2, 13), DateTime.Now.AddDays(3));

            /*
             * Generate Currency List
             *
            Currency dollar = new Currency("USD", range);

            Console.WriteLine(dollar.GetDatesCount() + " " + dollar.GetRatesCount());

            for (int i = 0; i < dollar.GetDatesCount(); i++)
            {
                Console.WriteLine();
                Console.Write("i: " + (i+1) + "   ");
                Console.Write(dollar.GetDateByIndex(i).Date + "  ");
                Console.WriteLine(dollar.GetRateByIndex(i));
                Console.WriteLine();
            }

            *
            DateTime from = new DateTime(2021, 2, 13);
            DateTime to = DateTime.Now.AddDays(3);
            ///(from, to, "USD");
            preRequest(from, to, "CAD");
            preRequest(from, to, "UdD");
            preRequest(from, to, "ZAR");

            Console.ReadLine();
            */

            DateTime from = new DateTime(2019, 2, 13);
            DateTime to = DateTime.Now.AddDays(3);

            List<string> Req = new List<string>();
            Req.Add("USD");
            Req.Add("PLN");
            Req.Add("ZAR");
            List<Currency> c = Request.Make(from, to, Req);



            Console.ReadLine();
        }

        public static void preRequest(DateTime from, DateTime to, string abb)
        {
            /*
             * Generate List of names and abbreviations
             */
            CurrencyNameList.Generate();
            foreach (CurrencyName name in CurrencyNameList.Get())
            {
                //Console.WriteLine(name.abbreviation + " is " + name.name);
            }

            /*
             * Generate Range
             */
            Range range = new Range(from, to);

            /*
             * Generate Currency List
             */
            Currency currency = new Currency(abb, range);

            Console.WriteLine(currency.Name + " (" + currency.Abbreviation + ")");

            for (int i = 0; i < currency.GetDatesCount(); i++)
            {
                Console.Write("i: " + (i + 1) + "   ");
                Console.Write(currency.GetDateByIndex(i).Date + "  ");
                Console.Write(currency.GetRateByIndex(i));
                Console.WriteLine();
            }

        }

    }
}
