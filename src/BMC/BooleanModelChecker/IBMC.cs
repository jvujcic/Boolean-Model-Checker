using System;
namespace BooleanModelChecker
{
    interface IBMC
    {
        Microsoft.Glee.Drawing.Graph GetASTasGLEEGraph();
        Microsoft.Glee.Drawing.Graph GetCFGasGLEEGraph();
        Microsoft.Glee.Drawing.Graph GetCFGNextFunctionAsGLEEGraph();
        string GetParserErrorLog();
        bool ParseProgram(string BooleanProgram);
    }
}
