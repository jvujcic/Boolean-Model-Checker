/*This file contains sub-functions that are part of the Reachability algorithm*/

using BooleanModelChecker.ControlFlowGraph;
using BDDlib;
using System.Collections.Generic;
using antlr;

namespace BooleanModelChecker
{
    public partial class BMC
    {
/*        private void Propagate(CFGNode node, Bdd pathEdge, LinkedList<CFGNode> workList)
        {
            Bdd tempBdd, newBdd;
            m_PathEdges.TryGetValue(node, out tempBdd);

            if(pathEdge != null)
            {
                newBdd = m_BddManager.LogicalOr(tempBdd, pathEdge);
                if (tempBdd != newBdd)
                {
                    m_PathEdges.Remove(node);
                    m_PathEdges.Add(node, newBdd);
                    workList.AddLast(node);
                    tempBdd.FreeBdd();
                }
                
            }
        }
*/
        private Bdd Join(Bdd bdd1, Bdd bdd2, Variables localVariables)
        {
            int len = localVariables.Length + ProgramCFG.GlobalVariables.Length;
            int i = 0, j = 0;
            Bdd tempBdd, tempBdd1;

            Dictionary<string, int>.ValueCollection localBddID = localVariables.VariablesToId.Values;
            Dictionary<string, int>.ValueCollection globalBddId = ProgramCFG.GlobalVariables.VariablesToId.Values;


            Bdd[] bddArray = new Bdd[len];
            Bdd[] bddArray1 = new Bdd[2 * len];
            Bdd[] bddArray2 = new Bdd[2 * len];

            if (bdd1 == null || bdd2 == null)
            {
                return m_BddManager.CreateBddZero();
            }

            foreach (int id in localBddID)
            {
                
                bddArray1[i] = m_BddManager.GetBddVariableWithID(id + 2);
                bddArray2[i] = m_BddManager.GetBddVariableWithID(id);

                bddArray[j] = bddArray2[i + 1] = bddArray1[i + 1] = m_BddManager.GetBddVariableWithID(id + 1);
                
                i += 2;
                j++;
            }
            
            foreach(int id in globalBddId)
            {
                bddArray1[i] = m_BddManager.GetBddVariableWithID(id + 2);
                bddArray2[i] = m_BddManager.GetBddVariableWithID(id);

                bddArray[j] = bddArray2[i + 1] = bddArray1[i + 1] = m_BddManager.GetBddVariableWithID(id + 1);

                i += 2;
                j++;
            }

            bdd1 = bdd1.Replace(bddArray1);
            bdd2 = bdd2.Replace(bddArray2);            

            tempBdd1 = m_BddManager.LogicalAnd(bdd1, bdd2);

            tempBdd = tempBdd1.Exists(bddArray);

            tempBdd1.FreeBdd();
            bdd1.FreeBdd();
            bdd2.FreeBdd();

            return tempBdd;            
        }

        private Bdd SelfLoop(Bdd f, Variables localVariables1, Variables localVariables2)
        {
            int i = 0;

            Bdd tempBdd, tempBdd1;

            Bdd[] bddArrayExists = new Bdd[localVariables1.Length + ProgramCFG.GlobalVariables.Length];            
            
            foreach (int id in localVariables1.VariablesToId.Values)
            {
                bddArrayExists[i] = m_BddManager.GetBddVariableWithID(id);
                i++;
            }

            foreach (int id in ProgramCFG.GlobalVariables.VariablesToId.Values)
            {
                bddArrayExists[i] = m_BddManager.GetBddVariableWithID(id);
                i++;
            }
              
            tempBdd1 = HelperFunctions.BuildIdentityTransfer(m_BddManager, localVariables2.VariablesToId, ProgramCFG.GlobalVariables.VariablesToId);

            tempBdd = f.Exists(bddArrayExists);

            f = m_BddManager.LogicalAnd(tempBdd1, tempBdd);
            //f = m_BddManager.LogicalAnd(f, tempBdd);
            tempBdd.FreeBdd();
            
            return f;
        }

