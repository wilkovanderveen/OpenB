using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace OpenB.DSL
{
    public class Tokenizer
    {
        private readonly IDictionary<string, Tokenizer> _childTokenizers;
        private readonly Regex _regex;

        public Tokenizer(string regularExpression)
        {
            _regex = new Regex(regularExpression, RegexOptions.Multiline);
            _childTokenizers = new Dictionary<string, Tokenizer>();
        }

        public void Add(string groupName, Tokenizer tokenizer)
        {
            _childTokenizers.Add(groupName, tokenizer);
        }

        public IEnumerable<Token> Tokenize(string expression)
        {
            IList<Token> tokens = new List<Token>();

            if (_regex.Match(expression).Success)
            {
                foreach (Match m in _regex.Matches(expression))
                {
                    foreach (string groupName in _regex.GetGroupNames())
                    {
                        int groupNumber;

                        if (int.TryParse(groupName, out groupNumber))
                        {
                            continue;
                        }

                        Group g = m.Groups[groupName];

                        if (g.Success)
                        {
                            foreach (Capture capture in g.Captures)
                            {
                                bool canChildTokenizersBeApplied = _childTokenizers.Any(c => c.Key.Equals(groupName));

                                Token token = canChildTokenizersBeApplied ? new Token(groupName, TokenizeChildren(groupName, capture.Value), capture.Value) : new Token(groupName, capture.Value);
                                tokens.Add(token);
                            }
                        }
                    }
                }
            }

            return tokens;
        }

        private IEnumerable<Token> TokenizeChildren(string groupName, string value)
        {
            List<Token> tokens = new List<Token>();

            foreach (Tokenizer tokenizer in _childTokenizers.Where(c => c.Key.Equals(groupName)).Select(c => c.Value))
            {
                tokens.AddRange(tokenizer.Tokenize(value));
            }
            return tokens;
        }
    }
}