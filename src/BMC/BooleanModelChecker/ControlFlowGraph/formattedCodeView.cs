using System;
using System.Collections.Generic;
using System.Text;

namespace BooleanModelChecker.ControlFlowGraph
{
    public class formattedCodeView
    {
        List<string> m_CodeLine;
        List<string> CodeLine
        {
            get { return m_CodeLine; }
        }

        Dictionary<CFGNode, int> m_nodeToLine;
        public Dictionary<CFGNode, int> nodeToLine
        {
            get { return m_nodeToLine; }
        }

        public formattedCodeView()
        {
            m_CodeLine = new List<string>();
            m_nodeToLine = new Dictionary<CFGNode, int>();
        }

        public int addLine(string codeLine)
        {
            m_CodeLine.Add(codeLine);

            return m_CodeLine.Count - 1;
        }

        public int addLine(string codeLine, CFGNode node)
        {
            m_CodeLine.Add(codeLine);

            if (node != null)
            {
                m_nodeToLine.Add(node, m_CodeLine.Count - 1);
            }

            return m_CodeLine.Count - 1;
        }

        public string getProgramText()
        {
            return getProgramText(false);
        }
        public string getProgramTextNumbered()
        {
            return getProgramText(true);
        }

        private string getProgramText(bool numberedLines)
        {
            string program = string.Empty;
            for (int i = 0; i < m_CodeLine.Count; i++)
            {
                string lineNum = string.Empty;
                if (numberedLines)
                {
                    if (m_nodeToLine.ContainsValue(i))
                    {
                        lineNum = "[" + i.ToString() + "]";
                        int length = lineNum.Length;
                        for (int j = 0; j < (6 - length); j++)
                            lineNum += " ";
                    } else
                    { 
                        lineNum = "      ";
                    }

                } 

                program += lineNum + m_CodeLine[i] + "\n";
            }

            return program;
        }

    }
}
