using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpertSystem
    {
    [Serializable]
    class Test<T> : ITest
        {
        /// <summary>
        /// Gets the comparison operator.
        /// </summary>
        /// <value>
        /// The comparison operator.
        /// </value>
        public Comparison ComparisonOperator { get; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public T Operand { get; set; }

        /// <summary>
        /// Gets or sets the fact.
        /// </summary>
        /// <value>
        /// The generic fact.
        /// </value>
        public GenericFact<T> Fact { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Test"/> class.
        /// </summary>
        /// <param name="testType">Type of the test.</param>
        /// <param name="value">The value.</param>
        public Test(Comparison testType, T value, GenericFact<T> fact)
            {
            this.ComparisonOperator = testType;
            this.Operand = value;
            this.Fact = fact;
            }

        /// <summary>
        /// Gets the fact.
        /// </summary>
        /// <returns>the fact (IGenericFact)</returns>
        public IGenericFactAndObservation GetFact()
            {
            return (IGenericFactAndObservation)this.Fact;
            }
        /// <summary>
        /// Determines whether the test is successful.
        /// </summary>
        /// <param name="fact">The fact.</param>
        /// <returns>
        ///   <c>true</c> if the specified test is succesful; otherwise, <c>false, undefined or unknown depending on state of fact</c>.
        /// </returns>
        public State TestState()
            {
            switch (this.ComparisonOperator)
                {               
                case Comparison.EqualTo:
                    return this.Fact.GetValue().Equals(this.Operand) ? State.True : State.False;
                default:
                    return this.Fact.GetState() == FactState.Undefined ? State.Undefined : State.Unknown;
                }
            }
        }
    }
