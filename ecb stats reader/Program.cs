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
            XmlNode secondCube = node.FirstChild.NextSibling.NextSibling.FirstChild;
            
            

            var from = DateTime.Parse("2021-01-21");
            var to = DateTime.Parse("2021-01-26");
            var index_date = to;
            do
            {
                XmlAttributeCollection attr = secondCube.Attributes;
                if(index_date == DateTime.Parse(attr[0].Value))
                {
                    Console.WriteLine(attr[0].Value);
                    secondCube = secondCube.NextSibling;

                }
                Console.WriteLine(DateTime.Parse(attr[0].Value));
                Console.WriteLine(index_date);
                Console.WriteLine("_______________________");

                
                index_date = index_date.AddDays(-1);
            } while (index_date != from);
            

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
