using BooleanModelChecker.ControlFlowGraph;
using BDDlib;
using System.Collections.Generic;
using System.Text;

namespace BooleanModelChecker
{
    public partial class BMC
    {
        public Bdd BuildRandomPath(Bdd root)
        {
            if (root == null) return null;
            if (root.GetBddRootVariable().ReturnBddType() == BddType.Zero) return m_BddManager.CreateBddZero();

            Bdd pathBdd = m_BddManager.CreateBddOne();
           // Bdd variableBdd = root.GetBddRootVariable(); 
            Bdd tempBdd, tempBddNot;

            while (root.GetBddRootVariable().ReturnBddType() == BddType.Variable)
            {
                if (root.GetThenBranch().GetBddRootVariable().ReturnBddType() != BddType.Zero)
                {
                    tempBdd = pathBdd;
                    pathBdd = m_BddManager.LogicalAnd(pathBdd, root.GetBddRootVariable());
                    tempBdd.FreeBdd();
                    root = root.GetThenBranch();                                       
                }
                else
                {
                    tempBdd = pathBdd;
                    tempBddNot = m_BddManager.LogicalNot(root.GetBddRootVariable());
                    pathBdd = m_BddManager.LogicalAnd(pathBdd, tempBddNot);
                    tempBdd.FreeBdd();
                    tempBddNot.FreeBdd();
                    root = root.GetElseBranch();

                }
            }

            return pathBdd;
        }

        static internal string printBddValuation(Bdd valuation, List<string> BddVariableToName)
        {
            string valPrint = string.Empty;

            if (valuation == null) return "";
            if (valuation.GetBddRootVariable().ReturnBddType() == BddType.Zero) return "";

            while (valuation.GetBddRootVariable().ReturnBddType() == BddType.Variable)
            {
                if (valuation.GetBddRootVariableID() % 3 == 0)
                {
                    valPrint += (BddVariableToName[valuation.GetBddRootVariableID()]);
                    valPrint = valPrint.Remove(valPrint.Length - 1);
                    valPrint += "=";
                }

                if (valuation.GetThenBranch().GetBddRootVariable().ReturnBddType() != BddType.Zero)
                {
                    if(valuation.GetBddRootVariableID() % 3 == 0) 
                        valPrint += "1 ";
                    valuation = valuation.GetThenBranch();
                }
                else
                {
                    if (valuation.GetBddRootVariableID() % 3 == 0)
                        valPrint += "0 ";
                    valuation = valuation.GetElseBranch();
                }

            }

            return valPrint;
        }
    }
}