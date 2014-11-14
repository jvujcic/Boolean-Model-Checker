/*This file contains part of partial Class BMC (Boolean model checker)
 * containing The Code Reachability algorithm*/

using BooleanModelChecker.ControlFlowGraph;
using BDDlib;
using System.Collections.Generic;
using System.Text;

namespace BooleanModelChecker
{
    public delegate void debugPrintDelegate(string message);
    public delegate bool debugStopDelegate();

    public partial class BMC
    {
        private BddManager m_BddManager;
        //Prominit u internal nakon debuga ako bude moglo ??
        public BddManager GetBddManager
        {
            get
            {
                return m_BddManager;
            }
        }

      /*  int x;
        private void Continue(object o, System.EventArgs e)
        {
            x = false;
        }*/
       

        private Dictionary<CFGNode, PathEdges> m_PathEdges;
        private Dictionary<CFGNode, Bdd> m_SummaryEdges;

        public void Reachable(debugPrintDelegate debugPrint)
        {
            if (ProgramCFG.CfgNodes.Count <= 1)
            {
                debugPrint("Invalid Program, unable to proceed!\n");
                return;
            }

            m_PathEdges.Clear();
            m_SummaryEdges.Clear();

            BuildTransferFunctions();
            
            LinkedList<CFGNode> workList = new LinkedList<CFGNode>();

            CFGNodeProcedure tempNodeProc;
            CFGNode tempNode;

            Bdd bddLift, bddSummaryEdges, bddNew;
            PathEdges bddSelfLoop, bddJoin, bddPathEdges;
            Bdd [] array = new Bdd[3];

            foreach(CFGNode node in ProgramCFG.CfgNodes)
            {
                m_PathEdges.Add(node, new PathEdges());
                m_SummaryEdges.Add(node, m_BddManager.CreateBddZero());
            }

            
            ProgramCFG.ProcedureNameToNode().TryGetValue("main", out tempNodeProc);
            workList.AddFirst(tempNodeProc.FirstStmtOf);
            m_PathEdges.Remove(tempNodeProc.FirstStmtOf);
            PathEdges firstMain = new PathEdges();
            firstMain.AddPathEdge(new PathEdgesPartition(0,HelperFunctions.BuildIdentityTransfer
                (m_BddManager, tempNodeProc.LocalVariables.VariablesToId, ProgramCFG.GlobalVariables.VariablesToId)),m_BddManager);
            m_PathEdges.Add(tempNodeProc.FirstStmtOf, firstMain);
            
            #region Debug part of the reachability algorithm
            /*
            ///////Debug ////////////
            Dictionary<string, int>.ValueCollection valCollection = tempNodeProc.LocalVariables.VariablesToId.Values;
            foreach (int id in valCollection)
            {
                tempBdd1 = tempBdd;                
                tempBdd = m_BddManager.LogicalOr(tempBdd, m_BddManager.GetBddVariableWithID(id));
                tempBdd1.FreeBdd();
                tempBdd1 = tempBdd;
                tempBdd = m_BddManager.LogicalXnor(tempBdd, m_BddManager.GetBddVariableWithID(id + 2));
                tempBdd1.FreeBdd();
            }

            foreach (int id in ProgramCFG.GlobalVariables.VariablesToId.Values)
            {
                tempBdd1 = tempBdd;
                tempBdd = m_BddManager.LogicalImplies(tempBdd, m_BddManager.GetBddVariableWithID(id));
                tempBdd1.FreeBdd();
                tempBdd1 = tempBdd;
                tempBdd = m_BddManager.LogicalOr(tempBdd, m_BddManager.GetBddVariableWithID(id + 2));
                tempBdd1.FreeBdd();
            }
            array[0] = tempBdd;
            tempBdd = m_BddManager.CreateBddOne();

            foreach (int id in valCollection)
            {
                tempBdd1 = tempBdd;
                tempBdd = m_BddManager.LogicalAnd(tempBdd, m_BddManager.GetBddVariableWithID(id));
                tempBdd1.FreeBdd();
                tempBdd1 = tempBdd;
                tempBdd = m_BddManager.LogicalNor(tempBdd, m_BddManager.GetBddVariableWithID(id + 2));
                tempBdd1.FreeBdd();
            }

            foreach (int id in ProgramCFG.GlobalVariables.VariablesToId.Values)
            {
                tempBdd1 = tempBdd;
                tempBdd = m_BddManager.LogicalImplies(tempBdd, m_BddManager.GetBddVariableWithID(id));
                tempBdd1.FreeBdd();
                tempBdd1 = tempBdd;
                tempBdd = m_BddManager.LogicalXor(tempBdd, m_BddManager.GetBddVariableWithID(id + 2));
                tempBdd1.FreeBdd();
            }
            array[1] = tempBdd;

            //array[2] = Join(array[0], array[1], tempNodeProc.LocalVariables);     
            array[2] = SelfLoop(array[1], tempNodeProc.LocalVariables, tempNodeProc.LocalVariables);
            m_BddManager.ForceGarbageCollection();
            return array;
            ////////////////////////
            */
            #endregion            
            while (workList.Count != 0)
            {                
                tempNode = workList.First.Value;

                debugPrint("Processing Node: " + tempNode.ToString() + ".\n");

                workList.RemoveFirst();
 
                if (tempNode is CFGNodeStmtProcCall)
                {                       
                    CFGNodeStmtProcCall procCall = tempNode as CFGNodeStmtProcCall;
                    m_PathEdges.TryGetValue(tempNode, out bddPathEdges);

                    bddJoin = Join(bddPathEdges, procCall.Transfer, procCall.ProcOf.LocalVariables);
                    bddSelfLoop = SelfLoop(bddJoin, procCall.ProcOf.LocalVariables, (procCall.Succesor as CFGNodeStatement).ProcOf.LocalVariables);
                    Propagate(procCall.Succesor, bddSelfLoop, workList);

                    bddJoin.FreeAllBdd();
                    bddSelfLoop.FreeAllBdd();
                  
                    m_PathEdges.TryGetValue(tempNode, out bddPathEdges);
                    m_SummaryEdges.TryGetValue(tempNode, out bddSummaryEdges);
                    if (bddSummaryEdges != null)
                    {
                        bddJoin = Join(bddPathEdges, bddSummaryEdges, procCall.ProcOf.LocalVariables);
                        Propagate(procCall.ReturnPoint, bddJoin, workList);
                        bddJoin.FreeAllBdd();
                    }                 
                }
                else if (tempNode is CFGNodeProcedure)
                {
                    foreach (CFGNodeStmtSkip node in (tempNode as CFGNodeProcedure).Succesor)
                    {
                        bddLift = Lift(node.previousProcCall, tempNode as CFGNodeProcedure);

                        m_SummaryEdges.TryGetValue(node.previousProcCall, out bddSummaryEdges);
                        m_PathEdges.TryGetValue(node.previousProcCall, out bddPathEdges);

                        if (bddSummaryEdges != null)
                        {
                            bddNew = m_BddManager.LogicalOr(bddLift, bddSummaryEdges);
                            bddLift.FreeBdd();
                        }
                        else
                        {
                            bddNew = bddLift;
                        }

                        if (bddSummaryEdges != bddNew)
                        {
                            m_SummaryEdges.Remove(node.previousProcCall);
                            m_SummaryEdges.Add(node.previousProcCall, bddNew);
                            bddSummaryEdges.FreeBdd();

                            bddJoin = Join(bddPathEdges, bddNew, (node as CFGNodeStmtSkip).previousProcCall.ProcOf.LocalVariables);

                            Propagate(node, bddJoin, workList);
                            bddJoin.FreeAllBdd();
                        }
                    }
                }
                else if (tempNode is CFGNodeStmtConditional)
                {
                    CFGNodeStmtConditional tempNode1 = tempNode as CFGNodeStmtConditional;

                    m_PathEdges.TryGetValue(tempNode, out bddPathEdges);
                    bddJoin = Join(bddPathEdges, tempNode1.TransferTrue, tempNode1.ProcOf.LocalVariables);
                    Propagate(tempNode1.TrueSuccesor, bddJoin, workList);
                    bddJoin.FreeAllBdd();

                    m_PathEdges.TryGetValue(tempNode, out bddPathEdges);
                    bddJoin = Join(bddPathEdges, tempNode1.TransferFalse, tempNode1.ProcOf.LocalVariables);
                    Propagate(tempNode1.FalseSuccesor, bddJoin, workList);
                    bddJoin.FreeAllBdd();
                }
                else
                {
                    if (!(tempNode is CFGNodeError))
                    {
                        m_PathEdges.TryGetValue(tempNode, out bddPathEdges);
                        bddJoin = Join(bddPathEdges, (tempNode as CFGNodeStatement).Transfer, (tempNode as CFGNodeStatement).ProcOf.LocalVariables);
                        Propagate(tempNode.Succesor, bddJoin, workList);
                        bddJoin.FreeAllBdd();
                    }
                }
            }

            m_BddManager.ForceGarbageCollection();
        }

