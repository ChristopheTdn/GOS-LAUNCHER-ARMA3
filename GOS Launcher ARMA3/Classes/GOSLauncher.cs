using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Windows.Forms;
using System.Diagnostics;
using System.Net;
using System.Reflection;
using Microsoft.Win32;

/*
 * class GOS LAUNCHER par ToF
 */
namespace GOSLauncherA3
{
    static class  GOSLauncherCore
    {
        static public string cheminARMA3, repertoireCourant, listMODS, listArguments;
        static public bool serveurRunning = false;
        static public bool dialogueReponse = false;
        static public string constCheminFTP;
        static public string constLoginFTP;
        static public string constMdpFTP;
        static public List<string>ListModsrealUrl= new List<string>();
        static public FenetrePrincipale fenetrePrincipale;
        static public System.Windows.Forms.Timer timerSynchro = new System.Windows.Forms.Timer();
       
        /*
         *         Config
         */

        #region Config
        static public void sauvegardeCheminArma3()
    {
        try
        {
            SetKeyValue(@"Software\Clan GOS\GOS Launcher A3\","cheminArma3",cheminARMA3);            
        }
        catch
        {
        }
    }
        static public void definirCheminArma3()
        {
            if (File.Exists(GetKeyValue(@"Software\Clan GOS\GOS Launcher A3\","cheminArma3")+@"\arma3.exe"))
            {
                cheminARMA3 = GetKeyValue(@"Software\Clan GOS\GOS Launcher A3\","cheminArma3");

            }
            else
            {
                try
                {
                    RegistryKey registryKey = Registry.CurrentUser.OpenSubKey(@"Software\Valve\Steam");
                    cheminARMA3 = (string)registryKey.GetValue("SteamPath") + @"\SteamApps\common\Arma 3";
                    cheminARMA3 = cheminARMA3.Replace("/", @"\");
                }
                catch
                {
                }
            };
            if (!File.Exists(cheminARMA3 + @"\arma3.exe"))
            {
                        Form dialogue = new Dial_DefautLocalisationA3();
                        dialogue.ShowDialog();
                        FolderBrowserDialog folderBrowserDialog2 = new FolderBrowserDialog();
                        folderBrowserDialog2.Description = "arma3.exe ?";
                        folderBrowserDialog2.ShowDialog();
                        cheminARMA3 = folderBrowserDialog2.SelectedPath;
                ////
                if (!System.IO.File.Exists(cheminARMA3 + @"\arma3.exe"))
                {
                    MessageBox.Show("impossible de determiner le repertoire d'installation de ARMA3. Le programme va etre arrêté !", "Fichier Arma3.exe Introuvable", MessageBoxButtons.OK);
                    Application.Exit();
                }
            }
            sauvegardeCheminArma3();
            Directory.SetCurrentDirectory(GOSLauncherCore.cheminARMA3);

            // supprime @RESSOURCES BUG
            if (System.IO.Directory.Exists(GOSLauncherCore.cheminARMA3+@"\@GOS\@RESSOURCES\"))
            {
                try
                {
                    System.IO.Directory.Delete(GOSLauncherCore.cheminARMA3 + @"\@GOS\@RESSOURCES\", true);
                }
                catch {
                }
            }

            return;
        }    
        static public void DefinitionConstante()
        {
            constCheminFTP = "37.59.36.179";
            constLoginFTP = "goslauncherA3";
            constMdpFTP = "goslauncherA3";
            repertoireCourant = AppDomain.CurrentDomain.BaseDirectory;
            definirCheminArma3();
            //creation repertoire
            if (!System.IO.Directory.Exists(GOSLauncherCore.cheminARMA3 + @"\userconfig\GOS-LauncherA3"))
            {
                // repertoire n'existe pas
                Directory.CreateDirectory(GOSLauncherCore.cheminARMA3 + @"\userconfig\GOS-LauncherA3");
            }
            if (GOSLauncherCore.GetKeyValue(@"Software\Clan GOS\GOS Launcher A3\", "langage") == "00")
            {
                langue langueform = new langue();
                langueform.ShowDialog();
            }
            Interface.ChangeLangage(GetKeyValue(@"Software\Clan GOS\GOS Launcher A3\", "langage"));
            if (isGOSValid())
           {
               Interface.AfficheMissionServeurMulti();
               timerSynchro.Tick += new EventHandler(TimerSynchroEvent);
               timerSynchro.Interval = 60000; 
               timerSynchro.Start();
           }
        }
        private static void TimerSynchroEvent(Object myObject, EventArgs myEventArgs)
        {
            timerSynchro.Stop();
            if (GOSLauncherCore.fenetrePrincipale.button1.Enabled)
            {
                Interface.AlerteVersionArma3();
                GOSLauncherCore.synchroRsyncTaille("", GOSLauncherCore.fenetrePrincipale.button16, null, null, GOSLauncherCore.fenetrePrincipale.label8, null);
                Interface.AfficheMissionServeurMulti();          
            }
            timerSynchro.Start();
        }
        
        static public void sauvegardeProfil()
        {
            sauvegardeConfigProfilXML("");

        }
        static public void sauvegardeConfigProfilXML(string nomProfil)
        {
            if (nomProfil == "") { nomProfil = (GOSLauncherCore.fenetrePrincipale.comboBox4.SelectedItem as ComboboxItem).Value.ToString(); }
            XmlTextWriter FichierProfilXML = new XmlTextWriter(GOSLauncherCore.cheminARMA3 + @"\userconfig\GOS-LauncherA3\" + nomProfil + ".profil.xml", System.Text.Encoding.UTF8);
            FichierProfilXML.Formatting = Formatting.Indented;
            FichierProfilXML.WriteStartDocument();
            FichierProfilXML.WriteComment("Creation Du profil GOS LAUNCHER " + nomProfil + ".profil.xml"); // commentaire
            FichierProfilXML.WriteStartElement("PROFIL");
            FichierProfilXML.WriteStartElement("MODS_GOS");
            //FRAMEWORK
            FichierProfilXML.WriteStartElement("FRAMEWORK");
            if (fenetrePrincipale.checkedListBox8.CheckedItems.Count != 0)
            {
                for (int x = 0; x <= fenetrePrincipale.checkedListBox8.CheckedItems.Count - 1; x++)
                {
                    FichierProfilXML.WriteElementString("MODS", @"@GOS\@FRAMEWORK\" + fenetrePrincipale.checkedListBox8.CheckedItems[x].ToString());
                }
            }
            FichierProfilXML.WriteEndElement();

            //ISLANDS
            FichierProfilXML.WriteStartElement("ISLANDS");
            if (fenetrePrincipale.checkedListBox1.CheckedItems.Count != 0)
            {
                for (int x = 0; x <= fenetrePrincipale.checkedListBox1.CheckedItems.Count - 1; x++)
                {
                    FichierProfilXML.WriteElementString("MODS", @"@GOS\@ISLANDS\" + fenetrePrincipale.checkedListBox1.CheckedItems[x].ToString());
                }

            }
            FichierProfilXML.WriteEndElement();

            //UNITS
            FichierProfilXML.WriteStartElement("UNITS");
            if (fenetrePrincipale.checkedListBox2.CheckedItems.Count != 0)
            {
                for (int x = 0; x <= fenetrePrincipale.checkedListBox2.CheckedItems.Count - 1; x++)
                {
                    FichierProfilXML.WriteElementString("MODS", @"@GOS\@UNITS\" + fenetrePrincipale.checkedListBox2.CheckedItems[x].ToString());

                }
            }
            FichierProfilXML.WriteEndElement();

            //MATERIEL
            FichierProfilXML.WriteStartElement("MATERIEL");
            if (fenetrePrincipale.checkedListBox3.CheckedItems.Count != 0)
            {
                for (int x = 0; x <= fenetrePrincipale.checkedListBox3.CheckedItems.Count - 1; x++)
                {
                    FichierProfilXML.WriteElementString("MODS", @"@GOS\@MATERIEL\" + fenetrePrincipale.checkedListBox3.CheckedItems[x].ToString());
                }
            }
            FichierProfilXML.WriteEndElement();

            //TEST
            FichierProfilXML.WriteStartElement("TEST");
            if (fenetrePrincipale.checkedListBox4.CheckedItems.Count != 0)
            {
                for (int x = 0; x <= fenetrePrincipale.checkedListBox4.CheckedItems.Count - 1; x++)
                {
                    FichierProfilXML.WriteElementString("MODS", @"@GOS\@TEST\" + fenetrePrincipale.checkedListBox4.CheckedItems[x].ToString());
                }
            }
            FichierProfilXML.WriteEndElement();

            //CLIENT
            FichierProfilXML.WriteStartElement("CLIENT");
            if (fenetrePrincipale.checkedListBox6.CheckedItems.Count != 0)
            {
                for (int x = 0; x <= fenetrePrincipale.checkedListBox6.CheckedItems.Count - 1; x++)
                {
                    FichierProfilXML.WriteElementString("MODS", @"@GOS\@CLIENT\" + fenetrePrincipale.checkedListBox6.CheckedItems[x].ToString());
                }
            }
            FichierProfilXML.WriteEndElement();

            //INTERCLAN
            FichierProfilXML.WriteStartElement("INTERCLAN");
            if (fenetrePrincipale.checkedListBox11.CheckedItems.Count != 0)
            {
                for (int x = 0; x <= fenetrePrincipale.checkedListBox11.CheckedItems.Count - 1; x++)
                {
                    FichierProfilXML.WriteElementString("MODS", @"@GOS\@INTERCLAN\" + fenetrePrincipale.checkedListBox11.CheckedItems[x].ToString());
                }
            }
            FichierProfilXML.WriteEndElement();
            //TEMPLATE
            FichierProfilXML.WriteStartElement("TEMPLATE");
            if (fenetrePrincipale.checkedListBox7.CheckedItems.Count != 0)
            {
                for (int x = 0; x <= fenetrePrincipale.checkedListBox7.CheckedItems.Count - 1; x++)
                {
                    FichierProfilXML.WriteElementString("MODS", @"@GOS\@TEMPLATE\" + fenetrePrincipale.checkedListBox7.CheckedItems[x].ToString());
                }
                // ecrire skin
                if (fenetrePrincipale.comboBox2.Text != "")
                {
                    FichierProfilXML.WriteElementString("MODS", @"@GOS\@TEMPLATE\@GOSSkin_" + fenetrePrincipale.comboBox2.Text);
                    
                }
            }
                // ecrire casque perso

                if (fenetrePrincipale.radioButton20.Checked == true) { FichierProfilXML.WriteElementString("MODS", @"@GOS\@TEMPLATE\@GOSUnit_HelmetsST"); }
                if (fenetrePrincipale.radioButton21.Checked == true) { FichierProfilXML.WriteElementString("MODS", @"@GOS\@TEMPLATE\@GOSUnit_HelmetsXT"); }

            FichierProfilXML.WriteEndElement();
            
            //ARMA3 ROOT

            FichierProfilXML.WriteStartElement("AUTRES_MODS");
            if (fenetrePrincipale.checkedListBox5.CheckedItems.Count != 0)
            {
                for (int x = 0; x <= fenetrePrincipale.checkedListBox5.CheckedItems.Count - 1; x++)
                {
                    FichierProfilXML.WriteElementString("MODS", fenetrePrincipale.checkedListBox5.CheckedItems[x].ToString());
                }
            }
            FichierProfilXML.WriteEndElement();
            //ARMA3 DOCUMENTS
            FichierProfilXML.WriteStartElement("DOC_ARMA3");
            if (fenetrePrincipale.checkedListBox9.CheckedItems.Count != 0)
            {
                for (int x = 0; x <= fenetrePrincipale.checkedListBox9.CheckedItems.Count - 1; x++)
                {
                    FichierProfilXML.WriteElementString("MODS", fenetrePrincipale.checkedListBox9.CheckedItems[x].ToString().Replace(System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments).ToString() + @"\Arma 3\", ""));
                }
            }
            FichierProfilXML.WriteEndElement();
            //*
            //ARMA3 DOCUMENTS OTHER PROFILE

            FichierProfilXML.WriteStartElement("DOC_OTHERPROFILE");
            if (fenetrePrincipale.checkedListBox10.CheckedItems.Count != 0)
            {
                for (int x = 0; x <= fenetrePrincipale.checkedListBox10.CheckedItems.Count - 1; x++)
                {
                    FichierProfilXML.WriteElementString("MODS", fenetrePrincipale.checkedListBox10.CheckedItems[x].ToString());
                }
            }
            FichierProfilXML.WriteEndElement();
             //*/


            // PARAMETRES
            FichierProfilXML.WriteStartElement("PARAMETRES");
            if (fenetrePrincipale.checkBox9.Checked) { FichierProfilXML.WriteElementString("winXP", "true"); } else { FichierProfilXML.WriteElementString("winXP", ""); }
            if (fenetrePrincipale.checkBox5.Checked) { FichierProfilXML.WriteElementString("showScriptErrors", "true"); } else { FichierProfilXML.WriteElementString("showScriptErrors", ""); }
            if (fenetrePrincipale.checkBox4.Checked) { FichierProfilXML.WriteElementString("worldEmpty", "true"); } else { FichierProfilXML.WriteElementString("worldEmpty", ""); }
            if (fenetrePrincipale.checkBox2.Checked) { FichierProfilXML.WriteElementString("noPause", "true"); } else { FichierProfilXML.WriteElementString("noPause", ""); }
            if (fenetrePrincipale.checkBox1.Checked) { FichierProfilXML.WriteElementString("nosplash", "true"); } else { FichierProfilXML.WriteElementString("nosplash", ""); }
            if (fenetrePrincipale.checkBox3.Checked) { FichierProfilXML.WriteElementString("window", "true"); } else { FichierProfilXML.WriteElementString("window", ""); }
            if (fenetrePrincipale.checkBox6.Checked) { FichierProfilXML.WriteElementString("maxMem", fenetrePrincipale.textBox5.Text); } else { FichierProfilXML.WriteElementString("maxMem", ""); }
            if (fenetrePrincipale.checkBox7.Checked) { FichierProfilXML.WriteElementString("cpuCount", fenetrePrincipale.textBox6.Text); } else { FichierProfilXML.WriteElementString("cpuCount", ""); }
            if (fenetrePrincipale.checkBox11.Checked) { FichierProfilXML.WriteElementString("fraps", "true"); } else { FichierProfilXML.WriteElementString("fraps", ""); }
            if (fenetrePrincipale.checkBox12.Checked) { FichierProfilXML.WriteElementString("trackir", "true"); } else { FichierProfilXML.WriteElementString("trackir", ""); }
            if (fenetrePrincipale.checkBox8.Checked) { FichierProfilXML.WriteElementString("noCB", "true"); } else { FichierProfilXML.WriteElementString("noCB", ""); }
            if (fenetrePrincipale.checkBox19.Checked) { FichierProfilXML.WriteElementString("minimize", "true"); } else { FichierProfilXML.WriteElementString("minimize", ""); }
            if (fenetrePrincipale.checkBox23.Checked) { FichierProfilXML.WriteElementString("noFilePatching", "true"); } else { FichierProfilXML.WriteElementString("noFilePatching", ""); }
            if (fenetrePrincipale.checkBox22.Checked) { FichierProfilXML.WriteElementString("VideomaxMem", fenetrePrincipale.textBox20.Text); } else { FichierProfilXML.WriteElementString("VideomaxMem", ""); }
            if (fenetrePrincipale.checkBox21.Checked) { FichierProfilXML.WriteElementString("threadMax", fenetrePrincipale.comboBox3.SelectedIndex.ToString()); } else { FichierProfilXML.WriteElementString("threadMax", ""); }
            if (fenetrePrincipale.checkBox24.Checked) { FichierProfilXML.WriteElementString("adminmode", "true"); } else { FichierProfilXML.WriteElementString("adminmode", ""); }
            if (fenetrePrincipale.checkBox10.Checked) { FichierProfilXML.WriteElementString("nologs", "true"); } else { FichierProfilXML.WriteElementString("nologs", ""); }
            if (fenetrePrincipale.checkBox_HeadlessClient.Checked)
                          {
                           FichierProfilXML.WriteElementString("HC", "true");
                           FichierProfilXML.WriteElementString("HCPort", fenetrePrincipale.textBox2.Text);
                           FichierProfilXML.WriteElementString("HCPassWord", fenetrePrincipale.textBox3.Text);                            
                          }
                    else
                          {
                              FichierProfilXML.WriteElementString("HC", "");                              
                              FichierProfilXML.WriteElementString("HCPort", "");
                              FichierProfilXML.WriteElementString("HCPassWord", "");                            
                          }
            if (fenetrePrincipale.checkBox13.Checked) { FichierProfilXML.WriteElementString("other", fenetrePrincipale.textBox4.Text); } else { FichierProfilXML.WriteElementString("other", ""); }
            if (fenetrePrincipale.checkBox3.Checked) { FichierProfilXML.WriteElementString("windowX", fenetrePrincipale.textBox7.Text); FichierProfilXML.WriteElementString("windowY", fenetrePrincipale.textBox8.Text); } else { FichierProfilXML.WriteElementString("windowX", ""); FichierProfilXML.WriteElementString("windowY", ""); }
            if (fenetrePrincipale.checkBox_EnableHT.Checked) { FichierProfilXML.WriteElementString("enableHT", "true"); } else { FichierProfilXML.WriteElementString("enableHT", ""); }

            FichierProfilXML.WriteEndElement();
            FichierProfilXML.WriteEndElement();
            FichierProfilXML.Flush(); //vide le buffer
            FichierProfilXML.Close(); // ferme le document
            sauvegardePrioriteProfilXML(nomProfil);
        }
        static public void sauvegardePrioriteProfilXML(string nomProfil)
        {
            Priority.actualisePrioriteMods();
            if (nomProfil == "") { nomProfil = (GOSLauncherCore.fenetrePrincipale.comboBox4.SelectedItem as ComboboxItem).Value.ToString(); }
            XmlTextWriter FichierProfilXML = new XmlTextWriter(GOSLauncherCore.cheminARMA3 + @"\userconfig\GOS-LauncherA3\" + nomProfil + ".profilPriorite.xml", System.Text.Encoding.UTF8);
            FichierProfilXML.Formatting = Formatting.Indented;
            FichierProfilXML.WriteStartDocument();
            FichierProfilXML.WriteComment("Creation Du profil GOS LAUNCHER " + nomProfil + ".profil.xml"); // commentaire
            FichierProfilXML.WriteComment("Détermination de la priorité par ordre d'affichage (le plus haut est le plus important"); // commentaire
            FichierProfilXML.WriteComment("> le plus haut est le plus important"); // commentaire
            FichierProfilXML.WriteStartElement("PROFIL");
            FichierProfilXML.WriteStartElement("PRIORITE");
            foreach (string ligne in GOSLauncherCore.fenetrePrincipale.ctrlListModPrioritaire.Items)
            {
                FichierProfilXML.WriteElementString("MODS", ligne);
            }
            FichierProfilXML.WriteEndElement();
            FichierProfilXML.WriteEndElement();
            FichierProfilXML.Flush(); //vide le buffer
            FichierProfilXML.Close(); // ferme le document
        }
        #endregion
         /*
                   INTERFACE
         */
        #region Interface


        static public List<string> GenereListeGOS(string repertoireSource)
        {
            List<string> listeRepertoire = new List<string>();
            if (!System.IO.Directory.Exists(cheminARMA3 + @"\@GOS\" + repertoireSource))
            {
                // repertoire n'existe pas
                Directory.CreateDirectory(cheminARMA3 + @"\@GOS\" + repertoireSource);
            }

            string[] tableauRepertoire = Directory.GetDirectories(cheminARMA3 + @"\@GOS\" + repertoireSource + @"\", "Add*", SearchOption.AllDirectories);

            foreach (var ligne in tableauRepertoire)
            {
                // Genere les Tab Specifiques pour les tenues GOS
                if ((ligne.IndexOf(@"\@GOS\@TEMPLATE\@GOSSkin_") != -1) && (ligne.IndexOf(@"\@GOS\@TEMPLATE\@GOSUnits_Cfg") == -1))
                {
                    string addons = ligne.Replace(cheminARMA3 + @"\@GOS\@TEMPLATE\@GOSSkin_", "");
                    fenetrePrincipale.comboBox2.Items.Add(addons.Replace(@"\addons","")); 
                }
                else
                    if (ligne.IndexOf(@"\@GOS\@TEMPLATE\@GOSUnit_Helmets") != -1) 
                   {
                       if ((ligne.IndexOf(@"@GOS\@TEMPLATE\@GOSUnit_HelmetsST\") != -1)) { fenetrePrincipale.radioButton20.Enabled = true; }
                       if ((ligne.IndexOf(@"@GOS\@TEMPLATE\@GOSUnit_HelmetsXT\") != -1)) { fenetrePrincipale.radioButton21.Enabled = true; }
                   }
                   else
                {
                    string menuRepertoire = System.IO.Directory.GetParent(ligne).ToString();
                    listeRepertoire.Add(menuRepertoire.Replace(cheminARMA3 + @"\@GOS\" + repertoireSource + @"\", ""));
                }
            }
            return listeRepertoire;
            
        }
        static public List<string> GenereListeAUTRE(string repertoireSource)
        {
            List<string> listeRepertoire = new List<string>();
            try
            {
                string[] tableauRepertoire = Directory.GetDirectories(repertoireSource, "Add*", SearchOption.AllDirectories);

                foreach (var ligne in tableauRepertoire)
                {
                    string menuRepertoire = System.IO.Directory.GetParent(ligne).ToString();
                    string nomAAjouter = menuRepertoire;
                    if ((nomAAjouter.IndexOf(cheminARMA3 + @"\@GOS\@ISLANDS\") == -1)
                        && (nomAAjouter.IndexOf(cheminARMA3 + @"\@GOS\@FRAMEWORK\") == -1)
                        && (nomAAjouter.IndexOf(cheminARMA3 + @"\@GOS\@UNITS\") == -1)
                        && (nomAAjouter.IndexOf(cheminARMA3 + @"\@GOS\@MATERIEL\") == -1)
                        && (nomAAjouter.IndexOf(cheminARMA3 + @"\@GOS\@TEMPLATE\") == -1)
                        && (nomAAjouter.IndexOf(cheminARMA3 + @"\@GOS\@CLIENT\") == -1)
                        && (nomAAjouter.IndexOf(cheminARMA3 + @"\@GOS\@TEST\") == -1)
                        && (nomAAjouter.IndexOf(cheminARMA3 + @"\@GOS\@INTERCLAN\") == -1)
                        && (nomAAjouter.IndexOf(".pack") == -1)
                        && (nomAAjouter.IndexOf(".rsync") == -1)
                        )
                    {

                        if ((menuRepertoire) != cheminARMA3)
                        {
                            listeRepertoire.Add(menuRepertoire.Replace(repertoireSource + @"\", ""));
                        }

                    }
                }
            }
            catch
            {
            }
            return listeRepertoire;
        }
        static public void ListeTab(CheckedListBox Tab, string nomRep, string nomProfil)
        {
            List<string> tableauValeur = new List<string>();
            switch (nomRep)
            {
                case "@TEMPLATE":
                case "@FRAMEWORK":
                case "@ISLANDS":
                case "@UNITS":
                case "@MATERIEL":
                case "@CLIENT":
                case "@TEST":
                case "@INTERCLAN":
                    tableauValeur = GenereListeGOS(nomRep);
                    break;
                case "AUTRES_MODS":
                    tableauValeur = GenereListeAUTRE(cheminARMA3);
                    break;
                case "DOC_ARMA3":
                    tableauValeur = GenereListeAUTRE(System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments).ToString() + @"\Arma 3");
                    break;
                case "DOC_OTHERPROFILE":
                    tableauValeur = GenereListeAUTRE(System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments).ToString() + @"\Arma 3 - Other Profiles");
                    break;
            } 
            string tagNameXML="";
            string filtreRepertoire = "";
            switch (nomRep)
            {
                case "AUTRES_MODS":
                    tagNameXML = "AUTRES_MODS";
                    filtreRepertoire = " ";
                    break;
                case "DOC_ARMA3":
                    tagNameXML = "DOC_ARMA3";
                    filtreRepertoire = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments).ToString() + @"\Arma 3\";
                    break;
                case "DOC_OTHERPROFILE":
                    tagNameXML = "DOC_OTHERPROFILE";
                    filtreRepertoire = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments).ToString() + @"\Arma 3 - Other Profiles\";
                    break;
                case "@TEMPLATE":
                    tagNameXML = "TEMPLATE";
                    filtreRepertoire = @"@GOS\@TEMPLATE\";
                    break;
                case "@FRAMEWORK":
                    tagNameXML = "FRAMEWORK";
                    filtreRepertoire = @"@GOS\@FRAMEWORK\";
                    break;
                case "@ISLANDS":
                    tagNameXML = "ISLANDS";
                    filtreRepertoire = @"@GOS\@ISLANDS\";
                    break;
                case "@UNITS":
                    tagNameXML = "UNITS";
                    filtreRepertoire = @"@GOS\@UNITS\";
                    break;
                case "@MATERIEL":
                    tagNameXML = "MATERIEL";
                    filtreRepertoire = @"@GOS\@MATERIEL\";
                    break;
                case "@CLIENT":
                    tagNameXML = "CLIENT";
                    filtreRepertoire = @"@GOS\@CLIENT\";
                    break;
                case "@TEST":
                    tagNameXML = "TEST";
                    filtreRepertoire = @"@GOS\@TEST\";
                    break;
                case "@INTERCLAN":
                    tagNameXML = "INTERCLAN";
                    filtreRepertoire = @"@GOS\@INTERCLAN\";
                    break;
            }
            XmlDocument fichierProfilXML = new XmlDocument();
            if (nomProfil == "") { nomProfil = "defaut"; };
            fichierProfilXML.Load(cheminARMA3 + @"\userconfig\GOS-LauncherA3\" + nomProfil + ".profil.xml");
            foreach (var ligne in tableauValeur)
            {
                bool elementsProfilChecked = false;
                // Read the XmlDocument (Directory Node)
                XmlNodeList elemList = fichierProfilXML.GetElementsByTagName(tagNameXML);
                if (elemList.Count == 0) { Tab.Items.Add(ligne, elementsProfilChecked); }
                for (int i = 0; i < elemList.Count; i++)
                {
                    XmlNodeList eltList = elemList[i].ChildNodes;
                    for (int j = 0; j < eltList.Count; j++)
                    {
                        string repertoireAChercher = eltList[j].InnerXml;                      
                        if  (repertoireAChercher.IndexOf(@"@GOS\@TEMPLATE\@GOSSkin_") != -1)
                        {
                            int indexApparence = 0;
                            foreach (string apparencePossible in fenetrePrincipale.comboBox2.Items)
                            {
                                if (@"@GOS\@TEMPLATE\@GOSSkin_" + apparencePossible == repertoireAChercher)
                                {
                                    fenetrePrincipale.comboBox2.SelectedIndex = indexApparence;
                                }
                                indexApparence++;
                            }

                        }
                        if (repertoireAChercher == @"@GOS\@TEMPLATE\@GOSUnit_HelmetsST") { fenetrePrincipale.radioButton20.Checked = true; }
                        if (repertoireAChercher == @"@GOS\@TEMPLATE\@GOSUnit_HelmetsXT") { fenetrePrincipale.radioButton21.Checked = true; }

                        if (filtreRepertoire + ligne.Replace(filtreRepertoire, "") == filtreRepertoire + repertoireAChercher.Replace(filtreRepertoire, "")) 
                        {
                            elementsProfilChecked = true;
                        }
                        
                    }
                    Tab.Items.Add(ligne.Replace(filtreRepertoire,""), elementsProfilChecked);
                }
                
            }
            // gestion coche des apparences



        }
        static public void SelectionneTousTAB(CheckedListBox tab)
        {
            for (int x = 0; x <= tab.Items.Count - 1; x++)
            {
                tab.SetItemChecked(x, true);
            }
        }
        static public void InverseTousTAB(CheckedListBox tab)
        {
            for (int x = 0; x <= tab.Items.Count - 1; x++)
            {
                if (tab.GetItemChecked(x))
                {
                    tab.SetItemChecked(x, false);
                }
                else
                {
                    tab.SetItemChecked(x, true);
                }
            }
        }
        #endregion
        /*
         *   SERVEUR
         */

        #region Serveur


        static public void generationLigneArguments(string profil)
        {
            listMODS = "-MOD=";
            listArguments = "";

            // Ligne Mods
            Priority.actualisePrioriteMods();
            foreach (string ligne in GOSLauncherCore.fenetrePrincipale.ctrlListModPrioritaire.Items)
            {
                listMODS += ligne + ";";
            }


            // PARAMETRES

            if (fenetrePrincipale.checkBox9.Checked) { listArguments += "-winxp "; }
            if (fenetrePrincipale.checkBox5.Checked) { listArguments += "-showScriptErrors "; }
            if (fenetrePrincipale.checkBox4.Checked) { listArguments += "-world=empty "; }
            if (fenetrePrincipale.checkBox2.Checked) { listArguments += "-noPause "; }
            if (fenetrePrincipale.checkBox1.Checked) { listArguments += "-nosplash "; }
            if (fenetrePrincipale.checkBox3.Checked) {
                listArguments += "-window ";
                if (fenetrePrincipale.textBox7.Text != "") { listArguments += "-X=" + fenetrePrincipale.textBox7.Text + " ";}
                if (fenetrePrincipale.textBox8.Text != "") { listArguments += "-Y=" + fenetrePrincipale.textBox8.Text + " ";}
                                                     }


            if (fenetrePrincipale.checkBox6.Checked) { listArguments += "-maxmem=" + fenetrePrincipale.textBox5.Text + " "; }
            if (fenetrePrincipale.checkBox7.Checked) { listArguments += "-cpuCount=" + fenetrePrincipale.textBox6.Text + " "; }
            if (fenetrePrincipale.checkBox8.Checked) { listArguments += "-noCB "; }
            if (fenetrePrincipale.checkBox10.Checked) { listArguments += "-nologs "; }
            if (fenetrePrincipale.checkBox23.Checked) { listArguments += "-noFilePatching "; }
            if (fenetrePrincipale.checkBox22.Checked) { listArguments += "-maxVRAM=" + fenetrePrincipale.textBox20.Text + " "; }
            if (fenetrePrincipale.checkBox21.Checked) { listArguments += "-exThreads=" + fenetrePrincipale.comboBox3.Text + " "; }
            if (fenetrePrincipale.checkBox_HeadlessClient.Checked) { listArguments += @"-name=""HeadLess Client"" -localhost=127.0.0.1 -connect=localhost -port="+fenetrePrincipale.textBox2.Text+" -password="+fenetrePrincipale.textBox3.Text+" -client -nosound "; }
            if (fenetrePrincipale.checkBox13.Checked) { listArguments += " " + fenetrePrincipale.textBox4.Text + " "; }
            if (fenetrePrincipale.checkBox_EnableHT.Checked) { listArguments += "-enableHT "; }
            if (//profil == "public"|| 
                profil == "interclan")
            {
                //if (profil == "public") { listArguments += @" ""-MOD=@GOS\@TEMPLATE\@CBA_A3;@GOS\@TEMPLATE\@task_force_radio;@GOS\@ISLANDS\@AllInArmaTerrainPack;@GOS\@ISLANDS\@fata;@GOS\@UNITS\@RHSAFRF;@GOS\@UNITS\@RHSUSF;@GOS\@FRAMEWORK\@agm;"" "; };
                if (profil == "interclan") { listArguments += @" """ + GOSLauncherCore.fenetrePrincipale.textBox18.Text + @""" "; }
            }
            else { listArguments += @"""" + listMODS + @""" "; };

            }

        #endregion


 /*
 *   LANCER PROGRAMMES
 */

        #region lancer programme
        
         static public void reductionFenetreOnLaunch()
    {
        if (fenetrePrincipale.checkBox19.Checked)
        {
            fenetrePrincipale.WindowState = FormWindowState.Minimized;
        }
    }
       static public void lancerJeu(string serveur)
        {
            
            if (apparenceValide())
            {
                fenetrePrincipale.button1.Enabled = false;
                generationLigneArguments(serveur);
                if (serveur == "newofficiel") { serveur = @"-connect=37.59.52.201 -port=4442 -password=honneur "; };
                if (serveur == "newmapping") { serveur = @"-connect=37.59.52.201 -port=3302 -password=patrie "; };
                if (serveur == "public") { serveur = @"-connect=37.59.52.201 -port=2902 -password= "; };
                if (serveur == "interclan") { serveur = @"-connect=" + GOSLauncherCore.fenetrePrincipale.textBox10.Text + " -port=" + GOSLauncherCore.fenetrePrincipale.textBox17.Text + " -password=" + GOSLauncherCore.fenetrePrincipale.textBox12.Text + " "; };
                ProgExterne.lancerFraps(); 
                ProgExterne.lancerTrackIR();

                // Lancement Jeu

                reductionFenetreOnLaunch();
                new ProcessSurveillance(GOSLauncherCore.cheminARMA3 + @"\arma3.exe ",serveur + listArguments);
            }
            else
            {
                Form dialogue = new Dial_DefautApparence();
                dialogue.ShowDialog();
            }
        }
       static private void casquesPersoValide()
       {
           if ((fenetrePrincipale.radioButton20.Checked == false) && (fenetrePrincipale.radioButton21.Checked == false))
           {
               fenetrePrincipale.radioButton20.Checked = true;
           }

       }
       static private bool apparenceValide()
       {
           try
           {
               if (fenetrePrincipale.checkedListBox7.GetItemChecked(fenetrePrincipale.checkedListBox7.FindString("@GOSUnits_Cfg")))
               {
                   if (fenetrePrincipale.comboBox2.Text == "")
                   {
                       return false;
                   }
                   else
                   {
                       casquesPersoValide();
                       return true;
                   }

               }
               else
               {
                   fenetrePrincipale.comboBox2.SelectedIndex = 0;
                   fenetrePrincipale.radioButton20.Checked = false;
                   fenetrePrincipale.radioButton21.Checked = false;
                   return true;
               }
           }
           catch
           {
           }
           return true;
       }


#endregion
       
        /*
         *           SYNCHRONISATION      
         */
        #region Synchronisation
 
       

        static public void synchroRsyncTaille(string NomRep, Button BoutonSender, ProgressBar ProgressDetail, ProgressBar ProgressGeneral, Control labelTailleSynchro, Control labelVitesseSynchro)
        {
            DirectoryInfo localDir = new DirectoryInfo(cheminARMA3 + @"\@GOS\" + NomRep);
            FileInfo rsyncExe = new FileInfo(AppDomain.CurrentDomain.BaseDirectory + @"rsync\rsync.exe");
            //String remoteServer = "127.0.0.1";
            String remoteServer = serveurSynchroIP();
            string arguments = "-vza";
            string remoteDir = NomRep.ToUpper();

            if (NomRep == "")
            {
                remoteDir = "@GOS";
                arguments = "";
                if (!GOSLauncherCore.fenetrePrincipale.checkBox_SyncBETA.Checked) { arguments += "--exclude '@TEST/' "; }
                if (!GOSLauncherCore.fenetrePrincipale.checkBox_SyncINTERCLAN.Checked) { arguments += "--exclude '@INTERCLAN/' "; }
                arguments += " -za";
            };

            RSync.RSyncCall rSyncCall = new RSync.RSyncCall(arguments, GOSLauncherCore.fenetrePrincipale, BoutonSender, GOSLauncherCore.fenetrePrincipale.textBox11, ProgressDetail, ProgressGeneral, rsyncExe, remoteServer, remoteDir, localDir, labelTailleSynchro, labelVitesseSynchro);            //new RSync.RSyncCall(fenetrePrincipale, BoutonSender, fenetrePrincipale.textBox11, fenetrePrincipale.progressBar3, fenetrePrincipale.progressBar2, rsyncExe, remoteServer, remoteDir, localDir);
            rSyncCall.setTotalSize(labelTailleSynchro);
        }

        static public void synchroRsyncSpec(string NomRep, Button BoutonSender, ProgressBar ProgressDetail,ProgressBar ProgressGeneral,Control labelTailleSynchro,Control labelVitesseSynchro)
        {
            DirectoryInfo localDir = new DirectoryInfo(cheminARMA3 + @"\@GOS\" + NomRep);
            FileInfo rsyncExe = new FileInfo(AppDomain.CurrentDomain.BaseDirectory + @"rsync\rsync.exe");
            //String remoteServer = "127.0.0.1";
            String remoteServer = serveurSynchroIP();
            string arguments = "-vza";
            string remoteDir = NomRep.ToUpper();
            if (NomRep == "") {
                remoteDir = "@GOS";
                arguments = "--exclude '@TEST/' --exclude '@CLIENT/' --exclude '@FRAMEWORK/' --exclude '@INTERCLAN/' --exclude '@ISLANDS/' --exclude '@MATERIEL/' --exclude '@TEMPLATE/' --exclude '@UNITS/' -za";
                localDir = new DirectoryInfo(cheminARMA3 + @"\@GOS");
                              };

            RSync.RSyncCall rSyncCall = new RSync.RSyncCall(arguments, GOSLauncherCore.fenetrePrincipale, BoutonSender, GOSLauncherCore.fenetrePrincipale.textBox11, ProgressDetail, ProgressGeneral, rsyncExe, remoteServer, remoteDir, localDir, labelTailleSynchro, labelVitesseSynchro);            //new RSync.RSyncCall(fenetrePrincipale, BoutonSender, fenetrePrincipale.textBox11, fenetrePrincipale.progressBar3, fenetrePrincipale.progressBar2, rsyncExe, remoteServer, remoteDir, localDir);
            rSyncCall.addControlToDisable(GOSLauncherCore.fenetrePrincipale.button16);
            rSyncCall.addControlToDisable(GOSLauncherCore.fenetrePrincipale.button1);
            rSyncCall.addControlToDisable(GOSLauncherCore.fenetrePrincipale.comboBox4);
            rSyncCall.addControlToDisable(GOSLauncherCore.fenetrePrincipale.labelSynchronisationInvisible);
            rSyncCall.start();
        }
        static public string serveurSynchroIP()
        {
            string ipserveur = "";
            if (GOSLauncherCore.fenetrePrincipale.radioButton7.Checked) {
                ipserveur = "176.31.118.12";
            };
            if (GOSLauncherCore.fenetrePrincipale.radioButton8.Checked) { 
                ipserveur = "www.clan-gign.net";
            };

            return ipserveur;
        }
        #endregion

        /*
         *         MOT DE PASSE
         */

        #region MOTDEPASSE
        static public bool isGOSValid()
        {
            if (Encoder(GetKeyValue(@"Software\Clan GOS\GOS Launcher A3\", "UnlockPass")) == "ca4dfdd5a617f09246d87b33f8e6da95")
            {
                return true;
            }
            return false;

        }

        static public string Encoder(string MotDePasse)
        {
            System.Security.Cryptography.MD5CryptoServiceProvider x = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] bs = System.Text.Encoding.UTF8.GetBytes(MotDePasse);
            bs = x.ComputeHash(bs);
            System.Text.StringBuilder s = new System.Text.StringBuilder();
            foreach (byte b in bs)
            {
                s.Append(b.ToString("x2").ToLower());
            }
            MotDePasse = s.ToString();
            return MotDePasse;
        }

        #endregion

        /*
         *         OUTILS
         */
        #region outils
        static public void CopyDir(string sourceDir, string destDir)
        {
            DirectoryInfo dir = new DirectoryInfo(sourceDir);
            if (dir.Exists)
            {
                string realDestDir;
                if (dir.Root.Name != dir.Name)
                {
                    realDestDir = Path.Combine(destDir, dir.Name);
                    if (!Directory.Exists(realDestDir))
                        Directory.CreateDirectory(realDestDir);
                }
                else realDestDir = destDir;
                foreach (string d in Directory.GetDirectories(sourceDir))
                    CopyDir(d, realDestDir);
                foreach (string file in Directory.GetFiles(sourceDir))
                {

                    string fileNameDest = Path.Combine(realDestDir, Path.GetFileName(file));
                    //if (!File.Exists(fileNameDest))

                    File.Copy(file, fileNameDest, true);
                }
            }
        }
        static private string testTailleFTP(string nomFichier, string ftpDistant, string login, string motDePasse)
        {
            string resultat = "";
            try
            {
                // test taille fichier FTP

                var req = (FtpWebRequest)WebRequest.Create("ftp://" + ftpDistant + "/" + nomFichier);
                req.Proxy = null;
                req.Credentials = new NetworkCredential(login, motDePasse);

                req.Method = WebRequestMethods.Ftp.GetFileSize;
                using (WebResponse resp = req.GetResponse())
                    resultat = resp.ContentLength.ToString();
            }
            catch
            {
            }
            return resultat;
        }

        static private string testTailleLocal(string cheminFichier)
        {
            FileInfo f = new FileInfo(cheminFichier);
            return f.Length.ToString();
        }
        static public void demandeDroitAdmin()
        {
            if (!testProcessCommeAdministrator())
            {
                
                    // It is not possible to launch a ClickOnce app as administrator directly, so instead we launch the
                    // app as administrator in a new process.
                    var processInfo = new ProcessStartInfo(Assembly.GetExecutingAssembly().CodeBase);

                    // The following properties run the new process as administrator
                    processInfo.UseShellExecute = true;
                    processInfo.Verb = "runas";

                    // Start the new process
                    try
                    {
                        Process.Start(processInfo);
                    }
                    catch (Exception)
                    {
                        // The user did not allow the application to run as administrator
                        MessageBox.Show("Cette application doit etre lancée en mode ADMINISTRATEUR");
                    }

                    // Shut down the current process
                    Application.Exit();
            }
        }
        static private bool testProcessCommeAdministrator()
        {
            var wi = System.Security.Principal.WindowsIdentity.GetCurrent();
            var wp = new System.Security.Principal.WindowsPrincipal(wi);
            return wp.IsInRole(System.Security.Principal.WindowsBuiltInRole.Administrator);
        }
        // recupere clé dans la base de registre

        static public string GetKeyValue(string DirName, string name)
        {
            try
            {
                Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(DirName, true);
                return key.GetValue(name).ToString();
            }
            catch
            {
                return "00";
            } 

        }

        // ecrit clé dans la base de registre

        static public void SetKeyValue(string DirName, string name, string value)
        {
            Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.CurrentUser.CreateSubKey(DirName);
            key.SetValue(name, value);
        }
        #endregion

    }
}
