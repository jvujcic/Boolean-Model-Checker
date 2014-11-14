using System;
using System.Collections.Generic;
using System.Text;

using CommonAST = antlr.CommonAST;

namespace BooleanModelChecker.BoolSemantics
{
    class BoolProgramChecker
    {
        private List<string> GlobalVariables;
        private List<string> ProcedureNames;
        private Dictionary<string, int> ProcedureToNumParam;

        private CommonAST BoolProgram;

        public BoolProgramChecker(CommonAST ProgramAST)
        {
            BoolProgram = ProgramAST;

            GlobalVariables = new List<string>();
            ProcedureNames = new List<string>();
            ProcedureToNumParam = new Dictionary<string, int>();
        }


        private List<SemanticError> CheckExpression(CommonAST EXPR, List<SemanticError> Errors,
                                                    List<string> LocalVariables, string ProcOf)
        {

            CommonAST walker = (CommonAST)EXPR.getFirstChild();

            while (walker != null)
            {
                switch (walker.Type)
                {
                    case(BoolParserTokenTypes.ID):
                        if (!(LocalVariables.Contains(walker.getText())) && 
                            !(GlobalVariables.Contains(walker.getText())))
                            Errors.Add(new SemanticError(BoolSemanticErrorTypes.VAR_UNDECLARED, 
                                walker.getText(), ProcOf));
                        break;
                    case(BoolParserTokenTypes.EXPR):
                        Errors = CheckExpression(walker, Errors, LocalVariables, ProcOf);
                        break;
                }

                walker = (CommonAST)walker.getNextSibling();
            }

            return Errors;
        }

        private List<SemanticError> CheckSSEQ(CommonAST sseqAST, List<SemanticError> Errors, 
                                                List<string> LocalVariables, string ProcOf)
        {

            CommonAST walker = (CommonAST)sseqAST.getFirstChild();

            while (walker != null)
            {
                CommonAST subWalker;
                
                CommonAST stmtWalker = (CommonAST)walker;
                if (stmtWalker.Type == BoolParserTokenTypes.LSTMT)
                    stmtWalker = (CommonAST)stmtWalker.getFirstChild().getNextSibling();
                else
                    stmtWalker = (CommonAST)stmtWalker.getFirstChild();                    

                switch (stmtWalker.Type)
                {
                    case(BoolParserTokenTypes.LITERAL_print):
                        subWalker = (CommonAST)stmtWalker.getFirstChild();
                        while(subWalker!=null)
                        {
                            Errors = CheckExpression(subWalker,Errors,LocalVariables,ProcOf);
                            subWalker = (CommonAST)subWalker.getNextSibling();
                        }
                        break;
                    case(BoolParserTokenTypes.ASSIGNMENT):
                        subWalker = (CommonAST)stmtWalker.getFirstChild();
                        while (subWalker != null)
                        {
                            switch (subWalker.Type)
                            {
                                case(BoolParserTokenTypes.ID):
                                    if (!(LocalVariables.Contains(subWalker.getText())) &&
                                        !(GlobalVariables.Contains(subWalker.getText())))
                                        Errors.Add(new SemanticError(BoolSemanticErrorTypes.VAR_UNDECLARED,
                                            subWalker.getText(), ProcOf));
                                    break;
                                case(BoolParserTokenTypes.EXPR):
                                    Errors = CheckExpression(subWalker,Errors,LocalVariables,ProcOf);
                                    break;
                            }
                            subWalker = (CommonAST)subWalker.getNextSibling();
                        }
                        break;
                    case(BoolParserTokenTypes.LITERAL_if):
                    case (BoolParserTokenTypes.LITERAL_while):
                        subWalker = (CommonAST)stmtWalker.getFirstChild();
                        if (subWalker.getFirstChild().Type == BoolParserTokenTypes.EXPR)
                            Errors = CheckExpression((CommonAST)subWalker.getFirstChild(), 
                                         Errors, LocalVariables, ProcOf);
                        subWalker = (CommonAST)subWalker.getNextSibling();
                        Errors = CheckSSEQ(subWalker, Errors, LocalVariables, ProcOf);
                        if (stmtWalker.Type == BoolParserTokenTypes.LITERAL_if)
                        {
                            subWalker = (CommonAST)subWalker.getNextSibling();
                            Errors = CheckSSEQ(subWalker, Errors, LocalVariables, ProcOf);
                        }
                        break;
                    case (BoolParserTokenTypes.LITERAL_assert):
                        subWalker = (CommonAST)stmtWalker.getFirstChild();
                        if (subWalker.getFirstChild().Type == BoolParserTokenTypes.EXPR)
                            Errors = CheckExpression((CommonAST)subWalker.getFirstChild(),
                                         Errors, LocalVariables, ProcOf);
                        break;
                    case (BoolParserTokenTypes.PROCCALL):
                        subWalker = (CommonAST)stmtWalker.getFirstChild();
                        if (ProcedureNames.Contains(subWalker.getText()))
                        {
                            //Checking for a required skip statement after a Procedure call
                            CommonAST afterProc = (CommonAST)walker.getNextSibling();
                            if (afterProc!=null)
                            {
                                if (afterProc.Type == BoolParserTokenTypes.LSTMT)
                                    afterProc = (CommonAST)afterProc.getFirstChild().getNextSibling();
                                else
                                    afterProc = (CommonAST)afterProc.getFirstChild();                    
                            }
                            if ((afterProc == null) || (afterProc.Type != BoolParserTokenTypes.LITERAL_skip))
                                Errors.Add(new SemanticError(BoolSemanticErrorTypes.SKIP_EXPECTED,
                                        subWalker.getText(), ProcOf));

                            string ProcCallName = subWalker.getText();
                            subWalker = (CommonAST)subWalker.getFirstChild();
                            int numParams=0;
                            while (subWalker != null)
                            {
                                numParams++;
                                Errors = CheckExpression(subWalker, Errors, LocalVariables, ProcOf);
                                subWalker = (CommonAST)subWalker.getNextSibling();
                            }
                            int realNumParams;
                            ProcedureToNumParam.TryGetValue(ProcCallName,out realNumParams);
                            if(numParams!=realNumParams)
                                Errors.Add(new SemanticError(BoolSemanticErrorTypes.WRONG_PARAM_NUM,
                                    ProcCallName,ProcOf));
                        }
                        else
                        {
                            Errors.Add(new SemanticError(BoolSemanticErrorTypes.PROC_UNDECLARED,
                                        subWalker.getText(),ProcOf));
                        }
                        break;
                }

                walker = (CommonAST)walker.getNextSibling();
            }

            return Errors;
        }
        
