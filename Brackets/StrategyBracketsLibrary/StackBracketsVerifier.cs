using System.Collections.Generic;

namespace StrategyBracketsLibrary
{
    public class StackBracketsVerifier : IBracketsVerifier
    {
        public bool CkeckBrackets(IEnumerable<char> input_enumerable)
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