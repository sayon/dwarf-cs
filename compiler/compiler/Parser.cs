using System;
using System.Collections.Generic;
using System.Linq;

namespace DwarfCompiler
{
    public class Parser
    {
        public Parser(IEnumerable<Token> tokens)
        {
            _tokens = tokens.ToList();
        }
        private List<Token> _tokens;
        private int position = 0;
        Token expect(TokenType type)
        {
            if (type == _tokens[position].Type)
                return _tokens[position];
            else return null;
        }

        Token AcceptIf(TokenType type)
        {
            var tok = expect(type);
            if (tok != null) position++;
            return tok;
        }
        bool Able<T>(ref T saveAs, T what) where T : AST
        {
            if (what != null) { saveAs = what; return true; }
            else return false;
        }

        bool Accepted(TokenType type)
        {
            var tok = expect(type);
            if (tok != null) { position++; return true; }
            return false;
        }

        /// <summary>
        /// statement := "{" statements "}" | assignment | if 
        /// | while | print | "skip"
        /// </summary> 
        public Statement ParseStatement()
        {
            Statement st = null;
            if (Accepted(TokenType.Lbrace) &&
                Able(ref st, ParseStatement()) &&
                Accepted(TokenType.Rbrace)) return new Block(st);
            st = ParseAssignment(); if (st != null) return st;
            st = ParseIf(); if (st != null) return st;
            st = ParseWhile(); if (st != null) return st;
            st = ParsePrint(); if (st != null) return st;
            if (Accepted(TokenType.Skip)) return Skip.Instance;
            return null;
        }
        /// <summary>
        /// print := "print" "(" expr ")"
        /// </summary>
        public Statement ParsePrint()
        {
            Expression expr = null;
            if (Accepted(TokenType.Print) &&
                Accepted(TokenType.Lpar) &&
                Able(ref expr, ParseExpression()) &&
                Accepted(TokenType.Rpar))
                return new Print(expr);
            return null;
        }
        public Identifier ParseIdentifier()
        {
            Token ident = AcceptIf(TokenType.Ident);
            if (ident == null) return null;
            return new Identifier(ident.Text);
        }
        /// <summary>
        /// assignment := IDENT "=" expr
        /// </summary>
        public Statement ParseAssignment()
        {
            Expression right = null; Identifier left = null;
            if (Able(ref left, ParseIdentifier()) &&
            Accepted(TokenType.Assign) &&
            Able(ref right, ParseExpression()))
                return new Assignment(left, right);
            return null;
        }

        public Statement ParseIf()
        {
            Expression cond = null;
            Statement yes = null,
                      no = null;
            if (Accepted(TokenType.If) &&
                Accepted(TokenType.Lpar) &&
                Able(ref cond, ParseExpression()) &&
                Accepted(TokenType.Rpar) &&
                Able(ref yes, ParseStatement()) &&
                Accepted(TokenType.Else) &&
                Able(ref no, ParseStatement()))
                return new If(cond, yes, no);
            return null;
        }

        public While ParseWhile()
        {
            Expression condition = null;
            Statement body = null;
            //var ok = true;
            //ok = ok && Accepted(TokenType.While);
            //ok = ok && Accepted(TokenType.Lpar);
            //ok = ok && Able(ref condition, ParseExpression());
            //ok = ok && Accepted(TokenType.Rpar);
            //ok = ok && Able(ref body, ParseStatement());
            //
            if (Accepted(TokenType.While) &&
                Accepted(TokenType.Lpar) &&
                Able(ref condition, ParseExpression()) &&
                Accepted(TokenType.Rpar) &&
                Able(ref body, ParseStatement()))
                return new While(condition, body);
            return null;
        }

        public Number ParseNumber()
        {
            Token num = AcceptIf(TokenType.Number);
            if (num != null) return new Number(Int64.Parse(num.Text));
            else return null;
        }

        /// <summary>
        /// expr := expr0 "<" expr | expr0 "<=" expr | expr0 "==" expr  
        ///       | expr0 ">" expr | expr0 ">=" expr | expr0 
        /// </summary>
        /// <returns></returns>
        public Expression ParseExpression()
        {
            var left = ParseExpression0();
            if (left == null) return null;

            Expression right = null;
            if (Accepted(TokenType.Lt) &&
                Able(ref right, ParseExpression()))
                return new Less(left, right);

            if (Accepted(TokenType.Leq) &&
               Able(ref right, ParseExpression()))
                return new LessOrEq(left, right);

            if (Accepted(TokenType.Gt) &&
                Able(ref right, ParseExpression()))
                return new Less(right, left);

            if (Accepted(TokenType.Geq) &&
                Able(ref right, ParseExpression()))
                return new LessOrEq(right, left);

            if (Accepted(TokenType.Eq) &&
                Able(ref right, ParseExpression()))
                return new Equal(left, right);

            return left;
        }
        /// <summary>
        /// expr0 = expr1 "+" expr | expr1 "-" expr | expr1
        /// </summary>
        public Expression ParseExpression0()
        {
            var left = ParseExpression1();
            Expression right = null;
            if (Accepted(TokenType.Plus) &&
                Able(ref right, ParseExpression0()))
                return new Add(left, right);
            if (Accepted(TokenType.Minus) &&
                Able(ref right, ParseExpression0()))
                return new Sub(left, right);
            return left;
        }
        /// <summary>
        /// expr1 := atom "*" expr1 | atom "/" expr1 | atom
        /// </summary>
        public Expression ParseExpression1()
        {
            var left = ParseAtom();
            Expression right = null;
            if (Accepted(TokenType.Mul) &&
                Able(ref right, ParseExpression1()))
                return new Mul(left, right);
            if (Accepted(TokenType.Div) &&
                Able(ref right, ParseExpression1()))
                return new Div(left, right);
            return left;
        }

        /// <summary>
        /// atom := "(" expr ")" | NUMBER
        /// </summary>
        public Expression ParseAtom()
        {
            Expression insideBraces = null;
            if (Accepted(TokenType.Lbrace) &&
                Able(ref insideBraces, ParseExpression()) &&
                Accepted(TokenType.Rbrace))
                return insideBraces;
            var number = ParseNumber();
            if (number == null) return ParseIdentifier();
            else return number;
        }
        public Skip ParseSkip()
        {
            if (Accepted(TokenType.Skip)) return Skip.Instance;
            return null;
        }

        /// <summary>
        /// statements := statement | statement ";" statements 
        /// </summary>
        public Statement ParseStatements()
        {
            var fst = ParseStatement();
            Statement snd = null;
            if (Accepted(TokenType.Semicolon) &&
                Able(ref snd, ParseStatements()))
                return new Seq(fst, snd);

            return fst;
        }
    }
}
