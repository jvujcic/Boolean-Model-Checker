using System;
using System.Collections.Generic;
using System.Text;

namespace BooleanModelChecker.ControlFlowGraph
{
    class CFGNodeError : CFGNode
    {
        override public string ToString()
        {
            return "Error Node!";
        }
    }
}
