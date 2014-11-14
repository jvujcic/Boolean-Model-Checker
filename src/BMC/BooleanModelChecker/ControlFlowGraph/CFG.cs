using System;
using System.Collections.Generic;
using System.Text;
using BDDlib;

using Microsoft.Glee.Drawing;

using CommonAST = antlr.CommonAST;

namespace BooleanModelChecker.ControlFlowGraph
{
    class CFG
    {
        private BddManager m_BddManager;

        private List<CFGNode> ListOfCfgNodes;
        internal List<CFGNode> CfgNodes
        {
            get { return ListOfCfgNodes; }
        }

        private CFGNodeError ErrorNode;

        private Dictionary<CommonAST, CFGNode> AstToCfgMapping;

        private Dictionary<string, CFGNode> LabelToStatement;

        public string[] GetLabels()
        {
            string[] labels = new string[LabelToStatement.Keys.Count];
            LabelToStatement.Keys.CopyTo(labels, 0);

            return labels;
        }
        internal CFGNode getLabeledStatement(string label)
        {
            CFGNode lStatement;
            if (LabelToStatement.TryGetValue(label, out lStatement))
            {
                return lStatement;
            }
            else
            {
                return null;
            }
        }

        private Dictionary<string, CFGNodeProcedure> ProcNameToNode;
        internal Dictionary<string, CFGNodeProcedure> ProcedureNameToNode()
        {
            return ProcNameToNode;
        }

        private Variables m_GlobalVariables;
        internal Variables GlobalVariables
        {
            get
            {
                return m_GlobalVariables;
            }
        }

        /*Because antlr ASTs do not provide an interface for accessing parent nodes
         we will use a dictionary to store parent nodes as we traverse down the AST*/
        private Dictionary<CommonAST, CommonAST> ASTParentByASTNode;

        private CommonAST BoolProgramAst;
        internal CommonAST GetAST()
        {
            return BoolProgramAst;
        }

        private List<string> m_BddVariableToName;
        internal List<string> BddVariableToName
        {
            get
            {
                return m_BddVariableToName;
            }
        }

        public CFG(CommonAST BoolProgram, BddManager manager)
        {
            BoolProgramAst = BoolProgram;

            m_BddManager = manager;

            m_BddVariableToName = new List<string>(1000);
            m_BddVariableToName.Add("");

            ListOfCfgNodes = new List<CFGNode>();

            AstToCfgMapping = new Dictionary<CommonAST, CFGNode>();

            ASTParentByASTNode = new Dictionary<CommonAST, CommonAST>();

            LabelToStatement = new Dictionary<string, CFGNode>();

            ProcNameToNode = new Dictionary<string, CFGNodeProcedure>();

            m_GlobalVariables = new Variables();

            BuildCfgFromAst();
        }

        public int ReBuildCfg(CommonAST NewProgramAST)
        {
            BoolProgramAst = NewProgramAST;
            return BuildCfgFromAst();
        }


        #region Sub Methods for creation of Control Flow Graph
        private void ExtractCfgNodes(CommonAST SubAst, CommonAST ParentAST, CFGNodeProcedure ProcOf)
        {
            while (SubAst != null)
            {
                ASTParentByASTNode.Add(SubAst, ParentAST);

                #region AST Nodes of type Statement
                if (SubAst.Type == BoolParserTokenTypes.STMT ||
                    SubAst.Type == BoolParserTokenTypes.LSTMT)
                {
                    if (!AstToCfgMapping.ContainsKey(SubAst))
                    {
                        CFGNodeStatement NewStatementNode;
                        if (SubAst.Type == BoolParserTokenTypes.STMT)
                        {
                            NewStatementNode = CFGStatementNodeFactory.Make((CommonAST)SubAst.getFirstChild());
                        }
                        else
                        {
                            NewStatementNode = CFGStatementNodeFactory.Make(
                                (CommonAST)SubAst.getFirstChild().getNextSibling());
                            NewStatementNode.setLabel(SubAst.getFirstChild().getText());
                            LabelToStatement.Add(SubAst.getFirstChild().getText(), NewStatementNode);
                        }
                        NewStatementNode.ProcOf = ProcOf;
                        ListOfCfgNodes.Add(NewStatementNode);
                        AstToCfgMapping.Add(SubAst, NewStatementNode);
                    }
                }
                
                #endregion
                #region AST Nodes of type Procedure
                if (SubAst.Type == BoolParserTokenTypes.PROC)
                {
                    if (!AstToCfgMapping.ContainsKey(SubAst))
                    {
                        CFGNodeProcedure NewStatementNode =
                            new CFGNodeProcedure((CommonAST)SubAst.getFirstChild());
                        ListOfCfgNodes.Add(NewStatementNode);
                        AstToCfgMapping.Add(SubAst, NewStatementNode);

                        ProcNameToNode.Add(SubAst.getFirstChild().getText(), NewStatementNode);

                        ProcOf = NewStatementNode;

                        //Inserting the function parameters into the procedure local variables list
                        CommonAST walker = (CommonAST)SubAst.getFirstChild().getFirstChild();
                        while (walker.Type == BoolParserTokenTypes.ID)
                        {
                            int varID = ProcOf.LocalVariables.AddVar(walker.getText(), m_BddManager, m_BddVariableToName);
                            ProcOf.FormalParameters.AddVar(walker.getText(), varID);
                            walker = (CommonAST)walker.getNextSibling();
                        }
                    }
                }
                
                #endregion
                #region AST Nodes of type Variable Declaration
                if (SubAst.Type == BoolParserTokenTypes.LITERAL_decl)
                {
                    CommonAST walker = (CommonAST)SubAst.getFirstChild();

                    while (walker != null)
                    {
                        if (ProcOf != null)
                        {
                            ProcOf.LocalVariables.AddVar(walker.getText(), m_BddManager, m_BddVariableToName);
                            
                        }
                        else
                            m_GlobalVariables.AddVar(walker.getText(), m_BddManager, m_BddVariableToName);

                        walker = (CommonAST)walker.getNextSibling();
                    }
                }
                #endregion

                ExtractCfgNodes((CommonAST)SubAst.getFirstChild(), SubAst,ProcOf);

                SubAst = (CommonAST)SubAst.getNextSibling();

            }
        }

