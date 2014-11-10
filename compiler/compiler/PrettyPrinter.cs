using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DwarfCompiler
{
    public sealed class PrettyPrinter : AbstractInterpreter
    {
        private StringBuilder _result = new StringBuilder();
        private int _indentLevel = 0;
        private const int SPACES_PER_INDENT = 3;
        private void _moreIndent() { _indentLevel++; }
        private void _lessIndent() { _indentLevel--; }
        private void _indent() { _result.Append(new String(' ', _indentLevel * SPACES_PER_INDENT)); }
        public string Result { get { return _result.ToString(); } }
        public override void VisitSeq(Seq node)
        {
            Visit(node.Left);
            _result.Append(";").AppendLine();
            Visit(node.Right);
        }

        public override void VisitAssignment(Assignment node)
        {
            _indent();
            Visit(node.Lhs);
            _result.Append(" = ");
            Visit(node.Rhs);
        }

        public override void VisitAdd(Add node)
        {
            Visit(node.Left);
            _result.Append(" + ");
            Visit(node.Right);
        }

        public override void VisitSub(Sub node)
        {
            Visit(node.Left);
            _result.Append(" - ");
            Visit(node.Right);
        }

        public override void VisitDiv(Div node)
        {
            Visit(node.Left);
            _result.Append(" / ");
            Visit(node.Right);
        }

        public override void VisitLess(Less node)
        {
            Visit(node.Left);
            _result.Append(" < ");
            Visit(node.Right);
        }

        public override void VisitLessOrEq(LessOrEq node)
        {
            Visit(node.Left);
            _result.Append(" <= ");
            Visit(node.Right);
        }

        public override void VisitEqual(Equal node)
        {
            Visit(node.Left);
            _result.Append(" == ");
            Visit(node.Right);
        }

        public override void VisitPrint(Print node)
        {
            _indent();
            _result.Append("print (");
            Visit(node.Expr);
            _result.Append(")");
        }

        public override void VisitSkip(Skip node)
        {
            _indent();
            _result.Append("skip");
        }

        public override void VisitWhile(While node)
        {
            _indent();
            _result.Append("while (");
            Visit(node.Condition);
            _result.Append(")");
            _moreIndent();
            Visit(node.Body);
            _lessIndent();
        }

        public override void VisitIf(If node)
        {
            _indent();
            _result.Append("if (");
            Visit(node.Condition);
            _result.AppendLine(")");
            _moreIndent();
            Visit(node.Yes);
            _lessIndent();
            _result.AppendLine();
            _indent();
            _result.AppendLine("else");
            _moreIndent();
            Visit(node.No);
            _lessIndent();
        }

        public override void VisitIdentifier(Identifier node)
        {
            _result.Append(node.Name);
        }

        public override void VisitNumber(Number node)
        {
            _result.Append(node.Value.ToString());
        }

        public override void VisitBlock(Block node)
        {
            _result.AppendLine();
            _indent();
            _result.AppendLine("{");
            _moreIndent();
            Visit(node.InnerStatement);
            _result.AppendLine();
            _lessIndent();
            _indent();
            _result.Append("}");
            _result.AppendLine();
        }
    }
}
