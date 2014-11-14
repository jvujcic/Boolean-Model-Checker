using System;
using System.Collections.Generic;
using System.Text;

using CommonAST = antlr.CommonAST;

namespace BooleanModelChecker.ControlFlowGraph
{
    class CFGNodeStmtReturn : CFGNodeStatement
    {
        public CFGNodeStmtReturn(CommonAST StatementAST)
            : base(StatementAST)
        {
        }

        public override string ToString()
        {
            return m_Label + "return;";
        }
    }
}
