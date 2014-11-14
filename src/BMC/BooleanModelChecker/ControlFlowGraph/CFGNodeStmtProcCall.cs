using System;
using System.Collections.Generic;
using System.Text;

using CommonAST = antlr.CommonAST;

using BDDlib;

namespace BooleanModelChecker.ControlFlowGraph
{
    class CFGNodeStmtProcCall : CFGNodeStatement
    {

        
        public CFGNodeStmtProcCall(CommonAST StatementAST)
            : base(StatementAST)
        {
        }
        
        public override string ToString()
        {
            CommonAST walker = (CommonAST)NodeASTSubTree.getFirstChild();
            string Text = m_Label + walker.getText() + "(";
            walker = (CommonAST)walker.getFirstChild();
            while (walker != null)
            {
                Text += HelperFunctions.ExprToString(walker);
                if (walker.getNextSibling() != null)
                    Text += ",";
                walker = (CommonAST)walker.getNextSibling();
            }
            Text += ");";

            return Text;
        }
        public static Bdd buildProcCallTransfer(CommonAST procCall, CFGNodeProcedure procedureCalled,
            BddManager Manager, Dictionary<string, int> LocalVarToId, Dictionary<string, int> GlobalVarToId)
        {
            Bdd transfer = Manager.CreateBddOne();

            CommonAST walkerExpr = procCall.getFirstChild().getFirstChild() as CommonAST;
            CommonAST walkerFormal = procedureCalled.GetAST().getFirstChild() as CommonAST;

            #region Build Bdd for FormalParameters = Call Expressions
            while (walkerExpr != null)
            {
                Bdd expr = HelperFunctions.ExprToBdd(walkerExpr, Manager, LocalVarToId, GlobalVarToId);
                int varID;
                if (!procedureCalled.FormalParameters.VariablesToId.TryGetValue(walkerFormal.getText(), out varID))
                    System.Diagnostics.Debug.Assert(false);

                Bdd formal = Manager.GetBddVariableWithID(varID + 2);

                Bdd tempBdd = Manager.LogicalXnor(formal, expr);

                Bdd tempBdd2 = transfer;
                transfer = Manager.LogicalAnd(transfer, tempBdd);

                expr.FreeBdd();
                formal.FreeBdd();
                tempBdd.FreeBdd();
                tempBdd2.FreeBdd();

                walkerExpr = walkerExpr.getNextSibling() as CommonAST;
                walkerFormal = walkerFormal.getNextSibling() as CommonAST;
            }            
            #endregion
            
            #region And with an identity over Global variables and true Local variables(non Formals)

        /*    int variableCount = procedureCalled.LocalVariables.VariablesToId.Values.Count -
                                procedureCalled.FormalParameters.VariablesToId.Values.Count;
            int[] variableIDs = new int[variableCount];
            int index = 0;
            Bdd identity;
            foreach (int varID in procedureCalled.LocalVariables.VariablesToId.Values)
            {
                if (!(procedureCalled.FormalParameters.VariablesToId.ContainsValue(varID)))
                {
                    variableIDs[index++] = varID;
                }
            }

            Bdd identNewLocals = HelperFunctions.BuildIdentityOverVariableIDs(Manager, variableIDs, variableCount);*/
            Bdd identGlobals = HelperFunctions.BuildIdentityTransfer(Manager, null, GlobalVarToId);

        /*    if (identNewLocals != null && identGlobals != null)
            {
                identity = Manager.LogicalAnd(identGlobals, identNewLocals);
                identNewLocals.FreeBdd();               
                identGlobals.FreeBdd();
            }
            else if (identNewLocals != null)
            {
                identity = identNewLocals;
            }
            else
            {
                identity = identGlobals;
            }*/

            Bdd identity = identGlobals;

            Bdd temp = transfer;

            transfer = Manager.LogicalAnd(transfer, identity);

            //temp.FreeBdd();

            #endregion

            return transfer;
        }

        internal CFGNode ReturnPoint
        {
            get
            {
                return this.Next;
            }
        }

    }
}
