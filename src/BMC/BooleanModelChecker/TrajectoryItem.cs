using System;
using System.Collections.Generic;
using System.Text;

using BDDlib;
using BooleanModelChecker.ControlFlowGraph;

namespace BooleanModelChecker
{
    public class TrajectoryItem
    {
        private CFGNode m_Node;
        internal CFGNode Node
        {
            get
            {
                return m_Node;
            }
            set
            {
                m_Node = value;
            }
        }

        private Bdd m_Valuation;
        public Bdd Valuation
        {
            get
            {
                return m_Valuation;
            }
            set
            {
                m_Valuation = value;
            }
        }

        public TrajectoryItem()
        {
            m_Node = null;
            m_Valuation = null;
        }

        internal TrajectoryItem(CFGNode node, Bdd bddVal)
        {
            m_Node = node;
            m_Valuation = bddVal;
        }
    }
}