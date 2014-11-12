using System;
using System.Collections.Generic;

namespace DwarfCompiler
{
    public class Lexer
    {
        private readonly string _text;
        private int _pos = 0;

        public Lexer(string program) { _text = program + "$"; }
        public List<Token> Tokens()
        {
            var tokens = new List<Token>();
            for (var token = _next(); token != null; token = _next())
                tokens.Add(token);
            return tokens;
        }
        private bool _expectSeparator(int offset = 0)
        {
            return !(Char.IsLetterOrDigit(_text[_pos + offset]) ||
                _text[_pos + offset] == '_');
        }

        private bool _expectWord(string what)
        {
            return _text.Substring(_pos).StartsWith(what)
                && _expectSeparator(what.Length);
        }

        private Token _ident()
        {
            int end = _pos;
            if (Char.IsLetter(_text[end]) || _text[end] == '_') end++;
            while (Char.IsLetterOrDigit(_text[end]) || _text[end] == '_') end++;
            if (end != _pos)
            {
                var res = new Token(TokenType.Ident, _text, _pos, end);
                _pos = end;
                return res;
            }
            else return null;
        }

        private Token _keyword(TokenType type, int size)
        {
            var ret = new Token(type, _text, _pos, _pos + size);
            _pos += size;
            return ret;
        }

        private Token _number()
        {
            int start = _pos;
            while (Char.IsDigit(_text[_pos])) _pos++;
            if (_pos != start) return new Token(TokenType.Number, _text, start, _pos);
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
                case '$': return _keyword(TokenType.EOF, 1);
                case ';': return _keyword(TokenType.Semicolon, 1);
                case '-': return _keyword(TokenType.Minus, 1);
                case '+': return _keyword(TokenType.Plus, 1);
                case '(': return _keyword(TokenType.Lpar, 1);
                case ')': return _keyword(TokenType.Rpar, 1);
                case '{': return _keyword(TokenType.Lbrace, 1);
                case '}': return _keyword(TokenType.Rbrace, 1);
                case '<': switch (_text[_pos + 1])
                    {
                        case ' ':
                        case '\t': return _keyword(TokenType.Lt, 1);
                        case '=': return _keyword(TokenType.Leq, 2);
                        default: return null;
                    }
                case '>': switch (_text[_pos + 1])
                    {
                        case ' ':
                        case '\t': return _keyword(TokenType.Gt, 1);
                        case '=': return _keyword(TokenType.Geq, 2);
                        default: return null;
                    }
                case '=': switch (_text[_pos + 1])
                    {
                        case ' ':
                        case '\t': return _keyword(TokenType.Assign, 1);
                        case '=': return _keyword(TokenType.Eq, 2);
                        default: return null;
                    }
                default:
                    if (_expectWord("if"))
                        return _keyword(TokenType.If, 2);
                    if (_expectWord("else"))
                        return _keyword(TokenType.Else, 4);
                    if (_expectWord("skip"))
                        return _keyword(TokenType.Skip, 4);
                    if (_expectWord("while"))
                        return _keyword(TokenType.While, 5);
                    if (_expectWord("print"))
                        return _keyword(TokenType.Print, 5);
                    var num = _number();
                    if (num != null) return num;
                    var ident = _ident();
                    if (ident != null) return ident;
                    return null;
            }
        }
    }
}
