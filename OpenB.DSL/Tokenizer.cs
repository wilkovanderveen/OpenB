using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace OpenB.DSL
{
    public class Tokenizer
    {
        private readonly bool _recursive;
        private readonly Regex _regex;

        public Tokenizer(string regularExpression, bool recursive)
        {
            _regex = new Regex(regularExpression, RegexOptions.Multiline);
            _recursive = recursive;
        }

        public IEnumerable<Token> Tokenize(string expression)
        {
            throw new NotImplementedException();

            if (_regex.Match(expression).Success)
            {
            }
           
        }
    }

    public class Token
    {
    }
}