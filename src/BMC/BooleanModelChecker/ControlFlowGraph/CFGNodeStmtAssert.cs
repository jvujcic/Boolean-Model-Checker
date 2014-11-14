using System;
using System.Collections.Generic;
using System.Text;
using BDDlib;

using CommonAST = antlr.CommonAST;

namespace BooleanModelChecker.ControlFlowGraph
{
    class CFGNodeStmtAssert : CFGNodeStmtConditional
    {
        
        public CFGNodeStmtAssert(CommonAST StatementAST)
            : base(StatementAST)
        {
        }

        override public string ToString()
        {
            string Text = m_Label + "assert(";
            CommonAST walker = (CommonAST)NodeASTSubTree.getFirstChild();
            Text += HelperFunctions.DeciderToString(walker);
            Text += ");";

            return Text;
        }
    }
}
