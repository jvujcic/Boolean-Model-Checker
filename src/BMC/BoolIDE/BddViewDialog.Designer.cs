namespace BoolIDE
{
    partial class BddViewDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.bddPathEdgeLabel = new System.Windows.Forms.Label();
            this.bddSummaryEdgeLabel = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.bddTransferComboBox = new System.Windows.Forms.ComboBox();
            this.bddPathEdgesControl = new BddGuiControl.BddControl();
            this.bddSummaryEdgesControl = new BddGuiControl.BddControl();
            this.bddTransferControl = new BddGuiControl.BddControl();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // bddPathEdgeLabel
            // 
            this.bddPathEdgeLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.bddPathEdgeLabel.Location = new System.Drawing.Point(3, 0);
            this.bddPathEdgeLabel.Name = "bddPathEdgeLabel";
            this.bddPathEdgeLabel.Size = new System.Drawing.Size(339, 20);
            this.bddPathEdgeLabel.TabIndex = 7;
            this.bddPathEdgeLabel.Text = "Path Edges";
            this.bddPathEdgeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // bddSummaryEdgeLabel
            // 
            this.bddSummaryEdgeLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.bddSummaryEdgeLabel.Location = new System.Drawing.Point(3, 0);
            this.bddSummaryEdgeLabel.Name = "bddSummaryEdgeLabel";
            this.bddSummaryEdgeLabel.Size = new System.Drawing.Size(361, 20);
            this.bddSummaryEdgeLabel.TabIndex = 9;
            this.bddSummaryEdgeLabel.Text = "Summary Edge";
            this.bddSummaryEdgeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tableLayoutPanel1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(1104, 652);
            this.splitContainer1.SplitterDistance = 349;
            this.splitContainer1.TabIndex = 13;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.bddPathEdgeLabel, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.bddPathEdgesControl, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(345, 648);
            this.tableLayoutPanel1.TabIndex = 8;
            // 
            // splitContainer2
            // 
            this.splitContainer2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.tableLayoutPanel2);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.tableLayoutPanel3);
            this.splitContainer2.Size = new System.Drawing.Size(751, 652);
            this.splitContainer2.SplitterDistance = 371;
            this.splitContainer2.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.bddSummaryEdgesControl, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.bddSummaryEdgeLabel, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(367, 648);
            this.tableLayoutPanel2.TabIndex = 11;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Controls.Add(this.bddTransferComboBox, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.bddTransferControl, 0, 1);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 514F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(372, 648);
            this.tableLayoutPanel3.TabIndex = 13;
            // 
            // bddTransferComboBox
            // 
            this.bddTransferComboBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.bddTransferComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.bddTransferComboBox.FormattingEnabled = true;
            this.bddTransferComboBox.Items.AddRange(new object[] {
            "Transfer True",
            "Transfer False"});
            this.bddTransferComboBox.Location = new System.Drawing.Point(3, 3);
            this.bddTransferComboBox.Name = "bddTransferComboBox";
            this.bddTransferComboBox.Size = new System.Drawing.Size(366, 21);
            this.bddTransferComboBox.TabIndex = 14;
            this.bddTransferComboBox.SelectedIndexChanged += new System.EventHandler(this.bddTransferComboBox_SelectedIndexChanged);
            // 
            // bddPathEdgesControl
            // 
            this.bddPathEdgesControl.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.bddPathEdgesControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bddPathEdgesControl.Location = new System.Drawing.Point(3, 23);
            this.bddPathEdgesControl.Name = "bddPathEdgesControl";
            this.bddPathEdgesControl.Size = new System.Drawing.Size(339, 622);
            this.bddPathEdgesControl.TabIndex = 7;
            // 
            // bddSummaryEdgesControl
            // 
            this.bddSummaryEdgesControl.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.bddSummaryEdgesControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bddSummaryEdgesControl.Location = new System.Drawing.Point(3, 23);
            this.bddSummaryEdgesControl.Name = "bddSummaryEdgesControl";
            this.bddSummaryEdgesControl.Size = new System.Drawing.Size(361, 622);
            this.bddSummaryEdgesControl.TabIndex = 10;
            // 
            // bddTransferControl
            // 
            this.bddTransferControl.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.bddTransferControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bddTransferControl.Location = new System.Drawing.Point(3, 23);
            this.bddTransferControl.Name = "bddTransferControl";
            this.bddTransferControl.Size = new System.Drawing.Size(366, 622);
            this.bddTransferControl.TabIndex = 12;
            // 
            // BddViewDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1104, 652);
            this.Controls.Add(this.splitContainer1);
            this.Name = "BddViewDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "BddViewDialog";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.BddViewDialog_FormClosing);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        internal BddGuiControl.BddControl bddPathEdgesControl;
        internal System.Windows.Forms.Label bddPathEdgeLabel;
        internal System.Windows.Forms.Label bddSummaryEdgeLabel;
        internal BddGuiControl.BddControl bddSummaryEdgesControl;
        internal BddGuiControl.BddControl bddTransferControl;
        internal System.Windows.Forms.ComboBox bddTransferComboBox;
        internal BddGuiControl.BddControl bddTransferTrueControl;
        internal BddGuiControl.BddControl bddTransferFalseControl;
    }
}