using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpertSystem
    {
    interface iRule//May not be needed
        {
        /// <summary>
        /// Checks if the clauses have been met.
        /// </summary>
        /// <returns>whether the rule's clauses have been met</returns>
        //bool AllClausesMet();

        /// <summary>
        /// Makes the assertion request.
        /// </summary>
        //void MakeAssertionRequest();

        /// <summary>
        /// Gets the state of the rule.
        /// </summary>
        /// <returns>The state (enum: True, False, Unkown, Undefined)</returns>
        State Evaluate();
        }
    }
