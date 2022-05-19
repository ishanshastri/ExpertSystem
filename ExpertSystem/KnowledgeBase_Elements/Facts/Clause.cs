using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpertSystem_2
    {
    class Clause
        {
        /// <summary>
        /// The collection of tests (or conditions)
        /// </summary>
        private List<Test> tests;

        public Clause(List<Test> tests)
            {
            this.tests = tests;
            }

        /// <summary>
        /// Evaluates the clause.
        /// </summary>
        /// <returns>the evaluation result (clause)</returns>
        public State Evaluate()
            {
            bool possUnknown = false;//There is an unknown fact/observation
            bool possUndef = false;//There is an undefined fact/observationn
            foreach (Test it in this.tests)
                {
                State s = it.Evaluate();
                switch (s)
                    {
                    case State.True:
                        return State.True;
                    case State.Undefined:
                        possUndef = true;
                        break;
                    case State.Unknown:
                        possUnknown = true;
                        break;
                    }
                }
            if (possUndef)//If not true but possible undefined
                {
                return State.Undefined;//If not true but possible undefined
                }
            if (possUnknown)//If not true or undefined but possibly unknown
                {
                return State.Unknown;
                }
            return State.False;//If none of the above conditions, then the clause must  be false
            }

        /// <summary>
        /// Gets the facts in a specified state.
        /// </summary>
        /// <returns>Collection of  facts (IEnumerable)</returns>
        public IEnumerable<Fact> GetFacts(FactState s)
            {
            List<Fact> facts = new List<Fact>();

            Fact f;
            foreach (Test t in this.tests)
                {
                f = t.Fact;
                if (f != null && f.GetState() == s)
                    {
                    facts.Add(f);
                    }
                }
            return (IEnumerable<Fact>)facts;
            }

        /// <summary>
        /// Gets the observations in a specified state.
        /// </summary>
        /// <returns>Collection of  observations (IEnumerable)</returns>
        public IEnumerable<Observation> GetObservations(FactState s)
            {
            List<Observation> observations = new List<Observation>();

            Observation o;
            foreach (Test t in this.tests)
                {
                o = t.Observation;
                if (o != null && o.GetState() == s)
                    {
                    observations.Add(o);
                    }
                }
            return (IEnumerable<Observation>)observations;
            }
        }
    }
