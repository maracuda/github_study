using System;
using System.Linq;
using NUnit.Framework;
using StrategyBracketsLibrary;

namespace StraregyBracketsLibrary.Tests
{
    [TestFixture]
    public class BracketsTest
    {
        [Test]
        public void CheckArrayBrackets_InvalidSequence_Fail()
        {
            var input_strings = new[]
            {
                "}()()()()()()()()[][][][][]{{{{<><><>}}}}", "((((())))", "]{}",
                "][[[[]]]]<><><><><><><>()()()()(((<<<{{{}}}>>>)))"
            };

            var result =
                input_strings.All(
                    t =>
                        !(new BracketsVerifier(t, "<>{}[]()".ToCharArray(), new ArrayBracketsVerifier())).CheckBrackets());

            Assert.IsTrue(result);
        }

        [Test]
        public void CheckArrayBrackets_ValidSequence_Success()
        {
            var input_strings = new[]
            {
                "()()()()()()()()[][][][][]{{{{<><><>}}}}", "(((())))", "{}",
                "[[[[]]]]<><><><><><><>()()()()(((<<<{{{}}}>>>)))"
            };

            var result =
                input_strings.All(
                    t =>
                        (new BracketsVerifier(t, "<>{}[]()".ToCharArray(), new ArrayBracketsVerifier())).CheckBrackets());

            Assert.IsTrue(result);
        }

        [Test]
        public void CheckBrackets_InvalidInput_ThrowArgumentException()
        {
            var input_string = "abcd";

            var brackets_verifier =
                Assert.Throws<ArgumentException>(
                    () => new BracketsVerifier(input_string, "<>{}[]()".ToCharArray(), new ArrayBracketsVerifier()));
        }

        [Test]
        public void CheckStackBrackets_InvalidSequence_Fail()
        {
            var input_strings = new[]
            {
                "}()()()()()()()()[][][][][]{{{{<><><>}}}}", "((((())))", "]{}",
                "][[[[]]]]<><><><><><><>()()()()(((<<<{{{}}}>>>)))"
            };

            var result =
                input_strings.All(
                    t =>
                       !(new BracketsVerifier(t, "<>{}[]()".ToCharArray(), new StackBracketsVerifier())).CheckBrackets());

            Assert.IsTrue(result);
        }

        [Test]
        public void CheckStackBrackets_ValidSequence_Success()
        {
            var input_strings = new[]
            {
                "()()()()()()()()[][][][][]{{{{<><><>}}}}", "(((())))", "{}",
                "[[[[]]]]<><><><><><><>()()()()(((<<<{{{}}}>>>)))"
            };

            var result =
                input_strings.All(
                    t =>
                        (new BracketsVerifier(t, "<>{}[]()".ToCharArray(), new StackBracketsVerifier())).CheckBrackets());

            Assert.IsTrue(result);
        }
    }
}