using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Diagnostics;
using System.Reflection;

namespace GOSLauncherA3
{
    class ProcessSurveillance
    {
        //private Form fenetrePrincipale;

        public ProcessSurveillance(string ligneCmd,string param)
        {
            Process myProcess = new Process();
            myProcess.EnableRaisingEvents = true;
            myProcess.StartInfo.UseShellExecute = true;
            myProcess.Exited += new EventHandler(ProcessExited);
            
            if (GOSLauncherCore.fenetrePrincipale.checkBox24.Checked)
            {
                myProcess.StartInfo.Verb = "runas";
            }
             
            myProcess.StartInfo.FileName = ligneCmd;
            myProcess.StartInfo.Arguments = "0 1 "+param;
            myProcess.Start();
        }

        public void ProcessExited(object sender, EventArgs e)
        {
            // Invocation d'une méthode du thread graphique
            GOSLauncherCore.fenetrePrincipale.Invoke(new MethodInvoker(ProcessDisabled));
        }

        static public void ProcessDisabled()
        {
            GOSLauncherCore.fenetrePrincipale.button1.Enabled = true;
        }
       
    }
}
