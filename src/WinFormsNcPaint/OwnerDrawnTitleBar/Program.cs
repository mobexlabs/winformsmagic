/*=====================================================================
  File:     Program.cs

  Summary:  A standard WinForms Program.cs

  Classes:	Program
			
  Origin:   MobEx Labs WinFormsNcPaint tutorial project
  
  Author:	Visual Studio 2017
  
  Ref:		1. System.Windows.Forms

=====================================================================*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OwnerDrawnTitleBar
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //if (Environment.OSVersion.Version.Major >= 6)
            //    SetProcessDPIAware();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern bool SetProcessDPIAware();
    }
}