        public Bdd GetCFGPathEdgeByHashCode(int CFGHashCode)
        {
            foreach (CFGNode node in m_PathEdges.Keys)
            {
                if (node.GetHashCode() == CFGHashCode)
                {
                    PathEdges pathEdge;

                    if (m_PathEdges.TryGetValue(node, out pathEdge))
                    {
                        return pathEdge.GetJointPaths(m_BddManager);
                    }
                }
            }

            return m_BddManager.CreateBddZero();
        }

        public Bdd GetCFGSummaryEdgeByHashCode(int CFGHashCode)
        {
            foreach (CFGNode node in m_SummaryEdges.Keys)
            {
                if (node.GetHashCode() == CFGHashCode)
                {
                    Bdd summaryEdge;

                    if (m_SummaryEdges.TryGetValue(node, out summaryEdge))
                    {
                        return summaryEdge;
                    }
                }
            }

            return m_BddManager.CreateBddZero();
        }

        public Bdd GetCFGTransferByHashCode(int CFGHashCode)
        {
            foreach (CFGNode node in ProgramCFG.CfgNodes)
            {
                if (node.GetHashCode() == CFGHashCode && node is CFGNodeStatement)
                {
                    return (node as CFGNodeStatement).Transfer;
                }
            }

            return m_BddManager.CreateBddZero();
        }

        public Bdd GetCFGTransferTrueByHashCode(int CFGHashCode)
        {
            foreach (CFGNode node in ProgramCFG.CfgNodes)
            {
                if (node.GetHashCode() == CFGHashCode && node is CFGNodeStmtConditional)
                {
                    return (node as CFGNodeStmtConditional).TransferTrue;
                }
            }
            return null;
        }

        public Bdd GetCFGTransferFalseByHashCode(int CFGHashCode)
        {
            foreach (CFGNode node in ProgramCFG.CfgNodes)
            {
                if (node.GetHashCode() == CFGHashCode && node is CFGNodeStmtConditional)
                {
                    return (node as CFGNodeStmtConditional).TransferFalse;
                }
            }
            return null;
        }

        public CFGNode GetCFGNodeByHashCode(int CFGHashCode)
        {
            foreach (CFGNode node in ProgramCFG.CfgNodes)
            {
                if (node.GetHashCode() == CFGHashCode)
                {
                    return node;
                }
            }
            return null;
        }

    }
}