using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpertSystem
    {
    class BoolFact : GenericFact<bool>
        {
        private string _trueDesc;
        private string _falseDesc;
        public BoolFact(string name, bool isVital = false, object vitalValue = null, UserFacingFact proxyFact = null, string desc = "") : base(name, isVital, vitalValue, proxyFact, desc)
            {
            }

        /// <summary>
        /// Gets the textual description of the fact, depending on the value of the boolean
        /// </summary>
        /// <returns></returns>
        public override string GetTextValue()
            {
            if(this.GetState() == FactState.Unknown || this.GetState() == FactState.Undefined)
                {
                return this._description;
                }

            if(this.Evaluate() == State.True)
                {
                return this._trueDesc;
                }
            else
                {
                return this._falseDesc;
                }
            }
        }
    }
