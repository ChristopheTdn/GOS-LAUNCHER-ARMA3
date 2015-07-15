using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace GOSLauncherA3
{
    class interclan
    {
        static public string url_organisateur;
        static public void init()
        {
            try
            {
                if (System.IO.File.Exists(GOSLauncherCore.cheminARMA3 + @"\@GOS\@INTERCLAN\interclan.xml"))
                {
                    string MODS = "-MOD=";
                    XmlTextReader fichierProfilXML = new XmlTextReader(GOSLauncherCore.cheminARMA3 + @"\@GOS\@INTERCLAN\interclan.xml");
                    while (fichierProfilXML.Read())
                    {
                        // nom evenement
                        fichierProfilXML.ReadToFollowing("name");
                        string name = fichierProfilXML.ReadString();
                        if (name != "") { GOSLauncherCore.fenetrePrincipale.textBox13.Text = name;};
                        // date
                        fichierProfilXML.ReadToFollowing("date");
                        string date = fichierProfilXML.ReadString();
                        if (date != "") { GOSLauncherCore.fenetrePrincipale.textBox14.Text = date; };
                        // organisateur
                        fichierProfilXML.ReadToFollowing("organisateur");
                        string organisateur = fichierProfilXML.ReadString();
                        if (organisateur != "") { GOSLauncherCore.fenetrePrincipale.linkLabel4.Text = organisateur; };
                        fichierProfilXML.ReadToFollowing("url_organisateur");
                        string organisateur_link = fichierProfilXML.ReadString();
                        if (organisateur_link != "") { url_organisateur = organisateur_link; }
                        // Teamspeak
                        fichierProfilXML.ReadToFollowing("teamspeak");
                        string teamspeak = fichierProfilXML.ReadString();
                        if (teamspeak != "") { GOSLauncherCore.fenetrePrincipale.textBox15.Text = teamspeak; };
                        fichierProfilXML.ReadToFollowing("teamspeak_pass");
                        string teamspeak_pass = fichierProfilXML.ReadString();
                        if (teamspeak_pass != "") { GOSLauncherCore.fenetrePrincipale.textBox16.Text = teamspeak_pass; }
                        // Serveur
                        fichierProfilXML.ReadToFollowing("serveur_ip");
                        string serveur_ip = fichierProfilXML.ReadString();
                        if (serveur_ip != "") { GOSLauncherCore.fenetrePrincipale.textBox10.Text = serveur_ip; };
                        fichierProfilXML.ReadToFollowing("serveur_port");
                        string serveur_port = fichierProfilXML.ReadString();
                        if (serveur_port != "") { GOSLauncherCore.fenetrePrincipale.textBox17.Text = serveur_port; }
                        fichierProfilXML.ReadToFollowing("serveur_pass");
                        string serveur_pass = fichierProfilXML.ReadString();
                        if (serveur_pass != "") { GOSLauncherCore.fenetrePrincipale.textBox12.Text = serveur_pass; }
                        // Description
                        fichierProfilXML.ReadToFollowing("description");
                        string description = fichierProfilXML.ReadString();
                        if (description != "") { GOSLauncherCore.fenetrePrincipale.textBox9.Text = description; };
                        // MODS
                        fichierProfilXML.ReadToFollowing("MODS");
                        string Mods_item = fichierProfilXML.ReadString();
                        if (Mods_item != "") { MODS += Mods_item + " ";  };
                    }
                    GOSLauncherCore.fenetrePrincipale.textBox18.Text = MODS;
                    fichierProfilXML.Close();

                }

                
            }
            catch { }
            

        }
    }
}
