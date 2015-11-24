﻿using System;
using Lib1 = BracketsLibrary;
using Lib2 = StrategyBracketsLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brackets
{
    class Program
    {
        static void Main(string[] args)
        {
            var str = "([][][][][])<>{{{}}}[][][][[[]]]]";

            Lib1.IBracketsVerifier brackets_verifier = new Lib1.ArrayBracketsVerifier(str, "<>{}[]()".ToCharArray());
            Lib1.IBracketsVerifier brackets_verifier_clone = new Lib1.StackBracketsVerifier(str, "<>{}[]()".ToCharArray());
            Console.WriteLine(brackets_verifier.CkeckBrackets());
            Console.WriteLine(brackets_verifier_clone.CkeckBrackets());
            Console.WriteLine("-------------");


            var strategy_brackets_verifier = new Lib2.BracketsVerifier(str, "<>{}[]()".ToCharArray(), new Lib2.ArrayBracketsVerifier());
            Console.WriteLine(strategy_brackets_verifier.VerifySequence());
            strategy_brackets_verifier.Verifier = new Lib2.StackBracketsVerifier();
            Console.WriteLine(strategy_brackets_verifier.VerifySequence());


            Console.ReadLine();
        }
    }
}
