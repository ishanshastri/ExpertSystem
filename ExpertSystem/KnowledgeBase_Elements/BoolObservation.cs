using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpertSystem_2
    {
    class BoolObservation : Observation
        {
        /// <summary>
        /// The value of the observation
        /// </summary>
        private bool value;

        public BoolObservation(string id, string description, string prompt = "") : base(id, description, prompt) { }//Constructor
        public BoolObservation(ProxyObservation proxyObs) : base(proxyObs) { }//Constructor

        /// <summary>
        /// Gets the value of this observation.
        /// </summary>
        /// <returns>the boolean value</returns>
        public override bool GetBoolValue()
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
        /// Sets the value of the observation.
        /// </summary>
        /// <param name="value">The observation value (boolean)</param>
        public override void SetValue(bool value)
            {
            this.value = value;
            this.state = FactState.Known;
            }
        }
    }
