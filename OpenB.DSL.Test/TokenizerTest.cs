using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using OpenB.DSL.Tokenizing;
using System.Text;

namespace OpenB.DSL.Test
{
    [TestFixture]
    public class TokenizerTest
    {
        [Test]
        public void SimpleTokenizer_TwoTokenTypes_GetCount_Returns_5()
        {
            var tokenizer = new RegexBasedTokenizer(@"(?<expr>.+?\))((\s+(?<log_op>(AND|OR))(\s+?))|$)");
            IList<Token> tokens = tokenizer.Tokenize("(1+1=5) AND (2+2=4) OR (3+3=6)").ToList();

            Assert.That(tokens.Count, Is.EqualTo(5));


            createCode(tokens);
        }

        public string createCode(IList<Token> tokens)
        {
            StringBuilder builder = new StringBuilder("var result = ");
            foreach(Token token in tokens)
            {
                if (token.Groupname == "expr")
                {
                    builder.Append(token.Value);
                }

                if (token.Groupname == "log_op")
                {
                    switch (token.Value.ToLower())
                    {
                        case "and":
                            builder.Append(" && ");
                            break;
                        case "or":
                            builder.Append("|| ");
                            break;

                    }
                }
            }

            return builder.ToString();
        }
             
        //[Test]
        //public void ComplexTokenizer_TwoTokenTypes_FirstChildHasMatch()
        //{
        //    var tokenizer = new RegexBasedTokenizer(@"(?<expr>.+?\))((\s+(?<log_op>(AND|OR))(\s+?))|$)");

        //    var innerTokenizer = new RegexBasedTokenizer(@"(\[(?<field>.+?)\])|(?<const>\d+(\.\d{0,5})?)|(?<operator>[\+|\-|\*|/])");
        //    innerTokenizer.Add()
        //    tokenizer.Add("expr", innerTokenizer);
            

        //    IList<Token> tokens = tokenizer.Tokenize("([Model.Property]+3.12) AND (2-1) OR (3*6+(40/4))").ToList();
            
        //    Assert.That(tokens[0].ChildTokens.Count(), Is.EqualTo(3));
        //    Assert.That(tokens[2].ChildTokens.Count() , Is.EqualTo(3));
        //    Assert.That(tokens[4].ChildTokens.Count(), Is.EqualTo(3));

        //    Assert.That(tokens[0].ChildTokens.ToList()[0].Value, Is.EqualTo("Model.Property"));
        //}
    }
}