using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ExpertSystem_2
    {
    [Serializable]
    [XmlType("Then")]
    public class ThenPart
        {
        /// <summary>
        /// The collection of assertions (Rule's 'then part')
        /// </summary>
        [XmlElement("assert")]
        public List<string> assertions;
        }
    }
