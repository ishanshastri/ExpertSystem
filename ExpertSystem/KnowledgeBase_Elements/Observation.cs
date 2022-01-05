using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ExpertSystem
    {
    [XmlType("Observation")]
    [Serializable]
    class Observation<T> : GenericFact<T>, IGenericFactAndObservation
        {
        /// <summary>
        /// Gets the prompt.
        /// </summary>
        /// <value>
        /// The prompt.
        /// </value>
        public string Prompt { get; private set; }

        /// <summary>
        /// Initializes a new instance of an observation <see cref="Observation{T}"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="prompt">The prompt.</param>
        /// <param name="isVital">if set to <c>true</c> [is vital].</param>
        /// <param name="vitalValue">The vital value.</param>
        public Observation(string name, string prompt, bool isVital = false, object vitalValue = null) : base(name, isVital, vitalValue)
            {
            this.Prompt = prompt;
            }
        }
    }




/*
public void GetValueFromUser()
    {
    Console.WriteLine(this.Prompt);
    try
        {
        TypeCode code = Convert.GetTypeCode(this.Value);
        T value = (T)Convert.ChangeType(Console.ReadLine(), code);
        base.SetValue(Console.ReadLine());
        }
    catch(Exception e)
        {

        }
    }*/

