using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Diagnostics;

namespace ExpertSystem
    {
    [XmlType("KnowledgeBase")]
    [Serializable]
    class KnowledgeBase : IKnowledgeBase
        {
        /// <summary>
        /// Gets the collection of observations.
        /// </summary>
        /// <value>
        /// The observations.
        /// </value>
        [XmlAttribute("Observations")]
        public IEnumerable<IGenericFactAndObservation> Observations
            {
            get { return this._observations; }
            }

        /// <summary>
        /// Gets the collection of facts.
        /// </summary>
        /// <value>
        /// The facts.
        /// </value>
        [XmlAttribute("Facts")]
        public IEnumerable<IGenericFactAndObservation> Facts
            {
            get { return this._facts; }
            }

        /// <summary>
        /// Gets the collection of rules.
        /// </summary>
        /// <value>
        /// The rules.
        /// </value>
        [XmlAttribute("Rules")]
        public IEnumerable<Rule> Rules
            {
            get { return this._rules; }
            }

        /// <summary>
        /// The list of observations
        /// </summary>
        private List<IGenericFactAndObservation> _observations;

        /// <summary>
        /// The list of facts
        /// </summary>
        private List<IGenericFactAndObservation> _facts;

        /// <summary>
        /// The list of rules
        /// </summary>
        private List<Rule> _rules;

        /// <summary>
        /// Adds the rule to the knowledge base.
        /// </summary>
        /// <param name="r">The rule.</param>
        internal void AddRule(Rule rule)
            {
            this._rules.Add(rule);
            }

        /// <summary>
        /// Adds the fact to the knowledge base.
        /// </summary>
        /// <param name="fact">The fact.</param>
        internal void AddFact(IGenericFactAndObservation fact)
            {
            this._facts.Add(fact);
            }

        /// <summary>
        /// Adds the observation to the knowledge base.
        /// </summary>
        /// <param name="observation">The observation.</param>
        internal void AddObservation(IGenericFactAndObservation observation)
            {
            this._observations.Add(observation);
            }

        public KnowledgeBase()
            {
            this._observations = new List<IGenericFactAndObservation>();
            this._facts = new List<IGenericFactAndObservation>();
            this._rules = new List<Rule>();
            }
  
        /// <summary>
        /// Gets the rules that assert the given fact or observation.
        /// </summary>
        /// <param name="factObs">The fact or observation.</param>
        /// <returns>a collection of rules</returns>
        public IEnumerable<Rule> GetRulesThatAssert(IGenericFactAndObservation factObs)
            {
            List<Rule> rules = new List<Rule>();
            foreach(Rule r in this.Rules)
                {
                IEnumerable<IGenericFactAndObservation> facts = r.GetAssertedFactsAndObs();
                foreach(IGenericFactAndObservation f in facts)
                    {
                    if (f.GetName().Equals(factObs.GetName()))
                        {
                        rules.Add(r);
                        break;
                        }
                    }
                }
            return rules;
            }

        internal void BinarySerialize(string fileName)
            {
            try
                {
                string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "//" + fileName + ".bin";
                using (Stream stream = File.Open(path, FileMode.Create))
                    {
                    BinaryFormatter bf = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                    bf.Serialize(stream, this);
                    stream.Close();
                    }
                }
            catch(Exception e)
                {
                //Output Error Message
                Debug.WriteLine("Error: " + e.Message);
                }          
            }

        internal static KnowledgeBase LoadFromBinaryFile(string fileName)
            {
            try
                {
                string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "//" + fileName + ".bin";
                using (Stream stream = File.Open(path, FileMode.Open))
                    {
                    BinaryFormatter bf = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                    return (KnowledgeBase)bf.Deserialize(stream);
                    }
                }
            catch (Exception e)
                {
                //Display Error Message
                Debug.WriteLine("Error: " + e.Message);
                return null;
                }
            }
        }
    }




















/*System.Xml.Serialization.XmlSerializer x = new System.Xml.Serialization.XmlSerializer(this.GetType());           
x.Serialize(Console.Out, this);
Console.WriteLine();*/
