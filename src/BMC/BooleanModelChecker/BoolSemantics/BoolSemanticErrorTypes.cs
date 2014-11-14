using System;
using System.Collections.Generic;
using System.Text;

namespace BooleanModelChecker.BoolSemantics
{
    class BoolSemanticErrorTypes
    {
        public const int PROC_NAME_CLASH = 1;
        public const int VAR_NAME_CLASH = 2;
        public const int SKIP_EXPECTED = 3;
        public const int MAIN_MISSING = 4;
        public const int VAR_UNDECLARED = 5;
        public const int PROC_UNDECLARED = 6;
        public const int WRONG_PARAM_NUM = 7;
     }
}
