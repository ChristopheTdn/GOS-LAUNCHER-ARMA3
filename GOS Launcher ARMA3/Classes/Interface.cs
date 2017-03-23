using System;
using System.Deployment.Application;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using Arma3Launcher;
using System.Globalization;
using System.Threading;

namespace GOSLauncherA3
{
    public class ComboboxItem
    {
        public string Text { get; set; }
        public object Value { get; set; }

       public override string ToString()
        {
            return Text;
        }      
    }

    class Interface
    {
        static public void dessineInterface()
        {


            // Affiche version 

            GOSLauncherCore.fenetrePrincipale.label31.Text = AfficheVersionProgramme();
            
            // Determine Serveur
            // Determine presence ARMA 3 64 bit
            if (File.Exists(GOSLauncherCore.cheminARMA3 + @"\arma3_x64.exe")) {
                GOSLauncherCore.fenetrePrincipale.checkBox_Arma364bit.Enabled = true;
            }
            else
            {
                GOSLauncherCore.fenetrePrincipale.checkBox_Arma364bit.Enabled = false;
                GOSLauncherCore.fenetrePrincipale.checkBox_Arma364bit.Checked = false;
            }


            ProgExterne.ValideProgExt();

            // isGOS
            if (!GOSLauncherCore.isGOSValid())
            {
                GOSLauncherCore.fenetrePrincipale.tabControl2.TabPages.Remove(GOSLauncherCore.fenetrePrincipale.ModsGOS);
                GOSLauncherCore.fenetrePrincipale.tabControl2.TabPages.Remove(GOSLauncherCore.fenetrePrincipale.SynchroZONE);
                GOSLauncherCore.fenetrePrincipale.tabControl2.TabPages.Remove(GOSLauncherCore.fenetrePrincipale.Interclan_Info);
                GOSLauncherCore.fenetrePrincipale.pictureBox6.Visible = false;
                GOSLauncherCore.fenetrePrincipale.pictureBox36.Visible = false;
                GOSLauncherCore.fenetrePrincipale.button_ImporterMods.Visible = false;
                GOSLauncherCore.fenetrePrincipale.button38.Visible = false;

                // TS3 version 3.0.14
                GOSLauncherCore.fenetrePrincipale.button18.Visible = false;
                GOSLauncherCore.fenetrePrincipale.button19.Visible = false;
                GOSLauncherCore.fenetrePrincipale.pictureBox26.Visible = false;
                GOSLauncherCore.fenetrePrincipale.button17.Visible = true;
                GOSLauncherCore.fenetrePrincipale.groupBox3.Visible = false;
            }
            else
            {
                AlerteVersionArma3();
                testToutesTaillesSynchroEnLigne();
            }
            AfficheSynchroActive();
            AfficheServeurActif();

        }
        static public void genereTab()
        {
            effaceTousItemsOnglets();

            // @GOS 
            GOSLauncherCore.ListeTab(GOSLauncherCore.fenetrePrincipale.checkedListBox_Template, "@TEMPLATE", (GOSLauncherCore.fenetrePrincipale.comboBox4.SelectedItem as ComboboxItem).Value.ToString());
            GOSLauncherCore.ListeTab(GOSLauncherCore.fenetrePrincipale.checkedListBox_Framework, "@FRAMEWORK", (GOSLauncherCore.fenetrePrincipale.comboBox4.SelectedItem as ComboboxItem).Value.ToString());
            GOSLauncherCore.ListeTab(GOSLauncherCore.fenetrePrincipale.checkedListBox_Islands, "@ISLANDS", (GOSLauncherCore.fenetrePrincipale.comboBox4.SelectedItem as ComboboxItem).Value.ToString());
            GOSLauncherCore.ListeTab(GOSLauncherCore.fenetrePrincipale.checkedListBox_Units, "@UNITS", (GOSLauncherCore.fenetrePrincipale.comboBox4.SelectedItem as ComboboxItem).Value.ToString());
            GOSLauncherCore.ListeTab(GOSLauncherCore.fenetrePrincipale.checkedListBox_Materiel, "@MATERIEL", (GOSLauncherCore.fenetrePrincipale.comboBox4.SelectedItem as ComboboxItem).Value.ToString());
            GOSLauncherCore.ListeTab(GOSLauncherCore.fenetrePrincipale.checkedListBox_Client, "@CLIENT", (GOSLauncherCore.fenetrePrincipale.comboBox4.SelectedItem as ComboboxItem).Value.ToString());
            GOSLauncherCore.ListeTab(GOSLauncherCore.fenetrePrincipale.checkedListBox_Test, "@TEST", (GOSLauncherCore.fenetrePrincipale.comboBox4.SelectedItem as ComboboxItem).Value.ToString());
            GOSLauncherCore.ListeTab(GOSLauncherCore.fenetrePrincipale.checkedListBox_Interclan, "@INTERCLAN", (GOSLauncherCore.fenetrePrincipale.comboBox4.SelectedItem as ComboboxItem).Value.ToString());
            // @Autre
            // Root
            GOSLauncherCore.ListeTab(GOSLauncherCore.fenetrePrincipale.checkedListBox_MODS_Arma3, "AUTRES_MODS", (GOSLauncherCore.fenetrePrincipale.comboBox4.SelectedItem as ComboboxItem).Value.ToString());
            //Arma3 profile            
            GOSLauncherCore.ListeTab(GOSLauncherCore.fenetrePrincipale.checkedListBox_MODS_Docs_Arma3, "DOC_ARMA3", (GOSLauncherCore.fenetrePrincipale.comboBox4.SelectedItem as ComboboxItem).Value.ToString());
            //Arma3 other profile
            GOSLauncherCore.ListeTab(GOSLauncherCore.fenetrePrincipale.checkedListBox_MODS_Docs_Arma3_OthersProfiles, "DOC_OTHERPROFILE", (GOSLauncherCore.fenetrePrincipale.comboBox4.SelectedItem as ComboboxItem).Value.ToString());
            //genereTabMods();
            genereTabParam();
            genereTabPriorite();
        }
        static public void effaceTousItemsOnglets()
        {
            GOSLauncherCore.fenetrePrincipale.comboBox2.Items.Clear();
            GOSLauncherCore.fenetrePrincipale.comboBox2.Items.Add("");
            GOSLauncherCore.fenetrePrincipale.radioButton20.Enabled = false;
            GOSLauncherCore.fenetrePrincipale.radioButton20.Checked = false;
            GOSLauncherCore.fenetrePrincipale.radioButton21.Enabled = false;
            GOSLauncherCore.fenetrePrincipale.radioButton21.Checked = false;
            GOSLauncherCore.fenetrePrincipale.pictureBox1.Image = GOSLauncherA3.Properties.Resources.logoGOS;
            GOSLauncherCore.fenetrePrincipale.checkedListBox_Framework.Items.Clear();
            GOSLauncherCore.fenetrePrincipale.checkedListBox_Template.Items.Clear();
            GOSLauncherCore.fenetrePrincipale.checkedListBox_Islands.Items.Clear();
            GOSLauncherCore.fenetrePrincipale.checkedListBox_Units.Items.Clear();
            GOSLauncherCore.fenetrePrincipale.checkedListBox_Materiel.Items.Clear();
            GOSLauncherCore.fenetrePrincipale.checkedListBox_Client.Items.Clear();
            GOSLauncherCore.fenetrePrincipale.checkedListBox_Test.Items.Clear();
            GOSLauncherCore.fenetrePrincipale.checkedListBox_MODS_Arma3.Items.Clear();
            GOSLauncherCore.fenetrePrincipale.checkedListBox_MODS_Docs_Arma3.Items.Clear();
            GOSLauncherCore.fenetrePrincipale.checkedListBox_MODS_Docs_Arma3_OthersProfiles.Items.Clear();
            GOSLauncherCore.fenetrePrincipale.checkedListBox_Interclan.Items.Clear();
        }
        static public void effaceTousparamsOnglet()
        {
            GOSLauncherCore.fenetrePrincipale.checkBox1.Checked = false;
            GOSLauncherCore.fenetrePrincipale.checkBox2.Checked = false;
            GOSLauncherCore.fenetrePrincipale.checkBox3.Checked = false;
            GOSLauncherCore.fenetrePrincipale.checkBox4.Checked = false;
            GOSLauncherCore.fenetrePrincipale.checkBox5.Checked = false;
            GOSLauncherCore.fenetrePrincipale.checkBox6.Checked = false;
            GOSLauncherCore.fenetrePrincipale.checkBox7.Checked = false;
            GOSLauncherCore.fenetrePrincipale.checkBox8.Checked = false;
            GOSLauncherCore.fenetrePrincipale.checkBox9.Checked = false;
            GOSLauncherCore.fenetrePrincipale.checkBox11.Checked = false;
            GOSLauncherCore.fenetrePrincipale.checkBox12.Checked = false;
            GOSLauncherCore.fenetrePrincipale.checkBox13.Checked = false;
            GOSLauncherCore.fenetrePrincipale.checkBox19.Checked = false;
            GOSLauncherCore.fenetrePrincipale.checkBox22.Checked = false;
            GOSLauncherCore.fenetrePrincipale.checkBox23.Checked = false;
            GOSLauncherCore.fenetrePrincipale.checkBox21.Checked = false;
            GOSLauncherCore.fenetrePrincipale.checkBox10.Checked = false;
            GOSLauncherCore.fenetrePrincipale.checkBox24.Checked = false;
            GOSLauncherCore.fenetrePrincipale.checkBox_ARMA3BattleyeOption.Checked = false;
            GOSLauncherCore.fenetrePrincipale.checkBox_HeadlessClient.Checked = false;
            GOSLauncherCore.fenetrePrincipale.checkBox_EnableHT.Checked = false;
            GOSLauncherCore.fenetrePrincipale.textBox2.Text = "";
            GOSLauncherCore.fenetrePrincipale.textBox3.Text = "";
            GOSLauncherCore.fenetrePrincipale.textBox4.Text = "";
            GOSLauncherCore.fenetrePrincipale.textBox7.Text = "";
            GOSLauncherCore.fenetrePrincipale.textBox8.Text = "";
            GOSLauncherCore.fenetrePrincipale.checkBox_Arma364bit.Checked = false;

        }
        static public void genereTabParam()
        {
            effaceTousparamsOnglet();
            ProgExterne.ValideProgExt();
            XmlTextReader fichierProfilXML = new XmlTextReader(GOSLauncherCore.cheminARMA3 + @"\userconfig\GOS-LauncherA3\" + (GOSLauncherCore.fenetrePrincipale.comboBox4.SelectedItem as ComboboxItem).Value.ToString() + ".profil.xml");
            while (fichierProfilXML.Read())
            {

                fichierProfilXML.ReadToFollowing("winXP");
                if (fichierProfilXML.ReadString() == "true") { GOSLauncherCore.fenetrePrincipale.checkBox9.Checked = true; }
                fichierProfilXML.ReadToFollowing("showScriptErrors");
                if (fichierProfilXML.ReadString() == "true") { GOSLauncherCore.fenetrePrincipale.checkBox5.Checked = true; }
                fichierProfilXML.ReadToFollowing("worldEmpty");
                if (fichierProfilXML.ReadString() == "true") { GOSLauncherCore.fenetrePrincipale.checkBox4.Checked = true; }
                fichierProfilXML.ReadToFollowing("noPause");
                if (fichierProfilXML.ReadString() == "true") { GOSLauncherCore.fenetrePrincipale.checkBox2.Checked = true; }
                fichierProfilXML.ReadToFollowing("nosplash");
                if (fichierProfilXML.ReadString() == "true") { GOSLauncherCore.fenetrePrincipale.checkBox1.Checked = true; }
                fichierProfilXML.ReadToFollowing("window");
                if (fichierProfilXML.ReadString() == "true") { GOSLauncherCore.fenetrePrincipale.checkBox3.Checked = true; }
                fichierProfilXML.ReadToFollowing("maxMem");
                string maxmem = fichierProfilXML.ReadString();
                if (maxmem != "") { GOSLauncherCore.fenetrePrincipale.checkBox6.Checked = true; GOSLauncherCore.fenetrePrincipale.trackBar1.Value = int.Parse(maxmem); GOSLauncherCore.fenetrePrincipale.textBox5.Text = maxmem; }
                fichierProfilXML.ReadToFollowing("cpuCount");
                string cpucount = fichierProfilXML.ReadString();
                if (cpucount != "") { GOSLauncherCore.fenetrePrincipale.checkBox7.Checked = true; GOSLauncherCore.fenetrePrincipale.trackBar2.Value = int.Parse(cpucount); GOSLauncherCore.fenetrePrincipale.textBox6.Text = cpucount; }
                fichierProfilXML.ReadToFollowing("fraps");
                if (fichierProfilXML.ReadString() == "true") { GOSLauncherCore.fenetrePrincipale.checkBox11.Checked = true; }
                fichierProfilXML.ReadToFollowing("trackir");
                if (fichierProfilXML.ReadString() == "true") { GOSLauncherCore.fenetrePrincipale.checkBox12.Checked = true; }
                fichierProfilXML.ReadToFollowing("noCB");
                if (fichierProfilXML.ReadString() == "true") { GOSLauncherCore.fenetrePrincipale.checkBox8.Checked = true; }
                fichierProfilXML.ReadToFollowing("minimize");
                if (fichierProfilXML.ReadString() == "true") { GOSLauncherCore.fenetrePrincipale.checkBox19.Checked = true; }
                fichierProfilXML.ReadToFollowing("filePatching");
                if (fichierProfilXML.ReadString() == "true") { GOSLauncherCore.fenetrePrincipale.checkBox23.Checked = true; }
                fichierProfilXML.ReadToFollowing("VideomaxMem");
                string Videomaxmem = fichierProfilXML.ReadString();
                if (Videomaxmem != "") { GOSLauncherCore.fenetrePrincipale.checkBox22.Checked = true; GOSLauncherCore.fenetrePrincipale.trackBar3.Value = int.Parse(Videomaxmem); GOSLauncherCore.fenetrePrincipale.textBox20.Text = Videomaxmem; }
                fichierProfilXML.ReadToFollowing("threadMax");
                string threadMax = fichierProfilXML.ReadString();
                if (threadMax != "") { GOSLauncherCore.fenetrePrincipale.checkBox21.Checked = true; GOSLauncherCore.fenetrePrincipale.comboBox3.SelectedIndex = int.Parse(threadMax); }
                fichierProfilXML.ReadToFollowing("adminmode");
                if (fichierProfilXML.ReadString() == "true") { GOSLauncherCore.fenetrePrincipale.checkBox24.Checked = true; }
                fichierProfilXML.ReadToFollowing("nologs");
                if (fichierProfilXML.ReadString() == "true") { GOSLauncherCore.fenetrePrincipale.checkBox10.Checked = true; }
                fichierProfilXML.ReadToFollowing("HC");
                if (fichierProfilXML.ReadString() == "true")
                {
                    GOSLauncherCore.fenetrePrincipale.checkBox_HeadlessClient.Checked = true;
                    fichierProfilXML.ReadToFollowing("HCPort");
                    GOSLauncherCore.fenetrePrincipale.textBox2.Text = fichierProfilXML.ReadString();
                    fichierProfilXML.ReadToFollowing("HCPassWord");
                    GOSLauncherCore.fenetrePrincipale.textBox3.Text = fichierProfilXML.ReadString();
                }
                fichierProfilXML.ReadToFollowing("other");
                string otherCMD = fichierProfilXML.ReadString();
                if (otherCMD != "") { GOSLauncherCore.fenetrePrincipale.checkBox13.Checked = true; GOSLauncherCore.fenetrePrincipale.textBox4.Text = otherCMD; }
                if (GOSLauncherCore.fenetrePrincipale.checkBox3.Checked)
                {
                    fichierProfilXML.ReadToFollowing("windowX");
                    string windowX = fichierProfilXML.ReadString();
                    if (windowX != "") { GOSLauncherCore.fenetrePrincipale.textBox7.Text = windowX; }
                    fichierProfilXML.ReadToFollowing("windowY");
                    string windowY = fichierProfilXML.ReadString();
                    if (windowY != "") { GOSLauncherCore.fenetrePrincipale.textBox8.Text = windowY; }
                }
                fichierProfilXML.ReadToFollowing("enableHT");
                if (fichierProfilXML.ReadString() == "true") { GOSLauncherCore.fenetrePrincipale.checkBox_EnableHT.Checked = true; }
                fichierProfilXML.ReadToFollowing("ARMA3Battleeyes");
                if (fichierProfilXML.ReadString() == "true") { GOSLauncherCore.fenetrePrincipale.checkBox_ARMA3BattleyeOption.Checked = true; }
                fichierProfilXML.ReadToFollowing("ARMA3-64bit");
                if (fichierProfilXML.ReadString() == "true") { GOSLauncherCore.fenetrePrincipale.checkBox_Arma364bit.Checked = true; }

            }
            fichierProfilXML.Close();
        }
        static public void genereTabPriorite()
        {
            GOSLauncherCore.fenetrePrincipale.ctrlListModPrioritaire.Items.Clear();
            if (System.IO.File.Exists(GOSLauncherCore.cheminARMA3 + @"\userconfig\GOS-LauncherA3\" + (GOSLauncherCore.fenetrePrincipale.comboBox4.SelectedItem as ComboboxItem).Value.ToString() + ".profilPriorite.xml"))
            {
                XmlTextReader fichierProfilPrioriteXML = new XmlTextReader(GOSLauncherCore.cheminARMA3 + @"\userconfig\GOS-LauncherA3\" + (GOSLauncherCore.fenetrePrincipale.comboBox4.SelectedItem as ComboboxItem).Value.ToString() + ".profilPriorite.xml");
                while (fichierProfilPrioriteXML.Read())
                {
                    fichierProfilPrioriteXML.ReadToFollowing("MODS");
                    string ligne = fichierProfilPrioriteXML.ReadString();
                    if (ligne != "")
                    {
                        GOSLauncherCore.fenetrePrincipale.ctrlListModPrioritaire.Items.Add(ligne);
                    }
                }
                fichierProfilPrioriteXML.Close();
            }
            else
            {
                Priority.actualisePrioriteMods();
                GOSLauncherCore.sauvegardeConfigProfilXML("");
            };

        }
        static private string AfficheVersionProgramme()
        {
            string versionProg = "";
            if (ApplicationDeployment.IsNetworkDeployed)
            {
                Version version = ApplicationDeployment.CurrentDeployment.CurrentVersion;
                versionProg = "v. " + version.Major + "." + version.Minor + "." + version.Build + " (Rev. " + version.Revision + ")";
            }
            return versionProg;
        }
        static private string VersionArma3()
        {
            try
            {
                FileVersionInfo version = FileVersionInfo.GetVersionInfo(GOSLauncherCore.cheminARMA3 + @"/arma3.exe");
                //ApplicationDeployment.CurrentDeployment.CurrentVersion;
                return version.ProductVersion;
            }
            catch
            {
                return "";
            }
        }
        static public void testToutesTaillesSynchroEnLigne()
            
        {
            GOSLauncherCore.synchroRsyncTaille("", GOSLauncherCore.fenetrePrincipale.button16, null, null, GOSLauncherCore.fenetrePrincipale.label8, null);
            GOSLauncherCore.synchroRsyncTaille("@TEMPLATE", GOSLauncherCore.fenetrePrincipale.button25, null, null, GOSLauncherCore.fenetrePrincipale.label45, null);
            GOSLauncherCore.synchroRsyncTaille("@ISLANDS", GOSLauncherCore.fenetrePrincipale.button26, null, null, GOSLauncherCore.fenetrePrincipale.label46, null);
            GOSLauncherCore.synchroRsyncTaille("@MATERIEL", GOSLauncherCore.fenetrePrincipale.button41, null, null, GOSLauncherCore.fenetrePrincipale.label47, null);
            GOSLauncherCore.synchroRsyncTaille("@UNITS", GOSLauncherCore.fenetrePrincipale.button42, null, null, GOSLauncherCore.fenetrePrincipale.label48, null);
            GOSLauncherCore.synchroRsyncTaille("@CLIENT", GOSLauncherCore.fenetrePrincipale.button43, null, null, GOSLauncherCore.fenetrePrincipale.label49, null);
            GOSLauncherCore.synchroRsyncTaille("@TEST", GOSLauncherCore.fenetrePrincipale.button_TESTBoutonSynchro, null, null, GOSLauncherCore.fenetrePrincipale.label_TESTTailleSynchro, null);
            GOSLauncherCore.synchroRsyncTaille("@FRAMEWORK", GOSLauncherCore.fenetrePrincipale.button45, null, null, GOSLauncherCore.fenetrePrincipale.label51, null);
            GOSLauncherCore.synchroRsyncTaille("@INTERCLAN", GOSLauncherCore.fenetrePrincipale.button_INTERCLANBoutonSynchro, null, null, GOSLauncherCore.fenetrePrincipale.label_INTERCLANTailleSynchro, null);
        }
        static public void AlerteVersionArma3()
        {
            try
            {
                GOSLauncherA3.FenetrePrincipale.DownloadConfigServeur("versionserveurdistant.xml", "ftp://37.59.36.179", GOSLauncherCore.cheminARMA3 + @"\userconfig\GOS-LauncherA3\");
                XmlTextReader fichierInfoServer = new XmlTextReader(GOSLauncherCore.cheminARMA3 + @"\userconfig\GOS-LauncherA3\versionserveurdistant.xml");
                fichierInfoServer.ReadToFollowing("VERSION");
                string VersionServeur = fichierInfoServer.ReadString();
                fichierInfoServer.Close();
                if (VersionServeur == VersionArma3())
                {
                    GOSLauncherCore.fenetrePrincipale.label7.Text = VersionArma3();
                    GOSLauncherCore.fenetrePrincipale.toolTip1.SetToolTip(GOSLauncherCore.fenetrePrincipale.pictureBox24, "Version (GOS server) : " + VersionServeur + Environment.NewLine);
                    GOSLauncherCore.fenetrePrincipale.pictureBox24.Image = GOSLauncherA3.Properties.Resources.valide;
                }
                else 
                {
                    GOSLauncherCore.fenetrePrincipale.label7.Text = VersionArma3();
                    GOSLauncherCore.fenetrePrincipale.toolTip1.SetToolTip(GOSLauncherCore.fenetrePrincipale.pictureBox24, "Version (GOS server) : " + VersionServeur );
                    GOSLauncherCore.fenetrePrincipale.pictureBox24.Image = GOSLauncherA3.Properties.Resources.delete;
                    GOSLauncherCore.fenetrePrincipale.label7.ForeColor = System.Drawing.Color.Red;
                }               
            }
            catch
            {
                
            }

        }
        static public void AfficheSynchroActive()
        {
             switch (GOSLauncherCore.GetKeyValue(@"Software\Clan GOS\GOS Launcher A3\", "synchro"))
            {
                case "beta" :
                    GOSLauncherCore.fenetrePrincipale.checkBox_SyncBETA.Checked = true;
                    break;
               case "officielle" :
                    GOSLauncherCore.fenetrePrincipale.checkBox_SyncBETA.Checked = false;
                    break;
            }
             switch (GOSLauncherCore.GetKeyValue(@"Software\Clan GOS\GOS Launcher A3\", "synchro_INTERCLAN"))
             {
                 case "true":
                     GOSLauncherCore.fenetrePrincipale.checkBox_SyncINTERCLAN.Checked = true;
                     break;
                 case "false":
                     GOSLauncherCore.fenetrePrincipale.checkBox_SyncINTERCLAN.Checked = false;
                     break;
             }
        }
        static public void AfficheServeurActif()
        {
            switch (GOSLauncherCore.GetKeyValue(@"Software\Clan GOS\GOS Launcher A3\", "serveur"))
            {
                case "SERVEUR1":
                    GOSLauncherCore.fenetrePrincipale.radioButton7.Checked = true;
                    break;
                case "SERVEUR2":
                    GOSLauncherCore.fenetrePrincipale.radioButton8.Checked = true;
                    break;
                default:
                    GOSLauncherCore.fenetrePrincipale.radioButton8.Checked = true ;
                    break;
            }
        }
        static private void genereInfoMapServeur(string serveur, string IpServeur, int portServeur, Control control_InfoServeur, Control control_NomMission)
        {
            string nomMission = "";
            string nombreJoueur ="";
            CallArma3Server Arma3Serveur = new CallArma3Server();
            try
            {

                Arma3ServerBean bean = Arma3Serveur.call(IpServeur, portServeur);
                nomMission = bean.getMissionName() + "." + bean.getMapName();
                nombreJoueur = bean.getConnected();
            }
            catch { }
            control_InfoServeur.Text = serveur + " (" + nombreJoueur + "):";
            control_NomMission.Text = nomMission;
            
        }
        static public void AfficheMissionServeurMulti()
         {
            // Serveur Officiel
           genereInfoMapServeur ("Officiel", "188.165.254.11", 4443, GOSLauncherCore.fenetrePrincipale.checkBox_SERVEUR_OFFICIEL, GOSLauncherCore.fenetrePrincipale.textBox_nomMissionOFFICIELLE);

            // Serveur Mapping
           genereInfoMapServeur("Mapping", "188.165.254.11", 3303, GOSLauncherCore.fenetrePrincipale.checkBox_SERVEUR_MAPPING, GOSLauncherCore.fenetrePrincipale.textBox_nomMissionMAPPING);

            // Serveur Public
           genereInfoMapServeur("Public", "188.165.254.11", 7303, GOSLauncherCore.fenetrePrincipale.checkBox_SERVEUR_PUBLIC, GOSLauncherCore.fenetrePrincipale.textBox_nomMissionPUBLIC);
        }


        /*
         *    LANGAGE
         */

        #region LANGAGE
        static public void ChangeLangage(string langue)
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(langue);
            SauvegardeLangage(langue);
        }
        static private void SauvegardeLangage(string langue)
        {
            GOSLauncherCore.SetKeyValue(@"Software\Clan GOS\GOS Launcher A3\", "langage", langue);
        }
        #endregion

        /*
         *    Gestion des profils
         */
        #region GESTION PROFIL
        static public void AjouteComboNomProfil(int index, string nomProfil)
        {
            ComboboxItem item = new ComboboxItem();
            string textAffiche = nomProfil;
            if (nomProfil == "defaut")
            {
                string langue = GOSLauncherCore.GetKeyValue(@"Software\Clan GOS\GOS Launcher A3\", "langage");
                switch (langue)
                {
                    case "en-US":
                        textAffiche = "default";
                        break;
                    case "ru-RU":
                        textAffiche = "умолчание";
                        break;
                    case "de-DE":
                        textAffiche = "Vorgabe";
                        break;
                    case "el-GR":
                        textAffiche = "Προεπιλογή";
                        break;
                    case "es-ES":
                        textAffiche = "Por defecto";
                        break;
                    default:
                        textAffiche = "defaut";
                        break;
                }

            }

            item.Text = textAffiche;
            item.Value = nomProfil;
            GOSLauncherCore.fenetrePrincipale.comboBox4.Items.Add(item);

        }
        static public void AjouteListeBoxNomProfil(int index, string nomProfil)
        {
            ComboboxItem item = new ComboboxItem();
            string textAffiche = nomProfil;
            if (nomProfil == "defaut")
            {
                string langue = GOSLauncherCore.GetKeyValue(@"Software\Clan GOS\GOS Launcher A3\", "langage");
                switch (langue)
                {
                    case "en-US":
                        textAffiche = "default";
                        break;
                    case "ru-RU":
                        textAffiche = "умолчание";
                        break;
                    case "de-DE":
                        textAffiche = "Vorgabe";
                        break;
                    case "el-GR":
                        textAffiche = "Προεπιλογή";
                        break;
                    case "es-ES":
                        textAffiche = "Por defecto";
                        break;

                    default:
                        textAffiche = "defaut";
                        break;
                }

            }

            item.Text = textAffiche;
            item.Value = nomProfil;
            GOSLauncherCore.fenetrePrincipale.listBox_ListingProfil.Items.Add(item);
        }
        public static void initialiseListeProfil()
        {
            string[] listeProfil = Directory.GetFiles(GOSLauncherCore.cheminARMA3 + @"\userconfig\GOS-LauncherA3\", "*.profil.xml", SearchOption.TopDirectoryOnly);
            string textMenuProfil = GOSLauncherCore.fenetrePrincipale.comboBox4.Text;
            if (textMenuProfil == "") { textMenuProfil = "defaut"; }
            GOSLauncherCore.fenetrePrincipale.listBox_ListingProfil.Items.Clear();
            GOSLauncherCore.fenetrePrincipale.comboBox4.Items.Clear();
            int compteur = 0;
            foreach (var ligne in listeProfil)
            {
                string textCombo = ligne.Replace(GOSLauncherCore.cheminARMA3 + @"\userconfig\GOS-LauncherA3\", "");                            
                Interface.AjouteListeBoxNomProfil(compteur, textCombo.Replace(".profil.xml", ""));
                Interface.AjouteComboNomProfil(compteur, textCombo.Replace(".profil.xml", ""));
                compteur++;
            }
        }
        #endregion


        /*
         *   UNLOCK SYSTEM
         */

        #region UNLOCK
        static public void UnlockGOSLauncher()
        {
            Form dialogue = new Dial_Unlock();
            dialogue.ShowDialog();
            Application.Restart();
        }
        #endregion

        /*
         *   CHANGELOG
         */

        #region CHANGELOG
        static public void AfficheChangelog()
        {
            Form dialogue = new Dial_ChangeLog();
            dialogue.ShowDialog();
        }
        #endregion

   
    }
}