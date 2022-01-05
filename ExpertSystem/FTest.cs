using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpertSystem
    {
    class FTest<T> //: ITest
        {
        public Comparison ComparisonOperator { get; }
        public T Value { get; set; }
        public GenericFact<T> Fact { get; set; }
        
        /// <summary>
        /// Initializes a new instance of the <see cref="Test"/> class.
        /// </summary>
        /// <param name="testType">Type of the test.</param>
        /// <param name="value">The value.</param>
        public FTest(Comparison testType, T value)
            {
            this.ComparisonOperator = testType;
            this.Value = value;
            }

        public bool EvaluateTest()
            {
            switch(this.ComparisonOperator)
                {
                case Comparison.EqualTo:
                    return (this.Value.Equals(this.Fact.GetValue()));
                default:
                    return false;
                }
            }
        }
    }