        private void BuildNextFunction()
        {
            foreach (CFGNode node in ListOfCfgNodes)
                if (node is CFGNodeStatement)
                {
                    CFGNodeStatement stmt = (CFGNodeStatement)node;

                    CFGNode NextNode = null;

                    CommonAST ASTwalker = stmt.GetAST();
                    ASTParentByASTNode.TryGetValue(ASTwalker, out ASTwalker);

                    bool done = false;
                    while (!done && ASTwalker != null)
                    {
                        #region STMT or LSTMT
                        if (ASTwalker.Type == BoolParserTokenTypes.LITERAL_while)
                        {
                            done = true;
                            ASTParentByASTNode.TryGetValue(ASTwalker, out ASTwalker);
                            if (AstToCfgMapping.TryGetValue(ASTwalker, out NextNode))
                                stmt.Next = NextNode;
                            else
                                stmt.Next = null;
                        } else
                            if ((ASTwalker.Type == BoolParserTokenTypes.STMT
                                || ASTwalker.Type == BoolParserTokenTypes.LSTMT) &&
                                (ASTwalker.getNextSibling() != null))
                            {
                                CommonAST ASTwalkerSibling = (CommonAST)ASTwalker.getNextSibling();
                                if (ASTwalkerSibling.Type == BoolParserTokenTypes.STMT
                                    || ASTwalkerSibling.Type == BoolParserTokenTypes.LSTMT)
                                {
                                    done = true;
                                    if (AstToCfgMapping.TryGetValue(ASTwalkerSibling, out NextNode))
                                        stmt.Next = NextNode;
                                    else
                                        stmt.Next = null;
                                }
                            }
                        #endregion

                        #region PROC
                        if (ASTwalker.Type == BoolParserTokenTypes.PROC)
                        {
                            done = true;
                            if (AstToCfgMapping.TryGetValue(ASTwalker, out NextNode))
                                stmt.Next = NextNode;
                            else
                                stmt.Next = null;
                        }
                        #endregion

                        if (!done)
                            ASTParentByASTNode.TryGetValue(ASTwalker, out ASTwalker);
                    }

                    if (stmt is CFGNodeStmtProcCall)
                    {
                        if (NextNode is CFGNodeStmtSkip)
                        {
                            (NextNode as CFGNodeStmtSkip).setPreviousProcCall(stmt as CFGNodeStmtProcCall);
                        }
                    }

                } else
                    //We use this pass trough the CFG nodes to build "First Statement Of Procedure" function
                    if (node is CFGNodeProcedure)
                    {
                        CFGNodeProcedure proc = (CFGNodeProcedure)node;

                        CommonAST ASTwalker = proc.GetAST();

                        ASTwalker = (CommonAST)ASTwalker.getFirstChild();
                        while (ASTwalker.Type != BoolParserTokenTypes.SSEQ)
                            ASTwalker = (CommonAST)ASTwalker.getNextSibling();
                        ASTwalker = (CommonAST)ASTwalker.getFirstChild();

                        CFGNode FirstOf;
                        if (AstToCfgMapping.TryGetValue(ASTwalker, out FirstOf))
                            proc.FirstStmtOf = FirstOf;
                    }
        }

