using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DwarfCompiler;

namespace Tests
{
    [TestClass]
    public class ParserTests
    {
        [TestMethod]
        public void Print()
        {
            var teststr = "print (x)";
            var parser = new Parser(new Lexer(teststr).Tokens());
            var res = parser.ParsePrint();
            var pp = new PrettyPrinter();
            res.Accept(pp);
            Assert.AreEqual(teststr, pp.Result);
        }
        
        [TestMethod]
        public void Identifier()
        {
            var teststr = "ident";
            var parser = new Parser(new Lexer(teststr).Tokens());
            var res = parser.ParseIdentifier();
            var pp = new PrettyPrinter();
            res.Accept(pp);
            Assert.AreEqual(teststr, pp.Result);
        }

        [TestMethod]
        public void Number()
        {
            var teststr = "42";
            var parser = new Parser(new Lexer(teststr).Tokens());
            var res = parser.ParseNumber();
            var pp = new PrettyPrinter();
            res.Accept(pp);
            Assert.AreEqual(teststr, pp.Result);
        }

        [TestMethod]
        public void Expression()
        {
            var teststr = "x + 42";
            var parser = new Parser(new Lexer(teststr).Tokens());
            var res = parser.ParseExpression();
            var pp = new PrettyPrinter();
            res.Accept(pp);
            Assert.AreEqual(teststr, pp.Result);
        }

        [TestMethod]
        public void Assignment()
        {
            var teststr = "x = 42 + 22 - 1";
            var parser = new Parser(new Lexer(teststr).Tokens());
            var res = parser.ParseAssignment();
            var pp = new PrettyPrinter();
            res.Accept(pp);
            Assert.AreEqual(teststr, pp.Result);
        } 

        [TestMethod]
        public void If()
        {
            var teststr = @"if (x == 0)    y = 10 else    y = 50";
            var parser = new Parser(new Lexer(teststr).Tokens());
            var res = parser.ParseIf();
            var pp = new PrettyPrinter();
            res.Accept(pp);
            Assert.AreEqual(teststr, pp.Result);
        }

        [TestMethod]
        public void Block()
        {
            var teststr = @"
{
   x = 42
}
";
            var parser = new Parser(new Lexer(teststr).Tokens());
            var res = parser.ParseStatement();
            var pp = new PrettyPrinter();
            res.Accept(pp);
            Assert.AreEqual(teststr, pp.Result);
        }
        
        [TestMethod]
        public void While()
        {
            var teststr = @"while (0 < x)
   {
      x = 10
   }
";
            var parser = new Parser(new Lexer(teststr).Tokens());
            var res = parser.ParseStatement();
            var pp = new PrettyPrinter();
            res.Accept(pp);
            Assert.AreEqual(teststr, pp.Result);
        }

        [TestMethod]
        public void Skip()
        {
            var teststr = @"skip";
            var parser = new Parser(new Lexer(teststr).Tokens());
            var res = parser.ParseStatement();
            var pp = new PrettyPrinter();
            res.Accept(pp);
            Assert.AreEqual(teststr, pp.Result);
        }

    }
}
