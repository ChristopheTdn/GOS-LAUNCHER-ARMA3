using System;
using System.IO;

namespace PboClassReferenceBuilder
{
    class ConfigCpp
    {
        public ConfigCpp(string configDir,string module)
        {
            string[] cppFiles = getAllCppFiles(configDir);
            if (cppFiles.Length == 0)
                Console.WriteLine("No files");
            foreach (string s in cppFiles)
                readClassFromCpp(s, module);
            try
            {
                Directory.Delete(configDir, true);
            }
            catch
            {
                //Console.WriteLine(e.Message);
            }
        }


        public string[] getAllCppFiles(string path)
        {
            try
            {
                string[] pboFiles = null;
                pboFiles = Directory.GetFiles(path, "*.cpp", SearchOption.AllDirectories);
                return pboFiles;
            }
            catch 
            {
                return new string[0];
            }
        }

        private void readClassFromCpp(string cppFile,string reference)
        {
            string lineOfText = "";
            bool startCfgPatches = false;
            var file = new StreamReader(cppFile);
            while ((lineOfText = file.ReadLine()) != null)
            {
                if (lineOfText.ToLower().IndexOf("class ") > -1 && startCfgPatches)
                {
                    //Program.tw.WriteLine(reference + ":" + lineOfText.Trim().Substring(6).Replace(" ","").Replace("{",""));
                    file.Close();
                    return;
                }
                if (lineOfText.ToLower().IndexOf("class cfgpatches") > -1)
                    startCfgPatches = true;
            }
            file.Close();
        }
    }
}
