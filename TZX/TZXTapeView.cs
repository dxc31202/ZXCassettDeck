using System;
using System.Collections.Generic;

using System.Text;

using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
namespace ZXCassetteDeck
{
    public class TapeView : PictureBox
    {
        TZXFile tZXFile;
        public TZXFile TZXFile
        {
            get { return tZXFile; }
            set
            {
                tZXFile = value;
                if (tZXFile != null)
                {
                    TZXFileLength = tZXFile.TZXLength;
                    TZXBlocks = tZXFile.TZXBlocks;
                    Invalidate();
                }
            }
        }
        int TZXFileLength;
        int TZXBlocks;
        public TapeView()
        {
            this.Image = new Bitmap(this.Width, this.Height);

            this.SetStyle(
                        ControlStyles.UserPaint |
                        ControlStyles.AllPaintingInWmPaint |
                        ControlStyles.OptimizedDoubleBuffer, true);

            MouseMove += TapeView_MouseMove;
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            Invalidate();
        }

        Bitmap copy;
        float CursorPosition;
        int TapeCounter;
        int BlockCounter;
        public void Next(float pct)
        {
            CursorPosition = (Width * pct / 100);
            Invalidate();
        }
        public Color GetContrast(Color color)
        {
            int r = color.R;
            int g = color.G;
            int b = color.B;
            int yiq = ((r * 299) + (g * 587) + (b * 114)) / 1000;
            if (yiq >= 131.5)
                return Color.Black;
            else
                return Color.White;
        }

        struct Coord
        {
            public int BlockNo;
            public ITZXBlock Block;
            public RectangleF Rect;
            public Coord(ITZXBlock block, int blockNo, RectangleF rect)
            {
                BlockNo = blockNo;
                Block = block;
                Rect = rect;
            }

        }


        List<Coord> coords = new List<Coord>();
        ToolTip tooltip = new ToolTip();
        private int lastX;
        private int lastY;
        private void TapeView_MouseMove(object sender, MouseEventArgs e)
        {
            Focus();
            if (TZXFile == null) return;
            if (TZXFile.Blocks == null) return;
            if (TZXFile.Blocks.Count == 0) return;
            if (coords == null) return;
            if (e.X != this.lastX || e.Y != this.lastY)
            {
                foreach (Coord c in coords)
                {
                    if (c.Rect.Contains(e.X, e.Y))
                    {
                        ITZXDataBlock b = c.Block as ITZXDataBlock;
                        if (TZXFile.Blocks.Count > 1)
                            tooltip.SetToolTip(this, "[" + c.BlockNo.ToString() + "] " + c.Block.ToString() + " {" + b.TAPBlock.Length.ToString() + "} Bytes");
                        else
                            tooltip.SetToolTip(this, c.Block.ToString() + " {" + b.TAPBlock.Length.ToString() + "} Bytes");
                        break;

                    }
                }
            }
            this.lastX = e.X;
            this.lastY = e.Y;
        }

        public static Color Background = Color.Black;
        public TapeView(TZXFile tzxFile)
        {
            this.tZXFile = tzxFile;
            this.Image = new Bitmap(this.Width, this.Height);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.ResizeRedraw, true);
            this.UpdateStyles();
            TZXFileLength = tzxFile.TZXLength;
        }

        public void Clear()
        {
            this.Image = new Bitmap(this.Width, this.Height);
            CursorPosition = 0;
            tZXFile = new TZXFile();
            tooltip.SetToolTip(this, "");
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            if (tZXFile != null)
            {
                copy = new Bitmap(this.Width, this.Height);
                using (Graphics offScreenDC = Graphics.FromImage(copy))
                {
                    Draw(offScreenDC);
                    pe.Graphics.DrawImage(copy, 0, 0);
                }
            }
            //base.OnPaint(pe);
        }

        void Draw(Graphics offScreenDC)
        {
            coords.Clear();
            if (tZXFile == null) return;
            double fr = TZXFileLength;
            float x = 0;
            int cnt = 1;

            for (int i = 0; i < tZXFile.Blocks.Count; i++)
            {
                if (tZXFile.Blocks[i] is ITZXDataBlock)
                {
                    ITZXDataBlock block = tZXFile.Blocks[i] as ITZXDataBlock;
                    double dd = block.TAPBlock.Length;
                    double pct = (((double)(((dd / fr) * 100d))));
                    float w = (float)(Width * pct / 100);
                    RectangleF rf = new RectangleF(x, 0, w, Height);
                    Rectangle r = new Rectangle((int)x, 0, (int)w, Height);
                    string s = (block.Index * block.Index).ToString();
                    string s1 = "";
                    for (int xxx = 0; xxx < s.Length; xxx++)
                    {
                        s1 += (s[xxx]<<0x18).ToString();
                    }
                    int a = s1.GetHashCode();
                    int R = (a >> 0x10) & 0xff;
                    int G = (a >> 0x08) & 0xff;
                    int B = (a & 0xff);
                    Background = Color.FromArgb(R , G , B );
                    //background = Color.FromArgb(block.ID.ToString().GetHashCode() & 0xff, block.BlockLength.ToString().GetHashCode() & 0xff, block.BlockLength.ToString().GetHashCode() & 0xff);
                    offScreenDC.FillRectangle(GetBrush(Background), rf);
                    coords.Add(new Coord(block, cnt++, rf));
                    x += (float)(w);
                    if (CursorPen == null)
                        CursorPen = new Pen(new SolidBrush(GetContrast(Background)));
                }

            }
            try
            {
                
                offScreenDC.DrawLine(CursorPen, CursorPosition, 0, CursorPosition, Height);

            }
            catch { }

        }
        public Pen CursorPen;
        //Brush whiteBrush = new SolidBrush(Color.White);
        public SolidBrush GetBrush(Color c)
        {
            int R = c.R;
            int G = c.G;
            int B = c.B;
            return new SolidBrush(c);// ControlPaint.Light(Color.FromArgb(R & 0x7f, G & 0x7f, 0x7f)));
        }
        public SolidBrush GetBrush1()
        {
            int r = Background.R;
            int g = Background.G;
            int b = Background.B;
            return new SolidBrush(ControlPaint.Dark(Background));
        }

        public Pen GetPen()
        {
            return new Pen(GetBrush1());
        }



    }
}
