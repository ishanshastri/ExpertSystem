using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ExpertSystem_2
    {
    class Program
        {
        static void Main(string[] args)
            {
            //Serialize a knowledge base
            WriteXML();

            //Deserialize the XML file to a proxy kb
            ProxyKnowledgeBase pkb = DeserializeObject(getFolder());

            //Output Facts
            Console.WriteLine("\nKnowledge Base\n===========================================\nFacts:\n");
            foreach(ProxyFact f in pkb.Facts)
                {
                Console.WriteLine(f.id);
                }

            //Output Observations
            Console.WriteLine("\n===========================================\nObservations:\n");
            foreach (ProxyObservation o in pkb.Observations)
                {
                Console.WriteLine(o.id);
                }

            //Output Rules
            Console.WriteLine("\n===========================================\nRules:\n");
            foreach (ProxyRule r in pkb.Rules)
                {
                Console.WriteLine("If-Part:\n");
                foreach(IfPart i in r.Clauses)
                    {
                    foreach (string con in i.conditions)
                        {
                        Console.WriteLine(con);
                        }
                    }

                Console.WriteLine("\n--------------------\nThen-Part:\n");
                foreach (ThenPart t in r.Assertions)
                    {
                    foreach (string then in t.assertions)
                        {
                        Console.WriteLine(then);
                        }
                    }
                Console.WriteLine("\n***********************\n");
                }

            //Validate knowledge base and get errors
            ValidationResult val = pkb.Validate();
            Stack<Error> errs = val.GetErrorStack();

            //Output any errors found
            Console.WriteLine("\n===========================================\nErrors:\n");
            foreach(Error e in errs)
                {
                Console.WriteLine(e.getMessage());
                }

            if(errs.Count == 0)
                {
                Console.WriteLine("No Errors found");
                }
            Console.ReadLine();
            }

        public static string getFolder()
            {
            string myDocumentsFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            return Path.Combine(myDocumentsFolder, "testSerialization_trial");
            }

        public static void WriteXML()//test
            {
            //Test facts and obs
            ProxyFact fact = new ProxyFact();
            fact.id = "lineIsLeaking";
            fact.type = "boolean";// FactObsType.BooleanFact;
            ProxyFact fact2 = new ProxyFact();
            fact2.id = "voltageToTransformer";
            fact2.type = "float";
            ProxyFact fact3 = new ProxyFact();
            fact3.id = "possibleFuelLeak";
            fact3.type = "boolean";

            ProxyObservation obs1 = new ProxyObservation();
            obs1.id = "fuelSmell";
            obs1.type = "enum_level";

            //Initializations
            IfPart ifPart1 = new IfPart();
            IfPart ifPart2 = new IfPart();
            ThenPart tPart = new ThenPart();//Each rule should only have 1 ThenPart
            ProxyRule r = new ProxyRule();
            ProxyKnowledgeBase pkb = new ProxyKnowledgeBase();
            pkb.Rules = new List<ProxyRule>();
            ifPart1.conditions = new List<string>();
            ifPart2.conditions = new List<string>();
            tPart.assertions = new List<string>();
            r.Clauses = new List<IfPart>();
            r.Assertions = new List<ThenPart>();
            pkb.Facts = new List<ProxyFact>();
            pkb.Observations = new List<ProxyObservation>();
            ifPart1.conditions.Add("$factId:lineIsLeaking.equals(true)");
            //OR
            ifPart1.conditions.Add("$factId:fuelSmell.equals(level.high)");
            //AND
            ifPart2.conditions.Add("$factId:voltageToTransformer.equals(210.00)");
            //THEN
            tPart.assertions.Add("$factId:possibleFuelLeak.set(true)");
            r.Clauses.Add(ifPart1);
            r.Clauses.Add(ifPart2);
            r.Assertions.Add(tPart);
            pkb.Rules.Add(r);
            pkb.Facts.Add(fact);
            pkb.Facts.Add(fact2);
            pkb.Facts.Add(fact3);
            pkb.Observations.Add(obs1);
            try
                {
                string path = getFolder();
                using (Stream stream = File.Open(path, FileMode.Create))
                    {
                    XmlSerializer ser = new XmlSerializer(pkb.GetType());
                    ser.Serialize(stream, pkb);
                    stream.Close();
                    }
                }
            catch (Exception e)
                {
                //Output Error Message
                Debug.WriteLine("Error: " + e.Message);
                }
            }

        private static ProxyKnowledgeBase DeserializeObject(string filename)
            {
            // Create an instance of the XmlSerializer.
            XmlSerializer serializer =
            new XmlSerializer(typeof(ProxyKnowledgeBase));

            // Declare an object variable of the type to be deserialized.
            ProxyKnowledgeBase result;

            using (Stream reader = new FileStream(filename, FileMode.Open))
                {
                result = (ProxyKnowledgeBase)serializer.Deserialize(reader);
                }
            return result;
            }
        }
    }

/*SAMPLE KNOWLEDGE BASE
 * <KnowledgeBase>
 *  <List>
 *      <Rule id="ruleName">
 *          <--If Part-->
 *          <List
 *      </Rule>
 *  </List>
 * </KnowledgeBase>
 * */
//ProxyFactObservation fact = new ProxyFact(); 
// fact.id = "lineIsLeaking";
// fact.type = "boolean";// FactObsType.BooleanFact;
//fact.trialInt = 5;
