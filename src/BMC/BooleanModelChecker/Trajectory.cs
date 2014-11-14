using System;
using System.Collections.Generic;
using System.Text;

using BDDlib;
using BooleanModelChecker.ControlFlowGraph;

namespace BooleanModelChecker
{
    public class Trajectory
    {
        private List<TrajectoryItem> m_TrajectoryList;
        public List<TrajectoryItem> TrajectoryList
        {
            get
            {
                return m_TrajectoryList;
            }
        }

        public Trajectory()
        {
            m_TrajectoryList = new List<TrajectoryItem>();
        }

        internal void Add(TrajectoryItem newItem)
        {
            m_TrajectoryList.Add(newItem);
        }

        public static string getTrajectoryPrintout
            (Trajectory printTrajectory, Dictionary<CFGNode, int> CFGNodeToLineNum, List<string> BddVariableToName)
        {
            string trajPrintout = string.Empty;

            for (int i = 0; i < printTrajectory.TrajectoryList.Count; i++)
            {
                TrajectoryItem trajItem = printTrajectory.TrajectoryList[i];

                int lineNum;
                if (CFGNodeToLineNum.TryGetValue(trajItem.Node, out lineNum))
                {
                    trajPrintout += "Line " + lineNum.ToString();
                    for (int j = 0; j < (5 - lineNum.ToString().Length); j++)
                    {
                        trajPrintout += " ";
                    }

                    trajPrintout += BMC.printBddValuation(trajItem.Valuation,BddVariableToName);

                    trajPrintout += "\n";
                }
            }

            return trajPrintout;
        }
    }
}