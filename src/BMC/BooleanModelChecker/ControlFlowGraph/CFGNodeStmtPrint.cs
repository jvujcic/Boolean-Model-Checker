using System;
using System.Collections.Generic;
using System.Text;

using CommonAST = antlr.CommonAST;

namespace BooleanModelChecker.ControlFlowGraph
{
    class CFGNodeStmtPrint : CFGNodeStatement
    {
        public CFGNodeStmtPrint(CommonAST StatementAST)
            : base(StatementAST)
        {
        }

        public override string ToString()
        {
            string Text = m_Label + "print(";
            CommonAST walker = (CommonAST)NodeASTSubTree.getFirstChild();
            while (walker != null)
            {
                Text += HelperFunctions.ExprToString(walker);
                if (walker.getNextSibling() != null)
                    Text += ",";
                walker = (CommonAST)walker.getNextSibling();
            }
            Text += ");";

            return Text;
        }
    }
}
