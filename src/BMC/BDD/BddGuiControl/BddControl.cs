using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using BDDlib;
using Microsoft.Glee.Drawing;

namespace BddGuiControl
{
    public partial class BddControl : UserControl
    {
        private BddManager m_Manager;
        private Bdd m_BddRoot;
        private List<string> m_BddVarToName;
        private List<Edge> m_pathEdges;

        public List<string> BddVariableToName
        {
            private get
            {
                return m_BddVarToName;
            }
            set
            {
                m_BddVarToName = value;
            }
        }

        public BddManager BddManager
        {
            private get
            {
                return m_Manager;
            }
            set
            {
                m_Manager = value;
            }
        }


        public Bdd BddRoot
        {
            private get
            {
                return m_BddRoot;
            }
            set
            {
                m_BddRoot = value;
            }
        }


        private void MakeTreeFromBdd(Bdd f, Node parent, Graph m_BddGraph ,bool branch)
        {
            INode nodeInGraph = m_BddGraph.FindNode(f.UniqueKey().ToString());

            Node tempNode = new Node("a");
            Edge tempEdge = new Edge("a","b","c");

            if (parent != null)
            {
                if (f.ReturnBddType() != BddType.One && f.ReturnBddType() != BddType.Zero)
                {
                    tempNode = m_BddGraph.AddNode(f.UniqueKey().ToString()) as Node;
                    tempNode.Attr.Label = m_BddVarToName[f.GetBddRootVariableID()];
                    tempNode.Attr.LineWidth = 2;
                    tempEdge = m_BddGraph.AddEdge(parent.Id, tempNode.Id) as Edge;
                    m_pathEdges.Add(tempEdge);
                }
                else
                {
                    if (f.ReturnBddType() == BddType.One)
                    {
                        tempEdge = m_BddGraph.AddEdge(parent.Id, "1") as Edge;
                        m_pathEdges.Add(tempEdge);
                        foreach (Edge edgeOfPathEdge in m_pathEdges)
                        {
                            edgeOfPathEdge.Attr.LineWidth = 2;
                            edgeOfPathEdge.Attr.Fontcolor = Microsoft.Glee.Drawing.Color.Blue;
                            edgeOfPathEdge.Attr.Color = Microsoft.Glee.Drawing.Color.Blue;
                            edgeOfPathEdge.Attr.Fontsize = 12;
                            edgeOfPathEdge.Attr.Weight = 1000;
                            (m_BddGraph.FindNode((edgeOfPathEdge.Source)) as Node).UserData = true;
                        }
                    }

                    else
                    {
                        if (!checkBoxOnlyTrueBranches.Checked)
                        {
                            tempEdge = m_BddGraph.AddEdge(parent.Id, "0") as Edge;
                            tempEdge.Attr.Color = Microsoft.Glee.Drawing.Color.MediumPurple;
                            tempEdge.Attr.Fontcolor = Microsoft.Glee.Drawing.Color.MediumPurple;
                            tempEdge.Attr.Fontsize = 8;
                        }
                    }
                }

                if (branch) tempEdge.Attr.Label = "1";
                else tempEdge.Attr.Label = "0";
                
            }
            else
            {
                if (f.ReturnBddType() == BddType.One) m_BddGraph.AddNode("1");
                else if (f.ReturnBddType() == BddType.Zero) m_BddGraph.AddNode("0");
                else
                {
                    tempNode = m_BddGraph.AddNode(f.UniqueKey().ToString()) as Node;
                    tempNode.Attr.LineWidth = 2;
                    tempNode.Attr.Label = m_BddVarToName[f.GetBddRootVariableID()];
                }
            }

            if (nodeInGraph != null)
            {
                if((bool)((nodeInGraph as Node).UserData))
                {
                    foreach (Edge edgeOfPathEdge in m_pathEdges)
                    {
                        edgeOfPathEdge.Attr.LineWidth = 2;
                        edgeOfPathEdge.Attr.Fontsize = 12;
                        edgeOfPathEdge.Attr.Fontcolor = Microsoft.Glee.Drawing.Color.Blue;
                        edgeOfPathEdge.Attr.Color = Microsoft.Glee.Drawing.Color.Blue;
                        (m_BddGraph.FindNode((edgeOfPathEdge.Source)) as Node).UserData = true;
                    }
                }
            }

            tempNode.UserData = new bool();
            tempNode.UserData = false;            

            if (f.ReturnBddType() != BddType.One && f.ReturnBddType() != BddType.Zero && nodeInGraph == null)
            {

                MakeTreeFromBdd(f.GetElseBranch(), tempNode, m_BddGraph, false);
                MakeTreeFromBdd(f.GetThenBranch(), tempNode, m_BddGraph, true);
                m_pathEdges.Remove(tempEdge);
            }
            else
            {
                return;
            }
        }


        public BddControl()
        {
            m_pathEdges = new List<Edge>();
            InitializeComponent();
        }

        public void DrawGraph()
        {
            Graph bddGraph = new Graph("BDD Graph");
            m_pathEdges.Clear();           

            if (m_BddRoot != null)
            {
                MakeTreeFromBdd(m_BddRoot, null, bddGraph, true);
            }

            gViewerBdd.Graph = bddGraph;        
        }

        private void checkBoxOnlyTrueBranches_CheckedChanged(object sender, EventArgs e)
        {
            DrawGraph();
        }
    }
}