        //This Procedure assumes Procedure is a PROC AST node
        private List<SemanticError> CheckProcedure(CommonAST Procedure, List<SemanticError> Errors)
        {
            List<string> LocalVariables = new List<string>();

            CommonAST walker = Procedure;

            walker = (CommonAST)walker.getFirstChild();
            string ProcedureName = walker.getText();
            walker = (CommonAST)walker.getFirstChild();

            #region Variables checking
            while (walker.Type == BoolParserTokenTypes.ID)
            {
                string variable = walker.ToString();
                if (LocalVariables.Contains(variable))
                {
                    Errors.Add(new SemanticError(BoolSemanticErrorTypes.VAR_NAME_CLASH, variable, ProcedureName));
                }
                else
                    LocalVariables.Add(variable);

                walker = (CommonAST)walker.getNextSibling();
            }

            while ((walker.Type == BoolParserTokenTypes.LITERAL_decl))
            {
                CommonAST subWalker = (CommonAST)walker.getFirstChild();

                while (subWalker != null)
                {
                    string variable = subWalker.ToString();
                    if (LocalVariables.Contains(variable))
                    {
                        Errors.Add(new SemanticError(BoolSemanticErrorTypes.VAR_NAME_CLASH, variable, ProcedureName));
                    }
                    else
                        LocalVariables.Add(variable);

                    subWalker = (CommonAST)subWalker.getNextSibling();
                }

                walker = (CommonAST)walker.getNextSibling();
            }
            #endregion


            Errors = CheckSSEQ(walker, Errors, LocalVariables,ProcedureName);

            return Errors;
        }

        public List<SemanticError> Check()
        {
            List<SemanticError> Errors = new List<SemanticError>();

            GlobalVariables.Clear();
            ProcedureNames.Clear();
            ProcedureToNumParam.Clear();

            CommonAST walker = BoolProgram;

            while ((walker != null) && (walker.Type == BoolParserTokenTypes.LITERAL_decl))
            {
                CommonAST subWalker = (CommonAST)walker.getFirstChild();

                while (subWalker != null)
                {
                    string variable = subWalker.ToString();
                    if (GlobalVariables.Contains(variable))
                    {
                        Errors.Add(new SemanticError(BoolSemanticErrorTypes.VAR_NAME_CLASH, variable));
                    }
                    else
                        GlobalVariables.Add(variable);

                    subWalker = (CommonAST)subWalker.getNextSibling();
                }

                walker = (CommonAST)walker.getNextSibling();
            }

            CommonAST procWalker = walker;
            while (walker != null)
            {
                string procName = walker.getFirstChild().getText();

                if (ProcedureNames.Contains(procName))
                {
                    Errors.Add(new SemanticError(BoolSemanticErrorTypes.PROC_NAME_CLASH, procName));
                }
                else
                {
                    ProcedureNames.Add(procName);
                    CommonAST subWalker = (CommonAST)walker.getFirstChild().getFirstChild();

                    int numParameters = 0;
                    while (subWalker.Type == BoolParserTokenTypes.ID)
                    {
                        numParameters++;
                        subWalker = (CommonAST)subWalker.getNextSibling();
                    }

                    ProcedureToNumParam.Add(procName, numParameters);
                }

                walker = (CommonAST)walker.getNextSibling();
            }

            while (procWalker != null)
            {
                Errors = CheckProcedure(procWalker, Errors);

                procWalker = (CommonAST)procWalker.getNextSibling();
            }

            if (!(ProcedureNames.Contains("main")))
                Errors.Add(new SemanticError(BoolSemanticErrorTypes.MAIN_MISSING,""));

            return Errors;
        }
    }
}
