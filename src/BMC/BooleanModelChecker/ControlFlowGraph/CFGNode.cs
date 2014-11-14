using System;
using System.Collections.Generic;
using System.Text;

using CommonAST = antlr.CommonAST;


namespace BooleanModelChecker.ControlFlowGraph
{
    public abstract class CFGNode
    {
        private static int CFGNodeHashIndex = 0;

        private int HashCode;

        protected int StartSourceLine;
        protected int StartSourceColumn;
        protected int EndingSourceLine;
        protected int EndingSourceColumn;

        // protected List<CFGNode> Succesors;
        private CFGNode m_Succesor;     
        public CFGNode Succesor
        {
            get
            {
                return m_Succesor;
            }
        }

        private List<CFGNode> m_Predecessor;
        internal List<CFGNode> Predecessor
        {
            get
            {
                return m_Predecessor;
            }
        }

        protected CommonAST NodeASTSubTree;
        public CommonAST GetAST() { return NodeASTSubTree; }

        protected string m_Label;
        internal void setLabel(string Label)
        {
            m_Label = Label+" : "; 
        }

        public CFGNode()
        {
            m_Label = "";

            HashCode = CFGNodeHashIndex++;
            m_Predecessor = new List<CFGNode>();
        }

        internal virtual void AddSuccesor(CFGNode SuccNode)
        {
            //Succesors.Add(SuccNode);
            m_Succesor = SuccNode;
            SuccNode.AddPredecessor(this);
        }

        internal void AddPredecessor(CFGNode PreNode)
        {
            m_Predecessor.Add(PreNode);
        }

        override public int GetHashCode()
        { return HashCode; }

        virtual public string ToString()
        {
            return NodeASTSubTree.getText();
        }
       
    }
}
