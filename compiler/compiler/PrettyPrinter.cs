using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DwarfCompiler
{
    public sealed class PrettyPrinter : IVisitor
    {
        private StringBuilder _result = new StringBuilder();
        private int _indentLevel = 0;
        private const int SPACES_PER_INDENT = 3;
        private void _moreIndent() { _indentLevel++; }
        private void _lessIndent() { _indentLevel--; }
        private void _indent() { _result.Append(new String(' ', _indentLevel * SPACES_PER_INDENT)); }
        public string Result { get { return _result.ToString(); } }
        public override void Visit(Seq node)
        {
            node.Left.Accept(this);
            _result.Append(";").AppendLine();
            node.Right.Accept(this);
        }

        public override void Visit(Assignment node)
        {
            _indent();
            node.Lhs.Accept(this);
            _result.Append(" = ");
            node.Rhs.Accept(this);
        }

        public override void Visit(Add node)
        {
            node.Left.Accept(this);
            _result.Append(" + ");
            node.Right.Accept(this);
        }

        public override void Visit(Sub node)
        {
            node.Left.Accept(this);
            _result.Append(" - ");
            node.Right.Accept(this);
        }

        public override void Visit(Div node)
        {
            node.Left.Accept(this);
            _result.Append(" / ");
            node.Right.Accept(this);
        }
        public override void Visit(Mul node)
        {
            node.Left.Accept(this);
            _result.Append(" * ");
            node.Right.Accept(this);
        }

        public override void Visit(Less node)
        {
            node.Left.Accept(this);
            _result.Append(" < ");
            node.Right.Accept(this);
        }

        public override void Visit(LessOrEq node)
        {
            node.Left.Accept(this);
            _result.Append(" <= ");
            node.Right.Accept(this);
        }

        public override void Visit(Equal node)
        {
            node.Left.Accept(this);
            _result.Append(" == ");
            node.Right.Accept(this);
        }

        public override void Visit(Print node)
        {
            _indent();
            _result.Append("print (");
            node.Expr.Accept(this);
            _result.Append(")");
        }

        public override void Visit(Skip node)
        {
            _indent();
            _result.Append("skip");
        }

        public override void Visit(While node)
        {
            _indent();
            _result.Append("while (");
            node.Condition.Accept(this);
            _result.Append(")");
            _moreIndent();
            node.Body.Accept(this);
            _lessIndent();
        }

        public override void Visit(If node)
        {
            _indent();
            _result.Append("if (");
            node.Condition.Accept(this);
            _result.Append(") ");
            _moreIndent();
            node.Yes.Accept(this);
            _lessIndent(); 
            _indent();
            _result.Append(" else ");
            _moreIndent();
            node.No.Accept(this);
            _lessIndent();
        }

        public override void Visit(Identifier node)
        {
            _result.Append(node.Name);
        }

        public override void Visit(Number node)
        {
            _result.Append(node.Value.ToString());
        }

        public override void Visit(Block node)
        {
            _result.AppendLine();
            _indent();
            _result.AppendLine("{");
            _moreIndent();
            node.InnerStatement.Accept(this);
            _result.AppendLine();
            _lessIndent();
            _indent();
            _result.Append("}");
            _result.AppendLine();
        }
    }
}
