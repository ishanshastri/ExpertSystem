using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpertSystem
    {
     interface IGenericFactAndObservation
        {
        event EventHandler<IGenericFactAndObservation> OnFactValueChanged;
        /// <summary>
        /// Gets the state of the fact.
        /// </summary>
        /// <returns>The fact's state</returns>
        FactState GetState();

        /// <summary>
        /// Gets the textual description of the fact of observation.
        /// </summary>
        /// <returns></returns>
        string GetTextValue();

        /// <summary>
        /// Gets the fact's current value.
        /// </summary>
        /// <returns>The current value.</returns>
        object GetValue();

        /// <summary>
        /// Sets fact state to unknown.
        /// </summary>
        void SetUnknown();

        /// <summary>
        /// Gets the name of the fact.
        /// </summary>
        /// <returns>the name</returns>
        string GetName();

        //FactSetter GetFactSetter();
        void SetValue(object value, Rule lastRule);

        void PopulateFromProxyFact(UserFacingFact factOrObs);
        }
    }
