using System.Collections.Generic;

namespace StrategyBracketsLibrary
{
    public class ArrayBracketsVerifier : IBracketsVerifier
    {

        public bool CkeckBrackets(IEnumerable<char> input_enumerable)
        {
            var i = 0;
            var char_list = new List<char>(input_enumerable);

            while (i < char_list.Count - 1)
            {
                if (char_list[i] == char_list[i + 1] - 1 || char_list[i] == char_list[i + 1] - 2)
                {
                    char_list.RemoveAt(i + 1);
                    char_list.RemoveAt(i);
                    if (i != 0)
                        i--;
                }
                else
                {
                    i++;
                }
            }
            return char_list.Count == 0;
        }
    }
}
