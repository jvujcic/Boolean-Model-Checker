using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using BDDlib;

/*ANTLR usage statements*/
using CommonAST = antlr.CommonAST;
using AST = antlr.collections.AST;
using RecognitionException = antlr.RecognitionException;
using TokenStreamException = antlr.TokenStreamException;
using LexerSharedInputState = antlr.LexerSharedInputState;

using Microsoft.Glee.Drawing;

using BooleanModelChecker.ControlFlowGraph;

namespace BooleanModelChecker
{
    public partial class BMC
    {
        private BoolLexer Lexer;
        private BoolParser Parser;
        private LexerSharedInputState CodeInput;
        private CommonAST BoolProgAST;
        private CFG ProgramCFG;

        string ErrorLog;

        #region Only for Debug
        public List<string> BddToName
        {
            get
            {
                return ProgramCFG.BddVariableToName;
            }
        } 
        #endregion

        public BMC()
        {
            CodeInput = new antlr.LexerSharedInputState(new StringReader(String.Empty));

            Lexer = new BoolLexer(CodeInput);
            Parser = new BoolParser(Lexer);

            ErrorLog = String.Empty;

            BoolProgAST = (CommonAST)Parser.getAST();

            m_BddManager = new BddManager();

            ProgramCFG = new CFG(BoolProgAST, m_BddManager);

            m_PathEdges = new Dictionary<CFGNode, PathEdges>();

            m_SummaryEdges = new Dictionary<CFGNode, Bdd>();
            
        }

        public bool ParseProgram(string BooleanProgram)
        {
            bool Succesful = true;
            try
            {
                ErrorLog = String.Empty;

                Lexer.resetState(new StringReader(BooleanProgram));
                Parser.resetState();

                StringWriter ErrorLogger = new StringWriter();
                Console.SetError(ErrorLogger);

                Parser.prog();

                ErrorLog = ErrorLogger.ToString();

                if (ErrorLog.Equals(String.Empty))
                {
                    ErrorLog += "Syntax validation completed without errors.\n";
                    BoolProgAST = (CommonAST)Parser.getAST();
                }
                else
                {
                    Succesful = false;
                }
            }
            catch (TokenStreamException err)
            {
                ErrorLog += ("exception: " + err.ToString() + "\n");
                Succesful = false;
            }
            catch (RecognitionException err)
            {
                ErrorLog += ("exception: " + err.ToString() + "\n");
                Succesful = false;
            }

            if (Succesful)
            {
                BoolSemantics.BoolProgramChecker SemanticChecker = 
                    new BoolSemantics.BoolProgramChecker(BoolProgAST);

                List<BoolSemantics.SemanticError> Errors = SemanticChecker.Check();

                if (Errors.Count == 0)
                {
                    ErrorLog += "Semantic validation completed without errors.\n";

                    BuildCFG();
                }
                else
                {
                    ErrorLog += "\n";
                    foreach (BoolSemantics.SemanticError Error in Errors)
                    {
                        ErrorLog += Error.ToString() + "\n";
                    }

                    BoolProgAST = null;
                }
            }

            return Succesful;
        }

        public string GetParserErrorLog()
        {
            return ErrorLog;
        }

        private void BuildCFG()
        {
            ProgramCFG.ReBuildCfg(BoolProgAST);
        }

        #region Code that exposes part of the CFG interface
        public string[] GetStatementLabels()
        {
            return ProgramCFG.GetLabels();
        }
        public bool isLabelValid(string label)
        {
            return ProgramCFG.getLabeledStatement(label) != null;
        }
        public CFGNode GetLabeledCFGNode(string label)
        {
            return ProgramCFG.getLabeledStatement(label);
        }

        public formattedCodeView getFormattedCode()
        {
            return ProgramCFG.getFormattedProgram();
        }        
        #endregion

        #region Functions That Return debug information from AST and CFG
        private List<CommonAST> ASTorderedByGraphIndex;
        public Bdd GetExprBdd(int ASTIndex, out String Description)
        {

            if (ASTorderedByGraphIndex != null)
            {
                CommonAST Expression = ASTorderedByGraphIndex[ASTIndex];
                if (Expression.Type == BoolParserTokenTypes.EXPR)
                {
                    Description = ControlFlowGraph.HelperFunctions.ExprToString(Expression);
                    return ControlFlowGraph.HelperFunctions.ExprToBdd(Expression, m_BddManager,
                        ProgramCFG.GlobalVariables.VariablesToId, ProgramCFG.GlobalVariables.VariablesToId);
                }
                else
                {
                    Description = "Not an expression";
                    return null;
                }
            }
            else
            {
                Description = "N/A";
                return null;
            }
        }

        /*public BddManager GetBddManager()
        { return ProgramCFG.GetBddManager(); }
        */
        #endregion

        #region Various GLEE Graph return functions
        int ASTforGLEEIndex;
        private int DrawAST(CommonAST AST, ref Graph g)
        {

            if (AST != null)
            {
                int thisNodeIndex = ASTforGLEEIndex++;

                ASTorderedByGraphIndex.Add(AST);

                Node thisNode = (Node)g.AddNode(thisNodeIndex.ToString());
                thisNode.Attr.Label = AST.getText();

                CommonAST ChildAST = (CommonAST)AST.getFirstChild();

                int previousChildIndex = -1;

                for (int i = 0; i < AST.getNumberOfChildren(); i++)
                {
                    int childIndex = DrawAST(ChildAST, ref g);

                    if (childIndex != -1)
                    {
                        g.AddEdge(thisNodeIndex.ToString(), childIndex.ToString());
                        /*                        if (previousChildIndex != -1)
                                                {
                                                    Edge SiblingEdge = (Edge)g.AddEdge(previousChildIndex.ToString(), 
                                                                                        childIndex.ToString());
                                                    SiblingEdge.Attr.Styles = new Style[] { Microsoft.Glee.Drawing.Style.Dashed };
                                                }
                          */
                    }

                    previousChildIndex = childIndex;

                    ChildAST = (CommonAST)ChildAST.getNextSibling();
                }

                return thisNodeIndex;
            }
            else
            {
                return -1;
            }
        }
       
        public Graph GetASTasGLEEGraph()
        {
            Graph g = new Graph("AST");
            g.GraphAttr.NodeAttr.Padding = 3;

            CommonAST BoolProgramASTRoot = new CommonAST();
            BoolProgramASTRoot.setText("Bool Program");
            BoolProgramASTRoot.addChild(BoolProgAST);

            ASTforGLEEIndex = 0;
            if (ASTorderedByGraphIndex == null)
                ASTorderedByGraphIndex = new List<CommonAST>();
            else
                ASTorderedByGraphIndex.Clear();

            DrawAST(BoolProgramASTRoot, ref g);

            return g;
        }

        public Graph GetCFGasGLEEGraph()
        {
            return ProgramCFG.ToGLEEGraph();
        }

        public Graph GetCFGNextFunctionAsGLEEGraph()
        {
            return ProgramCFG.GetNextFunctionAsGLEEGraph();
        }
        
        #endregion
    }
}
