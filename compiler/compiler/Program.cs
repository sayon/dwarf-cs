using System;
using System.Collections.Generic;
using System.Linq;
using System.Text; 

namespace DwarfCompiler
{
    class Program
    {
        static void Main(string[] args)
        {
            foreach(var tok in (new Lexer("if while 123 < <= - + == ( ) { } identifier")).Tokens()) 
                Console.WriteLine(tok);
            
        }
    }
}
