using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GOSLauncherA3
{
    class Priority
    {

        static public void actualisePrioriteMods()
        {
           // recupere tous les Mods coché dans une liste
           // Compare Liste Mods avec Liste Tab prioritaire
           

           // Efface ceux qui ne sont plus selectionné
           // Ajoute ceux qui manque en fin de liste

            // Affiche la liste par priorité dans la listeBox
           GOSLauncherCore.ListModsrealUrl.Clear();
            
            foreach (string ligne in  compareListeModsValidesEtListePrioritaire(ListeModsValide(), ListeModsPrioritaire()))
            {
                GOSLauncherCore.fenetrePrincipale.ctrlListModPrioritaire.Items.Add(ligne);
                GOSLauncherCore.ListModsrealUrl.Add(ligne);                
            }
            
        }
        static private List<string> compareListeModsValidesEtListePrioritaire(List<string> listModsValide, List<string> listModsPrioritaire)
        {
            
            // efface de la liste prioritaire les mods non valide
            List<string> Intersection = listModsPrioritaire.Intersect(listModsValide).ToList();
            List<string> Ajout = listModsValide.Except(listModsPrioritaire).ToList();


            // efface le listCheckBox priorité
            GOSLauncherCore.fenetrePrincipale.ctrlListModPrioritaire.Items.Clear();
            return Intersection.Union(Ajout).ToList();
        }
        static private List<string> ListeModsPrioritaire()
        {
            List<string> listeModsPrioritaire = new List<string>();
            int compteur = 0;
            foreach (string lignes in GOSLauncherCore.fenetrePrincipale.ctrlListModPrioritaire.Items)
            {
                listeModsPrioritaire.Add(lignes);
                compteur++;
            }
            return listeModsPrioritaire;
        }
        static private List<string> ListeModsValide()
        {
            List<string> listeModsValide = new List<string>();
            // recupere tous les Mods coché dans une seule liste
            // Template
            foreach (string ligne in ExtractionListeModsValides(GOSLauncherCore.fenetrePrincipale.checkedListBox_Template, @"@GOS\@TEMPLATE\"))
            {
                listeModsValide.Add(ligne);
                if (ligne==@"@GOS\@TEMPLATE\@GOSUnits_Cfg")
                {
                    if (GOSLauncherCore.fenetrePrincipale.comboBox2.Text != "")
                    {
                        listeModsValide.Add(@"@GOS\@TEMPLATE\@GOSSkin_" + GOSLauncherCore.fenetrePrincipale.comboBox2.Text);
                    }
                }
            }
            // Casque Perso
            if (GOSLauncherCore.fenetrePrincipale.radioButton20.Checked) {listeModsValide.Add(@"@GOS\@TEMPLATE\@GOSUnit_HelmetsST");};
            if (GOSLauncherCore.fenetrePrincipale.radioButton21.Checked) { listeModsValide.Add(@"@GOS\@TEMPLATE\@GOSUnit_HelmetsXT"); };
            // FRAMEWORK
            foreach (string ligne in ExtractionListeModsValides(GOSLauncherCore.fenetrePrincipale.checkedListBox_Framework, @"@GOS\@FRAMEWORK\"))
            {
                listeModsValide.Add(ligne);
            }
            // Islands
            foreach (string ligne in ExtractionListeModsValides(GOSLauncherCore.fenetrePrincipale.checkedListBox_Islands, @"@GOS\@ISLANDS\"))
            {
                listeModsValide.Add(ligne);
            }
            // Units
            foreach (string ligne in ExtractionListeModsValides(GOSLauncherCore.fenetrePrincipale.checkedListBox_Units, @"@GOS\@UNITS\"))
            {
                listeModsValide.Add(ligne);
            }
            // Materiel
            foreach (string ligne in ExtractionListeModsValides(GOSLauncherCore.fenetrePrincipale.checkedListBox_Materiel, @"@GOS\@MATERIEL\"))
            {
                listeModsValide.Add(ligne);
            }
            // Client
            foreach (string ligne in ExtractionListeModsValides(GOSLauncherCore.fenetrePrincipale.checkedListBox_Client, @"@GOS\@CLIENT\"))
            {
                listeModsValide.Add(ligne);
            }
            // test
            foreach (string ligne in ExtractionListeModsValides(GOSLauncherCore.fenetrePrincipale.checkedListBox_Test, @"@GOS\@TEST\"))
            {
                listeModsValide.Add(ligne);
            }
            // INTERCLAN
            foreach (string ligne in ExtractionListeModsValides(GOSLauncherCore.fenetrePrincipale.checkedListBox_Interclan, @"@GOS\@INTERCLAN\"))
            {
                listeModsValide.Add(ligne);
            }
            // Autres MODS
            foreach (string ligne in ExtractionListeModsValides(GOSLauncherCore.fenetrePrincipale.checkedListBox_MODS_Arma3, ""))
            {
                listeModsValide.Add(ligne);
            }
            // ARMA 3 DOC
            foreach (string ligne in ExtractionListeModsValides(GOSLauncherCore.fenetrePrincipale.checkedListBox_MODS_Docs_Arma3, ""))
            {
                listeModsValide.Add((System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments).ToString() + @"\Arma 3\") + ligne);
            }
            // ARMA 3 DOC OTHER PROFILE
            foreach (string ligne in ExtractionListeModsValides(GOSLauncherCore.fenetrePrincipale.checkedListBox_MODS_Docs_Arma3_OthersProfiles, ""))
            {
                listeModsValide.Add((System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments).ToString() + @"\Arma 3 - Other Profiles\") + ligne);
            }
            return listeModsValide;
        }
        static private List<string> ExtractionListeModsValides(CheckedListBox ListBox,string cheminModsGOS)
        {
            List<string> listeModsValide= new List<string>();
            int compteur=0;
            foreach (string lignes in ListBox.Items)
            {
                if (ListBox.GetItemChecked(compteur)) { listeModsValide.Add(cheminModsGOS + lignes); }
                compteur++;
            }
            return listeModsValide;

        }


        /*
         *  CONTROL BOUTONS Du FORM
         */
        static public void topPrioriteMod()
        {
            if (GOSLauncherCore.fenetrePrincipale.ctrlListModPrioritaire.SelectedIndex.ToString() != "-1")
            {
                int index;
                string valeur;
                while (GOSLauncherCore.fenetrePrincipale.ctrlListModPrioritaire.SelectedIndex > 0)
                {
                    valeur = GOSLauncherCore.fenetrePrincipale.ctrlListModPrioritaire.SelectedItem.ToString();
                    index = GOSLauncherCore.fenetrePrincipale.ctrlListModPrioritaire.SelectedIndex;
                    GOSLauncherCore.fenetrePrincipale.ctrlListModPrioritaire.Items.RemoveAt(index);
                    GOSLauncherCore.fenetrePrincipale.ctrlListModPrioritaire.Items.Insert(index - 1, valeur);
                    GOSLauncherCore.fenetrePrincipale.ctrlListModPrioritaire.SetSelected(index - 1, true);
                }
            }
        }
        static public void downPrioriteMod()
        {
            if (GOSLauncherCore.fenetrePrincipale.ctrlListModPrioritaire.SelectedIndex.ToString() != "-1")
            {
                while (GOSLauncherCore.fenetrePrincipale.ctrlListModPrioritaire.SelectedIndex < GOSLauncherCore.fenetrePrincipale.ctrlListModPrioritaire.Items.Count - 1)
                {
                    string valeur = GOSLauncherCore.fenetrePrincipale.ctrlListModPrioritaire.SelectedItem.ToString();
                    int index = GOSLauncherCore.fenetrePrincipale.ctrlListModPrioritaire.SelectedIndex;
                    GOSLauncherCore.fenetrePrincipale.ctrlListModPrioritaire.Items.RemoveAt(index);
                    GOSLauncherCore.fenetrePrincipale.ctrlListModPrioritaire.Items.Insert(index + 1, valeur);
                    GOSLauncherCore.fenetrePrincipale.ctrlListModPrioritaire.SetSelected(index + 1, true);
                }
            }
        }
        static public void augmentePrioriteMod()
        {
            if (GOSLauncherCore.fenetrePrincipale.ctrlListModPrioritaire.SelectedIndex.ToString() != "-1")
            {
                string valeur;
                int index;
                if (GOSLauncherCore.fenetrePrincipale.ctrlListModPrioritaire.SelectedIndex > 0)
                {
                    valeur = GOSLauncherCore.fenetrePrincipale.ctrlListModPrioritaire.SelectedItem.ToString();
                    index = GOSLauncherCore.fenetrePrincipale.ctrlListModPrioritaire.SelectedIndex;
                    GOSLauncherCore.fenetrePrincipale.ctrlListModPrioritaire.Items.RemoveAt(index);
                    GOSLauncherCore.fenetrePrincipale.ctrlListModPrioritaire.Items.Insert(index - 1, valeur);
                    GOSLauncherCore.fenetrePrincipale.ctrlListModPrioritaire.SetSelected(index - 1, true);
                }
            }
        }
        static public void diminuePrioriteMod()
        {
            if (GOSLauncherCore.fenetrePrincipale.ctrlListModPrioritaire.SelectedIndex.ToString() != "-1")
            {
                if (GOSLauncherCore.fenetrePrincipale.ctrlListModPrioritaire.SelectedIndex < GOSLauncherCore.fenetrePrincipale.ctrlListModPrioritaire.Items.Count - 1)
                {
                    string valeur = GOSLauncherCore.fenetrePrincipale.ctrlListModPrioritaire.SelectedItem.ToString();
                    int index = GOSLauncherCore.fenetrePrincipale.ctrlListModPrioritaire.SelectedIndex;
                    GOSLauncherCore.fenetrePrincipale.ctrlListModPrioritaire.Items.RemoveAt(index);
                    GOSLauncherCore.fenetrePrincipale.ctrlListModPrioritaire.Items.Insert(index + 1, valeur);
                    GOSLauncherCore.fenetrePrincipale.ctrlListModPrioritaire.SetSelected(index + 1, true);
                }
            }
        }
    }
}
