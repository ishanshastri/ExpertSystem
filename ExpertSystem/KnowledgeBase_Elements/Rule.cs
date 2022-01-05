using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ExpertSystem
    {
    /// <summary>
    /// Rules for the knowledge base
    /// </summary>
    /// <seealso cref="ExpertSystem.iRule" />
    //[XmlType ("GenericFact")]
    [Serializable]
    class Rule : iRule
        {
        /// <summary>
        /// The clauses
        /// </summary>
        private List<Clause> Clauses;

        /// <summary>
        /// The fact setter
        /// </summary>
        //private List<FactSetter> _FactSetters;

        private Dictionary<IGenericFactAndObservation, object> AssertionMap;
        /// <summary>
        /// Initializes a new instance of the <see cref="Rule" /> class.
        /// </summary>
        /// <param name="clauses">The clauses. (IEnumerable)</param>
        /// <param name="assertedFacts">The asserted facts.</param>
        public Rule(IEnumerable<Clause> clauses = null, Dictionary<IGenericFactAndObservation, Object> assertedFacts = null)
            {
            this.Clauses = (clauses == null) ? new List<Clause>() : (List<Clause>)clauses;
            this.AssertionMap = (assertedFacts == null) ? new Dictionary<IGenericFactAndObservation, Object>() : assertedFacts;
            //this._FactSetters = new List<FactSetter>();
            //Add event listeners for when a fact value is changed
            this.GetEventListeners();
            }

        //Go through each fact, and get eventlistners from each fact's factSetter
        private void GetEventListeners()
            {
            IEnumerable<IGenericFactAndObservation> facts;
            foreach (Clause c in this.Clauses)
                {
                facts = c.GetAllFacts();
                foreach (IGenericFactAndObservation f in facts)
                    {
                   // f.GetFactSetter().OnFactValueChanged += Rule_OnFactValueChanged;//OLD
                    f.OnFactValueChanged += F_OnFactValueChanged;
                    }
                }
            }

        /// <summary>
        ///  fired when fact value changed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e (The fact whose value has changed).</param>
        private void F_OnFactValueChanged(object sender, IGenericFactAndObservation e)
            {
            Console.WriteLine(string.Format("Fact {0} changed to {1}: ", e.GetName(), e.GetValue()));
         //   this.Evaluate();
            }

        /// <summary>
        /// Eventhandler fires when the vaule of a fact of interest changes; the rule is evaluated in real time for each changing value of a concerned fact
        /// </summary>
        /// <param name="sender">The sender (Fact).</param>
        /// <param name="e">null</param>
    /*    private void Rule_OnFactValueChanged(object sender, FactSetter e)//OLD
            {
            Console.WriteLine("Fact Value Changed: " + ((FactSetter)sender).FactName);
            this.Evaluate();
            }
        */

        /*
/// <summary>
/// Checks if the clauses have been met.
/// </summary>
/// <returns>
/// whether the rule's clauses have been met (boolean); clauses MUST be known
/// </returns>
public bool AllClausesMet()
{
foreach(Clause c in this.Clauses)//Loop through each clause 
   {
   if (!c.GetState().Equals(State.True))//If a clause is false, then rule is false
       {
       return false;
       }
   }
return true;
}
*/

        /// <summary>
        /// Evaluates the rule.
        /// </summary>
        /// <returns>
        /// The result of the evaluation (State enum: True, False, Unknown, Undefined)
        /// </returns>
        public State Evaluate()
            {
            if (GetClausesInState(State.False).Any())
                {
                return State.False;
                }
            if (GetClausesInState(State.Unknown).Any())
                {
                return State.Unknown;
                }
            if (GetClausesInState(State.Undefined).Any())
                {
                return State.Undefined;
                }
            MakeAssertionRequest();//Verify if this is the correct location
            return State.True;
            }

        /// <summary>
        /// Gets all the clauses.
        /// </summary>
        /// <returns>collection of clauses (IEnumerable)</returns>
        public IEnumerable<Clause> GetAllClauses()
            {
           // bool allClausesWanted = (s == null);
            List<Clause> clauses = new List<Clause>();
            foreach (Clause c in this.Clauses)
                {
              //  if (c.Evaluate() == s || allClausesWanted)
                //    {
                    clauses.Add(c);
                  //  }
                }
            return clauses;
            }
        public IEnumerable<Clause> GetClausesInState(State s)
            {
            List<Clause> clauses = new List<Clause>();
            foreach (Clause c in this.Clauses)
                {
                if (c.Evaluate() == s)
                    {
                    clauses.Add(c);
                    }
                }
            return clauses;
            }
       
        /// <summary>
        /// Gets the undefined facts.
        /// </summary>
        /// <returns>Collection of facts (IEnumerable)</returns>
        public IEnumerable<IGenericFactAndObservation> GetUndefinedFacts()//Not sure if needed
            {
            List<Clause> clauses = (List<Clause>)GetClausesInState(State.Undefined);
            List<IGenericFactAndObservation> undefinedFacts = new List<IGenericFactAndObservation>();
            foreach(Clause c in clauses)
                {
                foreach(IGenericFactAndObservation f in c.GetFacts(FactState.Undefined)){
                    undefinedFacts.Add(f);
                    }
                }
            return undefinedFacts;
            }

        /// <summary>
        /// Gets the facts.
        /// </summary>
        /// <param name="s">The s.</param>
        /// <returns>collection of facts (IEnumerable)</returns>
        public IEnumerable<IGenericFactAndObservation> GetFacts(FactState? s = null)
            {
            List<Clause> clauses = this.Clauses;
            Stack<IGenericFactAndObservation> facts = new Stack<IGenericFactAndObservation>();
            bool allFactsWanted = (s == null);
            foreach(Clause c in clauses)
                {
                foreach(IGenericFactAndObservation f in c.GetAllFacts())
                    {
                    if(f.GetState() == s || allFactsWanted)
                        {
                        facts.Push(f);
                        }
                    }
                }
            return (IEnumerable<IGenericFactAndObservation>)facts;
            }

        /// <summary>
        /// Gets the facts and observations asserted by the rule.
        /// </summary>
        /// <returns>a collection of facts and observations</returns>
        public IEnumerable<IGenericFactAndObservation> GetAssertedFactsAndObs()
            {
            return this.AssertionMap.Keys;
            }

        /// <summary>
        /// Makes the assertion request.
        /// </summary>
        private void MakeAssertionRequest()//public?
            {
            foreach(IGenericFactAndObservation f in this.AssertionMap.Keys)
                {
                //this._FactSetter.AssertFact(f, this.AssertionMap[f], this);STUB
                }
            }
        /*
        /// <summary>
        /// Checks the state of the rule, based on clauses connected by AND statements.
        /// </summary>
        /// <returns>the state</returns>
        public State GetRuleState()
            {
            bool possiblyUnkown = false;
            bool possiblyUndefined = false;
            State state = State.True;
            foreach (Clause c in this.Clauses)//Loop through each clause 
                {
                state = c.GetState();
                if (state == State.False)//If a clause is false, then rule is false
                    {
                    return state;
                    }
                if(state == State.Unknown) { possiblyUnkown = true; }
                if(state == State.Undefined) { possiblyUndefined = true; }
                }
            if (possiblyUnkown) { return State.Unknown; }//If no false clause, and if there are any unkown clauses, return unkown
            if(possiblyUndefined) { return State.Undefined; }//If no false or unkown clause, and if there are any undefined clauses, return undefined
            return State.True;//If no false, unknown or undefined clauses, there are only true clauses -> return true
            }*/
        }
    }
