using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Runtime.InteropServices;
using System.Drawing;
using System.Drawing.Drawing2D;


namespace OwnerDrawnWindow
{
	public partial class Form1 : Form
	{
		protected bool IsThemeEnabled = true;
		SizeF scaleFactor = new SizeF(1, 1);

		private readonly Cursor[] _resizeCursors = { Cursors.SizeNESW, Cursors.SizeWE, Cursors.SizeNWSE, Cursors.SizeWE, Cursors.SizeNS };

		Rectangle topBorderRect;
		Rectangle bottomBorderRect;
		Rectangle leftBorderRect;
		Rectangle rightBorderRect;

		Rectangle topLeftBorderRect;
		Rectangle topRightBorderRect;
		Rectangle bottomLeftBorderRect;
		Rectangle bottomRightBorderRect;

		Rectangle captionRectangle;
		Rectangle titleRectangle;
		Rectangle clientAreaRectangle;

		public int formBorderWidth = 8;

		public int thinBorderWidth = 2;
		public int systemButtonHeight = 20;
		public int systemButtonWidth = 20;

		public int captionHeight = 22;
		public int titleHeight = 60; //Can be bigger then caption
		public int borderWidth = 5;

		public Form1()
		{
			DoubleBuffered = true;
			SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
			SetStyle(ControlStyles.SupportsTransparentBackColor, true);
			SetStyle(ControlStyles.ResizeRedraw, true);

			InitializeComponent();

			EnableTheme(false);
		}

		#region Scaling and Resizing
		protected override void OnResize(EventArgs eventargs)
		{
			base.OnResize(eventargs);
			CalculateButtonRectangles();
			Refresh();
		}

		protected override void ScaleControl(System.Drawing.SizeF factor, System.Windows.Forms.BoundsSpecified specified)
		{
			scaleFactor = factor;
			base.ScaleControl(factor, specified);
			CalculateButtonRectangles();
		}

		private void CalculateButtonRectangles()
		{
			int ajustedBorderWidth = (int)(formBorderWidth * scaleFactor.Width);
			int ajustedBorderHeight = (int)(formBorderWidth * scaleFactor.Height);

			int ajustedTitleHeight = (int)(int)(titleHeight * scaleFactor.Width);
			int ajustedCaptionHeight = (int)(captionHeight * scaleFactor.Width);

			int ajustedSystemButtonHeight = (int)(systemButtonHeight * scaleFactor.Width);
			int ajustedSystemButtonWidth = (int)(systemButtonWidth * scaleFactor.Width);
			
			topBorderRect = new Rectangle(0, 0, this.Width, ajustedBorderHeight);
			bottomBorderRect = new Rectangle(0, this.Height - ajustedBorderHeight, this.Width, ajustedBorderWidth);
			leftBorderRect = new Rectangle(0, 0, ajustedBorderWidth, this.Height);
			rightBorderRect = new Rectangle(this.Width - ajustedBorderWidth, 0, this.Width - ajustedBorderWidth, this.Height);

			topLeftBorderRect = topBorderRect; topLeftBorderRect.Intersect(leftBorderRect);
			topRightBorderRect = topBorderRect; topRightBorderRect.Intersect(rightBorderRect);
			bottomLeftBorderRect = bottomBorderRect; bottomLeftBorderRect.Intersect(leftBorderRect);
			bottomRightBorderRect = bottomBorderRect; bottomRightBorderRect.Intersect(rightBorderRect);

			captionRectangle = new Rectangle(ajustedBorderWidth, ajustedBorderWidth, this.Width - ajustedBorderWidth * 2, ajustedCaptionHeight);
			titleRectangle = new Rectangle(ajustedBorderWidth, ajustedBorderWidth, this.Width - ajustedBorderWidth * 2, ajustedTitleHeight);
			clientAreaRectangle = new Rectangle(ajustedBorderWidth, ajustedBorderWidth , this.Width - 2 * ajustedBorderWidth, this.Height - ajustedBorderWidth * 2);
		}

		#endregion

		#region WindProc

