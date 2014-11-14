using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

using CommonAST = antlr.CommonAST;

namespace BooleanModelChecker.ControlFlowGraph
{
    class CFGStatementNodeFactory
    {
        public static CFGNodeStatement Make(CommonAST NodeAST)
        {
            switch (NodeAST.Type)
            {
                case BoolParserTokenTypes.LITERAL_skip : 
                    return new CFGNodeStmtSkip(NodeAST);
                case BoolParserTokenTypes.LITERAL_print:
                    return new CFGNodeStmtPrint(NodeAST);
                case BoolParserTokenTypes.LITERAL_goto:
                    return new CFGNodeStmtGoto(NodeAST);
                case BoolParserTokenTypes.LITERAL_return:
                    return new CFGNodeStmtReturn(NodeAST);
                case BoolParserTokenTypes.ASSIGNMENT:
                    return new CFGNodeStmtAssignment(NodeAST);
                case BoolParserTokenTypes.LITERAL_if:
                    return new CFGNodeStmtIf(NodeAST);
                case BoolParserTokenTypes.LITERAL_while:
                    return new CFGNodeStmtWhile(NodeAST);
                case BoolParserTokenTypes.LITERAL_assert:
                    return new CFGNodeStmtAssert(NodeAST);
                case BoolParserTokenTypes.PROCCALL:
                    return new CFGNodeStmtProcCall(NodeAST);
                default :
                    Debug.Assert(false);
                    return null;
            }
        }
    }
}
