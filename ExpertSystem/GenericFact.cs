using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ExpertSystem
    {
    // Fact<Employee> f = new GenericFact<Employee>();
    // Fact<int> f2 = new Fact<int>();
    [XmlType ("GenericFact")]
    [Serializable]
     class GenericFact<T> : IGenericFactAndObservation
        {
        /// <summary>
        /// Initializes a new instance of the <see cref="Fact"/> class.
        /// </summary>
        /// <param name="valueType">Type of the value.</param>
        /// <param name="value">The value.</param> 
        /// <param name="prompt">The prompt.</param>
        public GenericFact(string name, bool isVital = false, object vitalValue = null, UserFacingFact proxyFact = null, string desc = "")
            {
            if(proxyFact == null)
                {
                this.CurrentState = FactState.Undefined;//value == null ? State.Undefined : State.Known;   
                this.Name = name;
                this.IsVital = isVital;
                this.FactSetter = new FactSetter(name);
                this.VitalValue = vitalValue;
                this._description = desc;
                }
            else
                {
                PopulateFromProxyFact(proxyFact);
                }           
            }

        /// <summary>
        /// Gets or sets the fact setter.
        /// </summary>
        /// <value>
        /// The fact setter.
        /// </value>
        public FactSetter FactSetter { get; set; }

        private object VitalValue { get; set; }

        /// <summary>
        /// The type of the fact (purely for xml editing purposes)
        /// </summary>
        protected string _type;

        protected string _description;

        public event EventHandler<IGenericFactAndObservation> OnFactValueChanged;

        private FactState CurrentState { get; set; }
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
        [XmlAttribute("Name")]
        public string Name { get; private set; }

        /// <summary>
        /// Whether the fact is vital or not (alerts user upon being set to a certain value)
        /// </summary>
        private bool IsVital;//Not sure if needed - unless 'NULL'can possibly be a vital value

        /// <summary>
        /// The value of the Fact instance (of type object)
        /// </summary>
        protected Object Value { get; set; }

        /// <summary>
        /// The current value
        /// </summary>
        private T _currentValue;

        /*
    /// <summary>
    /// Gets the current value.
    /// </summary>
    /// <returns>The current value  of type T</t></returns>
    public T Get_currentValue()
        {
        return this.__currentValue;
        }

    /// <summary>
    /// Sets the current value.
    /// </summary>
    public void Set_currentValue(T value)
        {
        if (!this.IsNewValueSameAsExistingValue(value))
            {
            this.__currentValue = value;
            // Fire events, etc....
            if (this.OnValueChanged != null)
                {
                this.OnValueChanged(this, null);
                }
            }
        }
        */
        /// <summary>
        /// Sets the value of the fact.
        /// </summary>
        public void SetValue(object val, Rule lastRule = null)
            {
            this.LastAssertingRule = lastRule;
            if (!this.IsNewValueSameAsExistingValue((T)val))
                {
                this._currentValue = (T)val;
                this.CurrentState = FactState.Known;
                // Fire events, etc....
                if (this.OnFactValueChanged != null)
                    {
                    this.OnFactValueChanged(this, this);
                    }
                }
          //  else
          //      {
                // We should never get here!
          //      Debugger.Break();
          //      }
            }
            
        public object GetValue()//Consider having another function that returns a value of type 'T'
            {
                if (this.GetState() == FactState.Known)
                {
                    return this._currentValue;
                }
                throw new Exception(string.Format("Can't get value of unknown fact {0}", this.Name));
            }

        /// <summary>
        /// Gets the textual description of the fact of observation.
        /// </summary>
        /// <returns></returns>
        public virtual string GetTextValue()
            {
            return this._description;
            }
        
        //   public void AssertValue(object val, Rule lastRule = null)
          //  {
          //  this.FactSetter.AssertFact(this, val, lastRule);
          //  }
        /// <summary>
        /// Gets the state of the fact.
        /// </summary>
        /// <returns>The fact's state</returns>
        public FactState GetState()
            {
            return this.CurrentState;
            }

        /// <summary>
        /// Evaluates the fact, and returns an enum of 'true', 'false', ('unkown' or 'undefined')?
        /// </summary>
        /// <returns>State</returns>
        public State Evaluate()
            {
            Test<T> test = new Test<T>(Comparison.EqualTo, (T)this.Value, this);
            return (test.TestState());
            }

        /// <summary>
        /// Gets the fact setter.
        /// </summary>
        /// <returns>the fact setter interface (IFactSetter)</returns>
        public FactSetter GetFactSetter()
            {
            return this.FactSetter;
            }

        /// <summary>
        /// Determines whether [is new value same as existing value] [the specified value].
        /// </summary>
        /// <param name="newValue">The new value.</param>
        /// <returns>
        ///   <c>true</c> if [is new value same as existing value] [the specified value]; otherwise, <c>false</c>.
        /// </returns>
        public bool IsNewValueSameAsExistingValue
                (T newValue)
            {
         //   if (this.GetState() == FactState.Undefined)
               // {
           //     return false;
             //   }
            return this.GetState() == FactState.Known && this._currentValue.Equals(newValue);
            }

        /// <summary>
        /// Sets fact state to unknown.
        /// </summary>
        public void SetUnknown()
            {
            this.CurrentState = FactState.Unknown;
            }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <returns>the name of the fact</returns>
        public string GetName()
            {
            return this.Name;
            }

        /// <summary>
        /// Populates the fact with user-facing properties using a serializable fact POCO.
        /// </summary>
        /// <param name="factOrObs">The fact or obs.</param>
        public void PopulateFromProxyFact(UserFacingFact fact)
            {
            this.Name = fact.Name;
            this._description = fact.Description;
            this.IsVital = fact.IsVital;
            this.VitalValue = fact.VitalValue;            
            }
        }
    }
