using System;
using System.Collections.Generic;
using System.Text;
using BDDlib;

using CommonAST = antlr.CommonAST;

namespace BooleanModelChecker.ControlFlowGraph
{
    class CFGNodeStmtWhile : CFGNodeStmtConditional
    {       
        public CFGNodeStmtWhile(CommonAST StatementAST)
            : base(StatementAST)
        {
        }

        public override string ToString()
        {
            string Text = m_Label + "while(";
            Text += HelperFunctions.DeciderToString((CommonAST)NodeASTSubTree.getFirstChild()) + ")";

            return Text;
        }
    }
}