		protected override void WndProc(ref Message m)
		{
			const int WM_ACTIVATE = 0x0006;
			const int WM_NCACTIVATE = 0x0086;
			const int WM_NCPAINT = 0x0085;
			const int WM_NCCALCSIZE = 0x0083;
			const int WM_NCLBUTTONDOWN = 0xA1;
			const int WM_NCHITTEST = 0x84;
			const int WM_SYSCOMMAND = 0x0112;
			const int WM_NCMOUSEMOVE = 0x00A0;

			if (DesignMode || IsDisposed)
			{
				base.WndProc(ref m);
				return;
			}
			switch (m.Msg)
			{
				case WM_ACTIVATE:
					base.WndProc(ref m);
					WmNcActivate(ref m);
					break;

				case WM_NCCALCSIZE:
					WmNCCalcSize(ref m);
					break;

				case WM_NCHITTEST:
					WmNcHitTest(ref m);
					break;

				case WM_NCPAINT:
					WmNcPaint(ref m);
					break;

				case WM_NCLBUTTONDOWN:
					WmNcLeftMouseButtonDown(ref m);
					break;

				case WM_NCMOUSEMOVE:
					WmNcMouseMove(ref m);
					break;
				default:
					base.WndProc(ref m);
					break;
			}
		}

		protected virtual void WmNcActivate(ref Message msg)
		{
			IntPtr hWnd = msg.HWnd;
			IntPtr hDC = GetWindowDC(hWnd);

			if (hDC == IntPtr.Zero)
			{
				return;
			}

			int ajustedFormBorderWidth = (int)(formBorderWidth * scaleFactor.Width);

			var windowRect = new RECT();

			if (GetWindowRect(this.Handle, ref windowRect) != 0)
			{
				Rectangle bounds = new Rectangle(0, 0, windowRect.right - windowRect.left, windowRect.bottom - windowRect.top);

				using (Graphics graphics = Graphics.FromHdc(hDC))
				{
					graphics.ExcludeClip(clientAreaRectangle);
					PaintFormBg(graphics, bounds);
				}
				ReleaseDC(hWnd, hDC);
				msg.Result = (IntPtr)0;
			}
		}

		private void WmNCCalcSize(ref Message m)
		{
			// http://msdn.microsoft.com/library/default.asp?url=/library/en-us/winui/winui/windowsuserinterface/windowing/windows/windowreference/windowmessages/wm_nccalcsize.asp
			// http://groups.google.pl/groups?selm=OnRNaGfDEHA.1600%40tk2msftngp13.phx.gbl

			int ajustedFormBorderWidth = (int)(formBorderWidth * scaleFactor.Width);

			if (m.WParam == IntPtr.Zero)
			{
				var ncRect = (RECT)m.GetLParam(typeof(RECT));
				ncRect.left += ajustedFormBorderWidth;
				ncRect.top += ajustedFormBorderWidth;
				ncRect.right -= ajustedFormBorderWidth;
				ncRect.bottom -= ajustedFormBorderWidth;
				Marshal.StructureToPtr(ncRect, m.LParam, false);
			}
			else if (m.WParam == (IntPtr)1)
			{
				var ncParams = (NCCALCSIZE_PARAMS)m.GetLParam(typeof(NCCALCSIZE_PARAMS));
				//var proposed = ncParams.rectProposed.ToRectangle();
				//CalculateNonClientAreaSize(ref proposed);
				//ncParams.rectProposed = new RECT(proposed);
				ncParams.rectProposed.left += ajustedFormBorderWidth;
				ncParams.rectProposed.top += ajustedFormBorderWidth;
				ncParams.rectProposed.right -= ajustedFormBorderWidth;
				ncParams.rectProposed.bottom -= ajustedFormBorderWidth;
				Marshal.StructureToPtr(ncParams, m.LParam, false);

			}
			m.Result = IntPtr.Zero;
		}

		protected virtual void WmNcHitTest(ref Message m)
		{
			//https://docs.microsoft.com/en-us/windows/desktop/inputdev/about-mouse-input#nonclient-area-mouse-messages
			var screenPoint = new Point(m.LParam.ToInt32());
			var location = new Point(screenPoint.X-this.Location.X, screenPoint.Y - this.Location.Y);
			m.Result = (IntPtr)CalculateHitTest(location);
		}

		private NonClientHitTest CalculateHitTest(Point location)
		{
			Debug.WriteLine($"Client point: {location.X},{location.Y}");
			if (topLeftBorderRect.Contains(location))
			{
				return NonClientHitTest.TopLeft;
			}

			if (topRightBorderRect.Contains(location))
			{
				return NonClientHitTest.TopRight;
			}

			if (bottomLeftBorderRect.Contains(location))
			{
				return NonClientHitTest.BottomLeft;
			}

			if (bottomRightBorderRect.Contains(location))
			{
				return NonClientHitTest.BottomRight;
			}

			if (leftBorderRect.Contains(location))
			{
				return NonClientHitTest.Left;
			}

			if (topBorderRect.Contains(location))
			{
				return NonClientHitTest.Top;
			}

			if (rightBorderRect.Contains(location))
			{
				return NonClientHitTest.Right;
			}

			if (bottomBorderRect.Contains(location))
			{
				return NonClientHitTest.Bottom;
			}
			return NonClientHitTest.Caption;
		}

