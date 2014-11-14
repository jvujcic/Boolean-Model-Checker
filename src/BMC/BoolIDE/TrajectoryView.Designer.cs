namespace BoolIDE
{
    partial class TrajectoryView
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.codeView = new System.Windows.Forms.RichTextBox();
            this.trajectorDescription = new System.Windows.Forms.RichTextBox();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.codeView);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.trajectorDescription);
            this.splitContainer1.Size = new System.Drawing.Size(751, 521);
            this.splitContainer1.SplitterDistance = 232;
            this.splitContainer1.SplitterWidth = 6;
            this.splitContainer1.TabIndex = 0;
            // 
            // codeView
            // 
            this.codeView.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(246)))), ((int)(((byte)(246)))));
            this.codeView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.codeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.codeView.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.codeView.Location = new System.Drawing.Point(0, 0);
            this.codeView.Name = "codeView";
            this.codeView.ReadOnly = true;
            this.codeView.Size = new System.Drawing.Size(232, 521);
            this.codeView.TabIndex = 1;
            this.codeView.Text = "";
            this.codeView.WordWrap = false;
            // 
            // trajectorDescription
            // 
            this.trajectorDescription.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(246)))), ((int)(((byte)(246)))));
            this.trajectorDescription.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.trajectorDescription.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trajectorDescription.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.trajectorDescription.Location = new System.Drawing.Point(0, 0);
            this.trajectorDescription.Name = "trajectorDescription";
            this.trajectorDescription.ReadOnly = true;
            this.trajectorDescription.Size = new System.Drawing.Size(513, 521);
            this.trajectorDescription.TabIndex = 0;
            this.trajectorDescription.Text = "";
            // 
            // TrajectoryView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(751, 521);
            this.Controls.Add(this.splitContainer1);
            this.Name = "TrajectoryView";
            this.Text = "TrajectoryView";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TrajectoryView_FormClosing);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        internal System.Windows.Forms.RichTextBox trajectorDescription;
        internal System.Windows.Forms.RichTextBox codeView;
    }
}