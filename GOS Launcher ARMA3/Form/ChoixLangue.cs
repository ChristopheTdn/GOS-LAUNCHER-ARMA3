using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GOSLauncherA3
{
    public partial class langue : Form
    {
        public langue()
        {
            InitializeComponent();
        }

        private void langue_Load(object sender, EventArgs e)
        {

            radioButton2.Checked = true; 
            CultureInfo ci = CultureInfo.InstalledUICulture;
            
            if (ci.Name == "fr-FR") { radioButton1.Checked = true; this.Close();}
            if (ci.Name == "en-US") { radioButton2.Checked = true; this.Close(); }
            if (ci.Name == "de-DE") { radioButton3.Checked = true; this.Close(); }
            if (ci.Name == "ru-RU") { radioButton4.Checked = true; this.Close(); }
            if (ci.Name == "el-GR") { radioButton5.Checked = true; this.Close(); }
            if (ci.Name == "es-ES") { radioButton6.Checked = true; this.Close(); }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                GOSLauncherCore.SetKeyValue(@"Software\Clan GOS\GOS Launcher A3\", "langage", "fr-FR");
            };
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
                GOSLauncherCore.SetKeyValue(@"Software\Clan GOS\GOS Launcher A3\", "langage", "en-US");
            };
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton3.Checked)
            {
                GOSLauncherCore.SetKeyValue(@"Software\Clan GOS\GOS Launcher A3\", "langage", "de-DE");
            };
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
               
            if (radioButton4.Checked)
            {
                GOSLauncherCore.SetKeyValue(@"Software\Clan GOS\GOS Launcher A3\", "langage", "ru-RU");
            };
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton5.Checked)
            {
                GOSLauncherCore.SetKeyValue(@"Software\Clan GOS\GOS Launcher A3\", "langage", "el-GR");
            }
        }

        private void radioButton6_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton6.Checked)
            {
                GOSLauncherCore.SetKeyValue(@"Software\Clan GOS\GOS Launcher A3\", "langage", "es-ES");
            }
        }
        


    }
}
