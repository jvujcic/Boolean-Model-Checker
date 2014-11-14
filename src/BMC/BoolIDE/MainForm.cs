using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Microsoft.Glee.Drawing;
using BooleanModelChecker;
using BDDlib;
using BooleanModelChecker.ControlFlowGraph;

namespace BoolIDE
{
    public partial class MainForm : Form
    {
        private BddViewDialog BddViewForm;
        private TrajectoryView TrajectoryViewForm;
        private BddMemoryInfoDialog BddMemoryInfo;
        private BMC MyBMC;
        private BddManager m_BddManager;
        private List<string> m_BddVarToName;

        //to record if changes were recently made to the program code
        bool ProgramInvalidated = true;

        public MainForm()
        {
            BddViewForm = new BddViewDialog();
            AddOwnedForm(BddViewForm);

            TrajectoryViewForm = new TrajectoryView();
            AddOwnedForm(TrajectoryViewForm);

            BddMemoryInfo = new BddMemoryInfoDialog();
            AddOwnedForm(BddMemoryInfo);

            InitializeComponent();

            MyBMC = new BMC();
            m_BddManager = new BddManager();
            m_BddVarToName = new List<string>(100);
            m_BddVarToName.Insert(0, "");
        }

        private void ValidateProgram()
        {
            if (ProgramInvalidated)
            {
                MyBMC.ParseProgram(codeBox.Text);
                ErrorLogBox.Text = MyBMC.GetParserErrorLog();

                Graph g = new Graph("N/A");
                gleeViewCFG.Graph = g;

                ProgramInvalidated = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ValidateProgram();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openBProgDialog.ShowDialog() == DialogResult.OK)
            {
                codeBox.LoadFile(openBProgDialog.FileName);
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveBProgDialog.ShowDialog() == DialogResult.OK)
            {
                codeBox.SaveFile(saveBProgDialog.FileName);
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnDrawAST_Click(object sender, EventArgs e)
        {
            ValidateProgram();

            gViewer.Graph = MyBMC.GetASTasGLEEGraph();
        }

        private void btnBuildCFG_Click(object sender, EventArgs e)
        {
            ValidateProgram();

            gViewer.Graph = MyBMC.GetCFGasGLEEGraph();
        }

        private void btnDrawNextFunc_Click(object sender, EventArgs e)
        {
            ValidateProgram();

            gViewer.Graph = MyBMC.GetCFGNextFunctionAsGLEEGraph();
        }

        object OldSelectedObject = null;
        private void gViewer_SelectionChanged(object sender, EventArgs e)
        {
            if (OldSelectedObject != null)
            {
                if (OldSelectedObject is Node)
                {
                    (OldSelectedObject as Node).Attr.Color = Microsoft.Glee.Drawing.Color.Black;
                }
            }

            if (gViewer.SelectedObject != null)
            {
                if (gViewer.SelectedObject is Node)
                {
                    (gViewer.SelectedObject as Node).Attr.Color = Microsoft.Glee.Drawing.Color.Magenta;

                    OldSelectedObject = gViewer.SelectedObject;
                }
            }

            gViewer.Invalidate();
        }
       
        private void btnRunReachabilityAlg_Click(object sender, EventArgs e)
        {
            ValidateProgram();

            gleeViewCFG.Graph = MyBMC.GetCFGasGLEEGraph();            

            reachDebugBox.Clear();

            debugPrintDelegate matrix = delegate(string message)
            {
                reachDebugBox.AppendText(message);
                reachDebugBox.Refresh();
            };
     
            MyBMC.Reachable(matrix);

            labelsComboBox.Items.Clear();
            labelsComboBox.Items.AddRange(MyBMC.GetStatementLabels());
            if (labelsComboBox.Items.Count > 0)
            {
                labelsComboBox.SelectedIndex = 0;
            }
            else
            {
                labelsComboBox.Text = "Code contains no Labels!";
            }
        }       

        object OldCFGSelectedObject = null;
        private void gleeViewCFG_SelectionChanged(object sender, EventArgs e)
        {
            if (OldCFGSelectedObject != null)
            {
                if (OldCFGSelectedObject is Node)
                {
                    (OldCFGSelectedObject as Node).Attr.Color = Microsoft.Glee.Drawing.Color.Black;
                    (OldCFGSelectedObject as Node).Attr.LineWidth = 1;
                }
            }

            if (gleeViewCFG.SelectedObject != null)
            {
                if (gleeViewCFG.SelectedObject is Node)
                {
                    (gleeViewCFG.SelectedObject as Node).Attr.Color = Microsoft.Glee.Drawing.Color.DarkBlue;
                    (gleeViewCFG.SelectedObject as Node).Attr.LineWidth = 3;

                    OldCFGSelectedObject = gleeViewCFG.SelectedObject;
                }
            }

            gleeViewCFG.Invalidate();
        }

        private void gleeViewCFG_MouseClick(object sender, MouseEventArgs e)
        {                        
            if ((gleeViewCFG.Graph != null) && (gleeViewCFG.SelectedObject != null))
            {
                if (gleeViewCFG.SelectedObject is Node)
                {

                    BddViewForm.CleanAllBdd();

                    BddViewForm.bddPathEdgeLabel.Text = "Path Edges for: " + (gleeViewCFG.SelectedObject as Node).Attr.Label;

                    BddViewForm.bddSummaryEdgeLabel.Text = "Summary Edges for: " + (gleeViewCFG.SelectedObject as Node).Attr.Label;

                 //   BddViewForm.bddTransferLabel.Text = "Transfer Function for: " + (gleeViewCFG.SelectedObject as Node).Attr.Label;

                    int id = int.Parse((gleeViewCFG.SelectedObject as Node).Id);
                    
                    BddViewForm.bddPathEdgesControl.BddRoot = MyBMC.GetCFGPathEdgeByHashCode(id);
                    BddViewForm.bddPathEdgesControl.BddManager = MyBMC.GetBddManager;
                    BddViewForm.bddPathEdgesControl.BddVariableToName = MyBMC.BddToName;

                    BddViewForm.bddPathEdgesControl.DrawGraph();

                    BddViewForm.bddSummaryEdgesControl.BddRoot = MyBMC.GetCFGSummaryEdgeByHashCode(id);
                    BddViewForm.bddSummaryEdgesControl.BddManager = MyBMC.GetBddManager;
                    BddViewForm.bddSummaryEdgesControl.BddVariableToName = MyBMC.BddToName;

                    BddViewForm.bddSummaryEdgesControl.DrawGraph();


                    if(!BddViewForm.bddTransferComboBox.Items.Contains("Transfer"))
                        BddViewForm.bddTransferComboBox.Items.Add("Transfer");
                    
                    BddViewForm.bddTransferComboBox.SelectedItem = "Transfer";
                    BddViewForm.bddTransferComboBox.Enabled = false;


                    if (MyBMC.GetCFGTransferTrueByHashCode(id) != null)
                    {
                        BddViewForm.bddTransferTrueControl.BddRoot = MyBMC.GetCFGTransferTrueByHashCode(id);
                        BddViewForm.bddTransferFalseControl.BddRoot = MyBMC.GetCFGTransferFalseByHashCode(id);

                        BddViewForm.bddTransferTrueControl.BddManager = MyBMC.GetBddManager;
                        BddViewForm.bddTransferFalseControl.BddManager = MyBMC.GetBddManager;

                        BddViewForm.bddTransferTrueControl.BddVariableToName = MyBMC.BddToName;
                        BddViewForm.bddTransferFalseControl.BddVariableToName = MyBMC.BddToName;

                        BddViewForm.bddTransferTrueControl.DrawGraph();
                        BddViewForm.bddTransferFalseControl.DrawGraph();

                        BddViewForm.bddTransferComboBox.Enabled = true;

                        BddViewForm.bddTransferComboBox.Items.Remove("Transfer");
                        BddViewForm.bddTransferComboBox.SelectedItem = "Transfer True";
                    }
                    else
                    {
                        BddViewForm.bddTransferControl.BddRoot = MyBMC.GetCFGTransferByHashCode(id);
                        BddViewForm.bddTransferControl.BddManager = MyBMC.GetBddManager;
                        BddViewForm.bddTransferControl.BddVariableToName = MyBMC.BddToName;
                        BddViewForm.bddTransferControl.DrawGraph();

                        
                    }
                    
                    BddViewForm.Show();
                    BddViewForm.WindowState = FormWindowState.Normal;

                }
            }
        }

        private void buttonTrajectory_Click(object sender, EventArgs e)
        {
            if (MyBMC.isLabelValid(labelsComboBox.Text))
            {
                BooleanModelChecker.ControlFlowGraph.formattedCodeView formattedCode = MyBMC.getFormattedCode();

                TrajectoryViewForm.codeView.Clear();
                TrajectoryViewForm.codeView.AppendText(formattedCode.getProgramTextNumbered());

                Trajectory labelTrajectory = MyBMC.BuildTrajectory(MyBMC.GetLabeledCFGNode(labelsComboBox.Text));

                TrajectoryViewForm.trajectorDescription.Clear();
                TrajectoryViewForm.trajectorDescription.AppendText("Trajectory printed with matching line numbers:\n\n");
                TrajectoryViewForm.trajectorDescription.
                    AppendText(Trajectory.getTrajectoryPrintout(labelTrajectory, formattedCode.nodeToLine,MyBMC.BddToName));

                TrajectoryViewForm.Show();
                TrajectoryViewForm.WindowState = FormWindowState.Normal;
            }
            else
            {
                labelsComboBox.Text = "Invalid Label Selected!";
            }
        }

        private void codeBox_TextChanged(object sender, EventArgs e)
        {
            ProgramInvalidated = true;
        }

        private void bddMemoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BddMemoryInfo.WindowState = FormWindowState.Normal;
            BddMemoryInfo.Show();
            BddMemoryInfo.bddMemoryInfoTextBox.Clear();
            BddMemoryInfo.bddMemoryInfoTextBox.Text = MyBMC.GetBddManager.Statistics();
        }

    }
}
