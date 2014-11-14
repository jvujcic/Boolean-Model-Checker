using System;
using System.Collections.Generic;
using System.Text;

namespace BooleanModelChecker.BoolSemantics
{
    class SemanticError
    {
        private int Type;
        private string ProcOf;
        private string Data;


        public SemanticError(int ErrorType, string ErrorData)
            : this(ErrorType, ErrorData, null)
        { 
        }

        public SemanticError(int ErrorType, string ErrorData , string ProcOfError)           
        {
            Type = ErrorType;
            Data = ErrorData;
            ProcOf = ProcOfError;
        }   

        public int GetType()
        {
            return Type;
        }

        public string ToString()
        {
            switch (Type)
            {
                case BoolSemanticErrorTypes.VAR_NAME_CLASH:
                    if (ProcOf == null)
                        return "Global variable " + Data + " redeclared.";
                    else
                        return "Local variable " + Data + " redeclared in procedure " + ProcOf + "."; 
                case BoolSemanticErrorTypes.PROC_NAME_CLASH:
                    return "Procedure \"" + Data + "\" redefined.";
                case BoolSemanticErrorTypes.SKIP_EXPECTED:
                    return "Skip statement expected after call to " + Data + " in procedure " + ProcOf + ".";
                case BoolSemanticErrorTypes.MAIN_MISSING:
                    return "Procedure main is missing from the program.";
                case BoolSemanticErrorTypes.VAR_UNDECLARED:
                    return "Undeclared variable " + Data + " used in procedure " + ProcOf + ".";
                case BoolSemanticErrorTypes.PROC_UNDECLARED:
                    return "Call to undeclared procedure " + Data + " in procedure " + ProcOf + ".";
                case BoolSemanticErrorTypes.WRONG_PARAM_NUM:
                    return "Wrong number of paremeters in a call to " + Data + " from " + ProcOf + "."; 
                default:
                    return "Unrecognized Semantic error.";
            }
        }
    }
}
