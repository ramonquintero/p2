using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace P2
{
    class ComboBoxWrap : ComboBox
    {
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetWindowRect(IntPtr hwnd, out RECT lpRect);

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int x, int y, int cx, int cy, uint uFlags);

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left;        // x position of upper-left corner
            public int Top;         // y position of upper-left corner
            public int Right;       // x position of lower-right corner
            public int Bottom;      // y position of lower-right corner
        }

        public const int SWP_NOZORDER = 0x0004;
        public const int SWP_NOACTIVATE = 0x0010;
        public const int SWP_FRAMECHANGED = 0x0020;
        public const int SWP_NOOWNERZORDER = 0x0200;

        public const int WM_CTLCOLORLISTBOX = 0x0134;

        private int _hwndDropDown = 0;

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WM_CTLCOLORLISTBOX)
            {
                if (_hwndDropDown == 0)
                {
                    _hwndDropDown = m.LParam.ToInt32();

                    RECT r;
                    GetWindowRect((IntPtr)_hwndDropDown, out r);

                    //int newHeight = 0;
                    // for(int i=0; i<Items.Count && i < MaxDropDownItems; i++)
                    //    newHeight += this.GetItemHeight(i);

                    int total = 0;
                    for (int i = 0; i < this.Items.Count; i++)
                        total += this.GetItemHeight(i);
                    this.DropDownHeight = total + SystemInformation.BorderSize.Height * (this.Items.Count + 2);


                    SetWindowPos((IntPtr)_hwndDropDown, IntPtr.Zero,
                        r.Left,
                                 r.Top,
                                 DropDownWidth,
                                 DropDownHeight,
                                 SWP_FRAMECHANGED |
                                     SWP_NOACTIVATE |
                                     SWP_NOZORDER |
                                     SWP_NOOWNERZORDER);
                }
            }

            base.WndProc(ref m);
        }

        protected override void OnDropDownClosed(EventArgs e)
        {
            _hwndDropDown = 0;
            base.OnDropDownClosed(e);
        }

        public ComboBoxWrap()
            : base()
        {
            // add event handlers
            this.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.DrawItem += new DrawItemEventHandler(ComboBoxWrap_DrawItem);
            this.MeasureItem += new MeasureItemEventHandler(ComboBoxWrap_MeasureItem);
        }

        void ComboBoxWrap_MeasureItem(object sender, MeasureItemEventArgs e)
        {
            // set the height of the item, using MeasureString with the font and control width
            ComboBoxWrap ddl = (ComboBoxWrap)sender;
            string text = ddl.Items[e.Index].ToString();
            SizeF size = e.Graphics.MeasureString(text, this.Font, ddl.DropDownWidth);
            e.ItemHeight = (int)Math.Ceiling(size.Height) + 1;  // plus one for the border
            e.ItemWidth = ddl.DropDownWidth;
            System.Diagnostics.Trace.WriteLine(String.Format("Height {0}, Text {1}", e.ItemHeight, text));
        }

        void ComboBoxWrap_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0)
                return;

            // draw a lighter blue selected BG colour, the dark blue default has poor contrast with black text on a dark blue background
            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
                e.Graphics.FillRectangle(Brushes.PowderBlue, e.Bounds);
            else
                e.Graphics.FillRectangle(Brushes.White, e.Bounds);

            // get the text of the item
            ComboBoxWrap ddl = (ComboBoxWrap)sender;
            string text = ddl.Items[e.Index].ToString();

            // don't dispose the brush afterwards
            Brush b = Brushes.Black;
            e.Graphics.DrawString(text, this.Font, b, e.Bounds, StringFormat.GenericDefault);

            // draw a light grey border line to separate the items
            Pen p = new Pen(Brushes.Gainsboro, 1);
            e.Graphics.DrawLine(p, new Point(e.Bounds.Left, e.Bounds.Bottom - 1), new Point(e.Bounds.Right, e.Bounds.Bottom - 1));
            p.Dispose();

            e.DrawFocusRectangle();
        }
    }
}
