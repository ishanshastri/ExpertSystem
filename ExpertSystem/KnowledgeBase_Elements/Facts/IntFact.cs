using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpertSystem_2
    {
    class IntFact : Fact
        {
        /// <summary>
        /// The value of the fact
        /// </summary>
        private int value;

        public IntFact(string id, string description) : base(id, description) { }//Constructor
        public IntFact(ProxyFact proxyFact) : base(proxyFact) { }//Constructor

        /// <summary>
        /// Gets the value of this fact.
        /// </summary>
        /// <returns>the int value</returns>
        public override int GetIntValue()
            {
            return this.value;
            }

        /// <summary>
        /// Gets the value of this fact.
        /// </summary>
        /// <returns>the int value</returns>
        public override object GetValue()
            {
            return this.value;
            }

        /// <summary>
        /// Sets the value of the fact.
        /// </summary>
        /// <param name="value">The fact value (int)</param>
        public override void SetValue(int value)
            {
            this.value = value;
            this.state = FactState.Known;
            }
        }
    }
