using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GOSLauncherA3
{
    class ResidentAdmin
    {
        static public NotifyIcon trayIcon = new NotifyIcon();
        static public ContextMenu trayMenu = new ContextMenu();
        static public void initialiseTrayIcon()
        {

            /*
            GOSLauncherCore.fenetrePrincipale.WindowState = FormWindowState.Minimized;
            GOSLauncherCore.fenetrePrincipale.Visible = false;
            GOSLauncherCore.fenetrePrincipale.ShowInTaskbar = false;
            */
            GOSLauncherCore.fenetrePrincipale.Icon = GOSLauncherA3.Properties.Resources.GOSLauncherA3;  

            //Init trayMenu
            trayMenu.MenuItems.Add("&Ouvrir GOS Launcher", OuvrirGOSLauncher);
            trayMenu.MenuItems.Add("&Rendre résident");

            trayMenu.MenuItems.Add("-");
            trayMenu.MenuItems.Add("&Quitter");

            //Init trayIcon
            trayIcon.Text = GOSLauncherCore.fenetrePrincipale.Text;
            trayIcon.ContextMenu = trayMenu;
            trayIcon.Icon = GOSLauncherCore.fenetrePrincipale.Icon;
            trayIcon.Text = "GOS Launcher";
            trayIcon.Visible = true;
        }
        static private void OuvrirGOSLauncher(object sender, EventArgs e) 
        {
            trayIcon.BalloonTipTitle = "Rétablir GOS Launcher";
            trayIcon.BalloonTipText = "Vous avez restauré l'interface du GOS Launcher.";
            trayIcon.ShowBalloonTip(500);
            GOSLauncherCore.fenetrePrincipale.WindowState = FormWindowState.Normal;
            GOSLauncherCore.fenetrePrincipale.Activate();
            trayIcon.Icon.Dispose();
        }
    }
}
