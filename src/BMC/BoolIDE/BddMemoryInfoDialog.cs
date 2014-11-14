using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BoolIDE
{
    public partial class BddMemoryInfoDialog : Form
    {
        public BddMemoryInfoDialog()
        {
            InitializeComponent();
        }

        private void BddMemoryInfoDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                this.Hide();
                e.Cancel = true;
            }
        }
    }
}