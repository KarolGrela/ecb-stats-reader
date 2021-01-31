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
            XmlNode node = xmlDoc.DocumentElement;
            XmlNode currentCube = node.FirstChild.NextSibling.NextSibling.FirstChild;
            
            

            var from = DateTime.Parse("2021-01-21");
            var to = DateTime.Parse("2021-01-26");
            var indexDate = to;

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

            

            /*
            // date time
            var date1 = DateTime.Parse("2021-01-26");
            var date2 = DateTime.Parse("2021-01-21");
            int result = DateTime.Compare(date1, date2); // result = 1 if date1 is AFTER date2, 0 - the same day, -1 when date1 is BEFORE
            Console.WriteLine(result);
            */



            Console.ReadLine();
        }



    }
}
