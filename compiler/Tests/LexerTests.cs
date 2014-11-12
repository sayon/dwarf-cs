using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using DwarfCompiler;

namespace Tests
{
    [TestClass]
    public class LexerTests
    {
        private static string _program = @"
n = 10;
x = 1;
y = 1;
while ( n > 0 ) {
     x = x + y;
     y = y + x;
     if (x == 0) { y = 10 } else skip;
     print(x);
     print(y);
     n = n - 2
}
";

        [TestMethod]
        public void LexerBasic()
        {
            var tokens = new Lexer(_program).Tokens().Aggregate("", (a, t) => a + t.ToString() + Environment.NewLine);
            string good = @"Ident
Assign
Number
Semicolon
Ident
Assign
Number
Semicolon
Ident
Assign
Number
Semicolon
While
Lpar
Ident
Gt
Number
Rpar
Lbrace
Ident
Assign
Ident
Plus
Ident
Semicolon
Ident
Assign
Ident
Plus
Ident
Semicolon
If
Lpar
Ident
Eq
Number
Rpar
Lbrace
Ident
Assign
Number
Rbrace
Else
Skip
Semicolon
Print
Lpar
Ident
Rpar
Semicolon
Print
Lpar
Ident
Rpar
Semicolon
Ident
Assign
Ident
Minus
Number
Rbrace
EOF
";
            Assert.AreEqual(tokens, good);
        }
    }
}
