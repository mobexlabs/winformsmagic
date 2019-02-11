/*=====================================================================
  File:     Form1.cs

  Summary:  The code shows how to draw someting on non-client area 
			of the form. It is required to turn theming off. In the 
			other case all our changes will be invisible due to DWM
			overlayed painting.
  
  IMPORTANT To make project as simple as possible, default windows 
			buttons and borders are NOT paintd which causes some 
			errors. 

  Classes:	Form1
			
  Origin:   MobEx Labs WinFormsNcPaint tutorial project
  
  Author:	Sergey Vdovenko
			sv@mobexlabs.com
  
  Ref:		1. System.Windows.Forms
			
			2. GetWindowDC
			https://docs.microsoft.com/en-us/windows/desktop/api/winuser/nf-winuser-getwindowdc
			
			3. GetDCEx
			https://docs.microsoft.com/en-us/windows/desktop/api/winuser/nf-winuser-getdcex
			
			4. ReleaseDC
			https://docs.microsoft.com/en-us/windows/desktop/api/winuser/nf-winuser-releasedc

			5. DeleteObject
			https://docs.microsoft.com/en-us/windows/desktop/api/wingdi/nf-wingdi-deleteobject

			6. The evil WM_NCPAINT
			https://social.msdn.microsoft.com/Forums/vstudio/en-US/a407591a-4b1e-4adc-ab0b-3c8b3aec3153/the-evil-wmncpaint?forum=windowsuidevelopment

=====================================================================*/


