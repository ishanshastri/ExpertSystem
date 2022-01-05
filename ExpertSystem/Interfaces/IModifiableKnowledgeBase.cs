using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpertSystem
    {
    interface IModifiableKnowledgeBase
        {
        void AddRule(Rule rule);
        void AddFact(IGenericFactAndObservation fact);
        void AddObservation(IGenericFactAndObservation observation);
        void SaveToXmlFile(string filename);
        void LoadFromXmlFile();

        }
    }
