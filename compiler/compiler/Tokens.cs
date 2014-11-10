namespace DwarfCompiler
{
    public class Token
    {
        public readonly string Text;
        readonly int From, To;
        public TokenType Type;
        public override string ToString()
        {
            return Type.ToString();
        }
        public Token(TokenType type, string text, int from, int to) { Type = type; Text = text.Substring(from, to - from); From = from; To = to; }
    }
    public enum TokenType
    {
        Semicolon,
        Plus,
        Minus,
        Mul,
        Div,
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
        Print,
        Ident,
        Number,
        EOF
    }
}
