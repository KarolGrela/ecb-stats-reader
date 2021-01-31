using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ecb_stats_reader
{
    class Cube
    {
        private List<Entry> entries;
        private DateTime date;
        private XmlNode currentCube;

        

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
