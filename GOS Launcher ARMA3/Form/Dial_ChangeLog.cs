using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace GOSLauncherA3
{
    public partial class Dial_ChangeLog : Form
    {
        public Dial_ChangeLog()
        {
            InitializeComponent();
        }

        private void Dial_ChangeLog_Load(object sender, EventArgs e)
        {
            WebClient client = new WebClient ();
            string reply = client.DownloadString ("http://www.clan-GOS.fr/synchro/changelog.txt");
            richTextBox1.Text = reply;
        }
    }
}
