namespace BddGuiControl
{
    partial class BddControl
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

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.gViewerBdd = new Microsoft.Glee.GraphViewerGdi.GViewer();
            this.checkBoxOnlyTrueBranches = new System.Windows.Forms.CheckBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gViewerBdd
            // 
            this.gViewerBdd.AsyncLayout = false;
            this.gViewerBdd.AutoScroll = true;
            this.gViewerBdd.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.gViewerBdd.BackwardEnabled = false;
            this.gViewerBdd.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gViewerBdd.ForwardEnabled = false;
            this.gViewerBdd.Graph = null;
            this.gViewerBdd.Location = new System.Drawing.Point(3, 3);
            this.gViewerBdd.MouseHitDistance = 0.05;
            this.gViewerBdd.Name = "gViewerBdd";
            this.gViewerBdd.NavigationVisible = true;
            this.gViewerBdd.PanButtonPressed = false;
            this.gViewerBdd.SaveButtonVisible = true;
            this.gViewerBdd.Size = new System.Drawing.Size(787, 576);
            this.gViewerBdd.TabIndex = 0;
            this.gViewerBdd.ZoomF = 1;
            this.gViewerBdd.ZoomFraction = 0.5;
            this.gViewerBdd.ZoomWindowThreshold = 0.05;
            // 
            // checkBoxOnlyTrueBranches
            // 
            this.checkBoxOnlyTrueBranches.Checked = true;
            this.checkBoxOnlyTrueBranches.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxOnlyTrueBranches.Location = new System.Drawing.Point(3, 585);
            this.checkBoxOnlyTrueBranches.Name = "checkBoxOnlyTrueBranches";
            this.checkBoxOnlyTrueBranches.Size = new System.Drawing.Size(157, 19);
            this.checkBoxOnlyTrueBranches.TabIndex = 1;
            this.checkBoxOnlyTrueBranches.Text = "Show Only True Branches";
            this.checkBoxOnlyTrueBranches.UseVisualStyleBackColor = true;
            this.checkBoxOnlyTrueBranches.CheckedChanged += new System.EventHandler(this.checkBoxOnlyTrueBranches_CheckedChanged);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.gViewerBdd, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.checkBoxOnlyTrueBranches, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(793, 608);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // BddControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "BddControl";
            this.Size = new System.Drawing.Size(793, 608);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Glee.GraphViewerGdi.GViewer gViewerBdd;
        private System.Windows.Forms.CheckBox checkBoxOnlyTrueBranches;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    }
}
