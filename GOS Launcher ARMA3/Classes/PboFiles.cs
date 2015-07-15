using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PboClassReferenceBuilder
{
    class PboFiles
    {

        public PboFiles(string path)
        {
            Console.WriteLine("#Processing pbo files from " + path);
            string tempDir = System.IO.Path.GetTempPath();
            string[] files = getAllPboFiles(path);
            int count = 0;
            foreach (string s in files)
            {
                string[] tmp = s.Split(new char[] { '\\' });
                string module = (s.Substring(path.Length).Split(new char[] { '\\' }))[2];
                string configDir = tmp[tmp.Length - 1].Substring(0, tmp[tmp.Length - 1].Length - 4);
                count++;
                Console.WriteLine("[" + count + "|" + files.Length + "] " + configDir+".pbo");
                extractConfig(s);
                new ConfigCpp(tempDir + configDir, module);
            }
        }

        public string[] getAllPboFiles(string path)
        {
            string[] pboFiles = null;
            pboFiles = Directory.GetFiles(path, "*.pbo", SearchOption.AllDirectories);
            return pboFiles;
        }

        public void extractConfig(string pboFile)
        {
            string tempDir = System.IO.Path.GetTempPath();
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            startInfo.FileName = "ExtractPboDos.exe";
            startInfo.WorkingDirectory = "mikero";
            startInfo.Arguments = "-P -Y -K -F config.bin \"" + @pboFile + "\" " + @tempDir;
            var proc = Process.Start(startInfo);
            proc.WaitForExit();
            startInfo.Arguments = "-P -Y -K -F config.cpp \"" + @pboFile + "\" " + @tempDir;
            proc = Process.Start(startInfo);
            proc.WaitForExit();
        }

     
    }
}