		private void SetCursor(NonClientHitTest ht)
		{
			if (ht != NonClientHitTest.Client)
			{
				Debug.WriteLine(ht);
			}

			switch (ht)
			{
				case NonClientHitTest.TopLeft:
					Cursor = Cursors.SizeNWSE;
					break;
				case NonClientHitTest.TopRight:
					Cursor = Cursors.SizeNESW;
					break;
				case NonClientHitTest.BottomLeft:
					Cursor = Cursors.SizeNESW;
					break;
				case NonClientHitTest.BottomRight:
					Cursor = Cursors.SizeNWSE;
					break;
				case NonClientHitTest.Left:
					Cursor = Cursors.SizeWE;
					break;
				case NonClientHitTest.Top:
					Cursor = Cursors.SizeNS;
					break;
				case NonClientHitTest.Right:
					Cursor = Cursors.SizeWE;
					break;
				case NonClientHitTest.Bottom:
					Cursor = Cursors.SizeNS;
					break;
				default:
					Cursor = Cursors.Default;
					break;
			}
		}

		protected virtual void WmNcPaint(ref Message msg)
		{
			const int DCX_CACHE = 0x2;
			const int DCX_CLIPSIBLINGS = 0x10;
			const int DCX_INTERSECTRGN = 0x80;
			const int DCX_WINDOW = 0x1;

			msg.Result = (IntPtr)1;

			int ajustedFormBorderWidth = (int)(formBorderWidth * scaleFactor.Width);

			var windowRect = new RECT();

			if (GetWindowRect(this.Handle, ref windowRect) != 0)
			{
				IntPtr hWnd = msg.HWnd;
				IntPtr hRgn = msg.WParam;

				Rectangle bounds = new Rectangle(0, 0, windowRect.right - windowRect.left, windowRect.bottom - windowRect.top);

				if (bounds.Width != 0 && bounds.Height != 0)
				{
					// The update region is clipped to the window frame. When wParam
					// is 1, the entire window frame needs to be updated. 
					Region clipRegion = null;

					if (hRgn != (IntPtr)1)
					{
						clipRegion = Region.FromHrgn(hRgn);
					}

					// MSDN states that only WINDOW and INTERSECTRGN are needed,
					// but other sources confirm that CACHE is required on Win9x
					// and you need CLIPSIBLINGS to prevent painting on overlapping windows.

					var hDC = GetDCEx(hWnd, hRgn, DCX_WINDOW | DCX_INTERSECTRGN | DCX_CACHE | DCX_CLIPSIBLINGS);

					if (hDC == IntPtr.Zero)
					{
						hDC = GetWindowDC(hWnd);
					}

					if (hDC == IntPtr.Zero)
					{
						return;
					}

					using (Graphics graphics = Graphics.FromHdc(hDC))
					{
						graphics.ExcludeClip(clientAreaRectangle);
						PaintFormBg(graphics, bounds);
					}

					ReleaseDC(hWnd, hDC);
				}
			}
			//PaintFormBg(e.Graphics);

		}

		protected virtual void WmNcLeftMouseButtonDown(ref Message m)
		{
			byte bFlag = 0;
			const int WM_SYSCOMMAND = 0x0112;
			
			// Get which side to resize from
			if (_resizingLocationsToCmd.ContainsKey((int)m.WParam))
			{
				bFlag = (byte)_resizingLocationsToCmd[(int)m.WParam];
			}

			if (bFlag != 0)
			{
				var screenPoint = new Point(m.LParam.ToInt32());
				var location = new Point(screenPoint.X-this.Location.X, screenPoint.Y - this.Location.Y);
				SetCursor(CalculateHitTest(location));
				SendMessage(Handle, WM_SYSCOMMAND, (int)(0xF000 | bFlag), (int)m.LParam);
			}
			else
			{
				base.WndProc(ref m);
			}
		}

		protected virtual void WmNcMouseMove(ref Message m)
		{
			var screenPoint = new Point(m.LParam.ToInt32());
			var location = new Point(screenPoint.X - this.Location.X, screenPoint.Y - this.Location.Y);
			SetCursor(CalculateHitTest(location));
			base.WndProc(ref m);
		}

