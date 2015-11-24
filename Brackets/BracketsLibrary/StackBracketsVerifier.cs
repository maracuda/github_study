using System.Collections.Generic;

namespace BracketsLibrary
{
    public class StackBracketsVerifier : BracketsVerifierBase
    {
        public StackBracketsVerifier(string input_string, char[] valid_brackets) : base(input_string, valid_brackets)
        {
        }

        public override bool CkeckBrackets()
        {
            var st = new Stack<char>();

            foreach (var t in input_string)
            {
                if (st.Count != 0 && (t == st.Peek() + 1 || t == st.Peek() + 2))
                    st.Pop();
                else
                    st.Push(t);
            }
            return st.Count == 0;
        }
    }
}