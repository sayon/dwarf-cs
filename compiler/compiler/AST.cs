using System;

namespace DwarfCompiler
{
    /*To understand this, take a look inside Grammar.txt*/
    public abstract class AST { public abstract void Accept(IVisitor visitor); }
    public abstract class Statement : AST { }
    public abstract class Expression : AST {   } 
    public class Block : Statement
    {
        public readonly Statement InnerStatement;
        public Block(Statement inner) { InnerStatement = inner;  }

        public override void Accept(IVisitor visitor) { visitor.Visit(this); }
    }
    public abstract class BinOp : Expression
    {
        public readonly Expression Left, Right;
        protected BinOp(Expression left, Expression right) { Left = left; Right = right; }
        public abstract override void Accept(IVisitor visitor);
    }
    public sealed class Add : BinOp
    {
        public Add(Expression left, Expression right) : base(left, right) { }
        public override void Accept(IVisitor visitor) { visitor.Visit(this); }
    }
    public sealed class Sub : BinOp
    {
        public Sub(Expression left, Expression right) : base(left, right) { }
        public override void Accept(IVisitor visitor) { visitor.Visit(this); }
    }
    public sealed class Mul : BinOp
    {
        public Mul(Expression left, Expression right) : base(left, right) { }
        public override void Accept(IVisitor visitor) { visitor.Visit(this); }
    }
    public sealed class Div : BinOp
    {
        public Div(Expression left, Expression right) : base(left, right) { }
        public override void Accept(IVisitor visitor) { visitor.Visit(this); }
    }
    public sealed class Less : BinOp
    {
        public Less(Expression left, Expression right) : base(left, right) { }
        public override void Accept(IVisitor visitor) { visitor.Visit(this); }
    }
    public sealed class LessOrEq : BinOp
    {
        public LessOrEq(Expression left, Expression right) : base(left, right) { }
        public override void Accept(IVisitor visitor) { visitor.Visit(this); }
    }
    public sealed class Equal : BinOp
    {
        public Equal(Expression left, Expression right) : base(left, right) { }
        public override void Accept(IVisitor visitor) { visitor.Visit(this); }
    }

    public class Seq : Statement
    {
        public readonly Statement Left, Right;
        public Seq(Statement left, Statement right) { Left = left; Right = right; }
        public override void Accept(IVisitor visitor) { visitor.Visit(this); }
    }
    public class Print : Statement
    {
        public readonly Expression Expr;
        public Print(Expression expr) { Expr = expr; }
        public override void Accept(IVisitor visitor) { visitor.Visit(this); }
    }
    public class Skip : Statement { 
        public static readonly Skip Instance = new Skip();
        public override void Accept(IVisitor visitor) { visitor.Visit(this); }
    }
    public class While : Statement
    {
        public readonly Expression Condition; public readonly Statement Body;
        public While(Expression condition, Statement body) { Condition = condition; Body = body; }
        public override void Accept(IVisitor visitor) { visitor.Visit(this); }
    }
    public class If : Statement
    {
        public readonly Expression Condition;
        public readonly Statement Yes, No;
        public If(Expression condition, Statement yes, Statement no) { Condition = condition; Yes = yes; No = no; }
        public override void Accept(IVisitor visitor) { visitor.Visit(this); }
    }
    public class Assignment : Statement
    {
        public readonly Identifier Lhs; public readonly Expression Rhs;
        public Assignment(Identifier lhs, Expression rhs) { Lhs = lhs; Rhs = rhs; }
        public override void Accept(IVisitor visitor) { visitor.Visit(this); }
    }
    public class Identifier : Expression
    {
        public readonly string Name;
        public Identifier(string name)
        {
            Name = name;
        }
        public override void Accept(IVisitor visitor) { visitor.Visit(this); }
    }
    public class Number : Expression
    {
        public readonly Int64 Value;
        public Number(Int64 value) { Value = value; }
        public override void Accept(IVisitor visitor) { visitor.Visit(this); }
    }
}
