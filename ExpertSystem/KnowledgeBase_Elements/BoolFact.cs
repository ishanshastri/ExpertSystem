using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpertSystem_2
    {
    class BoolFact : Fact
        {
        /// <summary>
        /// The value of the fact
        /// </summary>
        private bool value;

        public BoolFact(string id, string description) : base(id, description) { }//Constructor
        public BoolFact(ProxyFact proxyFact) : base(proxyFact) { }//Constructor

        /// <summary>
        /// Gets the value of this fact.
        /// </summary>
        /// <returns>the boolean value</returns>
        public override bool GetBoolValue()
            {
            return this.value;
            }

        /// <summary>
        /// Gets the value of this fact.
        /// </summary>
        /// <returns>the boolean value</returns>
        public override object GetValue()
            {
            return this.value;
            }

        /// <summary>
        /// Sets the value of the fact.
        /// </summary>
        /// <param name="value">The fact value (boolean)</param>
        public override void SetValue(bool value)
            {
            this.value = value;
            this.state = FactState.Known;
            }
        }
    }
