using System;
using System.Collections.Generic;
using System.Text;

using CommonAST = antlr.CommonAST;

namespace BooleanModelChecker.ControlFlowGraph
{
    class CFGNodeStmtSkip : CFGNodeStatement
    {
        CFGNodeStmtProcCall m_previousProcCall;
        public CFGNodeStmtProcCall previousProcCall
        {
            get { return m_previousProcCall; }
        }
        internal void setPreviousProcCall(CFGNodeStmtProcCall procCall)
        {
            m_previousProcCall = procCall;
        }


        public CFGNodeStmtSkip(CommonAST StatementAST)
            : base(StatementAST)
        {
            m_previousProcCall = null;
        }

        public override string ToString()
        {
            return m_Label + "skip;";
        }
    }
}
