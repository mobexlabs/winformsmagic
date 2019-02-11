/*=============================================================================
  File:     Form1.cs

  Summary:  The application enables/disables theming of the 
            whole application. Child windows/controls are also
            re-drawn with no theme.

			The application also shows some AutoScale data useful for 
			furthure development.

  Classes:	Form1
			
  Origin:   MobEx Labs WinFormsNcPaint tutorial project
  
  Author:	Sergey Vdovenko
			sv@mobexlabs.com
  
  Ref:		1. System.Windows.Forms

			2. SetThemeAppProperties function
            https://docs.microsoft.com/en-us/windows/desktop/api/uxtheme/nf-uxtheme-setthemeappproperties
  
            3. SendMessage function
            https://docs.microsoft.com/en-us/windows/desktop/api/winuser/nf-winuser-sendmessage
  
			4. How to write WinForms code that auto-scales to system font and dpi settings?
			https://stackoverflow.com/questions/22735174/how-to-write-winforms-code-that-auto-scales-to-system-font-and-dpi-settings

			5. More info about DPI Awareness
			https://blogs.windows.com/buildingapps/2017/04/04/high-dpi-scaling-improvements-desktop-applications-windows-10-creators-update/
			
			6. Improving the high-DPI experience in GDI based Desktop Apps
			https://blogs.windows.com/buildingapps/2017/05/19/improving-high-dpi-experience-gdi-based-desktop-apps/#Uwv9gY1SvpbgQ4dK.97
==============================================================================*/

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
		SizeF scaleFactor = new SizeF(1, 1);

		public Form1()
        {
            InitializeComponent();
        }

		private void Form1_Load(object sender, EventArgs e)
		{
			UpdateLabels();
		}

		private void Form1_SizeChanged(object sender, EventArgs e)
		{
			UpdateLabels();
		}

		private void Form1_LocationChanged(object sender, EventArgs e)
		{
			UpdateLabels();
		}

		protected override void ScaleControl(System.Drawing.SizeF factor, System.Windows.Forms.BoundsSpecified specified)
		{
			scaleFactor = factor;
			base.ScaleControl(factor, specified);
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

		protected void UpdateLabels()
		{
			label4.Text = $"{AutoScaleFactor.Width}x{AutoScaleFactor.Height}";
			label5.Text = $"{this.Bounds.Left},{this.Bounds.Top},{this.Bounds.Right},{this.Bounds.Bottom} (Width={this.Bounds.Width},Height={this.Bounds.Height})";
			label6.Text = $"{this.ClientRectangle.Left},{this.ClientRectangle.Top},{this.ClientRectangle.Right},{this.ClientRectangle.Bottom} (Width={this.ClientRectangle.Width},Height={this.ClientRectangle.Height})";
			label7.Text = $"{this.CurrentAutoScaleDimensions.Width},{this.CurrentAutoScaleDimensions.Height}";
			label10.Text = $"{this.AutoScaleDimensions.Width},{this.AutoScaleDimensions.Height}";
			label12.Text = $"{this.scaleFactor.Width}x{this.scaleFactor.Height}";
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
