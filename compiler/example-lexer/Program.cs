using System;

namespace example_lexer
{
    class Lexer
    {
        int _pos = 0;
        string _text;
        public Lexer(string text) { _text = text; }
        char getSymbol() { return _text[_pos++]; }
        char peekSymbol() { return _text[_pos]; }
        public bool next()
        {
            while (_pos < _text.Length && _text[_pos] == ' ') _pos++;
            if (_pos >= _text.Length) { Console.WriteLine("End of string reached"); return false; }
            int start = _pos; var s = getSymbol();
            switch (s)
            {
                case '0':
                case '1':
                case '2':
                case '3':
                case '4':
                case '5':
                case '6':
                case '7':
                case '8':
                case '9':
                    getSymbol();
                    while (true)
                        switch (peekSymbol())
                        {
                            case '0':
                            case '1':
                            case '2':
                            case '3':
                            case '4':
                            case '5':
                            case '6':
                            case '7':
                            case '8':
                            case '9':
                                getSymbol();
                                break;
                            default:
                                Console.WriteLine("Number " + _text.Substring(start, _pos - start));
                                return true;
                        }
                case '(': Console.WriteLine("("); return true;
                case ')': Console.WriteLine(")"); return true;
                case '+': Console.WriteLine("Plus"); return true;
                case '-': Console.WriteLine("Minus"); return true;
                case '*': Console.WriteLine("Multiply"); return true;
                case '/': Console.WriteLine("Divide"); return true;
                default:
                    Console.WriteLine("Invalid string");
                    return false;
            }
        }

        public static void Main(string[] args)
        {
            var lexer = new Lexer("1 + 2 ( ) 3 + 4 *");
            while (lexer.next()) ;
            Console.ReadKey();
        }
    }
}
