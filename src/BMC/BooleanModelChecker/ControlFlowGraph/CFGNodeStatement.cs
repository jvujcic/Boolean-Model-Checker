using System;
using System.Collections.Generic;
using System.Text;
using BDDlib;

using antlr;

namespace BooleanModelChecker.ControlFlowGraph
{
    class CFGNodeStatement : CFGNode
    {
        protected Bdd m_Transfer;
        internal Bdd Transfer
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
        public CFGNodeStatement() : base()
        {
            m_Transfer = null;
        }
        private CFGNode m_Next;
        public CFGNode Next
        {
            get { return m_Next; }
            set { m_Next = value; }
        }

        private CFGNodeProcedure m_ProcOf;
        public CFGNodeProcedure ProcOf
        {
            get { return m_ProcOf; }
            set { if (m_ProcOf==null) m_ProcOf = value; }
        }

        public CFGNodeStatement(CommonAST StatementAST)
        {
            NodeASTSubTree = StatementAST;
            m_ProcOf = null;
        }

        override public string ToString()
        {
            string StatementString = NodeASTSubTree.getText();

            CommonAST ASTChild = (CommonAST)NodeASTSubTree.getFirstChild();

            while (ASTChild != null)
            {
                StatementString += " " + ASTChild.getText();

                ASTChild = (CommonAST)ASTChild.getNextSibling();
            }

            return StatementString;
        }


    }
}
