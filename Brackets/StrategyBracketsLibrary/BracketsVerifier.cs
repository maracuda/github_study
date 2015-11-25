using System;
using System.Linq;

namespace StrategyBracketsLibrary
{
    public class BracketsVerifier
    {
        private readonly char[] _validBrackets;
        protected string input_string;

        public BracketsVerifier(string input_string, char[] valid_brackets, IBracketsVerifier verifier)
        {
            this.input_string = input_string;
            if (input_string == "")
                throw new ArgumentException();
            _validBrackets = valid_brackets;
            Verifier = verifier;

            if (!ValidateInput(input_string))
                throw new ArgumentException();
        }

        public IBracketsVerifier Verifier { get; set; }

        public bool CheckBrackets()
        {
           return Verifier.CkeckBrackets(input_string);
        }
        private bool ValidateInput(string input_array)
        {
            return input_array.All(c => _validBrackets.Contains(c));
        }
    }
}