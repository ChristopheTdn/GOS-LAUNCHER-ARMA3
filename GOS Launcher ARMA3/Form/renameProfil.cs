using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GOSLauncherA3
{
    public partial class renameProfil : Form
    {
        public renameProfil()
        {
            InitializeComponent();
        }

        private void renameProfil_Load(object sender, EventArgs e)
        {
            textBox2.Text = GOSLauncherCore.nomProfilRename;
        }


        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GOSLauncherCore.nomProfilRename = string.Join("", textBox2.Text.Split(Path.GetInvalidFileNameChars()));
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
