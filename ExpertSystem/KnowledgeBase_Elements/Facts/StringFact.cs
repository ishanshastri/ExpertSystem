using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpertSystem_2
    {
    class StringFact : Fact
        {
        /// <summary>
        /// The value of the fact
        /// </summary>
        private string value;

        public StringFact(string id, string description) : base(id, description) { }//Constructor
        public StringFact(ProxyFact proxyFact) : base(proxyFact) { }//Constructor

        /// <summary>
        /// Gets the value of this fact.
        /// </summary>
        /// <returns>the string value</returns>
        public override string GetStringValue()
            {
            return this.value;
            }

        /// <summary>
        /// Gets the value of this fact.
        /// </summary>
        /// <returns>the string value</returns>
        public override object GetValue()
            {
            return this.value;
            }

        /// <summary>
        /// Sets the value of the fact.
        /// </summary>
        /// <param name="value">The fact value (string)</param>
        public override void SetValue(string value)
            {
            this.value = value;
            this.state = FactState.Known;
            }
        }
    }
