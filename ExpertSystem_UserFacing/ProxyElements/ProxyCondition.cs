using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ExpertSystem_2
    {
    [Serializable]
    [XmlType("condition")]
    public class ProxyCondition
        {
        public string comparison;

        public string getFactId()
            {
            return null; //STUB
            }
        }
    }
