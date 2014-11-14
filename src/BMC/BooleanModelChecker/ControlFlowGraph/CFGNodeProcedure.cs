using System;
using System.Collections.Generic;
using System.Text;

using CommonAST = antlr.CommonAST;

namespace BooleanModelChecker.ControlFlowGraph
{
    class CFGNodeProcedure : CFGNode
    {
        private List<CFGNode> m_Succesor;
        internal List<CFGNode> Succesor
        {
            get
            {
                return m_Succesor;
            }
        }
        
        Variables m_LocalVariables;        
        internal Variables LocalVariables
        {
            get { return m_LocalVariables; }
            set { m_LocalVariables = value; }
        }

        Variables m_FormalParameters;
        internal Variables FormalParameters
        {
            get { return m_FormalParameters; }
            set { m_FormalParameters = value; }
        }

        private CFGNode m_FirstStmtOf;
        public CFGNode FirstStmtOf
        {
            get{return m_FirstStmtOf;}
            set{m_FirstStmtOf = value;}
        }


        public CFGNodeProcedure(CommonAST StatementAST)
        {
            NodeASTSubTree = StatementAST;
            m_LocalVariables = new Variables();
            m_FormalParameters = new Variables();
            m_Succesor = new List<CFGNode>();
        }

        internal override void AddSuccesor(CFGNode SuccNode)
        {
            m_Succesor.Add(SuccNode);
            SuccNode.AddPredecessor(this);
        }

        override public string ToString()
        {
            string StatementString = "Exit of Proc ''"+NodeASTSubTree.getText()+"''";

            return StatementString;
        }

    }
}
