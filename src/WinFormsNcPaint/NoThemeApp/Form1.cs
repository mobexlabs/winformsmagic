/*=====================================================================
  File:     Form1.cs

  Summary:  The application enables/disables theming of the 
            whole application. Child windows/controls are also
            re-drawn with no theme.

  Classes:	Form1
			
  Origin:   MobEx Labs WinFormsNcPaint tutorial project
  
  Author:	Sergey Vdovenko
			sv@mobexlabs.com
  
  Ref:		1. System.Windows.Forms

			2. SetThemeAppProperties function
            https://docs.microsoft.com/en-us/windows/desktop/api/uxtheme/nf-uxtheme-setthemeappproperties
  
            3. SendMessage function
            https://docs.microsoft.com/en-us/windows/desktop/api/winuser/nf-winuser-sendmessage
  
=====================================================================*/

using System;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NoThemeApp
{
    public partial class Form1 : Form
    {
        protected bool IsThemeEnabled = true;

        public Form1()
        {
            InitializeComponent();
        }

        private void btnDisableEnableTheme_Click(object sender, EventArgs e)
        {
            if (IsThemeEnabled)
            {
                EnableTheme(false);
                btnDisableEnableTheme.Text = "Enable Theme";
            }
            else
            {
                EnableTheme();
                btnDisableEnableTheme.Text = "Disable Theme";
            }
        }

        protected void EnableTheme(bool enabled = true)
        {

            if (Environment.OSVersion.Version.Major >= 6)
            {
                const int WM_THEMECHANGED = 0x031A;

                const uint STAP_ALLOW_NONCLIENT = 0x00001;
                const uint STAP_ALLOW_CONTROLS = 0x00002;

                if (enabled)
                {
                    SetThemeAppProperties(STAP_ALLOW_NONCLIENT | STAP_ALLOW_CONTROLS);
                }
                else
                {
                    SetThemeAppProperties(0);
                }

                IsThemeEnabled = enabled;

                //
                // Notify Windows about application theme update
                //

                SendMessage(this.Handle, WM_THEMECHANGED, IntPtr.Zero, IntPtr.Zero);

                this.Refresh();
            }
        }

        #region Win32 imports

        [DllImport("uxtheme.dll", EntryPoint = "SetThemeAppProperties")]
        public static extern void SetThemeAppProperties(uint dwFlags);

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hwnd, int wMsg, IntPtr wParam, IntPtr lParam);

        #endregion
    }
}
