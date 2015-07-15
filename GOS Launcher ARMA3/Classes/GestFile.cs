using GOSLauncherA3;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace Arma3Launcher
{
    class GestFile
    {
        public bool downloadFile(string nomFichier, string repertoireFTP, string username, string password, string destinationRepertoire)
        {
            string data = "";
            // parametre : nom du fichier téléchargé sur le FTP, répertoire d'emplacement dans le FTP, emplacement ou sera enregistré le fichier
            try
            {
                WebClient request = new WebClient();
                request.Credentials = new NetworkCredential(username, password);
                data = "ftp://" + username + ":" + password + @"@" + repertoireFTP + "/" + nomFichier+".pbo";
                byte[] fileData = request.DownloadData(data);
                FileStream file = File.Create(destinationRepertoire + nomFichier + ".pbo");
                file.Write(fileData, 0, fileData.Length);
                file.Close();
                return true;
            }
            catch 
            {
                return false;
            }
        }
        public bool  extract_FichierSQM(string pboFile)
        {
            try
            {
                string tempDir = System.IO.Path.GetTempPath().Substring(0, System.IO.Path.GetTempPath().Length- 1);
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.WindowStyle = ProcessWindowStyle.Hidden;
                startInfo.FileName = "ExtractPboDos.exe";
                startInfo.WorkingDirectory = "mikero";
                string argument = @"-P -Y -K -F mission.sqm """ + pboFile + @""" """ + tempDir + @"""";
                startInfo.Arguments = argument;
                var proc = Process.Start(startInfo);
                proc.WaitForExit();
                return true;
            }
            catch {return false;}
        }
        public bool readClassFromMissionSQM(string NomMission)
        {
            List<string> listeAddons = new List<string> { };
            List<string> listeXMLAddons = new List<string> { };
            // Recupere liste des MODS dans le mission.sqm
            try {
                string[] lignes = System.IO.File.ReadAllLines(System.IO.Path.GetTempPath() +@"\"+ NomMission + @"\mission.sqm");

                bool startCfgAddonsclasses = false;
                foreach (string ligne in lignes)
                {
                    string testLigne = ligne.Trim();
                    if (startCfgAddonsclasses)
                    {
                        char[] charsToTrim = { '"', ',', '\'',' ','\t'};
                        testLigne = testLigne.Trim(charsToTrim);
                        if (testLigne != "};") listeAddons.Add(testLigne); else break;
                    }
                    if (testLigne.ToLower() == "addons[]=") startCfgAddonsclasses = true;
                }
            }
            catch
            {
                
                return false;
            }
            // creation du fichier IMPORT
            List<string> listeXMLAddons_TEMPLATE = new List<string> {};
            List<string> listeXMLAddons_CLIENT = new List<string> {};
            List<string> listeXMLAddons_ISLANDS = new List<string> { };
            List<string> listeXMLAddons_TEST = new List<string> { };
            List<string> listeXMLAddons_UNITS = new List<string> { };
            List<string> listeXMLAddons_MATERIEL = new List<string> { };
            List<string> listeXMLAddons_FRAMEWORK = new List<string> { };

            try
            {
                string[] refModsLignes = System.IO.File.ReadAllLines(GOSLauncherCore.cheminARMA3 + @"\@GOS\@TEMPLATE\GOS_launcher\PBOClassReference.txt");

                // TEMPLATE
                foreach (string ligne in refModsLignes)
                {
                    foreach (string addon in listeAddons)
                    {
                        string ligneLower = ligne.ToLower();
                        if (ligneLower.IndexOf(addon.ToLower()) != -1)
                        {
                            // traitement 
                            string addonslink = ligne.Substring(0, ligne.IndexOf(":"));

                                if (addonslink.IndexOf(@"@TEMPLATE") != -1 && !listeXMLAddons_TEMPLATE.Contains(addonslink))
                                {
                                    if (addonslink == @"@TEMPLATE\@GOS_Template" )
                                    {
                                        listeXMLAddons_TEMPLATE.Add(@"@GOS\@TEMPLATE\@CBA_A3");
                                        listeXMLAddons_TEMPLATE.Add(@"@GOS\@TEMPLATE\@GOS_Template");
                                        listeXMLAddons_TEMPLATE.Add(@"@GOS\@TEMPLATE\@GOS_Template_Revive");
                                        listeXMLAddons_TEMPLATE.Add(@"@GOS\@TEMPLATE\@GOS_EquilibreMunitions");
                                        listeXMLAddons_TEMPLATE.Add(@"@GOS\@TEMPLATE\@GOS_Halo");
                                        listeXMLAddons_TEMPLATE.Add(@"@GOS\@TEMPLATE\@GOS_TOOLS");
                                        listeXMLAddons_TEMPLATE.Add(@"@GOS\@TEMPLATE\@GOSUnits_Cfg");
                                        listeXMLAddons_TEMPLATE.Add(@"@GOS\@TEMPLATE\@task_force_radio");
                                        listeXMLAddons_TEMPLATE.Add(@"@GOS\@TEMPLATE\@Task_Force_XL_GOS");
                                    }
                                    else
                                    {
                                        listeXMLAddons_TEMPLATE.Add(@"@GOS\" + addonslink);
                                    }
                                }
                                if (addonslink.IndexOf(@"@CLIENT") != -1 && !listeXMLAddons_CLIENT.Contains(addonslink))
                                {
                                    listeXMLAddons_CLIENT.Add(@"@GOS\" + addonslink);
                                }
                                if (addonslink.IndexOf(@"@FRAMEWORK") != -1 && !listeXMLAddons_FRAMEWORK.Contains(addonslink))
                            {
                                    listeXMLAddons_FRAMEWORK.Add(@"@GOS\" + addonslink);
                                }
                                if (addonslink.IndexOf(@"@TEST") != -1 && !listeXMLAddons_TEST.Contains(addonslink))
                            {
                                    listeXMLAddons_TEST.Add(@"@GOS\" + addonslink);
                                }
                                if (addonslink.IndexOf(@"@ISLANDS") != -1 && !listeXMLAddons_ISLANDS.Contains(addonslink))
                            {
                                    listeXMLAddons_ISLANDS.Add(@"@GOS\" + addonslink);
                                if (addonslink.IndexOf(@"@GOS_Nziwasogo") != -1 ||
                                    addonslink.IndexOf(@"@GOS_Dariyah") != -1 ||
                                    addonslink.IndexOf(@"@GOS_KaluKhan") != -1 ||
                                    addonslink.IndexOf(@"@GOS_Koplic") != -1 ||
                                    addonslink.IndexOf(@"@panthera") != -1 ||
                                    addonslink.IndexOf(@"@GOS_Gunkizli") != -1)
                                {
                                    if (!listeXMLAddons_ISLANDS.Contains("@AllInArmaTerrainPack")) listeXMLAddons_ISLANDS.Add(@"@GOS\@ISLANDS\@AllInArmaTerrainPack");
                                    if (!listeXMLAddons_ISLANDS.Contains("@GOS_BibliothequeCommune")) listeXMLAddons_ISLANDS.Add(@"@GOS\@ISLANDS\@GOS_BibliothequeCommune");
                                }
                                if (addonslink.IndexOf(@"@isladuala") != -1 )
                                {
                                    if (!listeXMLAddons_ISLANDS.Contains("@AllInArmaTerrainPack")) listeXMLAddons_ISLANDS.Add(@"@GOS\@ISLANDS\@AllInArmaTerrainPack");
                                    if (!listeXMLAddons_ISLANDS.Contains("@GOS_BibliothequeCommune")) listeXMLAddons_ISLANDS.Add(@"@GOS\@ISLANDS\@GOS_BibliothequeCommune");
                                    if (!listeXMLAddons_ISLANDS.Contains("@panthera")) listeXMLAddons_ISLANDS.Add(@"@GOS\@ISLANDS\@panthera");
                                }
                            }
                                if (addonslink.IndexOf(@"@UNITS") != -1 && !listeXMLAddons_UNITS.Contains(addonslink))
                            {
                                    listeXMLAddons_UNITS.Add(@"@GOS\" + addonslink);
                                }
                                if (addonslink.IndexOf(@"@MATERIEL") != -1 && !listeXMLAddons_MATERIEL.Contains(addonslink))
                            {
                                    listeXMLAddons_MATERIEL.Add(@"@GOS\" + addonslink);
                                }

                            
                        }
                    }
                }
            }
            catch
            {
                return false;
            }
            // SAUVEGARDE LISTE ADDONS
            XmlTextWriter FichierProfilXML = new XmlTextWriter(GOSLauncherCore.cheminARMA3 + @"\userconfig\GOS-LauncherA3\ImportConfigServeurA3.xml", System.Text.Encoding.UTF8);
            FichierProfilXML.Formatting = Formatting.Indented;
            FichierProfilXML.WriteStartDocument();
            FichierProfilXML.WriteComment("Creation Du profil GOS LAUNCHER "); // commentaire
            FichierProfilXML.WriteStartElement("PROFIL");
            FichierProfilXML.WriteStartElement("MODS_GOS");

            //TEMPLATE
            FichierProfilXML.WriteStartElement("TEMPLATE");
            foreach (string line in listeXMLAddons_TEMPLATE)
            {
                FichierProfilXML.WriteElementString("MODS", line);
            }
            FichierProfilXML.WriteEndElement();

            //FRAMEWORK
            FichierProfilXML.WriteStartElement("FRAMEWORK");
            foreach (string line in listeXMLAddons_FRAMEWORK)
            {
                FichierProfilXML.WriteElementString("MODS", line);
            }
            FichierProfilXML.WriteEndElement();
            //ISLANDS
            FichierProfilXML.WriteStartElement("ISLANDS");
            foreach (string line in listeXMLAddons_ISLANDS)
            {
                FichierProfilXML.WriteElementString("MODS", line);
            }
            FichierProfilXML.WriteEndElement();
            //MATERIEL
            FichierProfilXML.WriteStartElement("MATERIEL");
            foreach (string line in listeXMLAddons_MATERIEL)
            {
                FichierProfilXML.WriteElementString("MODS", line);
            }
            FichierProfilXML.WriteEndElement();
            //UNITS
            FichierProfilXML.WriteStartElement("UNITS");
            foreach (string line in listeXMLAddons_UNITS)
            {
                FichierProfilXML.WriteElementString("MODS", line);
            }
            FichierProfilXML.WriteEndElement();
            //TEST
            FichierProfilXML.WriteStartElement("TEST");
            foreach (string line in listeXMLAddons_TEST)
            {
                FichierProfilXML.WriteElementString("MODS", line);
            }
            FichierProfilXML.WriteEndElement();
            //CLIENT
            FichierProfilXML.WriteStartElement("CLIENT");
            foreach (string line in listeXMLAddons_CLIENT)
            {
                FichierProfilXML.WriteElementString("MODS", line);
            }
            FichierProfilXML.WriteEndElement();
            FichierProfilXML.WriteEndElement();
            FichierProfilXML.WriteEndElement();

            FichierProfilXML.Flush(); //vide le buffer
            FichierProfilXML.Close(); // ferme le document

            return true;
            
        }
   
            }
        }
    

