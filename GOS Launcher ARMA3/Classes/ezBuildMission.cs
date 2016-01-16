using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GOSLauncherA3
{
    class ezBuildMission
    {

        private string mainPath;
        private Hashtable listMap = new Hashtable();

        private PictureBox pictureBox1;
        private ComboBox comboBox1, comboBox2;
        private Label label1, label2, label3;
        private TextBox textBox1;

        public ezBuildMission(Label label1, Label label2, Label label3, PictureBox pictureBox1, ComboBox comboBox1, ComboBox comboBox2, TextBox textBox1)
        {
            this.comboBox1 = comboBox1;
            this.comboBox2 = comboBox2;

            this.pictureBox1 = pictureBox1;


            this.label1 = label1;
            this.label2 = label2;
            this.label3 = label3;

            this.textBox1 = textBox1;

            initFrame();
        }

        private void initFrame()
        {

            //label1.Parent = pictureBox1;
            //label2.Parent = pictureBox1;
            //label3.Parent = pictureBox1;

            initVersion();
            initMapNames();
            mainPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Arma 3 - Other Profiles";
            string[] SubDirs = Directory.GetDirectories(mainPath);
            comboBox2.Items.Add("Défaut");
            foreach (string s in SubDirs)
            {
                comboBox2.Items.Add(s.Substring(s.LastIndexOf("\\") + 1));
            }

            foreach (DictionaryEntry d in listMap)
            {
                comboBox1.Items.Add(d.Key.ToString());
            }

            comboBox1.SelectedIndexChanged += new System.EventHandler(ezb_updateCombo_SelectedIndexChanged);
        }

        private void initVersion()
        {
            String maps = "";
            using (WebClient Client = new WebClient())
            {
                maps = Client.DownloadString("http://www.clan-gos.fr/forum/ezBuildMission/ver.txt");
            }
        }

        private void initMapNames()
        {
            String maps = "";
            using (WebClient Client = new WebClient())
            {
                maps = Client.DownloadString("http://www.clan-gos.fr/forum/ezBuildMission/TemplateMaps.txt");
            }
            String[] tmaps = maps.Split(new char[] { '=', '\r', '\n' });
            String last = "";
            foreach (string s in tmaps)
            {
                if (s.StartsWith("."))
                    listMap.Add(last, s);
                else
                    last = s;
            }
        }

        public void saveMission()
        {
            if (comboBox1.SelectedItem == null || comboBox1.SelectedItem.ToString() == "")
            {
                MessageBox.Show("Vous devez sélectionner un type de map");
                return;
            }
            if (comboBox2.SelectedItem == null || comboBox2.SelectedItem.ToString() == "")
            {
                MessageBox.Show("Vous devez sélectionner votre profile Arma 3");
                return;
            }
            if (textBox1 == null || textBox1.Text == "")
            {
                MessageBox.Show("Vous devez donner un nom à votre mission");
                return;
            }
            string tempPath = System.IO.Path.GetTempPath();
            string extractPath = tempPath + @"GOSTMP";
            string extMap = getExtMap();
            DirectoryInfo dir = new DirectoryInfo(extractPath);
            if (dir.Exists)
                dir.Delete(true);
            string movePath = mainPath + @"\" + comboBox2.SelectedItem.ToString() + @"\missions\" + textBox1.Text + extMap;
            if (comboBox2.SelectedItem.ToString() == "Défaut") { movePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Arma 3\missions\" + textBox1.Text + extMap;};
            dir = new DirectoryInfo(movePath);
            if (!dir.Exists)
            {
                using (WebClient Client = new WebClient())
                {
                    Client.DownloadFile("http://www.clan-gos.fr/forum/ezBuildMission/TemplateAlwaysUpToDate.php", tempPath + @"\template.zip");
                }
                ZipFile.ExtractToDirectory(tempPath + @"\template.zip", extractPath);
                //Directory.Move(extractPath + @"\[FSF]Template", movePath);
                FileSystem.CopyDirectory(extractPath + @"\[FSF]Template", movePath);
                Directory.Delete(extractPath + @"\[FSF]Template",true);
                MessageBox.Show("La mission a été générée sur la base de la dernière Template G.O.S, cliquez sur OK pour ouvrir le répertoire de Destination.","Creation d'une mission G.O.S",MessageBoxButtons.OK, MessageBoxIcon.Information);
                System.Diagnostics.Process.Start(movePath);
                textBox1.Text = "";
            }
            else
            {
                MessageBox.Show("La mission n'a pas été générée. Le repertoire de destination existe déjà. Veuillez changer le nom de la misison et recommencer.", "Creation d'une mission G.O.S", MessageBoxButtons.OK, MessageBoxIcon.Error);

                textBox1.Text = "";
            }
        }

        private string getExtMap()
        {
            string selected = comboBox1.SelectedItem.ToString();
            return listMap[selected].ToString();
        }

        private void ezb_updateCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                pictureBox1.Load("http://www.clan-gos.fr/forum/ezBuildMission/picmap/" + comboBox1.SelectedItem.ToString().Replace(" ", "") + ".jpg");
            }
            catch (WebException)
            {
                Graphics g = Graphics.FromImage(pictureBox1.Image);
                g.Clear(pictureBox1.BackColor);
            }
        }
    }


}

