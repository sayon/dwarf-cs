using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DwarfCompiler
{
    public abstract class AbstractInterpreter
    {
        public void Visit(AST node) {
            if (node is Seq) VisitSeq(node as Seq);
            else if (node is Assignment) VisitAssignment(node as Assignment);
            else if (node is Add) VisitAdd(node as Add);
            else if (node is Sub) VisitSub(node as Sub);
            else if (node is Div) VisitDiv(node as Div);
            else if (node is Less) VisitLess(node as Less);
            else if (node is LessOrEq) VisitLessOrEq(node as LessOrEq);
            else if (node is Equal) VisitEqual(node as Equal);
            else if (node is Print) VisitPrint(node as Print);
            else if (node is Skip) VisitSkip(node as Skip);
            else if (node is While) VisitWhile(node as While);
            else if (node is If) VisitIf(node as If);
            else if (node is Identifier) VisitIdentifier(node as Identifier);
            else if (node is Number) VisitNumber(node as Number);
            else if (node is Block) VisitBlock(node as Block);
        }
        public abstract void VisitSeq(Seq node);
        public abstract void VisitAssignment(Assignment node);
        public abstract void VisitAdd(Add node);
        public abstract void VisitSub(Sub node);
        public abstract void VisitDiv(Div node);
        public abstract void VisitLess(Less node);
        public abstract void VisitLessOrEq(LessOrEq node);
        public abstract void VisitEqual(Equal node);
        public abstract void VisitPrint(Print node);

        public abstract void VisitSkip(Skip node);
        public abstract void VisitWhile(While node);
        public abstract void VisitIf(If node);

        public abstract void VisitIdentifier(Identifier node);
        public abstract void VisitNumber(Number node);
        public abstract void VisitBlock(Block node);
    }
}
