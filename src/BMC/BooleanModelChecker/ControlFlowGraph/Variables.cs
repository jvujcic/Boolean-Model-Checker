using System;
using System.Collections.Generic;
using System.Text;
using BDDlib;

namespace BooleanModelChecker.ControlFlowGraph
{
    public class Variables
    {
        private Dictionary<string, int> m_VariablesToId;
        internal Dictionary<string, int> VariablesToId
        {
            get { return m_VariablesToId; }
        }

        internal int Length
        {
            get 
            {
                return m_VariablesToId.Count;
            }
        }

        public Variables()
        {
            m_VariablesToId = new Dictionary<string, int>();
        }

        public void Clear()
        {
            m_VariablesToId.Clear();
        }

        public int AddVar(string varName, BddManager manager, List<string> bddToName)
        {
            int varID = manager.CreateBddVariableLast().GetBddRootVariableID();
            m_VariablesToId.Add(varName, varID);
            bddToName.Add(varName);

            manager.CreateBddVariableLast();
            bddToName.Add(varName + "''");

            manager.CreateBddVariableLast();
            bddToName.Add(varName + "'");

            return varID;
        }

        public int AddVar(string varName, int varID)
        {
            m_VariablesToId.Add(varName, varID);

            return varID;
        }
    }
}
