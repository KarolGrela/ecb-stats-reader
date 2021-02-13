using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;           // used for reading XML file
using System.Globalization; // func. used: CultureInfo

namespace ecb_stats_reader
{
    /// <summary>
    /// Cube class - stores data from one cube:
    /// * date
    /// * list of entries
    /// * XmlNode variable
    /// </summary>
    class Cube
    {
        private List<Entry> entries;
        private DateTime date;
        private XmlNode nodeCube;

        /// <summary>
        /// Reads and stores date and list of entries from node
        /// </summary>
        /// <param name="node"> Cube node (second level) from which data is read </param>
        public Cube(XmlNode node)
        {
            nodeCube = node;    // save node
            entries = new List<Entry>();    // initialize list
            XmlAttributeCollection attr = node.Attributes;  // get collection of attributes
            date = DateTime.Parse(attr[0].Value);   // save date
            // create list of child nodes (entries) and loop through it in order to get abbreviation and data
            XmlNodeList childNodes = node.ChildNodes;
            foreach (XmlNode child in childNodes)
            {
                XmlAttributeCollection childAttr = child.Attributes;    // get attributes
                string abb = childAttr[0].Value;   // get abbreviation
                string strRate = childAttr[1].Value.Replace(',', '.');
                double rate = double.Parse(strRate, CultureInfo.InvariantCulture);         // get rate and parse it to dobule               
                Entry e = new Entry(abb, rate);                         // create entry
                entries.Add(e);                                         // add it to the list
            }
        }

        #region Date - getter

        public DateTime Date
        {
            get
            {
                return date;
            }
        }

        #endregion

        #region Entries - getters

        public Entry GetEntryByIndex(int index)
        {
            return entries[index];
        }

        public List<Entry> Entries
        {
            get
            {
                return entries;
            }
        }
        #endregion

    }
}
