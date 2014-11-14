using System;
using System.Collections.Generic;
using System.Text;

using BDDlib;

using CommonAST = antlr.CommonAST;

namespace BooleanModelChecker.ControlFlowGraph
{
    class CFGNodeStmtConditional : CFGNodeStatement
    {
        private Bdd m_Transfer2;

        internal Bdd TransferTrue
        {
            get
            {
                return m_Transfer;
            }
            set
            {
                m_Transfer = value;
            }
        }

        internal Bdd TransferFalse
        {
            get
            {
                return m_Transfer2;
            }
            set
            {
                m_Transfer2 = value;
            }
        }

        CFGNode m_TrueSuccesor;
        public CFGNode TrueSuccesor
        {
            get { return m_TrueSuccesor; }
            set
            {
                m_TrueSuccesor = value;
                value.AddPredecessor(this);
            }
        }
        CFGNode m_FalseSuccesor;
        public CFGNode FalseSuccesor
        {
            get { return m_FalseSuccesor; }
            set
            {
                m_FalseSuccesor = value;
                value.AddPredecessor(this);
            }
        }

        public CFGNodeStmtConditional(CommonAST StatementAST)
            : base(StatementAST)
        {
        }

    }
}
