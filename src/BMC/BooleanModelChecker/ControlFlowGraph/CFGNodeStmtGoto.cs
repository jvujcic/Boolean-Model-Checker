using System;
using System.Collections.Generic;
using System.Text;

using CommonAST = antlr.CommonAST;

namespace BooleanModelChecker.ControlFlowGraph
{
    class CFGNodeStmtGoto : CFGNodeStatement
    {
        public CFGNodeStmtGoto(CommonAST StatementAST)
            : base(StatementAST)
        {
        }

        public override string ToString()
        {
            string Text = m_Label + "goto ";
            Text += NodeASTSubTree.getFirstChild().getText() + ";";

            return Text;
        }
    }
}
