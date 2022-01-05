using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpertSystem
    {
    /// <summary>
    /// A fact that holds a certain value - can be undefined or unkown
    /// </summary>
    class Fact 
        {
        /// <summary>
        /// Initializes a new instance of the <see cref="Fact"/> public class.
        /// </summary>
        /// <param name="valueType">Type of the value.</param>
        /// <param name="value">The value.</param>
        /// <param name="prompt">The prompt.</param>
        public Fact(Type valueType, string name, string prompt, bool isVital = false, object vitalValue = null)
            {
            //this.Value = value;
            this.Prompt = prompt;
            this.FactType = valueType;//Make type an enum
            this.CurrentState = FactState.Undefined;//value == null ? State.Undefined : State.Known;   
            this.Name = name;
            this.IsVital = isVital;
            }

        /// <summary>
        /// Gets the last asserting rule.
        /// </summary>
        /// <value>
        /// The asserting rule.
        /// </value>
        public Rule LastAssertingRule { get; private set; }
      
        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; private set; }

        /// <summary>
        /// Whether the fact is vital or not (alerts user upon being set to a certain value)
        /// </summary>
        private bool IsVital;

        /// <summary>
        /// Occurs when fact value changed.
        /// </summary>
        public event EventHandler<Fact> OnValueChanged;

        /// <summary>
        /// The value of the Fact instance (of type object)
        /// </summary>
        private Object Value { get; set; }

        /// <summary>
        /// Determines whether [is new value same as existing value] [the specified value].
        /// </summary>
        /// <param name="val">The value.</param>
        /// <returns>
        ///   <c>true</c> if [is new value same as existing value] [the specified value]; otherwise, <c>false</c>.
        /// </returns>
        virtual protected bool IsNewValueSameAsExistingValue
               (object val)
            {
                return val.Equals(this.Value);
            }

        /// <summary>
        /// Sets the value of the fact.
        /// </summary>
        public void SetValue(object val, Rule lastRule = null)
            {
            if (this.IsNewValueSameAsExistingValue(this.Value))//verify that new value is not same as existing value
                {
                return;
                }

            this.Value = val;
            if (val != null)
                {
                this.CurrentState = FactState.Known;
                }
            else
                {
                this.CurrentState = FactState.Unknown;//NOTICE: CHECK IF CORRECT METHOD USED (set value null --> unknown state)
                }
            this.LastAssertingRule = lastRule;//Update last asserting rule
            //Fire event                                 
            if (this.OnValueChanged != null)
                {
                this.OnValueChanged(this, null);
                }
            }

        /// <summary>
        /// Sets fact state to unknown.
        /// </summary>
        public void SetUnknown()
            {
            this.CurrentState = FactState.Unknown;
            }

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <returns>this.Value (object)</returns>
        public object GetValue()
            {
            return this.Value;
            }

        private FactState CurrentState { get; set; }
        /// <summary>
        /// Gets or sets the type of the fact.
        /// </summary>
        /// <value>
        /// The type of the fact's value.
        /// </value>
        private Type FactType { get; set; }

        /// <summary>
        /// Gets or sets the prompt to gain information from the user.
        /// </summary>
        /// <value>
        /// The prompt (string).
        /// </value>
        public string Prompt { get; set; }

        /// <summary>
        /// Gets the state of the fact.
        /// </summary>
        /// <returns>The fact's state</returns>
        public FactState GetState()
            {
            return this.CurrentState;
            }

        /// <summary>
        /// Gets the type of the Fact's value.
        /// </summary>
        /// <returns>this.Value.GetType()</returns>
        public Type GetFactType()
            {
            return this.FactType;
            }
        }
    }
