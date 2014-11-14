using BooleanModelChecker.ControlFlowGraph;
using BDDlib;
using System.Collections.Generic;
using System.Text;

namespace BooleanModelChecker
{
    public partial class BMC
    {
        public Trajectory BuildTrajectory(CFGNode node)
        {
            int len, i;

            Trajectory tempTrajectory = new Trajectory();

            PathEdges paths, predecessorPaths;
            PathEdgesPartition pathPart;

            Bdd pathBdd = null;

            m_PathEdges.TryGetValue(node, out paths);

            if(paths.PathEdgesByLength.Count !=  0)
            {                
                pathPart = paths.PathEdgesByLength[0];
                len = pathPart.Length;

                pathBdd = BuildRandomPath(pathPart.PathEdges);

                while (len > 0)
                {
                    tempTrajectory.Add(new TrajectoryItem(node, pathBdd));

                    #region Node is not first statement of procedure
                    if ((node is CFGNodeProcedure) || ((node is CFGNodeStatement) && (node as CFGNodeStatement).ProcOf.FirstStmtOf != node))
                    {

                        Bdd tempPathEdges, reverseJoin, bddOK;

                        if (((node is CFGNodeStmtSkip) && (node as CFGNodeStmtSkip).previousProcCall != null))
                        {
                            CFGNodeStmtProcCall nodePredecessor = (node as CFGNodeStmtSkip).previousProcCall;
                            Bdd[] bddArray = new Bdd[nodePredecessor.ProcOf.LocalVariables.Length * 4 + ProgramCFG.GlobalVariables.Length * 4];
                            Bdd summaryEdges, summaryEdgesReverse;

                            m_PathEdges.TryGetValue(nodePredecessor as CFGNode, out predecessorPaths);
                            m_SummaryEdges.TryGetValue(nodePredecessor as CFGNode, out summaryEdges);
                            tempPathEdges = predecessorPaths.GetPathEdgesOfLength(len - 1);

                            if (tempPathEdges != null)
                            {
                                i = -1;
                                foreach (int id in nodePredecessor.ProcOf.LocalVariables.VariablesToId.Values)
                                {
                                    bddArray[++i] = m_BddManager.GetBddVariableWithID(id);
                                    bddArray[++i] = m_BddManager.GetBddVariableWithID(id + 2);
                                    bddArray[++i] = m_BddManager.GetBddVariableWithID(id + 2);
                                    bddArray[++i] = m_BddManager.GetBddVariableWithID(id);
                                }
                                foreach (int id in ProgramCFG.GlobalVariables.VariablesToId.Values)
                                {
                                    bddArray[++i] = m_BddManager.GetBddVariableWithID(id);
                                    bddArray[++i] = m_BddManager.GetBddVariableWithID(id + 2);
                                    bddArray[++i] = m_BddManager.GetBddVariableWithID(id + 2);
                                    bddArray[++i] = m_BddManager.GetBddVariableWithID(id);
                                }

                                summaryEdgesReverse = summaryEdges.Replace(bddArray);
                                reverseJoin = Join(pathBdd, summaryEdgesReverse, nodePredecessor.ProcOf.LocalVariables);
                                summaryEdgesReverse.FreeBdd();

                                bddOK = m_BddManager.LogicalAnd(reverseJoin, tempPathEdges);
                                reverseJoin.FreeBdd();

                                pathBdd = BuildRandomPath(bddOK);
                                bddOK.FreeBdd();

                                node = nodePredecessor as CFGNode;
                                len--;
                            }
                        }
                        else
                        {
                            foreach (CFGNode tempNode in node.Predecessor)
                            {                                
                                CFGNodeStatement nodePredecessor = tempNode as CFGNodeStatement;
                                Bdd[] bddArray = new Bdd[nodePredecessor.ProcOf.LocalVariables.Length * 4 + ProgramCFG.GlobalVariables.Length * 4];
                                Bdd transferReverse;

                                m_PathEdges.TryGetValue(tempNode, out predecessorPaths);
                                tempPathEdges = predecessorPaths.GetPathEdgesOfLength(len - 1);

                                if (tempPathEdges != null)
                                {
                                    i = -1;
                                    foreach (int id in nodePredecessor.ProcOf.LocalVariables.VariablesToId.Values)
                                    {
                                        bddArray[++i] = m_BddManager.GetBddVariableWithID(id);
                                        bddArray[++i] = m_BddManager.GetBddVariableWithID(id + 2);
                                        bddArray[++i] = m_BddManager.GetBddVariableWithID(id + 2);
                                        bddArray[++i] = m_BddManager.GetBddVariableWithID(id);
                                    }
                                    foreach (int id in ProgramCFG.GlobalVariables.VariablesToId.Values)
                                    {
                                        bddArray[++i] = m_BddManager.GetBddVariableWithID(id);
                                        bddArray[++i] = m_BddManager.GetBddVariableWithID(id + 2);
                                        bddArray[++i] = m_BddManager.GetBddVariableWithID(id + 2);
                                        bddArray[++i] = m_BddManager.GetBddVariableWithID(id);
                                    }

                                    if (nodePredecessor is CFGNodeStmtConditional)
                                    {
                                        if ((nodePredecessor as CFGNodeStmtConditional).TrueSuccesor == node)
                                        {
                                            transferReverse = (nodePredecessor as CFGNodeStmtConditional).TransferTrue.Replace(bddArray);
                                            reverseJoin = Join(pathBdd, transferReverse, nodePredecessor.ProcOf.LocalVariables);
                                            transferReverse.FreeBdd();
                                        }
                                        else
                                        {
                                            transferReverse = (nodePredecessor as CFGNodeStmtConditional).TransferFalse.Replace(bddArray);
                                            reverseJoin = Join(pathBdd, transferReverse, nodePredecessor.ProcOf.LocalVariables);
                                            transferReverse.FreeBdd();
                                        }
                                    }
                                    else
                                    {
                                        transferReverse = nodePredecessor.Transfer.Replace(bddArray);
                                        reverseJoin = Join(pathBdd, transferReverse, nodePredecessor.ProcOf.LocalVariables);
                                        transferReverse.FreeBdd();
                                    }

                                    bddOK = m_BddManager.LogicalAnd(reverseJoin, tempPathEdges);

                                    if (bddOK.ReturnBddType() != BddType.Zero)
                                    {
                                        pathBdd = BuildRandomPath(bddOK);
                                        bddOK.FreeBdd();
                                        node = tempNode;
                                        len--;
                                        break;
                                    }
                                    else
                                    {
                                        bddOK.FreeBdd();
                                    }                                    
                                }
                            }
                        }
                    } 
                    #endregion
                    #region Node is first statement of procedure
                    else
                    {
                        Bdd[] bddArray = new Bdd[(node as CFGNodeStatement).ProcOf.LocalVariables.Length + ProgramCFG.GlobalVariables.Length];
                        Bdd[] bddArrayForTransfer = new Bdd[(node as CFGNodeStatement).ProcOf.LocalVariables.Length + ProgramCFG.GlobalVariables.Length];
                        Bdd bddExist, tempPathEdges, bddAndWithTransfer, bddAndWithProcCall, bddExistTransfer, bddReplaceTransfer;                        

                        i = -1;

                        foreach (int id in (node as CFGNodeStatement).ProcOf.LocalVariables.VariablesToId.Values)
                        {
                            bddArray[++i] = m_BddManager.GetBddVariableWithID(id);
                            bddArrayForTransfer[i] = m_BddManager.GetBddVariableWithID(id + 2);
                            
                        }
                        foreach (int id in ProgramCFG.GlobalVariables.VariablesToId.Values)
                        {
                            bddArray[++i] = m_BddManager.GetBddVariableWithID(id);
                            bddArrayForTransfer[i] = m_BddManager.GetBddVariableWithID(id + 2);
                        }

                        bddExist = pathBdd.Exists(bddArray);


                        foreach (CFGNodeStmtProcCall nodePredecessor in node.Predecessor)
                        {
                            m_PathEdges.TryGetValue(nodePredecessor, out predecessorPaths);
                            tempPathEdges = predecessorPaths.GetPathEdgesOfLength(len - 1);
                            Bdd[] bddArrayReplace = new Bdd[nodePredecessor.ProcOf.LocalVariables.Length * 2 + ProgramCFG.GlobalVariables.Length * 2];

                            if (tempPathEdges != null)
                            {
                                bddAndWithTransfer = m_BddManager.LogicalAnd(nodePredecessor.Transfer, bddExist);
                                bddExistTransfer = bddAndWithTransfer.Exists(bddArrayForTransfer);
                                bddAndWithTransfer.FreeBdd();

                                i = -1;
                                foreach(int id in nodePredecessor.ProcOf.LocalVariables.VariablesToId.Values)
                                {
                                    bddArrayReplace[++i] = m_BddManager.GetBddVariableWithID(id);
                                    bddArrayReplace[++i] = m_BddManager.GetBddVariableWithID(id + 2);                                   
                                }
                                foreach (int id in ProgramCFG.GlobalVariables.VariablesToId.Values)
                                {
                                    bddArrayReplace[++i] = m_BddManager.GetBddVariableWithID(id);
                                    bddArrayReplace[++i] = m_BddManager.GetBddVariableWithID(id + 2);
                                }

                                bddReplaceTransfer = bddExistTransfer.Replace(bddArrayReplace);
                                bddExistTransfer.FreeBdd();

                                bddAndWithProcCall = m_BddManager.LogicalAnd(bddReplaceTransfer, tempPathEdges);
                                bddReplaceTransfer.FreeBdd();

                                if (bddAndWithProcCall.GetBddRootVariable().ReturnBddType() != BddType.Zero)
                                {
                                    pathBdd = BuildRandomPath(bddAndWithProcCall);
                                    bddAndWithProcCall.FreeBdd();
                                    node = nodePredecessor as CFGNode;
                                    len--;
                                    break;
                                }
                                else
                                {
                                    bddAndWithProcCall.FreeBdd();
                                }
                            }
                        }
                    } 
                    #endregion
                }
            }
            tempTrajectory.Add(new TrajectoryItem(node, pathBdd));
            tempTrajectory.TrajectoryList.Reverse();
            return tempTrajectory;
        }
    }
}