        private Bdd Lift(CFGNodeStmtProcCall procCallNode, CFGNodeProcedure exitNode)
        {
            int i = 0;
            int id;
            Bdd[] bddArrayFormalsNot = new Bdd[2 * exitNode.LocalVariables.Length - exitNode.FormalParameters.Length];
            //Bdd[] bddArrayFormals = new Bdd[2 * exitNode.FormalParameters.Length];
            Bdd[] bddArrayForSubstitution = new Bdd[2 * exitNode.FormalParameters.Length];
            Bdd tempBdd, tempBddAnd, tempBddXnor, tempBddReplace;
            Bdd var, exp, identity;
            CommonAST walkerVar = exitNode.GetAST().getFirstChild() as CommonAST;
            CommonAST walkerExp = procCallNode.GetAST().getFirstChild().getFirstChild() as CommonAST;

            foreach (string varName in exitNode.LocalVariables.VariablesToId.Keys)
            {
                exitNode.LocalVariables.VariablesToId.TryGetValue(varName, out id);
                if (exitNode.FormalParameters.VariablesToId.ContainsKey(varName) == false)
                {
                    bddArrayFormalsNot[i++] = m_BddManager.GetBddVariableWithID(id);
                    bddArrayFormalsNot[i++] = m_BddManager.GetBddVariableWithID(id + 2);
                }
                else
                {
                    bddArrayFormalsNot[i++] = m_BddManager.GetBddVariableWithID(id + 2);
                }
            }

            PathEdges tempPath;
            m_PathEdges.TryGetValue(exitNode, out tempPath);
            tempBdd = tempPath.GetJointPaths(m_BddManager);

            tempBdd = tempBdd.Exists(bddArrayFormalsNot);

            i = 0;

            while (walkerVar.Type == BoolParserTokenTypes.ID)
            {   
                exitNode.LocalVariables.VariablesToId.TryGetValue(walkerVar.getText(), out id);
                exp = HelperFunctions.ExprToBdd(walkerExp, m_BddManager, procCallNode.ProcOf.LocalVariables.VariablesToId, ProgramCFG.GlobalVariables.VariablesToId);
                var = m_BddManager.GetBddVariableWithID(id);

                bddArrayForSubstitution[i++] = var;
                bddArrayForSubstitution[i++] = exp;
                /*
                tempBddXnor = m_BddManager.LogicalXnor(exp, var);
                tempBddAnd = m_BddManager.LogicalAnd(tempBddXnor, tempBdd);

                tempBdd.FreeBdd();                
                exp.FreeBdd();
                tempBdd = tempBddAnd;
                */
                walkerExp = walkerExp.getNextSibling() as CommonAST;
                walkerVar = walkerVar.getNextSibling() as CommonAST;
            }

            i = 0;
            /*
            foreach (int bddId in exitNode.FormalParameters.VariablesToId.Values)
            {
                bddArrayFormals[i++] = m_BddManager.GetBddVariableWithID(bddId);
                bddArrayFormals[i++] = m_BddManager.GetBddVariableWithID(bddId + 2);
            }*/

            tempBddReplace = tempBdd.Replace(bddArrayForSubstitution);

            tempBdd.FreeBdd();
            tempBdd = tempBddReplace;

            identity = HelperFunctions.BuildIdentityTransfer(m_BddManager, procCallNode.ProcOf.LocalVariables.VariablesToId, null);

            tempBddAnd = m_BddManager.LogicalAnd(tempBdd, identity);

            tempBdd.FreeBdd();
            identity.FreeBdd();

            tempBdd = tempBddAnd;

            return tempBdd;            
        }

        private void BuildTransferFunctions()
        {
            foreach (CFGNode node in ProgramCFG.CfgNodes)
                if (node is CFGNodeStatement)
                {
                    if ((node is CFGNodeStmtSkip) || (node is CFGNodeStmtPrint) ||
                        (node is CFGNodeStmtGoto) || (node is CFGNodeStmtReturn))
                    {
                        (node as CFGNodeStatement).Transfer = HelperFunctions.BuildIdentityTransfer(m_BddManager,
                             (node as CFGNodeStatement).ProcOf.LocalVariables.VariablesToId,
                             ProgramCFG.GlobalVariables.VariablesToId);
                    }  else

                    if (node is CFGNodeStmtAssignment)
                    {
                        (node as CFGNodeStmtAssignment).Transfer = 
                            CFGNodeStmtAssignment.BuildAssignmentTransfer(node.GetAST(),m_BddManager,
                             (node as CFGNodeStatement).ProcOf.LocalVariables.VariablesToId,
                             ProgramCFG.GlobalVariables.VariablesToId);

                    }  else

                    if (node is CFGNodeStmtConditional) 
                    {
                        Bdd TransferTrue, TransferFalse;
                        CommonAST decider = node.GetAST().getFirstChild() as CommonAST;

                        HelperFunctions.BuildDeciderTransfers(decider, m_BddManager,
                             (node as CFGNodeStatement).ProcOf.LocalVariables.VariablesToId,
                             ProgramCFG.GlobalVariables.VariablesToId, out TransferTrue, out TransferFalse);

                        (node as CFGNodeStmtConditional).TransferTrue = TransferTrue;
                        (node as CFGNodeStmtConditional).TransferFalse = TransferFalse;
                    } else

                    if (node is CFGNodeStmtProcCall)
                    {
                        CFGNodeProcedure procedureCalled;
                        if (!(ProgramCFG.ProcedureNameToNode().TryGetValue(
                            node.GetAST().getFirstChild().getText(), out procedureCalled)))
                            System.Diagnostics.Debug.Assert(false);

                        (node as CFGNodeStmtProcCall).Transfer = 
                            CFGNodeStmtProcCall.buildProcCallTransfer(node.GetAST(),procedureCalled,m_BddManager,
                             (node as CFGNodeStatement).ProcOf.LocalVariables.VariablesToId,
                             ProgramCFG.GlobalVariables.VariablesToId);
                    }

                }
        }
    }
}
