using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

using CommonAST = antlr.CommonAST;

using BDDlib;

namespace BooleanModelChecker.ControlFlowGraph
{
    class HelperFunctions
    {
        public static string ExprToString(CommonAST Expression)
        {
            Debug.Assert(Expression != null);

            string output=String.Empty;

            Expression = (CommonAST) Expression.getFirstChild();
            while (Expression != null)
            {
                if (Expression.Type != BoolParserTokenTypes.EXPR)
                {
                    output += Expression.getText(); 
                }
                else
                {
                    output += ExprToString(Expression);
                }

                Expression = (CommonAST)Expression.getNextSibling();
            }

            return output;
        }

        public static string DeciderToString(CommonAST Decider)
        {
            Decider = (CommonAST)Decider.getFirstChild();
            if (Decider.Type == BoolParserTokenTypes.EXPR)
            {
                return HelperFunctions.ExprToString(Decider);
            }
            else
            {
                return Decider.getText();
            }
        }

        public static string DeclToString(CommonAST Decl)
        {
            Debug.Assert(Decl != null);

            string output = "decl ";

            Decl = (CommonAST)Decl.getFirstChild();
            bool first = true;
            while (Decl != null)
            {
                if (!first)
                {
                    output = output + ", ";
                }
                output = output + Decl.getText();

                Decl = (CommonAST)Decl.getNextSibling();
                first = false;
            }

            output = output + ";";

            return output;
        }

        public static Bdd ExprToBdd(CommonAST Expr, BddManager Manager, 
            Dictionary<string, int> LocalVarNameToId, Dictionary<string, int> GlobalVarNameToId)
        {
            Debug.Assert(Expr.Type == BoolParserTokenTypes.EXPR);

            Bdd ExprBdd = null;

            CommonAST walker = (CommonAST)Expr.getFirstChild();
            
            switch (walker.Type)
            {
                case BoolParserTokenTypes.EXPR:
                    ExprBdd = ExprToBdd(walker, Manager, LocalVarNameToId, GlobalVarNameToId);
                    break;
                case BoolParserTokenTypes.EMARK:
                    walker = (CommonAST)walker.getNextSibling();

                    Bdd tempBdd = ExprToBdd(walker, Manager, LocalVarNameToId, GlobalVarNameToId);
                    ExprBdd = Manager.LogicalNot(tempBdd);

                    tempBdd.FreeBdd();
                    break;
                case BoolParserTokenTypes.ID:
                    int VariableID;
                    if (!LocalVarNameToId.TryGetValue(walker.getText(), out VariableID))
                        if (!GlobalVarNameToId.TryGetValue(walker.getText(), out VariableID))
                            Debug.Assert(false);//Varijabla mora biti ili medju lokalnim ili medju globalnim
                    ExprBdd = Manager.GetBddVariableWithID(VariableID);
                    break;
                case BoolParserTokenTypes.CONST:
                    if (walker.getText() == "1")
                        ExprBdd = Manager.CreateBddOne();
                    else
                        ExprBdd = Manager.CreateBddZero();
                    break;
                default:
                    Debug.Assert(false);
                    break;
            }

            if ((walker.getNextSibling()!=null) && 
                ((walker = (CommonAST)walker.getNextSibling()).Type == BoolParserTokenTypes.BINOP))
            {
                Bdd firstOperand = ExprBdd;

                string Operation = walker.getText();
                walker = (CommonAST) walker.getNextSibling();

                Bdd secondOperand = ExprToBdd(walker, Manager, LocalVarNameToId, GlobalVarNameToId);

                switch(Operation)
                {
                    case("|") : 
                        ExprBdd = Manager.LogicalOr(firstOperand, secondOperand);
                        break;
                    case ("&"):
                        ExprBdd = Manager.LogicalAnd(firstOperand, secondOperand);
                        break;
                    case ("^"):
                        ExprBdd = Manager.LogicalXor(firstOperand, secondOperand);
                        break;
                    case ("="):
                        ExprBdd = Manager.LogicalXnor(firstOperand, secondOperand);
                        break;
                    case ("!="):
                        ExprBdd = Manager.LogicalXor(firstOperand, secondOperand);
                        break;
                    case ("=>"):
                        ExprBdd = Manager.LogicalImplies(firstOperand, secondOperand);
                        break;
                    default:
                        Debug.Assert(false);
                        break;
                }

                firstOperand.FreeBdd();
                secondOperand.FreeBdd();
            }

            return ExprBdd;
        }

