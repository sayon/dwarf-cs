using System;

namespace DwarfCompiler
{
    /*To understand this, take a look inside Grammar.txt*/
    public interface AST { }
    public interface Statement : AST { }
    public interface Expression : AST { }
    public interface Atom : AST { }

    public class Block : Statement
    {
        public readonly Statement InnerStatement;
        public Block(Statement inner) { InnerStatement = inner;  }
    }
    public abstract class BinOp : Expression
    {
        public readonly Expression Left, Right;
        protected BinOp(Expression left, Expression right) { Left = left; Right = right; }
    }
    public sealed class Add : BinOp
    {
        public Add(Expression left, Expression right) : base(left, right) { }
    }
    public sealed class Sub : BinOp
    {
        public Sub(Expression left, Expression right) : base(left, right) { }
    }
    public sealed class Mul : BinOp
    {
        public Mul(Expression left, Expression right) : base(left, right) { }
    }
    public sealed class Div : BinOp
    {
        public Div(Expression left, Expression right) : base(left, right) { }
    }
    public sealed class Less : BinOp
    {
        public Less(Expression left, Expression right) : base(left, right) { }
    }
    public sealed class LessOrEq : BinOp
    {
        public LessOrEq(Expression left, Expression right) : base(left, right) { }
    }
    public sealed class Equal : BinOp
    {
        public Equal(Expression left, Expression right) : base(left, right) { }
    }

    public class Seq : Statement
    {
        public readonly Statement Left, Right;
        public Seq(Statement left, Statement right) { Left = left; Right = right; }
    }
    public class Print : Statement
    {
        public readonly Expression Expr;
        public Print(Expression expr) { Expr = expr; }
    }
    public class Skip : Statement { public static readonly Skip Instance; }
    public class While : Statement
    {
        public readonly Expression Condition; public readonly Statement Body;
        public While(Expression condition, Statement body) { Condition = condition; Body = body; }
    }
    public class If : Statement
    {
        public readonly Expression Condition;
        public readonly Statement Yes, No;
        public If(Expression condition, Statement yes, Statement no) { Condition = condition; Yes = yes; No = no; }
    }
    public class Assignment : Statement
    {
        public readonly Identifier Lhs; public readonly Expression Rhs;
        public Assignment(Identifier lhs, Expression rhs) { Lhs = lhs; Rhs = rhs; }
    }
    public class Identifier : Expression
    {
        public readonly string Name;
        public Identifier(string name)
        {
            Name = name;
        }
    }
    public class Number : Expression
    {
        public readonly Int64 Value;
        public Number(Int64 value) { Value = value; }
    }
}
