using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
    public class RichTextBoxEx : RichTextBox//,IMessageFilter
    {
        public int X;
        public int Y;
        int updating;
        int oldEventMask;
        //public static CPU.IULA ULA;
        #region Declarations
        const int EM_LINESCROLL = 0x00B6;
        [DllImport("user32.dll")]
        static extern int SetScrollPos(IntPtr hWnd, int nBar,
                                       int nPos, bool bRedraw);
        [DllImport("user32.dll")]
        static extern int SendMessage(IntPtr hWnd, int wMsg,
                                       int wParam, int lParam);
        //const int LF_FACESIZE = 32;
        //const int WM_USER = 0x400;
        //const int EM_SETCHARFORMAT = (WM_USER + 68);
        //const int CFM_BACKCOLOR = 0x4000000;
        //const int SCF_SELECTION = 1;

        //const int WM_SETREDRAW = 0x000B;
        //const int EM_GETEVENTMASK = (WM_USER + 59);
        //const int EM_SETEVENTMASK = (WM_USER + 69);

        //const int EM_GETLINE = 0xC4;
        //const int EM_LINELENGTH = 0xC1;

        //const int EM_LINEFROMCHAR = 0xC9;
        //const int EM_LINEINDEX = 0xBB;
        //const int EM_GETLINECOUNT = 0xBA;
        //const int EM_LINESCROLL = 0xB6;

        #endregion Declarations

        public RichTextBoxEx()
        {
            //HideSelection = false;
            Font = new Font("Courier New", 9f);
            WordWrap = false;
            //Application.AddMessageFilter(this);
        }
        public void BeginUpdate()
        {
            if (IsDisposed) return;
            ++updating;
            if (updating > 1) return;

            oldEventMask = NativeMethods.SendMessage(Handle, NativeMethods.EM_SETEVENTMASK, 0, 0);
            NativeMethods.SendMessage(Handle, NativeMethods.WM_SETREDRAW, 0, 0);
        }

        public void EndUpdate()
        {
            if (IsDisposed) return;
            --updating;
            if (updating > 0) return;

            NativeMethods.SendMessage(Handle, NativeMethods.WM_SETREDRAW, 1, 0);
            NativeMethods.SendMessage(Handle, NativeMethods.EM_SETEVENTMASK, 0, oldEventMask);
        }

        NativeMethods.CHARRANGE cr = new NativeMethods.CHARRANGE();
        public void SelectLine(int lineStart, int lineLength)
        {
            SelectTextX(lineStart, lineLength);
        }
        public void SelectTextX(int start, int length)
        {
            cr.cpMin = start;
            cr.cpMax = start + length;


            try
            {
            oldEventMask = NativeMethods.SendMessage(Handle, NativeMethods.EM_SETEVENTMASK, 0, 0);
            NativeMethods.SendMessage(Handle, NativeMethods.WM_SETREDRAW, 0, 0);
            NativeMethods.SendMessage(Handle, NativeMethods.EM_EXSETSEL, 0, ref cr);
            ScrollToCaret();
            NativeMethods.SendMessage(Handle, NativeMethods.WM_SETREDRAW, 1, 0);
            NativeMethods.SendMessage(Handle, NativeMethods.EM_SETEVENTMASK, 0, oldEventMask);
        }
        catch { }

        }
        public void Highlight(int address)
        {
            //CPU.SourceLine line = ULA.SourceLine[address];
            //if (previousline.Length != 0) RestoreLine(previousline.Start, previousline.Length, previousline.Breakpoint);

            //SelectLine(line.Start, line.Length, line.Breakpoint);
            //previousline = line;
            Application.DoEvents();
        }

        public void SelectLine(int start, int length, bool breakpoint)
        {
            SelectTextX(start, length);
            if (breakpoint)
            {
                SelectionBackColor = Color.Yellow;
                SelectionColor = Color.Red;
            }
            else
            {
                SelectionBackColor = ForeColor;
                SelectionColor = BackColor;
            }
            SelectionLength = 0;
        }

        protected void RestoreLine(int start, int length, bool breakpoint)
        {
            SelectTextX(start, length);
            if (breakpoint)
            {
                SelectionBackColor = Color.Red;
                SelectionColor = Color.Yellow;
            }
            else
            {
                SelectionBackColor = BackColor;
                SelectionColor = ForeColor;
            }
            SelectionLength = 0;
        }
        public Color SelectedBackColor
        {
            get
            {
                NativeMethods.CHARFORMAT2 fmt = new NativeMethods.CHARFORMAT2();
                fmt.cbSize = Marshal.SizeOf(fmt);

                NativeMethods.SendMessage(Handle, NativeMethods.EM_GETCHARFORMAT, NativeMethods.SCF_SELECTION, ref fmt);

                if ((fmt.dwMask & NativeMethods.CFM_BACKCOLOR) == 0)
                    return Color.Empty;

                int backCol = fmt.crBackColor;
                Color ret = ColorTranslator.FromWin32(backCol);

                return ret;
            }

            set
            {
                NativeMethods.CHARFORMAT2 fmt = new NativeMethods.CHARFORMAT2();
                fmt.cbSize = Marshal.SizeOf(fmt);
                fmt.dwMask = NativeMethods.CFM_BACKCOLOR;

                fmt.crBackColor = ColorTranslator.ToWin32(value);

                NativeMethods.SendMessage(Handle, NativeMethods.EM_SETCHARFORMAT, NativeMethods.SCF_SELECTION, ref fmt);
            }
        }

        public Color SelectedColor
        {
            get
            {
                NativeMethods.CHARFORMAT2 fmt = new NativeMethods.CHARFORMAT2();
                fmt.cbSize = Marshal.SizeOf(fmt);

                NativeMethods.SendMessage(Handle, NativeMethods.EM_GETCHARFORMAT, NativeMethods.SCF_SELECTION, ref fmt);

                if ((fmt.dwMask & NativeMethods.CFM_COLOR) == 0)
                    return Color.Empty;

                int backCol = fmt.crTextColor;
                Color ret = ColorTranslator.FromWin32(backCol);

                return ret;
            }

            set
            {
                NativeMethods.CHARFORMAT2 fmt = new NativeMethods.CHARFORMAT2();
                fmt.cbSize = Marshal.SizeOf(fmt);
                fmt.dwMask = NativeMethods.CFM_COLOR;

                fmt.crTextColor = ColorTranslator.ToWin32(value);

                NativeMethods.SendMessage(Handle, NativeMethods.EM_SETCHARFORMAT, NativeMethods.SCF_SELECTION, ref fmt);
            }
        }

        public int FirstVisibleLine()
        {
            return NativeMethods.SendMessage(this.Handle, NativeMethods.EM_GETFIRSTVISIBLELINE, 0, 0);
        }

        public int LastVisibleLine()
        {
            return LineNumber(ClientRectangle.Width, ClientRectangle.Height);
        }

        public int CharAtPoint(int x, int y)
        {
            NativeMethods.POINT p = new NativeMethods.POINT(x, y);
            return NativeMethods.SendMessage(this.Handle, NativeMethods.EM_CHARFROMPOS, 0, p);
        }
        public int LineNumber(int x, int y)
        {
            return NativeMethods.SendMessage(this.Handle, NativeMethods.EM_EXLINEFROMCHAR, 0, CharAtPoint(x, y));
        }
        public int LineFirstChar(int x, int y)
        {
            return NativeMethods.SendMessage(this.Handle, NativeMethods.EM_LINEINDEX, LineNumber(x, y), 0);
        }
        public int LineFirstChar(int line)
        {
            return NativeMethods.SendMessage(this.Handle, NativeMethods.EM_LINEINDEX, line, 0);
        }
        public int LineLength(int x, int y)
        {
            return NativeMethods.SendMessage(this.Handle, NativeMethods.EM_LINELENGTH, CharAtPoint(x, y), 0);
        }

        public override string Text
        {
            get
            {
                return base.Text;
            }
            set
            {
                BeginUpdate();
                base.Text = value;
                EndUpdate();
            }
        }
        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
        }

        public int clicks;
        //protected override void OnMouseDown(MouseEventArgs e)
        //{
        //    clicks = e.Clicks;
        //    this.X = e.X;
        //    this.Y = e.Y;

        //    base.OnMouseDown(e);
        //}

        //protected override void WndProc(ref Message m)
        //{
        //    base.WndProc(ref m);
        //}
        protected override void OnMouseMove(MouseEventArgs e)
        {

            this.X = e.X;
            this.Y = e.Y;
            try
            {
                //index = GetCharIndexFromPosition(new Point(e.X, e.Y));
                //line = GetLineFromCharIndex(index);
                //charIndex = GetFirstCharIndexFromLine(line);
                //CurrentAddress = Functions.BaseToDecimal(Text.Substring(charIndex, 4), Functions.BaseType.Hex);
                //CurrentLine = ULA.SourceLine[CurrentAddress];

            }
            catch { }

            base.OnMouseMove(e);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            Parent.Focus();
            //base.OnMouseDown(e);
        }
        public int CurrentAddress;
        int index;
        int line;
        int charIndex;
        protected override void OnDoubleClick(EventArgs e)
        {
            try
            {
                //ULA.SourceLine[CurrentAddress].Breakpoint = !ULA.SourceLine[CurrentAddress].Breakpoint;
                //CurrentLine = ULA.SourceLine[CurrentAddress];
                //RestoreLine(CurrentLine.Start, CurrentLine.Length, CurrentLine.Breakpoint);
            }
            catch { }
            Parent.Focus();
            base.OnDoubleClick(e);
        }


        //#region IMessageFilter Members

        //public bool PreFilterMessage(ref Message m)
        //{
        //    if (m.Msg == NativeMethods.WM_NCRBUTTONUP || m.Msg == NativeMethods.WM_NCRBUTTONDOWN || m.Msg == NativeMethods.WM_NCXBUTTONDBLCLK) return true;
        //    if (m.Msg == NativeMethods.WM_RBUTTONDOWN || m.Msg == NativeMethods.WM_RBUTTONUP || m.Msg == NativeMethods.WM_RBUTTONDBLCLK) return true;
        //    if (m.Msg == NativeMethods.WM_LBUTTONDBLCLK) return true;
        //    return false;
        //}

        //#endregion
    }