        private void BuildSuccesors()
        {
            foreach (CFGNode node in ListOfCfgNodes)
            {
                CommonAST nodeAST = node.GetAST();
                CFGNode succNode;

                #region Succesors for Statements
                if (node is CFGNodeStatement)
                {
                    CFGNodeStatement stmtNode = (CFGNodeStatement)node;
                    switch (nodeAST.Type)
                    {
                        case BoolParserTokenTypes.LITERAL_goto :
                            nodeAST = (CommonAST)nodeAST.getFirstChild();
                            if (LabelToStatement.TryGetValue(nodeAST.getText(),out succNode))
                                stmtNode.AddSuccesor(succNode);
                            break;

                        case BoolParserTokenTypes.ASSIGNMENT:
                        case BoolParserTokenTypes.LITERAL_skip:
                        case BoolParserTokenTypes.LITERAL_print:
                            stmtNode.AddSuccesor(stmtNode.Next);
                            break;

                        case BoolParserTokenTypes.LITERAL_return:
                            stmtNode.AddSuccesor(stmtNode.ProcOf);
                            break;

                        case BoolParserTokenTypes.LITERAL_if:
                            nodeAST = (CommonAST)nodeAST.getFirstChild();
                            nodeAST = (CommonAST)nodeAST.getNextSibling();
                            if (AstToCfgMapping.TryGetValue(nodeAST.getFirstChild() as CommonAST,out succNode))
                                (stmtNode as CFGNodeStmtIf).TrueSuccesor = succNode;
                            nodeAST = (CommonAST)nodeAST.getNextSibling();
                            if (AstToCfgMapping.TryGetValue(nodeAST.getFirstChild() as CommonAST,out succNode))
                                (stmtNode as CFGNodeStmtIf).FalseSuccesor = succNode;
                            break;

                        case BoolParserTokenTypes.LITERAL_while:
                            nodeAST = (CommonAST)nodeAST.getFirstChild();
                            nodeAST = (CommonAST)nodeAST.getNextSibling();
                            nodeAST = (CommonAST)nodeAST.getFirstChild();
                            if (AstToCfgMapping.TryGetValue(nodeAST,out succNode))
                                (stmtNode as CFGNodeStmtWhile).TrueSuccesor = succNode;
                            (stmtNode as CFGNodeStmtWhile).FalseSuccesor = stmtNode.Next;
                            break;

                        case BoolParserTokenTypes.LITERAL_assert:
                            (stmtNode as CFGNodeStmtAssert).FalseSuccesor = ErrorNode;
                            (stmtNode as CFGNodeStmtAssert).TrueSuccesor = stmtNode.Next;
                            break;

                        case BoolParserTokenTypes.PROCCALL:
                            nodeAST = (CommonAST)nodeAST.getFirstChild();
                            CFGNodeProcedure procNode;
                            if (ProcNameToNode.TryGetValue(nodeAST.getText(),out procNode))
                                stmtNode.AddSuccesor(procNode.FirstStmtOf);

                            //Exit points of procedures need to be filled when processing all procedure calls
                            procNode.AddSuccesor(stmtNode.Next);
                            break;

                        default:
                            System.Diagnostics.Debug.Assert(false);
                            break;
                    }
                }              
                #endregion
                //Succesors for Procedure Exits are built when processing Procedure calls
            }
        }

        #endregion
        private int BuildCfgFromAst()
        {          
            ListOfCfgNodes.Clear();
            AstToCfgMapping.Clear();
            ASTParentByASTNode.Clear();
            LabelToStatement.Clear();
            ProcNameToNode.Clear();
            m_GlobalVariables.Clear();
            m_BddManager.DeleteAll();
            m_BddVariableToName.Clear();
            m_BddVariableToName.Add("");


            ExtractCfgNodes(BoolProgramAst,null,null);
            ErrorNode = new CFGNodeError();
            ListOfCfgNodes.Add(ErrorNode);

            BuildNextFunction();

            BuildSuccesors();

            return 0;
        }

        public Graph GetNextFunctionAsGLEEGraph()
        {
            Graph g = new Graph("Next");

            foreach (CFGNode Node in ListOfCfgNodes)
            {
                Node n = (Node) g.AddNode(Node.GetHashCode().ToString());
                n.Attr.Label = Node.ToString();
            }

            foreach (CFGNode Node in ListOfCfgNodes)
            if (Node is CFGNodeStatement)
            {
                CFGNodeStatement StmtNode = (CFGNodeStatement) Node;
                CFGNode NextNode = StmtNode.Next;
                g.AddEdge(Node.GetHashCode().ToString(),NextNode.GetHashCode().ToString());
            }

            return g;
        }

