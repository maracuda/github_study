using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BracketsLibrary
{
    public class ArrayBracketsVerifier : BracketsVerifierBase
    {
        public ArrayBracketsVerifier(string input_enumerable, char[] valid_brackets)
            : base(input_enumerable, valid_brackets)
        {
        }

        public override bool CkeckBrackets()
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
