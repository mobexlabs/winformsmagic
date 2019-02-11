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

namespace NoThemeApp
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
