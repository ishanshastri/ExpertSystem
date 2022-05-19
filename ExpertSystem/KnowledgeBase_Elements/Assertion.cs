using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpertSystem_2
    {
    class Assertion
        {
        private Fact fact;
        private object value;
        
        public Assertion(Fact fact, object value)
            {
            this.fact = fact;
            this.value = value;
            }

        /// <summary>
        /// Follows through with assertion.
        /// </summary>
        public void Assert()
            {
            //Assert accordingly depending on type of value
            if (this.value.GetType().Equals(typeof(bool)))
                {
                this.fact.SetValue((bool)this.value);
                return; 
                }
            if (this.value.GetType().Equals(typeof(int)))
                {
                this.fact.SetValue((int)this.value);
                return;
                }
            if (this.value.GetType().Equals(typeof(double)))
                {
                this.fact.SetValue((double)this.value);
                return;
                }
            }

        public Fact GetFact()
            {
            return this.fact;
            }
        }
    }
