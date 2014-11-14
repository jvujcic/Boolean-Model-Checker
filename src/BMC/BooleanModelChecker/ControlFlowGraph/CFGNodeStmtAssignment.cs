using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

using CommonAST = antlr.CommonAST;

using BDDlib;

namespace BooleanModelChecker.ControlFlowGraph
{
    class CFGNodeStmtAssignment : CFGNodeStatement
    {
        public CFGNodeStmtAssignment(CommonAST StatementAST)
            : base(StatementAST)
        {
        }

        public override string ToString()
        {
            CommonAST walker = (CommonAST)NodeASTSubTree.getFirstChild();

            string Text = m_Label + walker.getText();

            walker = (CommonAST) walker.getNextSibling();

            while (walker.Type != BoolParserTokenTypes.ASSIGN)
            {
                Text += "," + walker.getText();
                walker = (CommonAST)walker.getNextSibling();
            }
            Text += ":=";
            walker = (CommonAST) walker.getNextSibling();
            while (walker != null)
            {
                Text += HelperFunctions.ExprToString(walker);
                if (walker.getNextSibling() != null) Text += ",";
                walker = (CommonAST)walker.getNextSibling();
            }
            Text += ';';

            return Text;
        }

        public static Bdd BuildAssignmentTransfer(CommonAST assignAST, BddManager Manager,
                    Dictionary<string, int> LocalVarToId, Dictionary<string, int> GlobalVarToId)
        {
            CommonAST walkerVariable = assignAST.getFirstChild() as CommonAST;
            CommonAST walkerExpresion = assignAST.getFirstChild() as CommonAST;

            while (walkerExpresion.Type == BoolParserTokenTypes.ID)
            {
                walkerExpresion = walkerExpresion.getNextSibling() as CommonAST;
            }
            walkerExpresion = walkerExpresion.getNextSibling() as CommonAST;

            List<int> assignedVariables = new List<int>();

            Bdd transfer = null;
            bool bddTransferIsnull = true;

            while (walkerVariable.Type == BoolParserTokenTypes.ID)
            {
                Bdd tempBdd, varBdd, exprBdd;
                int VariableID;

                if (!LocalVarToId.TryGetValue(walkerVariable.getText(), out VariableID))
                    if (!GlobalVarToId.TryGetValue(walkerVariable.getText(), out VariableID))
                        Debug.Assert(false);//Varijabla mora biti ili medju lokalnim ili medju globalnim
                assignedVariables.Add(VariableID);

                varBdd = Manager.GetBddVariableWithID(VariableID + 2);
                exprBdd = HelperFunctions.ExprToBdd(walkerExpresion, Manager, LocalVarToId, GlobalVarToId);

                tempBdd = Manager.LogicalXnor(varBdd, exprBdd);

                varBdd.FreeBdd();
                exprBdd.FreeBdd();

                if (bddTransferIsnull)
                {
                    transfer = tempBdd;
                    bddTransferIsnull = false;
                }
                else
                {
                    varBdd = transfer;

                    transfer = Manager.LogicalAnd(transfer, tempBdd);

                    varBdd.FreeBdd();
                    tempBdd.FreeBdd();
                }

                walkerVariable = walkerVariable.getNextSibling() as CommonAST;
                walkerExpresion = walkerExpresion.getNextSibling() as CommonAST;
            }

            #region add identitiy transfer for the rest of the variables
            //Now we add the identity over variables that were not assigned to
            int[] localVariablesIDs = new int[LocalVarToId.Values.Count];
            LocalVarToId.Values.CopyTo(localVariablesIDs, 0);
            int[] globalVariablesIDs = new int[GlobalVarToId.Values.Count];
            GlobalVarToId.Values.CopyTo(globalVariablesIDs, 0);

            int[] unassignedVariables =
                new int[LocalVarToId.Values.Count + GlobalVarToId.Values.Count - assignedVariables.Count];
            int unAssIndex = 0;

            for (int index = 0; index < LocalVarToId.Values.Count; index++)
            {
                if (!(assignedVariables.Contains(localVariablesIDs[index])))
                {
                    unassignedVariables[unAssIndex++] = localVariablesIDs[index];
                }
            }
            for (int index = 0; index < GlobalVarToId.Values.Count; index++)
            {
                if (!(assignedVariables.Contains(globalVariablesIDs[index])))
                {
                    unassignedVariables[unAssIndex++] = globalVariablesIDs[index];
                }
            }

            Bdd identitiy = HelperFunctions.BuildIdentityOverVariableIDs(Manager, unassignedVariables);

            Bdd tempBdd2 = transfer;
            transfer = Manager.LogicalAnd(transfer, identitiy);

            identitiy.FreeBdd();
            tempBdd2.FreeBdd();
            
            #endregion

            return transfer;
        }
    }
}
