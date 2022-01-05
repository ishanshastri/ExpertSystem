using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace ExpertSystem
    {//Check Out: https://skilldrick.github.io/easy6502/
    class Program
        {
        /// <summary>
        /// The list of rules (knowledge base)
        /// </summary>
        static List<Rule> rulesList;

        /// <summary>
        /// The goal stack
        /// </summary>
        static Stack<Fact> GoalStack;

        /// <summary>
        /// The context stack
        /// </summary>
        public static List<IGenericFactAndObservation> ContextList;
        static void Main(string[] args)
            {
            Program.InitializeCollections();//Initialize the goal-stack, rule-list and context-list (none of these collections are used so far)
            /*TEST CODE*/ 
            //Generate Facts
            GenericFact<bool> FireAlarmOn = new GenericFact<bool>("FireAlarmOn");
            GenericFact<bool> FireInReactor = new GenericFact<bool>("FireInReactor");

            //test***
            string str = "true";
            bool value = false;
            TypeCode code = value.GetTypeCode();
             value = (bool)Convert.ChangeType(str, code);
            Console.WriteLine(value);

            //Generate Tests for each fact
            Test<bool> FireAlarmOn_Test = new Test<bool>(Comparison.EqualTo, false, FireAlarmOn);
            Test<bool> FireInReactor_Test = new Test<bool>(Comparison.EqualTo, true, FireInReactor);

            //Define a clause: (FireInReactor == true)
            Clause c1 = new Clause();
            c1.AddCondition(FireAlarmOn_Test);

            //Define a clause: (FireAlarmOn == false)
            Clause c2 = new Clause();
            c2.AddCondition(FireInReactor_Test);

            //Define a rule: (c1 && c2). Add the clauses
            List<Clause> clauses = new List<Clause>();
            clauses.Add(c1);
            clauses.Add(c2);
            Rule r = new Rule(clauses);

            IEnumerable<IGenericFactAndObservation> facts = r.GetFacts();//Get the list of facts in the rule; just to verify if this works

            //Give each fact a value, then evaluate the rule. For each fact assertion, an eventhandler is called causing text to display on the console; see 'Rule_OnFactValueChanged' in class 'Rule' 
            FireInReactor.SetValue(true);
            FireAlarmOn.SetValue(false);//remember that this has to be false for rule to be true (FireAlarmOn)
            Console.WriteLine(r.Evaluate());//evaluate rule given fact values
            FireInReactor.SetValue(false);//change value of FireInReactor fact
            Console.WriteLine(r.Evaluate());//reevaulate the rule given new fact value

            //populate and binary-serialize a knowledge base
            KnowledgeBase kb = new KnowledgeBase();
            kb.AddFact(FireInReactor);
            kb.AddFact(FireAlarmOn);
            kb.AddRule(r);
            kb.BinarySerialize("BinFile");

            //Load from bin file test
            KnowledgeBase newKb = KnowledgeBase.LoadFromBinaryFile("BinFile");
            }

        /// <summary>
        /// Initializes the global collections (not really used yet, but may find a use).
        /// </summary>
        private static void InitializeCollections()
            {
            ContextList = new List<IGenericFactAndObservation>();
            rulesList = new List<Rule>();
            GoalStack = new Stack<Fact>();
            }

        /// <summary>
        /// Adds a rule to rule list.
        /// </summary>
        /// <param name="r">a rule.</param>
        void AddToRuleList(Rule r)
            {
            rulesList.Add(r);
            }

        void BeginSession(Dictionary<string, IGenericFactAndObservation> PreambleLines)
            {
            foreach(string scriptLine in PreambleLines.Keys)
                {
                Console.WriteLine(scriptLine);
                }          
            }

        /// <summary>
        /// Prints the fact's name.
        /// </summary>
        /// <param name="f">The f.</param>
        void PrintFact(IGenericFactAndObservation f)
            {
            Console.WriteLine(f.GetName());
            }
        }
    }
