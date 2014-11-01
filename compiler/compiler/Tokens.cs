using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DwarfCompiler
{
    internal abstract class Token
    {
        public readonly string Text;
        readonly int From, To;
        internal Token(string text, int from, int to) { Text = text.Substring(from, to - from); From = from; To = to; }
    }
    internal class Number : Token
    {
        readonly Int64 Value;
        public Number(string text, int from, int to)
            : base(text, from, to)
        {
            Value = Int64.Parse(Text);
        }
        public override string ToString()
        {
            return "<num:" + Value + ">";
        }
    }

    internal class Ident : Token
    {
        internal Ident(string text, int from, int to) : base(text, from, to) { }

        public override string ToString()
        {
            return "<ident:" + Text + ">";
        }
    }
    public enum KeywordType
    {
        Plus,
        Minus,
        Mul,
        Sub,
        If,
        Then,
        Else,
        Skip,
        While,
        Lpar,
        Rpar,
        Lbrace,
        Rbrace,
        Lt,
        Leq,
        Gt,
        Geq,
        Eq,
        Assign,
        EOF
    }
    internal class Keyword : Token
    {
        public readonly KeywordType Type;
        public Keyword(KeywordType type, string text, int from, int to) : base(text, from, to) { Type = type; }

        public override string ToString()
        {
            return Type.ToString();
        }
    }
 
}
