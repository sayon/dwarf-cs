using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DwarfCompiler
{   internal class Lexer
    {
        private readonly string _text;
        private int _pos = 0;

        internal Lexer(string program) { _text = program + "$"; }
        internal IEnumerable<Token> Tokens()
        {
            while (true)
            {
                var token = _next();
                if (token == null) yield break;
                yield return token;
            }
        }
        private bool _expectSeparator(int offset = 0)
        {
            return !(Char.IsLetterOrDigit(_text[_pos + offset]) || _text[_pos + offset] == '_');
        }

        private bool _expectWord(string what)
        {
            return _text.Substring(_pos).StartsWith(what) && _expectSeparator(what.Length);
        }

        private Ident _ident()
        {
            int end = _pos;
            if (Char.IsLetter(_text[end]) || _text[end] == '_') end++;
            while (Char.IsLetterOrDigit(_text[end]) || _text[end] == '_') end++;
            if (end != _pos) { var res = new Ident(_text, _pos, end); _pos = end; return res;  }
            else return null;
        }

        private Keyword _keyword(KeywordType type, int size)
        {
            var ret = new Keyword(type, _text, _pos, _pos + size);
            _pos += size;
            return ret;
        }

        private Number _number()
        {
            int start = _pos;
            while (Char.IsDigit(_text[_pos])) _pos++;
            if (_pos != start) return new Number(_text, start, _pos);
            else return null;
        }
        private bool eof() { return _pos >= _text.Length; }
        private Token _next()
        {
            if (eof()) return null;
            
            while (_pos < _text.Length && Char.IsWhiteSpace(_text[_pos])) _pos++;
            
            if (eof()) return null;
            switch (_text[_pos])
            {
                case '$': return _keyword(KeywordType.EOF, 1);
                case '-': return _keyword(KeywordType.Minus, 1);
                case '+': return _keyword(KeywordType.Plus, 1);
                case '(': return _keyword(KeywordType.Lpar, 1);
                case ')': return _keyword(KeywordType.Rpar, 1);
                case '{': return _keyword(KeywordType.Lbrace, 1);
                case '}': return _keyword(KeywordType.Rbrace, 1);
                case '<': switch (_text[_pos + 1])
                    {
                        case ' ':
                        case '\t': return _keyword(KeywordType.Lt, 1);
                        case '=': return _keyword(KeywordType.Leq, 2);
                        default: return null;
                    }
                case '>': switch (_text[_pos + 1])
                    {
                        case ' ':
                        case '\t': return _keyword(KeywordType.Gt, 1);
                        case '=': return _keyword(KeywordType.Geq, 2);
                        default: return null;
                    }
                case '=': switch (_text[_pos + 1])
                    {
                        case ' ':
                        case '\t': return _keyword(KeywordType.Assign, 1);
                        case '=': return _keyword(KeywordType.Eq, 2);
                        default: return null;
                    }
                default:
                    if (_expectWord("if"))
                        return _keyword(KeywordType.If, 2);
                    if (_expectWord("else"))
                        return _keyword(KeywordType.Else, 4);
                    if (_expectWord("while"))
                        return _keyword(KeywordType.While, 5);
                    var num = _number();
                    if (num != null) return num;
                    var ident = _ident();
                    if (ident != null) return ident;
                    return null;
            }
        }
    }
}
