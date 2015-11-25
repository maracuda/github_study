using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BracketsLibrary
{
    public abstract class BracketsVerifierBase : IBracketsVerifier
    {
        private char[] _validBrackets;
        protected string input_enumerable;

        public BracketsVerifierBase(string input_enumerable, char[] valid_brackets)
        {
            this.input_enumerable = input_enumerable;
            if (input_enumerable == "")
                throw new ArgumentException();
            _validBrackets = valid_brackets;
            if (!ValidateInput(input_enumerable))
                throw new ArgumentException();
        }

        private bool ValidateInput(string input_array)
        {
            return input_array.All(c => _validBrackets.Contains(c));
        }

        public abstract bool CkeckBrackets();
    }
}
