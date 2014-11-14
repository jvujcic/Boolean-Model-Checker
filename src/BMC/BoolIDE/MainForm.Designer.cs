namespace BoolIDE
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.btnValidate = new System.Windows.Forms.Button();
            this.ErrorLogBox = new System.Windows.Forms.RichTextBox();
            this.MainFormMenu = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.infoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bddMemoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openBProgDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveBProgDialog = new System.Windows.Forms.SaveFileDialog();
            this.CodePanel = new System.Windows.Forms.Panel();
            this.codeBox = new System.Windows.Forms.RichTextBox();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.btnRunReachabilityAlg = new System.Windows.Forms.Button();
            this.labelsComboBox = new System.Windows.Forms.ComboBox();
            this.buttonTrajectory = new System.Windows.Forms.Button();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.gleeViewCFG = new Microsoft.Glee.GraphViewerGdi.GViewer();
            this.reachDebugBox = new System.Windows.Forms.RichTextBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.btnDrawAST = new System.Windows.Forms.Button();
            this.btnBuildCFG = new System.Windows.Forms.Button();
            this.btnDrawNextFunc = new System.Windows.Forms.Button();
            this.gViewer = new Microsoft.Glee.GraphViewerGdi.GViewer();
            this.MainFormMenu.SuspendLayout();
            this.CodePanel.SuspendLayout();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnValidate
            // 
            this.btnValidate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.btnValidate.BackColor = System.Drawing.SystemColors.MenuBar;
            this.btnValidate.FlatAppearance.BorderSize = 3;
            this.btnValidate.Font = new System.Drawing.Font("Comic Sans MS", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnValidate.Location = new System.Drawing.Point(0, 424);
            this.btnValidate.Name = "btnValidate";
            this.btnValidate.Size = new System.Drawing.Size(236, 32);
            this.btnValidate.TabIndex = 0;
            this.btnValidate.Text = "VALIDATE";
            this.btnValidate.UseVisualStyleBackColor = false;
            this.btnValidate.Click += new System.EventHandler(this.button1_Click);
            // 
            // ErrorLogBox
            // 
            this.ErrorLogBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ErrorLogBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(246)))), ((int)(((byte)(246)))));
            this.ErrorLogBox.Location = new System.Drawing.Point(0, 462);
            this.ErrorLogBox.Name = "ErrorLogBox";
            this.ErrorLogBox.ReadOnly = true;
            this.ErrorLogBox.Size = new System.Drawing.Size(236, 74);
            this.ErrorLogBox.TabIndex = 2;
            this.ErrorLogBox.TabStop = false;
            this.ErrorLogBox.Text = "";
            // 
            // MainFormMenu
            // 
            this.MainFormMenu.Font = new System.Drawing.Font("Comic Sans MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.MainFormMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.infoToolStripMenuItem});
            this.MainFormMenu.Location = new System.Drawing.Point(0, 0);
            this.MainFormMenu.Name = "MainFormMenu";
            this.MainFormMenu.Size = new System.Drawing.Size(900, 24);
            this.MainFormMenu.TabIndex = 3;
            this.MainFormMenu.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.toolStripMenuItem1,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(109, 22);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(109, 22);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(106, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(109, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // infoToolStripMenuItem
            // 
            this.infoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bddMemoryToolStripMenuItem});
            this.infoToolStripMenuItem.Name = "infoToolStripMenuItem";
            this.infoToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.infoToolStripMenuItem.Text = "Info";
            // 
            // bddMemoryToolStripMenuItem
            // 
            this.bddMemoryToolStripMenuItem.Name = "bddMemoryToolStripMenuItem";
            this.bddMemoryToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.bddMemoryToolStripMenuItem.Text = "BDD Memory";
            this.bddMemoryToolStripMenuItem.Click += new System.EventHandler(this.bddMemoryToolStripMenuItem_Click);
            // 
            // openBProgDialog
            // 
            this.openBProgDialog.Filter = "Bool programs|*.bool|All files|*.*";
            // 
            // saveBProgDialog
            // 
            this.saveBProgDialog.Filter = "Bool programs|*.bool|All files|*.*";
            // 
            // CodePanel
            // 
            this.CodePanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.CodePanel.Controls.Add(this.codeBox);
            this.CodePanel.Controls.Add(this.ErrorLogBox);
            this.CodePanel.Controls.Add(this.btnValidate);
            this.CodePanel.Location = new System.Drawing.Point(3, 3);
            this.CodePanel.Name = "CodePanel";
            this.CodePanel.Size = new System.Drawing.Size(236, 536);
            this.CodePanel.TabIndex = 4;
            // 
            // codeBox
            // 
            this.codeBox.AcceptsTab = true;
            this.codeBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.codeBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(246)))), ((int)(((byte)(246)))));
            this.codeBox.DetectUrls = false;
            this.codeBox.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.codeBox.Location = new System.Drawing.Point(0, 0);
            this.codeBox.Name = "codeBox";
            this.codeBox.Size = new System.Drawing.Size(236, 418);
            this.codeBox.TabIndex = 2;
            this.codeBox.Text = resources.GetString("codeBox.Text");
            this.codeBox.TextChanged += new System.EventHandler(this.codeBox_TextChanged);
            // 
            // splitContainer
            // 
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.Location = new System.Drawing.Point(0, 24);
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.CodePanel);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.tabControl1);
            this.splitContainer.Size = new System.Drawing.Size(900, 539);
            this.splitContainer.SplitterDistance = 242;
            this.splitContainer.TabIndex = 5;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(654, 539);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.splitContainer1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(646, 513);
            this.tabPage2.TabIndex = 0;
            this.tabPage2.Text = "Reachability";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(3, 3);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer3);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(640, 507);
            this.splitContainer1.TabIndex = 1;
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.btnRunReachabilityAlg);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.labelsComboBox);
            this.splitContainer3.Panel2.Controls.Add(this.buttonTrajectory);
            this.splitContainer3.Size = new System.Drawing.Size(640, 50);
            this.splitContainer3.SplitterDistance = 411;
            this.splitContainer3.TabIndex = 2;
            // 
            // btnRunReachabilityAlg
            // 
            this.btnRunReachabilityAlg.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnRunReachabilityAlg.Location = new System.Drawing.Point(0, 0);
            this.btnRunReachabilityAlg.Name = "btnRunReachabilityAlg";
            this.btnRunReachabilityAlg.Size = new System.Drawing.Size(271, 50);
            this.btnRunReachabilityAlg.TabIndex = 0;
            this.btnRunReachabilityAlg.Text = "Run Reachability Algorithm";
            this.btnRunReachabilityAlg.UseVisualStyleBackColor = true;
            this.btnRunReachabilityAlg.Click += new System.EventHandler(this.btnRunReachabilityAlg_Click);
            // 
            // labelsComboBox
            // 
            this.labelsComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.labelsComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.labelsComboBox.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.labelsComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.labelsComboBox.FormattingEnabled = true;
            this.labelsComboBox.Location = new System.Drawing.Point(0, 29);
            this.labelsComboBox.Name = "labelsComboBox";
            this.labelsComboBox.Size = new System.Drawing.Size(225, 21);
            this.labelsComboBox.TabIndex = 2;
            // 
            // buttonTrajectory
            // 
            this.buttonTrajectory.AutoSize = true;
            this.buttonTrajectory.Dock = System.Windows.Forms.DockStyle.Top;
            this.buttonTrajectory.Location = new System.Drawing.Point(0, 0);
            this.buttonTrajectory.Name = "buttonTrajectory";
            this.buttonTrajectory.Size = new System.Drawing.Size(225, 26);
            this.buttonTrajectory.TabIndex = 1;
            this.buttonTrajectory.Text = "Generate Sample Trajectory";
            this.buttonTrajectory.UseVisualStyleBackColor = true;
            this.buttonTrajectory.Click += new System.EventHandler(this.buttonTrajectory_Click);
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.gleeViewCFG);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.reachDebugBox);
            this.splitContainer2.Size = new System.Drawing.Size(640, 453);
            this.splitContainer2.SplitterDistance = 380;
            this.splitContainer2.TabIndex = 0;
            // 
            // gleeViewCFG
            // 
            this.gleeViewCFG.AsyncLayout = false;
            this.gleeViewCFG.AutoScroll = true;
            this.gleeViewCFG.BackwardEnabled = false;
            this.gleeViewCFG.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gleeViewCFG.ForwardEnabled = false;
            this.gleeViewCFG.Graph = null;
            this.gleeViewCFG.Location = new System.Drawing.Point(0, 0);
            this.gleeViewCFG.MouseHitDistance = 0.05;
            this.gleeViewCFG.Name = "gleeViewCFG";
            this.gleeViewCFG.NavigationVisible = true;
            this.gleeViewCFG.PanButtonPressed = false;
            this.gleeViewCFG.SaveButtonVisible = true;
            this.gleeViewCFG.Size = new System.Drawing.Size(640, 380);
            this.gleeViewCFG.TabIndex = 5;
            this.gleeViewCFG.ZoomF = 1;
            this.gleeViewCFG.ZoomFraction = 0.5;
            this.gleeViewCFG.ZoomWindowThreshold = 0.05;
            this.gleeViewCFG.SelectionChanged += new System.EventHandler(this.gleeViewCFG_SelectionChanged);
            this.gleeViewCFG.MouseClick += new System.Windows.Forms.MouseEventHandler(this.gleeViewCFG_MouseClick);
            // 
            // reachDebugBox
            // 
            this.reachDebugBox.BackColor = System.Drawing.Color.Black;
            this.reachDebugBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.reachDebugBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.reachDebugBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.reachDebugBox.Location = new System.Drawing.Point(0, 0);
            this.reachDebugBox.Name = "reachDebugBox";
            this.reachDebugBox.ReadOnly = true;
            this.reachDebugBox.Size = new System.Drawing.Size(640, 69);
            this.reachDebugBox.TabIndex = 1;
            this.reachDebugBox.Text = "";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.tableLayoutPanel1);
            this.tabPage3.Controls.Add(this.gViewer);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(646, 513);
            this.tabPage3.TabIndex = 1;
            this.tabPage3.Text = "CFG";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 34F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33F));
            this.tableLayoutPanel1.Controls.Add(this.btnDrawAST, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnBuildCFG, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnDrawNextFunc, 1, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 38F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(629, 38);
            this.tableLayoutPanel1.TabIndex = 10;
            // 
            // btnDrawAST
            // 
            this.btnDrawAST.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDrawAST.BackColor = System.Drawing.SystemColors.MenuBar;
            this.btnDrawAST.Font = new System.Drawing.Font("Comic Sans MS", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnDrawAST.Location = new System.Drawing.Point(3, 3);
            this.btnDrawAST.Name = "btnDrawAST";
            this.btnDrawAST.Size = new System.Drawing.Size(201, 32);
            this.btnDrawAST.TabIndex = 5;
            this.btnDrawAST.Text = "Visualize AST";
            this.btnDrawAST.UseVisualStyleBackColor = false;
            this.btnDrawAST.Click += new System.EventHandler(this.btnDrawAST_Click);
            // 
            // btnBuildCFG
            // 
            this.btnBuildCFG.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBuildCFG.BackColor = System.Drawing.SystemColors.MenuBar;
            this.btnBuildCFG.Font = new System.Drawing.Font("Comic Sans MS", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnBuildCFG.Location = new System.Drawing.Point(423, 3);
            this.btnBuildCFG.Name = "btnBuildCFG";
            this.btnBuildCFG.Size = new System.Drawing.Size(203, 32);
            this.btnBuildCFG.TabIndex = 6;
            this.btnBuildCFG.Text = "Build CFG";
            this.btnBuildCFG.UseVisualStyleBackColor = false;
            this.btnBuildCFG.Click += new System.EventHandler(this.btnBuildCFG_Click);
            // 
            // btnDrawNextFunc
            // 
            this.btnDrawNextFunc.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDrawNextFunc.BackColor = System.Drawing.SystemColors.MenuBar;
            this.btnDrawNextFunc.Font = new System.Drawing.Font("Comic Sans MS", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnDrawNextFunc.Location = new System.Drawing.Point(210, 3);
            this.btnDrawNextFunc.Name = "btnDrawNextFunc";
            this.btnDrawNextFunc.Size = new System.Drawing.Size(207, 32);
            this.btnDrawNextFunc.TabIndex = 7;
            this.btnDrawNextFunc.Text = "Draw Next";
            this.btnDrawNextFunc.UseVisualStyleBackColor = false;
            this.btnDrawNextFunc.Click += new System.EventHandler(this.btnDrawNextFunc_Click);
            // 
            // gViewer
            // 
            this.gViewer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gViewer.AsyncLayout = false;
            this.gViewer.AutoScroll = true;
            this.gViewer.BackwardEnabled = false;
            this.gViewer.ForwardEnabled = false;
            this.gViewer.Graph = null;
            this.gViewer.Location = new System.Drawing.Point(5, 39);
            this.gViewer.MouseHitDistance = 0.05;
            this.gViewer.Name = "gViewer";
            this.gViewer.NavigationVisible = true;
            this.gViewer.PanButtonPressed = false;
            this.gViewer.SaveButtonVisible = true;
            this.gViewer.Size = new System.Drawing.Size(627, 433);
            this.gViewer.TabIndex = 9;
            this.gViewer.ZoomF = 1;
            this.gViewer.ZoomFraction = 0.5;
            this.gViewer.ZoomWindowThreshold = 0.05;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 563);
            this.Controls.Add(this.splitContainer);
            this.Controls.Add(this.MainFormMenu);
            this.MainMenuStrip = this.MainFormMenu;
            this.Name = "MainForm";
            this.Text = "Bool IDE";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.MainFormMenu.ResumeLayout(false);
            this.MainFormMenu.PerformLayout();
            this.CodePanel.ResumeLayout(false);
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            this.splitContainer.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            this.splitContainer3.Panel2.PerformLayout();
            this.splitContainer3.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnValidate;
        private System.Windows.Forms.RichTextBox ErrorLogBox;
        private System.Windows.Forms.MenuStrip MainFormMenu;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openBProgDialog;
        private System.Windows.Forms.SaveFileDialog saveBProgDialog;
        private System.Windows.Forms.Panel CodePanel;
        private System.Windows.Forms.RichTextBox codeBox;
        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button btnDrawAST;
        private System.Windows.Forms.Button btnBuildCFG;
        private System.Windows.Forms.Button btnDrawNextFunc;
        private Microsoft.Glee.GraphViewerGdi.GViewer gViewer;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.Button btnRunReachabilityAlg;
        private System.Windows.Forms.ComboBox labelsComboBox;
        private System.Windows.Forms.Button buttonTrajectory;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private Microsoft.Glee.GraphViewerGdi.GViewer gleeViewCFG;
        private System.Windows.Forms.RichTextBox reachDebugBox;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem infoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bddMemoryToolStripMenuItem;
    }
}

