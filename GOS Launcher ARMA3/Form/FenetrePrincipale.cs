using System;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using System.Net;
using System.Globalization;
using System.Threading;


namespace GOSLauncherA3
{
    public partial class FenetrePrincipale : Form
    {
        Form splashscreen = new SplashScreen();
        string[] argumentGOSLauncher;

        public FenetrePrincipale(string [] args)
        {

            argumentGOSLauncher = args;
            InitializeComponent();
        }

        private void FenetrePrincipale_Load(object sender, EventArgs e)
        {
            splashscreen.Show();      
            CheckForIllegalCrossThreadCalls = false;
            label8.TextChanged += label8_TextChanged;
            label45.TextChanged += label45_TextChanged;
            label46.TextChanged += label46_TextChanged;
            label47.TextChanged += label47_TextChanged;
            label48.TextChanged += label48_TextChanged;
            label49.TextChanged += label49_TextChanged;
            label_TESTTailleSynchro.TextChanged += label50_TextChanged;
            label51.TextChanged += label51_TextChanged;
            labelSynchronisationInvisible.EnabledChanged += labelSynchronisationInvisible_EnabledChanged;



        // PREPARATION INITIALISATION INTERFACE
        GOSLauncherCore.fenetrePrincipale = this; 
        GOSLauncherCore.DefinitionConstante();

            
            /*
                   Config repertoire GOS Launcher
            */

            initialiseFichierConfig();
            Interface.initialiseListeProfil();
            initialiseProfilActif();
            configureInstallationMODS();
            /* 
                Organisation INTERFACE
            */
            //splashscreen.ShowDialog();          
            Interface.dessineInterface();

            // tray icon
            //if (argumentGOSLauncher.Length > 0) ResidentAdmin.initialiseTrayIcon();
            splashscreen.Close();
        }



        void labelSynchronisationInvisible_EnabledChanged(object sender, EventArgs e)
        {
            if (labelSynchronisationInvisible.Enabled == true)
            {
                GOSLauncherA3.Interface.AfficheSynchroActive();
                Interface.dessineInterface();
                Interface.genereTab();
            }
        }

        void label8_TextChanged(object sender, EventArgs e)
        {
            // Taille generale
            if (GOSLauncherCore.fenetrePrincipale.label8.Text.Replace(",",".") == "0.000 Mo")
            {
                GOSLauncherCore.fenetrePrincipale.label8.Text = "A jour";
                GOSLauncherCore.fenetrePrincipale.pictureBox25.Image = GOSLauncherA3.Properties.Resources.valide;
                GOSLauncherCore.fenetrePrincipale.label8.ForeColor = System.Drawing.Color.Black;
            }
            else
            {
                GOSLauncherCore.fenetrePrincipale.pictureBox25.Image = GOSLauncherA3.Properties.Resources.delete;
                GOSLauncherCore.fenetrePrincipale.label8.ForeColor = System.Drawing.Color.Red;
            }
        }


                void label45_TextChanged(object sender, EventArgs e)
                {
                    // Taille generale
                    if (GOSLauncherCore.fenetrePrincipale.label45.Text.Replace(",", ".") == "0.000 Mo")
                    {
                        GOSLauncherCore.fenetrePrincipale.label45.Text = "A jour";
                        GOSLauncherCore.fenetrePrincipale.label45.ForeColor = System.Drawing.Color.Black;
                    }
                    else
                    {
                        GOSLauncherCore.fenetrePrincipale.label45.ForeColor = System.Drawing.Color.Red;
                    }
                }
                void label46_TextChanged(object sender, EventArgs e)
                {
                    // Taille generale
                    if (GOSLauncherCore.fenetrePrincipale.label46.Text.Replace(",", ".") == "0.000 Mo")
                    {
                        GOSLauncherCore.fenetrePrincipale.label46.Text = "A jour";
                        GOSLauncherCore.fenetrePrincipale.label46.ForeColor = System.Drawing.Color.Black;
                    }
                    else
                    {
                        GOSLauncherCore.fenetrePrincipale.label46.ForeColor = System.Drawing.Color.Red;
                    }
                }
                void label47_TextChanged(object sender, EventArgs e)
                {
                    // Taille generale
                    if (GOSLauncherCore.fenetrePrincipale.label47.Text.Replace(",", ".") == "0.000 Mo")
                    {
                        GOSLauncherCore.fenetrePrincipale.label47.Text = "A jour";
                        GOSLauncherCore.fenetrePrincipale.label47.ForeColor = System.Drawing.Color.Black;
                    }
                    else
                    {
                        GOSLauncherCore.fenetrePrincipale.label47.ForeColor = System.Drawing.Color.Red;
                    }
                }
                void label48_TextChanged(object sender, EventArgs e)
                {
                    // Taille generale
                    if (GOSLauncherCore.fenetrePrincipale.label48.Text.Replace(",", ".") == "0.000 Mo")
                    {
                        GOSLauncherCore.fenetrePrincipale.label48.Text = "A jour";
                        GOSLauncherCore.fenetrePrincipale.label48.ForeColor = System.Drawing.Color.Black;
                    }
                    else
                    {
                        GOSLauncherCore.fenetrePrincipale.label48.ForeColor = System.Drawing.Color.Red;
                    }
                }
                void label49_TextChanged(object sender, EventArgs e)
                {
                    // Taille generale
                    if (GOSLauncherCore.fenetrePrincipale.label49.Text.Replace(",", ".") == "0.000 Mo")
                    {
                        GOSLauncherCore.fenetrePrincipale.label49.Text = "A jour";
                        GOSLauncherCore.fenetrePrincipale.label49.ForeColor = System.Drawing.Color.Black;
                    }
                    else
                    {
                        GOSLauncherCore.fenetrePrincipale.label49.ForeColor = System.Drawing.Color.Red;
                    }
                }

                void label50_TextChanged(object sender, EventArgs e)
                {
                    // Taille generale
                    if (GOSLauncherCore.fenetrePrincipale.label_TESTTailleSynchro.Text.Replace(",", ".") == "0.000 Mo")
                    {
                        GOSLauncherCore.fenetrePrincipale.label_TESTTailleSynchro.Text = "A jour";
                        GOSLauncherCore.fenetrePrincipale.label_TESTTailleSynchro.ForeColor = System.Drawing.Color.Black;
                    }
                    else
                    {
                        GOSLauncherCore.fenetrePrincipale.label_TESTTailleSynchro.ForeColor = System.Drawing.Color.Red;
                    }
                }
                void label51_TextChanged(object sender, EventArgs e)
                {
                    // Taille generale
                    if (GOSLauncherCore.fenetrePrincipale.label51.Text.Replace(",", ".") == "0.000 Mo")
                    {
                        GOSLauncherCore.fenetrePrincipale.label51.Text = "A jour";
                        GOSLauncherCore.fenetrePrincipale.label51.ForeColor = System.Drawing.Color.Black;
                    }
                    else
                    {
                        GOSLauncherCore.fenetrePrincipale.label51.ForeColor = System.Drawing.Color.Red;
                    }
                }
                private void label_INTERCLANTailleSynchro_TextChanged(object sender, EventArgs e)
                {
                    // Taille generale
                    if (GOSLauncherCore.fenetrePrincipale.label_INTERCLANTailleSynchro.Text.Replace(",", ".") == "0.000 Mo")
                    {
                        GOSLauncherCore.fenetrePrincipale.label_INTERCLANTailleSynchro.Text = "A jour";
                        GOSLauncherCore.fenetrePrincipale.label_INTERCLANTailleSynchro.ForeColor = System.Drawing.Color.Black;
                    }
                    else
                    {
                        GOSLauncherCore.fenetrePrincipale.label_INTERCLANTailleSynchro.ForeColor = System.Drawing.Color.Red;
                    }

                }
        /***********************************
         
                Procedures PERSO
          
         ***********************************/



