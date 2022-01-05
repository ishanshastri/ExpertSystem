using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpertSystem
    {
    /// <summary>
    /// each GenericFact object instance has a FactSetter, through which the value of a fact should be changed; it allows eventhandler for a changed fact value to be added to a rule.
    /// </summary>
    [Serializable]
    class FactSetter
        {
        //public List<GenericFact<T>> Facts;

        /// <summary>
        /// Occurs when fact value changed.
        /// </summary>
        public event EventHandler<FactSetter> OnFactValueChanged;

        /// <summary>
        /// The fact name
        /// </summary>
        public string FactName;

        public FactSetter(string factName)//List<GenericFact<T>> facts = null)
            {
            //this.Facts = facts == null ? new List<GenericFact<T>>() : facts;
            this.FactName = factName;
            }
        public void AssertFact(IGenericFactAndObservation fact, object value, Rule lastAssertingRule = null)//T value, Rule lastAssertingRule)
            {
            if (Program.ContextList.Contains(fact))//If the list already has this fact, then remove it before updating the fact and reinserting it into the list
                {
                Program.ContextList.Remove(fact);
                }
            try
                {
                fact.SetValue(value, lastAssertingRule);
                if (this.OnFactValueChanged != null)
                    {
                    this.OnFactValueChanged(this, null);
                    }
                }
            catch (Exception e)
                {
                Console.WriteLine("Error during fact assertion: " + e.Message);
                }
            Program.ContextList.Add(fact);
            }
        }
    }
