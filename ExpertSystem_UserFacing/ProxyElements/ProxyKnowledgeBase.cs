using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ExpertSystem_2
    {
    [Serializable]
    [XmlType("KnowledgeBase")]
    public class ProxyKnowledgeBase : IProxy
        {
        /// <summary>
        /// The collection of facts
        /// </summary>
        [XmlElement("Fact")]//Add container
        public List<ProxyFact> Facts;

        /// <summary>
        /// The collection of observations
        /// </summary>
        [XmlElement("Observation")]
        public List<ProxyObservation> Observations;

        /// <summary>
        /// The collection of observations
        /// </summary>
        [XmlElement("Rule")]
        public List<ProxyRule> Rules;

        /// <summary>
        /// Determines whether a given fact is valid
        /// </summary>
        /// <param name="fact">The fact.</param>
        /// <returns>
        /// <c>true</c> if the fact is in the list; otherwise, <c>false</c>.
        /// </returns>
        private bool isInFactList(string fid)
            {
            foreach(ProxyFact f in Facts)
                {
                if (f.id.Equals(fid))
                    {
                    return true;
                    }
                }
            return false;
            }

        /// <summary>
        /// Syntactically validates the proxy object.
        /// </summary>
        /// <returns></returns>
        public ValidationResult Validate()
            {
            ValidationResult result = new ValidationResult();
            //Check for different errors  
            Error err = error_duplicateFact();//#1: for duplicate fact error
            if (err != null)
                {
                result.AddError(err);
                //result.SetState(TestState.InvalidXmlInput);
                }

            err = error_factDoesNotExist();//#3: if fact referenced in a given rule does not exist in knowledge base
            if (err != null)
                {
                result.AddError(err);
                //result.SetState(TestState.InvalidXmlInput);
                }

            return result;
            }

        /// <summary>
        /// Duplicates the fact.
        /// </summary>
        /// <returns></returns>
        private Error error_duplicateFact()//make return type Error/boolean
            {
            HashSet<string> factIds = new HashSet<string>();
            foreach(ProxyFact f in Facts)
                {
                if(factIds.Contains(f.id))
                    {
                    return new Error("duplicate_fact_error", "There is a duplicate Fact in the Knowledge Base: " + f.id);
                    }
                factIds.Add(f.id);
                }
            return null;
            }

        /// <summary>
        /// Error: The fact referenced in a rule does not exist
        /// </summary>
        /// <returns></returns>
        private Error error_factDoesNotExist()
            {
            IEnumerable<string> ids;
            foreach(ProxyRule r in Rules)
                {
                ids = getFactIds(r);
                foreach(string id in ids){
                    if (!isInFactList(id))
                        {
                        return new Error("nonexistent_fact", "A fact referenced in a rule does not exist in the Knowledge Base: " + id);
                        }
                    }
                }
            return null;
            }

        /// <summary>
        /// Gets the fact ids in a given rule (both if and then parts).
        /// </summary>
        /// <param name="r">The rule.</param>
        /// <returns></returns>
        private IEnumerable<string> getFactIds(ProxyRule r)
            {
            HashSet<string> ids = new HashSet<string>();
            string id = string.Empty;
            int startInd;

            //Gather facts from IF-Part
            foreach (IfPart iPart in r.Clauses)
                {
                foreach (string con in iPart.conditions)
                    {
                    startInd = con.IndexOf("$factId:") + 8;
                    id = con.Substring(startInd, (con.IndexOf(".")) - startInd);
                    if (!isInArr(ids, id))
                        {
                        ids.Add(id);
                        }
                    }
                }

            //Gather facts from THEN-Part
            foreach (ThenPart tPart in r.Assertions)
                {
                foreach (string then in tPart.assertions)
                    {
                    startInd = then.IndexOf("$factId:") + 8;
                    id = then.Substring(startInd, (then.IndexOf(".")) - startInd);
                    if (!isInArr(ids, id))
                        {
                        ids.Add(id);
                        }
                    }
                }           
            return ((IEnumerable<string>)ids);
            }
        /// <summary>
        /// Determines whether a given string is present in a collection of strings.
        /// </summary>
        /// <param name="arr">The arr.</param>
        /// <param name="find">The string being checked for presense.</param>
        /// <returns>
        ///   <c>true</c> if find is in arr; otherwise, <c>false</c>.
        /// </returns>
        private bool isInArr(HashSet<string> strings, string find)
            {
            foreach(string s in strings)
                {
                if (s.Equals(find))
                    {
                    return true;
                    }
                }
            return false;
            }
        }
    }
