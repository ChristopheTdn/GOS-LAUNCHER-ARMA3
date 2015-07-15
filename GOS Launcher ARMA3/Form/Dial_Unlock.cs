using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GOSLauncherA3
{
    public partial class Dial_Unlock : Form
    {
        public Dial_Unlock()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
          GOSLauncherCore.SetKeyValue(@"Software\Clan GOS\GOS Launcher A3\","UnlockPass",textBox2.Text);
          this.Close();
        }
    }
}
