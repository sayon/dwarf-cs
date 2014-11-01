Hello, stranger. 

Dwarf is a toy language used solely in educational purposes.

Grammar:
statements := statement | statement ";" statements 

statement := "{" statements "}" | assignment | if | while | print
assignment := IDENT "=" expr
if := "if" "(" expr ")" statement "else" statement
while := "while" "(" expr ")" statement
expr := expr0 "<" expr | expr0 "<=" expr  | expr0 "==" expr  | expr0 ">" expr | expr0 ">=" expr | expr0 
expr0 = expr1 "+" expr | expr1 "-" expr | expr1
expr1 := atom "*" expr1 | atom "/" expr1 | atom
atom := "(" expr ")" | NUMBER

{ statement }
statement ; statement

Only integers are supported as a datatype.