        void initialiseFichierConfig()
        {


            if (!System.IO.File.Exists(GOSLauncherCore.cheminARMA3 + @"\userconfig\GOS-LauncherA3\config.ini"))
            {
                //le fichier n'existe pas
                FileStream fs = File.Create(GOSLauncherCore.cheminARMA3 + @"\userconfig\GOS-LauncherA3\config.ini");
                fs.Close();
            }
            if (!System.IO.File.Exists(GOSLauncherCore.cheminARMA3 + @"\userconfig\GOS-LauncherA3\defaut.profil.xml"))
            {
                //le fichier n'existe pas 
                GOSLauncherCore.sauvegardeConfigProfilXML("defaut");

                comboBox4.Text = "defaut";
            }

            string langue = GOSLauncherCore.GetKeyValue(@"Software\Clan GOS\GOS Launcher A3\", "langage");
            if (langue == "fr-FR") { radioButton_Francais.Checked = true; };
            if (langue == "en-US") { radioButton_English.Checked = true; };
            if (langue == "ru-RU") { radioButton_russe.Checked = true; };
            if (langue == "de-DE") { radioButton_allemand.Checked = true; };
            if (langue == "el-GR") { radioButton_grec.Checked = true; };
            if (langue == "es-ES") { radioButton_espagnol.Checked = true; };
        }
        
        public void initialiseProfilActif()
        {
            comboBox4.SelectedIndex = 0;
            int indexNom = 0;          

            foreach (ComboboxItem nomProfil in comboBox4.Items)
            {
                if (nomProfil.Value.ToString() + ".profil.xml" == System.IO.File.ReadAllText(GOSLauncherCore.cheminARMA3 + @"\userconfig\GOS-LauncherA3\config.ini"))
                {
                    comboBox4.SelectedIndex = indexNom;
                }
                indexNom++;
            }
        }

        void genereTabModsImportServeur()
        {
            // efface les onglets

            checkedListBox_Template.Items.Clear();
            checkedListBox_Framework.Items.Clear();
            checkedListBox_Islands.Items.Clear();
            checkedListBox_Units.Items.Clear();
            checkedListBox_Materiel.Items.Clear();
            checkedListBox_Test.Items.Clear();
            pictureBox1.Image = GOSLauncherA3.Properties.Resources.logoGOS;
            //comboBox2.Items.Clear();

            // Recupere et genere les tabs pour chaque repertoire

            //ouvre the XmlDocument
            XmlDocument fichierProfilXML = new XmlDocument();
            fichierProfilXML.Load(GOSLauncherCore.cheminARMA3 + @"\userconfig\GOS-LauncherA3\ImportConfigServeurA3.xml");
            string[] listeRepertoire = Directory.GetDirectories(GOSLauncherCore.cheminARMA3, "Addons*", SearchOption.AllDirectories);

            // Genere les Tab Specifiques pour les tenues GOS
            foreach (var ligne in listeRepertoire)
            {               
                if ((ligne.IndexOf(GOSLauncherCore.cheminARMA3 + @"\@GOS\@TEMPLATE\@GOSUnit_HelmetsST;") != -1)) { radioButton20.Enabled = true; }
                if ((ligne.IndexOf(GOSLauncherCore.cheminARMA3 + @"\@GOS\@TEMPLATE\@GOSUnit_HelmetsXT;") != -1)) { radioButton21.Enabled = true; }
            }

            // TEMPLATE

            foreach (var ligne in listeRepertoire)
            {
                if (ligne.IndexOf(GOSLauncherCore.cheminARMA3 + @"\@GOS\@TEMPLATE") != -1)
                {
                    bool elementsProfilChecked = false;
                    // Read the XmlDocument (Directory Node)
                    XmlNodeList elemList = fichierProfilXML.GetElementsByTagName("TEMPLATE");
                    for (int i = 0; i < elemList.Count; i++)
                    {
                        XmlNodeList eltList = elemList[i].ChildNodes;
                        for (int j = 0; j < eltList.Count; j++)
                        {
                        string repertoireAChercher = eltList[j].InnerXml;                      
                        if  (repertoireAChercher.IndexOf(@"@GOS\@TEMPLATE\@GOSSkin_") != -1)
                        {
                            int indexApparence = 0;
                            foreach (string apparencePossible in comboBox2.Items)
                            {
                                if (@"@GOS\@TEMPLATE\@GOSSkin_" + apparencePossible == repertoireAChercher)
                                {
                                    comboBox2.SelectedIndex = indexApparence;
                                }
                                indexApparence++;
                            }
                        }
                        if ((System.IO.Directory.GetParent(ligne).ToString() + @"\") == GOSLauncherCore.cheminARMA3 + @"\" + eltList[j].InnerXml + @"\")
                            {
                                if (repertoireAChercher == @"@GOS\@TEMPLATE\@GOSUnit_HelmetsST") { radioButton20.Checked = true; }
                                if (repertoireAChercher == @"@GOS\@TEMPLATE\@GOSUnit_HelmetsXT") { radioButton21.Checked = true; }
                                elementsProfilChecked = true;
                            }
                        }
                        string menuRepertoire = System.IO.Directory.GetParent(ligne).ToString();
                        if (menuRepertoire.Replace(GOSLauncherCore.cheminARMA3, "").IndexOf("@") != -1)
                        {
                            if (menuRepertoire.IndexOf("@GOSSkin_") != -1 ||
                                menuRepertoire.IndexOf("@GOSUnit_Helmets") != -1)
                            {
                                //
                            }
                            else
                            {
                                checkedListBox_Template.Items.Add(menuRepertoire.Replace(GOSLauncherCore.cheminARMA3 + @"\@GOS\@TEMPLATE\", ""), elementsProfilChecked);
                            }

                        }
                    }
                }
            }

            // FRAMEWORK

            foreach (var ligne in listeRepertoire)
            {
                if (ligne.IndexOf(GOSLauncherCore.cheminARMA3 + @"\@GOS\@FRAMEWORK") != -1)
                {
                    bool elementsProfilChecked = false;
                    // Read the XmlDocument (Directory Node)
                    XmlNodeList elemList = fichierProfilXML.GetElementsByTagName("FRAMEWORK");
                    for (int i = 0; i < elemList.Count; i++)
                    {
                        XmlNodeList eltList = elemList[i].ChildNodes;
                        for (int j = 0; j < eltList.Count; j++)
                        {
                            string repertoireAChercher = GOSLauncherCore.cheminARMA3 + @"\" + eltList[j].InnerXml + @"\";
                            if ((System.IO.Directory.GetParent(ligne).ToString() + @"\") == repertoireAChercher) { elementsProfilChecked = true; }
                        }
                        string menuRepertoire = System.IO.Directory.GetParent(ligne).ToString();
                        if (menuRepertoire.Replace(GOSLauncherCore.cheminARMA3, "").IndexOf("@") != -1)
                        {
                            checkedListBox_Framework.Items.Add(menuRepertoire.Replace(GOSLauncherCore.cheminARMA3 + @"\@GOS\@FRAMEWORK\", ""), elementsProfilChecked);
                        }
                    }
                }
            }


            // ISLANDS

            foreach (var ligne in listeRepertoire)
            {
                if (ligne.IndexOf(GOSLauncherCore.cheminARMA3 + @"\@GOS\@ISLANDS") != -1)
                {
                    bool elementsProfilChecked = false;
                    // Read the XmlDocument (Directory Node)
                    XmlNodeList elemList = fichierProfilXML.GetElementsByTagName("ISLANDS");
                    for (int i = 0; i < elemList.Count; i++)
                    {
                        XmlNodeList eltList = elemList[i].ChildNodes;
                        for (int j = 0; j < eltList.Count; j++)
                        {
                            string repertoireAChercher = GOSLauncherCore.cheminARMA3 + @"\" + eltList[j].InnerXml + @"\";
                            if ((System.IO.Directory.GetParent(ligne).ToString() + @"\") == repertoireAChercher) { elementsProfilChecked = true; }
                        }
                        string menuRepertoire = System.IO.Directory.GetParent(ligne).ToString();
                        if (menuRepertoire.Replace(GOSLauncherCore.cheminARMA3, "").IndexOf("@") != -1)
                        {
                            checkedListBox_Islands.Items.Add(menuRepertoire.Replace(GOSLauncherCore.cheminARMA3 + @"\@GOS\@ISLANDS\", ""), elementsProfilChecked);
                        }
                    }
                }
            }

            // CLIENT

            foreach (var ligne in listeRepertoire)
            {
                if (ligne.IndexOf(GOSLauncherCore.cheminARMA3 + @"\@GOS\@CLIENT") != -1)
                {
                    bool elementsProfilChecked = false;
                    // Read the XmlDocument (Directory Node)
                    XmlNodeList elemList = fichierProfilXML.GetElementsByTagName("CLIENT");
                    for (int i = 0; i < elemList.Count; i++)
                    {
                        XmlNodeList eltList = elemList[i].ChildNodes;
                        for (int j = 0; j < eltList.Count; j++)
                        {
                            string repertoireAChercher = GOSLauncherCore.cheminARMA3 + @"\" + eltList[j].InnerXml + @"\";
                            if ((System.IO.Directory.GetParent(ligne).ToString() + @"\") == repertoireAChercher) { elementsProfilChecked = true; }
                        }
                        string menuRepertoire = System.IO.Directory.GetParent(ligne).ToString();
                        if (menuRepertoire.Replace(GOSLauncherCore.cheminARMA3, "").IndexOf("@") != -1)
                        {
                            checkedListBox_Client.Items.Add(menuRepertoire.Replace(GOSLauncherCore.cheminARMA3 + @"\@GOS\@CLIENT\", ""), elementsProfilChecked);
                        }
                    }
                }
            }

            // UNITS


            foreach (var ligne in listeRepertoire)
            {
                if (ligne.IndexOf(GOSLauncherCore.cheminARMA3 + @"\@GOS\@UNITS") != -1)
                {
                    bool elementsProfilChecked = false;
                    // Read the XmlDocument (Directory Node)
                    XmlNodeList elemList = fichierProfilXML.GetElementsByTagName("UNITS");
                    for (int i = 0; i < elemList.Count; i++)
                    {
                        XmlNodeList eltList = elemList[i].ChildNodes;
                        for (int j = 0; j < eltList.Count; j++)
                        {
                            string repertoireAChercher = GOSLauncherCore.cheminARMA3 + @"\" + eltList[j].InnerXml + @"\";
                            if ((System.IO.Directory.GetParent(ligne).ToString() + @"\") == repertoireAChercher) { elementsProfilChecked = true; }
                        }
                        string menuRepertoire = System.IO.Directory.GetParent(ligne).ToString();
                        if (menuRepertoire.Replace(GOSLauncherCore.cheminARMA3, "").IndexOf("@") != -1)
                        {
                            checkedListBox_Units.Items.Add(menuRepertoire.Replace(GOSLauncherCore.cheminARMA3 + @"\@GOS\@UNITS\", ""), elementsProfilChecked);
                        }
                    }
                }
            }

            // MATERIEL

            foreach (var ligne in listeRepertoire)
            {
                if (ligne.IndexOf(GOSLauncherCore.cheminARMA3 + @"\@GOS\@MATERIEL") != -1)
                {
                    bool elementsProfilChecked = false;
                    // Read the XmlDocument (Directory Node)
                    XmlNodeList elemList = fichierProfilXML.GetElementsByTagName("MATERIEL");
                    for (int i = 0; i < elemList.Count; i++)
                    {
                        XmlNodeList eltList = elemList[i].ChildNodes;
                        for (int j = 0; j < eltList.Count; j++)
                        {
                            string repertoireAChercher = GOSLauncherCore.cheminARMA3 + @"\" + eltList[j].InnerXml + @"\";
                            if ((System.IO.Directory.GetParent(ligne).ToString() + @"\") == repertoireAChercher) { elementsProfilChecked = true; }
                        }
                        string menuRepertoire = System.IO.Directory.GetParent(ligne).ToString();
                        if (menuRepertoire.Replace(GOSLauncherCore.cheminARMA3, "").IndexOf("@") != -1)
                        {
                            checkedListBox_Materiel.Items.Add(menuRepertoire.Replace(GOSLauncherCore.cheminARMA3 + @"\@GOS\@MATERIEL\", ""), elementsProfilChecked);
                        }
                    }
                }
            }

            // TEST
            foreach (var ligne in listeRepertoire)
            {
                if (ligne.IndexOf(GOSLauncherCore.cheminARMA3 + @"\@GOS\@TEST") != -1)
                {
                    bool elementsProfilChecked = false;
                    // Read the XmlDocument (Directory Node)
                    XmlNodeList elemList = fichierProfilXML.GetElementsByTagName("TEST");
                    for (int i = 0; i < elemList.Count; i++)
                    {
                        XmlNodeList eltList = elemList[i].ChildNodes;
                        for (int j = 0; j < eltList.Count; j++)
                        {
                            string repertoireAChercher = GOSLauncherCore.cheminARMA3 + @"\" + eltList[j].InnerXml + @"\";
                            if ((System.IO.Directory.GetParent(ligne).ToString() + @"\") == repertoireAChercher) { elementsProfilChecked = true; }
                        }
                        string menuRepertoire = System.IO.Directory.GetParent(ligne).ToString();
                        if (menuRepertoire.Replace(GOSLauncherCore.cheminARMA3, "").IndexOf("@") != -1)
                        {
                            checkedListBox_Test.Items.Add(menuRepertoire.Replace(GOSLauncherCore.cheminARMA3 + @"\@GOS\@TEST\", ""), elementsProfilChecked);
                        }
                    }
                }
            }
        }
        static bool validationNomFichier(string nomFichier)
        {
            char[] InvalidFileNameChars = System.IO.Path.GetInvalidFileNameChars();
            foreach (char InvalidFileNameChar in InvalidFileNameChars)
                if (nomFichier.Contains(InvalidFileNameChar.ToString()))
                    return false;
            return true;
        }
        static string ConversionNomFichierValide(string FileName, char RemplaceChar)
        {
            char[] InvalidFileNameChars = System.IO.Path.GetInvalidFileNameChars();
            foreach (char InvalidFileNameChar in InvalidFileNameChars)
                if (FileName.Contains(InvalidFileNameChar.ToString()))
                    FileName = FileName.Replace(InvalidFileNameChar, RemplaceChar);
            return FileName;
        }



        static public void DownloadConfigServeur(string nomFichier, string repertoireFTP, string repertoireLocal)
        {
            // parametre : nom du fichier téléchargé sur le FTP, répertoire d'emplacement dans le FTP, emplacement ou sera enregistré le fichier
            WebClient request = new WebClient();
            request.Credentials = new NetworkCredential(GOSLauncherCore.constLoginFTP, GOSLauncherCore.constMdpFTP);
            byte[] fileData = request.DownloadData(repertoireFTP + "/" + nomFichier);
            FileStream file = File.Create(repertoireLocal + "\\" + nomFichier);
            file.Write(fileData, 0, fileData.Length);
            file.Close();
        }
        static public void UploadConfigServeur(string nomFichier, string repertoireFTP)
        {
            // parametre : nom du fichier téléchargé sur le FTP, répertoire d'emplacement dans le FTP, emplacement ou sera enregistré le fichier
            FtpWebRequest request = (FtpWebRequest)FtpWebRequest.Create(repertoireFTP);
            request.Method = WebRequestMethods.Ftp.UploadFile;
            request.Credentials = new NetworkCredential(GOSLauncherCore.constLoginFTP, GOSLauncherCore.constMdpFTP);
            request.UsePassive = true;
            request.UseBinary = true;
            request.KeepAlive = false;
            FileStream stream = File.OpenRead(nomFichier);
            byte[] buffer = new byte[stream.Length];
            stream.Read(buffer, 0, buffer.Length);
            stream.Close();
            Stream reqStream = request.GetRequestStream();
            reqStream.Write(buffer, 0, buffer.Length);
            reqStream.Close();
        }

        void sauvegardeInfoServeur(string typeServeur)
        {
            if (typeServeur == "LOCAL")
            {
                XmlTextWriter FichierProfilXML = new XmlTextWriter(GOSLauncherCore.cheminARMA3 + @"\userconfig\GOS-LauncherA3\ImportConfigServeurA3.xml", System.Text.Encoding.UTF8);
                FichierProfilXML.Formatting = Formatting.Indented;
                FichierProfilXML.WriteStartDocument();
                FichierProfilXML.WriteComment("Creation Du profil GOS LAUNCHER " + typeServeur); // commentaire
                FichierProfilXML.WriteStartElement("PROFIL");
                FichierProfilXML.WriteStartElement("MODS_GOS");

                //FRAMEWORK
                FichierProfilXML.WriteStartElement("FRAMEWORK");
                if (checkedListBox_Framework.CheckedItems.Count != 0)
                {
                    for (int x = 0; x <= checkedListBox_Framework.CheckedItems.Count - 1; x++)
                    {
                        FichierProfilXML.WriteElementString("MODS", @"@GOS\@FRAMEWORK\" + checkedListBox_Framework.CheckedItems[x].ToString());
                    }
                }
                FichierProfilXML.WriteEndElement();

                //ISLANDS
                FichierProfilXML.WriteStartElement("ISLANDS");
                if (checkedListBox_Islands.CheckedItems.Count != 0)
                {
                    for (int x = 0; x <= checkedListBox_Islands.CheckedItems.Count - 1; x++)
                    {
                        FichierProfilXML.WriteElementString("MODS", @"@GOS\@ISLANDS\" + checkedListBox_Islands.CheckedItems[x].ToString());
                    }
                }
                FichierProfilXML.WriteEndElement();

                //UNITS
                FichierProfilXML.WriteStartElement("UNITS");
                if (checkedListBox_Units.CheckedItems.Count != 0)
                {
                    for (int x = 0; x <= checkedListBox_Units.CheckedItems.Count - 1; x++)
                    {
                        FichierProfilXML.WriteElementString("MODS", @"@GOS\@UNITS\" + checkedListBox_Units.CheckedItems[x].ToString());

                    }
                }
                FichierProfilXML.WriteEndElement();

                //MATERIEL
                FichierProfilXML.WriteStartElement("MATERIEL");
                if (checkedListBox_Materiel.CheckedItems.Count != 0)
                {
                    for (int x = 0; x <= checkedListBox_Materiel.CheckedItems.Count - 1; x++)
                    {
                        FichierProfilXML.WriteElementString("MODS", @"@GOS\@MATERIEL\" + checkedListBox_Materiel.CheckedItems[x].ToString());
                    }
                }
                FichierProfilXML.WriteEndElement();

                //TEST
                FichierProfilXML.WriteStartElement("TEST");
                if (checkedListBox_Test.CheckedItems.Count != 0)
                {
                    for (int x = 0; x <= checkedListBox_Test.CheckedItems.Count - 1; x++)
                    {
                        FichierProfilXML.WriteElementString("MODS", @"@GOS\@TEST\" + checkedListBox_Test.CheckedItems[x].ToString());
                    }
                }
                FichierProfilXML.WriteEndElement();


                //TEMPLATE
                FichierProfilXML.WriteStartElement("TEMPLATE");
                if (checkedListBox_Template.CheckedItems.Count != 0)
                {
                    for (int x = 0; x <= checkedListBox_Template.CheckedItems.Count - 1; x++)
                    {
                        FichierProfilXML.WriteElementString("MODS", @"@GOS\@TEMPLATE\" + checkedListBox_Template.CheckedItems[x].ToString());
                    }
                // ecrire skin
                if (comboBox2.Text != "") { FichierProfilXML.WriteElementString("MODS", @"@GOS\@TEMPLATE\@GOSSkin_"+comboBox2.Text);
                // ecrire casque
                if (radioButton20.Checked == true) { FichierProfilXML.WriteElementString("MODS", @"@GOS\@TEMPLATE\@GOSUnit_HelmetsST"); }
                if (radioButton21.Checked == true) { FichierProfilXML.WriteElementString("MODS", @"@GOS\@TEMPLATE\@GOSUnit_HelmetsXT"); }
                }
                FichierProfilXML.WriteEndElement();
                }
                FichierProfilXML.WriteEndElement();
                FichierProfilXML.WriteEndElement();

                FichierProfilXML.Flush(); //vide le buffer
                FichierProfilXML.Close(); // ferme le document
                UploadConfigServeur(GOSLauncherCore.cheminARMA3 + @"\userconfig\GOS-LauncherA3\ImportConfigServeurA3.xml", @"ftp://37.59.36.179/system/listemod/ImportConfigServeurA3.xml");

            }
        }

 
        string testTailleFTP(string nomFichier, string ftpDistant, string login, string motDePasse)
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
        void CopyDir(string sourceDir, string destDir)
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



        bool downloadnouvelleVersion(string nomFichier, string repertoireFTP, string username, string password, string destinationRepertoire)
        {
            // parametre : nom du fichier téléchargé sur le FTP, répertoire d'emplacement dans le FTP, emplacement ou sera enregistré le fichier
            try
            {

                WebClient request = new WebClient();
                request.Credentials = new NetworkCredential(username, password);
                byte[] fileData = request.DownloadData("ftp://" + repertoireFTP + "/" + nomFichier);
                FileStream file = File.Create(destinationRepertoire + nomFichier);
                file.Write(fileData, 0, fileData.Length);
                file.Close();
                return true;
            }
            catch
            {
                //MessageBox.Show("Impossible de réaliser la mise à jour automatique du programme. Nouvel essai...\n\n"+e,"Erreur Critique",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                return false;
            }
        }
        public void configureInstallationMODS()
        {
            string filename;
            // Test Raven A
            filename = GOSLauncherCore.cheminARMA3 + @"\userconfig\RQ11RAVEN\raven_keys.hpp";
            if (!File.Exists(filename)) //Si le fichier n existe pas
            {
                try
                {
                    //Déplacement du dossier 
                    CopyDir(GOSLauncherCore.cheminARMA3 + @"\@GOS\@TEST\@RQ-11_RAVEN_AB_A3\userconfig\RQ11RAVEN", GOSLauncherCore.cheminARMA3 + @"\userconfig");
                }
                catch
                {
                }
            }

        }
        string testTailleLocal(string cheminFichier)
        {
            FileInfo f = new FileInfo(cheminFichier);
            return f.Length.ToString();
        }
 
     
        /*
         * 
         * 
         *   LISTE ACTION CONTROL 
         * 
         * 
         * 
         */


        private void button29_Click(object sender, EventArgs e)
        {
            Priority.augmentePrioriteMod();
        }
        private void button30_Click(object sender, EventArgs e)
        {
            Priority.diminuePrioriteMod();           
        }
        private void button33_Click(object sender, EventArgs e)
        {
            Priority.actualisePrioriteMods();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string serveur="";
            if (checkBox_SERVEUR_OFFICIEL.Checked) { serveur = "newofficiel"; };
            if (checkBox_SERVEUR_MAPPING.Checked) { serveur = "newmapping"; };
            if (checkBox_SERVEUR_PUBLIC.Checked) { serveur = "public"; };
            GOSLauncherCore.lancerJeu(serveur);

        }
        private void button2_Click(object sender, EventArgs e)
        {
            GOSLauncherCore.SelectionneTousTAB(checkedListBox_Islands);
        }
        private void button3_Click(object sender, EventArgs e)
        {
            GOSLauncherCore.InverseTousTAB(checkedListBox_Islands);
        }
        private void button5_Click(object sender, EventArgs e)
        {
            GOSLauncherCore.SelectionneTousTAB(checkedListBox_Units);
        }
        private void button7_Click(object sender, EventArgs e)
        {
            GOSLauncherCore.SelectionneTousTAB(checkedListBox_Materiel);
        }
        private void button9_Click(object sender, EventArgs e)
        {
            GOSLauncherCore.SelectionneTousTAB(checkedListBox_Test);
        }
        private void button10_Click(object sender, EventArgs e)
        {
            GOSLauncherCore.SelectionneTousTAB(checkedListBox_Client);
        }
        private void button4_Click(object sender, EventArgs e)
        {
            GOSLauncherCore.InverseTousTAB(checkedListBox_Units);
        }
        private void button6_Click(object sender, EventArgs e)
        {
            GOSLauncherCore.InverseTousTAB(checkedListBox_Materiel);
        }
        private void button8_Click(object sender, EventArgs e)
        {
            GOSLauncherCore.InverseTousTAB(checkedListBox_Test);
        }
        private void button11_Click(object sender, EventArgs e)
        {
            GOSLauncherCore.InverseTousTAB(checkedListBox_Client);
        }
        private void button12_Click(object sender, EventArgs e)
        {
            GOSLauncherCore.SelectionneTousTAB(checkedListBox_Template);
        }
        private void button13_Click(object sender, EventArgs e)
        {
            GOSLauncherCore.InverseTousTAB(checkedListBox_Template);
        }

        private void button15_Click(object sender, EventArgs e)
        {
            GOSLauncherCore.SelectionneTousTAB(checkedListBox_MODS_Arma3);
        }

        private void button14_Click(object sender, EventArgs e)
        {
            GOSLauncherCore.InverseTousTAB(checkedListBox_MODS_Arma3);
        }


        private void pictureBox2_Click_1(object sender, EventArgs e)
        {
            timer1.Start();
            label4.Visible = true;
            GOSLauncherCore.sauvegardeProfil();
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            label4.Visible = false;
            label5.Visible = false;
            timer1.Stop();
        }


        private void pictureBox4_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem == null || (GOSLauncherCore.fenetrePrincipale.listBox1.SelectedItem as ComboboxItem).Value.ToString() == "" || (GOSLauncherCore.fenetrePrincipale.listBox1.SelectedItem as ComboboxItem).Value.ToString() == "defaut" || (GOSLauncherCore.fenetrePrincipale.listBox1.SelectedItem as ComboboxItem).Text.ToString() == (GOSLauncherCore.fenetrePrincipale.comboBox4.SelectedItem as ComboboxItem).Text.ToString())
            {
                var infoBox = MessageBox.Show("Impossible d'effacer ce profil si il est celui par defaut ou celui actif.", "Erreur Suppression profil", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    File.Delete(GOSLauncherCore.cheminARMA3 + @"\userconfig\GOS-LauncherA3\" + listBox1.SelectedItem.ToString() + ".profil.xml");
                    File.Delete(GOSLauncherCore.cheminARMA3 + @"\userconfig\GOS-LauncherA3\" + listBox1.SelectedItem.ToString() + ".profilServeur.xml");
                }
                catch
                {
                }
                string profilactif = (GOSLauncherCore.fenetrePrincipale.comboBox4.SelectedItem as ComboboxItem).Text.ToString();
                Interface.initialiseListeProfil();
                int compteur = 0;
                foreach (ComboboxItem profil in GOSLauncherCore.fenetrePrincipale.comboBox4.Items)
                {
                    if (profil.Text.ToString() == profilactif) { GOSLauncherCore.fenetrePrincipale.comboBox4.SelectedIndex = compteur; };
                    compteur++;
                }
            }
        }
        private void pictureBox3_Click(object sender, EventArgs e)
        {
            string nomProfil = ConversionNomFichierValide(textBox1.Text, '_');
            nomProfil = nomProfil.TrimStart();
            string[] listeProfil = Directory.GetFiles(GOSLauncherCore.cheminARMA3 + @"\userconfig\GOS-LauncherA3\", "*.profil.xml", SearchOption.TopDirectoryOnly);
            Boolean nomProfilValid = true;
            foreach (var ligne in listeProfil)
            {
                string textCombo = ligne.Replace(GOSLauncherCore.cheminARMA3 + @"\userconfig\GOS-LauncherA3\", "");
                string nomProfilATester = textCombo.Replace(".profil.xml", "");
                if (nomProfil == nomProfilATester) { nomProfilValid = false; }

            }
            if (nomProfil == "") { nomProfilValid = false; }
            if (nomProfilValid)
            {
                Interface.effaceTousItemsOnglets();
                Interface.effaceTousparamsOnglet();
                Directory.CreateDirectory(GOSLauncherCore.cheminARMA3 + @"\userconfig\GOS-LauncherA3");
                GOSLauncherCore.sauvegardeConfigProfilXML(nomProfil);
                Interface.initialiseListeProfil();
                int compteur = 0;
                foreach (ComboboxItem profil in GOSLauncherCore.fenetrePrincipale.comboBox4.Items)
                {
                    if (profil.Text.ToString() == nomProfil) { GOSLauncherCore.fenetrePrincipale.comboBox4.SelectedIndex = compteur; };
                    compteur++;
                }
            }
            else
            {
                var infoBox = MessageBox.Show("Votre nom de profil n'est pas valide.", "Erreur création profil", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            Interface.genereTab();
        }
        private void pictureBox5_Click(object sender, EventArgs e)
        {
            timer1.Start();
            label5.Visible = true;
            if (listBox1.SelectedItem != null)
            {
                string profilChoisis = (GOSLauncherCore.fenetrePrincipale.listBox1.SelectedItem as ComboboxItem).Value.ToString();
                string text = profilChoisis + ".profil.xml";
                System.IO.File.WriteAllText(GOSLauncherCore.cheminARMA3 + @"\userconfig\GOS-LauncherA3\config.ini", text);

            }

        }
        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox6.Checked) { trackBar1.Enabled = true; textBox5.Enabled = true; textBox5.Text = trackBar1.Value.ToString(); } else { trackBar1.Enabled = false; textBox5.Enabled = false; }

        }
        private void checkBox7_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox7.Checked) { trackBar2.Enabled = true; textBox6.Enabled = true; textBox6.Text = trackBar2.Value.ToString(); } else { trackBar2.Enabled = false; textBox6.Enabled = false; }

        }

        private void button18_Click(object sender, EventArgs e)
        {
            tabControl2.TabPages.Remove(MODs);
        }
        private void button19_Click(object sender, EventArgs e)
        {
            tabControl2.TabPages.Add(MODs);
        }
        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            textBox5.Text = trackBar1.Value.ToString();
        }
        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            textBox6.Text = trackBar2.Value.ToString();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.clan-GOS.fr");
        }

        private void linkLabel1_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.arma2.com/beta-patch.php");
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.clan-GOS.fr");
        }

        private void Priorité_Enter(object sender, EventArgs e)
        {
            Priority.actualisePrioriteMods();
        }

        private void button31_Click(object sender, EventArgs e)
        {
            Priority.topPrioriteMod();
        }

        private void button32_Click(object sender, EventArgs e)
        {
            Priority.downPrioriteMod();
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            pictureBox1.Image = GOSLauncherA3.Properties.Resources.logoGOS;
        }

        private void button39_Click(object sender, EventArgs e)
        {
            if (!System.IO.File.Exists(GOSLauncherCore.cheminARMA3 + @"\userconfig\GOS-LauncherA3\ImportConfigServeurA3.xml"))
            {
                //le fichier n'existe pas
                FileStream fs = File.Create(GOSLauncherCore.cheminARMA3 + @"\userconfig\GOS-LauncherA3\ImportConfigServeurA3.xml");
                fs.Close();
            }
                DownloadConfigServeur("ImportConfigServeurA3.xml", "ftp://37.59.36.179/system/listemod", GOSLauncherCore.cheminARMA3 + @"\userconfig\GOS-LauncherA3\");
                genereTabModsImportServeur();
                MessageBox.Show("Liste des MODS importée.", "Importation Liste MODs", MessageBoxButtons.OK, MessageBoxIcon.Information);
          
        }

        private void button38_Click(object sender, EventArgs e)
        {
            try
            {
                sauvegardeInfoServeur("LOCAL");
                MessageBox.Show("Liste des MODS exportée", "Exportation Liste MODs", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch
            {
            }
        }


        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }


        private void progExterne_Click(object sender, EventArgs e)
        {

        }

        private void button17_Click_1(object sender, EventArgs e)
        {
            Interface.UnlockGOSLauncher();

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/ChristopheTdn/GOS-LAUNCHER-ARMA3/issues");
        }



        private void trackBar3_Scroll(object sender, EventArgs e)
        {
            textBox20.Text = trackBar3.Value.ToString();
        }

        private void checkBox22_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox22.Checked) { trackBar3.Enabled = true; textBox20.Enabled = true; textBox20.Text = trackBar3.Value.ToString(); } else { trackBar3.Enabled = false; textBox20.Enabled = false; }

        }

        private void checkBox21_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox21.Checked)
            {
                comboBox3.Enabled = true;
                if (comboBox3.SelectedIndex == -1)
                   { comboBox3.SelectedIndex = 0; };
            }
            else
            {
                comboBox3.Enabled = false;
                comboBox3.SelectedIndex = -1;
                pictureBox26.Image = GOSLauncherA3.Properties.Resources.delete;
                label34.Enabled = false;
                pictureBox28.Image = GOSLauncherA3.Properties.Resources.delete;
                label35.Enabled = false;
                pictureBox30.Image = GOSLauncherA3.Properties.Resources.delete;
                label36.Enabled = false;
            }
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox3.Text)
            {
                case "0":
                    pictureBox26.Image = GOSLauncherA3.Properties.Resources.delete;
                    label34.Enabled = false;
                    pictureBox28.Image = GOSLauncherA3.Properties.Resources.delete;
                    label35.Enabled = false;
                    pictureBox30.Image = GOSLauncherA3.Properties.Resources.delete;
                    label36.Enabled = false;
                    break;

                case "1":
                    pictureBox26.Image = GOSLauncherA3.Properties.Resources.valide;
                    label34.Enabled = true;
                    pictureBox28.Image = GOSLauncherA3.Properties.Resources.delete;
                    label35.Enabled = false;
                    pictureBox30.Image = GOSLauncherA3.Properties.Resources.delete;
                    label36.Enabled = false;
                    break;
                case "3":
                    pictureBox26.Image = GOSLauncherA3.Properties.Resources.valide;
                    label34.Enabled = true;
                    pictureBox28.Image = GOSLauncherA3.Properties.Resources.valide;
                    label35.Enabled = true;
                    pictureBox30.Image = GOSLauncherA3.Properties.Resources.delete;
                    label36.Enabled = false;
                    break;
                case "5":
                    pictureBox26.Image = GOSLauncherA3.Properties.Resources.valide;
                    label34.Enabled = true;
                    pictureBox28.Image = GOSLauncherA3.Properties.Resources.delete;
                    label35.Enabled = false;
                    pictureBox30.Image = GOSLauncherA3.Properties.Resources.valide;
                    label36.Enabled = true;
                    break;
                case "7":
                    pictureBox26.Image = GOSLauncherA3.Properties.Resources.valide;
                    label34.Enabled = true;
                    pictureBox28.Image = GOSLauncherA3.Properties.Resources.valide;
                    label35.Enabled = true;
                    pictureBox30.Image = GOSLauncherA3.Properties.Resources.valide;
                    label36.Enabled = true;
                    break;

            }
        }

 

        private void groupBox12_Enter(object sender, EventArgs e)
        {

        }

        private void button19_Click_1(object sender, EventArgs e)
        {
        }



        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }


        private void button37_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog CheminA3Server = new FolderBrowserDialog();
            CheminA3Server.SelectedPath = GOSLauncherCore.cheminARMA3;
            CheminA3Server.ShowDialog();
        }

        private void linkLabel2_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/ChristopheTdn/GOS-LAUNCHER-ARMA3");
        }



        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            splashscreen.Close();
        }
        private void button21_Click(object sender, EventArgs e)
        {

            GOSLauncherCore.SelectionneTousTAB(checkedListBox_Framework);
        }

        private void button20_Click(object sender, EventArgs e)
        {
            GOSLauncherCore.InverseTousTAB(checkedListBox_Framework);
        }

        private void button22_Click(object sender, EventArgs e)
        {
            Interface.genereTab();
        }

        private void checkBox_HeadlessClient_CheckedChanged(object sender, EventArgs e)
        {
            GOSLauncherCore.fenetrePrincipale.textBox2.Enabled = GOSLauncherCore.fenetrePrincipale.checkBox_HeadlessClient.Checked;
            GOSLauncherCore.fenetrePrincipale.textBox3.Enabled = GOSLauncherCore.fenetrePrincipale.checkBox_HeadlessClient.Checked;
        }

        private void button24_Click(object sender, EventArgs e)
        {
            GOSLauncherCore.SelectionneTousTAB(checkedListBox_MODS_Docs_Arma3);
        }

        private void button27_Click(object sender, EventArgs e)
        {
            GOSLauncherCore.InverseTousTAB(checkedListBox_MODS_Docs_Arma3);
        }

        private void button33_Click_1(object sender, EventArgs e)
        {
            GOSLauncherCore.SelectionneTousTAB(checkedListBox_MODS_Docs_Arma3_OthersProfiles);
        }

        private void button34_Click(object sender, EventArgs e)
        {
            GOSLauncherCore.InverseTousTAB(checkedListBox_MODS_Docs_Arma3_OthersProfiles);
        }

        private void button28_Click(object sender, EventArgs e)
        {
            Interface.genereTab();
        }



        private void button23_Click(object sender, EventArgs e)
        {
            Interface.genereTab();
        }



        private void button37_Click_1(object sender, EventArgs e)
        {
            GOSLauncherCore.lancerJeu("server1");
        }

        private void button40_Click(object sender, EventArgs e)
        {
            GOSLauncherCore.lancerJeu("mapping");
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            textBox7.Enabled = checkBox3.Checked;
            textBox8.Enabled = checkBox3.Checked;
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox2.Text)
            {
                case "AlphaOne":
                    pictureBox1.Image = GOSLauncherA3.Properties.Resources.AlphaOne;
                    break;
                case "CCE_Mixte":
                    pictureBox1.Image = GOSLauncherA3.Properties.Resources.MAK_CCE_Mixte;
                    break;
                case "CCE_Std":
                    pictureBox1.Image = GOSLauncherA3.Properties.Resources.MAK_CCE_std;
                    break;
                case "DAG":
                    pictureBox1.Image = GOSLauncherA3.Properties.Resources.MAK_DAG;
                    break;
                case "DPM_ARID":
                    pictureBox1.Image = GOSLauncherA3.Properties.Resources.MAK_DPM_ARID;
                    break;
                case "DPM_TROPICAL":
                    pictureBox1.Image = GOSLauncherA3.Properties.Resources.MAK_DPM_TROPIC;
                    break;
                case "Tiger_Green":
                    pictureBox1.Image = GOSLauncherA3.Properties.Resources.MAK_Tiger_Green;
                    break;
                case "Tiger_Golden":
                    pictureBox1.Image = GOSLauncherA3.Properties.Resources.MAK_Tiger_Golden;
                    break;
                case "RU_RASTR":
                    pictureBox1.Image = GOSLauncherA3.Properties.Resources.MAK_RU_Rastr;
                    break;
                case "RU_SURPAT":
                    pictureBox1.Image = GOSLauncherA3.Properties.Resources.MAK_RU_Surpat;
                    break;
                case "RU_VSR":
                    pictureBox1.Image = GOSLauncherA3.Properties.Resources.MAK_RU_VSR;
                    break;
                case "RU_KAMYSH":
                    pictureBox1.Image = GOSLauncherA3.Properties.Resources.MAK_RU_Kamish;
                    break;
                case "LEOPARD_A":
                    pictureBox1.Image = GOSLauncherA3.Properties.Resources.MAK_Leopard_A;
                    break;
                case "LEOPARD_F":
                    pictureBox1.Image = GOSLauncherA3.Properties.Resources.MAK_Leopard_F;
                    break;
                default:
                    pictureBox1.Image = GOSLauncherA3.Properties.Resources.logo_goslauncher;
                    break;
            }

        }

        private void button18_Click_1(object sender, EventArgs e)
        {
            ProgExterne.ReinstallTS3TaskForce();
        }

        private void button19_Click_2(object sender, EventArgs e)
        {
            ProgExterne.lancerTeamspeak3TaskForce();
        }

        private void button35_Click(object sender, EventArgs e)
        {
            GOSLauncherCore.lancerJeu("newofficiel");
        }

        private void button36_Click(object sender, EventArgs e)
        {
            GOSLauncherCore.lancerJeu("newmapping");
        }

        private void button37_Click_2(object sender, EventArgs e)
        {
            GOSLauncherCore.lancerJeu("public");
        }
        private void button16_Click(object sender, EventArgs e)
        {
            GOSLauncherCore.synchroRsyncSpec("", button16, null, null, null, null);
            if (button25.Visible==true) GOSLauncherCore.synchroRsyncSpec("@TEMPLATE", button25, progressBar11, progressBar4, label45, label38);
            if (button26.Visible == true) GOSLauncherCore.synchroRsyncSpec("@ISLANDS", button26, progressBar1, progressBar5, label46, label39);
            if (button41.Visible == true) GOSLauncherCore.synchroRsyncSpec("@MATERIEL", button41, progressBar6, progressBar7, label47, label40);
            if (button42.Visible == true) GOSLauncherCore.synchroRsyncSpec("@UNITS", button42, progressBar8, progressBar9, label48, label42);
            if (button43.Visible == true) GOSLauncherCore.synchroRsyncSpec("@CLIENT", button43, progressBar10, progressBar12, label49, label43);
            if (button_TESTBoutonSynchro.Visible == true) GOSLauncherCore.synchroRsyncSpec("@TEST", button_TESTBoutonSynchro, progressBar_TESTGlobalSynchro, progressBar__TESTFichierSynchro, label_TESTTailleSynchro, label_TESTVitesseSynchro);
            if (button_INTERCLANBoutonSynchro.Visible == true) GOSLauncherCore.synchroRsyncSpec("@INTERCLAN", button_INTERCLANBoutonSynchro, progressBar_INTERCLANGlobalSynchro, progressBar_INTERCLANFichierSynchro, label_INTERCLANTailleSynchro, label_INTERCLANVitesseSynchro);
            if (button45.Visible == true) GOSLauncherCore.synchroRsyncSpec("@FRAMEWORK", button45, progressBar16, progressBar15, label51, label52);
        }
        private void button25_Click(object sender, EventArgs e)
        {
            GOSLauncherCore.synchroRsyncSpec("@TEMPLATE", (Button)sender, progressBar11, progressBar4, label45, label38);
        }

        private void button26_Click(object sender, EventArgs e)
        {
            GOSLauncherCore.synchroRsyncSpec("@ISLANDS", (Button)sender, progressBar1, progressBar5, label46, label39);
        }

        private void button41_Click(object sender, EventArgs e)
        {
            GOSLauncherCore.synchroRsyncSpec("@MATERIEL", (Button)sender, progressBar6, progressBar7, label47, label40);
        }

        private void button42_Click(object sender, EventArgs e)
        {
            GOSLauncherCore.synchroRsyncSpec("@UNITS", (Button)sender, progressBar8, progressBar9, label48, label42);
        }

        private void button43_Click(object sender, EventArgs e)
        {
            GOSLauncherCore.synchroRsyncSpec("@CLIENT", (Button)sender, progressBar10, progressBar12, label49, label43);
        }

        private void button44_Click(object sender, EventArgs e)
        {
            GOSLauncherCore.synchroRsyncSpec("@TEST", (Button)sender, progressBar_TESTGlobalSynchro, progressBar__TESTFichierSynchro, label_TESTTailleSynchro, label_TESTVitesseSynchro);
        }

        private void button45_Click(object sender, EventArgs e)
        {
             GOSLauncherCore.synchroRsyncSpec("@FRAMEWORK", (Button)sender, progressBar16, progressBar15, label51, label52);
        }

 
        private void checkBox14_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_SyncBETA.Checked) {
                GOSLauncherCore.SetKeyValue(@"Software\Clan GOS\GOS Launcher A3\", "Synchro", "beta");
                progressBar__TESTFichierSynchro.Visible = true;
                progressBar_TESTGlobalSynchro.Visible = true;
                label_TESTSynchro.Visible = true;
                label_TESTTailleSynchro.Visible = true;
                label_TESTVitesseSynchro.Visible = true;
                button_TESTBoutonSynchro.Visible = true;                
            }
            else
            {
                GOSLauncherCore.SetKeyValue(@"Software\Clan GOS\GOS Launcher A3\", "Synchro", "officielle");
                progressBar__TESTFichierSynchro.Visible = false;
                progressBar_TESTGlobalSynchro.Visible = false;
                label_TESTSynchro.Visible = false;
                label_TESTTailleSynchro.Visible = false;
                label_TESTVitesseSynchro.Visible = false;
                button_TESTBoutonSynchro.Visible = false;
            }
            Interface.testToutesTaillesSynchroEnLigne();
        }

        private void radioButton7_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton7.Checked)
            {
                GOSLauncherCore.SetKeyValue(@"Software\Clan GOS\GOS Launcher A3\", "Serveur", "SERVEUR1");
            }
            Interface.testToutesTaillesSynchroEnLigne();
        }

        private void radioButton8_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton8.Checked)
            {
                GOSLauncherCore.SetKeyValue(@"Software\Clan GOS\GOS Launcher A3\", "Serveur", "SERVEUR2");
            }
            Interface.testToutesTaillesSynchroEnLigne();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button40_Click_2(object sender, EventArgs e)
        {
            Interface.AfficheChangelog();
        }

        private void checkBox_SyncINTERCLAN_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_SyncINTERCLAN.Checked)
            {
                GOSLauncherCore.SetKeyValue(@"Software\Clan GOS\GOS Launcher A3\", "Synchro_Interclan", "true");
                progressBar_INTERCLANFichierSynchro.Visible = true;
                progressBar_INTERCLANGlobalSynchro.Visible = true;
                label_INTERCLANSynchro.Visible = true;
                label_INTERCLANTailleSynchro.Visible = true;
                label_INTERCLANVitesseSynchro.Visible = true;
                button_INTERCLANBoutonSynchro.Visible = true;
            }
            else
            {
                GOSLauncherCore.SetKeyValue(@"Software\Clan GOS\GOS Launcher A3\", "Synchro_Interclan", "false");
                progressBar_INTERCLANFichierSynchro.Visible = false;
                progressBar_INTERCLANGlobalSynchro.Visible = false;
                label_INTERCLANSynchro.Visible = false;
                label_INTERCLANTailleSynchro.Visible = false;
                label_INTERCLANVitesseSynchro.Visible = false;
                button_INTERCLANBoutonSynchro.Visible = false;
            }
            Interface.testToutesTaillesSynchroEnLigne();
        }

        private void button_INTERCLANBoutonSynchro_Click(object sender, EventArgs e)
        {
            GOSLauncherCore.synchroRsyncSpec("@INTERCLAN", (Button)sender, progressBar_INTERCLANGlobalSynchro, progressBar_INTERCLANFichierSynchro, label_INTERCLANTailleSynchro, label_INTERCLANVitesseSynchro);
        }

        private void button46_Click(object sender, EventArgs e)
        {
            GOSLauncherCore.SelectionneTousTAB(checkedListBox_Interclan);
        }

        private void button44_Click_1(object sender, EventArgs e)
        {
            GOSLauncherCore.InverseTousTAB(checkedListBox_Interclan);
        }

        private void checkedListBox4_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void ctrlListModPrioritaire_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Priorité_Click(object sender, EventArgs e)
        {

        }

        private void tabControl2_Enter(object sender, EventArgs e)
        {

        }

        private void Interclan_Info_Enter(object sender, EventArgs e)
        {
            interclan.init();
        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(interclan.url_organisateur);
        }

 
        private void button47_Click(object sender, EventArgs e)
        {
            GOSLauncherA3.ProgExterne.lancerTeamspeak3TaskForceINTERCLAN(textBox15.Text,textBox16.Text);
        }

        private void button48_Click(object sender, EventArgs e)
        {
            GOSLauncherCore.lancerJeu("interclan");
        }

        private void checkBox14_CheckedChanged_1(object sender, EventArgs e)
        {
            if (checkBox_SERVEUR_OFFICIEL.Checked)
            {
                checkBox_SERVEUR_MAPPING.Checked = false;
                checkBox_SERVEUR_PUBLIC.Checked = false;
            }
        }

        private void checkBox15_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_SERVEUR_MAPPING.Checked)
            {
                checkBox_SERVEUR_OFFICIEL.Checked = false;
                checkBox_SERVEUR_PUBLIC.Checked = false;
            }
        }

        private void checkBox16_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_SERVEUR_PUBLIC.Checked)
            {
                checkBox_SERVEUR_OFFICIEL.Checked = false;
                checkBox_SERVEUR_MAPPING.Checked = false;
            }
        }

        private void pictureBox31_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem == null || (GOSLauncherCore.fenetrePrincipale.listBox1.SelectedItem as ComboboxItem).Value.ToString() == "" || (GOSLauncherCore.fenetrePrincipale.listBox1.SelectedItem as ComboboxItem).Value.ToString() == "defaut" || (GOSLauncherCore.fenetrePrincipale.listBox1.SelectedItem as ComboboxItem).Text.ToString() == (GOSLauncherCore.fenetrePrincipale.comboBox4.SelectedItem as ComboboxItem).Text.ToString())
            {
                var infoBox = MessageBox.Show("Impossible de renommer ce profil si il est celui par defaut ou celui actif.", "Erreur renommage profil", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {

            }
        }

        private void tabControl3_Selected(object sender, TabControlEventArgs e)
        {
            if (GOSLauncherCore.ezbm == null)
                GOSLauncherCore.ezbm = new ezBuildMission(label44, label56, label57, pictureBox32, comboBox1, comboBox5, textBox19);
        }

        private void button35_Click_1(object sender, EventArgs e)
        {
            GOSLauncherCore.ezbm.saveMission();
        }
        // gestion Langage Interface
        private void button_ValiderLangage_Click(object sender, EventArgs e)
        {
            string langage = "fr-FR";
            int i = GOSLauncherCore.fenetrePrincipale.comboBox4.SelectedIndex;

            if (radioButton_Francais.Checked) langage= "fr-FR";
            if (radioButton_allemand.Checked) langage = "de-DE";
            if (radioButton_English.Checked) langage = "en-US";
            if (radioButton_grec.Checked) langage = "el-GR";
            if (radioButton_espagnol.Checked) langage = "es-ES";
            if (radioButton_russe.Checked) langage = "ru-RU";
            Interface.ChangeLangage(langage);
            Controls.Clear();
            InitializeComponent();
            Interface.dessineInterface();
            Interface.initialiseListeProfil();
            GOSLauncherCore.fenetrePrincipale.comboBox4.SelectedIndex = i;
            switch (langage)
            {
                case "de-DE":
                    radioButton_allemand.Checked = true;
                    break;
                case "en-US":
                    radioButton_English.Checked = true;
                    break;
                case "ru-RU":
                    radioButton_russe.Checked = true;
                    break;
                case "el-GR":
                    radioButton_grec.Checked = true;
                    break;
                case "es-ES":
                    radioButton_espagnol.Checked = true;
                    break;
                default:
                    radioButton_Francais.Checked = true;
                    break;
            }
        }


    }
}
    


    

