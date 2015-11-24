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
        protected string input_string;

        public BracketsVerifierBase(string input_string, char[] valid_brackets)
        {
            this.input_string = input_string;
            _validBrackets = valid_brackets;
            if (!ValidateInput(input_string))
                throw new FormatException();
        }

        private bool ValidateInput(string input_array)
        {
            return input_array.All(c => _validBrackets.Contains(c));
        }

        public abstract bool CkeckBrackets();
    }
}
