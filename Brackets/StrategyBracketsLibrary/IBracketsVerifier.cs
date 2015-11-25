using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrategyBracketsLibrary
{
    public interface IBracketsVerifier
    {
        bool CkeckBrackets(IEnumerable<char> input_enumerable);
    }
}
