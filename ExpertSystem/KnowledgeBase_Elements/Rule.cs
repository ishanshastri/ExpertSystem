using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpertSystem_2
    {
    class Rule
        {
        /// <summary>
        /// The clauses
        /// </summary>
        private List<Clause> clauses;

        /// <summary>
        /// The assertions
        /// </summary>
        private List<Assertion> assertions;

        /// <summary>
        /// The rule's identifier
        /// </summary>
        private string id;

        /// <summary>
        /// Gets or sets the rule's rank (for conflict resolution)
        /// </summary>
        /// <value>
        /// The rank.
        /// </value>
        public int Rank { get; set; }

        public Rule(List<Clause> clauses, List<Assertion> assertions, string id = "")
            {
            this.clauses = clauses;
            this.assertions = assertions;
            this.id = id;
            }

        public Rule(ProxyRule pr)
            {
            this.id = pr.ID;
            }

        private void consumeProxyRule(ProxyRule pr)
            {
            this.id = pr.ID;

            //Consume proxy clauses and conditions (IF PART)
            foreach(IfPart ip in pr.Clauses)
                {
                foreach(string condition in ip.conditions)
                    {
                    //STUB
                    }
                }
            }

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
            this.assert();
            return State.True;
            }

        /// <summary>
        /// Asserts all assertions to be made (invoked when rule evaluates to true).
        /// </summary>
        private void assert()
            {
            foreach(Assertion a in this.assertions)
                {
                a.Assert();
                }
            }

        /// <summary>
        /// Gets all clauses in in specified state.
        /// </summary>
        /// <param name="s">The state.</param>
        /// <returns>collection of clauses</returns>
        private IEnumerable<Clause> GetClausesInState(State s)
            {
            List<Clause> clauses = new List<Clause>();
            foreach (Clause c in this.clauses)
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
        public IEnumerable<Fact> GetUndefinedFacts()//Not sure if needed
            {
            List<Clause> clauses = (List<Clause>)GetClausesInState(State.Undefined);
            List<Fact> undefinedFacts = new List<Fact>();
            foreach (Clause c in clauses)
                {
                foreach (Fact f in c.GetFacts(FactState.Undefined))
                    {
                    undefinedFacts.Add(f);
                    }
                }
            return undefinedFacts;
            }

        /// <summary>
        /// Gets the undefined observations.
        /// </summary>
        /// <returns>Collection of observations (IEnumerable)</returns>
        public IEnumerable<Observation> GetUndefinedObservations()//Not sure if needed
            {
            List<Clause> clauses = (List<Clause>)GetClausesInState(State.Undefined);
            List<Observation> undefinedObservations = new List<Observation>();
            foreach (Clause c in clauses)
                {
                foreach (Observation o in c.GetObservations(FactState.Undefined))
                    {
                    undefinedObservations.Add(o);
                    }
                }
            return undefinedObservations;
            }

        /// <summary>
        /// Gets the asserted facts.
        /// </summary>
        /// <returns>a collection of facts</returns>
        public List<Fact> GetAssertedFacts()
            {
            List<Fact> result = new List<Fact>();
            foreach(Assertion a in this.assertions)
                {
                result.Add(a.GetFact());
                }
            return result;
            }
        }
    }
