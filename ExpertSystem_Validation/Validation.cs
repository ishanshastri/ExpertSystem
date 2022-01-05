using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpertSystem_2
    {
    public class ValidationResult
        {
        private Stack<Error> _errorStack;
        private TestState state;

        public ValidationResult()
            {
            this._errorStack = new Stack<Error>();
            }

        public void AddError(Error e)
            {
            this._errorStack.Push(e);
            }

        public Stack<Error> GetErrorStack()
            {
            return this._errorStack;
            }

        public void SetState(TestState t)
            {
            this.state = t;
            }

        public TestState GetState()
            {
            return this.state;
            }
        }
    }
