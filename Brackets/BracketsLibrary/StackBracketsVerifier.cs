using System.Collections.Generic;

namespace BracketsLibrary
{
    public class StackBracketsVerifier : BracketsVerifierBase
    {
        public StackBracketsVerifier(string input_enumerable, char[] valid_brackets) : base(input_enumerable, valid_brackets)
        {
        }

        public override bool CkeckBrackets()
        {
            var st = new Stack<char>();

            foreach (var t in input_enumerable)
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