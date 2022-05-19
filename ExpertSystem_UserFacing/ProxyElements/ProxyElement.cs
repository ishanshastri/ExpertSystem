using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ExpertSystem_2
{
    public class ProxyElement
    {
        [XmlAttribute("ID")]
        public string ID { get; set; }
    }
}
