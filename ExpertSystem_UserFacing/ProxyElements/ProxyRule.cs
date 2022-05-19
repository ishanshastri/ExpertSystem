using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ExpertSystem_2
    {
    [Serializable]
    [XmlType("Rule")]
    public class ProxyRule : ProxyElement, IProxy
        {
        /// <summary>
        /// The name/id of the rule
        /// </summary>
        //[XmlAttribute("ID")]
        //public string ID;

        /// <summary>
        /// The collection of clauses that make up 'if part' (each element connected by AND)
        /// </summary>
        [XmlElement("If")]
        public List<IfPart> Clauses;

        /// <summary>
        /// The collection of assertions that make up 'then part' 
        /// </summary>
        [XmlElement("Then")]
        public List<ThenPart> Assertions;

        /// <summary>
        /// Syntactically validates the proxy object.
        /// </summary>
        /// <returns></returns>
        public ValidationResult Validate()
            {
            ValidationResult result = new ValidationResult();
            return result;
            }
        }
    }