        public Graph ToGLEEGraph()
        {
            Graph g = new Graph("CFG");

            foreach (CFGNode Node in ListOfCfgNodes)
            {
                Node n = (Node)g.AddNode(Node.GetHashCode().ToString());
                n.Attr.Label = Node.ToString();
            }
            
            foreach (CFGNode Node in ListOfCfgNodes)
            {
                if (Node is CFGNodeProcedure)
                {
                    List<CFGNode> Succ = (Node as CFGNodeProcedure).Succesor;

                    foreach (CFGNode Succesor in Succ)
                    {
                        g.AddEdge(Node.GetHashCode().ToString(), Succesor.GetHashCode().ToString());
                    }
                }
                else if (Node is CFGNodeStmtConditional)
                {
                    g.AddEdge(Node.GetHashCode().ToString(), (Node as CFGNodeStmtConditional).FalseSuccesor.GetHashCode().ToString());
                    g.AddEdge(Node.GetHashCode().ToString(), (Node as CFGNodeStmtConditional).TrueSuccesor.GetHashCode().ToString());
                }
                else if (Node is CFGNodeError) { }
                else 
                {
                    g.AddEdge(Node.GetHashCode().ToString(), Node.Succesor.GetHashCode().ToString());
                }

            }

            return g;
        }

        #region Code for generating formatted code listing
	    private formattedCodeView getFormattedSSEQ(CommonAST sseq, formattedCodeView formattedCode, string tab)
        {
            CommonAST walker = (CommonAST)sseq.getFirstChild();

            while (walker != null)
            {
                CommonAST statement;
                if (walker.Type == BoolParserTokenTypes.STMT)
                {
                    statement = (CommonAST)walker.getFirstChild();
                }
                else
                {
                    statement = (CommonAST)walker.getFirstChild().getNextSibling();
                }

                CFGNode stmtNode;
                if (AstToCfgMapping.TryGetValue(walker, out stmtNode))
                {                   
                    if ((stmtNode is CFGNodeStmtIf) || (stmtNode is CFGNodeStmtWhile))
                    {
                        if (stmtNode is CFGNodeStmtIf)
                        {
                            string line = tab + stmtNode.ToString() + " then";
                            formattedCode.addLine(line, stmtNode);
                            
                            statement = (CommonAST)statement.getFirstChild().getNextSibling();
                            formattedCode = getFormattedSSEQ(statement, formattedCode, tab + "  ");

                            formattedCode.addLine(tab + "else");

                            statement = (CommonAST)statement.getNextSibling();
                            formattedCode = getFormattedSSEQ(statement, formattedCode, tab + "  ");

                            formattedCode.addLine(tab + "fi");
                        }
                        else
                        {
                            formattedCode.addLine(tab + stmtNode.ToString(), stmtNode);
                            formattedCode.addLine(tab + "do");

                            statement = (CommonAST)statement.getFirstChild().getNextSibling();
                            formattedCode = getFormattedSSEQ(statement, formattedCode, tab + "  ");

                            formattedCode.addLine(tab + "od");
                        }
                    }
                    else
                    {
                        formattedCode.addLine(tab + stmtNode.ToString(), stmtNode);
                    }

                }

                walker = (CommonAST)walker.getNextSibling();
            }

            return formattedCode;
        }
        private formattedCodeView getFormattedProcedure(CommonAST procedure, formattedCodeView formattedCode)
        {
            CommonAST walker = (CommonAST)procedure.getFirstChild();

            string procedureHead = string.Empty;

            procedureHead += walker.getText() + "(";
            walker = (CommonAST)walker.getFirstChild();

            bool first = true;
            while (walker.Type == BoolParserTokenTypes.ID)
            {
                if (!first)
                {
                    procedureHead += ", ";
                }
                procedureHead += walker.getText();

                walker = (CommonAST)walker.getNextSibling();
                first = false;
            }
            procedureHead += ")";

            formattedCode.addLine(procedureHead);
            formattedCode.addLine("begin");

            while (walker.Type == BoolParserTokenTypes.LITERAL_decl)
            {
                formattedCode.addLine("  "+HelperFunctions.DeclToString(walker));

                walker = (CommonAST)walker.getNextSibling();
            }

            formattedCode = getFormattedSSEQ(walker, formattedCode, "  ");

            formattedCode.addLine("end");

            return formattedCode;
        }
        public formattedCodeView getFormattedProgram()
        {
            formattedCodeView formattedProgram = new formattedCodeView();

            CommonAST walker = BoolProgramAst;

            while ((walker!=null) && (walker.Type == BoolParserTokenTypes.LITERAL_decl))
            {
                formattedProgram.addLine(HelperFunctions.DeclToString(walker));

                walker = (CommonAST)walker.getNextSibling();
            }

            formattedProgram.addLine("");

            while((walker!=null) && (walker.Type == BoolParserTokenTypes.PROC))
            {
                formattedProgram = getFormattedProcedure(walker, formattedProgram);

                formattedProgram.addLine("");

                walker = (CommonAST)walker.getNextSibling();
            }

            return formattedProgram;
        }
 
    	#endregion
    }
}
