using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpertSystem
    {
    [Serializable]
    class Clause
        {
        /// <summary>
        /// Initializes a new instance of the <see cref="Clause" /> class.
        /// </summary>
        /// <param name="FactsWithConditions">The facts with conditions.</param>
        public Clause(Dictionary<IGenericFactAndObservation, ITest> FactsWithConditions = null, List<ITest> tests = null)
            {
            //this.ConditionMap = (FactsWithConditions == null) ? new Dictionary<IGenericFact, Test<string>>() : FactsWithConditions;//OLD
            this.Tests = (tests == null) ? new List<ITest>() : tests;
            }

        /// <summary>
        /// The condition map (OLD)
        /// </summary>
       // private Dictionary<IGenericFact, Test<String>> ConditionMap;

        /*new*/
        private List<ITest> Tests;
        /// <summary>
        /// The facts that the clause evaluates.
        /// </summary>
        /// <returns>Collection of facts</returns>
        public IEnumerable<IGenericFactAndObservation> GetAllFacts()
            {
            List<IGenericFactAndObservation> ifacts = new List<IGenericFactAndObservation>();
            foreach(ITest it in this.Tests)
                {
                ifacts.Add(it.GetFact());
                }
            return ifacts;
            }

        /// <summary>
        /// Evaluates the clause.
        /// </summary>
        /// <returns>
        /// State of clause (enum)
        /// </returns>
        public State EEvaluate()
            {
            IEnumerable<IGenericFactAndObservation> knownFacts = this.GetFacts(FactState.Known);
            if (knownFacts.Count() != 0)
                {
                State s = new State();
                foreach (IGenericFactAndObservation f in knownFacts)
                    {
                    s = State.False;//this.EvaluateCondition(f, this.ConditionMap[f]);
                    if (s == State.True)
                        {
                        return State.True;
                        }
                    }
                }
            if (GetFacts(FactState.Undefined).Count() != 0)
                {
                return State.Undefined;
                }
            if (GetFacts(FactState.Unknown).Count() != 0)
                {
                return State.Unknown;
                }
            return State.False;
            }

        public State Evaluate()
            {
            bool possUnknown = false;
            bool possUndef = false;
            foreach (ITest it in this.Tests)
                {
                State s = it.TestState();
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
            if (possUndef)
                {
                return State.Undefined;
                }
            if (possUnknown)
                {
                return State.Unknown;
                }
            return State.False;
            }

        /// <summary>
        /// Gets the facts in a specified state.
        /// </summary>
        /// <returns>Collection of  facts (IEnumerable)</returns>
        public IEnumerable<IGenericFactAndObservation> GetFacts(FactState s)
            {
            Stack<IGenericFactAndObservation> facts = new Stack<IGenericFactAndObservation>();
            //bool allFactsWanted = (s == null);
            IEnumerable<IGenericFactAndObservation> allFacts = this.GetAllFacts();
            foreach (IGenericFactAndObservation f in allFacts)
                {
                if (f.GetState() == s)//||allFactsWanted
                    {
                    facts.Push(f);
                    }
                }
            return (IEnumerable<IGenericFactAndObservation>)facts;
            }

        /// <summary>
        /// Evaluates the condition.
        /// </summary>
        /// <param name="fact">The fact.</param>
        /// <param name="test">The test.</param>
        /// <returns>State of condition</returns>
        /*    private State EvaluateCondition(IGenericFact fact, Test test)
                {
                FactState s = fact.GetState();
                //return test
                if(s == FactState.Known)
                    {
                    bool conditionResult = false;
                    if (test.ComparisonOperator == Comparison.EqualTo) { conditionResult = (fact.CurrentValue.Equals(test.Value)); }//Case#1
                    //There will be more tests, such as 'greater than' for ints, etc.

                    if (conditionResult)
                        {
                        return State.True;
                        }
                    else
                        {
                        return State.False;
                        }             
                    }
                return (s == FactState.Undefined) ? State.Undefined : State.Unknown;
               */ /*
                if(s == State.Unknown || s == State.Undefined)
                    {
                    return s;
                    }

                bool ConditionResult = false;
                if(test.ComparisonOperator == Comparison.EqualTo)
                    {
                    ConditionResult = (fact.GetValue() == test.Value);
                    }
                //There will be more tests, such as 'greater than' for ints, etc.

                if (ConditionResult)
                    {
                    return State.True;
                    }
                else
                    {
                    return State.False;
                    }*/

        /*
        bool possibleFire;
        bool fireDrillIsOn;
        Fact handlePossibleFire;
        public EventHandler handlePossibleFireChanged(Fact f)
            {
            if ((bool)f.Value)//There is a possible fire
                {
                possibleFire = true;
                }
            else
                {
                possibleFire = false;
                }
            }

        public EventHandler handleFireDrillStateChanged(Fact f)
            {
            if ((bool)f.Value)//There is a possible fire
                {
                fireDrillIsOn = false;
                }
            else
                {
                fireDrillIsOn = true;
                }
            }

        private void setClauseValue()
            {
            if(!fireDrillIsOn && possibleFire)
                {
                this.State = true;
                }
            }
        */
        /// <summary>
        /// Adds the condition to the dictionary of conditions
        /// </summary>
        /// <param name="fact">The fact.</param>
        /// <param name="test">The test.</param>
        /// <param name="value">The value.</param>
        /// <exception cref="Exception">Value type does not match with Fact</exception>
        public void AddCondition(ITest test)
            {
            //verify that value is of correct type
            //if (!test.Value.GetType().Equals(fact.GetFactType()))
            //  {
            //throw new Exception("Value type does not match with Fact");
            //  }
            //Console.WriteLine();
            //Console.WriteLine(fact + " " + test.ComparisonOperator + " " + test.Value);
            //Add fact and corresponding value to dictionary

            //this.ConditionMap.Add(fact, test);
            this.Tests.Add(test);
            }

        /*OLD METHOD
        /// <summary>
        /// The state of the clause.
        /// </summary>
        /// <returns>bool; the state of the clause</returns>
        public State GetState()
            {
            State state = State.Undefined;
            bool possiblyUndefined = false;
            //bool possiblyFalse = false;
            bool possiblyUnknown = false;
            foreach(Fact f in this.ConditionMap.Keys)
                {
                state = this.EvaluateCondition(f, this.ConditionMap[f]);
                if(state == State.True)
                    {
                    return state;
                    }    

                //If it is not yet determined to be true, then check if it can be either undefined or false
                if(state == State.Undefined) { possiblyUndefined = true; }
                if(state == State.Unknown) { possiblyUnknown = true; }
                //if(state == State.False) { possiblyFalse = true; }
                }
            /*LOGIC behind whether clause is true or false (or statements)
             *============================================================
             * All this code runs assuming it is NOT true, since otherwise it would never reach here (see RETURN statement in foreach loop).
             * 
             * Only if all conditions are false can the clause be false, since even a single undefined or unknown condition can mean that 
             * the condition is either undefined or unknown given a certain number of false conditions.
             * 
             * Only if all conditions are unknown, or false and unknown, can the clause be unknown, since a single undefined condition can mean that 
             * the condition is undefined given a certain number of unknown conditions.
             * 
             * If it's not false, unknown, or true, then its undefined.
             *//*
            if(!possiblyUndefined && !possiblyUnknown) { state = State.False; }//If not true, not possibly undefined and not possibly unknown, then all FALSE conditions -> return false
            else if(!possiblyUndefined && possiblyUnknown) { state = State.Unknown; }//If not true, not possibly undefined and possibly unknown, then all UNKNOWN conditions of UNKNOWN+FALSE conditions -> return unknown
            else { state = State.Undefined; }
            return state;//If not true, not false, and not unknown, then must be undefined -> return undefined 
            }*/
        }
    }

