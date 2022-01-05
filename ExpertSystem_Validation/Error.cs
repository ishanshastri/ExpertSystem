using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpertSystem_2
    {
    /// <summary>
    /// Contains information of a given error
    /// </summary>
    public class Error
        {
        private string message;
        private string errorType;//enum
        private int lineNumber;
        private TestState state;

        public Error(string errorType = "default_error", string message = "There was an error.", int lineNumber = -1)
            {
            this.message = message;
            this.errorType = errorType;
            this.lineNumber = lineNumber;
            }

        public string getMessage()
            {
            return this.message;
            }

        public string getErrorType()
            {
            return this.errorType;
            }

        public int getLineNumber()
            {
            return lineNumber;
            }

        public TestState GetState()
            {
            return this.state;
            }

        public void SetState(TestState t)
            {
            this.state = t;
            }
        }
    }
