#region À la vie, à la mort

// ###########################################
// 
// FILE:                      Program.cs 
// PROJECT:              Arbiter
// SOLUTION:           PolarOrbit
// 
// CREATED BY RAISTLIN TAO
// DEMONVK@GMAIL.COM
// 
// FILE CREATION: 5:44 PM  05/12/2015
// 
// ###########################################

#endregion

#region Embrace the code

using System;
using System.Windows.Forms;
using Arbiter.UI;

#endregion

namespace Arbiter
{
    internal static class Program
    {
        /// <summary>
        ///     The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            var newConfigFilePath =
                Application.StartupPath + "\\ArbiterCore.prx";

            if (AppDomain.CurrentDomain.SetupInformation.ConfigurationFile != newConfigFilePath)
                AppDomain.CurrentDomain.SetData("APP_CONFIG_FILE", newConfigFilePath);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmMain());
        }
    }
}