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
    public partial class DIAL_SynchroMission : Form
    {
        public string MISSION_PBO { get; private set; }
        private bool IMPORT_VALIDE = true;
        public DIAL_SynchroMission()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void DIAL_SynchroMission_Load(object sender, EventArgs e)
        {
            pictureBox1.Visible = false;
            pictureBox2.Visible = false;
            pictureBox3.Visible = false;
   
            
        }
        private void charge_mission()
        {

            if (GOSLauncherCore.fenetrePrincipale.checkBox_SERVEUR_MAPPING.Checked)
            {
                Arma3Launcher.GestFile Dlfile = new Arma3Launcher.GestFile();
                if (Dlfile.downloadFile(GOSLauncherCore.fenetrePrincipale.textBox_nomMissionMAPPING.Text, "server1.clan-GOS.fr", "mappinga3", "GOSMissions2012", GOSLauncherCore.cheminARMA3 + @"\MPmissions\"))
                {
                    pictureBox1.Image = GOSLauncherA3.Properties.Resources.valide;
                    pictureBox1.Visible = true;
                    MISSION_PBO = GOSLauncherCore.fenetrePrincipale.textBox_nomMissionMAPPING.Text;
                }
                else
                {
                    pictureBox1.Image = GOSLauncherA3.Properties.Resources.delete;
                    pictureBox1.Visible = true;
                    IMPORT_VALIDE = false;
                };

            }
            else if (GOSLauncherCore.fenetrePrincipale.checkBox_SERVEUR_OFFICIEL.Checked)
            {
                Arma3Launcher.GestFile Dlfile = new Arma3Launcher.GestFile();
                if (Dlfile.downloadFile(GOSLauncherCore.fenetrePrincipale.textBox_nomMissionOFFICIELLE.Text, "server1.clan-GOS.fr", "officiela3", "GOSMissions2012", GOSLauncherCore.cheminARMA3 + @"\MPmissions\"))
                {
                    pictureBox1.Image = GOSLauncherA3.Properties.Resources.valide;
                    pictureBox1.Visible = true;
                    MISSION_PBO = GOSLauncherCore.fenetrePrincipale.textBox_nomMissionOFFICIELLE.Text;
                }
                else
                {
                    pictureBox1.Image = GOSLauncherA3.Properties.Resources.delete;
                    IMPORT_VALIDE = false;
                };

            }
            else if (GOSLauncherCore.fenetrePrincipale.checkBox_SERVEUR_PUBLIC.Checked)
            {
                Arma3Launcher.GestFile Dlfile = new Arma3Launcher.GestFile();
                if (Dlfile.downloadFile(GOSLauncherCore.fenetrePrincipale.textBox_nomMissionPUBLIC.Text, "server1.clan-GOS.fr", "publica3", "GOSMissions2012", GOSLauncherCore.cheminARMA3 + @"\MPmissions\"))
                {
                    pictureBox1.Image = GOSLauncherA3.Properties.Resources.valide;
                    MISSION_PBO = GOSLauncherCore.fenetrePrincipale.textBox_nomMissionPUBLIC.Text;
                }
                else
                {
                    pictureBox1.Image = GOSLauncherA3.Properties.Resources.delete;
                    IMPORT_VALIDE = false;
                };

            }
        
    }
          private void decompile_mission(string pboFile)
        {
            Arma3Launcher.GestFile extrait_sqm = new Arma3Launcher.GestFile();
            if (extrait_sqm.extract_FichierSQM(GOSLauncherCore.cheminARMA3 + @"\MPmissions\"+pboFile))
                {
                pictureBox2.Image = GOSLauncherA3.Properties.Resources.valide;
            }
            else
            {
                pictureBox2.Image = GOSLauncherA3.Properties.Resources.delete;
                IMPORT_VALIDE = false;
            };
      
        }
        private void extraitlisteModmissionSQM()
        {
            Arma3Launcher.GestFile extraitliste_sqm = new Arma3Launcher.GestFile();            
            if (extraitliste_sqm.readClassFromMissionSQM(MISSION_PBO))
            {
                pictureBox3.Image = GOSLauncherA3.Properties.Resources.valide;
                button2.Enabled = true;
            }
            else
            {
                pictureBox3.Image = GOSLauncherA3.Properties.Resources.delete;
                IMPORT_VALIDE = false;
            };
        }
        private void DIAL_SynchroMission_Shown(object sender, EventArgs e)
        {
            pictureBox1.Image = GOSLauncherA3.Properties.Resources.point_interrogation;
            pictureBox1.Visible = true;
            Refresh();
            charge_mission();
            pictureBox2.Image = GOSLauncherA3.Properties.Resources.point_interrogation;
            pictureBox2.Visible = true;
            Refresh();
            decompile_mission(MISSION_PBO+".pbo");
            pictureBox3.Image = GOSLauncherA3.Properties.Resources.point_interrogation;
            pictureBox3.Visible = true;
            Refresh();
            extraitlisteModmissionSQM();
            button2.Enabled = IMPORT_VALIDE;
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}
