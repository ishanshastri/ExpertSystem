using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpertSystem_2
    {
    class Test
        {
        /// <summary>
        /// Gets the comparison operator.
        /// </summary>
        /// <value>
        /// The comparison operator.
        /// </value>
        public ComparisonOperator ComparisonOperator { get; }

        /// <summary>
        /// Gets or sets the fact that is compared with a given value.
        /// </summary>
        /// <value>
        /// The fact.
        /// </value>
        public Fact Fact { get; set; }

        /// <summary>
        /// Gets or sets the observation that is compared with a given value.
        /// </summary>
        /// <value>
        /// The observation.
        /// </value>
        public Observation Observation { get; set; }

        /// <summary>
        /// Gets the operand.
        /// </summary>
        /// <value>
        /// The operand (value that the fact is compared to).
        /// </value>
        public object Operand { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Test"/> class.
        /// </summary>
        /// <param name="testType">Type of the test.</param>
        /// <param name="value">The value.</param>
        public Test(ComparisonOperator testType, object value, Fact fact)
            {
            this.ComparisonOperator = testType;
            this.Operand = value;
            this.Fact = fact;
            }

        /// <summary>
        /// Initializes a new instance of the <see cref="Test"/> class.
        /// </summary>
        /// <param name="testType">Type of the test.</param>
        /// <param name="value">The value.</param>
        public Test(ComparisonOperator testType, object value, Observation observation)
            {
            this.ComparisonOperator = testType;
            this.Operand = value;
            this.Observation = observation;
            }

        /// <summary>
        /// Evaluates the condition.
        /// </summary>
        /// <returns>the evaluation result (state)</returns>
        public State Evaluate()
            {
            //Get numerical value, if the fact type is numeric
            double numVal = 0;
            bool isInt = false;

            FactState factState = new FactState();
            object value;

            //Get values from observation or fact appropriately
            if (this.Observation.Equals(null))
                {
                factState = this.Fact.GetState();
                value = this.Fact.GetValue();
                if (value.GetType().Equals(typeof(int)))//Or compare with operand
                    {
                    numVal = this.Fact.GetIntValue();
                    isInt = true;
                    }
                if (value.GetType().Equals(typeof(double)))
                    {
                    numVal = this.Fact.GetDoubleValue();
                    }
                }
            else
                {
                factState = this.Observation.GetState();
                value = this.Observation.GetValue();
                if (value.GetType().Equals(typeof(int)))//Or compare with operand
                    {
                    numVal = this.Observation.GetIntValue();
                    isInt = true;
                    }
                if (value.GetType().Equals(typeof(double)))
                    {
                    numVal = this.Observation.GetDoubleValue();
                    }
                }

            //Perform checks
            if (!factState.Equals(FactState.Known))
                {
                //If the fact isn't known (it is unknown or undefined), return the state of the fact
                return factState == FactState.Undefined ? State.Undefined : State.Unknown;
                }
            switch (this.ComparisonOperator)
                {
                //If the comparing whether the fact is equal to a given value
                case ComparisonOperator.EqualTo:
                    return value.Equals(this.Operand) ? State.True : State.False;
                //If the comparing whether the fact is greater than a given value (only numerics)
                case ComparisonOperator.GreaterThan:
                    if (isInt)
                        {
                        return numVal > (int)this.Operand ? State.True : State.False;
                        }
                    return numVal > (double)this.Operand ? State.True : State.False;
                //If the comparing whether the fact is less than a given value (only numerics)
                case ComparisonOperator.LessThan:
                    if (isInt)
                        {
                        return numVal < (int)this.Operand ? State.True : State.False;
                        }
                    return numVal < (double)this.Operand ? State.True : State.False;
                default:
                    return State.Unknown;//Default case; this is not intended to run
                }
            }
        }
    }
