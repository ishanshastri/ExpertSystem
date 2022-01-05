using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ExpertSystem
    {
    [XmlType("Operator")]
    public enum Comparison 
        {
        //GreaterThan = 2,    *LATER*
        //LessThan = 1,       *LATER*
        EqualTo,
        }
    }