using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace OwnerDrawnTitleBar
{
    public partial class Form1 : Form
    {
        protected bool IsThemeEnabled = true;

        public Form1()
        {
            SetStyle(ControlStyles.ResizeRedraw, true);
            InitializeComponent();
            EnableTheme(false);
        }
        protected override void WndProc(ref Message m)
        {
            const int WM_ACTIVATE = 0x0006;
            const int WM_NCPAINT = 0x0085;
			const int WM_NCACTIVATE = 0x0086;
			const int WM_SIZE = 0x0005;

			if (DesignMode || IsDisposed)
            {
                base.WndProc(ref m);
                return;
            }
            switch (m.Msg)
            {
                case WM_NCACTIVATE:
				case WM_ACTIVATE:
					base.WndProc(ref m);
                    WmNcActivate(ref m);
                    break;
                case WM_NCPAINT:
                    base.WndProc(ref m);
                    WmNcPaint(ref m);
					break;
				case WM_SIZE:
					WmSize(ref m);
					break;
				default:
                    base.WndProc(ref m);
                    break;
            }
        }

        protected virtual void WmNcActivate(ref Message msg)
        {
            IntPtr hWnd = msg.HWnd;
            IntPtr hDC = ObtainDc(hWnd, IntPtr.Zero);
            
            if (hDC == IntPtr.Zero)
            {
                return;
            }

            using (Graphics graphics = Graphics.FromHdc(hDC))
            {
				PaintCaption(graphics, ((int)msg.WParam == 1)); //Take in account window state Active/inactive
            }

            ReleaseDC(hWnd, hDC);
            msg.Result = (IntPtr)0;
        }

		protected virtual void WmSize(ref Message msg)
		{
			IntPtr hWnd = msg.HWnd;
			IntPtr hDC = ObtainDc(hWnd, IntPtr.Zero);

			if (hDC == IntPtr.Zero)
			{
				return;
			}

			using (Graphics graphics = Graphics.FromHdc(hDC)) //Just repaint caption as active
			{
				PaintCaption(graphics);
			}

			ReleaseDC(hWnd, hDC);
			msg.Result = (IntPtr)0;
		}

		protected virtual void WmNcPaint(ref Message msg)
        {
            IntPtr hWnd = msg.HWnd;
            IntPtr hRgn = msg.WParam;
            IntPtr hDC = ObtainDc(hWnd, hRgn);

            if (hDC == IntPtr.Zero)
            {
                return;
            }

            using (Graphics graphics = Graphics.FromHdc(hDC))
            {
                PaintCaption(graphics);
            }

            ReleaseDC(hWnd, hDC);
            msg.Result = (IntPtr)0;
        }

        IntPtr ObtainDc(IntPtr hWnd, IntPtr hRgn)
        {
			//
			// NOTES: We are going to draw our caption carefully.
			// So the function should return simply GetWindowDC(hWnd);
			// The code below is provided with the reference purposes only.
			//
		
			const int DCX_CACHE = 0x2;
            const int DCX_CLIPSIBLINGS = 0x10;
            const int DCX_INTERSECTRGN = 0x80;
            const int DCX_WINDOW = 0x1;

            IntPtr hDC = IntPtr.Zero;

            if (hRgn != IntPtr.Zero)
                hDC = GetDCEx(hWnd, hRgn, DCX_WINDOW | DCX_INTERSECTRGN | DCX_CACHE | DCX_CLIPSIBLINGS);

            if (hDC == IntPtr.Zero)
            {
                hDC = GetWindowDC(hWnd);
            }
            return hDC;
        }

        protected virtual void PaintCaption(Graphics g, bool active = true)
        {
            float scale = this.AutoScaleFactor.Height;
            
            //Calculate small icon bounds 
            Rectangle iconBounds = new Rectangle((int)(2 + 5 * scale + 1 * scale+5*scale), (int)(2 + 5 * scale + 1 * scale+4*scale), (int)(14*scale), (int)(14 * scale));

            //Calculate caption rectangle to be filled with gradient
            int left = (int)(2 + 5 * scale + 2 + 5 * scale + 14 * scale + 4 * scale);
            int right = this.Bounds.Width-(int)(2 + 5 * scale + 1 * scale + 2 * scale + 20 * 3 * scale + 2);
            Rectangle bounds = new Rectangle(left, (int)(2+5* scale + 1*scale), right-left, (int)(22*scale));
			
			//Fill caption with gradient
			Color rightColor = Color.Blue;
			Color leftColor = Color.Red;

			//Ajust color for inactive border
			if (!active)
			{
				rightColor = Color.Gray;
			}

			using (LinearGradientBrush linGrBrush = new LinearGradientBrush(new Point(bounds.Left-1, 0), new Point(bounds.Left+bounds.Width, 0), leftColor, rightColor))
            {
                g.FillRectangle(linGrBrush, bounds);
            }

            //Draw a red rectangle around mall icon
            Color cc = Color.Red;
            using (Pen p = new Pen(cc, 1))
            {
                g.DrawRectangle(p, iconBounds);
            }

            //
            //Draw form caption text
            //

            StringFormat sf = new StringFormat();
            sf.FormatFlags = StringFormatFlags.LineLimit;
            sf.Alignment = StringAlignment.Near;
            sf.LineAlignment = StringAlignment.Center;
            sf.Trimming = StringTrimming.EllipsisCharacter;

            g.DrawString(this.Text, this.Font, Brushes.White, bounds, sf);
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
                SendMessage(this.Handle, WM_THEMECHANGED, IntPtr.Zero, IntPtr.Zero);

                this.Refresh();
            }
        }

        #region Win32 imports


        [DllImport("user32.dll")]
        public static extern IntPtr GetWindowDC(IntPtr hWnd);
        [DllImport("user32.dll")]
        public static extern IntPtr GetDCEx(IntPtr hwnd, IntPtr hrgnclip, uint fdwOptions);

        [DllImport("user32.dll", ExactSpelling = true)]
        public static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);

        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("gdi32.dll", EntryPoint = "DeleteObject")]
        public static extern bool DeleteObject([In] IntPtr hObject);


        [DllImport("uxtheme.dll", EntryPoint = "SetThemeAppProperties")]
        public static extern void SetThemeAppProperties(uint dwFlags);

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hwnd, int wMsg, IntPtr wParam, IntPtr lParam);

        #endregion
    }
}