		#endregion

		#region Client area Mouse Events
		protected override void OnMouseMove(MouseEventArgs e)
		{
			base.OnMouseMove(e);

			if (DesignMode)
			{
				return;
			}

			var isChildUnderMouse = GetChildAtPoint(e.Location) != null;

			if (!isChildUnderMouse)
			{
				SetCursor(CalculateHitTest(e.Location));

				if ((e.Button == MouseButtons.None))
				{
					Debug.WriteLine("Mouse Movie with no buttons");
				}
				else
				{
					Debug.WriteLine("Mouse Movie with a button!");
				}
			}
		}
		#endregion

		#region Painting and Drawing
		protected virtual void PaintFormBg(Graphics graphics, Rectangle bounds)
		{
			graphics.FillRectangle(Brushes.DarkRed, bounds);
		}

		#endregion
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
				SendMessage(this.Handle, WM_THEMECHANGED, 0, 0);

				this.Refresh();
			}
		}

		#region Win32 imports

		enum NonClientHitTest
		{
			Error = -2,
			Transparent = -1,
			Nowhere = 0,
			Client = 1,
			Caption = 2,
			SystemMenu = 3,
			GrowBox = 4,
			Menu = 5,
			HorizontalScroll = 6,
			VerticalScroll = 7,
			MinButton = 8,
			MaxButton = 9,
			Left = 10,
			Right = 11,
			Top = 12,
			TopLeft = 13,
			TopRight = 14,
			Bottom = 15,
			BottomLeft = 16,
			BottomRight = 17,
			Border = 18,
			Object = 19,
			Close = 20,
			Help = 21
		}

		private const int HTBOTTOMLEFT = 16;
		private const int HTBOTTOMRIGHT = 17;
		private const int HTLEFT = 10;
		private const int HTRIGHT = 11;
		private const int HTBOTTOM = 15;
		private const int HTTOP = 12;
		private const int HTTOPLEFT = 13;
		private const int HTTOPRIGHT = 14;

		public const int HT_CAPTION = 0x2;

		private const int WMSZ_TOP = 3;
		private const int WMSZ_TOPLEFT = 4;
		private const int WMSZ_TOPRIGHT = 5;
		private const int WMSZ_LEFT = 1;
		private const int WMSZ_RIGHT = 2;
		private const int WMSZ_BOTTOM = 6;
		private const int WMSZ_BOTTOMLEFT = 7;
		private const int WMSZ_BOTTOMRIGHT = 8;

		private readonly Dictionary<int, int> _resizingLocationsToCmd = new Dictionary<int, int>
		{
			{HTTOP,         WMSZ_TOP},
			{HTTOPLEFT,     WMSZ_TOPLEFT},
			{HTTOPRIGHT,    WMSZ_TOPRIGHT},
			{HTLEFT,        WMSZ_LEFT},
			{HTRIGHT,       WMSZ_RIGHT},
			{HTBOTTOM,      WMSZ_BOTTOM},
			{HTBOTTOMLEFT,  WMSZ_BOTTOMLEFT},
			{HTBOTTOMRIGHT, WMSZ_BOTTOMRIGHT}
		};

		[StructLayout(LayoutKind.Sequential)]
		public struct POINT
		{
			/// LONG->int
			public int x;

			/// LONG->int
			public int y;
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct RECT
		{
			public int left;
			public int top;
			public int right;
			public int bottom;

			public RECT(Rectangle r)
			{
				left = r.Left;
				top = r.Top;
				right = r.Right;
				bottom = r.Bottom;
			}

			public Rectangle ToRectangle()
			{
				return new Rectangle(left, top, right - left, bottom - top);
			}
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct NCCALCSIZE_PARAMS
		{
			public RECT rectProposed;
			public RECT rectBeforeMove;
			public RECT rectClientBeforeMove;
			public WINDOWPOS lpPos;
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct WINDOWPOS
		{
			public IntPtr hwnd;
			public IntPtr hWndInsertAfter;
			public int x;
			public int y;
			public int cx;
			public int cy;
			public uint flags;
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct TPMPARAMS
		{
			/// UINT->unsigned int
			public uint cbSize;

			/// RECT->tagRECT
			public RECT rcExclude;
		}

		[DllImport("user32.dll")]
		public static extern int GetWindowRect(IntPtr hwnd, ref RECT lpRect);

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
		public static extern int SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);

		#endregion
	}
}
