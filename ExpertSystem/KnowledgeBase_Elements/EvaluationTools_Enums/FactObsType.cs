using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ExpertSystem
    {
    [XmlType("Type_Of_Fact_Or_Observation")]
    public enum FactObsType
        {
        Integer,
        String,
        Enum,
        Floating_Point,
        Boolean
        }
    }