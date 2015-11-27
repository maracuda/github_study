using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrueBrackets
{
    interface ISequenceValidationAlgo
    {
        bool IsValid(string source);
    }

    public class SequenceValidationAlgo : ISequenceValidationAlgo
    {
        private readonly IObjectMatcher _objectMatcher;

        public SequenceValidationAlgo(IObjectMatcher object_matcher)
        {
            _objectMatcher = object_matcher;
        }

        public bool IsValid(string source)
        {
            var stack = new Stack<char>();

            foreach (var element in source)
            {
                if (_objectMatcher.IsOpenening(element))
                    stack.Push(element);
                else
                {
                    char opening;
                    if (_objectMatcher.TryGetOpening(element, out opening))
                        if (stack.Pop() != opening)
                            return false;
                }
                return !stack.Any();
            }
        }
    }

    public interface IObjectMatcher
    {
        bool IsOpenening(char element);
        bool TryGetOpening(char element, out char opening);
    }

    public class ObjectMatcher : IObjectMatcher
    {
        private readonly Dictionary<char, char> _dictionary;
        private readonly HashSet<char> _hashSet;

        public ObjectMatcher()
        {
            _dictionary = new[] { "()", "[]" }.ToDictionary(x => x[1], x => x[0]);
            _hashSet = new HashSet<char>(_dictionary.Values);
        }

        public bool IsOpenening(char element)
        {
            return _hashSet.Contains(element);
        }

        public bool TryGetOpening(char element, out char opening)
        {
            return _dictionary.TryGetValue(element, out opening);
        }
    }
}
