using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ExpertSystem_2
    {
    [Serializable]
    [XmlType("If")]
    public class IfPart
        {
        /// <summary>
        /// The collection of condition statements (connected by OR statements in KB)
        /// </summary>
        [XmlElement("or_if")]
        public List<string> conditions;
        }
    }
