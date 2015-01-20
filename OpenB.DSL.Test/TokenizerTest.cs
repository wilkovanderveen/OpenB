using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace OpenB.DSL.Test
{
    [TestFixture]
    public class TokenizerTest
    {
        [Test]
        public void SimpleTokenizer_TwoTokenTypes_GetCount_Returns_5()
        {
            var tokenizer = new Tokenizer(@"(?<expr>.+?\))((\s+(?<log_op>(AND|OR))(\s+?))|$)");
            IList<Token> tokens = tokenizer.Tokenize("(1+1) AND (2+2) OR (3+3)").ToList();

            Assert.That(tokens.Count, Is.EqualTo(5));
        }

        [Test]
        public void ComplexTokenizer_TwoTokenTypes_FirstChildHasMatch()
        {
            var tokenizer = new Tokenizer(@"(?<expr>.+?\))((\s+(?<log_op>(AND|OR))(\s+?))|$)");

            var innnerTokenizer = new Tokenizer(@"(\[(?<field>.+?)\])|(?<const>\d+(\.\d{0,5})?)|(?<operator>[\+|\-|\*])");
            tokenizer.Add("expr", innnerTokenizer);

            IList<Token> tokens = tokenizer.Tokenize("([Model.Property]+3.12) AND (2-1) OR (3*6)").ToList();
            
            Assert.That(tokens[0].ChildTokens.Count(), Is.EqualTo(3));
            Assert.That(tokens[2].ChildTokens.Count() , Is.EqualTo(3));
            Assert.That(tokens[4].ChildTokens.Count(), Is.EqualTo(3));
        }
    }
}