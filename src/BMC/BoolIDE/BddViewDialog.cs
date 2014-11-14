using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BoolIDE
{
    public partial class BddViewDialog : Form
    {
        public BddViewDialog()
        {
            InitializeComponent();
            bddTransferFalseControl = new BddGuiControl.BddControl();
            bddTransferTrueControl = new BddGuiControl.BddControl();

            this.bddTransferTrueControl.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.bddTransferTrueControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bddTransferTrueControl.Location = new System.Drawing.Point(3, 23);
            this.bddTransferTrueControl.Name = "bddTransferControl";
            this.bddTransferTrueControl.Size = new System.Drawing.Size(388, 536);

            this.bddTransferFalseControl.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.bddTransferFalseControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bddTransferFalseControl.Location = new System.Drawing.Point(3, 23);
            this.bddTransferFalseControl.Name = "bddTransferControl";
            this.bddTransferFalseControl.Size = new System.Drawing.Size(388, 536);
        }

        private void BddViewDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                this.Hide();
                e.Cancel = true;
            }
        }

        private void bddTransferComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (bddTransferComboBox.SelectedItem as String == "Transfer True")
            {
                this.tableLayoutPanel3.Controls.Remove(this.bddTransferControl);
                this.tableLayoutPanel3.Controls.Remove(this.bddTransferFalseControl);
                this.tableLayoutPanel3.Controls.Add(this.bddTransferTrueControl, 0, 1);
            }
            else
            {
                this.tableLayoutPanel3.Controls.Remove(this.bddTransferControl);
                this.tableLayoutPanel3.Controls.Remove(this.bddTransferTrueControl);
                this.tableLayoutPanel3.Controls.Add(this.bddTransferFalseControl, 0, 1);
            }
        }

        internal void CleanAllBdd()
        {                                    
            this.tableLayoutPanel3.Controls.Remove(this.bddTransferTrueControl);
            this.tableLayoutPanel3.Controls.Remove(this.bddTransferFalseControl);
            this.tableLayoutPanel3.Controls.Remove(this.bddTransferControl);

            this.tableLayoutPanel3.Controls.Add(this.bddTransferControl, 0, 1);

            this.bddTransferControl.BddRoot = null;
            this.bddTransferFalseControl.BddRoot = null;
            this.bddSummaryEdgesControl.BddRoot = null;
            this.bddPathEdgesControl.BddRoot = null;
            this.bddTransferTrueControl.BddRoot = null;
            this.bddTransferFalseControl.BddRoot = null;

            this.bddPathEdgesControl.DrawGraph();
            this.bddSummaryEdgesControl.DrawGraph();
            this.bddTransferControl.DrawGraph();
            this.bddTransferTrueControl.DrawGraph();
            this.bddTransferFalseControl.DrawGraph();
        }
    }
}