        /*uses the Manager to build an identity between primed and unprimed instance of each variable
         in VariableIDs */
        public static Bdd BuildIdentityOverVariableIDs(BddManager Manager, int[] VariableIDs)
        {
            Bdd identity = null;
            
            if (VariableIDs.Length > 0)
            {
                identity = Manager.CreateBddOne();

                Bdd xnorBdd, tempBdd;
                for (int variable = 0; variable < VariableIDs.Length; variable++)
                {
                    xnorBdd = Manager.LogicalXnor(
                                Manager.GetBddVariableWithID(VariableIDs[variable]),
                                Manager.GetBddVariableWithID(VariableIDs[variable] + 2)
                                );

                    tempBdd = identity;
                    identity = Manager.LogicalAnd(identity, xnorBdd);
                    tempBdd.FreeBdd();
                    xnorBdd.FreeBdd();
                }
            }

            return identity;
        }

        public static Bdd BuildIdentityTransfer(BddManager Manager,
            Dictionary<string, int> LocalVarToId, Dictionary<string, int> GlobalVarToId)
        {
            Bdd identityTransfer = null;

            if ((LocalVarToId != null) && (LocalVarToId.Values.Count > 0))
            {
                int[] VariableIDs = new int[LocalVarToId.Values.Count];
                LocalVarToId.Values.CopyTo(VariableIDs, 0);

                identityTransfer = BuildIdentityOverVariableIDs(Manager, VariableIDs);
            }

            if ((GlobalVarToId != null) && (GlobalVarToId.Values.Count > 0))
            {
                int[] VariableIDs = new int[GlobalVarToId.Values.Count];
                GlobalVarToId.Values.CopyTo(VariableIDs, 0);

                if (identityTransfer!=null)
                {
                    Bdd identGlobal = BuildIdentityOverVariableIDs(Manager, VariableIDs);
                    Bdd tempBdd = identityTransfer;

                    identityTransfer = Manager.LogicalAnd(identityTransfer, identGlobal);

                    identGlobal.FreeBdd();
                    tempBdd.FreeBdd();
                }
                else
                {
                    identityTransfer = BuildIdentityOverVariableIDs(Manager, VariableIDs);    
                }
            }

            if (identityTransfer==null) identityTransfer = Manager.CreateBddZero();

            return identityTransfer;
        }

        public static void BuildDeciderTransfers(CommonAST decider, BddManager Manager,
            Dictionary<string, int> LocalVarToId, Dictionary<string, int> GlobalVarToId,
            out Bdd TransferTrue, out Bdd TransferFalse)
        {
            Bdd identity = BuildIdentityTransfer(Manager, LocalVarToId, GlobalVarToId);

            Debug.Assert(decider.Type == BoolParserTokenTypes.DECIDER);

            CommonAST walker = decider.getFirstChild() as CommonAST;

            if (walker.Type == BoolParserTokenTypes.QMARK)
            {
                TransferTrue = identity;
                TransferFalse = identity;
            }
            else
            {
                Debug.Assert(walker.Type == BoolParserTokenTypes.EXPR);

                Bdd expr = ExprToBdd(walker, Manager, LocalVarToId, GlobalVarToId);

                TransferTrue = Manager.LogicalAnd(Manager.LogicalXnor(expr, Manager.CreateBddOne()), identity);
                TransferFalse = Manager.LogicalAnd(Manager.LogicalXnor(expr, Manager.CreateBddZero()), identity);

                identity.FreeBdd();
                expr.FreeBdd();                
            }
        }

    }
}
