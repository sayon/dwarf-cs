namespace DwarfCompiler
{
    public abstract class IVisitor
    {
        public abstract void Visit(Seq node);
        public abstract void Visit(Assignment node);
        public abstract void Visit(Add node);
        public abstract void Visit(Sub node);
        public abstract void Visit(Div node);
        public abstract void Visit(Mul node);
        public abstract void Visit(Less node);
        public abstract void Visit(LessOrEq node);
        public abstract void Visit(Equal node);
        public abstract void Visit(Print node);
        public abstract void Visit(Skip node);
        public abstract void Visit(While node);
        public abstract void Visit(If node);
        public abstract void Visit(Identifier node);
        public abstract void Visit(Number node);
        public abstract void Visit(Block node);
    }
}
