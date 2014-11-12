using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text; 

namespace DwarfCompiler
{
    class Program
    {
        static void Main(string[] args)
        {
            var program = @"
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
            var tkns = new Lexer(program).Tokens();
            var tree = new Parser(tkns).ParseStatements(); 
            //File.WriteAllLines("tokens.txt", tkns.Select(t => t.ToString()));
            var pp = new PrettyPrinter();
            tree.Accept(pp);
            File.WriteAllText("prettyprinted.txt", pp.Result);
        }
    }
}
